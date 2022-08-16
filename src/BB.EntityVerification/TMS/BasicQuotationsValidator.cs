using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 公式报价明细 验证类
/// </summary>
public class BasicQuotationsValidator : AbstractValidator<BasicQuotations>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public BasicQuotationsValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public BasicQuotationsValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 报价编号
        RuleFor(x => x.QuotationNo).IsLength(true, 30).IsGo();
        // 条件范围
        RuleFor(x => x.MathConditional).IsEmpty().IsGo();
        // 条件公式
        RuleFor(x => x.MathContent).IsEmpty().IsGo();
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

        #endregion

        #region 参数值唯一性验证


        #endregion
    }
}