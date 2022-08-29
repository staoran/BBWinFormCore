using BB.Entity.Dictionary;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

/// <summary>
/// 字典数据对象
/// </summary>
public class DictDataValidator : AbstractValidator<DictDataInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public DictDataValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public DictDataValidator(OperationType operationType = OperationType.View)
    {
    }
}