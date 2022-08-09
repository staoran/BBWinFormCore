using System.ComponentModel;
using SettingsProviderNet;

namespace BB.Starter.UI.Settings;

/// <summary>
/// 邮箱设置
/// </summary>
public class EmailParameter
{
    /// <summary>
    /// 邮件账号
    /// </summary>
    //[DefaultValue("bubing@bb.com")]
    public string Email { get; set; }

    /// <summary>
    /// POP3服务器
    /// </summary>
    [DefaultValue("pop.163.com")]
    public string Pop3Server { get; set; }

    /// <summary>
    /// POP3端口
    /// </summary>
    [DefaultValue(110)]
    public int Pop3Port { get; set; }

    /// <summary>
    /// SMTP服务器
    /// </summary>
    [DefaultValue("smtp.163.com")]
    public string SmtpServer { get; set; }

    /// <summary>
    /// SMTP端口
    /// </summary>
    [DefaultValue(25)]
    public int SmtpPort { get; set; }

    /// <summary>
    /// 登陆账号
    /// </summary>
    public string LoginId { get; set; }

    /// <summary>
    /// 登陆密码
    /// </summary>
    [ProtectedString]
    public string Password { get; set; }

    /// <summary>
    /// 使用SSL加密
    /// </summary>
    [DefaultValue(false)]
    public bool UseSsl { get; set; }
}