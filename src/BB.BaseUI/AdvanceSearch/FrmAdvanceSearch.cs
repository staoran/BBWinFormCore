using System.Collections.Specialized;
using System.Data;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.Tools.Entity;

namespace BB.BaseUI.AdvanceSearch;

/// <summary>
/// 高级查询处理
/// </summary>
public partial class FrmAdvanceSearch : BaseForm
{
    public delegate void ConditionChangedEventHandler(NameValueCollection condition);

    /// <summary>
    /// 查询条件触发的事件
    /// </summary>
    public event ConditionChangedEventHandler ConditionChanged;
    /// <summary>
    /// 清除数据触发事件
    /// </summary>
    public event EventHandler DataClear;    

    #region 字段设置
        
    private DataTable _dtFieldTypeTable;//用于高级查询用途的表字段名称、类型列表
    private string _displayColumns = "";
    private readonly Dictionary<string, string> _columnNameAlias = new();//字段别名字典集合
    private readonly Dictionary<string, List<CListItem>> _listItemDict = new();
    private DataTable _dtAdvance;

    /// <summary>
    /// 用于高级查询用途的表字段名称、类型列表
    /// </summary>
    public DataTable FieldTypeTable
    {
        get => _dtFieldTypeTable;
        set => _dtFieldTypeTable = value;
    }

    /// <summary>
    /// 列名的别名字典集合
    /// </summary>
    public Dictionary<string, string> ColumnNameAlias
    {
        get => _columnNameAlias;
        set
        {
            if (value != null)
            {
                foreach (string key in value.Keys)
                {
                    AddColumnAlias(key, value[key]);
                }
            }
        }
    }

