using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using BB.Tools.Entity;
using BB.Tools.Extension;
using FluentValidation;
using FluentValidation.Results;

namespace BB.Tools.Validation;

/// <summary>
/// 自定义验证器扩展
/// </summary>
public static class CustomValidatorExtensions
{
    /// <summary>
    /// 字段显示名称解析器
    /// </summary>
    public static readonly Func<Type, MemberInfo, LambdaExpression, string> DisplayNameResolver =
        (containerType, member, expression) =>
        {
            if (member == null)
                return string.Empty;
            string? dis =
                member.GetAttribute<ColumnAttribute, string>(x => x.Display.IsNullOrEmpty() ? x.Name : x.Display);
            return dis.IsNullOrEmpty() ? member.Name : dis!;
        };

    #region 同步验证扩展

    /// <summary>
    /// 执行验证，如果验证失败则抛出异常。
    /// 这个方法是一个快捷方式：Validate(instance, options => options.ThrowOnFailures());
    /// </summary>
    /// <param name="validator">此方法正在扩展的验证器</param>
    /// <param name="obj">正在验证的类型的实例或hash</param>
    /// <param name="operationType">当前操作类型</param>
    public static void ValidateAndThrow<T>(this IValidator<T> validator, object obj,
        OperationType operationType = OperationType.View)
    {
        validator.Validate(obj, operationType, true);
    }

    /// <summary>
    /// 执行验证
    /// </summary>
    /// <param name="validator">此方法正在扩展的验证器</param>
    /// <param name="obj">正在验证的类型的实例或hash</param>
    /// <param name="operationType">当前操作类型</param>
    /// <param name="isThrow">是否抛异常</param>
    public static ValidationResult Validate<T>(this IValidator<T> validator, object obj,
        OperationType operationType = OperationType.View, bool isThrow = false)
    {
        T entity;
        Hashtable? ht = null;
        switch (obj)
        {
            case T node:
                entity = node;
                break;
            case Hashtable h:
                entity = h.ToObject<T>();
                ht = h;
                break;
            default:
                throw new ArgumentException("参数类型错误, 只能为 实体 或 Hashtable");
        }
        
        var context = ValidationContext<T>.CreateWithOptions(entity, options =>
        {
            if (isThrow) options.ThrowOnFailures(); // 抛出异常
        });
        // 根上下文增加基础数据
        context.RootContextData.Add("hashData", ht);
        context.RootContextData.Add("operationType", operationType);
        return validator.Validate(context);
    }

    /// <summary>
    /// 待验证的源数据中包含当前 lambda 中指定的字段才执行
    /// </summary>
    /// <param name="rule">目前的规则</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> IsGo<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule)
    {
        return rule.When((_, y) =>
        {
            // .When(x => hashTable == null || hashTable.ContainsKey(nameof(x.TranNodeNO)))
            if (!y.RootContextData.ContainsKey("hashData")) return true;
            return y.RootContextData["hashData"] is not Hashtable ht || ht.ContainsKey(y.PropertyName);
        });
    }

    /// <summary>
    /// 后台新增修改时执行
    /// </summary>
    /// <param name="rule">目前的规则</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> IsAddOrEdit<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule)
    {
        // .When(customer => customer.IsPreferredCustomer, ApplyConditionTo.CurrentValidator)
        // ApplyConditionTo.CurrentValidator 控制作用范围，只对当前规则生效，默认是 ApplyConditionTo.AllValidators，对所有规则链生效
        return rule.When((_, y) =>
        {
            if (!y.RootContextData.ContainsKey("operationType")) return true;
            return y.RootContextData["operationType"] is OperationType.Add or OperationType.Edit;
        }, ApplyConditionTo.CurrentValidator);
    }

    /// <summary>
    /// 指定验证器何时运行的条件限制。
    /// 只有当 lambda 的结果返回 true 时，才会执行验证器。
    /// </summary>
    /// <param name="rule">目前的规则</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> IsAdd<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule)
    {
        return rule.When((_, y) =>
        {
            if (!y.RootContextData.ContainsKey("operationType")) return true;
            return y.RootContextData["operationType"] is OperationType.Add;
        }, ApplyConditionTo.CurrentValidator);
    }

