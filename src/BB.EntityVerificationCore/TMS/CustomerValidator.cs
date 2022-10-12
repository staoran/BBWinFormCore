using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

/// <summary>
/// 客户管理 验证类
/// </summary>
public class CustomerValidator : AbstractValidator<Customer>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public CustomerValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public CustomerValidator(OperationType operationType = OperationType.View)
    {
        #region 参数值唯一性验证

        RuleFor(x => x.CustomerCode).IsUnique().IsAdd().IsGo();;
        RuleFor(x => x.MnemonicCode).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}