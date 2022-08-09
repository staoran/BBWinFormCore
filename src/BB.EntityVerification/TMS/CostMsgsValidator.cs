using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 费用调整确认 验证类
/// </summary>
public class CostMsgsValidator : AbstractValidator<CostMsgs>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public CostMsgsValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public CostMsgsValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 费用调整编号
        RuleFor(x => x.CostMsgNo).IsLength(true, 50).IsGo();
        // 单据状态
        RuleFor(x => x.StatusID).IsLength(true, 20).IsGo();
        // 回复网点
        RuleFor(x => x.RecvMsgNode).IsLength(true, 50).IsGo();
        // 回复内容
        RuleFor(x => x.RecvMsgContent).IsEmpty().IsGo();
        // 附件
        RuleFor(x => x.AttaPath).IsLength(false, 200).IsGo();
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