using BB.Entity.Dictionary;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

/// <summary>
/// 用户参数配置
/// </summary>
public class UserParameterValidator : AbstractValidator<UserParameterInfo>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public UserParameterValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public UserParameterValidator(OperationType operationType = OperationType.View)
    {
    }
}