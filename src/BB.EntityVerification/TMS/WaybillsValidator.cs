using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 运单货物明细 验证类
/// </summary>
public class WaybillsValidator : AbstractValidator<Waybills>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public WaybillsValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public WaybillsValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 运单号
        RuleFor(x => x.WaybillNo).IsLength(true, 50).IsGo();
        // 货物名称
        RuleFor(x => x.CargoName).IsLength(true, 50).IsGo();
        // 包装类型
        RuleFor(x => x.PackageType).IsLength(false, 20).IsGo();
        // 货物单位
        RuleFor(x => x.CargoUnit).IsLength(false, 10).IsGo();
        // 件数
        RuleFor(x => x.Qty).IsEmpty().IsGo();
        // 重量
        RuleFor(x => x.Weight).IsEmpty().IsGo();
        // 体积
        RuleFor(x => x.Cubage).IsEmpty().IsGo();
        // 报价类型
        RuleFor(x => x.PriceType).IsLength(false, 20).IsGo();
        // 运费
        RuleFor(x => x.CarriageCharge).IsEmpty().IsGo();
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