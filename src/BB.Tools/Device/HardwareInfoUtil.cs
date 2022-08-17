using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace BB.Tools.Device;

/// <summary>
/// 获取系统信息、电脑CPU、磁盘、网卡、内存等相关信息辅助类
/// </summary>
public sealed class HardwareInfoUtil
{
    #region 硬盘信息获取

    [DllImport("kernel32.dll")]
    private static extern int GetVolumeInformation(
        string lpRootPathName,
        string lpVolumeNameBuffer,
        int nVolumeNameSize,
        ref int lpVolumeSerialNumber,
        int lpMaximumComponentLength,
        int lpFileSystemFlags,
        string lpFileSystemNameBuffer,
        int nFileSystemNameSize
    );

    /// <summary>
    /// 获得盘符为drvID的硬盘序列号，缺省为C
    /// </summary>
    /// <param name="drvId">盘符，如"C"</param>
    /// <returns></returns>
    public static string HdVal(string drvId)
    {
        const int maxFilenameLen = 256;
        int retVal = 0;
        int a = 0;
        int b = 0;
        string str1 = null;
        string str2 = null;

        int i = GetVolumeInformation(
            drvId + @":\",
            str1,
            maxFilenameLen,
            ref retVal,
            a,
            b,
            str2,
            maxFilenameLen
        );

        return retVal.ToString();
    }

    /// <summary>
    /// 获取默认C盘的磁盘序列号
    /// </summary>
    /// <returns></returns>
    public static string HdVal()
    {
        return HdVal("C");
    }

    /// <summary>
    /// 获取硬盘ID
    /// </summary>
    /// <returns></returns>
    public static string GetDiskId()
    {
        string hDid = "";
        ManagementClass mc = new ManagementClass("Win32_DiskDrive");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {
            hDid = mo.Properties["signature"].Value.ToString();
        }
        return hDid;
    }

