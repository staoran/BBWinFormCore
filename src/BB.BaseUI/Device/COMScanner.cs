using System.IO.Ports;

namespace BB.BaseUI.Device;

/// <summary>
/// 串口条码扫描器封装类
/// </summary>
public class ComScanner
{
    private SerialPort _port;

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="portName">串口号，比如"COM1"</param>
    public ComScanner(string portName)
    {
        _port = new SerialPort();
        _port.PortName = portName;
        _port.BaudRate = 9600;
        _port.DataBits = 8;
        _port.Parity = Parity.None;
        _port.StopBits = StopBits.One;
        _port.DataReceived += _port_DataReceived;
        _port.Open();
    }

    void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        //小睡一会，因为扫描器发串口在字节之间有一些延迟
        Thread.Sleep(10);
        //读出串口中的字节
        byte[] data = new byte[_port.BytesToRead];
        _port.Read(data, 0, _port.BytesToRead);
        if (ScannerRead != null)
        {
            //转化为字符串
            string scanCode = "";
            foreach (byte b in data)
            {
                scanCode = scanCode + (char)b;
            }
            //截掉首位回车
            scanCode = scanCode.Trim((char)13);
            //截掉第一个字符
            scanCode = scanCode.Substring(1);
            //发送事件
            ScannerRead(scanCode);
        }
    }
    /// <summary>
    /// 扫描器读到一个扫描码的事件
    /// </summary>
    public event ScannerReadEventHandler ScannerRead;

}