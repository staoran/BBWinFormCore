using BB.BaseUI.BaseUI;
using BB.Tools.MultiLanuage;
using DevExpress.XtraSplashScreen;

namespace BB.BaseUI.Other;

/// <summary>
/// SplashScreen帮助类
/// </summary>
public static class SplashScreenHelper
{
    #region Fields

    private const bool FadeIn = false;
    private const bool FadeOut = true;
    private const bool ThrowExceptionIfIsAlreadyClosed = false;
    private const bool ThrowExceptionIfIsAlreadyShown = false;

    #endregion Fields

    #region Methods

    /// <summary>
    /// 关闭闪屏窗口
    /// </summary>
    public static void Close()
    {
        if (SplashScreenManager.Default != null)
        {
            SplashScreenManager.CloseForm(ThrowExceptionIfIsAlreadyClosed);
        }
    }

    /// <summary>
    /// 设置Title
    /// </summary>
    /// <param name="caption">需要设置的Title</param>
    public static void SetCaption(string caption)
    {
        if (SplashScreenManager.Default != null && !string.IsNullOrEmpty(caption))
        {
            //多语言支持
            caption = JsonLanguage.Default.GetString(caption);
            SplashScreenManager.Default.SetWaitFormCaption(caption);
        }
    }

    /// <summary>
    /// 设置文字提示信息
    /// </summary>
    /// <param name="description">需要设置的文字提示信息</param>
    public static void SetDescription(string description)
    {
        if (SplashScreenManager.Default != null && !string.IsNullOrEmpty(description))
        {
            //多语言支持
            description = JsonLanguage.Default.GetString(description);
            SplashScreenManager.Default.SetWaitFormDescription(description);
        }
    }

    /// <summary>
    /// 显示闪屏窗口
    /// </summary>
    /// <param name="type">窗口对象类型</param>
    public static void Show(Type type)
    {
        Close();
        SplashScreenManager.ShowForm(null, type, FadeIn, FadeOut, ThrowExceptionIfIsAlreadyShown);
    }

    /// <summary>
    /// 显示闪屏窗口
    /// </summary>
    /// <param name="type">窗口对象类型</param>
    public static void Show()
    {
        Close();
        SplashScreenManager.ShowForm(null, typeof(FrmWaitForm), FadeIn, FadeOut, ThrowExceptionIfIsAlreadyShown);
    }

    #endregion Methods
}