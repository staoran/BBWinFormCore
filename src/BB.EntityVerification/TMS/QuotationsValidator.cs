using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 报价明细 验证类
/// </summary>
public class QuotationsValidator : AbstractValidator<Quotations>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public QuotationsValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public QuotationsValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 报价编号
        RuleFor(x => x.QuotationNo).IsLength(true, 30).IsGo();
        // 报价类型
        RuleFor(x => x.QuotationType).IsLength(true, 2).IsGo();
        // 起始组
        RuleFor(x => x.FromGroups).IsEmpty().IsGo();
        // 到达组
        RuleFor(x => x.ToGroups).IsEmpty().IsGo();
        // 最小金额
        RuleFor(x => x.MinCost).IsEmpty().IsGo();
        // 最大金额
        RuleFor(x => x.MaxCost).IsEmpty().IsGo();
        // 首值金额
        RuleFor(x => x.FirstCost).IsEmpty().IsGo();
        // 首值
        RuleFor(x => x.FirstValue).IsEmpty().IsGo();
        // 最小值
        RuleFor(x => x.MinValue).IsEmpty().IsGo();
        // 最大值
        RuleFor(x => x.MaxValue).IsEmpty().IsGo();
        // 单价
        RuleFor(x => x.UnitPrice).IsEmpty().IsGo();
        // 单价加成
        RuleFor(x => x.UnitPricePer).IsEmpty().IsGo();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 200).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 20).IsGo();
        // 修改时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();
        // 修改人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 50).IsGo();

        #endregion

        #region 参数值唯一性验证


        #endregion
    }
}