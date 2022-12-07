using System.ComponentModel;
using System.Diagnostics;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Tools.Encrypt;
using BB.Tools.Utils;
using DevExpress.XtraEditors;
using Furion.Logging.Extensions;

namespace BB.BaseUI.BaseUI;

/// <summary>
/// Logon 的摘要说明。
/// </summary>
public class Logon : XtraForm
{
	#region Private Members

	private GroupBox _groupBox1;
	private Label _label1;
	private Label _label2;
	private SimpleButton _btExit;
	private SimpleButton _btLogin;
	private ComboBoxEdit _cmbzhanhao;
	private TextEdit _tbPass;
	private Label _lblTitle;
	private Label _lblCalendar;
	private LinkLabel _linkHelp;
	private LinkLabel _lnkSecurity;

	/// <summary>
	/// 必需的设计器变量。
	/// </summary>
	private readonly Container _components = null;

	#endregion

	public bool BLogin = false; //判断用户是否登录
	private readonly RegisterHotKeyHelper _hotKey1 = new();
	private LabelControl _lnkRegister;
	private readonly RegisterHotKeyHelper _hotKey2 = new();

	public Logon()
	{
		InitializeComponent();
#if !DEBUG
		_lnkSecurity.Visible = false;
#endif

		//初始化账号列表
		try
		{
			//InitLoginName();
			InitHistoryLoginName();
			// SetHotKey();
		}
		catch (Exception ex)
		{
			ex.ToString().LogError();
			ex.Message.ShowUxError();
		}

		_btExit.DialogResult = DialogResult.Cancel;
	}

	/// <summary>
	/// 设置F1帮助 F2权限系统 的全局热键
	/// </summary>
	private void SetHotKey()
	{
		_hotKey1.Keys = Keys.F1;
		_hotKey1.ModKey = 0;
		_hotKey1.WindowHandle = Handle;
		_hotKey1.WParam = 10001;
		_hotKey1.HotKey += hotKey1_HotKey;
		_hotKey1.StarHotKey();

		_hotKey2.Keys = Keys.F2;
		_hotKey2.ModKey = 0;
		_hotKey2.WindowHandle = Handle;
		_hotKey2.WParam = 10002;
		_hotKey2.HotKey += hotKey2_HotKey;
		_hotKey2.StarHotKey();
	}

	void hotKey1_HotKey()
	{
		linkHelp_LinkClicked(null, null);
	}

	void hotKey2_HotKey()
	{
		lnkSecurity_LinkClicked(null, null);
	}

