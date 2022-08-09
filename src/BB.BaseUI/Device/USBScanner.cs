using Timer = System.Windows.Forms.Timer;

namespace BB.BaseUI.Device;

/// <summary>
/// USB条码扫描器的封装类
/// </summary>
public class UsbScanner
{
    private System.Windows.Forms.Control _hostCtrl;
    private string _scanCode;
    private Timer _timer;

    /// <summary>
    /// 扫描器读到一个扫描码的事件
    /// </summary>
    public event ScannerReadEventHandler ScannerRead;

    /// <summary>
    /// 默认扫描器（挂在主窗体上，会被主窗体初始化，在模块里用肯定是安全的）
    /// </summary>
    public static UsbScanner Default { get; set; }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="hostCtrl">接受键盘事件的宿主控件</param>
    public UsbScanner(System.Windows.Forms.Control hostCtrl)
    {
        _hostCtrl = hostCtrl;
        if (_hostCtrl is Form)
        {
            (_hostCtrl as Form).KeyPreview = true;
        }
        _hostCtrl.KeyUp += hostCtrl_KeyUp;
        _scanCode = "";
        _timer = new Timer();
        _timer.Interval = 20;
        _timer.Tick += timer_Tick;
        _timer.Start();
    }

    /// <summary>
    /// 定时器到期的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void timer_Tick(object sender, EventArgs e)
    {
        //约定条码最少5位
        if (_scanCode.Length >= 5)
        {
            _scanCode = _scanCode.Trim((char)13);
            //由于读卡器和扫描器原理一样，都是模拟USB键盘，所以这里可能收到的是读卡器的键盘消息
            //读卡器的卡号规律比较明显，所以很容易刨除
            if (!CardReader.IsCardCode(_scanCode))
            {
                _timer.Stop();
                OnScannerRead(_scanCode);
            }
        }
        _scanCode = "";
        _timer.Start();
    }

    /// <summary>
    /// 监听按键弹起的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void hostCtrl_KeyUp(object sender, KeyEventArgs e)
    {
        _timer.Stop();
        _scanCode = _scanCode + (char)e.KeyValue;
        _timer.Start();
    }

    private void OnScannerRead(string scanCode)
    {
        if (ScannerRead != null)
        {
            ScannerRead(scanCode);
        }
    }

}

/// <summary>
/// 扫描器读到一个条码的事件处理委托
/// </summary>
/// <param name="scanCode"></param>
public delegate void ScannerReadEventHandler(string scanCode);