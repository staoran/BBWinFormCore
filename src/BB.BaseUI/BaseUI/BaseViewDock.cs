using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using BB.BaseUI.AdvanceSearch;
using BB.BaseUI.Extension;
using BB.Entity.Base;
using BB.Entity.TMS;
using BB.HttpServices.Base;
using BB.Tools.Entity;
using BB.Tools.Format;
using BB.Tools.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Furion;
using BB.Tools.Extension;
using Furion.FriendlyException;
using Mapster;

namespace BB.BaseUI.BaseUI;

public partial class BaseViewDock<T, IT, TE> : BaseDock
    where T : BaseEntity
    where IT : BaseHttpService<T>
    where TE : BaseForm
{
    protected string moduleName;
    
    protected readonly IT _bll;
    protected readonly LazilyResolved<TE> _baseForm;
    protected BarButtonItem addButton;
    protected BarButtonItem editButton;
    protected BarButtonItem checkButton;
    protected BarButtonItem importButton;
    protected BarButtonItem queryButton;
    protected BarButtonItem clearButton;
    protected BarButtonItem advQueryButton;
    protected BarButtonItem exportButton;
    protected BarToggleSwitchItem hideTreeButton;
    protected BarToggleSwitchItem tableDirectionButton;
    protected BarButtonItem closeButton;

    /// <summary>
    /// 高级查询条件语句对象
    /// </summary>
    protected NameValueCollection? _advanceCondition;

    /// <summary>
    /// 快捷树查询条件语句对象
    /// </summary>
    protected NameValueCollection? _treeCondition;

    public BaseViewDock(IT bll, LazilyResolved<TE> baseForm)
    {
        InitializeComponent();

        _bll = bll;
        _baseForm = baseForm;

        Shown += View_Shown;
    }

    /// <summary>
    /// 编写初始化窗体的实现
    /// </summary>
    public override Task FormOnLoad()
    {
        #region 网格事件和配置

        winGridViewPager1.OnPageChanged += winGridViewPager1_OnPageChanged;
        winGridViewPager1.OnStartExport += winGridViewPager1_OnStartExport;
        winGridViewPager1.OnEditSelected += winGridViewPager1_OnEditSelected;
        winGridViewPager1.OnAddNew += winGridViewPager1_OnAddNew;
        winGridViewPager1.OnDeleteSelected += winGridViewPager1_OnDeleteSelected;
        winGridViewPager1.OnRefresh += winGridViewPager1_OnRefresh;
        winGridViewPager1.AppendedMenu = contextMenuStrip1;
        winGridViewPager1.ShowLineNumber = true;
        winGridViewPager1.BestFitColumnWith = false; //是否设置为自动调整宽度，false为不设置
        winGridViewPager1.gridView1.DataSourceChanged += gridView1_DataSourceChanged;
        winGridViewPager1.gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
        winGridViewPager1.gridView1.RowCellStyle += gridView1_RowCellStyle;

        #endregion

        #region 查询表单

        //关联回车键进行查询
        foreach (System.Windows.Forms.Control control in layoutControl1.Controls)
        {
            control.KeyUp += SearchControl_KeyUp;
        }

        #endregion

        #region 按钮和按钮权限

        addButton = bar1.AddBarButtonItem("Add", "新增(&N)", "add", btnAddNew_Click, new BarShortcut(Keys.Alt | Keys.N));
        editButton = bar1.AddBarButtonItem("Edit", "修改", "edit", winGridViewPager1_OnEditSelected, null, true);
        checkButton = bar1.AddBarButtonItem("Check", "审核", "task", btnCheck_Click, null, true);
        importButton = bar1.AddBarButtonItem("Import", "导入", "inbox", btnImport_Click, null, true);
        queryButton = bar1.AddBarButtonItem("Query", "查询(&Q)", "functionslookupandreference", btnSearch_Click,
            new BarShortcut(Keys.Alt | Keys.Q), false, BarItemLinkAlignment.Right);
        clearButton = bar1.AddBarButtonItem("Clear", "清空(&C)", "clearall", btnClear_Click,
            new BarShortcut(Keys.Alt | Keys.C), false, BarItemLinkAlignment.Right);
        advQueryButton = bar1.AddBarButtonItem("AdvQuery", "高级查询", "filter", btnAdvanceSearch_Click, null, false,
            BarItemLinkAlignment.Right);
        exportButton = bar1.AddBarButtonItem("Export", "导出", "outbox", btnExport_Click, null, true,
            BarItemLinkAlignment.Right);
        hideTreeButton = bar1.AddBarSwitchItem("HideTree", "隐藏快查", "left", chkHideTree_CheckedChanged, null, true,
            BarItemLinkAlignment.Right);
        tableDirectionButton = bar1.AddBarSwitchItem("TableDirection", "列表横向布局", "chartswitchrowcolumn",
            chkTableDirection_CheckedChanged, null, true,
            BarItemLinkAlignment.Right);
        clearButton =
            bar1.AddBarButtonItem("Close", "关闭", "close", btnClose_Click, null, true, BarItemLinkAlignment.Right);

        addButton.Enabled = HasFunction($"{Name}/Add");
        editButton.Enabled = HasFunction($"{Name}/Edit");
        checkButton.Enabled = HasFunction($"{Name}/Check");
        importButton.Enabled = HasFunction($"{Name}/Import");
        queryButton.Enabled = HasFunction($"{Name}/Query");
        clearButton.Enabled = HasFunction($"{Name}/Clear");
        advQueryButton.Enabled = HasFunction($"{Name}/AdvQuery");
        exportButton.Enabled = HasFunction($"{Name}/Export");
        addButton.Visibility = BarItemVisibility.Never;
        editButton.Visibility = BarItemVisibility.Never;
        checkButton.Visibility = BarItemVisibility.Never;
        importButton.Visibility = BarItemVisibility.Never;
        queryButton.Visibility = BarItemVisibility.Never;
        clearButton.Visibility = BarItemVisibility.Never;
        advQueryButton.Visibility = BarItemVisibility.Never;
        exportButton.Visibility = BarItemVisibility.Never;
        hideTreeButton.Visibility = BarItemVisibility.Never;
        tableDirectionButton.Visibility = BarItemVisibility.Never;

        #endregion
        
        return Task.CompletedTask;
    }

    /// <summary>
    /// 窗体首次打开后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual async void View_Shown(object sender, EventArgs e)
    {
        InitTree();
        await BindData();
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected virtual void InitTree()
    {
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected virtual async Task InitDictItem()
    {
        #region Grid初始化

        #region 主表列

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 后台获取并缓存

        // 字段列显示名称转义
        winGridViewPager1.ColumnNameAlias = (await Cache.Instance.GetOrCreateAsync($"{typeof(T).Name}ColumnNameAlias",
            async () => await _bll.GetColumnNameAliasAsync(), new TimeSpan(6, 0, 0)))!;

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #endregion

        #endregion
    }

    #region 网格列表信息

    /// <summary>
    /// 绑定列表数据
    /// </summary>
    protected virtual async Task BindData()
    {
        ShowWaitForm();
        WaitForm.SetWaitFormDescription("数据加载中...");
        Dictionary<string,string> condition = GetQueryCondition();
        PageInput pagerInfo = winGridViewPager1.PagerInfo.Adapt<PageInput>();
        PageResult<T> list = await _bll.GetEntitiesByPageAsync(new PaginatedSearchInfos(condition, pagerInfo));
        winGridViewPager1.InitDataSource(list, $"{moduleName}报表");
        HideWaitForm();
    }

    /// <summary>
    /// 单元格样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        if (e.Column.FieldName is "OrderStatus" or "OrderStatus")
        {
            string status = winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, e.Column.FieldName).ObjToStr();
            Color color = Color.White;
            if (status is "已审核" or "Y" || status.ToLower() == "true")
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
        }
    }

    /// <summary>
    /// 自定义列显示文本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
    {
        // 如果字段权限不够，那么字段的标签设置为*的
        if (string.Concat(e.Column.Tag) == "*") return;

        // string columnName = e.Column.FieldName;
        // if (columnName == "Age")
        // {
        //     e.DisplayText = string.Format("{0}岁", e.Value);
        // }
        // else if (columnName == "ReceivedMoney")
        // {
        //     if (e.Value != null)
        //     {
        //         e.DisplayText = e.Value.ToString().ToDecimal().ToString("C");
        //     }
        // }
    }

    /// <summary>
    /// 数据源变更时，分配各列的宽度
    /// </summary>
    protected virtual void gridView1_DataSourceChanged(object sender, EventArgs e)
    {
        if (winGridViewPager1.gridView1.Columns.Count > 0 && winGridViewPager1.gridView1.RowCount > 0)
        {
            // 统一设置100宽度
            foreach (GridColumn column in winGridViewPager1.gridView1.Columns)
            {
                column.Width = 100;
            }
        }
    }

    /// <summary>
    /// 分页控件刷新操作
    /// </summary>
    protected virtual async void winGridViewPager1_OnRefresh(object sender, EventArgs e)
    {
        await BindData();
    }

    /// <summary>
    /// 分页控件删除操作
    /// </summary>
    protected virtual async void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
    {
        await DeleteData();
    }

    /// <summary>
    /// 分页控件编辑项操作
    /// </summary>
    protected virtual void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
    {
        EditData();
    }

    /// <summary>
    /// 分页控件新增操作
    /// </summary>
    protected virtual void winGridViewPager1_OnAddNew(object sender, EventArgs e)
    {
        AddData();
    }

    /// <summary>
    /// 分页控件全部导出操作前的操作
    /// </summary>
    protected virtual async void winGridViewPager1_OnStartExport(object sender, EventArgs e)
    {
        Dictionary<string,string> condition = GetQueryCondition();
        winGridViewPager1.AllToExport = await _bll.FindAsync(condition);
    }

    /// <summary>
    /// 分页控件翻页的操作
    /// </summary>
    protected virtual async void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
    {
        await BindData();
    }

    #endregion

    #region 快捷查询条件

    /// <summary>
    /// 根据查询条件构造查询条件对象
    /// </summary>
    protected virtual Dictionary<string,string> GetQueryCondition()
    {
        return new Dictionary<string, string>();
    }

    #endregion
    
    #region 操作按钮

    /// <summary>
    /// 查询数据操作
    /// </summary>
    protected virtual async void btnSearch_Click(object sender, EventArgs e)
    {
        // 必须重置查询条件，否则可能会使用高级查询条件了
        _advanceCondition = null;
        _treeCondition = null;
        await BindData();
    }

    /// <summary>
    /// 清空查询条件
    /// </summary>
    protected virtual void btnClear_Click(object sender, EventArgs e)
    {
        layoutControl1.ClearPanelEditValue();
    }

    /// <summary>
    /// 新增数据操作
    /// </summary>
    protected virtual void btnAddNew_Click(object sender, EventArgs e)
    {
        AddData();
    }

    /// <summary>
    /// 审核数据操作
    /// </summary>
    protected virtual async void btnCheck_Click(object sender, EventArgs e)
    {
        await CheckData();
    }

    /// <summary>
    /// 高级查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual async void btnAdvanceSearch_Click(object sender, EventArgs e)
    {
        await AdvanceSearch();
    }

    /// <summary>
    /// 提供给控件回车执行查询的操作
    /// </summary>
    protected virtual void SearchControl_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnSearch_Click(sender, e);
        }
    }

    /// <summary>
    /// 导入Excel的操作
    /// </summary>
    protected virtual void btnImport_Click(object sender, EventArgs e)
    {
        ImportData();
    }

    /// <summary>
    /// 导入数据刷新事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual async void ExcelData_OnRefreshData(object sender, EventArgs e)
    {
        await BindData();
    }

    /// <summary>
    /// 导出Excel的操作
    /// </summary>
    protected virtual void btnExport_Click(object sender, EventArgs e)
    {
        ExportData();
    }

    /// <summary>
    /// 子窗体保存后事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual async void edit_OnDataSaved(object sender, EventArgs e)
    {
        await BindData();
    }

    /// <summary>
    /// 网格布局切换
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void chkTableDirection_CheckedChanged(object sender, EventArgs e)
    {
        splitContainer1.Horizontal = !((BarToggleSwitchItem)sender).Checked;
    }

    /// <summary>
    /// 隐藏树
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void chkHideTree_CheckedChanged(object sender, EventArgs e)
    {
        var panelVis = ((BarToggleSwitchItem)sender).Checked ? SplitPanelVisibility.Panel2 : SplitPanelVisibility.Both;
        splitContainerControl1.PanelVisibility = panelVis;
    }

    /// <summary>
    /// 树选择后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual async void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        // 构建 快查树的查询条件
        switch (e.Node)
        {
            case { Text: "所有记录" }:
                _treeCondition = null;
                btnClear_Click(sender, e);
                await BindData();
                break;
            // Tag 是单个键值类，上级 Tag 是 列名，键值类的值是值
            case { Tag: CListItem item, Parent.Tag: string fieldName }:
                _treeCondition = new NameValueCollection()
                {
                    { fieldName, item.Value }
                };
                await BindData();
                break;
            // Tag 是键值类列表，键值类的键是列名，值是值
            case { Tag: List<CListItem> items }:
                _treeCondition = new NameValueCollection();
                items.ForEach(x => _treeCondition.Add(x.Text, x.Value));
                await BindData();
                break;
        }
    }

    /// <summary>
    /// 展开树
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void menuTree_ExpandAll_Click(object sender, EventArgs e)
    {
        treeView1.ExpandAll();
    }

    /// <summary>
    /// 折叠树
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void menuTree_Clapase_Click(object sender, EventArgs e)
    {
        treeView1.CollapseAll();
    }

    /// <summary>
    /// 刷新树
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void menuTree_Refresh_Click(object sender, EventArgs e)
    {
        InitTree();
    }

    #endregion

    #region 增删改审

    /// <summary>
    /// 添加数据
    /// </summary>
    protected virtual void AddData()
    {
        var edit = App.GetService<TE>();

        // 保存成功后事件
        edit.OnDataSaved += edit_OnDataSaved;

        if (DialogResult.OK == edit.ShowDialog())
        {
            // BindData();
        }
    }

    /// <summary>
    /// 编辑列表数据
    /// </summary>
    private void EditData()
    {
        string id = GetFocusedRowCellPrimaryValue();
        List<string> idList = new();
        for (var i = 0; i < winGridViewPager1.gridView1.RowCount; i++)
        {
            string strTemp = winGridViewPager1.GridView1.GetRowCellDisplayText(i, GetPrimaryKey());
            idList.Add(strTemp);
        }

        if (!string.IsNullOrEmpty(id))
        {
            _baseForm.Value.ID = id;
            _baseForm.Value.IdList = idList;

            // 保存成功后事件
            _baseForm.Value.OnDataSaved += edit_OnDataSaved;
            _baseForm.Value.InitFunction(LoginUserInfo, FunctionDict); //给子窗体赋值用户权限信息

            if (DialogResult.OK == _baseForm.Value.ShowDialog())
            {
                // BindData();
            }
        }
    }

    /// <summary>
    /// 删除选中列表数据
    /// </summary>
    public virtual async Task DeleteData()
    {
        if ("您确定删除选定的记录么？".ShowYesNoAndUxTips() == DialogResult.No)
        {
            return;
        }

        string primaryKey =  typeof(T).GetFieldValue("PrimaryKey").ObjToStr();
        if (string.IsNullOrEmpty(primaryKey))
        {
            throw new Exception("主表实体没有配置主键，无法进行查询");
        }
        int[] rowSelected = winGridViewPager1.GridView1.GetSelectedRows();
        foreach (int iRow in rowSelected)
        {
            string id = winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, primaryKey);
            await _bll.DeleteAsync(id);
        }

        await BindData();
    }

    /// <summary>
    /// 审核选中列表数据
    /// </summary>
    private async Task CheckData()
    {
        string id = GetFocusedRowCellPrimaryValue();
        var entity = winGridViewPager1.gridView1.GetFocusedRow().To<Car>();
        if (string.IsNullOrEmpty(id))
        {
            "主键异常".ShowUxError();
            return;
        }

        if($"确定需要{(entity.FlagApp ? "取消审核" : "审核" )}么".ShowYesNoAndUxTips() == DialogResult.No)
            return;

        if (await _bll.ApproveAsync(id))
        {
            $"{(entity.FlagApp ? "取消审核" : "审核" )}成功！".ShowSuccessTip(this);
            await BindData();
        }
        else
        {
            $"{(entity.FlagApp ? "取消审核" : "审核" )}失败！".ShowUxError();
        }
    }

    #endregion

    #region 导入导出

    /// <summary>
    /// 导入的操作
    /// </summary>
    private void ImportData()
    {
        var templateFile = $"{moduleName}-模板.xls";
        var import = new FrmImportExcelData();
        import.SetTemplate(templateFile, Path.Combine(Application.StartupPath, templateFile));
        import.OnDataSave += ExcelData_OnDataSave;
        import.OnRefreshData += ExcelData_OnRefreshData;
        import.ShowDialog();
    }

    /// <summary>
    /// 如果字段存在，则获取对应的值，否则返回默认空
    /// </summary>
    /// <param name="row">DataRow对象</param>
    /// <param name="columnName">字段列名</param>
    /// <returns></returns>
    protected string GetRowData(DataRow row, string columnName)
    {
        string result = "";
        if (row.Table.Columns.Contains(columnName))
        {
            result = row[columnName].ToString();
        }

        return result;
    }

    /// <summary>
    /// 导入数据保存事件
    /// </summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    protected virtual Task<bool> ExcelData_OnDataSave(DataRow dr)
    {
        return Task.FromResult(false);
    }

    /// <summary>
    /// 导出的操作
    /// </summary>
    protected virtual Task ExportData()
    {
        return Task.CompletedTask;
    }

    #endregion

    #region 高级查询

    /// <summary>
    /// 高级查询界面
    /// </summary>
    protected FrmAdvanceSearch? AdvDlg;

    /// <summary>
    /// 高级查询的操作
    /// </summary>
    protected virtual async Task AdvanceSearch()
    {
        if (AdvDlg == null)
        {
            DataTable? fieldTypeTable = await Cache.Instance.GetOrCreateAsync("CarFieldTypeList",
                async () => await _bll.GetFieldTypeListAsync(), new TimeSpan(3, 0, 0));

            Dictionary<string, string>? columnNameAlias = await Cache.Instance.GetOrCreateAsync("CarColumnNameAlias",
                async () => await _bll.GetColumnNameAliasAsync(), new TimeSpan(3, 0, 0));

            if (fieldTypeTable is null || columnNameAlias is null)
            {
                throw Oops.Bah("字段类型表或字段别名表为空");
            }

            AdvDlg = new FrmAdvanceSearch();
            AdvDlg.FieldTypeTable = fieldTypeTable;
            AdvDlg.ColumnNameAlias = columnNameAlias;
        }
    }

    protected virtual async void advDlg_ConditionChanged(NameValueCollection condition)
    {
        _advanceCondition = condition;
        _treeCondition = null;
        await BindData();
    }

    #endregion

    #region 内部方法

    /// <summary>
    /// 获取选中行的主键值
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string GetFocusedRowCellPrimaryValue()
    {
        string no = winGridViewPager1.gridView1.GetFocusedRowCellDisplayText(GetPrimaryKey());
        if (string.IsNullOrEmpty(no))
        {
            "主键异常，可能是没有选中记录！".ShowErrorTip(winGridViewPager1);
        }
        return no;
    }

    /// <summary>
    /// 获取当前模块的主键字段名
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string GetPrimaryKey()
    {
        PropertyInfo keyProp =  ReflectionExtension.GetProperties<T, KeyAttribute>().FirstOrDefault();
        if (keyProp == null)
        {
            "主表实体没有找到[Key]特性配置的主键".ShowErrorTip(winGridViewPager1);
        }

        string? primaryKey = keyProp!.GetAttribute<ColumnAttribute, string>(x => x.Name);
        if (string.IsNullOrEmpty(primaryKey))
        {
            primaryKey = keyProp!.Name;
        }

        return primaryKey;
    }

    #endregion
}

