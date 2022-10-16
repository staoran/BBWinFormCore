using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 公式定义
/// </summary>
public partial class FrmEditBasicTranslateWords : BaseEditForm<BasicTranslateWords, BasicTranslateWordsHttpService>
{
    public FrmEditBasicTranslateWords(BasicTranslateWordsHttpService bll, IValidator<BasicTranslateWords> validator) : base(bll, validator)
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
        txtTranslateType.BindDictItems("公式关键字类型", "", true, false);
        txtCanSelectYN.BindDictItems("是,否", false);
        txtCancelYN.BindDictItems("是,否", false);
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

        txtWordsInFront.Tag = BasicTranslateWords.FieldWordsInFront;
        txtWordsBehind.Tag = BasicTranslateWords.FieldWordsBehind;
        txtTranslateType.Tag = BasicTranslateWords.FieldTranslateType;
        txtCanSelectYN.Tag = BasicTranslateWords.FieldCanSelectYN;
        txtExampleStr.Tag = BasicTranslateWords.FieldExampleStr;
        txtCancelYN.Tag = BasicTranslateWords.FieldCancelYN;
        txtRemark.Tag = BasicTranslateWords.FieldRemark;
        txtCreationDate.Tag = BasicTranslateWords.FieldCreationDate;
        txtCreatedBy.Tag = BasicTranslateWords.FieldCreatedBy;
        txtLastUpdateDate.Tag = BasicTranslateWords.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = BasicTranslateWords.FieldLastUpdatedBy;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(BasicTranslateWordsInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "公式定义";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
