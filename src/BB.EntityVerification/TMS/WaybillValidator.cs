using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 运单管理 验证类
/// </summary>
public class WaybillValidator : AbstractValidator<Waybill>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public WaybillValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public WaybillValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 运单号
        RuleFor(x => x.WaybillNo).IsLength(true, 50).IsGo();
        // 运单时间
        RuleFor(x => x.WaybillDate).IsEmpty().IsGo();
        // 转入单号
        RuleFor(x => x.TransferInNo).IsLength(false, 50).IsGo();
        // 订单号
        RuleFor(x => x.OrderNo).IsLength(false, 50).IsGo();
        // 起始网点编号
        RuleFor(x => x.FromNode).IsLength(true, 20).IsGo();
        // 起始区域编号
        RuleFor(x => x.FromNodes).IsEmpty().IsGo();
        // 发货省
        RuleFor(x => x.FromProvinceId).IsLength(true, 50).IsGo();
        // 发货市
        RuleFor(x => x.FromCityId).IsLength(true, 50).IsGo();
        // 发货区
        RuleFor(x => x.FromAreaId).IsLength(true, 50).IsGo();
        // 目的网点编号
        RuleFor(x => x.ToNode).IsLength(true, 20).IsGo();
        // 目的区域编号
        RuleFor(x => x.ToNodes).IsEmpty().IsGo();
        // 目的省
        RuleFor(x => x.ToProvinceId).IsLength(true, 50).IsGo();
        // 目的市
        RuleFor(x => x.ToCityId).IsLength(true, 50).IsGo();
        // 目的区
        RuleFor(x => x.ToAreaId).IsLength(true, 50).IsGo();
        // 发货公司
        RuleFor(x => x.ShipperCompanyName).IsLength(false, 100).IsGo();
        // 发货地址
        RuleFor(x => x.ShipperAddress).IsLength(true, 500).IsGo();
        // 发货电话
        RuleFor(x => x.ShipperTel).IsPhoneAndMobile(true, 50).IsGo();
        // 发货人
        RuleFor(x => x.Shipper).IsLength(true, 50).IsGo();
        // 收货公司
        RuleFor(x => x.ConsigneeCompanyName).IsLength(false, 100).IsGo();
        // 收货地址
        RuleFor(x => x.ConsigneeAddress).IsLength(true, 500).IsGo();
        // 收货电话
        RuleFor(x => x.ConsigneeTel).IsPhoneAndMobile(true, 50).IsGo();
        // 收货人
        RuleFor(x => x.Consignee).IsLength(true, 50).IsGo();
        // 运输方式
        RuleFor(x => x.TransportType).IsLength(false, 20).IsGo();
        // 提货方式
        RuleFor(x => x.PickUpType).IsLength(false, 20).IsGo();
        // 交货方式
        RuleFor(x => x.DeliveryType).IsLength(true, 20).IsGo();
        // 付款方式
        RuleFor(x => x.PaymentType).IsLength(true, 20).IsGo();
        // 等通知放货
        RuleFor(x => x.WaitNoticeYN).IsEmpty().IsGo();
        // 通知人员
        RuleFor(x => x.NoticeUser).IsLength(false, 20).IsGo();
        // 通知备注
        RuleFor(x => x.NoticeRemark).IsLength(false, 100).IsGo();
        // 回单号
        RuleFor(x => x.AckRecNo).IsLength(false, 50).IsGo();
        // 异形件
        RuleFor(x => x.AbnormityType).IsEmpty().IsGo();
        // 数量
        RuleFor(x => x.Qty).IsEmpty().IsGo();
        // 重量
        RuleFor(x => x.Weight).IsEmpty().IsGo();
        // 体积
        RuleFor(x => x.Cubage).IsEmpty().IsGo();
        // 计费重量
        RuleFor(x => x.ChargeableWeight).IsEmpty().IsGo();
        // 代收款
        RuleFor(x => x.AgencyReceiveCharge).IsEmpty().IsGo();
        // 现付金额
        RuleFor(x => x.CarriagePrepaid).IsEmpty().IsGo();
        // 到付金额
        RuleFor(x => x.CarriageForward).IsEmpty().IsGo();
        // 月结金额
        RuleFor(x => x.CarriageMonthly).IsEmpty().IsGo();
        // 回付金额
        RuleFor(x => x.CarriageReceipt).IsEmpty().IsGo();
        // 欠付金额
        RuleFor(x => x.CarriageOwed).IsEmpty().IsGo();
        // 其他金额
        RuleFor(x => x.CarriageOther).IsEmpty().IsGo();
        // 回扣金额
        RuleFor(x => x.Brokerage).IsEmpty().IsGo();
        // 保价金额
        RuleFor(x => x.AmountInsured).IsEmpty().IsGo();
        // 状态网点
        RuleFor(x => x.StatusNode).IsLength(true, 20).IsGo();
        // 运单状态
        RuleFor(x => x.StatusId).IsLength(true, 20).IsGo();
        // 卸货
        RuleFor(x => x.UnloadYN).IsEmpty().IsGo();
        // 上楼
        RuleFor(x => x.UpstairYN).IsEmpty().IsGo();
        // 业务员
        RuleFor(x => x.SalesMan).IsLength(false, 50).IsGo();
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
        // 自动单号
        RuleFor(x => x.AutoNoYN).IsEmpty().IsGo();
        // 自动回单
        RuleFor(x => x.AutoReceipt).IsEmpty().IsGo();

        #endregion
    }
}