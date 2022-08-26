namespace BB.Tools.Const;

public partial class Const
{
	/// <summary>
	/// 休眠时间
	/// </summary>
	public const int SLEEP_TIME = 200;

	//部分常量
	public const int WS_CHILD = 0x40000000;
	public const int WS_VISIBLE = 0x10000000;
	public const int SWP_NOMOVE = 0x2;
	public const int SWP_NOZORDER = 0x4;

	/// <summary>点</summary>
	public const string DOT = ".";

	/// <summary>下划线</summary>
	public const string UNDERSCORE = "_";

	/// <summary>逗号+空格</summary>
	public const string COMMA_SPACE = ", ";

	/// <summary>逗号</summary>
	public const string COMMA = ",";

	/// <summary>左括号</summary>
	public const string OPEN_PAREN = "(";

	/// <summary>右括号</summary>
	public const string CLOSED_PAREN = ")";

	/// <summary>单引号</summary>
	public const string SINGLE_QUOTE = "\'";

	/// <summary> 双引号 </summary>
	public const string DOUBLE_QUOTE = "\"";

	/// <summary> 文本换行符 </summary>
	public const string WRAP_TEXT = "\r\n";

	/// <summary>
	/// 斜杠
	/// </summary>
	public const string SLASH = @"\";

	/// <summary>
	/// 竖线
	/// </summary>
	public const string VERTICAL_LINE  = "|";

	/// <summary>
	/// 冒号:
	/// </summary>
	public const string COLON = ":";

	/// <summary>
	/// 加
	/// </summary>
	public const string PLUS = "+";

	/// <summary>
	/// 减
	/// </summary>
	public const string MINUS = "-";

	/// <summary>
	/// 乘
	/// </summary>
	public const string MULTIPLY = "*";

	/// <summary>
	/// 除以
	/// </summary>
	public const string DIVIDE = "/";

	/// <summary>
	/// 单引号
	/// </summary>
	public const string SINGLE_QUOTATION = "'";

	/// <summary>
	/// 双个单引号
	/// </summary>
	public const string DUOBLE_SINGLE_QUOTATION = "''";

	// Not used in this application.  Descriptions can be found with documentation
	// of Windows GDI function SetMapMode
	public const int MM_TEXT = 1;
	public const int MM_LOMETRIC = 2;
	public const int MM_HIMETRIC = 3;
	public const int MM_LOENGLISH = 4;
	public const int MM_HIENGLISH = 5;
	public const int MM_TWIPS = 6;

	// Ensures that the metafile maintains a 1:1 aspect ratio
	public const int MM_ISOTROPIC = 7;

	// Allows the x-coordinates and y-coordinates of the metafile to be adjusted
	// independently
	public const int MM_ANISOTROPIC = 8;

	// Represents an unknown font family
	public const string FF_UNKNOWN = "UNKNOWN";

	// The number of hundredths of millimeters (0.01 mm) in an inch
	// For more information, see GetImagePrefix() method.
	public const int HMM_PER_INCH = 2540;

	// The number of twips in an inch
	// For more information, see GetImagePrefix() method.
	public const int TWIPS_PER_INCH = 1440;

	public const int IME_CMODE_FULLSHAPE = 0x8;
	public const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;
       

	// public const int SC_RESTORE = 0xF120;
	public const int SC_CLOSE = 0xF060;
	public const int SC_MAXIMIZE = 0xF030;
	public const int SC_MINIMIZE = 0xF020;

        
        
	public static IntPtr HwndTopmost = new IntPtr(-1);
	public static IntPtr HwndTop = new IntPtr(0);
	public const int IDOK = 011;

	public const int GWL_EXSTYLE = -20;
	public const int GWL_ID = -12;
	public const int BS_DEFPUSHBUTTON = 0x1;
	public const int BS_GROUPBOX = 0x7;
	public const int BS_CHECKBOX = 0x2;
	public const int BS_AUTOCHECKBOX = 0x3;
	public const int BS_RADIOBUTTON = 0x4;
	public const int BS_AUTORADIOBUTTON = 0x9;

	public const int WM_SIZE = 0x5;
	public const int WM_ACTIVATE = 0x06;
	public const int WM_CLOSE = 0x0010;
	public const int WM_SETTEXT = 0xC;
	public const int WM_GETTEXT = 0xD;

