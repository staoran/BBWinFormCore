using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 公式报价 验证类
/// </summary>
public class BasicQuotationValidator : AbstractValidator<BasicQuotation>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public BasicQuotationValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public BasicQuotationValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 报价编号
        RuleFor(x => x.QuotationNo).IsLength(true, 30).IsGo();
        // 报价名称
        RuleFor(x => x.QuotationDesc).IsLength(true, 100).IsGo();
        // 所属网点
        RuleFor(x => x.TranNodeNO).IsLength(true, 50).IsGo();
        // 费用类型
        RuleFor(x => x.CostType).IsLength(true, 10).IsGo();
        // 货物类型
        RuleFor(x => x.CargoType).IsLength(false, 20).IsGo();
        // 收货方式
        RuleFor(x => x.PickUpType).IsLength(false, 20).IsGo();
        // 交货方式
        RuleFor(x => x.DeliveryType).IsLength(false, 20).IsGo();
        // 运输方式
        RuleFor(x => x.TransportType).IsLength(false, 20).IsGo();
        // 起始区域
        RuleFor(x => x.Froms).IsEmpty().IsGo();
        // 目的区域
        RuleFor(x => x.Tos).IsEmpty().IsGo();
        // 生效时间
        RuleFor(x => x.BeginTime).IsEmpty().IsGo();
        // 过期时间
        RuleFor(x => x.EndTime).IsEmpty().IsGo();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 200).IsGo();
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

        #endregion

        #region 参数值唯一性验证

        RuleFor(x => x.QuotationDesc).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}