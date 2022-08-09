using BB.Tools.File;

namespace BB.BaseUI.Print;

/// <summary>
/// Winform窗口打印辅助类
/// </summary>
public class PrintFormHelper
{
    /// <summary>
    /// 弹出打印窗体的预览对话框
    /// </summary>
    /// <param name="form">窗体对象</param>
    /// <param name="allowPrintRotate">旋转图像，如果它符合页面更好</param>
    public static void Print(Form form, bool allowPrintRotate = true)
    {
        ScreenCapture capture = new ScreenCapture();
        System.Drawing.Image image = capture.CaptureWindow(form.Handle);

        ImagePrintHelper helper = new ImagePrintHelper(image)
        {
            AllowPrintRotate = allowPrintRotate
        };
        helper.PrintPreview();
    }

    /// <summary>
    /// 打印窗体控件
    /// </summary>
    /// <param name="control">控件对象</param>
    /// <param name="allowPrintRotate">旋转图像，如果它符合页面更好</param>
    public static void Print(System.Windows.Forms.Control control, bool allowPrintRotate = true)
    {
        ScreenCapture capture = new ScreenCapture();
        System.Drawing.Image image = capture.CaptureWindow(control.Handle);

        ImagePrintHelper helper = new ImagePrintHelper(image)
        {
            AllowPrintRotate = allowPrintRotate
        };
        helper.PrintPreview();
    }
}