	public const int WM_KEYDOWN = 0x100;  //Key down
	public const int WM_KEYUP = 0x101;   //Key up
	public const int WM_CHAR = 0x0102;
	public const int WM_SYSKEYDOWN = 0x0104;
	public const int WM_SYSKEYUP = 0x0105;
	public const int WM_COMMAND = 0x111;
	public const int WM_SYSCOMMAND = 0x0112;

	public const int WM_MOUSEMOVE = 0x200;
	public const int WM_LBUTTONDOWN = 0x201;
	public const int WM_LBUTTONUP = 0x202;
	public const int WM_LBUTTONDBLCLK = 0x203; //Left mousebutton doubleclick

	public const int WM_RBUTTONDOWN = 0x204; //Right mousebutton down
	public const int WM_RBUTTONUP = 0x205;   //Right mousebutton up
	public const int WM_RBUTTONDBLCLK = 0x206; //Right mousebutton doubleclick
	public const int WM_MBUTTONDOWN = 0x207;
	public const int WM_MBUTTONUP = 0x208;
	public const int WM_MOUSEWHEEL = 0x020A;
	public const int WM_XBUTTONDOWN = 0x20B;
	public const int WM_XBUTTONUP = 0x20C;

	public const int WM_USER = 0x0400;

	#region Mouse stuff
	public const int XBUTTON1 = 0x1;
	public const int XBUTTON2 = 0x2;
	#endregion

	#region TreeView
	public const int TV_FIRST = 4352;
	public const int TVSIL_NORMAL = 0;
	public const int TVSIL_STATE = 2;
	public const int TVM_EXPAND = TV_FIRST + 2;
	public const int TVM_SETIMAGELIST = TV_FIRST + 9;
	public const int TVM_GETNEXTITEM = TV_FIRST + 10;
	public const int TVM_SELECTITEM = TV_FIRST + 11;
	public const int TVM_GETITEMRECT = TV_FIRST + 4;
	public const int TVM_GETITEMSTATE = TV_FIRST + 39;
	public const int TVIF_HANDLE = 16;
	public const int TVIF_STATE = 8;
	public const int TVIF_TEXT = 0x0001;
	public const int TVIS_STATEIMAGEMASK = 61440;
	public const int TVIS_SELECTED = 0x0002;
	public const int TVM_GETITEM = TV_FIRST + 62;
	public const int TVM_SETITEM = TV_FIRST + 13;
	public const int TVGN_ROOT = 0x0;
	public const int TVGN_NEXT = 0x1;
	public const int TVGN_PARENT = 0x3;
	public const int TVGN_CHILD = 0x4;
	public const int TVGN_DROPHILITE = 0x8;
	public const int TVGN_CARET = 0x9;
	public const int TVE_COLLAPSE = 0x1;
	public const int TVE_EXPAND = 0x2;
	#endregion

	#region Edit
	public const int EM_GETPASSWORDCHAR = 0x00D2;
	public const int EM_SETPASSWORDCHAR = 0x00CC;
	#endregion

	#region Button
	public const int BM_GETSTATE = 0x00F2;
	public const int BST_UNCHECKED = 0x0000;
	public const int BST_CHECKED = 0x0001;
	#endregion

	#region Process
	public const int PROCESS_VM_READ = 0x10;
	public const int PROCESS_VM_WRITE = 0x20;
	public const int PROCESS_VM_OPERATION = 0x8;
	public const int PROCESS_ALL_ACCESS = 0x000F0000 | 0x00100000 | 0xFFF;
	#endregion

	#region Memory
	public const int MEM_COMMIT = 0x1000;
	public const int MEM_RESERVE = 0x2000;
	public const int MEM_RELEASE = 0x8000;
	public const int PAGE_READWRITE = 0x4;
	#endregion

	#region Mouse
       
	#endregion

	#region PropertySheet
	public const int PSM_SETCURSEL = WM_USER + 101;
	public const int PSM_GETCURRENTPAGEHWND = WM_USER + 118;
	#endregion

