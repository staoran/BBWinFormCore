using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 车辆档案 验证类
/// </summary>
public class CarValidator : AbstractValidator<Car>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public CarValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public CarValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 网点编码
        RuleFor(x => x.TranNode).IsLength(true, 20).IsGo();
        // 车牌号
        RuleFor(x => x.CarNo).IsLength(true, 50).IsGo();
        // 车辆性质
        RuleFor(x => x.Property).IsEmpty(10).IsGo();
        // 车型
        RuleFor(x => x.Model).IsEmpty(10).IsGo();
        // 车体状况
        RuleFor(x => x.CarType).IsEmpty(10).IsGo();
        // 服务范围
        RuleFor(x => x.ServiceRange).IsEmpty(10).IsGo();
        // 信誉
        RuleFor(x => x.TrustLevel).IsEmpty(20).IsGo();
        // 主营路线
        RuleFor(x => x.TargetPlace).IsLength(false, 10).IsGo();
        // 运营编号
        RuleFor(x => x.OperationNo).IsLength(false, 50).IsGo();
        // 发动机号
        RuleFor(x => x.EngineNo).IsLength(false, 50).IsGo();
        // 车架号
        RuleFor(x => x.VIN).IsLength(false, 30).IsGo();
        // 载重
        RuleFor(x => x.Tonnage).IsLength(true, 20).IsGo();
        // 体积
        RuleFor(x => x.Volume).IsEmpty().IsGo();
        // 联系人
        RuleFor(x => x.Contact).IsChinese(true, 20).IsGo();
        // 联系电话
        RuleFor(x => x.ContactTel).IsPhone(true, 30).IsGo();
        // 驾驶员
        RuleFor(x => x.Driver).IsChinese(false, 20).IsGo();
        // 驾驶证号
        RuleFor(x => x.DriverCert).IsLength(false, 30).IsGo();
        // 手机号码
        RuleFor(x => x.DriverMobile).IsMobile(false, 30).IsGo();
        // 固定电话
        RuleFor(x => x.DriverTel).IsPhone(false, 30).IsGo();
        // 家庭住址
        RuleFor(x => x.DriverHomeAddress).IsLength(false, 100).IsGo();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 50).IsGo();

        #endregion

        #region 参数值唯一性验证

        RuleFor(x => x.CarNo).IsUnique().IsAdd().IsGo();;

        #endregion
    }
}