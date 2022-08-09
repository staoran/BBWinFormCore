using BB.BaseUI.Other;
using BB.Tools.MultiLanuage;

namespace BB.BaseUI.WinForm;

/// <summary>
/// 框架统一的对话框提示辅助类
/// </summary>
public static class MessageUtil
{
    private static string _captionTips = "提示信息";
    private static string _captionWarning = "警告信息";
    private static string _captionError = "错误信息";

    static MessageUtil()
    {
        _captionTips = JsonLanguage.Default.GetString(_captionTips);
        _captionWarning = JsonLanguage.Default.GetString(_captionWarning);
        _captionError = JsonLanguage.Default.GetString(_captionError);
    }

    /// <summary>
    /// 显示一般的提示信息
    /// </summary>
    /// <param name="message">提示信息</param>
    public static DialogResult ShowTips(this string message)
    {
        return ShowTips(message, null);
    }

    /// <summary>
    /// 显示一般的提示信息
    /// </summary>
    /// <param name="message">提示信息</param>
    /// <param name="args">字符串里面的参数内容</param>
    /// <returns></returns>
    public static DialogResult ShowTips(this string message, params object[] args)
    {
        message = JsonLanguage.Default.GetString(message);
        if (args != null)
        {
            message = string.Format(message, args);
        }

        return MessageBox.Show(message, _captionTips,MessageBoxButtons.OK, MessageBoxIcon.Information);
    }


    /// <summary>
    /// 显示警告信息
    /// </summary>
    /// <param name="message">警告信息</param>
    public static DialogResult ShowWarning(this string message)
    {
        return ShowWarning(message, null);
    }

    /// <summary>
    /// 显示警告信息
    /// </summary>
    /// <param name="message">警告信息</param>
    /// <param name="args">字符串里面的参数内容</param>
    public static DialogResult ShowWarning(this string message, params object[] args)
    {
        message = JsonLanguage.Default.GetString(message);
        if (args != null)
        {
            message = string.Format(message, args);
        }

        return MessageBox.Show(message, _captionWarning,  MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    /// <summary>
    /// 显示错误信息
    /// </summary>
    /// <param name="message">错误信息</param>
    public static DialogResult ShowError(this string message)
    {
        return ShowError(message, null);
    }
    /// <summary>
    /// 显示错误信息
    /// </summary>
    /// <param name="message">错误信息</param>
    /// <param name="args">字符串里面的参数内容</param>
    public static DialogResult ShowError(this string message, params object[] args)
    {
        message = JsonLanguage.Default.GetString(message);
        if (args != null)
        {
            message = string.Format(message, args);
        }
        return MessageBox.Show(message, _captionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    /// <summary>
    /// 显示询问用户信息，并显示错误标志
    /// </summary>
    /// <param name="message">错误信息</param>
    public static DialogResult ShowYesNoAndError(this string message)
    {
        return ShowYesNoAndError(message, null);
    }
    /// <summary>
    /// 显示询问用户信息，并显示错误标志
    /// </summary>
    /// <param name="message">错误信息</param>
    /// <param name="args">字符串里面的参数内容</param>
    public static DialogResult ShowYesNoAndError(this string message, params object[] args)
    {
        message = JsonLanguage.Default.GetString(message);
        if (args != null)
        {
            message = string.Format(message, args);
        }
        return MessageBox.Show(message, _captionError, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
    }

    /// <summary>
    /// 显示询问用户信息，并显示提示标志
    /// </summary>
    /// <param name="message">错误信息</param>
    public static DialogResult ShowYesNoAndTips(this string message)
    {
        return ShowYesNoAndTips(message, null);
    }

    /// <summary>
    /// 显示询问用户信息，并显示提示标志
    /// </summary>
    /// <param name="message">错误信息</param>
    /// <param name="args">字符串里面的参数内容</param>
    public static DialogResult ShowYesNoAndTips(this string message, params object[] args)
    {
        message = JsonLanguage.Default.GetString(message);
        if (args != null)
        {
            message = string.Format(message, args);
        }
        return MessageBox.Show(message, _captionTips, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
    }

    /// <summary>
    /// 显示询问用户信息，并显示警告标志
    /// </summary>
    /// <param name="message">警告信息</param>
    public static DialogResult ShowYesNoAndWarning(this string message)
    {
        return ShowYesNoAndWarning(message, null);
    }

    /// <summary>
    /// 显示询问用户信息，并显示警告标志
    /// </summary>
    /// <param name="message">警告信息</param>
    /// <param name="args">字符串里面的参数内容</param>
    public static DialogResult ShowYesNoAndWarning(this string message, params object[] args)
    {
        message = JsonLanguage.Default.GetString(message);
        if (args != null)
        {
            message = string.Format(message, args);
        }
        return MessageBox.Show(message, _captionWarning,  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
    }

    /// <summary>
    /// 显示询问用户信息，并显示提示标志
    /// </summary>
    /// <param name="message">错误信息</param>
    public static DialogResult ShowYesNoCancelAndTips(this string message)
    {
        return ShowYesNoCancelAndTips(message, null);
    }

    /// <summary>
    /// 显示询问用户信息，并显示提示标志
    /// </summary>
    /// <param name="message">错误信息</param>
    /// <param name="args">字符串里面的参数内容</param>
    public static DialogResult ShowYesNoCancelAndTips(this string message, params object[] args)
    {
        message = JsonLanguage.Default.GetString(message);
        if (args != null)
        {
            message = string.Format(message, args);
        }
        return MessageBox.Show(message, _captionTips, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
    }

    /// <summary>
    /// 显示一个YesNo选择对话框
    /// </summary>
    /// <param name="message">对话框的选择内容提示信息</param>
    /// <returns>如果选择Yes则返回true，否则返回false</returns>
    public static bool ConfirmYesNo(this string message)
    {
        message = JsonLanguage.Default.GetString(message);
        return MessageBox.Show(message, _captionTips, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }

    /// <summary>
    /// 显示一个YesNoCancel选择对话框
    /// </summary>
    /// <param name="message">对话框的选择内容提示信息</param>
    /// <returns>返回选择结果的的DialogResult值</returns>
    public static DialogResult ConfirmYesNoCancel(this string message)
    {
        return MessageBox.Show(message, _captionTips, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
    }

    /// <summary>
    /// 询问一个输入字符串
    /// </summary>
    /// <param name="prompt">提示信息</param>
    /// <param name="initValue">初始值</param>
    /// <param name="isPassword">是否密码字符串</param>
    /// <returns>询问到的字符串</returns>
    public static string QueryInputStr(this string prompt, string initValue = "", bool isPassword = false)
    {
        prompt = JsonLanguage.Default.GetString(prompt);

        QueryInputDialog dlg = new QueryInputDialog();
        dlg.Text = prompt;
        dlg.lblPrompt.Text = prompt.EndsWith(":") || prompt.EndsWith("：") ? prompt : prompt + ":";
        dlg.txtInput.Text = initValue;
        dlg.IsEncryptInput = isPassword;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            return dlg.txtInput.Text;
        }
        return initValue;
    }
}