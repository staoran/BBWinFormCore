using BB.BaseUI.BaseUI;

namespace BB.BaseUI.Other;

/// <summary>
/// 询问一个输入字符串的窗体
/// </summary>
public partial class QueryInputDialog : BaseForm
{
    /// <summary>
    /// 是否需要对输入框进行屏蔽显示（用于密码信息）
    /// </summary>
    public bool IsEncryptInput { get; set; }

    /// <summary>
    /// 构造器
    /// </summary>
    public QueryInputDialog()
    {
        InitializeComponent();
    }

    private void QueryInputDialog_Load(object? sender, EventArgs e)
    {
        if (IsEncryptInput)
        {
            txtInput.Properties.PasswordChar = '*';
            txtInput.Properties.UseSystemPasswordChar = true;
        }
    }
}