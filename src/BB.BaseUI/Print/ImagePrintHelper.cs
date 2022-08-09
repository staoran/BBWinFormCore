using System.Drawing.Printing;

namespace BB.BaseUI.Print;

/// <summary>
/// 图片打印辅助类
/// </summary>
public class ImagePrintHelper
{
    private System.Drawing.Image _image;
    private PrintDocument _printDocument = new();
    private PrintDialog _printDialog = new();
    private CoolPrintPreviewDialog _previewDialog = new();

    #region 配置参数
    /// <summary>
    /// 将在页面上居中打印输出。
    /// </summary>
    public bool AllowPrintCenter = true;

    /// <summary>
    /// 旋转图像，如果它符合页面更好
    /// </summary>
    public bool AllowPrintRotate = true;
    /// <summary>
    /// 缩放图像，以更好地适应页面
    /// </summary>
    public bool AllowPrintEnlarge = true;
    /// <summary>
    /// 允许打印收缩，以更好适应页面
    /// </summary>
    public bool AllowPrintShrink = true;
    #endregion

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="image">待打印的图片对象</param>
    public ImagePrintHelper(System.Drawing.Image image) : this(image, "test.png")
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="image">待打印的图片对象</param>
    /// <param name="documentname">文档名称</param>
    public ImagePrintHelper(System.Drawing.Image image, string documentname) 
    {
        _image = (System.Drawing.Image)image.Clone();
        _printDialog.UseEXDialog = true;
        _printDocument.DocumentName = documentname;
        _printDocument.PrintPage += GetImageForPrint;
        _printDialog.Document = _printDocument;

        _previewDialog.Document = _printDocument;
    }

