using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 网点区域 验证类
/// </summary>
public class NodesValidator : AbstractValidator<Nodes>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public NodesValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public NodesValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 网点ID
        RuleFor(x => x.TranNodeNO).IsLength(true, 20).IsGo();
        // 区域名称
        RuleFor(x => x.TranNodeAreaName).IsLength(true, 100).IsGo();
        // 区域详情
        RuleFor(x => x.TranNodeAreaDesc).IsEmpty().IsGo();
        // 交货方式
        RuleFor(x => x.DeliveryType).IsLength(true, 20).IsGo();
        // 中心坐标
        RuleFor(x => x.CenterCoordinate).IsLength(false, 50).IsGo();
        // 重泡比
        RuleFor(x => x.ConvertVK).IsEmpty().IsGo();
        // 区
        RuleFor(x => x.AreaId).IsLength(false, 50).IsGo();
        // 地址
        RuleFor(x => x.Address).IsLength(false, 200).IsGo();
        // 负责人
        RuleFor(x => x.Person).IsLength(false, 50).IsGo();
        // 联系方式
        RuleFor(x => x.Phone).IsLength(false, 50).IsGo();
        // 作废
        RuleFor(x => x.CancelYN).IsEmpty().IsGo();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 50).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 50).IsGo();
        // 更新时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();
        // 更新人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 20).IsGo();

        #endregion

        #region 参数值唯一性验证


        #endregion
    }
}