public partial class BaseViewDock<T, IT, TE, T1, IT1> : BaseViewDock<T, IT, TE>
    where T : BaseEntity
    where IT : BaseHttpService<T>
    where TE : BaseForm
    where T1 : BaseEntity
    where IT1 : BaseHttpService<T1>
{
    private readonly IT1 _childBll;

    public BaseViewDock(IT bll, IT1 childBll, LazilyResolved<TE> baseForm) : base(bll, baseForm)
    {
        _childBll = childBll;
    }

    /// <summary>
    /// 编写初始化窗体的实现
    /// </summary>
    public override async Task FormOnLoad()
    {
        await base.FormOnLoad();

        splitContainerControl1.Panel1.Dock = DockStyle.Left;
        
        tableDirectionButton.Visibility = BarItemVisibility.Always;

        #region 网格事件和配置

        #region 主表事件和配置

        winGridViewPager1.gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;

        #endregion

        #region 明细表事件和配置

        //从表明细信息
        winGridView2.OnRefresh += winGridView2_OnRefresh;
        winGridView2.ShowLineNumber = true;
        winGridView2.BestFitColumnWith = false; //是否设置为自动调整宽度，false为不设置
        winGridView2.gridView1.DataSourceChanged += gridView2_DataSourceChanged;
        winGridView2.gridView1.CustomColumnDisplayText += gridView2_CustomColumnDisplayText;

        #endregion

        #endregion
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected override async Task InitDictItem()
    {
        await base.InitDictItem();

        #region 明细表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridView2.DisplayColumns = "ISID,ContactPerson,NativeName,Address,AreaNo,Tel,Mobile,InsuranceRate,Coordinate,DefaultToNode,DefaultToNodes,DefaultToNodesName,CargoName,PackageType,CargoUnit,Price,PriceType,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy";

        #region 后台获取并缓存

        // 字段列显示名称转义
        winGridView2.ColumnNameAlias = await Cache.Instance.GetOrCreateAsync($"{typeof(T1).Name}ColumnNameAlias",
            async () => await _childBll.GetColumnNameAliasAsync(), new TimeSpan(6, 0, 0));

        #endregion

        #endregion
    }

    #region 网格列表信息

    /// <summary>
    /// 列表选择或者移动行焦点的触发事件操作
    /// </summary>
    protected virtual void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
    {
        //获取主表表头ID
        if (sender is not GridView gv || gv.Columns.Count == 0) return;

        // List<PropertyInfo> prop =  ReflectionExtension.GetProperties<T, KeyAttribute>();
        // if (!prop.Any())
        // {
        //     throw new Exception("主表实体没有配置主键，无法进行查询");
        // }
        // string primaryKey = prop.First().Name;
        // string headerId = gv.GetRowCellDisplayText(e.FocusedRowHandle, primaryKey); // 另一种获取方式
        // // string headerId = winGridViewPager1.gridView1.GetFocusedRowCellDisplayText(primaryKey);
        // BindDetail(headerId);
        BindDetail(GetFocusedRowCellPrimaryValue());
    }

    #region 从表明细列表

    protected virtual void winGridView2_OnRefresh(object sender, EventArgs e)
    {
        BindDetail(GetFocusedRowCellPrimaryValue());
    }

    /// <summary>
    /// 绑定主表明细列表数据
    /// </summary>
    protected virtual async Task BindDetail(string headerId)
    {
        if (string.IsNullOrEmpty(headerId)) return;
        List<T1> list = await _childBll.FindByForeignKeyAsync(headerId);
        winGridView2.DataSource = list; //new SortableBindingList<T1>(list);
        winGridView2.PrintTitle = $"{moduleName}明细报表";
    }

    /// <summary>
    /// 明细表数据源变更时
    /// </summary>
    protected virtual void gridView2_DataSourceChanged(object sender, EventArgs e)
    {
        // 绑定数据后，分配各列的宽度
        if (winGridView2.gridView1.Columns.Count > 0 && winGridView2.gridView1.RowCount > 0)
        {
            //统一设置100宽度
            foreach (GridColumn column in winGridView2.gridView1.Columns)
            {
                column.Width = 80;
            }
        }
    }

    /// <summary>
    /// 表格显示内容转义
    /// </summary>
    protected virtual void gridView2_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
    {
        // string fieldName = e.Column.FieldName;
        // if (fieldName == "SalePrice" || fieldName == "Price" || fieldName == "Amount")
        // {
        //     if (e.Value != null)
        //     {
        //         e.DisplayText = e.Value.ToString().ToDecimal().ToString("C2");
        //     }
        // }
    }

    #endregion

    #endregion

    #region 增删改审

    /// <summary>
    /// 删除选中列表数据
    /// </summary>
    public override async Task DeleteData()
    {
        if ("您确定删除选定的记录么？".ShowYesNoAndUxTips() == DialogResult.No)
        {
            return;
        }

        // 获取主键名称
        string primaryKey =  typeof(T).GetFieldValue("PrimaryKey").ObjToStr();
        if (primaryKey.IsNullOrEmpty())
        {
            throw new Exception("主表实体没有配置主键，无法进行查询");
        }
        int[] rowSelected = winGridViewPager1.GridView1.GetSelectedRows();
        object[] keys = Array.Empty<object>();
        foreach (int iRow in rowSelected)
        {
            object id = winGridViewPager1.GridView1.GetRowCellValue(iRow, primaryKey);
            if (id is not null)
            {
                keys.SetValue(id, keys.Length);
            }
        }

        if (keys.Length > 0)
        {
            // 批量删除
            await _bll.DeleteByIdsAsync(keys);
            await BindData();
        }
        else
        {
            throw Oops.Bah("主键异常，没有找到主键");
        }
    }

    #endregion
}