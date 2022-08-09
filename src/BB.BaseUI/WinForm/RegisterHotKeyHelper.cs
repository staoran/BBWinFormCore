using System.Runtime.InteropServices;

namespace BB.BaseUI.WinForm;

/// <summary>
/// 实现系统全局热键注册辅助类
/// </summary>
public class RegisterHotKeyHelper
{
    private IntPtr _mWindowHandle = IntPtr.Zero;
    private Modkey _mModKey = 0;
    private Keys _mKeys = Keys.A;
    private int _mWParam = 10000;
    private bool _star = false;
    private HotKeyWndProc _mHotKeyWnd = new HotKeyWndProc();

    /// <summary>
    /// 主窗体句柄
    /// </summary>
    public IntPtr WindowHandle
    {
        get => _mWindowHandle;
        set { if (_star)return; _mWindowHandle = value; }
    }

    /// <summary>
    /// 系统控制键
    /// </summary>
    public Modkey ModKey
    {
        get => _mModKey;
        set { if (_star)return; _mModKey = value; }
    }

    /// <summary>
    /// 系统支持的键
    /// </summary>
    public Keys Keys
    {
        get => _mKeys;
        set { if (_star)return; _mKeys = value; }
    }

    /// <summary>
    /// 定义热键的参数,建议从10000开始
    /// </summary>
    public int WParam
    {
        get => _mWParam;
        set { if (_star)return; _mWParam = value; }
    }

    /// <summary>
    /// 开始注册系统全局热键
    /// </summary>
    public void StarHotKey()
    {
        if (_mWindowHandle != IntPtr.Zero)
        {
            if (!RegisterHotKey(_mWindowHandle, _mWParam, _mModKey, _mKeys))
            {
                throw new Exception("注册热键失败");
            }
            try
            {
                _mHotKeyWnd.MHotKeyPass = KeyPass;
                _mHotKeyWnd.MWParam = _mWParam;
                _mHotKeyWnd.AssignHandle(_mWindowHandle);
                _star = true;
            }
            catch
            {
                StopHotKey();
            }
        }
    }

    private void KeyPass()
    {
        if (HotKey != null) HotKey();
    }

    /// <summary>
    /// 取消系统全局热键
    /// </summary>
    public void StopHotKey()
    {
        if (_star)
        {
            if (!UnregisterHotKey(_mWindowHandle, _mWParam))
            {
                throw new Exception("取消热键失败");
            }
            _star = false;
            _mHotKeyWnd.ReleaseHandle();
        }
    }

    /// <summary>
    /// 热键处理代理定义
    /// </summary>
    public delegate void HotKeyPass();

    /// <summary>
    /// 热键处理事件
    /// </summary>
    public event HotKeyPass HotKey;

    private class HotKeyWndProc : NativeWindow
    {
        public int MWParam = 10000;
        public HotKeyPass MHotKeyPass;
        protected override void WndProc(ref Message m)
        {
            //0x0312 热键消息
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == MWParam)
            {
                if (MHotKeyPass != null) MHotKeyPass.Invoke();
            }

            base.WndProc(ref m);
        }
    }

    /// <summary>
    /// 控制键枚举
    /// </summary>
    public enum Modkey
    {
        /// <summary>
        /// Alt控制键
        /// </summary>
        ModAlt = 0x0001,
            
        /// <summary>
        /// Ctrl控制键
        /// </summary>
        ModControl = 0x0002,

        /// <summary>
        /// Shift控制键
        /// </summary>
        ModShift = 0x0004,

        /// <summary>
        /// Windows键
        /// </summary>
        ModWin = 0x0008,
    }

    /// <summary>
    /// 注册热键
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool RegisterHotKey(IntPtr wnd, int id, Modkey mode, Keys vk);

    /// <summary>
    /// 取消热键
    /// </summary>
    [DllImport("user32.dll")]
    public static extern bool UnregisterHotKey(IntPtr wnd, int id);
}