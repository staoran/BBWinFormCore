using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;
using FluentValidation.Results;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 网点资料 验证类
/// </summary>
public class NodeValidator : AbstractValidator<Node>
{
    /// <summary>
    /// 验证前的操作
    /// </summary>
    /// <param name="context"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected override bool PreValidate(ValidationContext<Node> context, ValidationResult result)
    {
        base.PreValidate(context, result);

        // 判断传入的 Node 是否为空
        if (context.InstanceToValidate == null) 
        {
            result.Errors.Add(new ValidationFailure("", "模型不能为空"));
            return false;
        }
        return true;
    }

    /// <summary>
    /// 无参构造函数
    /// </summary>
    public NodeValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public NodeValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // RuleForEach(x => x.NodesList).SetValidator(new NodesValidator());
        // todo 研究全局的？ When()
        // 网点ID
        RuleFor(x => x.TranNodeNO).IsLength(true, 20).IsGo();
        // 结算网点编号
        RuleFor(x => x.TranNodeCostNo).IsLength(false, 20).IsGo();
        // 网点名称
        RuleFor(x => x.TranNodeName).IsLength(true, 50).IsGo();
        // 网点类型
        RuleFor(x => x.TranNodeType).IsLength(true, 10).IsGo();
        // 合同开始时间
        RuleFor(x => x.TranNodeBeginDate).IsEmpty().IsGo();
        // 合同终止时间
        RuleFor(x => x.TranNodeEndDate).IsEmpty().IsGo();
        // 上级网点ID
        RuleFor(x => x.ParentNo).IsLength(true, 20).IsGo();
        // 网点负责人
        RuleFor(x => x.TranNodePerson).IsLength(true, 50).IsGo();
        // 负责人证件号码
        RuleFor(x => x.TranNodePersonID).IsIdCard(false, 30).IsGo();
        // 网点联系方式
        RuleFor(x => x.TranNodeMobile).IsPhoneAndMobile(true, 20).IsGo();
        // 网点地址
        RuleFor(x => x.TranNodeAddress).IsLength(true, 500).IsGo();
        // 区
        RuleFor(x => x.AreaNo).IsLength(true, 20).IsGo();
        // 进港时间
        RuleFor(x => x.InTime).IsEmpty().IsGo();
        // 出港时间
        RuleFor(x => x.OutTime).IsEmpty().IsGo();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 1000).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 20).IsGo();
        // 更新时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();
        // 更新人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 50).IsGo();
        // 网点状态
        RuleFor(x => x.TranNodeStatus).IsLength(true, 30).IsGo();
        // 审核人
        RuleFor(x => x.AppUser).IsLength(false, 20).IsGo();
        // 签收天数
        RuleFor(x => x.SignDays).IsEmpty().IsGo();
        // 回单返回天数
        RuleFor(x => x.AckRecDays).IsEmpty().IsGo();
        // 合同备注
        RuleFor(x => x.ContractNote).IsLength(false, 1000).IsGo();
        // 网点坐标
        RuleFor(x => x.TranNodeAxes).IsLength(false, 50).IsGo();
        // 所属财务中心
        RuleFor(x => x.FinancialCenter).IsLength(true, 50).IsGo();
        // 白名单
        RuleFor(x => x.WhiteList).IsLength(false, 2147483647).IsGo();
        // 黑名单
        RuleFor(x => x.BlackList).IsLength(false, 2147483647).IsGo();

        #endregion

        #region 参数值唯一性验证

        RuleFor(x => x.TranNodeName).IsUnique().IsAddOrEdit().IsGo();

        #endregion
    }
}