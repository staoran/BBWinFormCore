using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using BB.Tools.Format;
using Microsoft.Win32;

namespace BB.Framework.Commons.Network;

/// <summary>
/// 网络相关操作辅助类
/// </summary>
public class NetworkUtil
{
    /// <summary>
    /// 获取以太网卡的物理地址
    /// </summary>
    /// <returns></returns>
    public static string GetMacAddress2()
    {
        NetworkInterface[] aclLocalNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        string result = "";

        // 判断所有网卡的mac地址
        foreach (NetworkInterface adapter in aclLocalNetworkInterfaces)
        {
            // 网卡是以太网网卡
            if (!IsEthernet(adapter))
            {
                continue;
            }

            // 将mac地址组织为00:11:22:33:44:55的格式
            result = GetMacAddr(adapter);
        }
        return result;
    }

    // 获取mac地址
    private static string GetMacAddr(NetworkInterface adapter)
    {
        String strMacAddr = "";
        PhysicalAddress clMacAddr = adapter.GetPhysicalAddress();
        byte[] abMacAddr = clMacAddr.GetAddressBytes();

        for (int i = 0; i < abMacAddr.Length; i++)
        {
            strMacAddr = strMacAddr + abMacAddr[i].ToString("X2");

            // 在每个字节间插入冒号
            if (abMacAddr.Length - 1 != i)
            {
                strMacAddr = strMacAddr + ":";
            }
        }

        return strMacAddr;
    }

    // 网卡是以太网网卡
    private static  bool IsEthernet(NetworkInterface adapter)
    {
        if (NetworkInterfaceType.Ethernet == adapter.NetworkInterfaceType)
        {
            return true;
        }

        return false;
    }
                
    /// <summary>
    /// 获取网卡列表
    /// </summary>
    /// <returns></returns>
    public static List<KeyValuePair<string, string>> GetNetCardList()
    {
        List<KeyValuePair<string, string>> cardList = new List<KeyValuePair<string, string>>();
        try
        {
            RegistryKey regNetCards = Registry.LocalMachine.OpenSubKey(Win32Utils.REG_NET_CARDS_KEY);
            if (regNetCards != null)
            {
                string[] names = regNetCards.GetSubKeyNames();
                RegistryKey subKey = null;
                foreach (string name in names)
                {
                    subKey = regNetCards.OpenSubKey(name);
                    if (subKey != null)
                    {
                        object o = subKey.GetValue("ServiceName");
                        object description = subKey.GetValue("Description");
                        if (o != null)
                        {
                            KeyValuePair<string, string> p = new KeyValuePair<string, string>(o.ToString(), description.ToString());
                            cardList.Add(p);
                        }
                    }
                }
            }
        }
        catch
        {
            // ignored
        }

        return cardList;
    }

    /// <summary>
    /// 获取未经修改过的MAC地址（真实的特理地址）
    /// </summary>
    /// <param name="cardId">网卡ID</param>
    /// <returns></returns>
    public static string GetPhysicalAddr(string cardId)
    {
        string macAddress = string.Empty;
        uint device = 0;
        try
        {
            string driveName = "\\\\.\\" + cardId;
            device = Win32Utils.CreateFile(driveName,
                Win32Utils.GENERIC_READ | Win32Utils.GENERIC_WRITE,
                Win32Utils.FILE_SHARE_READ | Win32Utils.FILE_SHARE_WRITE,
                0, Win32Utils.OPEN_EXISTING, 0, 0);
            if (device != Win32Utils.INVALID_HANDLE_VALUE)
            {
                byte[] outBuff = new byte[6];
                uint bytRv = 0;
                int intBuff = Win32Utils.PERMANENT_ADDRESS;

                if (0 != Win32Utils.DeviceIoControl(device, Win32Utils.IOCTL_NDIS_QUERY_GLOBAL_STATS,
                    ref intBuff, 4, outBuff, 6, ref bytRv, 0))
                {
                    string temp = string.Empty;
                    foreach (byte b in outBuff)
                    {
                        temp = Convert.ToString(b, 16).PadLeft(2, '0');
                        macAddress += temp;
                        temp = string.Empty;
                    }
                }
            }
        }
        finally
        {
            if (device != 0)
            {
                Win32Utils.CloseHandle(device);
            }
        }

        return macAddress;
    }

