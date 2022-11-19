using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 配载明细表 验证类
/// </summary>
public class StowagesValidator : AbstractValidator<Stowages>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public StowagesValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public StowagesValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 配载编号
        RuleFor(x => x.StowageNo).IsLength(true, 50).IsGo();
        // 导入类型
        RuleFor(x => x.InputType).IsLength(true, 20).IsGo();
        // 运单编号
        RuleFor(x => x.WaybillNo).IsLength(true, 50).IsGo();
        // 发货网点
        RuleFor(x => x.FromNode).IsLength(true, 50).IsGo();
        // 发货区域
        RuleFor(x => x.FromNodes).IsEmpty().IsGo();
        // 目的网点
        RuleFor(x => x.ToNode).IsLength(true, 50).IsGo();
        // 目的区域
        RuleFor(x => x.ToNodes).IsEmpty().IsGo();
        // 收货公司
        RuleFor(x => x.ConsigneeCompanyName).IsLength(true, 100).IsGo();
        // 收货地址
        RuleFor(x => x.ConsigneeAddress).IsLength(true, 500).IsGo();
        // 收货电话
        RuleFor(x => x.ConsigneeTel).IsLength(true, 50).IsGo();
        // 收货人
        RuleFor(x => x.Consignee).IsLength(true, 50).IsGo();
        // 交货方式
        RuleFor(x => x.DeliveryType).IsLength(true, 20).IsGo();
        // 付款方式
        RuleFor(x => x.PaymentType).IsLength(true, 20).IsGo();
        // 回单类型
        RuleFor(x => x.AckRecType).IsLength(false, 2).IsGo();
        // 回单号
        RuleFor(x => x.AckRecNo).IsLength(false, 50).IsGo();
        // 是否卸货
        RuleFor(x => x.UnloadYN).IsEmpty().IsGo();
        // 是否上楼
        RuleFor(x => x.UpstairYN).IsEmpty().IsGo();
        // 是否短信
        RuleFor(x => x.SmsYN).IsEmpty().IsGo();
        // 状态
        RuleFor(x => x.StatusID).IsLength(true, 20).IsGo();
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