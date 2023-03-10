using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;

using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 预付金操作类型
/// </summary>
#if DESIGNER
public partial class FrmEditBasicCostBillType : BaseEditDesigner
{
    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
#else
public partial class FrmEditBasicCostBillType : BaseEditForm<BasicCostBillType, BasicCostBillTypeHttpService>
{
#endif
    public FrmEditBasicCostBillType(BasicCostBillTypeHttpService bll, IValidator<BasicCostBillType> validator) : base(bll, validator)
    {
        InitializeComponent();

        Load += FrmEditBasicCostBillType_Load;
    }

    /// <summary>
    /// 窗体第一次显示前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmEditBasicCostBillType_Load(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    protected override Task InitDictItem()
    {
        //初始化代码
        txtCreatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", false, false);
        txtFlagApp.BindDictItems("已审核,未审核", false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", false, false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtCostType.Tag = BasicCostBillType.FieldCostType;
        txtCostDesc.Tag = BasicCostBillType.FieldCostDesc;
        txtCtrl.Tag = BasicCostBillType.FieldCtrl;
        txtRemark.Tag = BasicCostBillType.FieldRemark;
        txtUseType.Tag = BasicCostBillType.FieldUseType;
        txtCreatedBy.Tag = BasicCostBillType.FieldCreatedBy;
        txtCreationDate.Tag = BasicCostBillType.FieldCreationDate;
        txtFlagApp.Tag = BasicCostBillType.FieldFlagApp;
        txtAppUser.Tag = BasicCostBillType.FieldAppUser;
        txtAppDate.Tag = BasicCostBillType.FieldAppDate;
        txtLastUpdatedBy.Tag = BasicCostBillType.FieldLastUpdatedBy;
        txtLastUpdateDate.Tag = BasicCostBillType.FieldLastUpdateDate;

        #endregion

        await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(BasicCostBillTypeInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "预付金操作类型";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
