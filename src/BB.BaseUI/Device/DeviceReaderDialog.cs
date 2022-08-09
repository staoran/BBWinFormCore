using BB.BaseUI.BaseUI;

namespace BB.BaseUI.Device;

/// <summary>
/// 读卡器、USB条码扫描器、串口条码扫描器数据读取及显示窗体
/// </summary>
public partial class DeviceReaderDialog : BaseForm
{
    private CardReader _cardReader;
    private UsbScanner _usbScanner;
    private ComScanner _comScanner;

    /// <summary>
    /// 读卡构造函数
    /// </summary>
    /// <param name="type"></param>
    public DeviceReaderDialog(DeviceType type = DeviceType.Card)
    {
        InitializeComponent();
        //TODO: 硬件 接口没有做好之前先能手填
        Readonly = false;

        if (type == DeviceType.Card)
        {
            _cardReader = new CardReader(this);
            _cardReader.CardRead += _cardReader_CardRead;
        }
        else if (type == DeviceType.UsbScanner)
        {
            _usbScanner = new UsbScanner(this);
            _usbScanner.ScannerRead += Scanner_ScannerRead;
        }
        else if (type == DeviceType.ComScanner)
        {
            _comScanner = new ComScanner("COM3");
            _comScanner.ScannerRead += Scanner_ScannerRead;
        }
    }

    void Scanner_ScannerRead(string scanCode)
    {
        txtCode.Text = scanCode;
        DialogResult = DialogResult.OK;
    }

    void _cardReader_CardRead(string cardCode)
    {
        txtCode.Text = cardCode;
        DialogResult = DialogResult.OK;
    }

    /// <summary>
    /// 读取的代码
    /// </summary>
    public string Code => txtCode.Text;

    /// <summary>
    /// 只读
    /// </summary>
    public bool Readonly
    {
        get => txtCode.Properties.ReadOnly;
        set
        {
            txtCode.Properties.ReadOnly = value;
            btnOK.Enabled = !value;
            btnOK.Visible = !value;
        }
    }

    private void DeviceReaderDialog_Load(object sender, EventArgs e)
    {
        if (!Readonly)
        {
            KeyDown += DeviceReaderDialog_KeyDown;
        }
    }

    void DeviceReaderDialog_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            DialogResult = DialogResult.OK;
        }
    }
}

/// <summary>
/// 设备类型
/// </summary>
public enum DeviceType
{
    Card,
    UsbScanner,
    ComScanner
}