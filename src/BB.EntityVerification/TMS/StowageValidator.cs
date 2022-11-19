using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 送货配载 验证类
/// </summary>
public class StowageValidator : AbstractValidator<Stowage>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public StowageValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public StowageValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 配载编号
        RuleFor(x => x.StowageNo).IsLength(true, 50).IsGo();
        // 网点编号
        RuleFor(x => x.TranNodeNO).IsLength(true, 50).IsGo();
        // 配载单类型
        RuleFor(x => x.StowageType).IsLength(true, 2).IsGo();
        // 配载运输类型
        RuleFor(x => x.TransType).IsLength(true, 2).IsGo();
        // 配载车标号
        RuleFor(x => x.TransMarkNo).IsLength(true, 50).IsGo();
        // 车辆编号
        RuleFor(x => x.TransNo).IsLength(false, 50).IsGo();
        // 司机
        RuleFor(x => x.TransDriver).IsLength(false, 50).IsGo();
        // 司机电话
        RuleFor(x => x.TransDriverPhone).IsPhoneAndMobile(false, 50).IsGo();
        // 核销
        RuleFor(x => x.CheckInYN).IsEmpty().IsGo();
        // 核销人
        RuleFor(x => x.CheckInAccount).IsLength(false, 50).IsGo();
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
        // 审核
        RuleFor(x => x.FlagApp).IsEmpty().IsGo();
        // 审核人
        RuleFor(x => x.AppUser).IsLength(false, 20).IsGo();
        // 分摊类型
        RuleFor(x => x.SharType).IsLength(false, 2).IsGo();

        #endregion
    }
}