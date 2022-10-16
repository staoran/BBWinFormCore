using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Interfaces;

namespace BB.BaseUI.LocalReport;

/// <summary>
/// 本地报表文件无界面打印类
/// </summary>
public class LocalReportPrinter
{
    /// <summary>
    /// 打印完成事件
    /// </summary>
    public event PrintCompleteHandler PrintComplete;

    private FileManagerWrapper _fileManagerWrapper;
    private Microsoft.Reporting.WinForms.LocalReport _report;
    private ProcessingThread _processingThread;

    private PrinterSettings _printerSettings;
    internal PrinterSettings PrinterSettings
    {
        get
        {
            if (!_printerSettings.IsValid)
            {
                _printerSettings = new PrinterSettings();
            }
            return _printerSettings;
        }
        set => _printerSettings = value;
    }

    private PageSettings _pageSettings;
    internal PageSettings PageSettings
    {
        get
        {
            PageSettings pageSettings = _pageSettings;
            if (pageSettings == null)
            {
                ReportPageSettings reportPageSettings = _report.GetDefaultPageSettings();
                Func<PrinterSettings, PageSettings> func = (Func<PrinterSettings, PageSettings>)Delegate.CreateDelegate(
                    typeof(Func<PrinterSettings, PageSettings>), reportPageSettings, "ToPageSettings");
                pageSettings = func(PrinterSettings);
                _pageSettings = pageSettings;
            }
            return pageSettings;
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="report"></param>
    internal LocalReportPrinter(Microsoft.Reporting.WinForms.LocalReport report)
    {
        _report = report;
        _fileManagerWrapper = new FileManagerWrapper();
        _processingThread = new ProcessingThread();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="reportName"></param>
    /// <param name="dataSourceDict"></param>
    /// <param name="parameters"></param>
    public LocalReportPrinter(string reportName, Dictionary<string, object> dataSourceDict, NameValueCollection parameters)
    {
        Microsoft.Reporting.WinForms.LocalReport report = new Microsoft.Reporting.WinForms.LocalReport();
        ReportHelper.LoadReport(report, reportName, dataSourceDict, parameters);

        _report = report;
        _fileManagerWrapper = new FileManagerWrapper();
        _processingThread = new ProcessingThread();
    }

    private static Func<int, string> _toInchesDelegate;
    private static Type _reportPrintDocumentType;
    private static Type _asyncAllStreamsRenderingOperationType;
    private static Type _postRenderArgsType;

    /// <summary>
    /// 静态构造函数
    /// </summary>
    static LocalReportPrinter()
    {
        Assembly asm = typeof(Microsoft.Reporting.WinForms.LocalReport).Assembly;
        _reportPrintDocumentType = asm.GetType("Microsoft.Reporting.WinForms.ReportPrintDocument");
        _asyncAllStreamsRenderingOperationType = asm.GetType("Microsoft.Reporting.WinForms.AsyncAllStreamsRenderingOperation");
        _postRenderArgsType = asm.GetType("Microsoft.Reporting.WinForms.PostRenderArgs");

        _toInchesDelegate = (Func<int, string>)Delegate.CreateDelegate(typeof(Func<int, string>), typeof(ReportViewer), "ToInches");
    }

    private void OnPrintComplete(FileManagerStatus status)
    {
        if (PrintComplete != null)
        {
            PrintComplete(this, new PrintCompletedEventArgs(status));
        }
    }

    /// <summary>
    /// 创建默认的打印配置信息
    /// </summary>
    /// <returns></returns>
    private PrinterSettings CreateDefaultPrintSettings()
    {
        PrinterSettings settings = new PrinterSettings
        {
            PrintRange = PrintRange.AllPages,
            MinimumPage = 1,
            FromPage = 1
        };

        FileManagerWrapper fileManager = _fileManagerWrapper;
        if (fileManager.Status == FileManagerStatus.Complete)
        {
            settings.MaximumPage = fileManager.Count;
            settings.ToPage = fileManager.Count;
            return settings;
        }
        settings.ToPage = 1;
        return settings;
    }

    /// <summary>
    /// 取消打印
    /// </summary>
    /// <param name="millisecondsTimeout"></param>
    /// <returns></returns>
    public bool CancelPrint(int millisecondsTimeout)
    {
        return _processingThread.Cancel(millisecondsTimeout);
    }

    /// <summary>
    /// 打印
    /// </summary>
    public void Print()
    {
        Print(CreateDefaultPrintSettings());
    }

    /// <summary>
    /// 打印
    /// </summary>
    /// <param name="printerSettings"></param>
    public void Print(PrinterSettings printerSettings)
    {
        using (PrintDialog dialog = new PrintDialog())
        {
            dialog.PrinterSettings = printerSettings;
            dialog.AllowSelection = false;
            dialog.AllowSomePages = true;
            dialog.UseEXDialog = true;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            printerSettings = dialog.PrinterSettings;
            PrinterSettings = printerSettings;

            string displayNameForUse = GetReportNameForFile();
            FileManagerWrapper fileManagerWrapper = _fileManagerWrapper;
            if (fileManagerWrapper.Status == FileManagerStatus.Aborted || fileManagerWrapper.Status == FileManagerStatus.NotStarted)
            {
                int num;
                int toPage;
                bool printAllPages = dialog.PrinterSettings.PrintRange == PrintRange.AllPages;
                if (!printAllPages)
                {
                    num = 1;
                    toPage = dialog.PrinterSettings.ToPage;
                }
                else
                {
                    num = 0;
                    toPage = 0;
                }
                string deviceInfo = CreateEmfDeviceInfo(num, toPage);
                BeginAsyncRender("IMAGE",
                    true,
                    deviceInfo,
                    PageCountMode.Estimate,
                    CreateStreamEMFPrintOnly,
                    OnRenderingCompletePrintOnly,
                    !printAllPages);
            }

            PrintDocument document = (PrintDocument)Activator.CreateInstance(
                _reportPrintDocumentType,
                BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance,
                null,
                new[] { fileManagerWrapper.FileManager, PageSettings.Clone() },
                null);
            document.DocumentName = displayNameForUse;
            document.PrinterSettings = dialog.PrinterSettings;
            document.EndPrint += document_EndPrint;
            document.Print();
        }
    }

    void document_EndPrint(object sender, PrintEventArgs e)
    {
        OnPrintComplete(_fileManagerWrapper.Status);
    }

    /// <summary>
    /// 创建打印设备信息
    /// </summary>
    /// <param name="startPage">开始页码</param>
    /// <param name="endPage">结束页码</param>
    /// <returns></returns>
    private string CreateEmfDeviceInfo(int startPage, int endPage)
    {
        PageSettings pageSettings = PageSettings;
        int pageWidth = pageSettings.Landscape ? pageSettings.PaperSize.Height : pageSettings.PaperSize.Width;
        int pageHeight = pageSettings.Landscape ? pageSettings.PaperSize.Width : pageSettings.PaperSize.Height;
        string str = string.Format(CultureInfo.InvariantCulture,
            "<MarginTop>{0}</MarginTop>" +
            "<MarginLeft>{1}</MarginLeft>" +
            "<MarginRight>{2}</MarginRight>" +
            "<MarginBottom>{3}</MarginBottom>" +
            "<PageHeight>{4}</PageHeight>" +
            "<PageWidth>{5}</PageWidth>",
            _toInchesDelegate(pageSettings.Margins.Top),
            _toInchesDelegate(pageSettings.Margins.Left),
            _toInchesDelegate(pageSettings.Margins.Right),
            _toInchesDelegate(pageSettings.Margins.Bottom),
            _toInchesDelegate(pageHeight),
            _toInchesDelegate(pageWidth));

        return string.Format(
            CultureInfo.InvariantCulture,
            "<DeviceInfo>" +
            "<OutputFormat>emf</OutputFormat>" +
            "<StartPage>{0}</StartPage>" +
            "<EndPage>{1}</EndPage>" +
            "{2}" +
            "</DeviceInfo>", startPage, endPage, str);
    }

    /// <summary>
    /// 获取打印时显示的名称
    /// </summary>
    /// <returns></returns>
    private string GetReportNameForFile()
    {
        Microsoft.Reporting.WinForms.LocalReport report = _report;
        if (string.IsNullOrEmpty(report.DisplayName))
        {
            return Path.GetFileNameWithoutExtension(report.ReportPath);
        }
        return report.DisplayName;
    }

    /// <summary>
    /// 开始执行异步打印
    /// </summary>
    /// <param name="format"></param>
    /// <param name="allowInternalRenderers"></param>
    /// <param name="deviceInfo"></param>
    /// <param name="pageCountMode"></param>
    /// <param name="createStreamCallback"></param>
    /// <param name="onCompleteCallback"></param>
    /// <param name="isPartialRendering"></param>
    private void BeginAsyncRender(string format,
        bool allowInternalRenderers,
        string deviceInfo,
        PageCountMode pageCountMode,
        CreateAndRegisterStream createStreamCallback,
        AsyncCompletedEventHandler onCompleteCallback,
        bool isPartialRendering)
    {
        object postRenderArgs = Activator.CreateInstance(_postRenderArgsType, false, isPartialRendering);

        object operation = Activator.CreateInstance(
            _asyncAllStreamsRenderingOperationType,
            _report, pageCountMode, format, deviceInfo,
            allowInternalRenderers,
            postRenderArgs,
            createStreamCallback);

        EventInfo eventInfo = _asyncAllStreamsRenderingOperationType.GetEvent("Completed");
        eventInfo.AddEventHandler(operation, onCompleteCallback);
        _processingThread.BeginBackgroundOperation(operation);
    }

    /// <summary>
    /// 创建打印页面的内容流
    /// </summary>
    /// <param name="name"></param>
    /// <param name="extension"></param>
    /// <param name="encoding"></param>
    /// <param name="mimeType"></param>
    /// <param name="useChunking"></param>
    /// <param name="operation"></param>
    /// <returns></returns>
    private Stream CreateStreamEMFPrintOnly(string name, string extension, Encoding encoding, string mimeType, bool useChunking, StreamOper operation)
    {
        return _fileManagerWrapper.CreatePage(operation == StreamOper.CreateAndRegister);
    }

    /// <summary>
    /// 打印完成后事件处理函数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void OnRenderingCompletePrintOnly(object? sender, AsyncCompletedEventArgs args)
    {
        PropertyInfo pi = _asyncAllStreamsRenderingOperationType.GetProperty("PostRenderArgs");
        object postRenderArgs = pi.GetValue(sender, null);

        pi = _postRenderArgsType.GetProperty("IsPartialRendering");
        bool isPartialRendering = (bool)pi.GetValue(postRenderArgs, null);

        if (isPartialRendering || args.Error != null)
        {
            _fileManagerWrapper.Status = FileManagerStatus.Aborted;
        }
        else
        {
            _fileManagerWrapper.Status = FileManagerStatus.Complete;
        }
    }
}
/// <summary>
/// 但因完成委托处理
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
public delegate void PrintCompleteHandler(object sender, PrintCompletedEventArgs e);

/// <summary>
/// 打印完成参数对象
/// </summary>
public class PrintCompletedEventArgs : EventArgs
{
    private FileManagerStatus _status;
    public FileManagerStatus Status => _status;

    internal PrintCompletedEventArgs(FileManagerStatus status)
    {
        _status = status;
    }
}