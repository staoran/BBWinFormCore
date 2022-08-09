using System.Collections.Specialized;
using System.Data;
using BB.BaseUI.AdvanceSearch;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace BB.BaseUI.BaseUI;

public partial class BaseViewDesigner : BaseDock
{
    protected readonly string moduleName;
    protected readonly dynamic _bll;
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
    /// 高级查询界面
    /// </summary>
    protected FrmAdvanceSearch _advDlg;

    /// <summary>
    /// 高级查询条件语句对象
    /// </summary>
    protected NameValueCollection _advanceCondition;

    /// <summary>
    /// 快捷树查询条件语句对象
    /// </summary>
    protected NameValueCollection _treeCondition;
    public BaseViewDesigner()
    {
        InitializeComponent();
    }
    public BaseViewDesigner(string name) : this()
    {
    }

    /// <summary>
    /// 窗体首次打开后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void View_Shown(object sender, EventArgs e)
    {
        InitTree();
        BindData();
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
    protected virtual void InitDictItem()
    {
    }

    #region 网格列表信息

    /// <summary>
    /// 绑定列表数据
    /// </summary>
    protected virtual void BindData()
    {
    }

    /// <summary>
    /// 单元格样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
    }

    /// <summary>
    /// 自定义列显示文本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
    {
    }

    /// <summary>
    /// 数据源变更时，分配各列的宽度
    /// </summary>
    protected virtual void gridView1_DataSourceChanged(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 分页控件刷新操作
    /// </summary>
    protected virtual void winGridViewPager1_OnRefresh(object sender, EventArgs e)
    {
        BindData();
    }

    /// <summary>
    /// 分页控件删除操作
    /// </summary>
    protected virtual void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 分页控件编辑项操作
    /// </summary>
    protected virtual void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
    {
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
    protected virtual void winGridViewPager1_OnStartExport(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 分页控件翻页的操作
    /// </summary>
    protected virtual void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    #endregion

    #region 快捷查询条件

    /// <summary>
    /// 根据查询条件构造查询条件对象
    /// </summary>
    protected virtual NameValueCollection GetQueryCondition()
    {
        return null;
    }

    #endregion
    
    #region 操作按钮

    /// <summary>
    /// 查询数据操作
    /// </summary>
    protected virtual void btnSearch_Click(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 清空查询条件
    /// </summary>
    protected virtual void btnClear_Click(object sender, EventArgs e)
    {
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
    protected virtual void btnCheck_Click(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 高级查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void btnAdvanceSearch_Click(object sender, EventArgs e)
    {
        AdvanceSearch();
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
    }

    /// <summary>
    /// 导入数据刷新事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void ExcelData_OnRefreshData(object sender, EventArgs e)
    {
        BindData();
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
    protected virtual void edit_OnDataSaved(object sender, EventArgs e)
    {
        BindData();
    }

    /// <summary>
    /// 网格布局切换
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void chkTableDirection_CheckedChanged(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 隐藏树
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void chkHideTree_CheckedChanged(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 树选择后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
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
    }

    #endregion

    #region 导入导出

    /// <summary>
    /// 导入数据保存事件
    /// </summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    protected virtual bool ExcelData_OnDataSave(DataRow dr)
    {
        return false;
    }

    /// <summary>
    /// 导出的操作
    /// </summary>
    protected virtual void ExportData()
    {
    }

    #endregion

    #region 高级查询

    /// <summary>
    /// 高级查询的操作
    /// </summary>
    protected virtual void AdvanceSearch()
    {
    }

    protected virtual void advDlg_ConditionChanged(NameValueCollection condition)
    {
    }

    #endregion
        #region 网格列表信息

    /// <summary>
    /// 列表选择或者移动行焦点的触发事件操作
    /// </summary>
    protected virtual void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
    {
    }

    #region 从表明细列表

    protected virtual void winGridView2_OnRefresh(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 绑定主表明细列表数据
    /// </summary>
    protected virtual void BindDetail(string headerId)
    {
    }

    /// <summary>
    /// 明细表数据源变更时
    /// </summary>
    protected virtual void gridView2_DataSourceChanged(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 表格显示内容转义
    /// </summary>
    protected virtual void gridView2_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
    {
    }

    #endregion

    #endregion
    
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
}