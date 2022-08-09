using DevExpress.XtraEditors;

namespace BB.BaseUI.Control;

/// <summary>
/// 展示姓名控件
/// </summary>
public partial class NameControl : XtraUserControl
{
    public delegate void DeleteEventHandler(string id);
    public event DeleteEventHandler OnDeleteItem;

    /// <summary>
    /// 构造函数
    /// </summary>
    public NameControl()
    {
        InitializeComponent();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (OnDeleteItem != null)
        {
            if (lblInfo.Tag != null)
            {
                OnDeleteItem(lblInfo.Tag.ToString());
            }
        }
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public void BindData(string id, string name)
    {
        lblInfo.Text = name;
        lblInfo.Tag = id;

        btnDelete.Tag = id;
    }
}