using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

/// <summary>
/// 公式定义 验证类
/// </summary>
public class BasicTranslateWordsValidator : AbstractValidator<BasicTranslateWords>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public BasicTranslateWordsValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public BasicTranslateWordsValidator(OperationType operationType = OperationType.View)
    {
        #region 参数值唯一性验证

        RuleFor(x => x.WordsInFront).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}