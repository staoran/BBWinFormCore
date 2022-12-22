using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;

using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 签收表
/// </summary>
#if DESIGNER
public partial class FrmEditWaybillSign : BaseEditDesigner
{
    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
#else
public partial class FrmEditWaybillSign : BaseEditForm<WaybillSign, WaybillSignHttpService>
{
#endif
    public FrmEditWaybillSign(WaybillSignHttpService bll, IValidator<WaybillSign> validator) : base(bll, validator)
    {
        InitializeComponent();

        Load += FrmEditWaybillSign_Load;
    }

    /// <summary>
    /// 窗体第一次显示前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmEditWaybillSign_Load(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    protected override Task InitDictItem()
    {
        //初始化代码
        txtTranNode.BindDictItems(GB.AllOuDict, "*当前机构*", false, false);
        txtDeliveryType.BindDictItems("交货方式", null, false, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", false, false);
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

        txtWaybillNo.Tag = WaybillSign.FieldWaybillNo;
        txtTranNode.Tag = WaybillSign.FieldTranNode;
        txtTranNodes.Tag = WaybillSign.FieldTranNodes;
        txtDeliveryType.Tag = WaybillSign.FieldDeliveryType;
        txtConsignee.Tag = WaybillSign.FieldConsignee;
        txtConsigneeid.Tag = WaybillSign.FieldConsigneeid;
        txtConsigneeidPicAdds.Tag = WaybillSign.FieldConsigneeidPicAdds;
        txtConsigneeRemark.Tag = WaybillSign.FieldConsigneeRemark;
        txtQty.Tag = WaybillSign.FieldQty;
        txtSignQty.Tag = WaybillSign.FieldSignQty;
        txtAckRecYN.Tag = WaybillSign.FieldAckRecYN;
        txtAckRecNo.Tag = WaybillSign.FieldAckRecNo;
        txtAckRecQty.Tag = WaybillSign.FieldAckRecQty;
        txtAckRecRemark.Tag = WaybillSign.FieldAckRecRemark;
        txtCreationDate.Tag = WaybillSign.FieldCreationDate;
        txtCreatedBy.Tag = WaybillSign.FieldCreatedBy;
        txtLastUpdateDate.Tag = WaybillSign.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = WaybillSign.FieldLastUpdatedBy;
        txtFlagApp.Tag = WaybillSign.FieldFlagApp;
        txtAppUser.Tag = WaybillSign.FieldAppUser;
        txtAppDate.Tag = WaybillSign.FieldAppDate;

        #endregion

        await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(WaybillSignInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "签收表";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
