using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 预付金管理 验证类
/// </summary>
public class CostBillValidator : AbstractValidator<CostBill>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public CostBillValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public CostBillValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 预付单类型
        RuleFor(x => x.CostBillType).IsLength(true, 2).IsGo();
        // 预付单编号
        RuleFor(x => x.CostBillNo).IsLength(true, 50).IsGo();
        // 运单编号
        RuleFor(x => x.WaybillNo).IsLength(true, 50).IsGo();
        // 收款网点
        RuleFor(x => x.TranNodeNO).IsLength(true, 50).IsGo();
        // 付款网点
        RuleFor(x => x.TranNodeNOPay).IsLength(true, 50).IsGo();
        // 来源单号
        RuleFor(x => x.SourceNo).IsLength(false, 50).IsGo();
        // 操作类型
        RuleFor(x => x.CostType).IsLength(true, 20).IsGo();
        // 正负
        RuleFor(x => x.Ctrl).IsEmpty().IsGo();
        // 金额
        RuleFor(x => x.Cost).IsEmpty().IsGo();
        // 入账人
        RuleFor(x => x.PostBy).IsLength(false, 50).IsGo();
        // 单据状态
        RuleFor(x => x.StatusID).IsLength(false, 2).IsGo();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 1000).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 20).IsGo();
        // 创建网点
        RuleFor(x => x.CreatedByNode).IsLength(true, 50).IsGo();
        // 修改时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();
        // 修改人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 50).IsGo();
        // 审批
        // 审批人
        RuleFor(x => x.AppUser).IsLength(false, 20).IsGo();
        // 财务中心
        RuleFor(x => x.FinancialCenter).IsLength(true, 50).IsGo();

        #endregion

        #region 参数值唯一性验证


        #endregion
    }
}