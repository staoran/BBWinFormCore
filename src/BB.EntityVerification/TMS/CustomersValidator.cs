using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 客户收货人 验证类
/// </summary>
public class CustomersValidator : AbstractValidator<Customers>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public CustomersValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public CustomersValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 公司编号
        RuleFor(x => x.CustomerCode).IsLength(true, 20).IsGo();
        // 网点编号
        RuleFor(x => x.TranNode).IsLength(true, 50).IsGo();
        // 联系人
        RuleFor(x => x.ContactPerson).IsLength(true, 100).IsGo();
        // 公司名称
        RuleFor(x => x.NativeName).IsLength(true, 100).IsGo();
        // 地址
        RuleFor(x => x.Address).IsLength(true, 200).IsGo();
        // 区
        RuleFor(x => x.AreaNo).IsLength(true, 50).IsGo();
        // 电话
        RuleFor(x => x.Tel).IsPhone(true, 20).IsGo();
        // 手机
        RuleFor(x => x.Mobile).IsMobile(true, 50).IsGo();
        // 坐标
        RuleFor(x => x.Coordinate).IsLength(false, 50).IsGo();
        // 默认网点
        RuleFor(x => x.DefaultToNode).IsLength(false, 50).IsGo();
        // 默认区域名称
        RuleFor(x => x.DefaultToNodesName).IsLength(false, 100).IsGo();
        // 货物名称
        RuleFor(x => x.CargoName).IsLength(false, 50).IsGo();
        // 包装方式
        RuleFor(x => x.PackageType).IsLength(false, 2).IsGo();
        // 货物单位
        RuleFor(x => x.CargoUnit).IsLength(false, 10).IsGo();
        // 计价方式
        RuleFor(x => x.PriceType).IsLength(false, 2).IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(false, 50).IsGo();
        // 更新人
        RuleFor(x => x.LastUpdatedBy).IsLength(false, 50).IsGo();

        #endregion

        #region 参数值唯一性验证


        #endregion
    }
}