    /// <summary>
    /// 获取硬盘Model的信息
    /// </summary>
    public static string GetDiskModel()
    {
        string hDid = string.Empty;
        using (ManagementClass mc = new ManagementClass("Win32_DiskDrive"))
        {
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                hDid = (string)mo.Properties["Model"].Value;
            }
        }
        return hDid;
    }    

    #endregion

    #region CPU信息获取

    #region CpuUsage类

    /// <summary>
    /// 定义一个抽象基类实现CPU使用率计数器
    /// </summary>
    public abstract class CpuUsage
    {
        /// <summary>
        /// Creates and returns a CpuUsage instance that can be used to query the CPU time on this operating system.
        /// </summary>
        /// <returns>An instance of the CpuUsage class.</returns>
        /// <exception cref="NotSupportedException">This platform is not supported -or- initialization of the CPUUsage object failed.</exception>
        public static CpuUsage Create()
        {
            if (_mCpuUsage == null)
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    _mCpuUsage = new CpuUsageNt();
                else
                    throw new NotSupportedException();
            }
            return _mCpuUsage;
        }

        /// <summary>
        /// Determines the current average CPU load.
        /// </summary>
        /// <returns>An integer that holds the CPU load percentage.</returns>
        /// <exception cref="NotSupportedException">One of the system calls fails. The CPU time can not be obtained.</exception>
        public abstract int Query();

        /// <summary>
        /// Holds an instance of the CPUUsage class.
        /// </summary>
        private static CpuUsage _mCpuUsage = null;
    }

    //------------------------------------------- win nt ---------------------------------------
    /// <summary>
    /// Inherits the CPUUsage class and implements the Query method for Windows NT systems.
    /// </summary>
    /// <remarks>
    /// <p>This class works on Windows NT4, Windows 2000, Windows XP, Windows .NET Server and higher.</p>
    /// <p>You should not use this class directly in your code. Use the CPUUsage.Create() method to instantiate a CPUUsage object.</p>
    /// </remarks>
    internal sealed class CpuUsageNt : CpuUsage
    {
        /// <summary>
        /// Initializes a new CpuUsageNt instance.
        /// </summary>
        /// <exception cref="NotSupportedException">One of the system calls fails.</exception>
        public CpuUsageNt()
        {
            byte[] timeInfo = new byte[32];         // SYSTEM_TIME_INFORMATION structure
            byte[] perfInfo = new byte[312];        // SYSTEM_PERFORMANCE_INFORMATION structure
            byte[] baseInfo = new byte[44];         // SYSTEM_BASIC_INFORMATION structure
            int ret;
            // get new system time
            ret = NtQuerySystemInformation(SystemTimeinformation, timeInfo, timeInfo.Length, IntPtr.Zero);
            if (ret != NoError)
                throw new NotSupportedException();
            // get new CPU's idle time
            ret = NtQuerySystemInformation(SystemPerformanceinformation, perfInfo, perfInfo.Length, IntPtr.Zero);
            if (ret != NoError)
                throw new NotSupportedException();
            // get number of processors in the system
            ret = NtQuerySystemInformation(SystemBasicinformation, baseInfo, baseInfo.Length, IntPtr.Zero);
            if (ret != NoError)
                throw new NotSupportedException();
            // store new CPU's idle and system time and number of processors
            _oldIdleTime = BitConverter.ToInt64(perfInfo, 0); // SYSTEM_PERFORMANCE_INFORMATION.liIdleTime
            _oldSystemTime = BitConverter.ToInt64(timeInfo, 8); // SYSTEM_TIME_INFORMATION.liKeSystemTime
            _processorCount = baseInfo[40];
        }

        /// <summary>
        /// Determines the current average CPU load.
        /// </summary>
        /// <returns>An integer that holds the CPU load percentage.</returns>
        /// <exception cref="NotSupportedException">One of the system calls fails. The CPU time can not be obtained.</exception>
        public override int Query()
        {
            byte[] timeInfo = new byte[32];         // SYSTEM_TIME_INFORMATION structure
            byte[] perfInfo = new byte[312];        // SYSTEM_PERFORMANCE_INFORMATION structure
            double dbIdleTime, dbSystemTime;
            int ret;
            // get new system time
            ret = NtQuerySystemInformation(SystemTimeinformation, timeInfo, timeInfo.Length, IntPtr.Zero);
            if (ret != NoError)
                throw new NotSupportedException();
            // get new CPU's idle time
            ret = NtQuerySystemInformation(SystemPerformanceinformation, perfInfo, perfInfo.Length, IntPtr.Zero);
            if (ret != NoError)
                throw new NotSupportedException();
            // CurrentValue = NewValue - OldValue
            dbIdleTime = BitConverter.ToInt64(perfInfo, 0) - _oldIdleTime;
            dbSystemTime = BitConverter.ToInt64(timeInfo, 8) - _oldSystemTime;
            // CurrentCpuIdle = IdleTime / SystemTime
            if (dbSystemTime != 0)
                dbIdleTime = dbIdleTime / dbSystemTime;
            // CurrentCpuUsage% = 100 - (CurrentCpuIdle * 100) / NumberOfProcessors
            dbIdleTime = 100.0 - dbIdleTime * 100.0 / _processorCount + 0.5;
            // store new CPU's idle and system time
            _oldIdleTime = BitConverter.ToInt64(perfInfo, 0); // SYSTEM_PERFORMANCE_INFORMATION.liIdleTime
            _oldSystemTime = BitConverter.ToInt64(timeInfo, 8); // SYSTEM_TIME_INFORMATION.liKeSystemTime
            return (int)dbIdleTime;
        }

        /// <summary>
        /// NtQuerySystemInformation is an internal Windows function that retrieves various kinds of system information.
        /// </summary>
        /// <param name="dwInfoType">One of the values enumerated in SYSTEM_INFORMATION_CLASS, indicating the kind of system information to be retrieved.</param>
        /// <param name="lpStructure">Points to a buffer where the requested information is to be returned. The size and structure of this information varies depending on the value of the SystemInformationClass parameter.</param>
        /// <param name="dwSize">Length of the buffer pointed to by the SystemInformation parameter.</param>
        /// <param name="returnLength">Optional pointer to a location where the function writes the actual size of the information requested.</param>
        /// <returns>Returns a success NTSTATUS if successful, and an NTSTATUS error code otherwise.</returns>
        [DllImport("ntdll", EntryPoint = "NtQuerySystemInformation")]
        private static extern int NtQuerySystemInformation(int dwInfoType, byte[] lpStructure, int dwSize, IntPtr returnLength);

        /// <summary>Returns the number of processors in the system in a SYSTEM_BASIC_INFORMATION structure.</summary>
        private const int SystemBasicinformation = 0;

        /// <summary>Returns an opaque SYSTEM_PERFORMANCE_INFORMATION structure.</summary>
        private const int SystemPerformanceinformation = 2;

        /// <summary>Returns an opaque SYSTEM_TIMEOFDAY_INFORMATION structure.</summary>
        private const int SystemTimeinformation = 3;

        /// <summary>The value returned by NtQuerySystemInformation is no error occurred.</summary>
        private const int NoError = 0;

        /// <summary>Holds the old idle time.</summary>
        private long _oldIdleTime;

        /// <summary>Holds the old system time.</summary>
        private long _oldSystemTime;

        /// <summary>Holds the number of processors in the system.</summary>
        private double _processorCount;
    }
    #endregion

    /// <summary>
    /// 获得Cpu使用率
    /// </summary>
    public static int GetCpuUsage()
    {
        return CpuUsage.Create().Query();
    }

    /// <summary>
    /// 获取CPU的ID
    /// </summary>
    /// <returns></returns>
    public static string GetCpuId()
    {
        string strCpuId = "";
        try
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                strCpuId = mo.Properties["ProcessorId"].Value.ToString();
                break;
            }
        }
        catch
        {
            strCpuId = "078BFBFF00020FC1";//默认给出一个
        }
        return strCpuId;

    }

    /// <summary>
    /// 获取CPU的名称
    /// </summary>
    /// <returns></returns>
    public static string GetCpuName()
    {
        RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0");

        object obj = rk.GetValue("ProcessorNameString");
        string cpuName = (string)obj;
        return cpuName.TrimStart();
    }
 
    #endregion

    #region USB盘符列表

    /// <summary>
    /// 返回USB盘符列表
    /// </summary>
    public static List<string> GetUsbDriveLetters()
    {
        List<string> list = new List<string>();
        ManagementObjectSearcher ddMgmtObjSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");

        foreach (ManagementObject ddObj in ddMgmtObjSearcher.Get())
        {
            foreach (ManagementObject dpObj in ddObj.GetRelated("Win32_DiskPartition"))
            {
                foreach (ManagementObject ldObj in dpObj.GetRelated("Win32_LogicalDisk"))
                {
                    list.Add(ldObj["DeviceID"].ToString());
                }
            }
        }

        return list;
    } 
    #endregion

    #region 获取硬盘信息的实现

    #region 结构

    /// <summary>
    /// 硬盘信息
    /// </summary>
    [Serializable]
    public struct HardDiskInfo
    {
        /// <summary>
        /// 型号
        /// </summary>
        public string ModuleNumber;
        /// <summary>
        /// 固件版本
        /// </summary>
        public string Firmware;
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber;
        /// <summary>
        /// 容量，以M为单位
        /// </summary>
        public uint Capacity;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct GetVersionOutParams
    {
        public byte bVersion;
        public byte bRevision;
        public byte bReserved;
        public byte bIDEDeviceMap;
        public uint fCapabilities;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] dwReserved; // For future use.
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct IdeRegs
    {
        public byte bFeaturesReg;
        public byte bSectorCountReg;
        public byte bSectorNumberReg;
        public byte bCylLowReg;
        public byte bCylHighReg;
        public byte bDriveHeadReg;
        public byte bCommandReg;
        public byte bReserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct SendCmdInParams
    {
        public uint cBufferSize;
        public IdeRegs irDriveRegs;
        public byte bDriveNumber;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] bReserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] dwReserved;
        public byte bBuffer;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct DriverStatus
    {
        public byte bDriverError;
        public byte bIDEStatus;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] bReserved;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] dwReserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct SendCmdOutParams
    {
        public uint cBufferSize;
        public DriverStatus DriverStatus;
        public IdSector bBuffer;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 512)]
    internal struct IdSector
    {
        public ushort wGenConfig;
        public ushort wNumCyls;
        public ushort wReserved;
        public ushort wNumHeads;
        public ushort wBytesPerTrack;
        public ushort wBytesPerSector;
        public ushort wSectorsPerTrack;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public ushort[] wVendorUnique;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] sSerialNumber;
        public ushort wBufferType;
        public ushort wBufferSize;
        public ushort wECCSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] sFirmwareRev;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public byte[] sModelNumber;
        public ushort wMoreVendorUnique;
        public ushort wDoubleWordIO;
        public ushort wCapabilities;
        public ushort wReserved1;
        public ushort wPIOTiming;
        public ushort wDMATiming;
        public ushort wBS;
        public ushort wNumCurrentCyls;
        public ushort wNumCurrentHeads;
        public ushort wNumCurrentSectorsPerTrack;
        public uint ulCurrentSectorCapacity;
        public ushort wMultSectorStuff;
        public uint ulTotalAddressableSectors;
        public ushort wSingleWordDMA;
        public ushort wMultiWordDMA;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] bReserved;
    }

    #endregion

    #region API

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern int CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr CreateFile(
        string lpFileName,
        uint dwDesiredAccess,
        uint dwShareMode,
        IntPtr lpSecurityAttributes,
        uint dwCreationDisposition,
        uint dwFlagsAndAttributes,
        IntPtr hTemplateFile);

    [DllImport("kernel32.dll")]
    static extern int DeviceIoControl(
        IntPtr hDevice,
        uint dwIoControlCode,
        IntPtr lpInBuffer,
        uint nInBufferSize,
        ref GetVersionOutParams lpOutBuffer,
        uint nOutBufferSize,
        ref uint lpBytesReturned,
        [Out] IntPtr lpOverlapped);

    [DllImport("kernel32.dll")]
    static extern int DeviceIoControl(
        IntPtr hDevice,
        uint dwIoControlCode,
        ref SendCmdInParams lpInBuffer,
        uint nInBufferSize,
        ref SendCmdOutParams lpOutBuffer,
        uint nOutBufferSize,
        ref uint lpBytesReturned,
        [Out] IntPtr lpOverlapped);


    const uint DfpGetVersion = 0x00074080;
    const uint DfpSendDriveCommand = 0x0007c084;
    const uint DfpReceiveDriveData = 0x0007c088;


    const uint GenericRead = 0x80000000;
    const uint GenericWrite = 0x40000000;
    const uint FileShareRead = 0x00000001;
    const uint FileShareWrite = 0x00000002;
    const uint CreateNew = 1;
    const uint OpenExisting = 3;


    #endregion

    /// <summary>
    /// 获取9X架构的硬盘信息
    /// </summary>
    private static HardDiskInfo GetHddInfo9X(byte driveIndex)
    {
        GetVersionOutParams vers = new GetVersionOutParams();
        SendCmdInParams inParam = new SendCmdInParams();
        SendCmdOutParams outParam = new SendCmdOutParams();
        uint bytesReturned = 0;


        IntPtr hDevice = CreateFile(
            @"\\.\Smartvsd",
            0,
            0,
            IntPtr.Zero,
            CreateNew,
            0,
            IntPtr.Zero);
        if (hDevice == IntPtr.Zero)
        {
            throw new Exception("Open smartvsd.vxd failed.");
        }
        if (0 == DeviceIoControl(
            hDevice,
            DfpGetVersion,
            IntPtr.Zero,
            0,
            ref vers,
            (uint)Marshal.SizeOf(vers),
            ref bytesReturned,
            IntPtr.Zero))
        {
            CloseHandle(hDevice);
            throw new Exception("DeviceIoControl failed:DFP_GET_VERSION");
        }
        // If IDE identify command not supported, fails
        if (0 == (vers.fCapabilities & 1))
        {
            CloseHandle(hDevice);
            throw new Exception("Error: IDE identify command not supported.");
        }
        if (0 != (driveIndex & 1))
        {
            inParam.irDriveRegs.bDriveHeadReg = 0xb0;
        }
        else
        {
            inParam.irDriveRegs.bDriveHeadReg = 0xa0;
        }
        if (0 != (vers.fCapabilities & (16 >> driveIndex)))
        {
            // We don''t detect a ATAPI device.
            CloseHandle(hDevice);
            throw new Exception($"Drive {driveIndex + 1} is a ATAPI device, we don''t detect it");
        }
        else
        {
            inParam.irDriveRegs.bCommandReg = 0xec;
        }
        inParam.bDriveNumber = driveIndex;
        inParam.irDriveRegs.bSectorCountReg = 1;
        inParam.irDriveRegs.bSectorNumberReg = 1;
        inParam.cBufferSize = 512;
        if (0 == DeviceIoControl(
            hDevice,
            DfpReceiveDriveData,
            ref inParam,
            (uint)Marshal.SizeOf(inParam),
            ref outParam,
            (uint)Marshal.SizeOf(outParam),
            ref bytesReturned,
            IntPtr.Zero))
        {
            CloseHandle(hDevice);
            throw new Exception("DeviceIoControl failed: DFP_RECEIVE_DRIVE_DATA");
        }
        CloseHandle(hDevice);


        return GetHardDiskInfo(outParam.bBuffer);
    }

    /// <summary>
    /// 获取NT架构的硬盘信息
    /// </summary>
    private static HardDiskInfo GetHddInfoNt(byte driveIndex)
    {
        GetVersionOutParams vers = new GetVersionOutParams();
        SendCmdInParams inParam = new SendCmdInParams();
        SendCmdOutParams outParam = new SendCmdOutParams();
        uint bytesReturned = 0;


        // We start in NT/Win2000
        IntPtr hDevice = CreateFile(
            $@"\\.\PhysicalDrive{driveIndex}",
            GenericRead | GenericWrite,
            FileShareRead | FileShareWrite,
            IntPtr.Zero,
            OpenExisting,
            0,
            IntPtr.Zero);
        if (hDevice == IntPtr.Zero)
        {
            throw new Exception("CreateFile faild.");
        }
        if (0 == DeviceIoControl(
            hDevice,
            DfpGetVersion,
            IntPtr.Zero,
            0,
            ref vers,
            (uint)Marshal.SizeOf(vers),
            ref bytesReturned,
            IntPtr.Zero))
        {
            CloseHandle(hDevice);
            throw new Exception($"Drive {driveIndex + 1} may not exists.");
        }
        // If IDE identify command not supported, fails
        if (0 == (vers.fCapabilities & 1))
        {
            CloseHandle(hDevice);
            throw new Exception("Error: IDE identify command not supported.");
        }
        // Identify the IDE drives
        if (0 != (driveIndex & 1))
        {
            inParam.irDriveRegs.bDriveHeadReg = 0xb0;
        }
        else
        {
            inParam.irDriveRegs.bDriveHeadReg = 0xa0;
        }
        if (0 != (vers.fCapabilities & (16 >> driveIndex)))
        {
            CloseHandle(hDevice);
            throw new Exception($"Drive {driveIndex + 1} is a ATAPI device, we don''t detect it.");
        }
        else
        {
            inParam.irDriveRegs.bCommandReg = 0xec;
        }
        inParam.bDriveNumber = driveIndex;
        inParam.irDriveRegs.bSectorCountReg = 1;
        inParam.irDriveRegs.bSectorNumberReg = 1;
        inParam.cBufferSize = 512;


        if (0 == DeviceIoControl(
            hDevice,
            DfpReceiveDriveData,
            ref inParam,
            (uint)Marshal.SizeOf(inParam),
            ref outParam,
            (uint)Marshal.SizeOf(outParam),
            ref bytesReturned,
            IntPtr.Zero))
        {
            CloseHandle(hDevice);
            throw new Exception("DeviceIoControl failed: DFP_RECEIVE_DRIVE_DATA");
        }
        CloseHandle(hDevice);


        return GetHardDiskInfo(outParam.bBuffer);
    }

    /// <summary>
    /// 获取硬盘信息的细节
    /// </summary>
    /// <param name="phdinfo"></param>
    /// <returns></returns>
    private static HardDiskInfo GetHardDiskInfo(IdSector phdinfo)
    {
        HardDiskInfo hddInfo = new HardDiskInfo();
        ChangeByteOrder(phdinfo.sModelNumber);
        hddInfo.ModuleNumber = Encoding.ASCII.GetString(phdinfo.sModelNumber).Trim();

        ChangeByteOrder(phdinfo.sFirmwareRev);
        hddInfo.Firmware = Encoding.ASCII.GetString(phdinfo.sFirmwareRev).Trim();

        ChangeByteOrder(phdinfo.sSerialNumber);
        hddInfo.SerialNumber = Encoding.ASCII.GetString(phdinfo.sSerialNumber).Trim();

        hddInfo.Capacity = phdinfo.ulTotalAddressableSectors / 2 / 1024;

        return hddInfo;
    }

    /// <summary>
    /// 将byte数组中保存的信息转换成字符串
    /// </summary>
    /// <param name="charArray"></param>
    private static void ChangeByteOrder(byte[] charArray)
    {
        byte temp;
        for (int i = 0; i < charArray.Length; i += 2)
        {
            temp = charArray[i];
            charArray[i] = charArray[i + 1];
            charArray[i + 1] = temp;
        }
    }

    /// <summary>
    /// 获得硬盘信息
    /// </summary>
    public static HardDiskInfo GetHdInfo(byte driveIndex)
    {
        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Win32Windows:
                return GetHddInfo9X(driveIndex);
            case PlatformID.Win32NT:
                return GetHddInfoNt(driveIndex);
            case PlatformID.Win32S:
                throw new NotSupportedException("Win32s is not supported.");
            case PlatformID.WinCE:
                throw new NotSupportedException("WinCE is not supported.");
            default:
                throw new NotSupportedException("Unknown Platform.");
        }
    }

    #endregion
        
    #region 其他数据
        
    /// <summary>
    /// 获取MAC地址
    /// </summary>
    /// <returns></returns>
    public static string GetMacAddress()
    {
        string mac = "";
        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {
            if ((bool)mo["IPEnabled"] == true)
            {
                mac = mo["MacAddress"].ToString();
                break;
            }
        }
        return mac;
    }

    /// <summary>
    /// 获取IP地址
    /// </summary>
    public static string GetIPAddress()
    {
        string st = "";
        ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {
            if ((bool)mo["IPEnabled"] == true)
            {
                //st=mo["IPAddress"].ToString();
                Array ar;
                ar = (Array)(mo.Properties["IPAddress"].Value);
                st = ar.GetValue(0).ToString();
                break;
            }
        }
        moc = null;
        mc = null;
        return st;
    }

    /// <summary>
    /// 获取操作系统的登录用户名
    /// </summary>
    public static string GetUserName()
    {
        return Environment.UserName;
    }
    /// <summary>
    /// 获取计算机名
    /// </summary>
    public static string GetComputerName()
    {
        return Environment.MachineName;
    }

    /// <summary>
    /// 获取PC类型
    /// </summary>
    public static string GetSystemType()
    {
        string st = "";
        ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {

            st = mo["SystemType"].ToString();

        }
        return st;
    }

    /// <summary>
    /// 获取物理内存
    /// </summary>
    public static string GetTotalPhysicalMemory()
    {
        string st = "";
        ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
        ManagementObjectCollection moc = mc.GetInstances();
        foreach (ManagementObject mo in moc)
        {

            st = mo["TotalPhysicalMemory"].ToString();

        }
        return st;
    }
        
    #endregion

}