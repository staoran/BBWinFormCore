using BB.Entity.Security;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

public class MenuValidator : AbstractValidator<MenuInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public MenuValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public MenuValidator(OperationType operationType = OperationType.View)
    {
    }
}