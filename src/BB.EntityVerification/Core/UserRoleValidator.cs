using BB.Entity.Security;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

public class UserRoleValidator : AbstractValidator<UserRoleEntity>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public UserRoleValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public UserRoleValidator(OperationType operationType = OperationType.View)
    {
    }
}