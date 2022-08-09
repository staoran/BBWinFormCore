using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.EntityVerification.TMS;

/// <summary>
/// 客户管理 验证类
/// </summary>
public class CustomerValidator : AbstractValidator<Customer>
{
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public CustomerValidator() : this(OperationType.View)
    {
    }

    /// <summary>
    /// 验证规则构造函数
    /// </summary>
    /// <param name="operationType">当前操作类型</param>
    public CustomerValidator(OperationType operationType = OperationType.View)
    {
        #region 实体通用验证

        // 公司编号
        RuleFor(x => x.CustomerCode).IsLength(true, 20);
        // 助记码
        RuleFor(x => x.MnemonicCode).IsLength(true, 50);
        // 网点编号
        RuleFor(x => x.TranNode).IsLength(true, 50);
        // 联系人
        RuleFor(x => x.ContactPerson).IsLength(true, 100);
        // 公司名称
        RuleFor(x => x.NativeName).IsLength(true, 100);
        // 地址
        RuleFor(x => x.Address).IsLength(true, 200);
        // 区
        RuleFor(x => x.AreaNo).IsLength(true, 50);
        // 电话号码
        RuleFor(x => x.Tel).IsPhone(true, 20);
        // 手机号
        RuleFor(x => x.Mobile).IsMobile(true, 50);
        // 开户行
        RuleFor(x => x.Bank).IsLength(false, 20);
        // 银行账号
        RuleFor(x => x.BankAccount).IsLength(false, 50);
        // 备注
        RuleFor(x => x.Remark).IsLength(false, 200);
        // 是否在用(Y/N）
        // 付款方式
        RuleFor(x => x.PaymentType).IsLength(true, 20);
        // 业务员提成方式
        RuleFor(x => x.CommissionType).IsLength(false, 20);
        // 客服
        RuleFor(x => x.SalesDeputy).IsLength(false, 20);
        // 项目主管
        RuleFor(x => x.ProjectManager).IsLength(false, 20);
        // 业务员
        RuleFor(x => x.SalesPerson).IsLength(false, 20);
        // 创建日期
        RuleFor(x => x.CreationDate).IsEmpty();
        // 创建人
        RuleFor(x => x.CreatedBy).IsLength(true, 50);
        // 更新日期
        RuleFor(x => x.LastUpdateDate).IsEmpty();
        // 更新人
        RuleFor(x => x.LastUpdatedBy).IsLength(true, 50);
        // 审核
        // 审核人
        RuleFor(x => x.AppUser).IsLength(false, 20);

        #endregion

        #region 参数值唯一性验证

        RuleFor(x => x.CustomerCode).IsUnique().IsAdd().IsGo();;
        RuleFor(x => x.MnemonicCode).IsUnique().IsAddOrEdit().IsGo();;

        #endregion
    }
}