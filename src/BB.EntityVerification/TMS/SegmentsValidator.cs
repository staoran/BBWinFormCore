using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 线路报价 验证类
/// </summary>
public class SegmentsValidator : AbstractValidator<Segments>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public SegmentsValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public SegmentsValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 线路编号
        RuleFor(x => x.SegmentNo).IsLength(true, 50).IsGo();
        // 费用类型
        RuleFor(x => x.CostType).IsLength(true, 10).IsGo();
        // 报价编号
        RuleFor(x => x.QuotationNo).IsLength(true, 50).IsGo();
        // 付款网点类型
        RuleFor(x => x.PayNodeType).IsLength(true, 2).IsGo();
        // 付款网点
        RuleFor(x => x.PayNodeNo).IsLength(false, 50).IsGo();
        // 收款网点类型
        RuleFor(x => x.RecvNodeType).IsLength(true, 2).IsGo();
        // 收款网点
        RuleFor(x => x.RecvNodeNo).IsLength(false, 50).IsGo();
        // 启用时间
        RuleFor(x => x.OpenTime).IsEmpty().IsGo();
        // 终止时间
        RuleFor(x => x.Closetime).IsEmpty().IsGo();
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
        // 财务中心类型
        RuleFor(x => x.FinancialCenterType).IsLength(true, 2).IsGo();
        // 财务中心
        RuleFor(x => x.FinancialCenter).IsLength(false, 50).IsGo();

        #endregion

        #region 参数值唯一性验证


        #endregion
    }
}