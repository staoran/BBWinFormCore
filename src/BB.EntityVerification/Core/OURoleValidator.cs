using BB.Entity.Security;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.Core;

public class OURoleValidator : AbstractValidator<OURoleEntity>
{
	/// <summary>
	/// 无参构造函数
	/// </summary>
	public OURoleValidator() : this(OperationType.View)
	{
	}

	/// <summary>
	/// 验证规则构造函数
	/// </summary>
	/// <param name="operationType">当前操作类型</param>
	public OURoleValidator(OperationType operationType = OperationType.View)
	{
	}
}