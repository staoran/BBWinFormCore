using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 问题件回复
/// </summary>
public partial class FrmEditMessages : BaseEditForm<Messages, MessagesHttpService>
{
    public FrmEditMessages(MessagesHttpService bll, IValidator<Messages> validator) : base(bll, validator)
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
        txtDealStatus.BindDictItems("问题件处理类型", "8", false, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdateBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtMsgNo.Tag = Messages.FieldMsgNo;
        txtDealStatus.Tag = Messages.FieldDealStatus;
        txtDealContent.Tag = Messages.FieldDealContent;
        txtAttaPath.Tag = Messages.FieldAttaPath;
        txtCreationDate.Tag = Messages.FieldCreationDate;
        txtCreatedBy.Tag = Messages.FieldCreatedBy;
        txtLastUpdateDate.Tag = Messages.FieldLastUpdateDate;
        txtLastUpdateBy.Tag = Messages.FieldLastUpdateBy;

        #endregion

         await base.SetPermit();
    }

    public void SetMsgNo(string no)
    {
        TempInfo.MsgNo = no;
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(MessagesInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "问题件回复";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
