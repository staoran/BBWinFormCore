using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BB.BaseUI.Device;

/// <summary>
/// 摄像头封装类
/// </summary>
public class Camera
{
    private System.Windows.Forms.Control _ctrl;               //视频窗口宿主的托管控件
    private IntPtr _controlHandle;       //视频窗口宿主的托管控件的句柄
    private IntPtr _captureWindowHandle; //视频窗口的句柄
    private int _windowWidth;            //视频窗口的宽度
    private int _windowHeight;           //视频窗口的高度
    private int _imageX, _imageY, _imageWidth, _imageHeight;        //图像的位置和大小
    private AviCap32Helper.FrameEventHandler _frameEventHandler;    //原生的收到帧的事件处理委托
    //private AviCap32Helper.CAPDRIVERCAPS _capDriverCAPS; //捕获驱动器的能力，如有无视频叠加能力、有无控制视频源、视频格式的对话框等；
    private AviCap32Helper.Capstatus _capStatus;          //该结构用于保存视频设备捕获窗口的当前状态，如图像的宽、高等


    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="ctrl">视频窗口宿主的托管控件</param>
    public Camera(System.Windows.Forms.Control ctrl)
    {
        Debug.Assert(ctrl != null);
        _controlHandle = ctrl.Handle;
        _windowWidth = ctrl.Width;
        _windowHeight = ctrl.Height;
        _ctrl = ctrl;
        //_capDriverCAPS = new AviCap32Helper.CAPDRIVERCAPS();
        _capStatus = new AviCap32Helper.Capstatus();
        State = CameraState.Closed;
    }

    /// <summary>
    /// 接受到一帧的事件
    /// </summary>
    public event RecievedFrameEventHandler RecievedFrame;

    /// <summary>
    /// 摄像头连接成功的事件
    /// </summary>
    public event CameraConnectedEventHandler CameraConnected;

    /// <summary>
    /// 摄像头的状态
    /// </summary>
    public CameraState State { get; set; }

    /// <summary>
    /// 启动摄像头
    /// </summary>
    public void Open()
    {
        if (State != CameraState.Closed)
        {
            return;
        }

        //创建视频窗口
        _captureWindowHandle = AviCap32Helper.capCreateCaptureWindowA(
            "",
            AviCap32Helper.WS_VISIBLE + AviCap32Helper.WS_CHILD,
            0,
            0,
            _windowWidth,
            _windowHeight,
            _controlHandle,
            0);
        //连接设备
        if (CapDriverConnect(_captureWindowHandle, 0))
        {
            //获得当前视频 CAPDRIVERCAPS定义了捕获驱动器的能力，如有无视频叠加能力、有无控制视频源、视频格式的对话框等
            //AviCap32Helper.SendMessage(_captureWindowHandle, AviCap32Helper.WM_CAP_DRIVER_GET_CAPS, AviCap32Helper.SizeOf(_capDriverCAPS), ref _capDriverCAPS);
            //获得当前视频流的尺寸 存入CapStatus结构
            AviCap32Helper.SendMessage(_captureWindowHandle, AviCap32Helper.WM_CAP_GET_STATUS, AviCap32Helper.SizeOf(_capStatus), ref _capStatus);
            //if (CameraConnected != null)
            //{
            //    CameraConnected(new CameraConntectedEventArgs()
            //    {
            //        ImageWidth = _capStatus.uiImageWidth,
            //        ImageHeight = _capStatus.uiImageHeight
            //    });
            //}

            _imageX = (_windowWidth - _capStatus.uiImageWidth) / 2;
            _imageY = (_windowHeight - _capStatus.uiImageHeight) / 2;
            _imageWidth = Math.Min(_windowWidth, _capStatus.uiImageWidth);
            _imageHeight = Math.Min(_windowHeight, _capStatus.uiImageHeight);
            //调整视口的位置和大小
            AviCap32Helper.SetWindowPos(_captureWindowHandle, 0, _imageX, _imageY, _capStatus.uiImageWidth, _capStatus.uiImageHeight, 0);

            //设置视频格式
            //AviCap32Helper.BITMAPINFO bitmapInfo = new AviCap32Helper.BITMAPINFO();
            //bitmapInfo.bmiHeader = new AviCap32Helper.BITMAPINFOHEADER();
            //bitmapInfo.bmiHeader.biSize = AviCap32Helper.SizeOf(bitmapInfo.bmiHeader);
            //bitmapInfo.bmiHeader.biWidth = 50;
            //bitmapInfo.bmiHeader.biHeight = 50;
            //bitmapInfo.bmiHeader.biPlanes = 1;
            //bitmapInfo.bmiHeader.biBitCount = 24;
            //CapSetVideoFormat(_captureWindowHandle, ref bitmapInfo, AviCap32Helper.SizeOf(bitmapInfo));

            //设置在PREVIEW模式下设定视频窗口的刷新率 设置每50毫秒显示一帧，即显示帧速为每秒20帧
            CapSetPreviewRate(_captureWindowHandle, 50);
            //打开预览视频的缩放功能
            //AviCap32Helper.SendMessage(_captureWindowHandle, AviCap32Helper.WM_CAP_SET_SCALE, 1, 0);
            CapSetPreview(_captureWindowHandle, true);

            _frameEventHandler = FrameCallBack;
            CapSetCallbackFrame(_captureWindowHandle, _frameEventHandler);
            State = CameraState.Opend;
        }
        else
        {
            throw new Exception("无法连接摄像头");
        }

    }

