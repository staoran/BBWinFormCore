using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerificationCore.TMS;

/// <summary>
/// 回单操作记录 验证类
/// </summary>
public class WaybillAckRecsValidatorCore : AbstractValidator<WaybillAckRecs>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public WaybillAckRecsValidatorCore() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public WaybillAckRecsValidatorCore(OperationType operationType = OperationType.View)
    {
        #region 参数值唯一性验证


        #endregion
    }
}