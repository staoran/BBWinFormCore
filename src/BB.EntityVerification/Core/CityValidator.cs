using BB.Entity.Dictionary;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

/// <summary>
/// 城市业务对象类
/// </summary>
public class CityValidator : AbstractValidator<CityInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public CityValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public CityValidator(OperationType operationType = OperationType.View)
    {
    }
}