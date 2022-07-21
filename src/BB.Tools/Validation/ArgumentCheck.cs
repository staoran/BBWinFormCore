using System.Text.RegularExpressions;
using BB.Tools.Extension;
using BB.Tools.Format;
using BB.Tools.MultiLanuage;

namespace BB.Tools.Validation;

/// <summary>
/// 参数验证帮助类，使用扩展函数实现
/// </summary>
/// <example>
/// eg:
/// ArgumentCheck.Begin().NotNull(sourceArray, "需要操作的数组").NotNull(addArray, "被添加的数组");
/// </example>
public static class ArgumentCheck
{
    #region Methods

    /// <summary>
    /// 验证初始化
    /// <para>
    /// eg:
    /// ArgumentCheck.Begin().NotNull(sourceArray, "需要操作的数组").NotNull(addArray, "被添加的数组");
    /// </para>
    /// <para>
    /// ArgumentCheck.Begin().NotNullOrEmpty(tableName, "表名").NotNullOrEmpty(primaryKey, "主键");</para>
    /// <para>
    /// ArgumentCheck.Begin().CheckLessThan(percent, "百分比", 100, true);</para>
    /// <para>
    /// ArgumentCheck.Begin().CheckGreaterThan&lt;int&gt;(pageIndex, "页索引", 0, false).CheckGreaterThan&lt;int&gt;(pageSize, "页大小", 0, false);</para>
    /// <para>
    /// ArgumentCheck.Begin().NotNullOrEmpty(filepath, "文件路径").IsFilePath(filepath).NotNullOrEmpty(regexString, "正则表达式");</para>
    /// <para>
    /// ArgumentCheck.Begin().NotNullOrEmpty(libFilePath, "非托管DLL路径").IsFilePath(libFilePath).CheckFileExists(libFilePath);</para>
    /// <para>
    /// ArgumentCheck.Begin().InRange(brightnessValue, 0, 100, "图片亮度值");</para>
    /// <para>
    /// ArgumentCheck.Begin().Check&lt;ArgumentNullException&gt;(() => config.HasFile, "config文件不存在。");</para>
    /// <para>
    /// ArgumentCheck.Begin().NotNull(serialPort, "串口").Check&lt;ArgumentException&gt;(() => serialPort.IsOpen, "串口尚未打开！").NotNull(data, "串口发送数据");
    /// </para>
    /// </summary>
    /// <returns>Validation对象</returns>
    public static Validation Begin(object value, string argumentName, bool isValid = false, string message = null)
    {
        return new Validation(value, argumentName, isValid, message);
    }

