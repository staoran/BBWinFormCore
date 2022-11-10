using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 承运商资料
/// </summary>
public partial class FrmEditLogisticCompany : BaseEditForm<LogisticCompany, LogisticCompanyHttpService>
{
    public FrmEditLogisticCompany(LogisticCompanyHttpService bll, IValidator<LogisticCompany> validator) : base(bll, validator)
    {
        InitializeComponent();

        Load += FrmEditTest1Car_Load;
    }

    /// <summary>
    /// 窗体第一次显示前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmEditTest1Car_Load(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    protected override Task InitDictItem()
    {
        //初始化代码
        txtOrgCode.BindDictItems(GB.AllOuDict, GB.LoginUserInfo.CompanyId, false, false);
        txtTrustLevel.BindDictItems("信誉类型", null, false, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        txtFlagApp.BindDictItems("已审核,未审核", false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtOrgCode.Tag = LogisticCompany.FieldOrgCode;
        txtLogisticCode.Tag = LogisticCompany.FieldLogisticCode;
        txtLogisticName.Tag = LogisticCompany.FieldLogisticName;
        txtZJM.Tag = LogisticCompany.FieldZJM;
        txtContacts.Tag = LogisticCompany.FieldContacts;
        txtTel.Tag = LogisticCompany.FieldTel;
        txtAddress.Tag = LogisticCompany.FieldAddress;
        txtMainLine.Tag = LogisticCompany.FieldMainLine;
        txtTrustLevel.Tag = LogisticCompany.FieldTrustLevel;
        txtLegal.Tag = LogisticCompany.FieldLegal;
        txtTax.Tag = LogisticCompany.FieldTax;
        txtBank.Tag = LogisticCompany.FieldBank;
        txtBankAccount.Tag = LogisticCompany.FieldBankAccount;
        txtPaymentTerm.Tag = LogisticCompany.FieldPaymentTerm;
        txtContractDate1.Tag = LogisticCompany.FieldContractDate1;
        txtContractDate2.Tag = LogisticCompany.FieldContractDate2;
        txtInUse.Tag = LogisticCompany.FieldInUse;
        txtFlagInvoice.Tag = LogisticCompany.FieldFlagInvoice;
        txtRemark.Tag = LogisticCompany.FieldRemark;
        txtCreationDate.Tag = LogisticCompany.FieldCreationDate;
        txtCreatedBy.Tag = LogisticCompany.FieldCreatedBy;
        txtLastUpdateDate.Tag = LogisticCompany.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = LogisticCompany.FieldLastUpdatedBy;
        txtFlagApp.Tag = LogisticCompany.FieldFlagApp;
        txtAppUser.Tag = LogisticCompany.FieldAppUser;
        txtAppDate.Tag = LogisticCompany.FieldAppDate;
        txtCancelYN.Tag = LogisticCompany.FieldCancelYN;
        txtCancelDate.Tag = LogisticCompany.FieldCancelDate;
        txtCancelUser.Tag = LogisticCompany.FieldCancelUser;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(LogisticCompanyInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "承运商资料";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
