using System.Diagnostics;
using BB.BaseUI.Extension;
using BB.Tools.Const;
using BB.Tools.Extension;
using BB.Tools.File;
using BB.Tools.Utils;
using Furion.Logging.Extensions;

namespace BB.BaseUI.Other;

public partial class FrmException : Form
{
    #region 全局变量
    Exception _bugInfo;
    #endregion

    #region 构造函数
    /// <summary>
    /// Bug发送窗口
    /// </summary>
    /// <param name="bugInfo">Bug信息</param>
    public FrmException(Exception bugInfo)
    {
        InitializeComponent();
        _bugInfo = bugInfo;
        txtBugInfo.Text = bugInfo.Message;
        lblErrorCode.Text = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Bug发送窗口
    /// </summary>
    /// <param name="bugInfo">Bug信息</param>
    /// <param name="errorCode">错误号</param>
    public FrmException(Exception bugInfo, string errorCode)
    {
        InitializeComponent();
        _bugInfo = bugInfo;
        txtBugInfo.Text = bugInfo.Message;
        lblErrorCode.Text = errorCode;
    }
    #endregion

    #region 公开静态方法
    /// <summary>
    /// 提示Bug
    /// </summary>
    /// <param name="bugInfo">Bug信息</param>
    /// <param name="errorCode">错误号</param>
    public static void ShowBug(Exception bugInfo, string errorCode)
    {
        new FrmException(bugInfo, errorCode).ShowDialog();
    }

    /// <summary>
    /// 提示Bug
    /// </summary>
    /// <param name="bugInfo">Bug信息</param>
    public static void ShowBug(Exception bugInfo)
    {
        ShowBug(bugInfo, Guid.NewGuid().ToString());
    }
    #endregion

    private void btnDetailsInfo_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("异常详细信息：" + _bugInfo.Message + "\r\n跟踪：" + _bugInfo.StackTrace);
    }

    /// <summary>
    /// 处理异常
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnOk_Click(object? sender, EventArgs e)
    {
        // 发送错误报告 
        if (chkSendBug.Checked)
        {
            DialogResult = DialogResult.None;
            if (txtContentInfo.Text == null || txtContentInfo.Text.Trim().Length < 10)
            {
                "您反馈内容太短(少于10个字符），请输入详细一些内容，谢谢。".ShowUxTips();
                txtContentInfo.Focus();
                return;
            }

            if (SendEmail())
            {
                DialogResult = DialogResult.OK;
                "反馈成功，感谢你的本产品的支持".ShowUxTips();
            }
            else
            {
                DialogResult = DialogResult.No;
                "反馈失败".ShowUxError();
            }
        }

        // 重启程序
        if (chkReboot.Checked)
        {
            string strAppFileName = Process.GetCurrentProcess().MainModule.FileName;
            Process myNewProcess = new Process();
            myNewProcess.StartInfo.FileName = strAppFileName;
            myNewProcess.StartInfo.Arguments = Const.RESTART;
            myNewProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            myNewProcess.Start();
            Application.ExitThread();  
        }

        Close();
    }

    private bool SendEmail()
    {
        Thread.Sleep(Const.SLEEP_TIME);
        EmailHelper email = new EmailHelper("smtp.163.com", "codeany@163.com", "abc123")
        {
            Subject = string.Format("Hummar 框架奔溃反馈 "),
            Body = "这个问题是如何出现的:"+txtContentInfo.Text+"<br />异常详细信息：" + _bugInfo.Message + "<br />跟踪日志：<br />" + _bugInfo.StackTrace + "<br />",
            IsHtml = true,
            From = "codeany@163.com",
            FromName = "Hummar 官方邮箱"
        };
        email.AddRecipient("codeany@163.com");
        email.RecipientName = "Hummar 官方邮箱";

        // 把日志打包以附件的形式发送到邮箱
        if (chkCanHelp.Checked)
        {
            var config = GB.Config;
            // if (config == null)
            // {
            //     config = new AppConfig();
            //     Cache.Instance.Set<AppConfig>("AppConfig", config);
            // }
            string licensePath = config.AppConfigGet("LicensePath");
            if (FileUtil.IsExistFile(licensePath))
            {
                string[] tmpstr = FileUtil.FileToString(licensePath).Split(Convert.ToChar(Const.VERTICAL_LINE));

                if (tmpstr.Length == 3)
                {
                    email.Body += "<br /> 注册用户:" + tmpstr[1] + "<br />注册公司:" + tmpstr[2];
                }
            }
        }
        bool success = false;

        try
        {
            success = email.SendEmail();
            return success;
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();
            ex.Message.ShowUxError();
            success = false;
            return success;
        }
    }

}