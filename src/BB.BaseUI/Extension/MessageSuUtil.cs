using System.ComponentModel;
using Sunny.UI;

namespace BB.BaseUI.Extension;

/// <summary>
/// Sunny 各种提示的封装
/// </summary>
public static class MessageSuUtil
{
    /// <summary>
    /// 显示进度提示窗
    /// </summary>
    /// <param name="desc">描述文字</param>
    /// <param name="maximum">最大进度值</param>
    /// <param name="decimalCount">显示进度条小数个数</param>
    public static void ShowStatusForm(int maximum = 100, string desc = "系统正在处理中，请稍候...", int decimalCount = 1) =>
        UIStatusFormService.ShowStatusForm(maximum, desc, decimalCount);

    /// <summary>
    /// 隐藏进度提示窗
    /// </summary>
    public static void HideStatusForm() => UIStatusFormService.HideStatusForm();

    /// <summary>
    /// 设置进度提示窗步进值加1
    /// </summary>
    public static void StatusFormStepIt() => UIStatusFormService.StepIt();

    /// <summary>
    /// 设置进度提示窗描述文字
    /// </summary>
    /// <param name="desc">描述文字</param>
    public static void SetStatusFormDescription(string desc) => UIStatusFormService.SetDescription(desc);

    /// <summary>
    /// 显示等待提示窗
    /// </summary>
    /// <param name="desc">描述文字</param>
    public static void ShowWaitForm(string desc = "系统正在处理中，请稍候...") => UIWaitFormService.ShowWaitForm(desc);

    /// <summary>
    /// 隐藏等待提示窗
    /// </summary>
    public static void HideWaitForm() => UIWaitFormService.HideWaitForm();

    /// <summary>
    /// 设置等待提示窗描述文字
    /// </summary>
    /// <param name="desc">描述文字</param>
    public static void SetWaitFormDescription(string desc) => UIWaitFormService.SetDescription(desc);

    /// <summary>
    /// 正确信息提示框
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="showMask">显示遮罩层</param>
    /// <param name="topMost">是否顶层</param>
    public static void ShowSuccessDialog(this string msg, bool showMask = true, bool topMost = false) =>
        UIMessageDialog.ShowMessageDialog(msg, UILocalize.SuccessTitle, false, UIStyle.Green, showMask, topMost);

    /// <summary>
    /// 信息提示框
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="showMask">显示遮罩层</param>
    /// <param name="topMost">是否顶层</param>
    public static void ShowInfoDialog(this string msg, bool showMask = true, bool topMost = false) =>
        UIMessageDialog.ShowMessageDialog(msg, UILocalize.InfoTitle, false, UIStyle.Gray, showMask, topMost);

    /// <summary>
    /// 警告信息提示框
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="showMask">显示遮罩层</param>
    /// <param name="topMost">是否顶层</param>
    public static void ShowWarningDialog(this string msg, bool showMask = true, bool topMost = false) =>
        UIMessageDialog.ShowMessageDialog(msg, UILocalize.WarningTitle, false, UIStyle.Orange, showMask, topMost);

    /// <summary>
    /// 错误信息提示框
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="showMask">显示遮罩层</param>
    /// <param name="topMost">是否顶层</param>
    public static void ShowErrorDialog(this string msg, bool showMask = true, bool topMost = false) =>
        UIMessageDialog.ShowMessageDialog(msg, UILocalize.ErrorTitle, false, UIStyle.Red, showMask, topMost);

    /// <summary>
    /// 确认信息提示框
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="showMask">显示遮罩层</param>
    /// <param name="topMost">是否顶层</param>
    /// <returns>结果</returns>
    public static bool ShowAskDialog(this string msg, bool showMask = true, bool topMost = false) =>
        UIMessageDialog.ShowMessageDialog(msg, UILocalize.AskTitle, true, UIStyle.Blue, showMask, topMost);