    /// <summary>
    /// 显示打印对话框，确定则进行打印
    /// </summary>
    /// <returns></returns>
    public PrinterSettings PrintWithDialog()
    {
        if (_printDialog.ShowDialog() == DialogResult.OK)
        {
            _printDocument.Print();
            return _printDialog.PrinterSettings;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 在预览对话框中预览图片
    /// </summary>
    public void PrintPreview()
    {
        if (_previewDialog.ShowDialog() == DialogResult.OK)
        {
            _printDocument.Print();
        }
    }

    private void GetImageForPrint(object sender, PrintPageEventArgs e)
    {
        //PrintOptionsDialog pod = new PrintOptionsDialog();
        //pod.ShowDialog();

        ContentAlignment alignment = AllowPrintCenter ? ContentAlignment.MiddleCenter : ContentAlignment.TopLeft;

        RectangleF pageRect = e.PageSettings.PrintableArea;
        GraphicsUnit gu = GraphicsUnit.Pixel;
        RectangleF imageRect = _image.GetBounds(ref gu);
        // rotate the image if it fits the page better
        if (AllowPrintRotate)
        {
            if ((pageRect.Width > pageRect.Height && imageRect.Width < imageRect.Height) ||
                (pageRect.Width < pageRect.Height && imageRect.Width > imageRect.Height))
            {
                _image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                imageRect = _image.GetBounds(ref gu);
                if (alignment.Equals(ContentAlignment.TopLeft)) alignment = ContentAlignment.TopRight;
            }
        }
        RectangleF printRect = new RectangleF(0, 0, imageRect.Width, imageRect.Height); ;
        // scale the image to fit the page better
        if (AllowPrintEnlarge || AllowPrintShrink)
        {
            SizeF resizedRect = ScaleHelper.GetScaledSize(imageRect.Size, pageRect.Size, false);
            if ((AllowPrintShrink && resizedRect.Width < printRect.Width) ||
                AllowPrintEnlarge && resizedRect.Width > printRect.Width)
            {
                printRect.Size = resizedRect;
            }
        }

        // align the image
        printRect = ScaleHelper.GetAlignedRectangle(printRect, new RectangleF(0, 0, pageRect.Width, pageRect.Height), alignment);
        e.Graphics.DrawImage(_image, printRect, imageRect, GraphicsUnit.Pixel);
    }
}

/// <summary>
/// 提供一些辅助函数，用以缩放、对齐图片的操作。
/// </summary>
internal static class ScaleHelper
{
    /// <summary>
    /// calculates the Size an element must be resized to, in order to fit another element, keeping aspect ratio
    /// </summary>
    /// <param name="currentSize">the size of the element to be resized</param>
    /// <param name="targetSize">the target size of the element</param>
    /// <param name="crop">in case the aspect ratio of currentSize and targetSize differs: shall the scaled size fit into targetSize (i.e. that one of its dimensions is smaller - false) or vice versa (true)</param>
    /// <returns>a new SizeF object indicating the width and height the element should be scaled to</returns>
    public static SizeF GetScaledSize(SizeF currentSize, SizeF targetSize, bool crop)
    {
        float wFactor = targetSize.Width / currentSize.Width;
        float hFactor = targetSize.Height / currentSize.Height;

        float factor = crop ? Math.Max(wFactor, hFactor) : Math.Min(wFactor, hFactor);
        //System.Diagnostics.Debug.WriteLine(currentSize.Width+"..."+targetSize.Width);
        //System.Diagnostics.Debug.WriteLine(wFactor+"..."+hFactor+">>>"+factor);
        return new SizeF(currentSize.Width * factor, currentSize.Height * factor);
    }

    /// <summary>
    /// calculates the position of an element depending on the desired alignment within a RectangleF
    /// </summary>
    /// <param name="currentRect">the bounds of the element to be aligned</param>
    /// <param name="targetRect">the rectangle reference for aligment of the element</param>
    /// <param name="alignment">the System.Drawing.ContentAlignment value indicating how the element is to be aligned should the width or height differ from targetSize</param>
    /// <returns>a new RectangleF object with Location aligned aligned to targetRect</returns>
    public static RectangleF GetAlignedRectangle(RectangleF currentRect, RectangleF targetRect, ContentAlignment alignment)
    {
        RectangleF newRect = new RectangleF(targetRect.Location, currentRect.Size);
        switch (alignment)
        {
            case ContentAlignment.TopCenter:
                newRect.X = (targetRect.Width - currentRect.Width) / 2;
                break;
            case ContentAlignment.TopRight:
                newRect.X = (targetRect.Width - currentRect.Width);
                break;
            case ContentAlignment.MiddleLeft:
                newRect.Y = (targetRect.Height - currentRect.Height) / 2;
                break;
            case ContentAlignment.MiddleCenter:
                newRect.Y = (targetRect.Height - currentRect.Height) / 2;
                newRect.X = (targetRect.Width - currentRect.Width) / 2;
                break;
            case ContentAlignment.MiddleRight:
                newRect.Y = (targetRect.Height - currentRect.Height) / 2;
                newRect.X = (targetRect.Width - currentRect.Width);
                break;
            case ContentAlignment.BottomLeft:
                newRect.Y = (targetRect.Height - currentRect.Height);
                break;
            case ContentAlignment.BottomCenter:
                newRect.Y = (targetRect.Height - currentRect.Height);
                newRect.X = (targetRect.Width - currentRect.Width) / 2;
                break;
            case ContentAlignment.BottomRight:
                newRect.Y = (targetRect.Height - currentRect.Height);
                newRect.X = (targetRect.Width - currentRect.Width);
                break;
        }
        return newRect;
    }

    /// <summary>
    /// calculates the Rectangle an element must be resized an positioned to, in ordder to fit another element, keeping aspect ratio
    /// </summary>
    /// <param name="currentRect">the rectangle of the element to be resized/repositioned</param>
    /// <param name="targetRect">the target size/position of the element</param>
    /// <param name="crop">in case the aspect ratio of currentSize and targetSize differs: shall the scaled size fit into targetSize (i.e. that one of its dimensions is smaller - false) or vice versa (true)</param>
    /// <param name="alignment">the System.Drawing.ContentAlignment value indicating how the element is to be aligned should the width or height differ from targetSize</param>
    /// <returns>a new RectangleF object indicating the width and height the element should be scaled to and the position that should be applied to it for proper alignment</returns>
    public static RectangleF GetScaledRectangle(RectangleF currentRect, RectangleF targetRect, bool crop, ContentAlignment alignment)
    {
        SizeF newSize = GetScaledSize(currentRect.Size, targetRect.Size, crop);
        RectangleF newRect = new RectangleF(new Point(0, 0), newSize);
        return GetAlignedRectangle(newRect, targetRect, alignment);
    }
}