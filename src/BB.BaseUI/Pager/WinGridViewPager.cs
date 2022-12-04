using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using BB.BaseUI.DocViewer;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using BB.Tools.MultiLanuage;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BB.BaseUI.Pager;

/// <summary>
/// 待分页导航的分页控件
/// </summary>
public partial class WinGridViewPager : XtraUserControl
{
    /// <summary>
    /// 是否显示CheckBox列
    /// </summary>
    public bool ShowCheckBox { get; set; }

    /// <summary>
    /// 是否导出所有页
    /// </summary>
    public bool IsExportAllPage { get; set; }

    /// <summary>
    /// 数据源
    /// </summary>
    private object _dataSource;
        
    /// <summary>
    /// 显示的列，默认按顺序排序
    /// </summary>
    private string _displayColumns = "";
        
    /// <summary>
    /// 报表标题
    /// </summary>
    private string _printTitle = "";
        
    /// <summary>
    /// 自定义字段排序
    /// </summary>
    private Dictionary<string, int> _columnDict = new();

    /// <summary>
    /// 分页信息
    /// </summary>
    private PagerInfo? _pagerInfo;
        
    /// <summary>
    /// 文件保存对话框
    /// </summary>
    private SaveFileDialog _saveFileDialog = new();
        
    /// <summary>
    /// 字段别名字典
    /// </summary>
    private readonly Dictionary<string, string> _columnNameAlias = new();
        
    /// <summary>
    /// 字段数据源字典
    /// </summary>
    private readonly Dictionary<string, List<CListItem>> _columnDataSource = new();
        
    /// <summary>
    /// 列汇总设置数据源
    /// </summary>
    private Dictionary<string, GridSummaryItem[]> _columnSummaryItemSource = new();
        
    /// <summary>
    /// 右键菜单
    /// </summary>
    private ContextMenuStrip _appendedMenu;

    /// <summary>
    /// 是否显示导出按钮
    /// </summary>
    private bool _mShowExportButton = true;
        
    /// <summary>
    /// 是否显示新建菜单
    /// </summary>
    private bool _mShowAddMenu = true;
        
    /// <summary>
    /// 是否显示编辑菜单
    /// </summary>
    private bool _mShowEditMenu = true;
        
    /// <summary>
    /// 是否显示删除菜单
    /// </summary>
    private bool _mShowDeleteMenu = true;

    #region 菜单显示文本
    /// <summary>
    /// 新建菜单的显示内容
    /// </summary>
    public string AddMenuText = "新建(&N)";
    /// <summary>
    /// 编辑菜单的显示内容
    /// </summary>
    public string EditMenuText = "编辑选定项(&E)";
    /// <summary>
    /// 删除菜单的显示内容
    /// </summary>
    public string DeleteMenuText = "删除选定项(&D)";
    /// <summary>
    /// 刷新菜单的显示内容
    /// </summary>
    public string RefreshMenuText = "刷新列表(&R)"; 
    #endregion

    /// <summary>
    /// 导出全部的数据源
    /// </summary>
    public object? AllToExport;

    /// <summary>
    /// 是否显示行号
    /// </summary>
    public bool ShowLineNumber = false;

    /// <summary>
    /// 是否显示汇总
    /// </summary>
    public bool ShowFooter = false;

    /// <summary>
    /// 获取或设置奇数行的背景色
    /// </summary>
    public Color EventRowBackColor = Color.LightCyan;

    /// <summary>
    /// 是否使用最佳宽度
    /// </summary>
    public bool BestFitColumnWith = true;

    /// <summary>
    /// 冻结列的固定样式，默认为左边
    /// </summary>
    public FixedStyle Fixed = FixedStyle.Left;

    /// <summary>
    /// 冻结列的字段，多个字段逗号分开
    /// </summary>
    public string FixedColumns { get;set;}

    #region 权限功能控制
    /// <summary>
    /// 是否显示导出按钮
    /// </summary>
    [Category("分页"), Description("是否显示导出按钮。"), Browsable(true)]
    public bool ShowExportButton
    {
        get => _mShowExportButton;
        set
        {
            _mShowExportButton = value;
            if (pager != null)
            {
                pager.ShowExportButton = ShowExportButton;
            }
        }
    }

    /// <summary>
    /// 是否显示新建菜单
    /// </summary>
    [Category("分页"), Description("是否显示新建菜单。"), Browsable(true)]
    public bool ShowAddMenu
    {
        get => _mShowAddMenu;
        set => _mShowAddMenu = value;
    }

