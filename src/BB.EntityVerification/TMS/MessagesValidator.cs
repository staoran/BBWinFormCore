using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 问题件回复 验证类
/// </summary>
public class MessagesValidator : AbstractValidator<Messages>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public MessagesValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public MessagesValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 问题件编号
        RuleFor(x => x.MsgNo).IsLength(true, 50).IsGo();
        // 处理状态
        RuleFor(x => x.DealStatus).IsLength(true, 2)
            .Must(x => x != "9").WithMessage("回复问题件时不可指定为作废状态。")
            .When(x => x.DealContent.IsNullOrEmpty(), ApplyConditionTo.CurrentValidator).IsGo();
        // 处理内容
        RuleFor(x => x.DealContent).IsEmpty().IsGo();
        // 附件地址
        RuleFor(x => x.AttaPath).IsLength(false, 50).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 50).IsGo();
        // 修改时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();
        // 修改人
        RuleFor(x => x.LastUpdateBy).IsLength(false, 50).IsGo();

        #endregion

        #region 参数值唯一性验证

        #endregion
    }
}