using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

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
        #region 实体通用验证

        // 关键字
        RuleFor(x => x.WordsInFront).IsLength(true, 200).IsGo();
        // 代码
        RuleFor(x => x.WordsBehind).IsEmpty().IsGo();
        // 代码类型
        RuleFor(x => x.TranslateType).IsLength(false, 20).IsGo();
        // 可选
        RuleFor(x => x.CanSelectYN).IsEmpty().IsGo();
        // 说明
        RuleFor(x => x.ExampleStr).IsLength(false, 200).IsGo();
        // 禁用
        RuleFor(x => x.CancelYN).IsEmpty().IsGo();
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
    }
}