    /// <summary>
    /// 按 ValidateType 验证
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="validateType">验证类型</param>
    /// <param name="checkNull">空是否验证</param>
    /// <param name="max">最大长度</param>
    /// <param name="min">最小长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> CheckByValidateType<T>(this IRuleBuilder<T, string> ruleBuilder, ValidateType validateType,
        bool checkNull = false, int max = 0, int min = 0, string? errorText = null)
    {
        return validateType switch
        {
            ValidateType.UserName => ruleBuilder.IsUserName(checkNull, max, errorText),
            ValidateType.Phone => ruleBuilder.IsPhone(checkNull, max, errorText),
            ValidateType.Email => ruleBuilder.IsEmail(checkNull, max, errorText),
            ValidateType.URL => ruleBuilder.IsUrl(checkNull, max, errorText),
            ValidateType.Number => ruleBuilder.IsNumber(checkNull, max, errorText),
            ValidateType.Mobile => ruleBuilder.IsMobile(checkNull, max, errorText),
            ValidateType.IdCard => ruleBuilder.IsIdCard(checkNull, max, errorText),
            ValidateType.Chinese => ruleBuilder.IsChinese(checkNull, max, errorText),
            ValidateType.Decimal => ruleBuilder.IsDecimal(checkNull, max, errorText),
            ValidateType.Letter => ruleBuilder.IsLetter(checkNull, max, errorText),
            ValidateType.Numeric => ruleBuilder.IsNumeric(checkNull, max, errorText),
            ValidateType.FilePath => ruleBuilder.IsFilePath(checkNull, max, errorText),
            ValidateType.PhoneAndMobile => ruleBuilder.IsPhoneAndMobile(checkNull, max, errorText),
            _ => ruleBuilder.IsLength(checkNull, max, min, errorText)
        };
    }

    /// <summary>
    /// 自定义委托验证
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkFactory">委托</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> Check<T>(this IRuleBuilder<T, string> ruleBuilder, Func<string, bool> checkFactory, bool checkNull = false,
        int max = 0, string? errorText = null)
    {
        return ruleBuilder.IsLength(checkNull, max)
            .Must(m=>
            {
                if (m.IsNullOrEmpty())
                {
                    return !checkNull;
                }
                return checkFactory(m);
            }).WithMessage(errorText ?? "参数 {PropertyName} 的格式不正确！");
    }

    /// <summary>
    /// 验证参数长度
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="min">最小长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static IRuleBuilderOptions<T, string> IsLength<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull, int max, int min = 0,
        string? errorText = null)
    {
        if (checkNull) ruleBuilder = ruleBuilder.NotEmpty().WithMessage("{PropertyName} 不可为空");

        if (max <= 0 && min <= 0) return (IRuleBuilderOptions<T, string>)ruleBuilder;
        errorText ??= max switch
        {
            > 0 when min == 0 => "{PropertyName}不能超过" + max + "个字符",
            > 0 when min > 0 => "{PropertyName}的长度必须在" + min + "-" + max + "个字符之间",
            0 when min > 0 => "{PropertyName}不能小于" + min + "个字符",
            _ => throw new ArgumentOutOfRangeException(nameof(max), max, null)
        };

        return ruleBuilder.Length(min, max).WithMessage(errorText);
    }

    /// <summary>
    /// 验证参数是否为空，是否符合长度要求
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static IRuleBuilderOptions<T, string> IsEmpty<T>(this IRuleBuilder<T, string> ruleBuilder, int max,
        string? errorText = null)
    {
        return ruleBuilder.IsLength(true, max, 0, errorText);
    }

    /// <summary>
    /// 验证参数是否为空
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <typeparam name="TProperty">正在验证的属性类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static IRuleBuilderOptions<T, TProperty> IsEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder,
        string? errorText = null)
    {
        return ruleBuilder.NotEmpty().WithMessage(errorText ?? "{PropertyName} 不可为空");
    }
    
    /// <summary>
    /// 检查指定路径的文件夹必须存在，否则抛出<see cref="DirectoryNotFoundException"/>异常。
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> DirectoryExists<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} 不可为空")
            .Must(Directory.Exists).WithMessage("指定的目录路径“{PropertyValue}”不存在。");
    }

    /// <summary>
    /// 检查文件类型
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="expectFileExt">期待文件类型</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> FileExt<T>(this IRuleBuilder<T, string> ruleBuilder, string expectFileExt)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} 不可为空")
            .Equal(expectFileExt).WithMessage($"文件类型不正确，期待的文件类型为：{expectFileExt}");
    }

    /// <summary>
    ///检查文件类型
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="expectFileExt">期待文件类型</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> FileExt<T>(this IRuleBuilder<T, string> ruleBuilder, string[] expectFileExt)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} 不可为空")
            .Must(expectFileExt.ContainIgnoreCase).WithMessage($"文件类型不正确，期待的文件类型为：{string.Join(",", expectFileExt)}");
    }

    /// <summary>
    /// 检查指定路径的文件必须存在，否则抛出<see cref="FileNotFoundException"/>异常。
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> FileExists<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("{PropertyName} 不可为空")
            .Must(System.IO.File.Exists).WithMessage("指定的路径“{PropertyValue}”的文件不存在。");
    }

    /// <summary>
    /// 用户名格式是否有效
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsUserName<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsValidUserName, checkNull, max, errorText ?? "{PropertyValue}不是用户名格式");
    }

    /// <summary>
    /// 是否是包含中文
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsChinese<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsChinese, checkNull, max, errorText ?? "{PropertyValue}中不包含中文字符");
    }

    /// <summary>
    /// 是否是电子邮箱
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsEmail<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsEmail, checkNull, max, errorText ?? "{PropertyValue}不是合法的邮箱地址");
    }

    /// <summary>
    /// 是否是文件路径
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsFilePath<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsFilePath, checkNull, max, errorText ?? "{PropertyValue}不是合法的文件路径");
    }

    /// <summary>
    /// 是否是十六进制字符串
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsHexString<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsHexString, checkNull, max, errorText ?? "{PropertyValue}不是合法的十六进制字符串");
    }

    /// <summary>
    /// 是否是身份证号码
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsIdCard<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsIdCard, checkNull, max, errorText ?? "{PropertyValue}不是合法的身份证号码");
    }

    /// <summary>
    /// 是否是整数
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsInt<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsInt, checkNull, max, errorText ?? "{PropertyValue}不是整数");
    }

    /// <summary>
    /// 是否是IP
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsIp<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsIp, checkNull, max, errorText ?? "{PropertyValue}不是合法的IP地址");
    }

    /// <summary>
    /// 是否是正整数
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsNumber<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsNumberSign, checkNull, max, errorText ?? "{PropertyValue}不是正整数");
    }

    /// <summary>
    /// 是否是数字
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsNumeric<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsNumeric, checkNull, max, errorText ?? "{PropertyValue}不是数字");
    }

    /// <summary>
    /// 是否是小数
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsDecimal<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsDecimal, checkNull, max, errorText ?? "{PropertyValue}不是小数");
    }

    /// <summary>
    /// 是否是固定电话
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsPhone<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsPhone, checkNull, max, errorText ?? "{PropertyValue}不是合法的固定电话");
    }

    /// <summary>
    /// 是否是手机或电话
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsPhoneAndMobile<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsPhoneAndMobile, checkNull, max, errorText ?? "{PropertyValue}不是合法的手机或电话号码");
    }

    /// <summary>
    /// 是否是字母
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsLetter<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsLetter, checkNull, max, errorText ?? "{PropertyValue}不是合法的纯字母字符串");
    }

    /// <summary>
    /// 是否是字母数字
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsLetterNum<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsLetterNum, checkNull, max, errorText ?? "{PropertyValue}只能包含大小写字母和数字");
    }

    /// <summary>
    /// 是否是大写字母
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsUpLetter<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsUpLetter, checkNull, max, errorText ?? "{PropertyValue}只能包含大写字母");
    }

    /// <summary>
    /// 是否是大写字母数字
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsUpLetterNum<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsUpLetterNum, checkNull, max, errorText ?? "{PropertyValue}只能包含大写字母和数字");
    }

    /// <summary>
    /// 是否是小写字母
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsLowLetter<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsLowLetter, checkNull, max, errorText ?? "{PropertyValue}只能包含小写字母");
    }

    /// <summary>
    /// 是否是小写字母数字
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsLowLetterNum<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsLowLetterNum, checkNull, max, errorText ?? "{PropertyValue}只能包含小写字母和数字");
    }

    /// <summary>
    /// 是否是手机号码
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsMobile<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsMobile, checkNull, max, errorText ?? "{PropertyValue}不是合法的手机号码");
    }

    /// <summary>
    /// 是否是合法端口
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsPort<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsPort, checkNull, max, errorText ?? "{PropertyValue}不是合法的端口号");
    }

    /// <summary>
    /// 是否是邮政编码
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsPostCode<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsPostCode, checkNull, max, errorText ?? "{0}不是合法的邮政编码");
    }

    /// <summary>
    /// 是否是URL
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> IsUrl<T>(this IRuleBuilder<T, string> ruleBuilder, bool checkNull = false, int max = 0,
        string? errorText = null)
    {
        return Check(ruleBuilder, ValidateUtil.IsUrl, checkNull, max, errorText ?? "{0}不是合法的URL");
    }

    #endregion

    #region 异步验证扩展

    /// <summary>
    /// 执行验证，如果验证失败则抛出异常。
    /// 这个方法是一个快捷方式：Validate(instance, options => options.ThrowOnFailures());
    /// </summary>
    /// <param name="validator">此方法正在扩展的验证器</param>
    /// <param name="obj">正在验证的类型的实例或hash</param>
    /// <param name="operationType">当前操作类型</param>
    public static async Task ValidateAndThrowAsync<T>(this IValidator<T> validator, object obj,
        OperationType operationType = OperationType.View)
    {
        await validator.ValidateAsync(obj, operationType, true);
    }

    /// <summary>
    /// 执行验证
    /// </summary>
    /// <param name="validator">此方法正在扩展的验证器</param>
    /// <param name="obj">正在验证的类型的实例或hash</param>
    /// <param name="operationType">当前操作类型</param>
    /// <param name="isThrow">是否抛异常</param>
    public static async Task<ValidationResult> ValidateAsync<T>(this IValidator<T> validator, object obj,
        OperationType operationType = OperationType.View, bool isThrow = false)
    {
        T entity;
        Hashtable? ht = null;
        switch (obj)
        {
            case T node:
                entity = node;
                break;
            case Hashtable h:
                entity = h.ToObject<T>();
                ht = h;
                break;
            default:
                throw new ArgumentException("参数类型错误, 只能为 实体 或 Hashtable");
        }
        
        var context = ValidationContext<T>.CreateWithOptions(entity, options =>
        {
            if (isThrow) options.ThrowOnFailures(); // 抛出异常
        });
        // 根上下文增加基础数据
        context.RootContextData.Add("hashData", ht);
        context.RootContextData.Add("operationType", operationType);
        return await validator.ValidateAsync(context);
    }

    #endregion
}