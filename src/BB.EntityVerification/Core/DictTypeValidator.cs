using BB.Entity.Dictionary;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

/// <summary>
/// 字典类型对象
/// </summary>
public class DictTypeValidator : AbstractValidator<DictTypeInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public DictTypeValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public DictTypeValidator(OperationType operationType = OperationType.View)
    {
    }
}