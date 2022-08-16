using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 区域分组 验证类
/// </summary>
public class BasicGroupListValidator : AbstractValidator<BasicGroupList>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public BasicGroupListValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public BasicGroupListValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 分组名称
        RuleFor(x => x.GroupName).IsLength(true, 300).IsGo();
        // 分组类型
        RuleFor(x => x.GroupType).IsLength(true, 20).IsGo();
        // 费用类型
        RuleFor(x => x.CostType).IsLength(true, 10).IsGo();
        // 分组区域
        RuleFor(x => x.GroupContent).IsEmpty().IsGo();
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

        #endregion

        #region 参数值唯一性验证

        RuleFor(x => x.GroupName).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}