    /// <summary>
    /// 检查设置的端口号是否正确，并返回正确的端口号,无效端口号返回-1。
    /// </summary>
    /// <param name="port">设置的端口号</param>        
    public static int GetValidPort(string port)
    {
        //声明返回的正确端口号
        int validPort = -1;

        validPort = ConvertHelper.ConvertTo<int>(port);

        //最小有效端口号
        const int minport = 0;
        //最大有效端口号
        const int maxport = 65535;

        if (validPort <= minport || validPort > maxport)
        {
            throw new ArgumentException("参数port端口号范围无效！");
        }

        return validPort;
    }

    /// <summary>
    /// 将字符串形式的IP地址转换成IPAddress对象
    /// </summary>
    /// <param name="ip">字符串形式的IP地址</param>        
    public static IPAddress StringToIPAddress(string ip)
    {
        return IPAddress.Parse(ip);
    }

    /// <summary>
    /// 检测指定的IP地址是否在两个IP段中
    /// </summary>
    /// <param name="ip">指定的IP地址</param>
    /// <param name="begip">起始ip</param>
    /// <param name="endip">结束ip</param>
    /// <returns></returns>
    public static bool IsInIp(string ip, string begip, string endip)
    {
        int[] inip, begipint, endipint = new int[4];
        inip = GetIp(ip);
        begipint = GetIp(begip);
        endipint = GetIp(endip);
        for (int i = 0; i < 4; i++)
        {
            if (inip[i] < begipint[i] || inip[i] > endipint[i])
            {
                return false;
            }
            else if (inip[i] > begipint[i] || inip[i] < endipint[i])
            {
                return true;
            }
        }
        return true;
    }

    /// <summary>
    /// 将ip地址转成整形数组
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    protected static int[] GetIp(string ip)
    {
        int[] retip = new int[4];
        int i, count;
        char c;
        for (i = 0, count = 0; i < ip.Length; i++)
        {
            c = ip[i];
            if (c != '.')
            {
                retip[count] = retip[count] * 10 + int.Parse(c.ToString());
            }
            else
            {
                count++;
            }
        }

        return retip;

    }

    /// <summary>
    /// 获取本机的计算机名
    /// </summary>
    public static string LocalHostName => Dns.GetHostName();

    /// <summary>
    /// 获取本机的局域网IP
    /// </summary>        
    public static string Lanip
    {
        get
        {
            //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
            IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            //如果本机IP列表为空，则返回空字符串
            if (addressList.Length < 1)
            {
                return "";
            }

            //返回本机的局域网IP
            return addressList[0].ToString();
        }
    }

    /// <summary>
    /// 获取本机在Internet网络的广域网IP
    /// </summary>        
    public static string Wanip
    {
        get
        {
            //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
            IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            //如果本机IP列表小于2，则返回空字符串
            if (addressList.Length < 2)
            {
                return "";
            }

            //返回本机的广域网IP
            return addressList[1].ToString();
        }
    }

    /// <summary>
    /// 获取远程客户机的IP地址
    /// </summary>
    /// <param name="clientSocket">客户端的socket对象</param>        
    public static string GetClientIp(Socket clientSocket)
    {
        IPEndPoint client = (IPEndPoint)clientSocket.RemoteEndPoint;
        return client.Address.ToString();
    }

    /// <summary>
    /// 创建一个IPEndPoint对象
    /// </summary>
    /// <param name="ip">IP地址</param>
    /// <param name="port">端口号</param>        
    public static IPEndPoint CreateIPEndPoint(string ip, int port)
    {
        IPAddress ipAddress = StringToIPAddress(ip);
        return new IPEndPoint(ipAddress, port);
    }

    /// <summary>
    /// 创建一个自动分配IP和端口的TcpListener对象
    /// </summary>        
    public static TcpListener CreateTcpListener()
    {
        //创建一个自动分配的网络节点
        IPAddress ipAddress = IPAddress.Any;
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 0);

