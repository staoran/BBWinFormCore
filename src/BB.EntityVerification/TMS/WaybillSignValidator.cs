using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 签收表 验证类
/// </summary>
public class WaybillSignValidator : AbstractValidator<WaybillSign>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public WaybillSignValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public WaybillSignValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 自增ID
        RuleFor(x => x.ISID).IsEmpty().IsGo();
        // 运单编号
        RuleFor(x => x.WaybillNo).IsLength(true, 50).IsGo();
        // 目的网点编号
        RuleFor(x => x.TranNode).IsLength(true, 20).IsGo();
        // 目的区域编号
        RuleFor(x => x.TranNodes).IsEmpty().IsGo();
        // 交货类型
        RuleFor(x => x.DeliveryType).IsLength(true, 2).IsGo();
        // 签收人
        RuleFor(x => x.Consignee).IsLength(true, 20).IsGo();
        // 签收人证件编号
        RuleFor(x => x.Consigneeid).IsLength(false, 50).IsGo();
        // 签收图片地址
        RuleFor(x => x.ConsigneeidPicAdds).IsLength(false, 200).IsGo();
        // 签收备注
        RuleFor(x => x.ConsigneeRemark).IsLength(false, 200).IsGo();
        // 回单编号
        RuleFor(x => x.AckRecNo).IsLength(false, 50).IsGo();
        // 回单备注
        RuleFor(x => x.AckRecRemark).IsLength(false, 200).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 20).IsGo();
        // 修改时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();
        // 修改人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 20).IsGo();
        // 审核
        RuleFor(x => x.FlagApp).IsEmpty().IsGo();
        // 审核人
        RuleFor(x => x.AppUser).IsLength(false, 20).IsGo();

        #endregion
    }
}