using System.IO;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.WinForm;
using Furion.Logging.Extensions;

namespace BB.BaseUI.DocViewer;

/// <summary>
/// PDF测试显示窗体
/// </summary>
public partial class FrmPdfView : BaseForm
{
    /// <summary>
    /// 加载流数据
    /// </summary>
    public Stream Stream { get; set; }

    /// <summary>
    /// 文件后缀名，如.pdf
    /// </summary>
    public string Extension { get; set; }

    /// <summary>
    /// 文档文件路径。如果指定了该属性，可以不用设置Stream和Extension属性。
    /// </summary>
    public string FilePath { get; set; }

    //记录窗体的名称
    readonly string _mainFormText;

    public FrmPdfView()
    {
        InitializeComponent();

        //记录窗体的名称，并实现文档变化事件的处理，方便显示新的文件名称
        _mainFormText = Text;
        pdfViewer1.DocumentChanged += pdfViewer1_DocumentChanged;
    }

    /// <summary>
    /// PDF文档变化后，实现对新文件名称的显示
    /// </summary>
    void pdfViewer1_DocumentChanged(object sender, DevExpress.XtraPdfViewer.PdfDocumentChangedEventArgs e)
    {
        string fileName = Path.GetFileName(e.DocumentFilePath);
        if (String.IsNullOrEmpty(fileName))
        {
            Text = _mainFormText;
        }
        else
        {
            Text = fileName + " - " + _mainFormText;
        }
    }

    /// <summary>
    /// 打开PDF文件
    /// </summary>
    private void btnOpenFile_Click(object? sender, EventArgs e)
    {
        string filePath = FileDialogHelper.OpenPdf();
        if (!string.IsNullOrEmpty(filePath))
        {
            pdfViewer1.LoadDocument(filePath);
        }
    }

    /// <summary>
    /// 另存为PDF文件
    /// </summary>
    private void btnSaveAs_Click(object? sender, EventArgs e)
    {
        string dir = Environment.CurrentDirectory;
        string filePath = FileDialogHelper.SavePdf("", dir);
        if (!string.IsNullOrEmpty(filePath))
        {
            try
            {
                pdfViewer1.SaveDocument(filePath);
                "保存成功".ShowTips();
            }
            catch (Exception ex)
            {
                ex.ToString().LogError();;
                ex.Message.ShowError();
            }
        }
    }

    /// <summary>
    /// PDF文件打印
    /// </summary>
    private void btnPreview_Click(object? sender, EventArgs e)
    {
        pdfViewer1.Print();
    }

    private void FrmPDFView_Load(object? sender, EventArgs e)
    {
        //如果文件流不为空，首先根据Stream对象加载文档，否则根据文件路径进行加载
        if (!DesignMode)
        {
            if (Stream != null)
            {
                #region MyRegion
                try
                {
                    pdfViewer1.LoadDocument(Stream);
                }
                catch (Exception ex)
                {
                    ex.ToString().LogError();;
                    ex.Message.ShowUxError();
                } 
                #endregion
            }
            else if (!string.IsNullOrEmpty(FilePath))
            {
                pdfViewer1.LoadDocument(FilePath);
            }
        }
    }

    private void FrmPDFView_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            Close();
        }
    }
}