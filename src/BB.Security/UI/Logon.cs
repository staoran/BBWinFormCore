using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Tools.Encrypt;
using BB.Tools.Utils;

namespace BB.Security.UI;

/// <summary>
/// Logon 的摘要说明。
/// </summary>
public class Logon : BaseForm
{
	#region Private Members

	private GroupBox _groupBox1;
	private Label _label1;
	private Label _label2;
	private DevExpress.XtraEditors.SimpleButton _btExit;
	private DevExpress.XtraEditors.SimpleButton _btLogin;
	private DevExpress.XtraEditors.TextEdit _txtPassword;
	private Label _label3;
	private LinkLabel _linkHelp;
	private Label _lblCalendar;

	/// <summary>
	/// 必需的设计器变量。
	/// </summary>
	private Container _components = null;

	#endregion
	private DevExpress.XtraEditors.ComboBoxEdit _txtLogin;


	public bool bLogin = false; //判断用户是否登陆

	public Logon()
	{
		InitializeComponent();

		//初始化账号列表
		//Initzhanhao();
		_btExit.DialogResult = DialogResult.Cancel;
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
		ComponentResourceManager resources = new ComponentResourceManager(typeof(Logon));
		_groupBox1 = new GroupBox();
		_txtPassword = new DevExpress.XtraEditors.TextEdit();
		_label1 = new Label();
		_label2 = new Label();
		_btExit = new DevExpress.XtraEditors.SimpleButton();
		_btLogin = new DevExpress.XtraEditors.SimpleButton();
		_label3 = new Label();
		_linkHelp = new LinkLabel();
		_lblCalendar = new Label();
		_txtLogin = new DevExpress.XtraEditors.ComboBoxEdit();
		_groupBox1.SuspendLayout();
		((ISupportInitialize)(_txtPassword.Properties)).BeginInit();
		((ISupportInitialize)(_txtLogin.Properties)).BeginInit();
		SuspendLayout();
		// 
		// groupBox1
		// 
		_groupBox1.BackColor = System.Drawing.Color.Transparent;
		_groupBox1.Controls.Add(_txtPassword);
		_groupBox1.Controls.Add(_label1);
		_groupBox1.Controls.Add(_label2);
		_groupBox1.Controls.Add(_txtLogin);
		_groupBox1.Location = new System.Drawing.Point(72, 86);
		_groupBox1.Name = "_groupBox1";
		_groupBox1.Size = new System.Drawing.Size(352, 154);
		_groupBox1.TabIndex = 0;
		_groupBox1.TabStop = false;
		_groupBox1.Text = "登陆信息";
		// 
		// txtPassword
		// 
		_txtPassword.Location = new System.Drawing.Point(96, 86);
		_txtPassword.Name = "_txtPassword";
		_txtPassword.Properties.PasswordChar = '*';
		_txtPassword.Size = new System.Drawing.Size(184, 20);
		_txtPassword.TabIndex = 1;
		_txtPassword.KeyDown += tbPass_KeyDown;
		// 
		// label1
		// 
		_label1.Location = new System.Drawing.Point(32, 43);
		_label1.Name = "_label1";
		_label1.Size = new System.Drawing.Size(56, 24);
		_label1.TabIndex = 0;
		_label1.Text = "登陆账号";
		_label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// label2
		// 
		_label2.Location = new System.Drawing.Point(32, 86);
		_label2.Name = "_label2";
		_label2.Size = new System.Drawing.Size(56, 24);
		_label2.TabIndex = 0;
		_label2.Text = "登陆密码";
		_label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		// 
		// btExit
		// 
		_btExit.Location = new System.Drawing.Point(272, 264);
		_btExit.Name = "_btExit";
		_btExit.Size = new System.Drawing.Size(75, 24);
		_btExit.TabIndex = 1;
		_btExit.Text = "退出";
		_btExit.Click += btExit_Click;
		// 
		// btLogin
		// 
		_btLogin.Location = new System.Drawing.Point(177, 264);
		_btLogin.Name = "_btLogin";
		_btLogin.Size = new System.Drawing.Size(75, 24);
		_btLogin.TabIndex = 0;
		_btLogin.Text = "登陆";
		_btLogin.Click += btLogin_Click;
		// 
		// label3
		// 
		_label3.BackColor = System.Drawing.Color.Transparent;
		_label3.Font = new System.Drawing.Font("华文行楷", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
		_label3.ForeColor = System.Drawing.Color.DodgerBlue;
		_label3.Location = new System.Drawing.Point(104, 17);
		_label3.Name = "_label3";
		_label3.Size = new System.Drawing.Size(292, 25);
		_label3.TabIndex = 3;
		_label3.Text = "权限管理系统登陆界面";
		// 
		// linkHelp
		// 
		_linkHelp.BackColor = System.Drawing.Color.Transparent;
		_linkHelp.Location = new System.Drawing.Point(440, 34);
		_linkHelp.Name = "_linkHelp";
		_linkHelp.Size = new System.Drawing.Size(56, 25);
		_linkHelp.TabIndex = 5;
		_linkHelp.TabStop = true;
		_linkHelp.Text = "寻求帮助";
		_linkHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		_linkHelp.LinkClicked += linkHelp_LinkClicked;
		// 
		// lblCalendar
		// 
		_lblCalendar.BackColor = System.Drawing.Color.Transparent;
		_lblCalendar.Location = new System.Drawing.Point(72, 343);
		_lblCalendar.Name = "_lblCalendar";
		_lblCalendar.Size = new System.Drawing.Size(362, 26);
		_lblCalendar.TabIndex = 6;
		// 
		// txtLogin
		// 
		_txtLogin.Location = new System.Drawing.Point(96, 45);
		_txtLogin.Name = "_txtLogin";
		_txtLogin.Properties.Buttons.AddRange(new[] {
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
		_txtLogin.Size = new System.Drawing.Size(184, 20);
		_txtLogin.TabIndex = 0;
		_txtLogin.KeyDown += txtLogin_KeyDown;
		// 
		// Logon
		// 
		BackgroundImageLayoutStore = ImageLayout.Tile;
		BackgroundImageStore = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImageStore")));
		ClientSize = new System.Drawing.Size(496, 346);
		Controls.Add(_lblCalendar);
		Controls.Add(_linkHelp);
		Controls.Add(_label3);
		Controls.Add(_btLogin);
		Controls.Add(_btExit);
		Controls.Add(_groupBox1);
		Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
		MaximizeBox = false;
		MaximumSize = new System.Drawing.Size(512, 384);
		MinimizeBox = false;
		MinimumSize = new System.Drawing.Size(512, 384);
		Name = "Logon";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "权限管理系统登陆界面";
		Load += Logon_Load;
		KeyDown += Logon_KeyDown;
		_groupBox1.ResumeLayout(false);
		((ISupportInitialize)(_txtPassword.Properties)).EndInit();
		((ISupportInitialize)(_txtLogin.Properties)).EndInit();
		ResumeLayout(false);

	}

	#endregion

	/// <summary>
	/// 初始化账号登陆用户
	/// </summary>
	private void Initzhanhao()
	{
		_txtLogin.Text = EncryptHelper.ComputeHash("", "admin");
	}

	private void btExit_Click(object? sender, EventArgs e)
	{
		Close();
	}

	private async void btLogin_Click(object? sender, EventArgs e)
	{
		if (_txtLogin.Text.Trim() == "")
		{
			"请输入帐号".ShowUxTips();
			_txtLogin.Focus();
			return;
		}

		try
		{
			string loginName = _txtLogin.Text.Trim();
			if (!await SecurityHelper.Login(loginName, _txtPassword.Text, "Security", true)) return;
			bLogin = true;
			DialogResult = DialogResult.OK;
		}
		catch (Exception err)
		{
			err.Message.ShowUxError();
		}
	}

	/// <summary>
	/// 对字符串加密
	/// </summary>
	/// <returns></returns>
	private string EncodePassword(string passwordText)
	{
		return EncodeHelper.Md5Encrypt(passwordText);
	}

	private void Logon_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter)
		{
			btLogin_Click(sender, null!);
		}
		if (e.KeyCode == Keys.F1) //按下F1键将跳出帮助
		{
			linkHelp_LinkClicked(sender, null!);
		}
	}

	private void tbPass_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter) //密码回车，则检查用户是否符合数据库的用户
		{
			btLogin_Click(sender, null!);
		}
		if (e.KeyCode == Keys.F1) //按下F1键将跳出帮助
		{
			linkHelp_LinkClicked(sender, null!);
		}
	}

	private void txtLogin_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter) //如果用户回车，跳转到密码的输入框，接受输入
		{
			_txtPassword.Focus();
		}
		if (e.KeyCode == Keys.F1) //按下F1键将跳出帮助
		{
			linkHelp_LinkClicked(sender, null!);
		}
	}

	/// <summary>
	/// 开始的时候提示登陆账号和密码
	/// </summary>
	private void Logon_Load(object? sender, EventArgs e)
	{
		ToolTip tp = new ToolTip();
		tp.SetToolTip(_txtLogin, "首次登陆的默认账号为:admin");
		tp.SetToolTip(_txtPassword, "首次登陆的默认密码为空, \r\n 以后请更改默认密码！");
			
		//当获取用户的注册信息后，再初始化系统设置信息。
		//GB.SystemSetting = SettingUtil.Setting;
		//this.Text = GB.SystemSetting.ApplicationName + GB.SystemSetting.ApplicationVersion;

		CCalendar cal = new CCalendar();
		_lblCalendar.Text = cal.GetDateInfo(DateTime.Now).Fullinfo;
	}

	private void linkHelp_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
	{
		try
		{
			Process.Start("Help.chm");
		}
		catch (Exception)
		{
			"不能打开帮助！".ShowUxWarning();
		}
	}


}