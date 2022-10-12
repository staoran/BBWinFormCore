using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

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
        #region 参数值唯一性验证

        RuleFor(x => x.CarNo).IsUnique().IsAdd().IsGo();;

        #endregion
    }
}