    /// <summary>
    /// 是否显示编辑菜单
    /// </summary>
    [Category("分页"), Description("是否显示编辑菜单。"), Browsable(true)]
    public bool ShowEditMenu
    {
        get => _mShowEditMenu;
        set => _mShowEditMenu = value;
    }

    /// <summary>
    /// 是否显示删除菜单
    /// </summary>
    [Category("分页"), Description("是否显示删除菜单。"), Browsable(true)]
    public bool ShowDeleteMenu
    {
        get => _mShowDeleteMenu;
        set => _mShowDeleteMenu = value;
    } 
    #endregion

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
    /// 列的数据源字典集合
    /// </summary>
    public Dictionary<string, List<CListItem>> ColumnDataSource
    {
        get => _columnDataSource;
        set
        {
            if (value == null) return;
            foreach (string key in value.Keys)
            {
                AddColumnDataSource(key, value[key]);
            }
        }
    }

    /// <summary>
    /// 列汇总设置数据源集合
    /// </summary>
    public Dictionary<string, GridSummaryItem[]> ColumnSummaryItemSource
    {
        get => _columnSummaryItemSource;
        set => _columnSummaryItemSource = value;
    }

    #region 事件处理

    /// <summary>
    /// 导出Excel前执行的操作
    /// </summary>
    public event EventHandler OnStartExport;
    /// <summary>
    /// 导出Excel后执行的操作
    /// </summary>
    public event EventHandler OnEndExport;
    /// <summary>
    /// 页面变化的操作
    /// </summary>
    public event EventHandler OnPageChanged;
    /// <summary>
    /// 双击控件实现的操作，实现后出现右键菜单“编辑选定项”
    /// </summary>
    public event EventHandler OnEditSelected;
    /// <summary>
    /// 实现事件后出现“删除选定项”菜单项
    /// </summary>
    public event EventHandler OnDeleteSelected;
    /// <summary>
    /// 实现事件后出现“更新”菜单项
    /// </summary>
    public event EventHandler OnRefresh;
    /// <summary>
    /// 实现事件后，出现“新建”菜单项
    /// </summary>
    public event EventHandler OnAddNew;
    /// <summary>
    /// 实现对单击GirdView控件的响应
    /// </summary>
    public event EventHandler OnGridViewMouseClick;
    /// <summary>
    /// 实现对双击GirdView控件的响应
    /// </summary>
    public event EventHandler OnGridViewMouseDoubleClick;
    /// <summary>
    /// GridView的焦点行改变时触发
    /// </summary>
    public event EventHandler OnGridViewFocusedRowChanged;
    /// <summary>
    /// 实现对复选框选择变化的响应
    /// </summary>
    public event DevExpress.Data.SelectionChangedEventHandler OnCheckBoxSelectionChanged;

    #endregion