        return new TcpListener(localEndPoint);
    }
    /// <summary>
    /// 创建一个TcpListener对象
    /// </summary>
    /// <param name="ip">IP地址</param>
    /// <param name="port">端口</param>        
    public static TcpListener CreateTcpListener(string ip, int port)
    {
        //创建一个网络节点
        IPAddress ipAddress = StringToIPAddress(ip);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

        return new TcpListener(localEndPoint);
    }

    /// <summary>
    /// 创建一个基于TCP协议的Socket对象
    /// </summary>        
    public static Socket CreateTcpSocket()
    {
        return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    /// <summary>
    /// 创建一个基于UDP协议的Socket对象
    /// </summary>        
    public static Socket CreateUdpSocket()
    {
        return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    }

    #region 获取TcpListener对象的本地终结点
    /// <summary>
    /// 获取TcpListener对象的本地终结点
    /// </summary>
    /// <param name="tcpListener">TcpListener对象</param>        
    public static IPEndPoint GetLocalPoint(TcpListener tcpListener)
    {
        return (IPEndPoint)tcpListener.LocalEndpoint;
    }

    /// <summary>
    /// 获取TcpListener对象的本地终结点的IP地址
    /// </summary>
    /// <param name="tcpListener">TcpListener对象</param>        
    public static string GetLocalPoint_IP(TcpListener tcpListener)
    {
        IPEndPoint localEndPoint = (IPEndPoint)tcpListener.LocalEndpoint;
        return localEndPoint.Address.ToString();
    }

    /// <summary>
    /// 获取TcpListener对象的本地终结点的端口号
    /// </summary>
    /// <param name="tcpListener">TcpListener对象</param>        
    public static int GetLocalPoint_Port(TcpListener tcpListener)
    {
        IPEndPoint localEndPoint = (IPEndPoint)tcpListener.LocalEndpoint;
        return localEndPoint.Port;
    }

    /// <summary>  
    /// 获取本机已被使用的网络端点  
    /// </summary>  
    public IList<IPEndPoint> GetUsedIPEndPoint()
    {
        //获取一个对象，该对象提供有关本地计算机的网络连接和通信统计数据的信息。  
        IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

        //获取有关本地计算机上的 Internet 协议版本 4 (IPV4) 传输控制协议 (TCP) 侦听器的终结点信息。  
        IPEndPoint[] ipEndPointTcp = ipGlobalProperties.GetActiveTcpListeners();

        //获取有关本地计算机上的 Internet 协议版本 4 (IPv4) 用户数据报协议 (UDP) 侦听器的信息。  
        IPEndPoint[] ipEndPointUdp = ipGlobalProperties.GetActiveUdpListeners();

        //获取有关本地计算机上的 Internet 协议版本 4 (IPV4) 传输控制协议 (TCP) 连接的信息。  
        TcpConnectionInformation[] tcpConnectionInformation = ipGlobalProperties.GetActiveTcpConnections();

        IList<IPEndPoint> allIPEndPoint = new List<IPEndPoint>();
        foreach (IPEndPoint iep in ipEndPointTcp) allIPEndPoint.Add(iep);
        foreach (IPEndPoint iep in ipEndPointUdp) allIPEndPoint.Add(iep);
        foreach (TcpConnectionInformation tci in tcpConnectionInformation) allIPEndPoint.Add(tci.LocalEndPoint);

        return allIPEndPoint;
    }

    /// <summary>  
    /// 判断指定的网络端点（只判断端口）是否被使用  
    /// </summary>  
    public bool IsUsedIPEndPoint(int port)
    {
        foreach (IPEndPoint iep in GetUsedIPEndPoint())
        {
            if (iep.Port == port)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>  
    /// 判断指定的网络端点（判断IP和端口）是否被使用  
    /// </summary>  
    public bool IsUsedIPEndPoint(string ip, int port)
    {
        foreach (IPEndPoint iep in GetUsedIPEndPoint())
        {
            if (iep.Address.ToString() == ip && iep.Port == port)
            {
                return true;
            }
        }
        return false;
    }

    #endregion

    #region 获取Socket对象的本地终结点
    /// <summary>
    /// 获取Socket对象的本地终结点
    /// </summary>
    /// <param name="socket">Socket对象</param>        
    public static IPEndPoint GetLocalPoint(Socket socket)
    {
        return (IPEndPoint)socket.LocalEndPoint;
    }

    /// <summary>
    /// 获取Socket对象的本地终结点的IP地址
    /// </summary>
    /// <param name="socket">Socket对象</param>        
    public static string GetLocalPoint_IP(Socket socket)
    {
        IPEndPoint localEndPoint = (IPEndPoint)socket.LocalEndPoint;
        return localEndPoint.Address.ToString();
    }

    /// <summary>
    /// 获取Socket对象的本地终结点的端口号
    /// </summary>
    /// <param name="socket">Socket对象</param>        
    public static int GetLocalPoint_Port(Socket socket)
    {
        IPEndPoint localEndPoint = (IPEndPoint)socket.LocalEndPoint;
        return localEndPoint.Port;
    }
    #endregion

    /// <summary>
    /// 绑定终结点
    /// </summary>
    /// <param name="socket">Socket对象</param>
    /// <param name="endPoint">要绑定的终结点</param>
    public static void BindEndPoint(Socket socket, IPEndPoint endPoint)
    {
        if (!socket.IsBound)
        {
            socket.Bind(endPoint);
        }
    }

    /// <summary>
    /// 绑定终结点
    /// </summary>
    /// <param name="socket">Socket对象</param>        
    /// <param name="ip">服务器IP地址</param>
    /// <param name="port">服务器端口</param>
    public static void BindEndPoint(Socket socket, string ip, int port)
    {
        //创建终结点
        IPEndPoint endPoint = CreateIPEndPoint(ip, port);

        //绑定终结点
        if (!socket.IsBound)
        {
            socket.Bind(endPoint);
        }
    }

    /// <summary>
    /// 指定Socket对象执行监听，默认允许的最大挂起连接数为100
    /// </summary>
    /// <param name="socket">执行监听的Socket对象</param>
    /// <param name="port">监听的端口号</param>
    public static void StartListen(Socket socket, int port)
    {
        //创建本地终结点
        IPEndPoint localPoint = CreateIPEndPoint(LocalHostName, port);

        //绑定到本地终结点
        BindEndPoint(socket, localPoint);

        //开始监听
        socket.Listen(100);
    }

    /// <summary>
    /// 指定Socket对象执行监听
    /// </summary>
    /// <param name="socket">执行监听的Socket对象</param>
    /// <param name="port">监听的端口号</param>
    /// <param name="maxConnection">允许的最大挂起连接数</param>
    public static void StartListen(Socket socket, int port, int maxConnection)
    {
        //创建本地终结点
        IPEndPoint localPoint = CreateIPEndPoint(LocalHostName, port);

        //绑定到本地终结点
        BindEndPoint(socket, localPoint);

        //开始监听
        socket.Listen(maxConnection);
    }

    /// <summary>
    /// 指定Socket对象执行监听
    /// </summary>
    /// <param name="socket">执行监听的Socket对象</param>
    /// <param name="ip">监听的IP地址</param>
    /// <param name="port">监听的端口号</param>
    /// <param name="maxConnection">允许的最大挂起连接数</param>
    public static void StartListen(Socket socket, string ip, int port, int maxConnection)
    {
        //绑定到本地终结点
        BindEndPoint(socket, ip, port);

        //开始监听
        socket.Listen(maxConnection);
    }

    /// <summary>
    /// 连接到基于TCP协议的服务器,连接成功返回true，否则返回false
    /// </summary>
    /// <param name="socket">Socket对象</param>
    /// <param name="ip">服务器IP地址</param>
    /// <param name="port">服务器端口号</param>     
    public static bool Connect(Socket socket, string ip, int port)
    {
        //连接服务器
        socket.Connect(ip, port);

        //检测连接状态
        return socket.Poll(-1, SelectMode.SelectWrite);

    }

    /// <summary>
    /// 以同步方式向指定的Socket对象发送消息
    /// </summary>
    /// <param name="socket">socket对象</param>
    /// <param name="msg">发送的消息</param>
    public static void SendMsg(Socket socket, byte[] msg)
    {
        //发送消息
        socket.Send(msg, msg.Length, SocketFlags.None);
    }

    /// <summary>
    /// 使用UTF8编码格式以同步方式向指定的Socket对象发送消息
    /// </summary>
    /// <param name="socket">socket对象</param>
    /// <param name="msg">发送的消息</param>
    public static void SendMsg(Socket socket, string msg)
    {
        //将字符串消息转换成字符数组
        byte[] buffer = ConvertHelper.StringToBytes(msg);

        //发送消息
        socket.Send(buffer, buffer.Length, SocketFlags.None);
    }

    /// <summary>
    /// 以同步方式接收消息
    /// </summary>
    /// <param name="socket">socket对象</param>
    /// <param name="buffer">接收消息的缓冲区</param>
    public static void ReceiveMsg(Socket socket, byte[] buffer)
    {
        socket.Receive(buffer);
    }

    /// <summary>
    /// 以同步方式接收消息，并转换为UTF8编码格式的字符串,使用5000字节的默认缓冲区接收。
    /// </summary>
    /// <param name="socket">socket对象</param>        
    public static string ReceiveMsg(Socket socket)
    {
        //定义接收缓冲区
        byte[] buffer = new byte[5000];
        //接收数据，获取接收到的字节数
        int receiveCount = socket.Receive(buffer);

        //定义临时缓冲区
        byte[] tempBuffer = new byte[receiveCount];
        //将接收到的数据写入临时缓冲区
        Buffer.BlockCopy(buffer, 0, tempBuffer, 0, receiveCount);
        //转换成字符串，并将其返回
        return ConvertHelper.BytesToString(tempBuffer);
    }

    /// <summary>
    /// 关闭基于Tcp协议的Socket对象
    /// </summary>
    /// <param name="socket">要关闭的Socket对象</param>
    public static void Close(Socket socket)
    {
        try
        {
            //禁止Socket对象接收和发送数据
            socket.Shutdown(SocketShutdown.Both);
        }
        catch (SocketException ex)
        {
            throw ex;
        }
        finally
        {
            //关闭Socket对象
            socket.Close();
        }
    }

    /// <summary>
    /// 检测本机是否联网（互联网）
    /// </summary>
    /// <param name="connectionDescription"></param>
    /// <param name="reservedValue"></param>
    /// <returns></returns>
    [DllImport("wininet")]
    private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        
    /// <summary>
    /// 检测本机是否联网
    /// </summary>
    /// <returns></returns>
    public static bool IsConnectedInternet()
    {
        int i = 0;
        if (InternetGetConnectedState(out i, 0))
        {
            //已联网
            return true;
        }
        else
        {
            //未联网
            return false;
        }

    }

    [DllImport("WININET", CharSet = CharSet.Auto)]
    private static extern bool InternetGetConnectedState(ref InternetConnectionStatesType lpdwFlags, int dwReserved);

    /// <summary>
    /// 检测本机是否联网的连接属性
    /// </summary>
    public static InternetConnectionStatesType CurrentState
    {
        get
        {
            InternetConnectionStatesType state = 0;

            InternetGetConnectedState(ref state, 0);

            return state;
        }
    }

    /// <summary>
    /// 检测本机是否联网（互联网）
    /// </summary>
    /// <returns></returns>
    public static bool IsOnline()
    {
        InternetConnectionStatesType connectionStatus = CurrentState;
        return (!IsFlagged((int)InternetConnectionStatesType.Offline, (int)connectionStatus));
    }

    internal static bool IsFlagged(int flaggedEnum, int flaggedValue)
    {
        if ((flaggedEnum & flaggedValue) != 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 转换主机域名DNS到IP地址
    /// </summary>
    /// <param name="hostname">主机域名DNS</param>
    /// <returns></returns>
    public static string ConvertDnsToIp(string hostname)
    {
        IPHostEntry ipHostEntry = Dns.GetHostEntry(hostname);

        if (ipHostEntry != null)
        {
            return ipHostEntry.AddressList[0].ToString();
        }
        return null;
    }


    /// <summary>
    /// 转换主机IP地址到DNS域名
    /// </summary>
    /// <param name="ipAddress">主机IP地址</param>
    /// <returns></returns>
    public static string ConvertIpToDns(string ipAddress)
    {
        IPHostEntry ipHostEntry = Dns.GetHostEntry(ipAddress);

        if (ipHostEntry != null)
        {
            return ipHostEntry.HostName;
        }
        return null;
    }

    /// <summary>
    /// 根据IP端点获取主机名称
    /// </summary>
    /// <param name="ipEndPoint">IP端点</param>
    /// <returns></returns>
    public static string GetHostName(IPEndPoint ipEndPoint)
    {
        return GetHostName(ipEndPoint.Address);
    }

    /// <summary>
    /// 根据主机IP地址对象获取主机名称
    /// </summary>
    /// <param name="ip">主机IP地址对象</param>
    /// <returns></returns>
    public static string GetHostName(IPAddress ip)
    {
        return GetHostName(ip.ToString());
    }

    /// <summary>
    /// 根据主机IP获取主机名称
    /// </summary>
    /// <param name="hostIp">主机IP</param>
    /// <returns></returns>
    public static string GetHostName(string hostIp)
    {
        try
        {
            return Dns.GetHostEntry(hostIp).HostName;
        }
        catch
        {
        }
        return null;

    }

    /// <summary>
    /// 得到一台机器的EndPoint端点
    /// </summary>
    /// <param name="entry">主机实体</param>
    /// <returns></returns>
    public static EndPoint GetNetworkAddressEndPoing(IPHostEntry entry)
    {
        return (new IPEndPoint(entry.AddressList[0], 0)) as EndPoint;
    }

    /// <summary>
    /// 主机名是否存在
    /// </summary>
    /// <param name="host">主机名</param>
    /// <returns></returns>
    public static bool IsHostAvailable(string host)
    {
        return (ResolveHost(host) != null);
    }


    /// <summary>
    /// 在主机名解析到一个IP主机实体
    /// </summary>
    /// <param name="host">主机名</param>
    /// <returns></returns>
    public static IPHostEntry ResolveHost(string host)
    {
        try { return Dns.GetHostEntry(host); }
        catch
        {
            // ignored
        }

        return null;
    }

}

/// <summary>
/// Internet连接状态枚举
/// </summary>
[Flags]
public enum InternetConnectionStatesType : int
{
    ModemConnection = 0x1,
    LanConnection = 0x2,
    ProxyConnection = 0x4,
    RasInstalled = 0x10,
    Offline = 0x20,
    ConnectionConfigured = 0x40
}

#region Win32Utils
internal class Win32Utils
{
    public const string REG_NET_CARDS_KEY = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\NetworkCards";
    public const uint GENERIC_READ = 0x80000000;
    public const uint GENERIC_WRITE = 0x40000000;
    public const uint FILE_SHARE_READ = 0x00000001;
    public const uint FILE_SHARE_WRITE = 0x00000002;
    public const uint OPEN_EXISTING = 3;
    public const uint INVALID_HANDLE_VALUE = 0xffffffff;
    public const uint IOCTL_NDIS_QUERY_GLOBAL_STATS = 0x00170002;
    public const int PERMANENT_ADDRESS = 0x01010101;

    [DllImport("kernel32.dll")]
    public static extern int CloseHandle(uint hObject);

    [DllImport("kernel32.dll")]
    public static extern int DeviceIoControl(uint hDevice,
        uint dwIoControlCode,
        ref int lpInBuffer,
        int nInBufferSize,
        byte[] lpOutBuffer,
        int nOutBufferSize,
        ref uint lpbytesReturned,
        int lpOverlapped);

    [DllImport("kernel32.dll")]
    public static extern uint CreateFile(string lpFileName,
        uint dwDesiredAccess,
        uint dwShareMode,
        int lpSecurityAttributes,
        uint dwCreationDisposition,
        uint dwFlagsAndAttributes,
        int hTemplateFile);

}
#endregion