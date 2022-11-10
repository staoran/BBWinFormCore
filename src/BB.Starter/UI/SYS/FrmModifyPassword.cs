using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.HttpServices.Core.User;

namespace BB.Starter.UI.SYS;

public partial class FrmModifyPassword : BaseDock
{
    private readonly UserHttpService _userHttpService;

    public FrmModifyPassword(UserHttpService userHttpService)
    {
        _userHttpService = userHttpService;
        InitializeComponent();
    }

    private void FrmModifyPassword_Load(object? sender, EventArgs e)
    {
        txtLogin.Text = GB.LoginUserInfo.FullName;
    }

    private async void btnOK_Click(object? sender, EventArgs e)
    {
        #region 输入验证
        if (txtRePassword.Text != txtPassword.Text)
        {
            "两个新密码的输入不一致".ShowUxTips();
            txtRePassword.Focus();
            return;
        }
        #endregion

        try
        {
            bool result = await _userHttpService.ModifyPasswordAsync(GB.LoginUserInfo.Name, txtPassword.Text);

            if (result)
            {
                DialogResult = DialogResult.OK;
                "密码修改成功".ShowUxTips();
            }
            else
            {
                "用户密码资料不正确，请核对".ShowUxWarning();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ShowUxError();
        }
    }
}