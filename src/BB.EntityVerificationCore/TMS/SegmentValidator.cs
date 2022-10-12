using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

/// <summary>
/// 线路表 验证类
/// </summary>
public class SegmentValidator : AbstractValidator<Segment>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public SegmentValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public SegmentValidator(OperationType operationType = OperationType.View)
    {
        #region 参数值唯一性验证

        RuleFor(x => x.SegmentName).IsUnique().IsAddOrEdit().IsGo();

        #endregion
    }
}