using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

/// <summary>
/// 单号规则验证类
/// </summary>
public class DocNoRuleValidator : AbstractValidator<DocNoRule>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public DocNoRuleValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 单号规则验证类
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public DocNoRuleValidator(OperationType operationType)
    {
        // 单据字头
        RuleFor(x => x.DocCode).IsUnique(u => u.ISID).IsAddOrEdit().IsGo();
    }
}