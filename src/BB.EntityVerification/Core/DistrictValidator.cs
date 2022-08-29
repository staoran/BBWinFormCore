using BB.Entity.Dictionary;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

/// <summary>
/// 地区业务类
/// </summary>
public class DistrictValidator : AbstractValidator<DistrictInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public DistrictValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public DistrictValidator(OperationType operationType = OperationType.View)
    {
    }
}