using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

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
        #region 实体通用验证

        // 费用类型编号
        RuleFor(x => x.CostType).IsNumber(true, 10).IsGo();
        // 费用类型
        RuleFor(x => x.CostTypeDesc).IsLength(true, 100).IsGo();
        // 正负
        RuleFor(x => x.Ctrl).IsEmpty().IsGo();
        // 适用范围
        RuleFor(x => x.UseType).IsLength(false, 1).IsGo();
        // 付款网点类型
        RuleFor(x => x.PayNodeType).IsLength(true, 1).IsGo();
        // 收款网点类型
        RuleFor(x => x.RecvNodeType).IsLength(true, 1).IsGo();
        // 付款入账类型
        RuleFor(x => x.PayPostType).IsLength(true, 1).IsGo();
        // 收款入账类型
        RuleFor(x => x.RecvPostType).IsLength(true, 1).IsGo();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 250).IsGo();
        // 审核人
        RuleFor(x => x.AppUser).IsLength(false, 20).IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 20).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 修改人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 20).IsGo();
        // 修改时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();

        #endregion

        #region 参数值唯一性验证

        RuleFor(x => x.CostType).IsUnique().IsAdd().IsGo();;
        RuleFor(x => x.CostTypeDesc).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}