    /// <summary>
    /// 添加列名的别名（显示名称）
    /// </summary>
    /// <param name="key">列的原始名称</param>
    /// <param name="alias">列的别名（显示名称）</param>
    public void AddColumnAlias(string key, string alias)
    {
        if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(alias))
        {
            if (!_columnNameAlias.ContainsKey(key.ToUpper()))
            {
                _columnNameAlias.Add(key.ToUpper(), alias);
            }
            else
            {
                _columnNameAlias[key.ToUpper()] = alias;
            }
        }
    }

    /// <summary>
    /// 为指定列设置下拉列表默认值
    /// </summary>
    /// <param name="key">列的原始名称</param>
    /// <param name="listItems">列的别名列表集合</param>
    public void AddColumnListItem(string key, ICollection<CListItem> listItems)
    {
        if (!string.IsNullOrEmpty(key) && listItems != null)
        {
            if (!_listItemDict.ContainsKey(key.ToUpper()))
            {
                List<CListItem> list = new List<CListItem>();
                if (listItems.Any())
                {
                    list.AddRange(listItems);
                }
                _listItemDict.Add(key.ToUpper(), list);
            }
        }
    }

    /// <summary>
    /// 为指定列设置下拉列表默认值
    /// </summary>
    /// <param name="key">列的原始名称</param>
    /// <param name="listItems">列的别名列表集合</param>
    public void AddColumnListItem(string key, List<CListItem> listItems)
    {
        if (!string.IsNullOrEmpty(key) && listItems != null)
        {
            if (!_listItemDict.ContainsKey(key.ToUpper()))
            {
                _listItemDict.Add(key.ToUpper(), listItems);
            }
        }
    }

    /// <summary>
    /// 为指定列设置下拉列表默认值
    /// </summary>
    /// <param name="key">列的原始名称</param>
    /// <param name="listItems">列的别名列表集合</param>
    public void AddColumnListItem(string key, List<string> listItems)
    {
        if (!string.IsNullOrEmpty(key) && listItems != null)
        {
            if (!_listItemDict.ContainsKey(key.ToUpper()))
            {
                List<CListItem> list = new();
                foreach (string item in listItems)
                {
                    list.Add(new CListItem(item));
                }
                _listItemDict.Add(key.ToUpper(), list);
            }
        }
    }

    /// <summary>
    /// 为指定列设置下拉列表默认值
    /// </summary>
    /// <param name="key">列的原始名称</param>
    /// <param name="listItems">列的别名列表集合，字符串格式，使用"|"或者","分开每个列，如“ID,Name”</param>
    public void AddColumnListItem(string key, string listItems)
    {
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(listItems)) return;
        if (_listItemDict.ContainsKey(key.ToUpper())) return;
        List<CListItem> list = new();
        string[] items = listItems.Split('|', ',');
        foreach (string t in items)
        {
            string str = t;
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim();
                if (!string.IsNullOrEmpty(str))
                {
                    list.Add(new CListItem(str));
                }
            }
        }
        _listItemDict.Add(key.ToUpper(), list);
    }

    #endregion

    /// <summary>
    /// 构造函数
    /// </summary>
    public FrmAdvanceSearch()
    {
        InitializeComponent();
        CreateGridView();
    }

    /// <summary>
    /// 处理数据查询后的事件触发
    /// </summary>
    public virtual void ProcessDataSearch(object? sender, EventArgs e)
    {
        if (_dtAdvance == null) return;
        var searchCondition = new NameValueCollection();
        foreach (DataRow row in _dtAdvance.Rows)
        {
            var fieldName = row["字段"].ToString();
            // var fieldDisplay = row["字段名称"].ToString();
            var fieldType = (FieldType)row["字段类型"];
            var fieldValue = row["查询条件值"].ToString();
            // var valueDisplay = row["查询条件显示"].ToString();

            switch (fieldType)
            {
                case FieldType.DateTime:
                case FieldType.Numeric:
                    searchCondition.Add(fieldName, fieldValue.Replace('~', ','));
                    break;
                case FieldType.Text:
                case FieldType.DropdownList:
                default:
                    searchCondition.Add(fieldName, fieldValue);
                    break;
            }
        }

        ConditionChanged?.Invoke(searchCondition);
    }

    /// <summary>
    /// 数据清除后的操作
    /// </summary>
    public virtual void ProcessDataClear(object? sender, EventArgs e)
    {
        if(DataClear != null)
        {
            ProcessDataSearch(sender, e);

            DataClear(sender, e);
        }
    }

    private void btnOK_Click(object? sender, EventArgs e)
    {
        ProcessDataSearch(null, null);
    }

    private void FrmAdvanceSearch_Load(object? sender, EventArgs e)
    {
        BindData();
    }

    private void CreateGridView()
    {
        gridView1.Columns.Clear();
        gridView1.CreateColumn("字段", "字段");
        gridView1.CreateColumn("字段名称", "字段名称");
        gridView1.CreateColumn("字段类型", "字段类型");
        gridView1.CreateColumn("查询条件值", "查询条件值");
        gridView1.CreateColumn("查询条件显示", "查询条件显示");
    }

    private void BindData()
    {
        //第一次创建对象内容，后面只需要更新界面即可
        if (_dtAdvance == null)
        {
            #region 首次创建
            _dtAdvance = new DataTable();
            _dtAdvance.Columns.Add("字段");
            _dtAdvance.Columns.Add("字段名称");
            _dtAdvance.Columns.Add("字段类型", typeof(FieldType));
            _dtAdvance.Columns.Add("查询条件值");
            _dtAdvance.Columns.Add("查询条件显示");

            FieldType customedType = FieldType.Text;
            foreach (DataRow dr in _dtFieldTypeTable.Rows)
            {
                #region 转换字段显示名称
                string originalName = dr["ColumnName"].ToString();
                string columnName = originalName;

                if (_columnNameAlias.ContainsKey(columnName.ToUpper()))
                    columnName = _columnNameAlias[columnName.ToUpper()];
                else
                    continue;
                    
                #endregion

                #region 转换数据内容
                string dataType = dr["DataType"].ToString();
                switch (dataType)
                {
                    case "system.byte[]"://跳过一些不需查询的字段类型                            
                        continue;

                    case "system.string":
                    case "system.guid":
                    case "system.char":
                    case "system.boolean":
                        customedType = FieldType.Text;
                        break;

                    case "system.int16":
                    case "system.int32":
                    case "system.int64":
                    case "system.uint16":
                    case "system.uint32":
                    case "system.uint64":
                    case "system.single":
                    case "system.decimal":
                    case "system.double":
                    case "system.float":
                    case "system.byte":
                        customedType = FieldType.Numeric;
                        break;
                    case "system.datetime":
                        customedType = FieldType.DateTime; //需要大写
                        break;
                }
                #endregion

                //特殊转换
                if (_listItemDict.ContainsKey(originalName))
                {
                    customedType = FieldType.DropdownList;
                }

                DataRow row = _dtAdvance.NewRow();
                row["字段"] = originalName;
                row["字段类型"] = customedType;
                row["字段名称"] = columnName;
                _dtAdvance.Rows.Add(row);
            } 
            #endregion
        }
        gridControl1.DataSource = _dtAdvance;
    }

    /// <summary>
    /// 更新查询字段值，并重新绑定界面
    /// </summary>
    /// <param name="fieldName">字段名称</param>
    /// <param name="fieldValue">字段值</param>
    private void UpdateFieldValue(string fieldName, string fieldValue, string valueDisplay)
    {
        if (_dtAdvance != null && !string.IsNullOrEmpty(fieldName))
        {
            foreach (DataRow row in _dtAdvance.Rows)
            {
                string name = row["字段"].ToString();
                if (fieldName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    row["查询条件值"] = fieldValue;
                    row["查询条件值"] = fieldValue;
                    row["查询条件显示"] = valueDisplay;
                }
            }
            BindData();
        }
    }

    void dlg_DataClear(string fieldName)
    {
        UpdateFieldValue(fieldName, "", "");

        //更新父窗体的数据显示
        ProcessDataSearch(null, null);
    }

    private void ctxMenuSetData_Click(object? sender, EventArgs e)
    {
        gridControl1_MouseDoubleClick(null, null);
    }

    private void ctxMenuClearData_Click(object? sender, EventArgs e)
    {
        int[] rowSelected = gridView1.GetSelectedRows();
        if (rowSelected.Length == 0) return;

        string fieldName = gridView1.GetFocusedRowCellValue("字段").ToString();
        if(!string.IsNullOrEmpty(fieldName))
        {
            dlg_DataClear(fieldName);
        }
    }

    private void gridControl1_DataSourceChanged(object? sender, EventArgs e)
    {
        if (gridView1.Columns.Count > 0)
        {
            gridView1.Columns["字段"].Visible = false;
            gridView1.Columns["字段类型"].Visible = false;
            gridView1.Columns["查询条件值"].Visible = false;

            gridView1.Columns["字段名称"].Width = 100;
            gridView1.Columns["查询条件显示"].Width = 200;
        }
    }

    private void gridControl1_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
        int[] rowSelected = gridView1.GetSelectedRows();
        if (rowSelected.Length == 0) return;

        string fieldName = gridView1.GetFocusedRowCellValue("字段").ToString();
        string fieldDisplay = gridView1.GetFocusedRowCellValue("字段名称").ToString();
        FieldType fieldType = (FieldType)gridView1.GetFocusedRowCellValue("字段类型");
        string fieldValue = gridView1.GetFocusedRowCellValue("查询条件值").ToString();

        #region 根据类型转换不同的窗体
        FrmQueryBase dlg;
        switch (fieldType)
        {
            case FieldType.Text:
                dlg = new FrmQueryTextEdit();
                break;
            case FieldType.Numeric:
                dlg = new FrmQueryNumericEdit();
                break;
            case FieldType.DateTime:
                dlg = new FrmQueryDateEdit();
                break;
            case FieldType.DropdownList:
                dlg = new FrmQueryDropdown();
                break;
            default:
                dlg = new FrmQueryTextEdit();
                break;
        }
        #endregion

        dlg.FieldName = fieldName;
        dlg.FieldDisplayName = fieldDisplay;
        dlg.FieldDefaultValue = fieldValue;
        if (_listItemDict.ContainsKey(fieldName.ToUpper()))
        {
            dlg.DropDownItems = _listItemDict[fieldName.ToUpper()];
        }

        dlg.DataClear += dlg_DataClear;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            //更新查询界面显示
            UpdateFieldValue(fieldName, dlg.ReturnValue, dlg.ReturnDisplay);

            //更新父窗体的数据显示
            ProcessDataSearch(null, null);
        }
    }

    private void gridControl1_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            gridControl1_MouseDoubleClick(null, null);
        }
    }
}