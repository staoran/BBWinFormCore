using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

public class FieldControlConfigValidator : AbstractValidator<Entity.Security.FieldControlConfig>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public FieldControlConfigValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public FieldControlConfigValidator(OperationType operationType = OperationType.View)
    {
    }
}