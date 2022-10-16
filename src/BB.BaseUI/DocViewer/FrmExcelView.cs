using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.WinForm;
using DevExpress.Spreadsheet;
using Furion.Logging.Extensions;

namespace BB.BaseUI.DocViewer;

/// <summary>
/// Excel控件的测试例子
/// </summary>
public partial class FrmExcelView : BaseForm
{    
    /// <summary>
    /// 加载流数据
    /// </summary>
    public Stream Stream { get; set; }

    /// <summary>
    /// 文件后缀名，如.xls
    /// </summary>
    public string Extension { get; set; }

    /// <summary>
    /// 文档文件路径。如果指定了该属性，可以不用设置Stream和Extension属性。
    /// </summary>
    public string FilePath { get; set; }

    //记录窗体的名称
    readonly string _mainFormText;

    public FrmExcelView()
    {
        InitializeComponent();

        //记录窗体的名称，并实现文档变化事件的处理，方便显示新的文件名称
        _mainFormText = Text;
        spreadsheetControl1.DocumentLoaded += spreadsheetControl1_DocumentLoaded;
    }

    /// <summary>
    /// 文档变化后，实现对新文件名称的显示
    /// </summary>
    void spreadsheetControl1_DocumentLoaded(object? sender, EventArgs e)
    {
        string fileName = Path.GetFileName(spreadsheetControl1.Document.Path);
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
    /// 打开Excel文件
    /// </summary>
    private void btnOpenFile_Click(object? sender, EventArgs e)
    { 
        string filePath = FileDialogHelper.OpenExcel();
        if (!string.IsNullOrEmpty(filePath))
        {
            IWorkbook workbook = spreadsheetControl1.Document;
            workbook.LoadDocument(filePath);
        }
    }

    /// <summary>
    /// 保存Excel文件
    /// </summary>
    private void btnSaveFile_Click(object? sender, EventArgs e)
    {
        spreadsheetControl1.SaveDocument();
    }

    /// <summary>
    /// 另存为Excel文件
    /// </summary>
    private void btnSaveAs_Click(object? sender, EventArgs e)
    {
        string dir = Environment.CurrentDirectory;
        string filePath = FileDialogHelper.SaveExcel("", dir);
        if (!string.IsNullOrEmpty(filePath))
        {
            try
            {
                IWorkbook workbook = spreadsheetControl1.Document;
                workbook.SaveDocument(filePath);

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
    /// Excel文件打印
    /// </summary>
    private void btnPreview_Click(object? sender, EventArgs e)
    {
        Close();
        spreadsheetControl1.ShowPrintPreview();
    }

    private void FrmExcelView_Load(object? sender, EventArgs e)
    {
        //如果文件流不为空，首先根据Stream对象加载文档，否则根据文件路径进行加载
        if (!DesignMode)
        {
            if (Stream != null)
            {
                #region MyRegion
                try
                {
                    if (!string.IsNullOrEmpty(Extension))
                    {
                        if (Extension.Equals(".xls", StringComparison.OrdinalIgnoreCase))
                        {
                            spreadsheetControl1.LoadDocument(Stream, DocumentFormat.Xls);
                        }
                        else if (Extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                        {
                            spreadsheetControl1.LoadDocument(Stream, DocumentFormat.Xlsx);
                        }
                        else if (Extension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
                        {
                            spreadsheetControl1.LoadDocument(Stream, DocumentFormat.Csv);
                        }
                        else
                        {
                            spreadsheetControl1.LoadDocument(Stream, DocumentFormat.Xls);
                        }
                    }
                    else
                    {
                        spreadsheetControl1.LoadDocument(Stream, DocumentFormat.Xls);
                    }
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
                spreadsheetControl1.LoadDocument(FilePath);
            }
        }
    }

    private void FrmExcelView_KeyUp(object? sender, KeyEventArgs e)
    {
        if(e.KeyCode == Keys.Escape)
        {
            Close();
        }
    }
}