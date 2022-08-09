using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.Settings;
using Furion.Logging.Extensions;
using SettingsProviderNet;

namespace BB.Starter.UI.Settings;

public partial class PageEmail : PropertyPage
{
    private SettingsProvider _settings;
    private ISettingsStorage _store;

    public PageEmail()
    {
        InitializeComponent();

        if (!DesignMode)
        {
            //DatabaseStorage：在数据库里面，以指定用户标识保存参数数据
            string creator = GB.LoginUserInfo.Name;
            _store = new DatabaseStorage(creator);
            _settings = new SettingsProvider(_store);
        }
    }

    public override void OnInit()
    {
        EmailParameter parameter = _settings.GetSettings<EmailParameter>();
        if (parameter != null)
        {
            txtEmail.Text = parameter.Email;
            txtLoginId.Text = parameter.LoginId;
            txtPassword.Text = parameter.Password;
            txtPassword.Tag = parameter.Password;
            txtPop3Port.Value = parameter.Pop3Port;
            txtPop3Server.Text = parameter.Pop3Server;
            txtSmtpPort.Value = parameter.SmtpPort;
            txtSmtpServer.Text = parameter.SmtpServer;
            txtUseSSL.Checked = parameter.UseSsl;
        }
    }

    public override void OnSetActive()
    {
    }

    public override bool OnApply()
    {
        bool result = false;
        try
        {
            //如果密码修改，需要确认密码
            if (txtPassword.Tag != null && txtPassword.Tag.ToString() != txtPassword.Text)
            {
                string confirmPassword = "请确认密码".QueryInputStrByUx("", true);
                if (confirmPassword != txtPassword.Text)
                {
                    "两次密码输入不一致".ShowUxTips();
                    txtPassword.Focus();
                    return result;
                }
            }

            EmailParameter parameter = _settings.GetSettings<EmailParameter>();
            if (parameter != null)
            {                    
                parameter.Email = txtEmail.Text;
                parameter.LoginId = txtLoginId.Text;
                parameter.Password = txtPassword.Text;
                parameter.Pop3Port = Convert.ToInt32(txtPop3Port.Value);
                parameter.Pop3Server = txtPop3Server.Text;
                parameter.SmtpPort = Convert.ToInt32(txtSmtpPort.Value);
                parameter.SmtpServer = txtSmtpServer.Text;
                parameter.UseSsl = txtUseSSL.Checked;

                _settings.SaveSettings<EmailParameter>(parameter);
            }
            result = true;
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();;
            ex.Message.ShowUxError();
        }

        return result;
    }
}