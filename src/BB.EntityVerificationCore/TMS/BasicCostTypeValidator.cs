using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

/// <summary>
/// 费用类型 验证类
/// </summary>
public class BasicCostTypeValidator : AbstractValidator<BasicCostType>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public BasicCostTypeValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public BasicCostTypeValidator(OperationType operationType = OperationType.View)
    {
        #region 参数值唯一性验证

        RuleFor(x => x.CostType).IsUnique().IsAdd().IsGo();;
        RuleFor(x => x.CostTypeDesc).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}