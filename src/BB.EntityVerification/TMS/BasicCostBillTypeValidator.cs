using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 预付金操作类型 验证类
/// </summary>
public class BasicCostBillTypeValidator : AbstractValidator<BasicCostBillType>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public BasicCostBillTypeValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public BasicCostBillTypeValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 类型编号
        RuleFor(x => x.CostType).IsNumber(true, 20).IsGo();
        // 类型名称
        RuleFor(x => x.CostDesc).IsLength(true, 50).IsGo();
        // 正负1/-1
        RuleFor(x => x.Ctrl).IsEmpty().IsGo();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 200).IsGo();
        // 适用范围
        RuleFor(x => x.UseType).IsLength(true, 50).IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 20).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 审批人
        RuleFor(x => x.AppUser).IsLength(false, 20).IsGo();
        // 修改人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 20).IsGo();
        // 修改时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();

        #endregion
    }
}