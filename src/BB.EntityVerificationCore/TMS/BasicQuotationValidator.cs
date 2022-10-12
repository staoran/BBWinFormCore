using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

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
        #region 参数值唯一性验证

        RuleFor(x => x.QuotationDesc).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}