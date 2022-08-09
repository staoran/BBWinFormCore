using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 费用调整 验证类
/// </summary>
public class CostMsgValidator : AbstractValidator<CostMsg>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public CostMsgValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public CostMsgValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 费用调整编号
        RuleFor(x => x.CostMsgNo).IsLength(true, 50).IsGo();
        // 来源类型
        RuleFor(x => x.SourceType).IsLength(true, 20).IsGo();
        // 运单编号
        RuleFor(x => x.WaybillNo).IsLength(false, 50).IsGo();
        // 申请网点
        RuleFor(x => x.SendMsgNode).IsLength(true, 50).IsGo();
        // 内容描述
        RuleFor(x => x.SendMsgContent).IsEmpty().IsGo();
        // 附件
        RuleFor(x => x.AttaPath).IsLength(false, 200).IsGo();
        // 接收类型
        RuleFor(x => x.RecvMsgType).IsLength(true, 20).IsGo();
        // 接收网点
        RuleFor(x => x.RecvMsgAccount).IsLength(true, 50).IsGo();
        // 费用类型
        RuleFor(x => x.ValueType).IsLength(true, 20).IsGo();
        // 原始值
        RuleFor(x => x.SourceValue).IsEmpty().IsGo();
        // 修改值
        RuleFor(x => x.ActiveValue).IsEmpty().IsGo();
        // 单据状态
        RuleFor(x => x.StatusID).IsLength(true, 20).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 20).IsGo();
        // 修改时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();
        // 修改人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 50).IsGo();
        // 审批人
        RuleFor(x => x.AppUser).IsLength(false, 20).IsGo();
        // 财务中心
        RuleFor(x => x.FinancialCenter).IsLength(true, 50).IsGo();

        #endregion

        #region 参数值唯一性验证


        #endregion
    }
}