    /// <summary>
    /// 正确信息提示框
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="title">标题</param>
    /// <param name="style">主题</param>
    /// <param name="showMask">显示遮罩层</param>
    public static void ShowSuccessDialog(this string msg, string title, UIStyle style = UIStyle.Green,
        bool showMask = true, bool topMost = false) =>
        UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, topMost);

    /// <summary>
    /// 信息提示框
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="title">标题</param>
    /// <param name="style">主题</param>
    /// <param name="showMask">显示遮罩层</param>
    public static void ShowInfoDialog(this string msg, string title, UIStyle style = UIStyle.Gray, bool showMask = true,
        bool topMost = false) => UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, topMost);

    /// <summary>
    /// 警告信息提示框
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="title">标题</param>
    /// <param name="style">主题</param>
    /// <param name="showMask">显示遮罩层</param>
    public static void ShowWarningDialog(this string msg, string title, UIStyle style = UIStyle.Orange,
        bool showMask = true, bool topMost = false) =>
        UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, topMost);

    /// <summary>
    /// 错误信息提示框
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="title">标题</param>
    /// <param name="style">主题</param>
    /// <param name="showMask">显示遮罩层</param>
    public static void ShowErrorDialog(this string msg, string title, UIStyle style = UIStyle.Red, bool showMask = true,
        bool topMost = false) => UIMessageDialog.ShowMessageDialog(msg, title, false, style, showMask, topMost);

    /// <summary>
    /// 确认信息提示框
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="title">标题</param>
    /// <param name="style">主题</param>
    /// <param name="showMask">显示遮罩层</param>
    /// <param name="topMost">是否顶层</param>
    /// <returns>结果</returns>
    public static bool ShowAskDialog(this string msg, string title, UIStyle style = UIStyle.Blue, bool showMask = true,
        bool topMost = false) => UIMessageDialog.ShowMessageDialog(msg, title, true, style, showMask, topMost);

    /// <summary>
    /// 显示消息
    /// </summary>
    /// <param name="text">消息文本</param>
    /// <param name="delay">消息停留时长(ms)。默认1秒</param>
    /// <param name="floating">是否漂浮</param>
    public static void ShowInfoTip(this string text, int delay = 2000, bool floating = true)
        => UIMessageTip.Show(text, null, delay, floating);

    /// <summary>
    /// 显示成功消息
    /// </summary>
    /// <param name="text">消息文本</param>
    /// <param name="delay">消息停留时长(ms)。默认1秒</param>
    /// <param name="floating">是否漂浮</param>
    public static void ShowSuccessTip(this string text, int delay = 2000, bool floating = true)
        => UIMessageTip.ShowOk(text, delay, floating);

    /// <summary>
    /// 显示警告消息
    /// </summary>
    /// <param name="text">消息文本</param>
    /// <param name="delay">消息停留时长(ms)。默认1秒</param>
    /// <param name="floating">是否漂浮</param>
    public static void ShowWarningTip(this string text, int delay = 2000, bool floating = true)
        => UIMessageTip.ShowWarning(text, delay, floating);

    /// <summary>
    /// 显示出错消息
    /// </summary>
    /// <param name="text">消息文本</param>
    /// <param name="delay">消息停留时长(ms)。默认1秒</param>
    /// <param name="floating">是否漂浮</param>
    public static void ShowErrorTip(this string text, int delay = 2000, bool floating = true)
        => UIMessageTip.ShowError(text, delay, floating);

    /// <summary>
    /// 在指定控件附近显示消息
    /// </summary>
    /// <param name="delay">消息停留时长(ms)。默认1秒</param>
    /// <param name="floating">是否漂浮</param>
    public static void ShowInfoTip(this string text, Component controlOrItem, int delay = 2000, bool floating = true)
        => UIMessageTip.Show(controlOrItem, text, null, delay, floating);

    /// <summary>
    /// 在指定控件附近显示良好消息
    /// </summary>
    /// <param name="text">消息文本</param>
    /// <param name="controlOrItem">控件或工具栏项</param>
    /// <param name="delay">消息停留时长(ms)。默认1秒</param>
    /// <param name="floating">是否漂浮</param>
    public static void ShowSuccessTip(this string text, Component controlOrItem, int delay = 2000, bool floating = true)
        => UIMessageTip.ShowOk(controlOrItem, text, delay, floating);

    /// <summary>
    /// 在指定控件附近显示出错消息
    /// </summary>
    /// <param name="text">消息文本</param>
    /// <param name="controlOrItem">控件或工具栏项</param>
    /// <param name="delay">消息停留时长(ms)。默认1秒</param>
    /// <param name="floating">是否漂浮</param>
    public static void ShowErrorTip(this string text, Component controlOrItem, int delay = 2000, bool floating = true)
        => UIMessageTip.ShowError(controlOrItem, text, delay, floating);

    /// <summary>
    /// 在指定控件附近显示警告消息
    /// </summary>
    /// <param name="text">消息文本</param>
    /// <param name="controlOrItem">控件或工具栏项</param>
    /// <param name="delay">消息停留时长(ms)。默认1秒</param>
    /// <param name="floating">是否漂浮</param>
    public static void ShowWarningTip(this string text, Component controlOrItem, int delay = 2000, bool floating = true)
        => UIMessageTip.ShowWarning(controlOrItem, text, delay, floating, false);

    /// <summary>
    /// 桌面信息通知
    /// </summary>
    /// <param name="desc">通知内容</param>
    /// <param name="isDialog">对话框形式</param>
    /// <param name="timeout">停留时间</param>
    public static void ShowInfoNotifier(this string desc, bool isDialog = false, int timeout = 2000) =>
        UINotifierHelper.ShowNotifier(desc, UINotifierType.INFO, UILocalize.InfoTitle, isDialog, timeout);

    /// <summary>
    /// 桌面成功通知
    /// </summary>
    /// <param name="desc">通知内容</param>
    /// <param name="isDialog">对话框形式</param>
    /// <param name="timeout">停留时间</param>
    public static void ShowSuccessNotifier(this string desc, bool isDialog = false, int timeout = 2000) =>
        UINotifierHelper.ShowNotifier(desc, UINotifierType.OK, UILocalize.SuccessTitle, isDialog, timeout);

    /// <summary>
    /// 桌面警告通知
    /// </summary>
    /// <param name="desc">通知内容</param>
    /// <param name="isDialog">对话框形式</param>
    /// <param name="timeout">停留时间</param>
    public static void ShowWarningNotifier(this string desc, bool isDialog = false, int timeout = 2000) =>
        UINotifierHelper.ShowNotifier(desc, UINotifierType.WARNING, UILocalize.WarningTitle, isDialog, timeout);

    /// <summary>
    /// 桌面错误通知
    /// </summary>
    /// <param name="desc">通知内容</param>
    /// <param name="isDialog">对话框形式</param>
    /// <param name="timeout">停留时间</param>
    public static void ShowErrorNotifier(this string desc, bool isDialog = false, int timeout = 2000) =>
        UINotifierHelper.ShowNotifier(desc, UINotifierType.ERROR, UILocalize.ErrorTitle, isDialog, timeout);

    /// <summary>
    /// 桌面信息通知
    /// </summary>
    /// <param name="desc">通知内容</param>
    /// <param name="clickEvent">点击事件</param>
    /// <param name="isDialog">对话框形式</param>
    /// <param name="timeout">停留时间</param>
    public static void ShowInfoNotifier(this string desc, EventHandler clickEvent, int timeout = 2000) =>
        UINotifierHelper.ShowNotifier(desc, clickEvent, UINotifierType.INFO, UILocalize.InfoTitle, timeout);

    /// <summary>
    /// 桌面成功通知
    /// </summary>
    /// <param name="desc">通知内容</param>
    /// <param name="clickEvent">点击事件</param>
    /// <param name="isDialog">对话框形式</param>
    /// <param name="timeout">停留时间</param>
    public static void ShowSuccessNotifier(this string desc, EventHandler clickEvent, int timeout = 2000) =>
        UINotifierHelper.ShowNotifier(desc, clickEvent, UINotifierType.OK, UILocalize.SuccessTitle, timeout);

    /// <summary>
    /// 桌面警告通知
    /// </summary>
    /// <param name="desc">通知内容</param>
    /// <param name="clickEvent">点击事件</param>
    /// <param name="isDialog">对话框形式</param>
    /// <param name="timeout">停留时间</param>
    public static void ShowWarningNotifier(this string desc, EventHandler clickEvent, int timeout = 2000) =>
        UINotifierHelper.ShowNotifier(desc, clickEvent, UINotifierType.WARNING, UILocalize.WarningTitle, timeout);

    /// <summary>
    /// 桌面错误通知
    /// </summary>
    /// <param name="desc">通知内容</param>
    /// <param name="clickEvent">点击事件</param>
    /// <param name="isDialog">对话框形式</param>
    /// <param name="timeout">停留时间</param>
    public static void ShowErrorNotifier(this string desc, EventHandler clickEvent, int timeout = 2000) =>
        UINotifierHelper.ShowNotifier(desc, clickEvent, UINotifierType.ERROR, UILocalize.ErrorTitle, timeout);
}