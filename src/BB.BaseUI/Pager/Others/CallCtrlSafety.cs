namespace BB.BaseUI.Pager.Others;

internal class CallCtrlSafety
{
    #region 跨线程的控件安全访问方式

    delegate void SetTextCallback(System.Windows.Forms.Control objCtrl, string text, Form winf);
    delegate void SetEnableCallback(System.Windows.Forms.Control objCtrl, bool enable, Form winf);
    delegate void SetFocusCallback(System.Windows.Forms.Control objCtrl, Form winf);
    delegate void SetCheckedCallback(CheckBox objCtrl, bool isCheck, Form winf);
    delegate void SetVisibleCallback(System.Windows.Forms.Control objCtrl, bool isVisible, Form winf);
    delegate void SetValueCallback(ProgressBar objCtrl, int value, Form winf);

    public static void SetText<TObject>(TObject objCtrl, string text, Form winf) where TObject : System.Windows.Forms.Control
    {
        if (objCtrl.InvokeRequired)
        {
            SetTextCallback d = SetText;
            if (winf.IsDisposed)
            {
                return;
            }
            winf.Invoke(d, new object[] { objCtrl, text, winf });
        }
        else
        {
            objCtrl.Text = text;
        }
    }
    public static void SetEnable<TObject>(TObject objCtrl, bool enable, Form winf) where TObject : System.Windows.Forms.Control
    {
        if (objCtrl.InvokeRequired)
        {
            SetEnableCallback d = SetEnable;
            if (winf.IsDisposed)
            {
                return;
            }
            winf.Invoke(d, new object[] { objCtrl, enable, winf });
        }
        else
        {
            objCtrl.Enabled = enable;
        }
    }
    public static void SetFocus<TObject>(TObject objCtrl, Form winf) where TObject : System.Windows.Forms.Control
    {
        if (objCtrl.InvokeRequired)
        {
            SetFocusCallback d = SetFocus;
            if (winf.IsDisposed)
            {
                return;
            }
            winf.Invoke(d, new object[] { objCtrl, winf });
        }
        else
        {
            objCtrl.Focus();
        }
    }
    public static void SetChecked<TObject>(TObject objCtrl, bool isChecked, Form winf) where TObject : CheckBox
    {
        if (objCtrl.InvokeRequired)
        {
            SetCheckedCallback d = SetChecked;
            if (winf.IsDisposed)
            {
                return;
            }
            winf.Invoke(d, new object[] { objCtrl, isChecked, winf });
        }
        else
        {
            objCtrl.Checked = isChecked;
        }
    }
    public static void SetVisible<TObject>(TObject objCtrl, bool isVisible, Form winf) where TObject : System.Windows.Forms.Control
    {
        if (objCtrl.InvokeRequired)
        {
            SetVisibleCallback d = SetVisible;
            if (winf.IsDisposed)
            {
                return;
            }
            winf.Invoke(d, new object[] { objCtrl, isVisible, winf });
        }
        else
        {
            objCtrl.Visible = isVisible;
        }
    }
    public static void SetValue<TObject>(TObject objCtrl, int value, Form winf) where TObject : ProgressBar
    {
        if (objCtrl.InvokeRequired)
        {
            SetValueCallback d = SetValue;
            if (winf.IsDisposed)
            {
                return;
            }
            winf.Invoke(d, new object[] { objCtrl, value, winf });
        }
        else
        {
            objCtrl.Value = value;
        }
    }
    #endregion
}