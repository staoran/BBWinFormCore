using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 承运商资料 验证类
/// </summary>
public class LogisticCompanyValidator : AbstractValidator<LogisticCompany>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public LogisticCompanyValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public LogisticCompanyValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 网点名称
        RuleFor(x => x.OrgCode).IsLength(true, 20).IsGo();
        // 承运商编号
        RuleFor(x => x.LogisticCode).IsLength(true, 32).IsGo();
        // 承运商名称
        RuleFor(x => x.LogisticName).IsLength(true, 100).IsGo();
        // 助记码
        RuleFor(x => x.ZJM).IsLength(true, 20).IsGo();
        // 联系人
        RuleFor(x => x.Contacts).IsLength(true, 20).IsGo();
        // 电话
        RuleFor(x => x.Tel).IsPhoneAndMobile(true, 50).IsGo();
        // 地址
        RuleFor(x => x.Address).IsLength(true, 100).IsGo();
        // 主营线路
        RuleFor(x => x.MainLine).IsLength(true, 50).IsGo();
        // 信誉
        RuleFor(x => x.TrustLevel).IsLength(true, 10).IsGo();
        // 法人
        RuleFor(x => x.Legal).IsLength(false, 20).IsGo();
        // 税号
        RuleFor(x => x.Tax).IsLength(false, 30).IsGo();
        // 开户行
        RuleFor(x => x.Bank).IsLength(false, 20).IsGo();
        // 银行账号
        RuleFor(x => x.BankAccount).IsLength(false, 50).IsGo();
        // 账期说明
        RuleFor(x => x.PaymentTerm).IsLength(false, 50).IsGo();
        // 合同起始日期
        RuleFor(x => x.ContractDate1).IsEmpty().IsGo();
        // 合同结束日期
        RuleFor(x => x.ContractDate2).IsEmpty().IsGo();
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 200).IsGo();
        // 创建时间
        RuleFor(x => x.CreationDate).IsEmpty().IsGo();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 20).IsGo();
        // 更新时间
        RuleFor(x => x.LastUpdateDate).IsEmpty().IsGo();
        // 更新人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 20).IsGo();
        // 审批人
        RuleFor(x => x.AppUser).IsLength(false, 20).IsGo();
        // 作废人
        RuleFor(x => x.CancelUser).IsLength(false, 20).IsGo();

        #endregion

        #region 参数值唯一性验证

        RuleFor(x => x.LogisticName).IsUnique().IsAddOrEdit().IsGo();;
        RuleFor(x => x.ZJM).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}