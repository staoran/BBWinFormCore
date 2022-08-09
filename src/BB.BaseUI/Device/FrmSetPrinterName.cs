using System.Drawing.Printing;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.Tools.File;

namespace BB.BaseUI.Device;

/// <summary>
/// 设置打印机名称的窗体
/// </summary>
public partial class FrmSetPrinterName : BaseForm
{
    /// <summary>
    /// 默认的打印机名称
    /// </summary>
    public string DefaultPrinterName { get; set; }

    /// <summary>
    /// 保存的配置名称节点
    /// </summary>
    public string SaveConfigName { get; set; }

    private AppConfig _config = new AppConfig();//配置文件操作类

    public FrmSetPrinterName()
    {
        InitializeComponent();
    }

    private void FrmSetPrinterName_Load(object sender, EventArgs e)
    {
        if (!DesignMode)
        {
            ListAllPrinterName();
        }
    }

    /// <summary>
    /// 在列表框中列出所有的打印机
    /// </summary>
    private void ListAllPrinterName()
    {
        txtPrinter.Properties.BeginUpdate();
        txtPrinter.Properties.Items.Clear();
        foreach (String strPrinter in PrinterSettings.InstalledPrinters)
        {
            txtPrinter.Properties.Items.Add(strPrinter);
        }
        txtPrinter.Properties.EndUpdate();

        //设置选定的默认打印机
        txtPrinter.Text = DefaultPrinterName;
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
        if (txtPrinter.Text.Length == 0)
        {
            "请选择或输入打印机名称".ShowUxTips();
            txtPrinter.Focus();
            return;
        }

        //保存配置并返回
        try
        {
            _config.AppConfigSet(SaveConfigName, txtPrinter.Text);

            DefaultPrinterName = txtPrinter.Text;
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            ex.Message.ShowUxTips();
        }
    }

}