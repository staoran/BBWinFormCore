using BB.Entity.Security;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

public class BlackIPValidator : AbstractValidator<BlackIpInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public BlackIPValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public BlackIPValidator(OperationType operationType = OperationType.View)
    {
    }
}