	#region ListView
	public const int LVIF_TEXT = 0x0001;
	public const int LVIF_IMAGE = 0x0002;
	public const int LVIF_PARAM = 0x0004;
	public const int LVIF_STATE = 0x0008;
	public const int LVIF_INDENT = 0x0010;
	public const int LVIF_NORECOMPUTE = 0x0800;
	public const int LVM_FIRST = 0x1000;
	public const int LVM_GETSUBITEMRECT = (LVM_FIRST + 56);
	public const int LVM_GETITEMSTATE = (LVM_FIRST + 44);
	public const int LVM_GETITEMTEXTW = (LVM_FIRST + 115);
	public const int LVM_GETNEXTITEM = (LVM_FIRST + 12);
	public const int LVM_GETITEMCOUNT = (LVM_FIRST + 4);
	public const int LVM_GETITEM = (LVM_FIRST + 75);
	public const int LVM_ENSUREVISIBLE = (LVM_FIRST + 19);
	public const int LVM_SETITEMSTATE = (LVM_FIRST + 43);
	public const int LVM_SETITEMTEXT = (LVM_FIRST + 116);
	public const int LVM_GETHEADER = (LVM_FIRST + 31);
	public const int LVM_GETEDITCONTROL = (LVM_FIRST + 24);
	public const int LVNI_ALL = 0x0000;
	public const int LVNI_FOCUSED = 0x0001;
	public const int LVNI_SELECTED = 0x0002;
	public const int LVNI_CUT = 0x0004;
	public const int LVNI_DROPHILITED = 0x0008;
	public const int LVNI_ABOVE = 0x0100;
	public const int LVNI_BELOW = 0x0200;
	public const int LVNI_TOLEFT = 0x0400;
	public const int LVNI_TORIGHT = 0x0800;
	public const int LVIS_FOCUSED = 0x0001;
	public const int LVIS_SELECTED = 0x0002;
	public const int LVIS_CUT = 0x0004;
	public const int LVIS_DROPHILITED = 0x0008;
	public const int LVIS_GLOW = 0x0010;
	public const int LVIS_ACTIVATING = 0x0020;
	public const int LVIR_BOUNDS = 0;
	#endregion

	#region Header
	public const int HDM_FIRST = 0x1200;
	public const int HDM_GETITEMCOUNT = HDM_FIRST;
	public const int HDM_GETITEMRECT = HDM_FIRST + 7;
	#endregion

	#region Toolbar
	public const int TB_GETBUTTON = WM_USER + 23;
	public const int TB_BUTTONCOUNT = WM_USER + 24;
	public const int TB_GETBUTTONTEXT = WM_USER + 45;
	public const int TB_ISBUTTONHIDDEN = WM_USER + 12;
	public const int TB_GETRECT = WM_USER + 51;
	public const int TB_GETITEMRECT = WM_USER + 29;
	public const int TB_ISBUTTONCHECKED = WM_USER + 10;
	public const int TB_GETSTYLE = WM_USER + 57;

	public const int TBSTYLE_BUTTON = 0x0000;
	public const int TBSTYLE_SEP = 0x0001;
	public const int TBSTYLE_CHECK = 0x0002;
	public const int TBSTYLE_DROPDOWN = 0x0008;
	public const int TBSTATE_CHECKED = 0x01;
	#endregion

	#region Menu
	public const uint MF_BYPOSITION = 0x400;
	#endregion

	#region Tab Control
	public static int TcmFirst = 0x1300;
	public static int TcmSetcursel = TcmFirst + 12;
	#endregion

	#region ComboBox
	public const int CB_ERR = -1;
	public const int CBS_SIMPLE = 0x0001;
	public const int CBS_DROPDOWN = 0x0002;
	public const int CBS_DROPDOWNLIST = 0x0003;
	public const int CBS_OWNERDRAWFIXED = 0x0010;
	public const int CBS_OWNERDRAWVARIABLE = 0x0020;
	public const int CBS_AUTOHSCROLL = 0x0040;
	public const int CBS_OEMCONVERT = 0x0080;
	public const int CBS_SORT = 0x0100;
	public const int CBS_HASSTRINGS = 0x0200;
	public const int CBS_NOINTEGRALHEIGHT = 0x0400;
	public const int CBS_DISABLENOSCROLL = 0x0800;
	public const int CB_SETCURSEL = 0x014E;
	public const int CB_SELECTSTRING = 0x014D;
	public const int CB_GETCOUNT = 0x146;
	public const int CB_GETCURSEL = 0x0147;
	public const int CB_GETLBTEXT = 0x0148;
	public const int CBN_SELCHANGE = 1;
	public const int CBN_DBLCLK = 2;
	public const int CBN_SETFOCUS = 3;
	public const int CBN_KILLFOCUS = 4;
	public const int CBN_EDITCHANGE = 5;
	public const int CBN_EDITUPDATE = 6;
	public const int CBN_DROPDOWN = 7;
	public const int CBN_CLOSEUP = 8;
	public const int CBN_SELENDOK = 9;
	public const int CBN_SELENDCANCEL = 10;
	#endregion

