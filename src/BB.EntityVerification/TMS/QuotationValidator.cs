using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 普通报价 验证类
/// </summary>
public class QuotationValidator : AbstractValidator<Quotation>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public QuotationValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public QuotationValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 报价编号
        RuleFor(x => x.QuotationNo).IsLength(true, 30).IsGo();
        // 报价名称
        RuleFor(x => x.QuotationDesc).IsLength(true, 50).IsGo();
        // 费用类型
        RuleFor(x => x.CostType).IsLength(true, 10).IsGo();
        // 支持加成
        RuleFor(x => x.CargoTypePerYN).IsEmpty().IsGo();
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
        // 创建网点
        RuleFor(x => x.TranNodeNO).IsLength(true, 50).IsGo();

        #endregion

        #region 参数值唯一性验证

        RuleFor(x => x.QuotationDesc).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}