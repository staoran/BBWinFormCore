using System.Drawing;

using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Tools.Extension;
using BB.Tools.Format;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 单号规则
/// </summary>
#if DESIGNER
public partial class FrmEditDocNoRule : BaseEditDesigner
{
    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
#else
public partial class FrmEditDocNoRule : BaseEditForm<DocNoRule, DocNoRuleHttpService>
{
#endif
    public FrmEditDocNoRule(DocNoRuleHttpService bll, IValidator<DocNoRule> validator) : base(bll, validator)
    {
        InitializeComponent();

        #region 表单容器设置

        layoutControl1.Appearance.ControlReadOnly.BackColor = Color.SeaShell;
        layoutControl1.Appearance.ControlReadOnly.Options.UseBackColor = true;

        #endregion

        Load += FrmEditTest1Car_Load;
        Shown += txtPerview_EditValueChanged;
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
        txtCreatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        List<string> list = new()
        {
            "<年年年年>",
            "<年年>",
            "<月月>",
            "<日日>"
        };
        txtElement1.BindDictItems(list, string.Empty);
        txtElement2.BindDictItems(list, string.Empty);
        txtElement3.BindDictItems(list, string.Empty);
        txtElement4.BindDictItems(list, string.Empty);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 初始化控件的显示信息
    /// </summary>
    /// <param name="info"></param>
    protected override void DisplayInfo(DocNoRule info)
    {
        base.DisplayInfo(info);
        
        if (txtRuleFormat.Text != "")
        {
            string tmp = txtRuleFormat.Text.Replace("><", ">|<");
            string[] items = tmp.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            txtElement1.EditValue = items.Length > 0 ? items[0] : "";
            txtElement2.EditValue = items.Length > 1 ? items[1] : "";
            txtElement3.EditValue = items.Length > 2 ? items[2] : "";
            txtElement4.EditValue = items.Length > 3 ? items[3] : "";
        }
        
        txtElement1.EditValueChanged += txtElement_EditValueChanged;
        txtElement2.EditValueChanged += txtElement_EditValueChanged;
        txtElement3.EditValueChanged += txtElement_EditValueChanged;
        txtElement4.EditValueChanged += txtElement_EditValueChanged;
        txtRuleFormat.EditValueChanged += txtPerview_EditValueChanged;
        txtFlagLastMillisecond.EditValueChanged += txtPerview_EditValueChanged;
        txtFlagSpilitNo.EditValueChanged += txtPerview_EditValueChanged;
        txtFlagIncludeDocCode.EditValueChanged += txtPerview_EditValueChanged;
        txtNoLength.EditValueChanged += txtPerview_EditValueChanged;
        txtDocCode.EditValueChanged += txtPerview_EditValueChanged;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtDocCode.Tag = DocNoRule.FieldDocCode;
        txtRuleFormat.Tag = DocNoRule.FieldRuleFormat;
        txtNoLength.Tag = DocNoRule.FieldNoLength;
        txtResetZero.Tag = DocNoRule.FieldResetZero;
        txtFlagSpilitNo.Tag = DocNoRule.FieldFlagSpilitNo;
        txtFlagIncludeDocCode.Tag = DocNoRule.FieldFlagIncludeDocCode;
        txtFlagLastMillisecond.Tag = DocNoRule.FieldFlagLastMillisecond;
        txtCurrentValue.Tag = DocNoRule.FieldCurrentValue;
        txtCurrentYMD.Tag = DocNoRule.FieldCurrentYMD;
        txtCreationDate.Tag = DocNoRule.FieldCreationDate;
        txtCreatedBy.Tag = DocNoRule.FieldCreatedBy;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(DocNoRuleInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "单号规则";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}

    private void txtElement_EditValueChanged(object? sender, EventArgs e)
    {
        TempInfo.RuleFormat = txtElement1.Text + txtElement2.Text + txtElement3.Text + txtElement4.Text;
        TempInfo.ResetZero = !TempInfo.RuleFormat.IsNullOrEmpty();
    }

    private void txtPerview_EditValueChanged(object? sender, EventArgs e)
    {
        DoPreviewCode();
    }
    
    private void DoPreviewCode()
    {
        if (txtNoLength.Text.ObjToInt() == 0)
        {
            txtPreview.Text = "请输入自增数长度.";
        }
        else
        {
            // txtRuleFormat="<年年>", "<年年年年>", "<月月>", "<日日>" });
            var preview = txtRuleFormat.Text;
            var length = txtNoLength.Text.ObjToInt();
            if (length == 0) length = 5;

            var docCode = "";
            if (txtFlagIncludeDocCode.IsOn)
                docCode = txtDocCode.EditValue.ToString();

            preview = docCode + preview;
            preview = preview.Replace("<无>", "");
            preview = preview.Replace("<年年年年>", "2022");
            preview = preview.Replace("<年年>", "22");
            preview = preview.Replace("<月月>", "06");
            preview = preview.Replace("<日日>", "18");
            preview = preview + (txtFlagSpilitNo.IsOn ? "-" : "") + "8".PadLeft(length, '0');
            txtPreview.Text = txtFlagLastMillisecond.IsOn ? preview + DateTime.Now.Millisecond : preview;
        }
    }
}
