using System.Drawing.Printing;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;

namespace BB.BaseUI.Device;

/// <summary>
/// POS小票打印的打印预览管理界面
/// </summary>
public partial class FrmPosPrintPreview : BaseForm
{
    #region 属性变量
    /// <summary>
    /// 设置待打印的内容
    /// </summary>
    public string PrintString { get; set; }

    /// <summary>
    /// 指定默认的小票打印机名称，用作快速POS打印
    /// </summary>
    public string PrinterName
    {
        get => _mPrinterName;
        set 
        {
            _mPrinterName = value;
            RefreshPrintSetting();
        }
    }

    /// <summary>
    /// POS打印机的边距,默认为2
    /// </summary>
    public int PosPageMargin
    {
        get => _mPosPageMargin;
        set
        {
            _mPosPageMargin = value; 
            RefreshPrintSetting();
        }
    }

    /// <summary>
    /// POS打印机默认横向还是纵向，默认设置为纵向(false)
    /// </summary>
    public bool Landscape
    {
        get => _mLandscape;
        set 
        { 
            _mLandscape = value;
            RefreshPrintSetting();
        }
    }

    private const string SaveConfigName = "POSPrinterName";
    private string _mPrinterName = "GP-5860III";//默认的小票打印机名称
    private int _mPosPageMargin = 2;//POS打印机的边距,默认为2
    private bool _mLandscape = false;//POS打印机默认横向还是纵向，默认设置为纵向(false)
    private MultipadPrintDocument _printdocument = new MultipadPrintDocument();
    private Font _printFont = new Font("宋体", 9f); 

    #endregion

    /// <summary>
    /// 构造函数
    /// </summary>
    public FrmPosPrintPreview()
    {
        InitializeComponent();
    }

    private void FrmPosPrintPreview_Load(object? sender, EventArgs e)
    {
        if (!DesignMode)
        {
            //默认从配置里面加载
            string printer = GB.Config.AppConfigGet(SaveConfigName);
            if (!string.IsNullOrEmpty(printer))
            {
                PrinterName = printer;
            }

            RefreshPrintSetting();
        }
    }

    /// <summary>
    /// 刷新打印设置
    /// </summary>
    public void RefreshPrintSetting()
    {
        txtContent.Text = PrintString;
        _printdocument.Text = txtContent.Text;
        _printdocument.Font = _printFont;
        _printdocument.DefaultPageSettings.Landscape = Landscape;
        int posMargin = PosPageMargin;
        _printdocument.DefaultPageSettings.Margins = new Margins(posMargin, posMargin, posMargin, posMargin);
        _printdocument.PrinterSettings.PrinterName = PrinterName;
    }

    private void btnPrintSetup_Click(object? sender, EventArgs e)
    {
        try
        {
            PageSetupDialog psd = new PageSetupDialog();
            psd.Document = _printdocument;
            psd.PageSettings.Margins = PrinterUnitConvert.Convert(psd.PageSettings.Margins,
                PrinterUnit.ThousandthsOfAnInch, PrinterUnit.HundredthsOfAMillimeter);

            if (psd.ShowDialog() == DialogResult.OK)
            {
                _printdocument.Print();
            }
            else
            {
                psd.PageSettings.Margins = PrinterUnitConvert.Convert(psd.PageSettings.Margins,
                    PrinterUnit.HundredthsOfAMillimeter, PrinterUnit.ThousandthsOfAnInch);
            }
        }
        catch (Exception ex)
        {
            ex.Message.ShowUxError();
        }
    }

    private void btnPreview_Click(object? sender, EventArgs e)
    {
        try
        {
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            _printdocument.Text = txtContent.Text;
            _printdocument.Font = _printFont;
            ppd.Document = _printdocument;
            ppd.ShowDialog();
        }
        catch (Exception ex)
        {
            ex.Message.ShowUxError();
        }
    }

    private void btnPrint_Click(object? sender, EventArgs e)
    {
        try
        {
            PrintDialog pd = new PrintDialog();
            _printdocument.Text = txtContent.Text;
            _printdocument.Font = _printFont;
            pd.Document = _printdocument;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                _printdocument.Print();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ShowUxError();
        }
    }

    private void btnClose_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void btnSetPrinterName_Click(object? sender, EventArgs e)
    {
        FrmSetPrinterName dlg = new FrmSetPrinterName();
        dlg.DefaultPrinterName = PrinterName;
        dlg.SaveConfigName = SaveConfigName;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            PrinterName = dlg.DefaultPrinterName;
        }
    }
}