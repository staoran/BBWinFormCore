using BB.Entity.Dictionary;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

/// <summary>
/// 中国省份业务对象类
/// </summary>
public class ProvinceValidator : AbstractValidator<ProvinceInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public ProvinceValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public ProvinceValidator(OperationType operationType = OperationType.View)
    {
    }
}