    /// <summary>
    /// 关闭摄像头
    /// </summary>
    public void Close()
    {
        if (State == CameraState.Closed)
        {
            return;
        }
        if (State == CameraState.Scoping)
        {
            StopKinescope();
        }
        CapDriverDisconnect(_captureWindowHandle);
        State = CameraState.Closed;
    }

    /// <summary>
    /// 抓图保存到文件
    /// </summary>
    /// <param name="bmpPath">要保存bmp文件的路径</param>
    public void GrabImage(string bmpPath)
    {
        if (State != CameraState.Closed)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(bmpPath);
            AviCap32Helper.SendMessage(_captureWindowHandle, AviCap32Helper.WM_CAP_FILE_SAVEDIB, 0, hBmp.ToInt32());
        }
    }

    /// <summary>
    /// 抓图
    /// </summary>
    /// <returns>抓到的图像</returns>
    public Bitmap GrabImage()
    {
        if (State != CameraState.Closed)
        {
            string tempFile = Path.GetTempFileName();
            GrabImage(tempFile);
            if (File.Exists(tempFile))
            {
                Bitmap bmp = Image.FromFile(tempFile) as Bitmap;
                Bitmap bmpOut = new Bitmap(_imageWidth, _imageHeight);

                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(bmp, new Rectangle(Math.Min(0, _imageX), Math.Min(0, _imageY), bmp.Width, bmp.Height),
                    new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
        }
        return null;
    }

    /// <summary>
    /// 开始录像
    /// </summary>
    /// <param name="aviPath">要保存avi文件的路径</param>
    public void StartKinescope(string aviPath)
    {
        if (State == CameraState.Opend)
        {
            IntPtr hBmp = Marshal.StringToHGlobalAnsi(aviPath);
            AviCap32Helper.SendMessage(_captureWindowHandle, AviCap32Helper.WM_CAP_FILE_SET_CAPTURE_FILE, 0, hBmp.ToInt32());
            AviCap32Helper.SendMessage(_captureWindowHandle, AviCap32Helper.WM_CAP_SEQUENCE, 0, 0);
            State = CameraState.Scoping;
        }
    }

    /// <summary>
    /// 停止录像
    /// </summary>
    public void StopKinescope()
    {
        if (State == CameraState.Scoping)
        {
            AviCap32Helper.SendMessage(_captureWindowHandle, AviCap32Helper.WM_CAP_STOP, 0, 0);
            State = CameraState.Opend;
        }
    }

    #region private methods

    //连接设备
    private bool CapDriverConnect(IntPtr lwnd, short i)
    {
        return AviCap32Helper.SendMessage(lwnd, AviCap32Helper.WM_CAP_DRIVER_CONNECT, i, 0);
    }

    //断开设备连接
    private bool CapDriverDisconnect(IntPtr lwnd)
    {
        return AviCap32Helper.SendMessage(lwnd, AviCap32Helper.WM_CAP_DRIVER_DISCONNECT, 0, 0);
    }

    //设置预览
    private bool CapSetPreview(IntPtr lwnd, bool f)
    { return AviCap32Helper.SendMessage(lwnd, AviCap32Helper.WM_CAP_SET_PREVIEW, f, 0); }

    //设置预览Rate
    private bool CapSetPreviewRate(IntPtr lwnd, short wMs)
    {
        return AviCap32Helper.SendMessage(lwnd, AviCap32Helper.WM_CAP_SET_PREVIEWRATE, wMs, 0);
    }

    //设置获取帧的回调
    private bool CapSetCallbackFrame(IntPtr lwnd, AviCap32Helper.FrameEventHandler lpProc)
    {
        return AviCap32Helper.SendMessage(lwnd, AviCap32Helper.WM_CAP_SET_CALLBACK_FRAME, 0, lpProc);
    }

    //获取视频格式
    //private int CapGetVideoFormat(IntPtr hCapWnd, ref AviCap32Helper.BITMAPINFO BmpFormat, int CapFormatSize)
    //{
    //    return AviCap32Helper.capGetVideoFormat(hCapWnd, IntPtr.Zero, 0);
    //}

    //设置视频格式
    private bool CapSetVideoFormat(IntPtr hCapWnd, ref AviCap32Helper.Bitmapinfo bmpFormat, int capFormatSize)
    {
        return AviCap32Helper.SendMessage(hCapWnd, AviCap32Helper.WM_CAP_SET_VIDEOFORMAT, capFormatSize, ref bmpFormat);
    }

    //收到帧时的处理
    private void FrameCallBack(IntPtr lwnd, IntPtr lpVHdr)
    {
        AviCap32Helper.Videohdr videoHeader = new AviCap32Helper.Videohdr();
        byte[] videoData;
        videoHeader = (AviCap32Helper.Videohdr)AviCap32Helper.GetStructure(lpVHdr, videoHeader);
        videoData = new byte[videoHeader.dwBytesUsed];
        AviCap32Helper.Copy(videoHeader.lpData, videoData);
        if (RecievedFrame != null)
        {
            RecievedFrame(videoData);
        }
    }

    #endregion
}

/// <summary>
/// 摄像头状态
/// </summary>
public enum CameraState
{
    /// <summary>
    /// 关闭状态
    /// </summary>
    Closed,
    /// <summary>
    /// 打开状态
    /// </summary>
    Opend,
    /// <summary>
    /// 录像中
    /// </summary>
    Scoping
}

/// <summary>
/// 接收到帧的事件处理参数
/// </summary>
/// <param name="data"></param>
public delegate void RecievedFrameEventHandler(byte[] data);

/// <summary>
/// 摄像头连接成功的参数
/// </summary>
public delegate void CameraConnectedEventHandler(CameraConntectedEventArgs e);

/// <summary>
/// 摄像头连接成功的事件
/// </summary>
public class CameraConntectedEventArgs
{
    public int ImageWidth { get; set; }
    public int ImageHeight { get; set; }
}

/// <summary>
/// AviCap的封装类
/// AviCap包含了执行视频捕获的函数，它给AVI文件I/O和视频、音频设备驱动程序提供一个高级接口
/// </summary>
public class AviCap32Helper
{
    #region 导入函数

    //avicap32的导出函数
    [DllImport("avicap32.dll")]
    public static extern IntPtr capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, int nId);
    [DllImport("avicap32.dll")]
    public static extern bool capGetDriverDescriptionA(short wDriver, byte[] lpszName, int cbName, byte[] lpszVer, int cbVer);
    [DllImport("avicap32.dll")]
    public static extern int capGetVideoFormat(IntPtr hWnd, IntPtr psVideoFormat, int wSize);
    //user32的导出函数
    [DllImport("User32.dll")]
    public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
    [DllImport("User32.dll")]
    public static extern bool SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);
    [DllImport("User32.dll")]
    public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, int lParam);
    [DllImport("User32.dll")]
    public static extern bool SendMessage(IntPtr hWnd, int wMsg, short wParam, FrameEventHandler lParam);
    [DllImport("User32.dll")]
    public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref Bitmapinfo lParam);
    [DllImport("User32.dll")]
    public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref Capdrivercaps lParam);
    [DllImport("User32.dll")]
    public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref Captureparms lParam);
    [DllImport("User32.dll")]
    public static extern bool SendMessage(IntPtr hWnd, int wMsg, int wParam, ref Capstatus lParam);

    #endregion

    #region 消息常量定义

    //消息常量 -------------------------------------------- 
    public const int WM_START = 0x400;    //此并非摄像头消息0x400表示的就是1024 
    public const int WS_CHILD = 0x40000000;
    public const int WS_VISIBLE = 0x10000000;
    public const int SWP_NOMOVE = 0x2;
    public const int SWP_NOZORDER = 0x4;
    public const int WM_CAP_GET_CAPSTREAMPTR = WM_START + 1;
    public const int WM_CAP_SET_CALLBACK_ERROR = WM_START + 2;//设置收回错误
    public const int WM_CAP_SET_CALLBACK_STATUS = WM_START + 3;//设置收回状态
    public const int WM_CAP_SET_CALLBACK_YIELD = WM_START + 4;//设置收回出产
    public const int WM_CAP_SET_CALLBACK_FRAME = WM_START + 5;//设置收回结构
    public const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_START + 6;//设置收回视频流
    public const int WM_CAP_SET_CALLBACK_WAVESTREAM = WM_START + 7;//设置收回视频波流
    public const int WM_CAP_GET_USER_DATA = WM_START + 8;//获得使用者数据
    public const int WM_CAP_SET_USER_DATA = WM_START + 9;//设置使用者数据
    public const int WM_CAP_DRIVER_CONNECT = WM_START + 10;//驱动程序连接
    public const int WM_CAP_DRIVER_DISCONNECT = WM_START + 11;//断开启动程序连接
    public const int WM_CAP_DRIVER_GET_NAME = WM_START + 12;//获得驱动程序名字
    public const int WM_CAP_DRIVER_GET_VERSION = WM_START + 13;//获得驱动程序版本
    public const int WM_CAP_DRIVER_GET_CAPS = WM_START + 14;//获得驱动程序帽子
    public const int WM_CAP_FILE_SET_CAPTURE_FILE = WM_START + 20;//设置捕获文件
    public const int WM_CAP_FILE_GET_CAPTURE_FILE = WM_START + 21;//获得捕获文件
    public const int WM_CAP_FILE_ALLOCATE = WM_START + 22;//分派文件
    public const int WM_CAP_FILE_SAVEAS = WM_START + 23;//另存文件为
    public const int WM_CAP_FILE_SET_INFOCHUNK = WM_START + 24;//设置开始文件
    public const int WM_CAP_FILE_SAVEDIB = WM_START + 25;//保存文件
    public const int WM_CAP_EDIT_COPY = WM_START + 30;//编辑复制
    public const int WM_CAP_SET_AUDIOFORMAT = WM_START + 35;//设置音频格式
    public const int WM_CAP_GET_AUDIOFORMAT = WM_START + 36;//捕获音频格式
    public const int WM_CAP_DLG_VIDEOFORMAT = WM_START + 41;//1065 打开视频格式设置对话框
    public const int WM_CAP_DLG_VIDEOSOURCE = WM_START + 42;//1066 打开属性设置对话框，设置对比度亮度等
    public const int WM_CAP_DLG_VIDEODISPLAY = WM_START + 43;//1067 打开视频显示
    public const int WM_CAP_GET_VIDEOFORMAT = WM_START + 44;//1068 获得视频格式
    public const int WM_CAP_SET_VIDEOFORMAT = WM_START + 45;//1069 设置视频格式
    public const int WM_CAP_DLG_VIDEOCOMPRESSION = WM_START + 46;//1070 打开压缩设置对话框
    public const int WM_CAP_SET_PREVIEW = WM_START + 50;//设置预览
    public const int WM_CAP_SET_OVERLAY = WM_START + 51;//设置覆盖
    public const int WM_CAP_SET_PREVIEWRATE = WM_START + 52;//设置预览比例
    public const int WM_CAP_SET_SCALE = WM_START + 53;//设置刻度
    public const int WM_CAP_GET_STATUS = WM_START + 54;//获得状态
    public const int WM_CAP_SET_SCROLL = WM_START + 55;//设置卷
    public const int WM_CAP_GRAB_FRAME = WM_START + 60;//逮捕结构
    public const int WM_CAP_GRAB_FRAME_NOSTOP = WM_START + 61;//停止逮捕结构
    public const int WM_CAP_SEQUENCE = WM_START + 62;//次序
    public const int WM_CAP_SEQUENCE_NOFILE = WM_START + 63;//使用WM_CAP_SEUENCE_NOFILE消息（capCaptureSequenceNoFile宏），可以不向磁盘文件写入数据。该消息仅在配合回调函数时有用，它允许你的应用程序直接使用音视频数据。
    public const int WM_CAP_SET_SEQUENCE_SETUP = WM_START + 64;//设置安装次序
    public const int WM_CAP_GET_SEQUENCE_SETUP = WM_START + 65;//获得安装次序
    public const int WM_CAP_SET_MCI_DEVICE = WM_START + 66;//设置媒体控制接口
    public const int WM_CAP_GET_MCI_DEVICE = WM_START + 67;//获得媒体控制接口
    public const int WM_CAP_STOP = WM_START + 68;//停止
    public const int WM_CAP_ABORT = WM_START + 69;//异常中断
    public const int WM_CAP_SINGLE_FRAME_OPEN = WM_START + 70;//打开单一的结构
    public const int WM_CAP_SINGLE_FRAME_CLOSE = WM_START + 71;//关闭单一的结构
    public const int WM_CAP_SINGLE_FRAME = WM_START + 72;//单一的结构
    public const int WM_CAP_PAL_OPEN = WM_START + 80;//打开视频
    public const int WM_CAP_PAL_SAVE = WM_START + 81;//保存视频
    public const int WM_CAP_PAL_PASTE = WM_START + 82;//粘贴视频
    public const int WM_CAP_PAL_AUTOCREATE = WM_START + 83; //自动创造
    public const int WM_CAP_PAL_MANUALCREATE = WM_START + 84;//手动创造
    public const int WM_CAP_SET_CALLBACK_CAPCONTROL = WM_START + 85;// 设置收回的错误

    #endregion

    #region 结构体定义

    /*********VideoHdr 结构体*********
    * 
    * 定义了视频数据块的头信息，
    * 在编写回调函数时常用到其数据成员lpData（指向数据缓存的指针）和dwBufferLength（数据缓存的大小）。
    * 视频帧到缓存的捕获则需要应用回调函数和相应的数据块结构 VIDEOHDR
    * 
    *******************************/
    [StructLayout(LayoutKind.Sequential)]
    public struct Videohdr
    {
        public IntPtr lpData;              /* 指向数据缓存的指针 */
        public int dwBufferLength;         /* 数据缓存的大小 */
        public int dwBytesUsed;            /* Bytes actually used */
        public int dwTimeCaptured;         /* Milliseconds from start of stream */
        public int dwUser;                 /* for client's use */
        public int dwFlags;                /* assorted flags (see defines) */
        public int dwReserved;             /* reserved for driver */
    }

    /*********BitmapInfoHeader 结构体*********
    * 
    * BitmapInfoHeader定义了位图的头部信息
    * 
    ****************************************/
    [StructLayout(LayoutKind.Sequential)]
    public struct Bitmapinfoheader
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
    }


    /*********BitmapInfo 结构体*********
    * 
    * BitmapInfo 定义了位图信息
    * 
    ***********************************/
    [StructLayout(LayoutKind.Sequential)]
    public struct Bitmapinfo
    {
        public Bitmapinfoheader bmiHeader;
        public int bmiColors;
    }

    /*********CAPTUREPARMS 结构体*********
    * 
    * CAPTUREPARMS 包含控制视频流捕获过程的参数，如捕获帧频、指定键盘或鼠标键以终止捕获、捕获时间限制等
    * 
    ***********************************/
    [StructLayout(LayoutKind.Sequential)]
    public struct Captureparms
    {
        public int dwRequestMicroSecPerFrame;         // 期望的桢播放率，以毫秒为单位，默认为66667，相当于15桢每秒。
        public bool fMakeUserHitOKToCapture;          // Show "Hit OK to cap" dlg?开始捕获标志位，如果值为真，则在开始捕获前要产生一个询问对话框，默认为假。
        public uint wPercentDropForError;             //所允许的最大丢桢百分比，可以从0变化到100，默认值为10。
        public bool fYield;                           /*另起线程标志位，如果为真，则程序重新启动一个线程用于视频流的捕获，默认值是假。
                                                            但是如果你是为了真，你必须要在程序中处理一些潜在的操作，因为当视频捕获时，其他操作并没有被屏蔽。*/
        public int dwIndexSize;                       // 在AVI文件中所允许的最大数目的索引项(32K)
        public uint wChunkGranularity;                // AVI文件的逻辑尺寸，以字节为单位。如果值是0，则说明该尺寸渐增 在 Win32程序中无用。(2K)
        public bool fUsingDOSMemory;                  // Use DOS buffers?
        public uint wNumVideoRequested;               // 所被允许分配的最大视频缓存
        public bool fCaptureAudio;                    // 音频标志位，如果音频流正在捕获，则该值为真。
        public uint wNumAudioRequested;               // 最大数量的音频缓存，默认值为10。
        public uint vKeyAbort;                        // 终止流捕获的虚拟键盘码，默认值为VK_ESCAPE
        [MarshalAs(UnmanagedType.Bool)]
        public bool fAbortLeftMouse;                  // 终止鼠标左键标志位，如果该值为真，则在流捕获过程中如果点击鼠标左键则该捕获终止，默认值为真。
        public bool fAbortRightMouse;                 // Abort on right mouse?
        public bool fLimitEnabled;                    // 捕获操作时间限制，如果为真，则时间到了以后捕获操作终止，默认为假
        public uint wTimeLimit;                       // 具体终止时间，只有 fLimitEnabled是真时.该位才有效
        public bool fMCIControl;                      // Use MCI video source?
        public bool fStepMCIDevice;                   // Step MCI device?MCI 设备标志。
        public int dwMCIStartTime;                    // Time to start in MS
        public int dwMCIStopTime;                     // Time to stop in MS
        public bool fStepCaptureAt2x;                 // Perform spatial averaging 2x
        public int wStepCaptureAverageFrames;         // 当基于平均采样来创建桢时，桢的采样时间，典型值是5
        public int dwAudioBufferSize;                 // 音频缓存的尺寸，如果用默认值0，缓存尺寸是最大0.5秒，或10k字节。
        public int fDisableWriteCache;                // Attempt to disable write cache
        public int AVStreamMaster;                    //音视频同步标志。
    }


    /*********CAPDRIVERCAPS 结构体*********
    * 
    * CAPDRIVERCAPS 定义了捕获驱动器的能力，如有无视频叠加能力、有无控制视频源、视频格式的对话框等
    * 
    **************************************/
    [StructLayout(LayoutKind.Sequential)]
    public struct Capdrivercaps
    {
        [MarshalAs(UnmanagedType.U2)]
        public UInt16 wDeviceIndex;         //捕获驱动器的索引值，该值可以由0到9变化。
        [MarshalAs(UnmanagedType.Bool)]
        public bool fHasOverlay;            // 视频叠加标志，如果设备支持视频叠加这该位是真。
        [MarshalAs(UnmanagedType.Bool)]
        public bool fHasDlgVideoSource;     //视频资源对话框标志位，如果设备支持视频选择、控制对话框，该值为真。
        [MarshalAs(UnmanagedType.Bool)]
        public bool fHasDlgVideoFormat;     //视频格式对话框标志位，如果设备支持对视频格式对话框的选择，该位真。
        [MarshalAs(UnmanagedType.Bool)]
        public bool fHasDlgVideoDisplay;    //视频展示对话框标志位，如果设备支持对视频捕获缓存区的重新播放，该位是真。
        [MarshalAs(UnmanagedType.Bool)]
        public bool fCaptureInitialized;    //捕获安装标志位，如果捕获驱动器已经成功连接，该值为真。
        //[MarshalAs(UnmanagedType.Bool)]
        public bool fDriverSuppliesPalettes; //驱动器调色板标志位，如果驱动器能创建调色板，则该位是真。
        [MarshalAs(UnmanagedType.I4)]
        public int hVideoIn;
        [MarshalAs(UnmanagedType.I4)]
        public int hVideoOut;
        [MarshalAs(UnmanagedType.I4)]
        public int hVideoExtIn;
        [MarshalAs(UnmanagedType.I4)]
        public int hVideoExtOut;
    }


    /*********CAPSTATUS 结构体*********
    * 
    * CAPSTATUS 定义了捕获窗口的当前状态，如图像的宽、高等
    * 
    **************************************/

    [StructLayout(LayoutKind.Sequential)]
    public struct Capstatus
    {
        public int uiImageWidth;                     //图像宽度
        public int uiImageHeight;                    //图像高度
        public bool fLiveWindow;                     //活动窗口标记，如果窗口正以预览的方式展示图像，那么该值为真
        public bool fOverlayWindow;                  //叠加窗口标志位，如果正在使用硬件叠加，则该位是真。
        public bool fScale;                          //输入所放标志位，如果窗口是正在缩放视频到客户区，那么该位是真。当使用硬件叠加时，改位无效。
        public Point ptScroll;                       //被展示在窗口客户区左上角的那个象素的x、y坐标偏移量。
        public bool fUsingDefaultPalette;            //默认调色板标志位，如果捕获窗口正在使用当前默认调色板，该值为真
        public bool fAudioHardware;                  // 音频硬件标志位，如果系统已经安装了音频硬件，该值为真。
        public bool fCapFileExists;                  //捕获文件标志位，如果一个捕获文件已经被创建，该值为真
        public int dwCurrentVideoFrame;              // 当前或最近流捕获过程中，所处理的桢的数目。包括丢弃的桢。
        public int dwCurrentVideoFramesDropped;      //当前流捕获过程中丢弃的桢的数目。
        public int dwCurrentWaveSamples;             // # of wave samples cap'td
        public int dwCurrentTimeElapsedMS;           // 从当前流捕获开始计算，程序所用的时间，以毫秒为单位。
        public IntPtr hPalCurrent;                   // 当前剪切板的句柄。
        public bool fCapturingNow;                   // 捕获标志位，当捕获是正在进行时，改位是真
        public int dwReturn;                         // 错误返回值，当你的应用程序不支持错误回调函数时可以应用改位
        public int wNumVideoAllocated;               // 被分配的视频缓存的数目。
        public int wNumAudioAllocated;               // 被分配的音频缓存的数目。
    }

    #endregion

    #region 基本函数

    public delegate void FrameEventHandler(IntPtr lwnd, IntPtr lpVHdr);

    public static object GetStructure(IntPtr ptr, ValueType structure)
    {
        return Marshal.PtrToStructure(ptr, structure.GetType());
    }

    public static object GetStructure(int ptr, ValueType structure)
    {
        return GetStructure(new IntPtr(ptr), structure);
    }

    public static void Copy(IntPtr ptr, byte[] data)
    {
        Marshal.Copy(ptr, data, 0, data.Length);
    }

    public static void Copy(int ptr, byte[] data)
    {
        Copy(new IntPtr(ptr), data);
    }

    public static int SizeOf(object structure)
    {
        return Marshal.SizeOf(structure);
    }

    #endregion
}