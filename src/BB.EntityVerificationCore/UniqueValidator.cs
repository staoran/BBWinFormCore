using System.Linq.Expressions;
using System.Reflection;
using BB.Core.Services.Base;
using BB.Entity.Base;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Furion;

namespace BB.EntityVerificationCore;

/// <summary>
/// 是否唯一验证
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TProperty"></typeparam>
public class UniqueValidator<T, TProperty> : AsyncPropertyValidator<T, TProperty> where T : BaseEntity, new()
{
    private readonly Expression<Func<T, object>> _expression;
    private readonly OperationType _operationType;

    public UniqueValidator(Expression<Func<T, object>> expression = null, OperationType operationType = OperationType.View)
    {
        _expression = expression;
        _operationType = operationType;
    }

    public override async Task<bool> IsValidAsync(ValidationContext<T> context, TProperty value, CancellationToken cancellation)
    {
        if (_operationType is OperationType.Add or OperationType.Edit && value != null)
        {
            try
            {
                if (_operationType is OperationType.Edit && _expression == null)
                {
                    throw new ArgumentException("编辑时，参数唯一验证必须指定主键");
                }

                object primaryKeyValue = null;
                string primaryKeyName = null;
                if (_expression != null)
                {
                    // 获取主键值
                    primaryKeyValue = _expression.Compile().Invoke(context.InstanceToValidate);
                    // 获取主键类型
                    MemberInfo member = _expression.GetMember();
                    // 获取主键类型的特性
                    var customAttribute = member.GetCustomAttribute<ColumnAttribute>();
                    // 通过特性中的配置得到主键字段名
                    primaryKeyName = customAttribute != null ? customAttribute.Name : string.Empty;
                }

                if (_operationType == OperationType.Edit &&
                    (primaryKeyName.IsNullOrEmpty() || primaryKeyValue.ObjToStr().IsNullOrEmpty()))
                {
                    throw new ArgumentException("没有正确提供主键名或主键值，无法验证编辑时的唯一性");
                }

                var bll = App.GetService<BaseService<T>>();
                
                // 反射获取当前实体字段的数据库字段名
                object propertyObjName =  ReflectionExtension.GetFieldValue(typeof(T), $"Field{context.PropertyName}");
                string propertyName = propertyObjName == null ? context.PropertyName : propertyObjName.ObjToStr();
                string parameterDis = context.DisplayName;
                // TODO 放到BLL中处理操作类型判断
                switch (_operationType)
                {
                    case OperationType.Add:
                        await bll.CheckUniqueAsync(propertyName, parameterDis, value);
                        break;
                    case OperationType.Edit:
                        await bll.CheckUniqueAsync(propertyName, parameterDis, value,
                            primaryKeyName, primaryKeyValue);
                        break;
                }

                return true;
            }
            catch (Exception e)
            {
                context.AddFailure(e.Message);
                Console.WriteLine(e);
                return false;
            }
        }

        return true;
    }

    public override string Name => "UniqueValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "输入的 {PropertyName} 值 {PropertyValue} 已存在，不能重复添加！";
}

/// <summary>
/// 唯一验证扩展
/// </summary>
public static class UniqueValidatorExtension
{
    /// <summary>
    /// 是否唯一
    /// </summary>
    /// <typeparam name="T">正在验证的对象类型</typeparam>
    /// <typeparam name="TProperty">正在验证的属性类型</typeparam>
    /// <param name="ruleBuilder">规则构建器</param>
    /// <param name="expression">指定主键的表达式</param>
    /// <param name="operationType">当前操作类型</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> IsUnique<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Expression<Func<T, object>> expression = null, OperationType operationType = OperationType.View) where T : BaseEntity, new()
    {
        return ruleBuilder.SetAsyncValidator(new UniqueValidator<T, TProperty>(expression, operationType));
    }
}