using BB.Entity.Security;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

public class RoleDataValidator : AbstractValidator<RoleDataInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public RoleDataValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public RoleDataValidator(OperationType operationType = OperationType.View)
    {
    }
}