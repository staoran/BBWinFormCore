using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 线路表 验证类
/// </summary>
public class SegmentValidator : AbstractValidator<Segment>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public SegmentValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public SegmentValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 线路编号
        RuleFor(x => x.SegmentNo).IsLength(true, 50);
        // 线路类型
        RuleFor(x => x.SegmentType).IsLength(true, 2);
        // 线路名称
        RuleFor(x => x.SegmentName).IsLength(true, 100);
        // 起始网点
        RuleFor(x => x.SegmentBeginNode).IsLength(true, 20);
        // 结束网点
        RuleFor(x => x.SegmentEndNode).IsLength(true, 20);
        // 起始时间
        RuleFor(x => x.PlanBeginTime).IsEmpty();
        // 预估时间（小时）
        RuleFor(x => x.ExpectedHour).IsEmpty();
        // 预估距离
        RuleFor(x => x.ExpectedDistance).IsEmpty();
        // 预估油耗
        RuleFor(x => x.ExpectedOilWear).IsEmpty();
        // 预估开支
        RuleFor(x => x.ExpectedCharge).IsEmpty();
        // 预估路桥费
        RuleFor(x => x.ExpectedPontage).IsEmpty();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 200);
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 20);
        // 修改时间
        RuleFor(x => x.LastUpdateDate).IsEmpty();
        // 修改人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 50);
        // 审核人
        RuleFor(x => x.AppUser).IsLength(false, 20);

        #endregion
    }
}