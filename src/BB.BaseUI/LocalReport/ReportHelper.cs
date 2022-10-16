using System.Collections.Specialized;
using System.Drawing.Imaging;
using Microsoft.Reporting.WinForms;

namespace BB.BaseUI.LocalReport;

/// <summary>
/// 报表打印辅助类
/// </summary>
public static class ReportHelper
{
    private readonly static string ReportDirectory = Path.Combine(Application.StartupPath, "Resource/Report");

    /// <summary>
    /// 通过报表名称加载报表
    /// </summary>
    /// <param name="report">LocalReport对象</param>
    /// <param name="reportName">报表名称（不带".rdlc"）</param>
    /// <param name="dataSourceDict">数据源集合</param>
    /// <param name="parameters">参数集合</param>
    public static void LoadReport(Microsoft.Reporting.WinForms.LocalReport report, string reportName, Dictionary<string, object> dataSourceDict, NameValueCollection? parameters)
    {
        string reportPath = Path.Combine(ReportDirectory, reportName) + ".rdlc";
        LoadReportWithPath(report, reportPath, dataSourceDict, parameters);
    }

    /// <summary>
    /// 通过报表相对路径，加载报表
    /// </summary>
    /// <param name="report">LocalReport对象</param>
    /// <param name="reportPath">报表文件相对路径（带".rdlc"后缀）</param>
    /// <param name="dataSourceDict">数据源集合</param>
    /// <param name="parameters">参数集合</param>
    public static void LoadReportWithPath(Microsoft.Reporting.WinForms.LocalReport report, string reportPath, Dictionary<string, object> dataSourceDict, NameValueCollection? parameters)
    {
        if (!Directory.Exists(reportPath))
        {
            reportPath = Path.Combine(Application.StartupPath, reportPath);
        }
        report.ReportPath = reportPath;

        report.DataSources.Clear();
        foreach (string sourceName in dataSourceDict.Keys)
        {
            report.DataSources.Add(new ReportDataSource(sourceName, dataSourceDict[sourceName]));
        }
        report.Refresh();

        if (parameters is { Count: > 0 })
        {
            foreach (string key in parameters)
            {
                report.SetParameters(new ReportParameter(key, parameters[key]));
            }
        }
    }

    /// <summary>
    /// 打印报表
    /// </summary>
    /// <param name="reportName">报表名称</param>
    /// <param name="dataSourceDict">报表数据集和数据源的映射关系</param>
    /// <param name="parameters">报表参数集合</param>
    public static void Print(string reportName, Dictionary<string, object> dataSourceDict, NameValueCollection? parameters = null)
    {
        Microsoft.Reporting.WinForms.LocalReport report = new Microsoft.Reporting.WinForms.LocalReport();
        LoadReport(report, reportName, dataSourceDict, parameters);
        LocalReportPrinter executor = new LocalReportPrinter(report);
        executor.Print();
    }

    /// <summary>
    /// 根据报表文件路径，打印报表
    /// </summary>
    /// <param name="reportPath">报表名称</param>
    /// <param name="dataSourceDict">报表数据集和数据源的映射关系</param>
    /// <param name="parameters">报表参数集合</param>
    public static void PrintWithPath(string reportPath, Dictionary<string, object> dataSourceDict, NameValueCollection? parameters = null)
    {
        Microsoft.Reporting.WinForms.LocalReport report = new Microsoft.Reporting.WinForms.LocalReport();
        LoadReportWithPath(report, reportPath, dataSourceDict, parameters);
        LocalReportPrinter executor = new LocalReportPrinter(report);
        executor.Print();
    }

    /// <summary>
    /// 转换Image对象为Base64字符串
    /// </summary>
    /// <param name="image">Image对象</param>
    /// <param name="format">图片格式</param>
    /// <returns></returns>
    public static string ConvertImageToBase64(System.Drawing.Image image, ImageFormat format)
    {
        byte[] imageArray;

        using (MemoryStream imageStream = new MemoryStream())
        {
            image.Save(imageStream, format);
            imageArray = new byte[imageStream.Length];
            imageStream.Seek(0, SeekOrigin.Begin);
            imageStream.Read(imageArray, 0, (int)imageStream.Length);
        }

        return Convert.ToBase64String(imageArray);
    }
}