	/// <summary>
	/// 初始化账号登录列表
	/// </summary>
	private void InitHistoryLoginName()
	{
		string loginNames = RegistryHelper.GetValue(GB.LoginNameKey);
		if (!string.IsNullOrEmpty(loginNames))
		{
			if (_cmbzhanhao.Properties.Items.Count > 0)
			{
				_cmbzhanhao.SetComboBoxItem(loginNames);
			}
			else
			{
				_cmbzhanhao.Text = loginNames;
			}
		}
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
		_tbPass = new TextEdit();
		_cmbzhanhao = new ComboBoxEdit();
		_label1 = new Label();
		_label2 = new Label();
		_btExit = new SimpleButton();
		_btLogin = new SimpleButton();
		_lblTitle = new Label();
		_linkHelp = new LinkLabel();
		_lblCalendar = new Label();
		_lnkSecurity = new LinkLabel();
		_lnkRegister = new LabelControl();
		_groupBox1.SuspendLayout();
		((ISupportInitialize)(_cmbzhanhao.Properties)).BeginInit();
		SuspendLayout();
		// 
		// groupBox1
		// 
		_groupBox1.BackColor = Color.Transparent;
		_groupBox1.Controls.Add(_tbPass);
		_groupBox1.Controls.Add(_cmbzhanhao);
		_groupBox1.Controls.Add(_label1);
		_groupBox1.Controls.Add(_label2);
		_groupBox1.Location = new Point(72, 86);
		_groupBox1.Name = "_groupBox1";
		_groupBox1.Size = new Size(352, 150);
		_groupBox1.TabIndex = 0;
		_groupBox1.TabStop = false;
		_groupBox1.Text = @"登录信息";
		// 
		// tbPass
		// 
		_tbPass.Location = new Point(96, 86);
		_tbPass.Name = "_tbPass";
		_tbPass.Properties.PasswordChar = '*';
		_tbPass.Size = new Size(184, 22);
		_tbPass.TabIndex = 1;
		_tbPass.KeyDown += tbPass_KeyDown;
		// 
		// cmbzhanhao
		// 
		_cmbzhanhao.Location = new Point(96, 43);
		_cmbzhanhao.Name = "_cmbzhanhao";
		_cmbzhanhao.Properties.Buttons.AddRange(new[] {
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
		_cmbzhanhao.Size = new Size(184, 20);
		_cmbzhanhao.TabIndex = 0;
		_cmbzhanhao.KeyDown += cmbzhanhao_KeyDown;
		// 
		// label1
		// 
		_label1.Location = new Point(32, 43);
		_label1.Name = "_label1";
		_label1.Size = new Size(56, 24);
		_label1.TabIndex = 0;
		_label1.Text = @"登录账号";
		_label1.TextAlign = ContentAlignment.MiddleRight;
		// 
		// label2
		// 
		_label2.Location = new Point(32, 86);
		_label2.Name = "_label2";
		_label2.Size = new Size(56, 24);
		_label2.TabIndex = 0;
		_label2.Text = "登录密码";
		_label2.TextAlign = ContentAlignment.MiddleRight;
		// 
		// btExit
		// 
		_btExit.Location = new Point(349, 268);
		_btExit.Name = "_btExit";
		_btExit.Size = new Size(75, 25);
		_btExit.TabIndex = 1;
		_btExit.Text = "退出";
		_btExit.Click += btExit_Click;
		// 
		// btLogin
		// 
		_btLogin.Location = new Point(248, 268);
		_btLogin.Name = "_btLogin";
		_btLogin.Size = new Size(75, 25);
		_btLogin.TabIndex = 0;
		_btLogin.Text = "登录";
		_btLogin.Click += btLogin_Click;
		// 
		// lblTitle
		// 
		_lblTitle.BackColor = Color.Transparent;
		_lblTitle.Font = new Font("华文行楷", 18F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
		_lblTitle.ForeColor = Color.DodgerBlue;
		_lblTitle.Location = new Point(32, 17);
		_lblTitle.Name = "_lblTitle";
		_lblTitle.Size = new Size(370, 25);
		_lblTitle.TabIndex = 3;
		_lblTitle.Text = "仓库管理系统登录界面";
		_lblTitle.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// linkHelp
		// 
		_linkHelp.BackColor = Color.Transparent;
		_linkHelp.Location = new Point(440, 34);
		_linkHelp.Name = "_linkHelp";
		_linkHelp.Size = new Size(56, 25);
		_linkHelp.TabIndex = 2;
		_linkHelp.TabStop = true;
		_linkHelp.Text = "寻求帮助";
		_linkHelp.TextAlign = ContentAlignment.MiddleCenter;
		_linkHelp.LinkClicked += linkHelp_LinkClicked;
		// 
		// lblCalendar
		// 
		_lblCalendar.BackColor = Color.Transparent;
		_lblCalendar.Location = new Point(59, 318);
		_lblCalendar.Name = "_lblCalendar";
		_lblCalendar.Size = new Size(343, 25);
		_lblCalendar.TabIndex = 6;
		// 
		// lnkSecurity
		// 
		_lnkSecurity.AutoSize = true;
		_lnkSecurity.BackColor = Color.Transparent;
		_lnkSecurity.Location = new Point(398, 327);
		_lnkSecurity.Name = "_lnkSecurity";
		_lnkSecurity.Size = new Size(102, 14);
		_lnkSecurity.TabIndex = 7;
		_lnkSecurity.TabStop = true;
		_lnkSecurity.Text = "代码生成(F2)";
		_lnkSecurity.LinkClicked += lnkSecurity_LinkClicked;
		// 
		// lnkRegister
		// 
		_lnkRegister.Appearance.Font = new Font("新宋体", 14.25F, ((FontStyle)((FontStyle.Bold | FontStyle.Underline))), GraphicsUnit.Point, ((byte)(134)));
		_lnkRegister.Appearance.ForeColor = Color.Red;
		_lnkRegister.Cursor = Cursors.Hand;
		_lnkRegister.Location = new Point(132, 274);
		_lnkRegister.Name = "_lnkRegister";
		_lnkRegister.Size = new Size(84, 19);
		_lnkRegister.TabIndex = 8;
		_lnkRegister.Text = "软件注册";
		_lnkRegister.Click += lnkRegister_Click;
		// 
		// Logon
		// 
		BackgroundImageLayoutStore = ImageLayout.Tile;
		BackgroundImageStore = ((Image)(resources.GetObject("$this.BackgroundImageStore")));
		ClientSize = new Size(500, 350);
		Controls.Add(_lnkRegister);
		Controls.Add(_lnkSecurity);
		Controls.Add(_lblCalendar);
		Controls.Add(_linkHelp);
		Controls.Add(_lblTitle);
		Controls.Add(_btLogin);
		Controls.Add(_btExit);
		Controls.Add(_groupBox1);
		Icon = ((Icon)(resources.GetObject("$this.Icon")));
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "Logon";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "仓库管理系统登录界面";
		Load += Logon_Load;
		KeyDown += Logon_KeyDown;
		_groupBox1.ResumeLayout(false);
		_groupBox1.PerformLayout();
		((ISupportInitialize)(_cmbzhanhao.Properties)).EndInit();
		ResumeLayout(false);
		PerformLayout();

	}

	#endregion

	private void btExit_Click(object? sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
	}

	private async void btLogin_Click(object? sender, EventArgs e)
	{
		try
		{
			SplashScreenHelper.Show();

			#region 检查验证

			if (GB.EnableRegister && !GB.Registed)
			{
				"软件未进行授权注册，不能使用。\r\n请单击左边【软件注册】按钮进行注册。".ShowErrorTip(this);
				return;
			}

			if (_cmbzhanhao.Text.Length == 0)
			{
				"请输入帐号".ShowErrorTip(_cmbzhanhao);
				_cmbzhanhao.Focus();
				return;
			}

			#endregion

			string loginName = _cmbzhanhao.Text.Trim();
			if (!await SecurityHelper.Login(loginName, _tbPass.Text, GB.SystemType)) return;
			BLogin = true;
			DialogResult = DialogResult.OK;
		}
		catch (Exception err)
		{
			err.Message.ShowUxError();
		}
		finally
		{
			SplashScreenHelper.Close();
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

	private void cmbzhanhao_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Enter) //如果用户回车，跳转到密码的输入框，接受输入
		{
			_tbPass.Focus();
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

	/// <summary>
	/// 开始的时候提示登录账号和密码
	/// </summary>
	private void Logon_Load(object? sender, EventArgs e)
	{
		ToolTip tp = new ToolTip();
		tp.SetToolTip(_cmbzhanhao, "首次登录的默认账号为:admin");
		tp.SetToolTip(_tbPass, "首次登录的默认密码为空, \r\n 以后请更改默认密码！");
			
		CCalendar cal = new CCalendar();
		_lblCalendar.Text = cal.GetDateInfo(DateTime.Now).Fullinfo;
		Text = GB.Config.AppName;
		_lblTitle.Text = GB.Config.AppName;

		//ValidateRegisterStatus();//提示是否已经注册

		if (_cmbzhanhao.Text != "")
		{
			_tbPass.Focus();
		}
		else
		{
			_cmbzhanhao.Focus();
		}

		if (GB.EnableRegister)
		{
			//先处理用户的注册信息
			GB.CheckRegister();

			if (!GB.Registed)
			{
				_lnkRegister.Visible = true;
				_btLogin.ForeColor = Color.Red;
				ToolTip tip = new ToolTip();
				tip.SetToolTip(_btLogin, "软件未进行授权注册，不能使用。\r\n请单击【软件注册】按钮进行注册。");

				Text += @" [未注册]";
			}
			else
			{
				_lnkRegister.Visible = false;
				//Text += " [已注册]";
			}
		}
		else
		{
			_lnkRegister.Visible = false;
		}
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

	private void lnkSecurity_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
	{
		DialogResult = DialogResult.Yes;
		// Security.UI.Portal.StartLogin();
	}

	private void lnkRegister_Click(object? sender, EventArgs e)
	{
		RegDlg myRegdlg = RegDlg.Instance();
		myRegdlg.ShowDialog();
	}
}