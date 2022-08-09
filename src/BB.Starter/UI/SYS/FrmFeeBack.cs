using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.Tools.Utils;
using BB.Tools.Validation;
using Furion.Logging.Extensions;

namespace BB.Starter.UI.SYS;

public partial class FrmFeeBack : BaseForm
{
    private int _maxTry = 3;
    private int _currentTry = 0;

    public FrmFeeBack()
    {
        InitializeComponent();
    }

    private void FrmFeeBack_FormClosing(object sender, FormClosingEventArgs e)
    {
        txtAdvise.Dispose();//显式关闭空间，防止错误出现
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        #region 检查地址
        DialogResult = DialogResult.None;
        if (txtAdvise.BodyHtml == null || txtAdvise.BodyHtml.Trim().Length < 10)
        {
            "您的建议太短(少于10个字符），请输入详细一些内容，谢谢。".ShowUxTips();
            txtAdvise.Focus();
            return;
        }
        else if (txtEmail.Text.Length == 0 || !ValidateUtil.IsEmail(txtEmail.Text))
        {
            "请输入邮件地址，以便我们能够快速联系到您。".ShowUxTips();
            txtEmail.Focus();
            return;
        }
        #endregion

        SendEmail();
        DialogResult = DialogResult.OK;
        "谢谢您的建议！".ShowUxTips();
    }

    private void SendEmail()
    {
        Thread.Sleep(100);
        _currentTry++;

        EmailHelper email = new EmailHelper("smtp.163.com", "bubing@bb.com", "123abc")
        {
            Subject = $"来自【{txtEmail.Text}】对Winform开发框架的建议",
            Body = txtAdvise.BodyHtml, //支持嵌入图片的内容发送
            IsHtml = true,
            From = "bubing@bb.com",
            FromName = "bubing"
        };
        email.AddRecipient("bubing@bb.com");
        //email.AddAttachment(System.IO.Path.Combine(Application.StartupPath, "cityroad.jpg")); 

        try
        {
            bool success = email.SendEmail();
            if (success)
            {
                _currentTry = 0;
            }
            else if (_currentTry < _maxTry)
            {
                email.ErrorMessage.LogError();
                SendEmail();
            }
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();;

            if (_currentTry <= _maxTry)
            {
                SendEmail();
            }
            _currentTry = 0;
        }  
    }
}