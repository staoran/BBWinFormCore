using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

/// <summary>
/// 网点资料 验证类
/// </summary>
public class NodeValidator : AbstractValidator<Node>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public NodeValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public NodeValidator(OperationType operationType = OperationType.View)
    {
        #region 参数值唯一性验证

        RuleFor(x => x.TranNodeName).IsUnique().IsAddOrEdit().IsGo();

        #endregion
    }
}