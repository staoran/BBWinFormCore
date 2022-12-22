using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;

using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 回单操作记录
/// </summary>
#if DESIGNER
public partial class FrmEditWaybillAckRecs : BaseEditDesigner
{
    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
#else
public partial class FrmEditWaybillAckRecs : BaseEditForm<WaybillAckRecs, WaybillAckRecsHttpService>
{
#endif
    public FrmEditWaybillAckRecs(WaybillAckRecsHttpService bll, IValidator<WaybillAckRecs> validator) : base(bll, validator)
    {
        InitializeComponent();

        Load += FrmEditWaybillAckRecs_Load;
    }

    /// <summary>
    /// 窗体第一次显示前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmEditWaybillAckRecs_Load(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    protected override Task InitDictItem()
    {
        //初始化代码
        txtTranNode.BindDictItems(GB.AllOuDict, "*当前用户*", false, false);
        txtStatusID.BindDictItems("运单状态", null, false, false);
        txtRelatedUser.BindDictItems(GB.AllUserDict, null, false, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", false, false);
        return Task.CompletedTask;
    }





    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtAckRecNo.Tag = WaybillAckRecs.FieldAckRecNo;
        txtTranNode.Tag = WaybillAckRecs.FieldTranNode;
        txtTranNodePN.Tag = WaybillAckRecs.FieldTranNodePN;
        txtStatusID.Tag = WaybillAckRecs.FieldStatusID;
        txtRelatedUser.Tag = WaybillAckRecs.FieldRelatedUser;
        txtCarMarkNo.Tag = WaybillAckRecs.FieldCarMarkNo;
        txtRemark.Tag = WaybillAckRecs.FieldRemark;
        txtCancelYN.Tag = WaybillAckRecs.FieldCancelYN;
        txtCancelDate.Tag = WaybillAckRecs.FieldCancelDate;
        txtCancelBy.Tag = WaybillAckRecs.FieldCancelBy;
        txtCreationDate.Tag = WaybillAckRecs.FieldCreationDate;
        txtCreatedBy.Tag = WaybillAckRecs.FieldCreatedBy;
        txtLastUpdateDate.Tag = WaybillAckRecs.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = WaybillAckRecs.FieldLastUpdatedBy;

        #endregion

        await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(WaybillAckRecsInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "回单操作记录";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
