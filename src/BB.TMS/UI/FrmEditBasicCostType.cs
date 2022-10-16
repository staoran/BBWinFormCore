using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 费用类型
/// </summary>
public partial class FrmEditBasicCostType : BaseEditForm<BasicCostType, BasicCostTypeHttpService>
{
    public FrmEditBasicCostType(BasicCostTypeHttpService bll, IValidator<BasicCostType> validator) : base(bll, validator)
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
        txtUseYN.BindDictItems("是,否", false);
        txtPayNodeType.BindDictItems("收付部门类型", null, false, false);
        txtRecvNodeType.BindDictItems("收付部门类型", null, false, false);
        txtPayPostType.BindDictItems("入账时机", null, false, false);
        txtRecvPostType.BindDictItems("入账时机", null, false, false);
        txtCostYN.BindDictItems("是,否", false);
        txtFlagApp.BindDictItems("已审核,未审核", false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtCostType.Tag = BasicCostType.FieldCostType;
        txtCostTypeDesc.Tag = BasicCostType.FieldCostTypeDesc;
        txtCtrl.Tag = BasicCostType.FieldCtrl;
        txtUseYN.Tag = BasicCostType.FieldUseYN;
        txtUseType.Tag = BasicCostType.FieldUseType;
        txtPayNodeType.Tag = BasicCostType.FieldPayNodeType;
        txtRecvNodeType.Tag = BasicCostType.FieldRecvNodeType;
        txtPayPostType.Tag = BasicCostType.FieldPayPostType;
        txtRecvPostType.Tag = BasicCostType.FieldRecvPostType;
        txtCostYN.Tag = BasicCostType.FieldCostYN;
        txtRemark.Tag = BasicCostType.FieldRemark;
        txtFlagApp.Tag = BasicCostType.FieldFlagApp;
        txtAppUser.Tag = BasicCostType.FieldAppUser;
        txtAppDate.Tag = BasicCostType.FieldAppDate;
        txtCreatedBy.Tag = BasicCostType.FieldCreatedBy;
        txtCreationDate.Tag = BasicCostType.FieldCreationDate;
        txtLastUpdatedBy.Tag = BasicCostType.FieldLastUpdatedBy;
        txtLastUpdateDate.Tag = BasicCostType.FieldLastUpdateDate;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(BasicCostTypeInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "费用类型";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
