using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 费用调整确认
/// </summary>
public partial class FrmEditCostMsgs : BaseEditForm<CostMsgs, CostMsgsHttpService>
{
    public FrmEditCostMsgs(CostMsgsHttpService bll, IValidator<CostMsgs> validator) : base(bll, validator)
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
        txtStatusID.BindDictItems("费用调整状态", "1", false, false);
        txtRecvMsgNode.BindDictItems(GB.AllOuDict, GB.LoginUserInfo.CompanyId, false, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtCostMsgNo.Tag = CostMsgs.FieldCostMsgNo;
        txtStatusID.Tag = CostMsgs.FieldStatusID;
        txtRecvMsgNode.Tag = CostMsgs.FieldRecvMsgNode;
        txtRecvMsgContent.Tag = CostMsgs.FieldRecvMsgContent;
        txtAttaPath.Tag = CostMsgs.FieldAttaPath;
        txtCreationDate.Tag = CostMsgs.FieldCreationDate;
        txtCreatedBy.Tag = CostMsgs.FieldCreatedBy;
        txtLastUpdateDate.Tag = CostMsgs.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = CostMsgs.FieldLastUpdatedBy;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(CostMsgsInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "费用调整确认";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