    /// <summary>
    /// 追加的菜单项目
    /// </summary>
    public ContextMenuStrip AppendedMenu
    {
        get => _appendedMenu;
        set
        {
            if (value != null)
            {
                _appendedMenu = value;
                for (int i = 0; _appendedMenu.Items.Count > 0; i++)
                {
                    contextMenuStrip1.Items.Insert(i, _appendedMenu.Items[0]);
                }
            }
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public WinGridViewPager()
    {
        InitializeComponent();
    }

    private void contextMenuStrip1_Opening(object? sender, CancelEventArgs e)
    {
        menu_Add.Visible = (OnAddNew != null && ShowAddMenu);
        menu_Delete.Visible = (OnDeleteSelected != null && ShowDeleteMenu);
        menu_Edit.Visible = (OnEditSelected != null && ShowEditMenu);
        menu_Refresh.Visible = (OnRefresh != null);
    }

    /// <summary>
    /// 封装的GridView对象
    /// </summary>
    public GridView GridView1 => gridView1;

    private void pager_PageChanged(object? sender, EventArgs e)
    {
        if (OnPageChanged != null)
        {
            OnPageChanged(this, EventArgs.Empty);
        }
    }
        
    /// <summary>
    /// 获取或设置数据源
    /// </summary>
    public object DataSource
    {
        get => _dataSource;
        set
        {
            if (gridView1.Columns != null)
            {
                gridView1.Columns.Clear();
            }

            _dataSource = value;
            gridControl1.DataSource = _dataSource;
            pager.InitPageInfo(PagerInfo.TotalRows, PagerInfo.PageSize);
        }
    }
        
    /// <summary>
    /// 取表的列数据类型字典
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    private Dictionary<string, string> GetColumnNameTypes(DataTable dt)
    {
        Dictionary<string, string> dict = new();
        foreach (DataColumn col in dt.Columns)
        {
            if (!dict.ContainsKey(col.ColumnName))
            {
                dict.Add(col.ColumnName, col.DataType.FullName);
            }
        }
        return dict;
    }

    /// <summary>
    /// 显示的列内容，需要指定以防止GridView乱序
    /// 使用"|"或者","分开每个列，如“ID|Name”
    /// </summary>
    public string DisplayColumns
    {
        get => _displayColumns;
        set
        {
            _displayColumns = value;
            _columnDict = new Dictionary<string, int>();
            string[] items = _displayColumns.Split(new[] { '|', ',' });
            for (int i = 0; i < items.Length; i++)
            {
                string str = items[i];
                if (!string.IsNullOrEmpty(str))
                {
                    str = str.Trim();
                    if (!_columnDict.ContainsKey(str))
                    {
                        _columnDict.Add(str, i);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 返回对应字段的显示顺序，如果没有，返回-1
    /// </summary>
    /// <param name="columnName">字段名 </param>
    /// <returns></returns>
    private int GetDisplayColumnIndex(string columnName)
    {
        int result = -1;
        if (_columnDict.ContainsKey(columnName))
        {
            result = _columnDict[columnName];
        }

        return result;
    }

    /// <summary>
    /// 添加列名的别名
    /// </summary>
    /// <param name="key">列的原始名称</param>
    /// <param name="alias">列的别名</param>
    public void AddColumnAlias(string key, string alias)
    {
        if (!key.IsNullOrEmpty() && !alias.IsNullOrEmpty())
        {
            if (!_columnNameAlias.ContainsKey(key))
            {
                _columnNameAlias.Add(key, alias);
            }
            else
            {
                _columnNameAlias[key] = alias;
            }
        }
    }

    public void InitDataSource<T>(PageResult<T> dataSource, string printTitle = "")
    {
        PrintTitle = printTitle;
        _pagerInfo = dataSource;
        DataSource = dataSource.Rows;
    }

    /// <summary>
    /// 添加列的数据源
    /// </summary>
    /// <param name="key">列名</param>
    /// <param name="source">数据源</param>
    public void AddColumnDataSource(string key, List<CListItem> source)
    {
        if (key.IsNullOrEmpty() || !source.Any()) return;
        _columnDataSource.AddOrUpdate(key, source);
    }

    /// <summary>
    /// 添加列的汇总数据源
    /// </summary>
    /// <param name="key">列名</param>
    /// <param name="source">数据源</param>
    public void AddColumnSummaryItem(string key, SummaryItemType summaryItemType = SummaryItemType.Count, string format = null)
    {
        if (string.IsNullOrEmpty(key)) return;

        _columnSummaryItemSource.AddOrUpdate(key, new GridSummaryItem[]
        {
            new GridColumnSummaryItem(summaryItemType, key, format ?? "{0}")
        });
    }

    /// <summary>
    /// 设置网格的汇总项
    /// </summary>
    private void SetGridSummaryItem()
    {
        gridView1.GroupSummary.Clear();
        if (!ShowFooter || _columnSummaryItemSource == null) return;
        gridView1.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
        gridView1.OptionsView.ShowFooter = true;
        gridView1.OptionsView.ShowGroupedColumns = true;
        gridView1.OptionsView.ShowGroupPanel = false;
        _columnSummaryItemSource.ForEach(x => SetGridColumnSummaryItem(x.Key, x.Value));
    }

    /// <summary>
    /// 设置列的汇总项
    /// </summary>
    /// <param name="fieldName">列名</param>
    /// <param name="summaryItemType">汇总类型</param>
    /// <param name="format">格式化</param>
    private void SetGridColumnSummaryItem(string fieldName, GridSummaryItem[] gridSummaryItem)
    {
        if (string.IsNullOrEmpty(fieldName) || gridSummaryItem.IsNull() || gridSummaryItem.Length == 0) return;
        GridColumn column = gridView1.Columns[fieldName];
        if (column == null) return;
        column.Summary.Clear();
        column.Summary.AddRange(gridSummaryItem);
    }

    /// <summary>
    /// 分页信息
    /// </summary>
    public PagerInfo PagerInfo
    {
        get
        {
            if (_pagerInfo == null)
            {
                _pagerInfo = new PagerInfo
                {
                    TotalRows = pager.RecordCount,
                    PageNo = pager.CurrentPageIndex,
                    PageSize = pager.PageSize
                };
            }
            else
            {
                _pagerInfo.PageNo = pager.CurrentPageIndex;
            }

            return _pagerInfo;
        }
    }

    /// <summary>
    /// 打印报表的抬头（标题）
    /// </summary>
    public string PrintTitle
    {
        get => _printTitle;
        set =>
            //多语言处理
            _printTitle = JsonLanguage.Default.GetString(value);
    }

    /// <summary>
    /// 导出所有记录的事件
    /// </summary>
    private void pager_ExportAll(object? sender, EventArgs e)
    {
        IsExportAllPage = true;
        ExportToExcel();
    }

    /// <summary>
    /// 导出当前页记录的事件
    /// </summary>
    private void pager_ExportCurrent(object? sender, EventArgs e)
    {
        IsExportAllPage = false;
        ExportToExcel();
    }

    #region 导出Excel操作

    private void ExportToExcel()
    {
        _saveFileDialog = new SaveFileDialog();
        _saveFileDialog.Title = JsonLanguage.Default.GetString("另存为");
        _saveFileDialog.Filter = "Excel (*.xls)|*.xls";

        if (_saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            if (!_saveFileDialog.FileName.Equals(String.Empty))
            {
                FileInfo f = new FileInfo(_saveFileDialog.FileName);
                if (f.Extension.ToLower().Equals(".xls"))
                {
                    StartExport(_saveFileDialog.FileName);
                }
                else
                {
                    "文件格式不正确".ShowUxTips();
                }
            }
            else
            {
                "需要指定一个保存的目录".ShowUxTips();
            }
        }
    }

    /// <summary>
    /// starts the export to new excel document
    /// </summary>
    /// <param name="filepath">the file to export to</param>
    private void StartExport(String filepath)
    {
        if (OnStartExport != null)
        {
            OnStartExport(this, EventArgs.Empty);
        }

        BackgroundWorker bg = new BackgroundWorker();
        bg.DoWork += bg_DoWork;
        bg.RunWorkerCompleted += bg_RunWorkerCompleted;
        bg.RunWorkerAsync(filepath);
    }

    /// <summary>
    /// 使用背景线程导出Excel文档
    /// </summary>
    private void bg_DoWork(object? sender, DoWorkEventArgs e)
    {
        DataTable table = new DataTable();
        if (AllToExport != null && IsExportAllPage)
        {
            if (AllToExport is DataView)
            {
                DataView dv = (DataView)AllToExport;//默认导出显示内容
                table = dv.ToTable();
            }
            else if (AllToExport is DataTable export)
            {
                table = export;
            }
            else
            {
                table =  ReflectionExtension.CreateTable(AllToExport);
            }

            //解析标题
            string originalName = string.Empty;
            foreach (DataColumn column in table.Columns)
            {
                originalName = column.Caption;
                if (_columnNameAlias.ContainsKey(originalName))
                {
                    var caption = _columnNameAlias[originalName];
                    //多语言处理
                    caption = JsonLanguage.Default.GetString(caption);

                    column.Caption = caption;
                    column.ColumnName = _columnNameAlias[originalName];
                }
            }
            //for (int i = 0; i < this.gridView1.Columns.Count; i++)
            //{
            //    if (!this.gridView1.Columns[i].Visible)
            //    {
            //        table.Columns.Remove(this.gridView1.Columns[i].FieldName);
            //    }
            //}
        }
        else
        {
            DataColumn column;
            DataRow row;
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                if (gridView1.Columns[i].Visible)
                {
                    column = new DataColumn(gridView1.Columns[i].FieldName, typeof(string));
                    column.Caption = gridView1.Columns[i].Caption;
                    table.Columns.Add(column);
                }
            }

            object cellValue = "";
            string fieldName = "";
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                row = table.NewRow();
                for (int j = 0; j < gridView1.Columns.Count; j++)
                {
                    if (gridView1.Columns[j].Visible)
                    {
                        fieldName = gridView1.Columns[j].FieldName;
                        cellValue = gridView1.GetRowCellValue(i, fieldName);
                        row[fieldName] = cellValue ?? "";
                    }
                }
                table.Rows.Add(row);
            }
        }

        string outError = "";
        AsposeExcelTools.DataTableToExcel2(table, (string)e.Argument, out outError);
    }

    //show a message to the user when the background worker has finished
    //and re-enable the export buttons
    private void bg_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (OnEndExport != null)
        {
            OnEndExport(this, EventArgs.Empty);
        }

        if ("导出操作完成, 您想打开该Excel文件么?".ShowYesNoAndUxTips() == DialogResult.Yes)
        {
            Process.Start(_saveFileDialog.FileName);
        }
    }
                
    #endregion

    #region 菜单操作
    private void menu_Delete_Click(object? sender, EventArgs e)
    {
        if (OnDeleteSelected != null && ShowDeleteMenu)
        {
            OnDeleteSelected(gridView1, EventArgs.Empty);
        }
    }

    private void menu_Refresh_Click(object? sender, EventArgs e)
    {
        if (OnRefresh != null)
        {
            OnRefresh(gridView1, EventArgs.Empty);
        }
    }

    private void menu_Edit_Click(object? sender, EventArgs e)
    {
        if (OnEditSelected != null && ShowEditMenu)
        {
            OnEditSelected(gridView1, EventArgs.Empty);
        }
    }

    private void menu_Print_Click(object? sender, EventArgs e)
    {
        PrintGV.Print_GridView(gridView1, _printTitle);
    }

    private void menu_Add_Click(object? sender, EventArgs e)
    {
        if (OnAddNew != null && ShowAddMenu)
        {
            OnAddNew(gridView1, EventArgs.Empty);
        }
    }

    private void menu_CopyInfo_Click(object? sender, EventArgs e)
    {
        int[] selectedRow = gridView1.GetSelectedRows();
        if (selectedRow == null || selectedRow.Length == 0) 
            return;

        StringBuilder sbHeader = new StringBuilder();
        StringBuilder sb = new StringBuilder();

        if (selectedRow.Length == 1)
        {
            //单行复制的时候
            foreach (GridColumn gridCol in gridView1.Columns)
            {
                if (gridCol.Visible)
                {
                    sbHeader.AppendFormat("{0}：{1} \r\n", gridCol.Caption, gridView1.GetRowCellDisplayText(selectedRow[0], gridCol.FieldName));
                }
            }
            sb.AppendLine();
        }
        else
        {
            //多行复制的时候
            foreach (GridColumn gridCol in gridView1.Columns)
            {
                if (gridCol.Visible)
                {
                    sbHeader.AppendFormat("{0}\t", gridCol.Caption);
                }
            }

            foreach (int row in selectedRow)
            {
                foreach (GridColumn gridCol in gridView1.Columns)
                {
                    if (gridCol.Visible)
                    {
                        sb.AppendFormat("{0}\t", gridView1.GetRowCellDisplayText(row, gridCol.FieldName));
                    }
                }
                sb.AppendLine();
            }
        }

        Clipboard.SetText(sbHeader + "\r\n" + sb);
    }

    private void menu_SetColumn_Click(object? sender, EventArgs e)
    {
        FrmSelectColumnDisplay dlg = new FrmSelectColumnDisplay();
        dlg.DisplayColumNames = _displayColumns;
        dlg.ColumnNameAlias = _columnNameAlias;
        dlg.DataGridView = gridView1;
        dlg.ShowDialog();
    }

    #endregion

    /// <summary>
    /// 数据源改变时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_DataSourceChanged(object? sender, EventArgs e)
    {
        #region 修改别名及可见
            
        var originalName = string.Empty;
        var tempColumns = string.Empty;
            
        //先判断设置显示的列(如果没有则使用别名的配置或显示全部）
        if (string.IsNullOrEmpty(DisplayColumns))
        {
            if (_columnNameAlias.Count > 0)
            {
                foreach (var columnNameAlia in _columnNameAlias)
                {
                    tempColumns += $"{columnNameAlia.Key},";
                }
            }
            else
            {
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    originalName = gridView1.Columns[i].FieldName;
                    tempColumns += $"{originalName},";
                }
            }
            tempColumns = tempColumns.Trim(',');
            DisplayColumns = tempColumns;//全部显示
        }

        //转换为大写列表
        List<string> fixedList = new List<string>();
        if (!string.IsNullOrEmpty(FixedColumns))
        {
            fixedList = FixedColumns.ToDelimitedList<string>(",");
        }

        //字段的排序顺序，先记录（使用排序的字典）
        SortedDictionary<int, string> colIndexList = new SortedDictionary<int, string>();
        foreach (GridColumn col in gridView1.Columns)
        {
            //设置列标题
            originalName = col.FieldName;
            if (_columnNameAlias.ContainsKey(originalName))
            {
                col.Caption = _columnNameAlias[originalName];
            }
            else
            {
                col.Caption = originalName;//如果没有别名用原始字段名称，如ID
            }

            //设置不显示字段
            //if (!columnDict.ContainsKey(originalName))
            //{
            //    col.Visible = false;
            //}

            //这里先记录每个字段名称，以及它的真实顺序位置
            int visibleIndex = GetDisplayColumnIndex(originalName);
            if (visibleIndex == -1)
            {
                //如果是不显示的，则设置不可见
                col.Visible = false;
            }
            else
            {
                //否则记录起来后面一并按顺序设置
                if (!colIndexList.ContainsKey(visibleIndex))
                {
                    colIndexList.Add(visibleIndex, originalName);
                }
            }
        }

        //统一设置所有可见的字段顺序
        foreach (int index in colIndexList.Keys)
        {
            originalName = colIndexList[index];
            gridView1.Columns[originalName].VisibleIndex = index;
        }

        //设置列固定（大写判断）
        for(int i = 0; i< gridView1.VisibleColumns.Count;i++)
        {
            GridColumn col = gridView1.VisibleColumns[i];
            originalName = col.FieldName;               
            if (fixedList != null && fixedList.Contains(originalName))
            {
                col.Fixed = Fixed;
            }
        }
            
        #endregion

        #region 设置特殊内容显示
            
        object cellValue = "";
        string fieldName = "";
        for (int i = 0; i < gridView1.RowCount; i++)
        {
            for (int j = 0; j < gridView1.Columns.Count; j++)
            {
                fieldName = gridView1.Columns[j].FieldName;
                cellValue = gridView1.GetRowCellValue(i, fieldName);
                if (cellValue != null && cellValue != DBNull.Value && cellValue.GetType() == typeof(DateTime))
                {
                    DateTime dtTemp = DateTime.MinValue;
                    bool flag = DateTime.TryParse(cellValue.ToString(), out dtTemp);
                    if (flag)
                    {
                        TimeSpan ts = dtTemp.Subtract(Convert.ToDateTime("1900/1/1"));
                        if (ts.TotalDays < 1)
                        {
                            gridView1.SetRowCellValue(i, fieldName, null);
                        }
                    }
                }
            }
        }
            
        #endregion

        // 显示行号
        if (ShowLineNumber)
        {
            gridView1.IndicatorWidth = 40;
        }

        // 开启自适应宽度
        gridView1.OptionsView.ColumnAutoWidth = BestFitColumnWith;
        if (BestFitColumnWith)
        {                
            gridView1.BestFitColumns();
        }

        // 显示Check列
        if (ShowCheckBox)
        {
            //GridCheckMarksSelection selection = new GridCheckMarksSelection(gridView1);
            //selection.CheckMarkColumn.VisibleIndex = 0;
            //selection.CheckMarkColumn.Width = 60;
            //selection.SelectionChanged += new SelectionChangedEventHandler(selection_SelectionChanged);
            //this.gridView1.OptionsBehavior.Editable = true;
            //this.gridView1.OptionsBehavior.ReadOnly = false;

            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.OptionsSelection.CheckBoxSelectorColumnWidth =  60;
            gridView1.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.True;
            gridView1.SelectionChanged += selection_SelectionChanged;
        }

        if (ShowFooter)
        {
            SetGridSummaryItem();
        }

        //实现多语言的处理代码(数据源变化后列的Caption要变化)
        LanguageHelper.SetGridViewColumns(gridView1);
    }

    /// <summary>
    /// 自定义列显示文本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
    {
        string columnName = e.Column.FieldName;
            
        if (_columnDataSource.Count > 0 && _columnDataSource.ContainsKey(columnName))
        {
            List<CListItem> cList = _columnDataSource[columnName];
            cList.ForEach(x =>
            {
                if (x.Value == e.Value.ObjToStr())
                {
                    e.DisplayText = x.Text;
                }
            });
        }
            
        if (e.Column.ColumnType == typeof(DateTime) || e.Column.ColumnType == typeof(DateTime?))
        {   
            if (e.Value != null)
            {
                if (e.Value == DBNull.Value || Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                {
                    e.DisplayText = "";
                }
                else
                {
                    e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm:ss");//yyyy-MM-dd HH:mm:ss.fff
                }
            }
        }
    }

    /// <summary>
    /// 网格焦点行改变时
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
    {
        if (OnGridViewFocusedRowChanged != null)
        {
            OnGridViewFocusedRowChanged(sender, e);
        }
    }
        
    void selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (OnCheckBoxSelectionChanged != null)
        {
            OnCheckBoxSelectionChanged(sender, e);
        }
    }

    /// <summary>
    /// 获取勾选上的行索引列表
    /// </summary>
    /// <returns></returns>
    public List<int> GetCheckedRows()
    {
        List<int> list = new List<int>();
        if (ShowCheckBox)
        {
            foreach (var rowIndex in gridView1.GetSelectedRows())
            {
                list.Add(rowIndex);
            }
        }
        return list;
    }

    private void dataGridView1_MouseClick(object? sender, MouseEventArgs e)
    {
        if (OnGridViewMouseClick != null)
        {
            OnGridViewMouseClick(sender, e);
        }
    }

    private void dataGridView1_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
        if (OnGridViewMouseDoubleClick != null)
        {
            OnGridViewMouseDoubleClick(gridView1, EventArgs.Empty);
        }
        else if (OnEditSelected != null && ShowEditMenu)
        {
            OnEditSelected(gridView1, EventArgs.Empty);
        }
    }

    private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
    {
        if (e.SelectedControl != gridControl1) return;

        ToolTipControlInfo info = null;
        //Get the view at the current mouse position
        GridView view = gridControl1.GetViewAt(e.ControlMousePosition) as GridView;
        if (view == null) return;

        //Get the view's element information that resides at the current position
        GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
        //Display a hint for row indicator cells
        if (hi.HitTest == GridHitTest.RowIndicator)
        {
            //An object that uniquely identifies a row indicator cell
            object o = hi.HitTest + hi.RowHandle.ToString();

            string tips = JsonLanguage.Default.GetString("行数据基本信息：");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tips);
            foreach (GridColumn gridCol in view.Columns)
            {
                if (gridCol.Visible)
                {
                    sb.AppendFormat("    {0}：{1}\r\n", gridCol.Caption, view.GetRowCellDisplayText(hi.RowHandle, gridCol.FieldName));
                }
            }
            info = new ToolTipControlInfo(o, sb.ToString());
        }

        //Supply tooltip information if applicable, otherwise preserve default tooltip (if any)
        if (info != null)
        {
            e.Info = info;
        }
    }

    /// <summary>
    /// 自定义行号显示
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
    {
        if (ShowLineNumber)
        {
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
        }
    }

    private void WinGridViewPager_Load(object? sender, EventArgs e)
    {
        if (!DesignMode)
        {
            pager.PageChanged += pager_PageChanged;
            pager.ExportCurrent += pager_ExportCurrent;
            pager.ExportAll += pager_ExportAll;

            // // 许可校验
            // LicenseCheckResult result = LicenseTool.CheckLicense();
            // if (result.IsValided)
            // {
            //         
            // }
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            gridControl1.MouseClick += dataGridView1_MouseClick;
            gridControl1.MouseDoubleClick += dataGridView1_MouseDoubleClick;

            gridView1.Appearance.EvenRow.BackColor = EventRowBackColor;
            gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;

            //设置菜单的别名
            menu_Add.Text = AddMenuText;
            menu_Edit.Text = EditMenuText;
            menu_Delete.Text = DeleteMenuText;
            menu_Refresh.Text = RefreshMenuText;

            //实现多语言的处理代码
            LanguageHelper.InitLanguage(this);
        }
    }

    private void menu_ColumnWidth_Click(object? sender, EventArgs e)
    {
        BestFitColumnWith = !BestFitColumnWith;
        ShowWidthStatus();
        if (OnRefresh != null)
        {
            OnRefresh(sender, e);
        }
    }

    private void ShowWidthStatus()
    {
        if (BestFitColumnWith)
        {
            menu_ColumnWidth.Text = JsonLanguage.Default.GetString("设置列固定宽度(&W)");
        }
        else
        {
            menu_ColumnWidth.Text = JsonLanguage.Default.GetString("设置列自动适应宽度(&W)");
        }
    }
}