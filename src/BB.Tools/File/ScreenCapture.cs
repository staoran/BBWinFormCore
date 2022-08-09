using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace BB.Tools.File;

/// <summary>
/// 抓取整个屏幕或指定窗口，并可保存到文件的操作类
/// </summary>
public class ScreenCapture
{
    private string _mImageSavePath = "C:\\CaptureImages";
    private string _mImageExtension = "";
    private ImageFormat _mImageFormat = ImageFormat.Png;//图片保存的格式

    /// <summary>
    /// 图片保存的路径，默认为C:\\CaptureImages
    /// </summary>
    public string ImageSavePath
    {
        get => _mImageSavePath;
        set => _mImageSavePath = value;
    }

    /// <summary>
    /// 图片的后缀名，如果为空，使用图片格式作为后缀名
    /// </summary>
    public string ImageExtension
    {
        get => _mImageExtension;
        set => _mImageExtension = value;
    }

    /// <summary>
    /// 图片保存的格式
    /// </summary>
    public ImageFormat ImageFormat
    {
        get => _mImageFormat;
        set => _mImageFormat = value;
    }

    /// <summary>
    /// 抓取桌面整个屏幕截图到一个图片对象中
    /// </summary>
    /// <returns></returns>
    public System.Drawing.Image CaptureScreen()
    {
        return CaptureWindow(User32.GetDesktopWindow());
    }

    /// <summary>
    /// 抓取桌面整个指定窗口的截图到一个图片对象中
    /// </summary>
    /// <param name="handle">指定窗口的句柄</param>
    /// <returns></returns>
    public System.Drawing.Image CaptureWindow(IntPtr handle)
    {
        // get te hDC of the target window
        IntPtr hdcSrc = User32.GetWindowDC(handle);
        // get the size
        User32.Rect windowRect = new User32.Rect();
        User32.GetWindowRect(handle, ref windowRect);
        int width = windowRect.right - windowRect.left;
        int height = windowRect.bottom - windowRect.top;
        // create a device context we can copy to
        IntPtr hdcDest = Gdi32.CreateCompatibleDC(hdcSrc);
        // create a bitmap we can copy it to,
        // using GetDeviceCaps to get the width/height
        IntPtr hBitmap = Gdi32.CreateCompatibleBitmap(hdcSrc, width, height);
        // select the bitmap object
        IntPtr hOld = Gdi32.SelectObject(hdcDest, hBitmap);
        // bitblt over
        Gdi32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, Gdi32.SRCCOPY);
        // restore selection
        Gdi32.SelectObject(hdcDest, hOld);
        // clean up
        Gdi32.DeleteDC(hdcDest);
        User32.ReleaseDC(handle, hdcSrc);
        // get a .NET image object for it
        System.Drawing.Image img = System.Drawing.Image.FromHbitmap(hBitmap);
        // free up the Bitmap object
        Gdi32.DeleteObject(hBitmap);
        return img;
    }

    /// <summary>
    /// 抓取桌面整个指定窗口的截图，并保存到文件中
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="filename"></param>
    /// <param name="format"></param>
    public void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
    {
        System.Drawing.Image img = CaptureWindow(handle);
        img.Save(filename, format);
    }

    /// <summary>
    /// 根据属性配置自动保存图片
    /// </summary>
    public void AutoCaptureScreen()
    {
        DirectoryUtil.AssertDirExist(ImageSavePath);
        if (DirectoryUtil.IsExistDirectory(ImageSavePath))
        {
            string subPath = Path.Combine(ImageSavePath, DateTime.Now.ToString("yyyy-MM-dd"));
            DirectoryUtil.CreateDirectory(subPath);

            DateTime snapTime = DateTime.Now;
            string baseFilename = snapTime.ToString("yyyy_MM_dd-HH_mm_ss");
            string fullFilename = Path.Combine(subPath, baseFilename);
            if (!string.IsNullOrEmpty(ImageExtension.Trim('.')))
            {
                fullFilename += "." + ImageExtension.Trim('.');
            }
            else
            {
                fullFilename += "." + ImageFormat;
            }
            CaptureScreenToFile(fullFilename, ImageFormat);
        }
    }

    /// <summary>
    /// 抓取桌面整个指定窗口的截图，并保存到文件中
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="format"></param>
    public void CaptureScreenToFile(string filename, ImageFormat format)
    {
        System.Drawing.Image img = CaptureScreen();
        img.Save(filename, format);
    }

    /// <summary>
    /// Helper class containing Gdi32 API functions
    /// </summary>
    private class Gdi32
    {
        public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hObjectSource,
            int nXSrc, int nYSrc, int dwRop);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDc, int nWidth,
            int nHeight);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDc);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDc);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDc, IntPtr hObject);
    }

    /// <summary>
    /// Helper class containing User32 API functions
    /// </summary>
    private class User32
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
    }
}