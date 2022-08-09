using BB.BaseUI.Other;
using BB.Tools.Entity;

namespace BB.BaseUI.AdvanceSearch;

internal partial class FrmQueryBase : DevExpress.XtraEditors.XtraForm
{
    public delegate void DataClearEventHandler(string fieldName);

    /// <summary>
    /// 字段名称
    /// </summary>
    public string FieldName = "";
    /// <summary>
    /// 字段显示名称
    /// </summary>
    public string FieldDisplayName = "查询字段";
    /// <summary>
    /// 字段默认值，多个值使用 ~ 分开
    /// </summary>
    public string FieldDefaultValue = "";
    /// <summary>
    /// 下拉列表的绑定数据
    /// </summary>
    public List<CListItem> DropDownItems = null;
    /// <summary>
    /// 返回的字段查询内容值
    /// </summary>
    public string ReturnValue = "";
    /// <summary>
    /// 返回的字段查询内容显示内容
    /// </summary>
    public string ReturnDisplay = "";

    public event DataClearEventHandler DataClear;//子窗体数据清除触发
    public event EventHandler DataSearch;//子窗体数据查询触发

    public FrmQueryBase()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 处理数据清除后的事件触发
    /// </summary>
    public virtual void ProcessDataClear(string fieldName)
    {
        if (DataClear != null)
        {
            DataClear(fieldName);
        }
    }

    /// <summary>
    /// 处理数据查询后的事件触发
    /// </summary>
    public virtual void ProcessDataSearch(object sender, EventArgs e)
    {
        if (DataSearch != null)
        {
            DataSearch(sender, e);
        }
    }

    private void FrmQueryBase_Load(object sender, EventArgs e)
    {
        if(!DesignMode)
        {
            LanguageHelper.InitLanguage(this);
        }
    }

    private void FrmQueryBase_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            Close();
        }
    }    
}