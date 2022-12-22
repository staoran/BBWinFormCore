using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;

using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 运单线路表
/// </summary>
#if DESIGNER
public partial class FrmEditWaybillSegment : BaseEditDesigner
{
    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
#else
public partial class FrmEditWaybillSegment : BaseEditForm<WaybillSegment, WaybillSegmentHttpService>
{
#endif
    public FrmEditWaybillSegment(WaybillSegmentHttpService bll, IValidator<WaybillSegment> validator) : base(bll, validator)
    {
        InitializeComponent();

        Load += FrmEditWaybillSegment_Load;
    }

    /// <summary>
    /// 窗体第一次显示前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmEditWaybillSegment_Load(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    protected override Task InitDictItem()
    {
        //初始化代码
        txtSegmentType.BindDictItems("线路选择类型", null, false, false);
        txtSegmentBeginNode.BindDictItems(GB.AllOuDict, null, false, false);
        txtSegmentEndNode.BindDictItems(GB.AllOuDict, null, false, false);
        txtSegmentBeginYN.BindDictItems("已到,未到", false);
        txtSegmentBeginUser.BindDictItems(GB.AllUserDict, null, true, false);
        txtSegmentEndYN.BindDictItems("已到,未到", false);
        txtSegmentEndUser.BindDictItems(GB.AllUserDict, null, true, false);
        txtStatusId.BindDictItems("运单线路状态", null, false, false);
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

        txtWaybillNo.Tag = WaybillSegment.FieldWaybillNo;
        txtSegmentNo.Tag = WaybillSegment.FieldSegmentNo;
        txtCarmarkNo.Tag = WaybillSegment.FieldCarmarkNo;
        txtSegmentType.Tag = WaybillSegment.FieldSegmentType;
        txtSegmentName.Tag = WaybillSegment.FieldSegmentName;
        txtSegmentBeginNode.Tag = WaybillSegment.FieldSegmentBeginNode;
        txtSegmentEndNode.Tag = WaybillSegment.FieldSegmentEndNode;
        txtExpectedTime.Tag = WaybillSegment.FieldExpectedTime;
        txtExpectedHour.Tag = WaybillSegment.FieldExpectedHour;
        txtExpectedDistance.Tag = WaybillSegment.FieldExpectedDistance;
        txtExpectedOilWear.Tag = WaybillSegment.FieldExpectedOilWear;
        txtExpectedCharge.Tag = WaybillSegment.FieldExpectedCharge;
        txtExpectedPontage.Tag = WaybillSegment.FieldExpectedPontage;
        txtSegmentBeginYN.Tag = WaybillSegment.FieldSegmentBeginYN;
        txtSegmentBeginUser.Tag = WaybillSegment.FieldSegmentBeginUser;
        txtSegmentBeginDate.Tag = WaybillSegment.FieldSegmentBeginDate;
        txtSegmentBeginRemark.Tag = WaybillSegment.FieldSegmentBeginRemark;
        txtSegmentEndYN.Tag = WaybillSegment.FieldSegmentEndYN;
        txtSegmentEndUser.Tag = WaybillSegment.FieldSegmentEndUser;
        txtSegmentEndDate.Tag = WaybillSegment.FieldSegmentEndDate;
        txtSegmentEndRemark.Tag = WaybillSegment.FieldSegmentEndRemark;
        txtStatusId.Tag = WaybillSegment.FieldStatusId;
        txtRemark.Tag = WaybillSegment.FieldRemark;
        txtCreationDate.Tag = WaybillSegment.FieldCreationDate;
        txtCreatedBy.Tag = WaybillSegment.FieldCreatedBy;
        txtLastUpdateDate.Tag = WaybillSegment.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = WaybillSegment.FieldLastUpdatedBy;

        #endregion

        await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(WaybillSegmentInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "运单线路表";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
