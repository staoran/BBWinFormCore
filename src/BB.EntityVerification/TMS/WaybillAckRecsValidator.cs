using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 回单操作记录 验证类
/// </summary>
public class WaybillAckRecsValidator : AbstractValidator<WaybillAckRecs>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public WaybillAckRecsValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public WaybillAckRecsValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 回单编号
        RuleFor(x => x.AckRecNo).IsLength(true, 50).IsGo();
        // 当前网点
        RuleFor(x => x.TranNode).IsLength(true, 20).IsGo();
        // 上一/下一网点
        RuleFor(x => x.TranNodePN).IsLength(false, 20).IsGo();
        // 当前状态
        RuleFor(x => x.StatusID).IsLength(true, 2).IsGo();
        // 相关人员
        RuleFor(x => x.RelatedUser).IsLength(true, 20).IsGo();
        // 车标号
        RuleFor(x => x.CarMarkNo).IsLength(false, 50).IsGo();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 200).IsGo();
        // 作废
        RuleFor(x => x.CancelYN).IsEmpty().IsGo();
        // 作废人
        RuleFor(x => x.CancelBy).IsLength(false, 20).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 20).IsGo();
        // 修改时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();
        // 修改人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 20).IsGo();

        #endregion
    }
}