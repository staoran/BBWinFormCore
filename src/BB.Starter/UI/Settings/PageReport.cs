using BB.BaseUI.Extension;
using BB.BaseUI.Settings;
using Furion.Logging.Extensions;
using SettingsProviderNet;

namespace BB.Starter.UI.Settings;

public partial class PageReport : PropertyPage
{
    private SettingsProvider _settings;
    private ISettingsStorage _store;

    public PageReport()
    {
        InitializeComponent();

        if(!DesignMode)
        {
            // PortableStorage: 在运行程序目录创建一个setting的文件记录参数数据
            _store = new PortableStorage();
            _settings = new SettingsProvider(_store);
        }
    }

    public override void OnInit()
    {
        ReportParameter parameter = _settings.GetSettings<ReportParameter>();
        if (parameter != null)
        {
            EnableOtherReport(false);
            string? reportFile = parameter.CarSendReportFile;
            if (reportFile == "BB.CarDispatch.CarSendBill2.rdlc")
            {
                radReport.SelectedIndex = 0;                    
            }
            else if (reportFile == "BB.CarDispatch.CarSendBill.rdlc")
            {
                radReport.SelectedIndex = 1;
            }
            else
            {
                EnableOtherReport(true);
                radReport.SelectedIndex = 2;
                txtOtherReport.Text = reportFile;
            }
        }
    }

    private void EnableOtherReport(bool enable)
    {
        lblOtherReport.Enabled = enable;
        txtOtherReport.Enabled = enable;
    }

    public override void OnSetActive()
    {
    }

    public override bool OnApply()
    {
        bool result = false;
        try
        {
            ReportParameter parameter = _settings.GetSettings<ReportParameter>();
            if (parameter != null)
            {                    
                int otherType = 2;//2代表其他类型
                parameter.CarSendReportFile = radReport.SelectedIndex < otherType ? radReport.Properties.Items[radReport.SelectedIndex].Value.ToString() : txtOtherReport.Text;
                _settings.SaveSettings<ReportParameter>(parameter);
            }
            result = true;
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();;
            ex.Message.ShowUxError();
        }

        return result;
    }

    private void PageSetting_Load(object? sender, EventArgs e)
    {

    }

    private void radReport_SelectedIndexChanged(object? sender, EventArgs e)
    {
        int otherType = 2;//2代表其他类型
        bool isOtherType = (radReport.SelectedIndex == otherType);
        EnableOtherReport(isOtherType);
    }
}