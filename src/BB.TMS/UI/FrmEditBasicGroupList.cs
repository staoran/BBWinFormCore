using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 区域分组
/// </summary>
#if DESIGNER
public partial class FrmEditBasicGroupList : BaseEditDesigner
{
    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
#else
public partial class FrmEditBasicGroupList : BaseEditForm<BasicGroupList, BasicGroupListHttpService>
{
#endif
    public FrmEditBasicGroupList(BasicGroupListHttpService bll, IValidator<BasicGroupList> validator) : base(bll, validator)
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
        txtGroupType.BindDictItems("分组类型", null, false, false);
        txtCostType.BindDictItems(GB.AllCostType, null, false, false);
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

        txtGroupName.Tag = BasicGroupList.FieldGroupName;
        txtGroupType.Tag = BasicGroupList.FieldGroupType;
        txtCostType.Tag = BasicGroupList.FieldCostType;
        txtGroupContent.Tag = BasicGroupList.FieldGroupContent;
        txtGroupExceptContent.Tag = BasicGroupList.FieldGroupExceptContent;
        txtRemark.Tag = BasicGroupList.FieldRemark;
        txtCreationDate.Tag = BasicGroupList.FieldCreationDate;
        txtCreatedBy.Tag = BasicGroupList.FieldCreatedBy;
        txtLastUpdateDate.Tag = BasicGroupList.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = BasicGroupList.FieldLastUpdatedBy;
        txtFlagApp.Tag = BasicGroupList.FieldFlagApp;
        txtAppUser.Tag = BasicGroupList.FieldAppUser;
        txtAppDate.Tag = BasicGroupList.FieldAppDate;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(BasicGroupListInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "区域分组";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
