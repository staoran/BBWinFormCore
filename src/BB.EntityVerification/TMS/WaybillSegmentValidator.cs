using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 运单线路表 验证类
/// </summary>
public class WaybillSegmentValidator : AbstractValidator<WaybillSegment>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public WaybillSegmentValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public WaybillSegmentValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 运单编号
        RuleFor(x => x.WaybillNo).IsLength(true, 50).IsGo();
        // 线路编号
        RuleFor(x => x.SegmentNo).IsLength(true, 20).IsGo();
        // 车标号
        RuleFor(x => x.CarmarkNo).IsLength(true, 50).IsGo();
        // 线路类型
        RuleFor(x => x.SegmentType).IsLength(true, 2).IsGo();
        // 线路名称
        RuleFor(x => x.SegmentName).IsLength(true, 100).IsGo();
        // 线路起点
        RuleFor(x => x.SegmentBeginNode).IsLength(true, 20).IsGo();
        // 线路终点
        RuleFor(x => x.SegmentEndNode).IsLength(true, 20).IsGo();
        // 起始登记
        RuleFor(x => x.SegmentBeginYN).IsEmpty().IsGo();
        // 起始登记人
        RuleFor(x => x.SegmentBeginUser).IsLength(false, 20).IsGo();
        // 起始登记备注
        RuleFor(x => x.SegmentBeginRemark).IsLength(false, 200).IsGo();
        // 到达登记
        RuleFor(x => x.SegmentEndYN).IsEmpty().IsGo();
        // 到达登记人
        RuleFor(x => x.SegmentEndUser).IsLength(false, 20).IsGo();
        // 到达登记备注
        RuleFor(x => x.SegmentEndRemark).IsLength(false, 200).IsGo();
        // 状态
        RuleFor(x => x.StatusId).IsLength(true, 2).IsGo();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 500).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 50).IsGo();
        // 修改时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();
        // 修改人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 20).IsGo();

        #endregion
    }
}