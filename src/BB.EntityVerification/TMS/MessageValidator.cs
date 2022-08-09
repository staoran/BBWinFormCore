using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 问题件 验证类
/// </summary>
public class MessageValidator : AbstractValidator<Message>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public MessageValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public MessageValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 问题件编号
        RuleFor(x => x.MsgNo).IsLength(true, 50).IsGo();
        // 问题件类型
        RuleFor(x => x.MsgType).IsLength(true, 20).IsGo();
        // 运单号
        RuleFor(x => x.WaybillNo).IsLength(true, 50).IsGo();
        // 发送方网点
        RuleFor(x => x.SendMsgNode).IsLength(true, 50).IsGo();
        // 问题件内容
        RuleFor(x => x.SendMsgContent).IsEmpty().IsGo();
        // 接收方网点
        RuleFor(x => x.RecvMsgNode).IsLength(true, 50).IsGo();
        // 处理状态
        RuleFor(x => x.DealStatus).IsLength(true, 2).IsGo();
        // 附件地址
        RuleFor(x => x.AttaPath).IsLength(false, 200).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 20).IsGo();
        // 修改时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();
        // 修改人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 50).IsGo();
        // 审核人
        RuleFor(x => x.AppUser).IsLength(false, 20).IsGo();

        #endregion

        #region 参数值唯一性验证


        #endregion
    }
}