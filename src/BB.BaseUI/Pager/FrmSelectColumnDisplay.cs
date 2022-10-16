using BB.BaseUI.Other;
using BB.Tools.Entity;
using BB.Tools.MultiLanuage;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace BB.BaseUI.Pager;

/// <summary>
/// 设置可见列
/// </summary>
public partial class FrmSelectColumnDisplay : XtraForm
{
    private CheckedListBox _mCheckedListBox;
    private GridView _mDataGridView;

    /// <summary>
    /// 显示数据的DataGridView对象
    /// </summary>
    public GridView DataGridView
    {
        get => _mDataGridView;
        set => _mDataGridView = value;
    }

    /// <summary>
    /// 显示的列名称（按顺序排列）
    /// </summary>
    public string DisplayColumNames
    {
        get;
        set;
    }

    /// <summary>
    /// 别名对照字典
    /// </summary>
    public Dictionary<string, string> ColumnNameAlias
    {
        get;
        set;
    }

    private int _maxHeight = 300;
    private int _displayWidth = 200;
    private bool _isSelectAll = false;

    /// <summary>
    /// 构造函数
    /// </summary>
    public FrmSelectColumnDisplay()
    {
        InitializeComponent();
    }

    /// <summary>
    /// 获取真正的列名称
    /// </summary>
    /// <param name="columnName">可能大小写不一样的列名</param>
    /// <returns></returns>
    private string GetRightColumnName(string columnName)
    {
        foreach (GridColumn c in _mDataGridView.Columns)
        {
            if (c.FieldName.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                return c.FieldName;
        }
        return "";
    }

    private string GetColumnNameAlias(string name)
    {
        if (ColumnNameAlias.ContainsKey(name.ToUpper()))
        {
            var alisName = ColumnNameAlias[name.ToUpper()];
            alisName = JsonLanguage.Default.GetString(alisName);
            return alisName;
        }
        else
        {
            return name;
        }
    }

    private void Init()
    {
        _mCheckedListBox = new CheckedListBox();
        _mCheckedListBox.CheckOnClick = true;
        _mCheckedListBox.ItemCheck += mCheckedListBox_ItemCheck;

        _mCheckedListBox.Items.Clear();
        foreach (string columnName in DisplayColumNames.Split(new[] { '|', ',' }))
        {
            string newName = GetRightColumnName(columnName);
            if (!string.IsNullOrEmpty(newName))
            {
                _mCheckedListBox.Items.Add(new CListItem(GetColumnNameAlias(newName), newName), true);
            }
        }

        int preferredHeight = (_mCheckedListBox.Items.Count * 16) + 7;
        _mCheckedListBox.Height = (preferredHeight < _maxHeight) ? preferredHeight : _maxHeight;
        _mCheckedListBox.Width = _displayWidth;
        _mCheckedListBox.Dock = DockStyle.Fill;

        panel1.Controls.Clear();
        panel1.Controls.Add(_mCheckedListBox);
    }

    void mCheckedListBox_ItemCheck(object? sender, ItemCheckEventArgs e)
    {
        if (_mCheckedListBox.Items[e.Index] is CListItem item)
        {
            _mDataGridView.Columns[item.Value].Visible = (e.NewValue == CheckState.Checked);
        }
        //mDataGridView.Columns[e.Index].Visible = (e.NewValue == CheckState.Checked);
    }

    private void FrmSelectColumnDisplay_Load(object? sender, EventArgs e)
    {
        Init();
            
    }

    private void chkSelectAll_CheckedChanged(object? sender, EventArgs e)
    {
        _isSelectAll = chkSelectAll.Checked;
        for (int i = 0; i < _mCheckedListBox.Items.Count; i++)
        {
            _mCheckedListBox.SetItemChecked(i, _isSelectAll);
        }
    }

    private void chkInverse_CheckedChanged(object? sender, EventArgs e)
    {
        for (int i = 0; i < _mCheckedListBox.Items.Count; i++)
        {
            _mCheckedListBox.SetItemChecked(i, !_mCheckedListBox.GetItemChecked(i));
        }
    }

    private void FrmSelectColumnDisplay_Shown(object? sender, EventArgs e)
    {
        if(!DesignMode)
        {
            LanguageHelper.InitLanguage(this);
        }
    }
}