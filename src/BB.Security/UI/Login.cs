using System.Windows.Forms;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using Furion.Logging.Extensions;

namespace BB.Security.UI;

public partial class Login : DevExpress.XtraEditors.XtraForm
{
    public bool bLogin = false; //判断用户是否登录

    public Login()
    {
        InitializeComponent();

        txtUserName.Focus();
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        #region 检查验证
        if (txtUserName.Text.Length == 0)
        {
            "请输入帐号".ShowUxTips();
            txtUserName.Focus();
            return;
        }
        #endregion

        try
        {
            string loginName = txtUserName.Text.Trim();
            if (!await SecurityHelper.Login(loginName, txtPassword.Text, "Security", true)) return;
            bLogin = true;
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();
            ex.Message.ShowUxError();
        }
    }


    private void labelControl1_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Application.Exit();
    }

    private void Login_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnLogin_Click(sender, e);
        }
    }

    private void txtUserName_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            txtPassword.Focus();
        }
    }

    private void txtPassword_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnLogin_Click(sender, e);
        }
    }

    private void txtRole_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnLogin_Click(sender, e);
        }
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close(); 
        //Application.ExitThread();
    }
}