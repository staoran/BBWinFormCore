using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using FluentValidation;

namespace BB.EntityVerification.TMS;

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
        RuleFor(x => x.DocCode).IsUpLetterNum(true, 2)
            .IsUnique(u => u.ISID).IsAddOrEdit().IsGo();
        // 编码规则
        RuleFor(x => x.RuleFormat).Length(0, 50).IsGo();
        // 创建日期
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsEmpty(20).IsGo();
        // 自增数长度
        RuleFor(x => x.NoLength).IsEmpty()
            .InclusiveBetween(5, 10).When(w => w.RuleFormat.IsNullOrEmpty(), ApplyConditionTo.CurrentValidator).WithMessage("不设规则时，自增长度必须在5-10之间")
            .InclusiveBetween(3, 8).When(w => !w.RuleFormat.IsNullOrEmpty(), ApplyConditionTo.CurrentValidator).WithMessage("设规则时，自增长度必须在3-8之间").IsGo();
        // 自动归零
        RuleFor(x => x.ResetZero)
            .Must(m => !m).When(w => w.RuleFormat.IsNullOrEmpty(), ApplyConditionTo.CurrentValidator).WithMessage("自动归零时自增数规则格式不能为空！")
            .Must(m => m).When(w => !w.RuleFormat.IsNullOrEmpty(), ApplyConditionTo.CurrentValidator).WithMessage("设定规则后必须开启归零！").IsGo();
    }
}