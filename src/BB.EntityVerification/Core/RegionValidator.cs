using BB.Entity.Dictionary;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

/// <summary>
/// 行政区域
/// </summary>
public class RegionValidator : AbstractValidator<RegionInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public RegionValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public RegionValidator(OperationType operationType = OperationType.View)
    {
    }
}