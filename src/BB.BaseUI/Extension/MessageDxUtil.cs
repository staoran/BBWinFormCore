using BB.BaseUI.Other;
using BB.Tools.MultiLanuage;

namespace BB.BaseUI.Extension;

/// <summary>
/// 框架统一的对话框提示辅助类
/// </summary>
public static class MessageDxUtil
{
	private static readonly string CaptionTips = "提示信息";
	private static readonly string CaptionWarning = "警告信息";
	private static readonly string CaptionError = "错误信息";

	static MessageDxUtil()
	{
		CaptionTips = JsonLanguage.Default.GetString(CaptionTips);
		CaptionWarning = JsonLanguage.Default.GetString(CaptionWarning);
		CaptionError = JsonLanguage.Default.GetString(CaptionError);
	}

	/// <summary>
	/// 显示一般的提示信息
	/// </summary>
	/// <param name="message">提示信息</param>
	public static DialogResult ShowUxTips(this string message)
	{
		return ShowUxTips(message, null);
	}

	/// <summary>
	/// 显示一般的提示信息
	/// </summary>
	/// <param name="message">提示信息</param>
	/// <param name="args">字符串里面的参数内容</param>
	/// <returns></returns>
	public static DialogResult ShowUxTips(this string message, params object[] args)
	{
		//对消息的内容进行多语言处理
		message = JsonLanguage.Default.GetString(message);
		if (args != null)
		{
			message = string.Format(message, args);
		}
		return DevExpress.XtraEditors.XtraMessageBox.Show(message, CaptionTips, MessageBoxButtons.OK, MessageBoxIcon.Information);
	}

	/// <summary>
	/// 显示警告信息
	/// </summary>
	/// <param name="message">警告信息</param>
	public static DialogResult ShowUxWarning(this string message)
	{
		return ShowUxWarning(message, null);
	}

	/// <summary>
	/// 显示警告信息
	/// </summary>
	/// <param name="message">警告信息</param>
	/// <param name="args">字符串里面的参数内容</param>
	public static DialogResult ShowUxWarning(this string message, params object[] args)
	{
		message = JsonLanguage.Default.GetString(message);
		if (args != null)
		{
			message = string.Format(message, args);
		} 
		return DevExpress.XtraEditors.XtraMessageBox.Show(message, CaptionWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
	}

	/// <summary>
	/// 显示错误信息
	/// </summary>
	/// <param name="message">错误信息</param>
	public static DialogResult ShowUxError(this string message)
	{
		return ShowUxError(message, null);
	}
	/// <summary>
	/// 显示错误信息
	/// </summary>
	/// <param name="message">错误信息</param>
	/// <param name="args">字符串里面的参数内容</param>
	public static DialogResult ShowUxError(this string message, params object[] args)
	{
		message = JsonLanguage.Default.GetString(message);
		if (args != null)
		{
			message = string.Format(message, args);
		}
		return DevExpress.XtraEditors.XtraMessageBox.Show(message, CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
	}

	/// <summary>
	/// 显示询问用户信息，并显示错误标志
	/// </summary>
	/// <param name="message">错误信息</param>
	public static DialogResult ShowYesNoAndUxError(this string message)
	{
		return ShowYesNoAndUxError(message, null);
	}
	/// <summary>
	/// 显示询问用户信息，并显示错误标志
	/// </summary>
	/// <param name="message">错误信息</param>
	/// <param name="args">字符串里面的参数内容</param>
	public static DialogResult ShowYesNoAndUxError(this string message, params object[] args)
	{
		message = JsonLanguage.Default.GetString(message);
		if (args != null)
		{
			message = string.Format(message, args);
		}
		return DevExpress.XtraEditors.XtraMessageBox.Show(message, CaptionError, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
	}

	/// <summary>
	/// 显示询问用户信息，并显示提示标志
	/// </summary>
	/// <param name="message">错误信息</param>
	public static DialogResult ShowYesNoAndUxTips(this string message)
	{
		return ShowYesNoAndUxTips(message, null);
	}

	/// <summary>
	/// 显示询问用户信息，并显示提示标志
	/// </summary>
	/// <param name="message">错误信息</param>
	/// <param name="args">字符串里面的参数内容</param>
	public static DialogResult ShowYesNoAndUxTips(this string message, params object[] args)
	{
		message = JsonLanguage.Default.GetString(message);
		if (args != null)
		{
			message = string.Format(message, args);
		}
		return DevExpress.XtraEditors.XtraMessageBox.Show(message, CaptionTips, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
	}

	/// <summary>
	/// 显示询问用户信息，并显示警告标志
	/// </summary>
	/// <param name="message">警告信息</param>
	public static DialogResult ShowYesNoAndUxWarning(this string message)
	{
		return ShowYesNoAndUxWarning(message, null);
	}

	/// <summary>
	/// 显示询问用户信息，并显示警告标志
	/// </summary>
	/// <param name="message">警告信息</param>
	/// <param name="args">字符串里面的参数内容</param>
	public static DialogResult ShowYesNoAndUxWarning(this string message, params object[] args)
	{
		message = JsonLanguage.Default.GetString(message);
		if (args != null)
		{
			message = string.Format(message, args);
		}
		return DevExpress.XtraEditors.XtraMessageBox.Show(message, CaptionWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
	}

	/// <summary>
	/// 显示询问用户信息，并显示提示标志
	/// </summary>
	/// <param name="message">错误信息</param>
	public static DialogResult ShowYesNoCancelAndUxTips(this string message)
	{
		return ShowYesNoCancelAndUxTips(message, null);
	}

	/// <summary>
	/// 显示询问用户信息，并显示提示标志
	/// </summary>
	/// <param name="message">错误信息</param>
	/// <param name="args">字符串里面的参数内容</param>
	public static DialogResult ShowYesNoCancelAndUxTips(this string message, params object[] args)
	{
		message = JsonLanguage.Default.GetString(message);
		if (args != null)
		{
			message = string.Format(message, args);
		}
		return DevExpress.XtraEditors.XtraMessageBox.Show(message, CaptionTips, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
	}


	/// <summary>
	/// 询问一个输入字符串
	/// </summary>
	/// <param name="prompt">提示信息</param>
	/// <param name="initValue">初始值</param>
	/// <param name="isPassword">是否密码字符串</param>
	/// <returns>询问到的字符串</returns>
	public static string QueryInputStrByUx(this string prompt, string initValue = "", bool isPassword = false)
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