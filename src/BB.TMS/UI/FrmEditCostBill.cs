using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 预付金管理
/// </summary>
#if DESIGNER
public partial class FrmEditCostBill : BaseEditDesigner
{
    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
#else
public partial class FrmEditCostBill : BaseEditForm<CostBill, CostBillHttpService>
{
#endif
    public FrmEditCostBill(CostBillHttpService bll, IValidator<CostBill> validator) : base(bll, validator)
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
        txtCostBillType.BindDictItems("费用单据类型", "1", false, false);
        txtTranNodeNO.BindDictItems(GB.AllOuDict, null, false, false);
        txtTranNodeNOPay.BindDictItems(GB.AllOuDict, null, false, false);
        txtCostType.BindDictItems(GB.AllCostBillType, null, false, false);
        txtPostYN.BindDictItems("是,否", false);
        txtPostBy.BindDictItems(GB.AllUserDict, null, true, false);
        txtStatusID.BindDictItems("账单状态", "0", true, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        txtCreatedByNode.BindDictItems(GB.AllOuDict, GB.LoginUserInfo.CompanyId, false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        txtFlagApp.BindDictItems("已审核,未审核", false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);
        txtFinancialCenter.BindDictItems(GB.AllOuDict, null, false, false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtCostBillType.Tag = CostBill.FieldCostBillType;
        txtCostBillNo.Tag = CostBill.FieldCostBillNo;
        txtWaybillNo.Tag = CostBill.FieldWaybillNo;
        txtTranNodeNO.Tag = CostBill.FieldTranNodeNO;
        txtTranNodeNOPay.Tag = CostBill.FieldTranNodeNOPay;
        txtSourceNo.Tag = CostBill.FieldSourceNo;
        txtCostType.Tag = CostBill.FieldCostType;
        txtCtrl.Tag = CostBill.FieldCtrl;
        txtCost.Tag = CostBill.FieldCost;
        txtPostYN.Tag = CostBill.FieldPostYN;
        txtPostDate.Tag = CostBill.FieldPostDate;
        txtPostBy.Tag = CostBill.FieldPostBy;
        txtStatusID.Tag = CostBill.FieldStatusID;
        txtRemark.Tag = CostBill.FieldRemark;
        txtCreationDate.Tag = CostBill.FieldCreationDate;
        txtCreatedBy.Tag = CostBill.FieldCreatedBy;
        txtCreatedByNode.Tag = CostBill.FieldCreatedByNode;
        txtLastUpdateDate.Tag = CostBill.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = CostBill.FieldLastUpdatedBy;
        txtFlagApp.Tag = CostBill.FieldFlagApp;
        txtAppUser.Tag = CostBill.FieldAppUser;
        txtAppDate.Tag = CostBill.FieldAppDate;
        txtFinancialCenter.Tag = CostBill.FieldFinancialCenter;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(CostBillInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "预付金管理";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
