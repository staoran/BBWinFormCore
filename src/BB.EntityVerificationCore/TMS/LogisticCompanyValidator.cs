using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

/// <summary>
/// 承运商资料 验证类
/// </summary>
public class LogisticCompanyValidator : AbstractValidator<LogisticCompany>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public LogisticCompanyValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public LogisticCompanyValidator(OperationType operationType = OperationType.View)
    {
        #region 参数值唯一性验证

        RuleFor(x => x.LogisticName).IsUnique().IsAddOrEdit().IsGo();;
        RuleFor(x => x.ZJM).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}