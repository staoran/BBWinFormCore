using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

/// <summary>
/// 预付金操作类型 验证类
/// </summary>
public class BasicCostBillTypeValidator : AbstractValidator<BasicCostBillType>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public BasicCostBillTypeValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public BasicCostBillTypeValidator(OperationType operationType = OperationType.View)
    {
        #region 参数值唯一性验证

        RuleFor(x => x.CostDesc).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}