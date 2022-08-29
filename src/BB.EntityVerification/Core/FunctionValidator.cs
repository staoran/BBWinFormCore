using BB.Entity.Security;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

public class FunctionValidator : AbstractValidator<FunctionInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public FunctionValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public FunctionValidator(OperationType operationType = OperationType.View)
    {
    }
}