    /// <summary>
    /// 需要验证的正则表达式
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkFactory">委托</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation Check(this Validation validation, Func<bool> checkFactory, bool checkNull = false,
        int max = 0, string errorText = null)
    {
        if (checkNull && !validation.NotNullOrEmpty().IsValid) return validation;

        if (max > 0 && !validation.InValueLengthRange(false, max).IsValid) return validation;

        if (validation.Value.ObjToStr().IsNullOrEmpty())
        {
            validation.IsValid = true;
            return validation;
        }

        //多语言支持，转换为对应语言的参数名称
        validation.ArgumentName = JsonLanguage.Default.GetString(validation.ArgumentName);
        return Check<ArgumentException>(validation, checkFactory,
            string.Format(errorText ?? ResourceKey.ParameterCheckMatch2, validation.ArgumentName));
    }

    /// <summary>
    /// 自定义参数检查
    /// </summary>
    /// <typeparam name="TException">泛型</typeparam>
    /// <param name="validation">Validation</param>
    /// <param name="checkedFactory">委托</param>
    /// <param name="message">自定义错误消息</param>
    /// <returns>Validation对象</returns>
    public static Validation Check<TException>(this Validation validation, Func<bool> checkedFactory, string message)
        where TException : Exception
    {
        if (validation == null)
            throw new ArgumentException("validation 不可为 null，使用前需初始化。");

        if (checkedFactory())
        {
            validation.IsValid = true;
            return validation;
        }

        var exception = (TException)Activator.CreateInstance(typeof(TException), message);
        throw exception;
    }

    /// <summary>
    /// 检查指定路径的文件夹必须存在，否则抛出<see cref="DirectoryNotFoundException"/>异常。
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <returns>Validation对象</returns>
    public static Validation CheckDirectoryExists(this Validation validation)
    {
        string path = validation.Value.ObjToStr();
        return Check<DirectoryNotFoundException>(validation, () => Directory.Exists(path),
            string.Format(ResourceKey.ParameterCheckDirectoryNotExists, path));
    }

    /// <summary>
    /// 检查文件类型
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="actualFileExt">实际文件类型；eg: .xls</param>
    /// <param name="expectFileExt">期待文件类型</param>
    /// <returns>Validation对象</returns>
    public static Validation CheckedFileExt(this Validation validation, string actualFileExt, string expectFileExt)
    {
        return Check<FileNotFoundException>(validation,
            () => string.Equals(actualFileExt, expectFileExt, StringComparison.OrdinalIgnoreCase),
            string.Format(ResourceKey.ParameterCheckFileExtCompare, expectFileExt));
    }

    /// <summary>
    ///检查文件类型
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="actualFileExt">实际文件类型；eg: .xls</param>
    /// <param name="expectFileExt">期待文件类型</param>
    /// <returns>Validation对象</returns>
    public static Validation CheckedFileExt(this Validation validation, string actualFileExt, string[] expectFileExt)
    {
        string allowFileExts = string.Join(",", expectFileExt);
        return Check<FileNotFoundException>(validation, () => expectFileExt.ContainIgnoreCase(actualFileExt),
            string.Format(ResourceKey.ParameterCheckFileExtCompare, allowFileExts));
    }

    /// <summary>
    /// 检查指定路径的文件必须存在，否则抛出<see cref="FileNotFoundException"/>异常。
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <returns>Validation对象</returns>
    public static Validation CheckFileExists(this Validation validation)
    {
        string filePath = validation.Value.ObjToStr();
        return Check<FileNotFoundException>(validation, () => System.IO.File.Exists(filePath),
            string.Format(ResourceKey.ParameterCheckFileNotExists, filePath));
    }

    /// <summary>
    /// 检查参数必须大于[或可等于，参数canEqual]指定值，否则抛出<see cref="ArgumentOutOfRangeException"/>异常。
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    /// <param name="validation">Validation</param>
    /// <param name="value">判断数据</param>
    /// <param name="paramName">参数名称</param>
    /// <param name="target">要比较的值</param>
    /// <param name="canEqual">是否可等于</param>
    /// <exception cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
    /// <returns>Validation对象</returns>
    public static Validation CheckGreaterThan<T>(this Validation validation, T value, string paramName, T target,
        bool canEqual)
        where T : IComparable<T>
    {
        // bool flag = canEqual ? value.CompareTo(target) >= 0 : value.CompareTo(target) > 0;
        string format = canEqual
            ? ResourceKey.ParameterCheckNotGreaterThanOrEqual
            : ResourceKey.ParameterCheckNotGreaterThan;
        return Check<ArgumentOutOfRangeException>(validation,
            () => canEqual ? value.CompareTo(target) >= 0 : value.CompareTo(target) > 0,
            string.Format(format, paramName, target));
    }

    /// <summary>
    /// 检查参数必须小于[或可等于，参数canEqual]指定值，否则抛出<see cref="ArgumentOutOfRangeException"/>异常。
    /// </summary>
    /// <typeparam name="T">参数类型</typeparam>
    /// <param name="validation">Validation</param>
    /// <param name="value">判断数据</param>
    /// <param name="paramName">参数名称</param>
    /// <param name="target">要比较的值</param>
    /// <param name="canEqual">是否可等于</param>
    /// <exception cref="ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
    /// <returns>Validation对象</returns>
    public static Validation CheckLessThan<T>(this Validation validation, T value, string paramName, T target,
        bool canEqual)
        where T : IComparable<T>
    {
        string format = canEqual ? ResourceKey.ParameterCheckNotLessThanOrEqual : ResourceKey.ParameterCheckNotLessThan;
        return Check<ArgumentOutOfRangeException>(validation,
            () => canEqual ? value.CompareTo(target) <= 0 : value.CompareTo(target) < 0,
            string.Format(format, paramName, target));
    }

    /// <summary>
    /// 验证是否在范围内
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="max">最大值</param>
    /// <param name="min">最小值</param>
    /// <param name="argumentName">参数名称</param>
    /// <returns>Validation对象</returns>
    public static Validation InRange(this Validation validation, int max, int min = 0)
    {
        //多语言支持，转换为对应语言的参数名称
        validation.ArgumentName = JsonLanguage.Default.GetString(validation.ArgumentName);
        int length = validation.Value.ObjToInt();
        return Check<ArgumentOutOfRangeException>(validation, () => length >= min && length <= max,
            string.Format(ResourceKey.ParameterCheckBetween, validation.ArgumentName, min, max));
    }

    /// <summary>
    /// 验证参数长度
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="min">最小长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation InValueLengthRange(this Validation validation, bool checkNull, int max, int min = 0,
        string errorText = null)
    {
        if (checkNull && max < 1)
            return validation.NotNullOrEmpty();

        if (max > 0)
        {
            //多语言支持，转换为对应语言的参数名称
            validation.ArgumentName = JsonLanguage.Default.GetString(validation.ArgumentName);
            var length = 0;
            if (validation.Value is bool value)
            {
                length = value ? 1 : 0;
            }
            else
            {
                length = validation.Value.ObjToStr().Length;
            }
            return Check<ArgumentOutOfRangeException>(validation, () => length >= min && length <= max,
                errorText ?? $"参数“{validation.ArgumentName}”的长度必须在“{min}”与“{max}”之间。");
        }

        validation.IsValid = true;
        return validation;
    }

    /// <summary>
    /// 用户名格式是否有效
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsUserName(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsValidUserName(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是用户名格式");
    }

    /// <summary>
    /// 是否是包含中文
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsChinese(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsChinese(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}中不包含中文字符");
    }

    /// <summary>
    /// 是否是电子邮箱
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsEmail(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsEmail(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是合法的邮箱地址");
    }

    /// <summary>
    /// 是否是文件路径
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsFilePath(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        string data = validation.Value.ObjToStr();
        return Check(validation, () => ValidateUtil.IsFilePath(data),
            checkNull, max, errorText ?? string.Format(ResourceKey.ParameterCheckIsFilePath, data));
    }

    /// <summary>
    /// 是否是十六进制字符串
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsHexString(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsHexString(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是合法的十六进制字符串");
    }

    /// <summary>
    /// 是否是身份证号码
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsIdCard(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsIdCard(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是合法的身份证号码");
    }

    /// <summary>
    /// 是否是整数
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsInt(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsInt(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是整数");
    }

    /// <summary>
    /// 是否是IP
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsIp(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsIp(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是合法的IP地址");
    }

    /// <summary>
    /// 是否是正整数
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsNumber(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsNumberSign(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是正整数");
    }

    /// <summary>
    /// 是否是数字
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsNumeric(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsNumeric(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是数字");
    }

    /// <summary>
    /// 是否是小数
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsDecimal(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsDecimal(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是小数");
    }

    /// <summary>
    /// 是否是固定电话
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsPhone(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsPhone(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是合法的固定电话");
    }

    /// <summary>
    /// 是否是手机或电话
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsPhoneAndMobile(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsPhoneAndMobile(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是合法的手机或电话号码");
    }

    /// <summary>
    /// 是否是字母
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsLetter(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsLetter(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是合法的字母");
    }

    /// <summary>
    /// 是否是手机号码
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsMobile(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsMobile(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是合法的手机号码");
    }

    /// <summary>
    /// 是否是合法端口
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsPort(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsPort(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? ResourceKey.ParameterCheckPort);
    }

    /// <summary>
    /// 是否是邮政编码
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsPostCode(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsPostCode(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是合法的邮政编码");
    }

    /// <summary>
    /// 判断字符串是否是要求的长度
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="requireLength">要求的长度</param>
    /// <param name="argumentName">参数名称</param>
    /// <returns>Validation对象</returns>
    public static Validation IsRequireLen(this Validation validation, bool checkNull = false, int requireLength = 0,
        string errorText = null)
    {
        return Check(validation, () => validation.ObjToStr().Length == requireLength, checkNull, requireLength,
            errorText ?? string.Format(ResourceKey.ParameterCheckStringLength, validation.ArgumentName, requireLength));
    }

    /// <summary>
    /// 判断类型是否能序列化
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsSerializable(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        Type type = validation.Value.GetType();
        return Check(validation, () => type.IsSerializable,
            checkNull, max, errorText ?? string.Format(ResourceKey.ParameterCheckSerializable, type.FullName));
    }

    /// <summary>
    /// 是否是URL
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation IsUrl(this Validation validation, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        return Check(validation, () => ValidateUtil.IsUrl(validation.Value.ObjToStr()),
            checkNull, max, errorText ?? "{0}不是合法的URL");
    }

    /// <summary>
    /// 验证参数不能等于某个值
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="equalObj">比较项</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation NotEqual(this Validation validation, object equalObj, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        object data = validation.Value;
        return Check(validation, () => data != equalObj, checkNull, max,
            errorText ?? string.Format(ResourceKey.ParameterCheckNotEqual, validation.ArgumentName, data));
    }

    /// <summary>
    /// 验证非空
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <returns>Validation对象</returns>
    public static Validation NotNull(this Validation validation)
    {
        //多语言支持，转换为对应语言的参数名称
        validation.ArgumentName = JsonLanguage.Default.GetString(validation.ArgumentName);
        return Check<ArgumentNullException>(validation, () => validation.Value != null,
            string.Format(ResourceKey.ParameterCheckNotNull, validation.ArgumentName));
    }

    /// <summary>
    /// 不能为空或者NULL验证
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <returns>Validation对象</returns>
    public static Validation NotNullOrEmpty(this Validation validation)
    {
        //多语言支持，转换为对应语言的参数名称
        validation.ArgumentName = JsonLanguage.Default.GetString(validation.ArgumentName);
        return Check<ArgumentNullException>(validation,
            () => !string.IsNullOrEmpty(validation.Value.ObjToStr()),
            string.Format(ResourceKey.ParameterCheckNotNullOrEmptyString, validation.ArgumentName));
    }

    /// <summary>
    /// 需要验证的正则表达式
    /// </summary>
    /// <param name="validation">Validation</param>
    /// <param name="pattern">正则表达式</param>
    /// <param name="checkNull">验证是否为空</param>
    /// <param name="max">最大长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns>Validation对象</returns>
    public static Validation RegexMatch(this Validation validation, string pattern, bool checkNull = false, int max = 0,
        string errorText = null)
    {
        string input = validation.Value.ObjToStr();
        return Check(validation, () => Regex.IsMatch(input, pattern),
            checkNull, max,
            errorText ?? string.Format(ResourceKey.ParameterCheckMatch, input, validation.ArgumentName));
    }

    /// <summary>
    /// 实体参数值验证
    /// </summary>
    /// <param name="validateType">验证类型</param>
    /// <param name="value">参数值</param>
    /// <param name="argumentName">参数名称</param>
    /// <param name="checkNull">空是否验证</param>
    /// <param name="max">最大长度</param>
    /// <param name="min">最小长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    public static Validation CheckEntityValue(ValidateType validateType, object value, string argumentName,
        bool checkNull = false, int max = 0, int min = 0, string errorText = null)
    {
        Validation validation = Begin(value, argumentName);
        return validateType switch
        {
            ValidateType.UserName => validation.IsUserName(checkNull, max, errorText),
            ValidateType.Phone => validation.IsPhone(checkNull, max, errorText),
            ValidateType.Email => validation.IsEmail(checkNull, max, errorText),
            ValidateType.URL => validation.IsUrl(checkNull, max, errorText),
            ValidateType.Number => validation.IsNumber(checkNull, max, errorText),
            ValidateType.Mobile => validation.IsMobile(checkNull, max, errorText),
            ValidateType.IdCard => validation.IsIdCard(checkNull, max, errorText),
            ValidateType.Chinese => validation.IsChinese(checkNull, max, errorText),
            ValidateType.Decimal => validation.IsDecimal(checkNull, max, errorText),
            ValidateType.Letter => validation.IsLetter(checkNull, max, errorText),
            ValidateType.Numeric => validation.IsNumeric(checkNull, max, errorText),
            ValidateType.FilePath => validation.IsFilePath(checkNull, max, errorText),
            ValidateType.PhoneAndMobile => validation.IsPhoneAndMobile(checkNull, max, errorText),
            _ => validation.InValueLengthRange(checkNull, max, min, errorText)
        };
    }

    #endregion Methods

    /// <summary>
    /// 参数验证实体类
    /// </summary>
    public sealed class Validation
    {
        public Validation(object value, string argumentName, bool isValid = false, string message = null)
        {
            Value = value;
            ArgumentName = argumentName;
            IsValid = isValid;
            Message = message;
        }

        /// <summary>
        /// 待验证的参数
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 待验证参数的名称
        /// </summary>
        public string ArgumentName { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Message { get; set; }
    }
}