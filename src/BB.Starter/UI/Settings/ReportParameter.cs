using System.ComponentModel;

namespace BB.Starter.UI.Settings;

/// <summary>
/// 报表设置
/// </summary>
public class ReportParameter
{
    /// <summary>
    /// 派车单报表文件
    /// </summary>
    [DefaultValue("BB.CarDispatch.CarSendBill2.rdlc")]
    public string? CarSendReportFile { get; set; }
}