	#region ListBox
	public const int LB_GETCOUNT = 0x018B;
	public const int LB_GETITEMRECT = 0x0198;
	public const int LB_ADDSTRING = 0x00000180;
	public const int LB_SETCURSEL = 0x00000186;
	public const int LB_GETITEMHEIGHT = 0x01A1;
	public const int LBS_OWNERDRAWVARIABLE = 0x0020;
	public const int LBS_MULTIPLESEL = 0x0008;
	public const int LBS_EXTENDEDSEL = 0x0800;
	public const int LB_GETCURSEL = 0x00000188;
	public const int LB_GETTEXTLEN = 0x0000018A;
	public const int LB_GETTEXT = 0x00000189;
	public const int LB_DELETESTRING = 0x00000182;
	public const int LB_SETSEL = 0x0185;
	public const int LB_SETTOPINDEX = 0x0197;
	#endregion

	#region BCGDockControlBar
	public const int ID_DOCKBAR_AUTOHIDE = 16037;
	public const int ID_DOCKBAR_UNDOCK = 16038;
	public const int ID_DOCKBAR_CLOSE = 16039;
	public const int ID_DOCKBAR_DOCK = 16040;
	#endregion

	#region 消息常量定义

	//消息常量 -------------------------------------------- 
	public const int WM_START = 0x400;    //此并非摄像头消息0x400表示的就是1024 
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

	#region DES对称加密解密
	public const string DEFAULT_ENCRYPT_KEY = "12345678";
	#endregion

	#region 连接access数据库
	public const string ACCESS_PREFIX = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};User ID=Admin;Jet OLEDB:Database Password=;";
	#endregion 

	public const int MAX_FILENAME_LEN = 256;

	/// <summary>Returns the number of processors in the system in a SYSTEM_BASIC_INFORMATION structure.</summary>
	public const int SYSTEM_BASICINFORMATION = 0;

	/// <summary>Returns an opaque SYSTEM_PERFORMANCE_INFORMATION structure.</summary>
	public const int SYSTEM_PERFORMANCEINFORMATION = 2;

	/// <summary>Returns an opaque SYSTEM_TIMEOFDAY_INFORMATION structure.</summary>
	public const int SYSTEM_TIMEINFORMATION = 3;

	/// <summary>The value returned by NtQuerySystemInformation is no error occurred.</summary>
	public const int NO_ERROR = 0;


	public const uint DFP_GET_VERSION = 0x00074080;
	public const uint DFP_SEND_DRIVE_COMMAND = 0x0007c084;
	public const uint DFP_RECEIVE_DRIVE_DATA = 0x0007c088;


      

       


	public const int SALT = 100716;    // 盐值

	#region Excel格式内容
	public const string START_EXCEL_XML = "<xml version>\r\n<Workbook " +
	                                      "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
	                                      " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
	                                      "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
	                                      "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
	                                      "office:spreadsheet\">\r\n <Styles>\r\n " +
	                                      "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
	                                      "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
	                                      "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
	                                      "\r\n <Protection/>\r\n </Style>\r\n " +
	                                      "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
	                                      "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
	                                      "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
	                                      " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
	                                      "ss:ID=\"Decimal\">\r\n <NumberFormat " +
	                                      "ss:Format=\"#,##0.###\"/>\r\n </Style>\r\n " +
	                                      "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
	                                      "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
	                                      "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
	                                      "ss:Format=\"yyyy-mm-dd;@\"/>\r\n </Style>\r\n " +
	                                      "</Styles>\r\n ";
	public const string END_EXCEL_XML = "</Workbook>";
	#endregion

	public const string CONFIG_FILTER = "配置文件(*.cfg)|*.cfg|All File(*.*)|*.*";

	#region 提示信息常量
	public const string EXCEPTION_EMPTY_STRING = "参数 '{0}'的值不能为空字符串。";
	public const string EXCEPTION_INVALID_NULL_NAME_ARGUMENT = "参数'{0}'的名称不能为空引用或空字符串。";
	public const string EXCEPTION_BYTE_ARRAY_VALUE_MUST_BE_GREATER_THAN_ZERO_BYTES = "数值必须大于0字节.";
	public const string EXCEPTION_EXPECTED_TYPE = "无效的类型，期待的类型必须为'{0}'。";
	public const string EXCEPTION_ENUMERATION_NOT_DEFINED = "{0}不是{1}的一个有效值";
	#endregion

	#region 端口号
	//最小有效端口号
	public const int MINPORT = 0;
	//最大有效端口号
	public const int MAXPORT = 65535;
	#endregion

	public const int INTERNET_OPTION_REFRESH = 0x000025;
	public const int INTERNET_OPTION_SETTINGS_CHANGED = 0x000027;

	public const int MAX_ATTACHMENT_NUM = 10;

	/// <summary>
	/// 自定义的CCalendarDate.XML的限定名称，命名空间修改，必须也跟着修改，
	/// 右键查看CCalendarData.xml文件的属性可以看到它的命名空间
	/// 修改其 生成操作:嵌入的资源;自定义工具命名空间:BB.Tools.Utils
	/// </summary>
	public const string C_CALENDAR_DATE_FILE = "BB.Tools.Utils.CCalendarData.xml";

	/// <summary>
	/// 缓存大小
	/// </summary>
	public const int BUFFSIZE = 4096;

	/// <summary>
	/// 过滤字符串
	/// </summary>
	public const string S_FILTER =
		@"首页|下载|中文|English|反馈|讨论区|投诉|建议|联系|关于|about|诚邀|工作|简介|新闻|掠影|风采
