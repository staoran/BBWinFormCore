using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;

using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 运单操作记录
/// </summary>
#if DESIGNER
public partial class FrmEditWaybillRecords : BaseEditDesigner
{
    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
#else
public partial class FrmEditWaybillRecords : BaseEditForm<WaybillRecords, WaybillRecordsHttpService>
{
#endif
    public FrmEditWaybillRecords(WaybillRecordsHttpService bll, IValidator<WaybillRecords> validator) : base(bll, validator)
    {
        InitializeComponent();

        Load += FrmEditWaybillRecords_Load;
    }

    /// <summary>
    /// 窗体第一次显示前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmEditWaybillRecords_Load(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    protected override Task InitDictItem()
    {
        //初始化代码
        txtTranNode.BindDictItems(GB.AllOuDict, "*当前机构*", false, false);
        txtTranNodePN.BindDictItems(GB.AllOuDict, null, false, false);
        txtStatusID.BindDictItems("运单状态", null, false, false);
        txtRelatedUser.BindDictItems(GB.AllUserDict, null, false, false);
        txtCancelYN.BindDictItems("是,否", false);
        txtCancelBy.BindDictItems(GB.AllUserDict, null, true, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", false, false);
        txtNotPublic.BindDictItems("是,否", true);
        return Task.CompletedTask;
    }





    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtWaybillNo.Tag = WaybillRecords.FieldWaybillNo;
        txtTranNode.Tag = WaybillRecords.FieldTranNode;
        txtTranNodePN.Tag = WaybillRecords.FieldTranNodePN;
        txtStatusID.Tag = WaybillRecords.FieldStatusID;
        txtRelatedUser.Tag = WaybillRecords.FieldRelatedUser;
        txtCarMarkNo.Tag = WaybillRecords.FieldCarMarkNo;
        txtSegmentNo.Tag = WaybillRecords.FieldSegmentNo;
        txtRemark.Tag = WaybillRecords.FieldRemark;
        txtCancelYN.Tag = WaybillRecords.FieldCancelYN;
        txtCancelDate.Tag = WaybillRecords.FieldCancelDate;
        txtCancelBy.Tag = WaybillRecords.FieldCancelBy;
        txtCreationDate.Tag = WaybillRecords.FieldCreationDate;
        txtCreatedBy.Tag = WaybillRecords.FieldCreatedBy;
        txtLastUpdateDate.Tag = WaybillRecords.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = WaybillRecords.FieldLastUpdatedBy;
        txtNotPublic.Tag = WaybillRecords.FieldNotPublic;

        #endregion

        await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(WaybillRecordsInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "运单操作记录";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
