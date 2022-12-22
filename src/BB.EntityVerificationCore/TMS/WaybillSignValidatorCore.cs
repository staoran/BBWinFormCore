using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

/// <summary>
/// 签收表 验证类
/// </summary>
public class WaybillSignValidatorCore : AbstractValidator<WaybillSign>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public WaybillSignValidatorCore() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public WaybillSignValidatorCore(OperationType operationType = OperationType.View)
    {
        #region 参数值唯一性验证


        #endregion
    }
}