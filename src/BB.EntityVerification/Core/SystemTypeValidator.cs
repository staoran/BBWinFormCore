using BB.Entity.Security;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

public class SystemTypeValidator : AbstractValidator<SystemTypeInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public SystemTypeValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public SystemTypeValidator(OperationType operationType = OperationType.View)
    {
    }
}