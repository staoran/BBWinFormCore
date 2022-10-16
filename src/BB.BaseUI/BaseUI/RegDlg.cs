using System.ComponentModel;
using System.Diagnostics;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Tools.Encrypt;
using Microsoft.Win32;

namespace BB.BaseUI.BaseUI;

/// <summary>
/// RegDlg 的摘要说明。
/// </summary>
public class RegDlg : BaseForm
{
    public Label label1;
    private TextBox _tbMachineCode;
    private Label _label2;
    private TextBox _tbSerialNumber;
    private Label _label3;
    private Container _components = null;
    private LinkLabel _linkLabel1;
    private Label _label4;
    private DevExpress.XtraEditors.SimpleButton _btRegister;
    private DevExpress.XtraEditors.SimpleButton _btnCopy;

    private static RegDlg _instance;

    public static RegDlg Instance()
    {
        if (_instance == null || _instance.IsDisposed)
            _instance = new RegDlg();
        return _instance;
    }

    protected RegDlg()
    {
        InitializeComponent();
        StartPosition = FormStartPosition.CenterScreen;
    }

    /// <summary>
    /// 清理所有正在使用的资源。
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_components != null)
            {
                _components.Dispose();
            }
        }
        base.Dispose(disposing);
    }

    #region Windows 窗体设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要使用代码编辑器修改
    /// 此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
        ComponentResourceManager resources = new ComponentResourceManager(typeof(RegDlg));
        label1 = new Label();
        _tbMachineCode = new TextBox();
        _label2 = new Label();
        _tbSerialNumber = new TextBox();
        _label3 = new Label();
        _btRegister = new DevExpress.XtraEditors.SimpleButton();
        _linkLabel1 = new LinkLabel();
        _label4 = new Label();
        _btnCopy = new DevExpress.XtraEditors.SimpleButton();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) 
                                         | AnchorStyles.Right)));
        label1.Font = new System.Drawing.Font("宋体", 9F);
        label1.ImeMode = ImeMode.NoControl;
        label1.Location = new System.Drawing.Point(21, 24);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(547, 49);
        label1.TabIndex = 0;
        label1.Text = "根据您的机器码，请使用下面的邮件地址，联系作者获取正确的序列号";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // tbMachineCode
        // 
        _tbMachineCode.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) 
                                                 | AnchorStyles.Right)));
        _tbMachineCode.Location = new System.Drawing.Point(104, 93);
        _tbMachineCode.Name = "_tbMachineCode";
        _tbMachineCode.ReadOnly = true;
        _tbMachineCode.Size = new System.Drawing.Size(419, 22);
        _tbMachineCode.TabIndex = 1;
        // 
        // label2
        // 
        _label2.ImeMode = ImeMode.NoControl;
        _label2.Location = new System.Drawing.Point(8, 93);
        _label2.Name = "_label2";
        _label2.Size = new System.Drawing.Size(88, 24);
        _label2.TabIndex = 0;
        _label2.Text = "机器码：";
        _label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // tbSerialNumber
        // 
        _tbSerialNumber.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) 
                                                  | AnchorStyles.Right)));
        _tbSerialNumber.Location = new System.Drawing.Point(104, 144);
        _tbSerialNumber.MinimumSize = new System.Drawing.Size(286, 100);
        _tbSerialNumber.Multiline = true;
        _tbSerialNumber.Name = "_tbSerialNumber";
        _tbSerialNumber.ScrollBars = ScrollBars.Vertical;
        _tbSerialNumber.Size = new System.Drawing.Size(481, 107);
        _tbSerialNumber.TabIndex = 1;
        // 
        // label3
        // 
        _label3.ImeMode = ImeMode.NoControl;
        _label3.Location = new System.Drawing.Point(8, 144);
        _label3.Name = "_label3";
        _label3.Size = new System.Drawing.Size(88, 25);
        _label3.TabIndex = 0;
        _label3.Text = "序列号：";
        _label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // btRegister
        // 
        _btRegister.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
        _btRegister.Location = new System.Drawing.Point(489, 267);
        _btRegister.Name = "_btRegister";
        _btRegister.Size = new System.Drawing.Size(96, 25);
        _btRegister.TabIndex = 2;
        _btRegister.Text = "注册";
        _btRegister.Click += btRegister_Click;
        // 
        // linkLabel1
        // 
        _linkLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
        _linkLabel1.ImeMode = ImeMode.NoControl;
        _linkLabel1.LinkArea = new LinkArea(0, 21);
        _linkLabel1.Location = new System.Drawing.Point(101, 312);
        _linkLabel1.Name = "_linkLabel1";
        _linkLabel1.Size = new System.Drawing.Size(230, 26);
        _linkLabel1.TabIndex = 3;
        _linkLabel1.TabStop = true;
        _linkLabel1.Text = "bubing@bb.com";
        _linkLabel1.UseCompatibleTextRendering = true;
        _linkLabel1.LinkClicked += linkLabel1_LinkClicked;
        // 
        // label4
        // 
        _label4.ImeMode = ImeMode.NoControl;
        _label4.Location = new System.Drawing.Point(24, 312);
        _label4.Name = "_label4";
        _label4.Size = new System.Drawing.Size(72, 24);
        _label4.TabIndex = 4;
        _label4.Text = "作者邮箱：";
        // 
        // btnCopy
        // 
        _btnCopy.Location = new System.Drawing.Point(530, 93);
        _btnCopy.Name = "_btnCopy";
        _btnCopy.Size = new System.Drawing.Size(55, 24);
        _btnCopy.TabIndex = 5;
        _btnCopy.Text = "复制";
        _btnCopy.Click += btnCopy_Click;
        // 
        // RegDlg
        // 
        ClientSize = new System.Drawing.Size(606, 350);
        Controls.Add(_btnCopy);
        Controls.Add(_label4);
        Controls.Add(_linkLabel1);
        Controls.Add(_btRegister);
        Controls.Add(_tbMachineCode);
        Controls.Add(label1);
        Controls.Add(_label2);
        Controls.Add(_tbSerialNumber);
        Controls.Add(_label3);
        Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new System.Drawing.Size(419, 377);
        Name = "RegDlg";
        StartPosition = FormStartPosition.CenterParent;
        Text = "注册";
        Load += RegDlg_Load;
        ResumeLayout(false);
        PerformLayout();

    }

    #endregion

    private void btRegister_Click(object? sender, EventArgs e)
    {
        bool passed = GB.Register(_tbSerialNumber.Text);
        if (passed)
        {
            GB.Registed = true;

            SaverInformationToRegedit(_tbSerialNumber.Text);
            "祝贺您，注册成功".ShowUxTips();
            Close();
            Application.Exit();
        }
        else
        {
            "对不起，注册失败".ShowUxWarning();
        }
    }

    /// <summary>
    /// 保存用户输入的序列号到注册表当中
    /// </summary>
    /// <param name="mySerail"></param>
    private void SaverInformationToRegedit(string mySerail)
    {
        RegistryKey reg;
        string regkey = UiConstants.SoftwareRegistryKey;
        reg = Registry.CurrentUser.OpenSubKey(regkey, true);
        if (null == reg)
        {
            reg = Registry.CurrentUser.CreateSubKey(regkey);
        }
        if (null != reg)
        {
            reg.SetValue("productName", UiConstants.SoftwareProductName);
            reg.SetValue("version", UiConstants.SoftwareVersion);
            reg.SetValue("SerialNumber", mySerail);
        }
    }

    private void RegDlg_Load(object? sender, EventArgs e)
    {
        _tbMachineCode.Text = GB.GetHardNumber();
    }

    private void linkLabel1_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            VisitLink();
        }
        catch (Exception)
        {
            MessageBox.Show("不能打开链接！");
        }
    }

    private void VisitLink()
    {
        _linkLabel1.LinkVisited = true;
        Process.Start("mailto:" + "bubing@bb.com");
    }

    private void btnCopy_Click(object? sender, EventArgs e)
    {
        Clipboard.SetText(_tbMachineCode.Text);
    }
}