|登录|注销|注册|使用|体验|立即|收藏夹|收藏|添加|加入
|更多|more|专题|精选|热卖|热销|推荐|精彩
|加盟|联盟|友情|链接|相关
|订阅|阅读器|RSS
|免责|条款|声明|我的|我们|组织|概况|有限|免费|公司|法律|导航|广告|地图|隐私
|〖|〗|【|】|（|）|［|］|『|』|\.";

	/// <summary>
	/// language
	/// </summary>
	public const string KEY = "language";

	/// <summary>
	/// EARTH_RADIUS
	/// </summary>
	public const double EARTH_RADIUS = 6378137; //地球半径

	public const string PARAMETER = "key";

	public const string PARAMETER1 = "key";
	public const string PARAMETER2 = "value";

	/// <summary>默认缓存大小</summary>
	public const int DEFAULT_BUFFER_SIZE = 1024 * 4;

	public const int MARGIN = 4;

	public const int MAX_PAGES = 2147483647;

	#region Elements required to create an RTF document
	/* RTF HEADER
	 * ----------
	 * 
	 * \rtf[N]		- For text to be considered to be RTF, it must be enclosed in this tag.
	 *				  rtf1 is used because the RichTextBox conforms to RTF Specification
	 *				  version 1.
	 * \ansi		- The character set.
	 * \ansicpg[N]	- Specifies that unicode characters might be embedded. ansicpg1252
	 *				  is the default used by Windows.
	 * \deff[N]		- The default font. \deff0 means the default font is the first font
	 *				  found.
	 * \deflang[N]	- The default language. \deflang1033 specifies US English.
	 * */
	public const string RTF_HEADER = @"{\rtf1\ansi\ansicpg936\deff0\deflang1033\deflangfe2052";

	/* RTF DOCUMENT AREA
	 * -----------------
	 * 
	 * \viewkind[N]	- The type of view or zoom level.  \viewkind4 specifies normal view.
	 * \uc[N]		- The number of bytes corresponding to a Unicode character.
	 * \pard		- Resets to default paragraph properties
	 * \cf[N]		- Foreground color.  \cf1 refers to the color at index 1 in
	 *				  the color table
	 * \f[N]		- Font number. \f0 refers to the font at index 0 in the font
	 *				  table.
	 * \fs[N]		- Font size in half-points.
	 * */
	public const string RTF_DOCUMENT_PRE = @"\viewkind4\uc1\pard\lang2052\cf1\f0\fs20";
	public const string RTF_DOCUMENT_POST = @"\cf0\fs17}";

	#endregion

	#region Windows Messages defines
	public const Int32 EM_FORMATRANGE = WM_USER + 57;
	public const Int32 EM_GETCHARFORMAT = WM_USER + 58;
	public const Int32 EM_SETCHARFORMAT = WM_USER + 68;
	#endregion

	#region Defines for EM_SETCHARFORMAT/EM_GETCHARFORMAT
	public const Int32 SCF_SELECTION = 0x0001;
	public const Int32 SCF_WORD = 0x0002;
	public const Int32 SCF_ALL = 0x0004;
	#endregion

	#region Defines for STRUCT_CHARFORMAT member dwMask
	public const UInt32 CFM_BOLD = 0x00000001;
	public const UInt32 CFM_ITALIC = 0x00000002;
	public const UInt32 CFM_UNDERLINE = 0x00000004;
	public const UInt32 CFM_STRIKEOUT = 0x00000008;
	public const UInt32 CFM_PROTECTED = 0x00000010;
	public const UInt32 CFM_LINK = 0x00000020;
	public const UInt32 CFM_SIZE = 0x80000000;
	public const UInt32 CFM_COLOR = 0x40000000;
	public const UInt32 CFM_FACE = 0x20000000;
	public const UInt32 CFM_OFFSET = 0x10000000;
	public const UInt32 CFM_CHARSET = 0x08000000;
	#endregion

	#region Defines for STRUCT_CHARFORMAT member dwEffects
	public const UInt32 CFE_BOLD = 0x00000001;
	public const UInt32 CFE_ITALIC = 0x00000002;
	public const UInt32 CFE_UNDERLINE = 0x00000004;
	public const UInt32 CFE_STRIKEOUT = 0x00000008;
	public const UInt32 CFE_PROTECTED = 0x00000010;
	public const UInt32 CFE_LINK = 0x00000020;
	public const UInt32 CFE_AUTOCOLOR = 0x40000000;
	#endregion

	//Hide the form. 
	public const int AW_HIDE = 0x10000;
	//Activate the form. 
	public const int AW_ACTIVATE = 0x20000;

	public const Int32 EOS = -1;
	public const Int32 NEW_LINE = -2;

	public const int SM_CXSCREEN = 0;
	public const int SM_CYSCREEN = 1;
	public const int WM_NCLBUTTONDOWN = 0xA1;
	public const int HT_CAPTION = 0x2;
	public const int CURSOR_SHOWING = 0x00000001;

	public const int NUM_OF_CHARS = 256;

	public const int WS_SHOWNORMAL = 1;

	/// <summary>
	/// Turn this window into a tool window, so it doesn't show up in the Alt-tab list...
	/// </summary>
	public const int WS_EX_TOOLWINDOW = 0x00000080;
	public const int WS_EX_APPWINDOW = 0x00040000;

	public const int SE_PRIVILEGE_ENABLED = 0x00000002;
	public const int TOKEN_QUERY = 0x00000008;
	public const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
	public const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
	public const int EWX_LOGOFF = 0x00000000;
	public const int EWX_SHUTDOWN = 0x00000001;
	public const int EWX_REBOOT = 0x00000002;
	public const int EWX_FORCE = 0x00000004;
	public const int EWX_POWEROFF = 0x00000008;
	public const int EWX_FORCEIFHUNG = 0x00000010;

	public const string CUSTOM_GRID_LOOK_UP_EDIT_NAME = "CustomGridLookUpEdit";

	public const int ANIMATION_LENGTH = 300;
	public const int FRAMES_COUNT = 10;

	public const int PHOTO_WIDTH = 226;
	public const int PHOTO_HEIGHT = 280;

	public const int CARD_CODE_LEN = 10;
	public const string CARD_CODE_START = "00";

	public const string SAVE_CONFIG_NAME = "POSPrinterName";

	public const int CHECKBOX_INDENT = 4;

	#region Win32 API constants
	public const int WM_THEMECHANGED = 0x031A;

	public const int SB_HORZ = 0;
	public const int SB_VERT = 1;
	public const int SB_CTL = 2;
	public const int SB_BOTH = 3;

	public const int WM_NCCALCSIZE = 0x83;

	public const int WS_HSCROLL = 0x100000;
	public const int WS_VSCROLL = 0x200000;
	public const int GWL_STYLE = (-16);
	#endregion

	#region Win32 API Constants

	public const int WM_HSCROLL = 0x114;
	public const int WM_VSCROLL = 0x115;
	public const int SIF_RANGE = 0x1;
	public const int SIF_PAGE = 0x2;
	public const int SIF_POS = 0x4;
	public const int SIF_DISABLENOSCROLL = 0x8;
	public const int SIF_TRACKPOS = 0x10;
	public const int SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_DISABLENOSCROLL | SIF_TRACKPOS;

	#endregion

	#region 关闭显示器
	public const uint SC_MONITORPOWER = 0xF170;
	#endregion

	public const string STR_HASH = "Commons";

	/// <summary>
	/// 公钥
	/// </summary>
	public const string PUBLIC_KEY = "<RSAKeyValue><Modulus>2R0c+bMw7wc6l4AYZBknyW3teZja2e8H4tE5JY1we/Ty9ukv2UAeGvKPFn5YMMHi0UJBm7nrfU+X64OTDdrAjVBdOcrh4/ZGmQ9gwXPzIiy0RHi7YG+bWH7zUEuql02EgJuqfnxcSW9JUtzDJwUqZqcZvsE/fu8DQ6kQJBQ/e4E=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

	/// <summary>
	/// 秘钥
	/// </summary>
	public const string PRIVATE_KEY = "<RSAKeyValue><Modulus>2R0c+bMw7wc6l4AYZBknyW3teZja2e8H4tE5JY1we/Ty9ukv2UAeGvKPFn5YMMHi0UJBm7nrfU+X64OTDdrAjVBdOcrh4/ZGmQ9gwXPzIiy0RHi7YG+bWH7zUEuql02EgJuqfnxcSW9JUtzDJwUqZqcZvsE/fu8DQ6kQJBQ/e4E=</Modulus><Exponent>AQAB</Exponent><P>6DJi2FRr4s916iB1WcTjhqUs1mPZVPT0nX/uSWvot42ivQycnUs3Iu1aQIocTjm3k4zDpz5EsAz9lFQL1HfZ3w==</P><Q>717liR5iAsMjHHt0T44EIbENfzqPticpHAYRRI55WqBlQGO1ObAX90JH5n7AiQexpilJvLHApH2D7x74PaoWnw==</Q><DP>KRdxorMd/J+WUH66Bc7wLQ3iJ3a4KW7IM29GbjvojUNFf4tR1AxRj57NkAphA/722+fXYCuG4FkML0nIZitnpQ==</DP><DQ>W6piiizpaZditcCNHP4MlP5hZcx+Rkoe0w17xV4uGMd9nrfQKaRGuThXomv9vTwGCtSa2TjUxekPAh5BABRHjw==</DQ><InverseQ>OUdNRsOUSe/wSSCo/oOoJkC2i0qAB0fJ9+0B57+UKVHSJ/WUMKaGTwNKftyGeITTUDICbUZrOmLgphOnWkx6SA==</InverseQ><D>GRwUW06dlK9t19KxP3ZnUxT7F4qVmQnbjCBtbwnmHffs16CNb59KPAycftn64hyyWkhC5TtB4HrBk7PiYIJRrW+5ioCKn+8rmitpdJmTIWForNx1v6joTAhERcfCZs2niQinqr0vs5P/BiBmXKjlntW1Z3BmviYe3DlUbDzCZO8=</D></RSAKeyValue>";

	/// <summary>
	/// 重置密码默认密码
	/// </summary>
	public const string DEFAULT_PWD = "123456";

	/// <summary>
	/// 默认最小时间字符串
	/// </summary>
	public const string DEFAULT_MINIMUM_TIME_STRING = "1900-01-01";

	/// <summary>
	/// 默认最小时间
	/// </summary>
	public static DateTime DEFAULT_MINIMUM_TIME = new DateTime(1900, 1, 1);

}