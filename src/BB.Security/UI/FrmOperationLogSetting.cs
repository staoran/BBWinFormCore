using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.Tools.Format;
using BB.Entity.Security;
using BB.HttpServices.Core.OperationLogSetting;
using BB.Tools.Entity;
using BB.Tools.Extension;
using Furion;
using Mapster;

namespace BB.Security.UI;

/// <summary>
/// 记录操作日志的数据表配置
/// </summary>
public partial class FrmOperationLogSetting : BaseDock
{
    private readonly OperationLogSettingHttpService _bll;

    public FrmOperationLogSetting(OperationLogSettingHttpService bll)
    {
        InitializeComponent();

        _bll = bll;

        InitDictItem();

        winGridViewPager1.OnPageChanged += winGridViewPager1_OnPageChanged;
        winGridViewPager1.OnStartExport += winGridViewPager1_OnStartExport;
        winGridViewPager1.OnEditSelected += winGridViewPager1_OnEditSelected;
        winGridViewPager1.OnAddNew += winGridViewPager1_OnAddNew;
        winGridViewPager1.OnDeleteSelected += winGridViewPager1_OnDeleteSelected;
        winGridViewPager1.OnRefresh += winGridViewPager1_OnRefresh;
        winGridViewPager1.AppendedMenu = contextMenuStrip1;
        winGridViewPager1.ShowLineNumber = true;
        winGridViewPager1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
        winGridViewPager1.gridView1.DataSourceChanged +=gridView1_DataSourceChanged;
        winGridViewPager1.gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
        winGridViewPager1.gridView1.RowCellStyle += gridView1_RowCellStyle;

        //关联回车键进行查询
        foreach (Control control in layoutControl1.Controls)
        {
            control.KeyUp += SearchControl_KeyUp;
        }
    }
    void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
    {
        if (e.Column.FieldName == "Forbid")
        {
            Color color = Color.White;
            if (e.CellValue.ObjToBool())
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
        }  
    }
    void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
    {
        if (e.Column.ColumnType == typeof(DateTime))
        {
            string columnName = e.Column.FieldName;
            if (e.Value != null)
            {
                if (Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
                {
                    e.DisplayText = "";
                }
                else
                {
                    e.DisplayText = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd HH:mm");//yyyy-MM-dd
                }
            }
        }
        //else if (e.Column.FieldName == "Age")
        //{
        //    e.DisplayText = string.Format("{0}岁", e.Value);
        //}
        //else if (Column.FieldName == "ReceivedMoney")
        //{
        //    if (e.Value != null)
        //    {
        //        e.DisplayText = e.Value.ToString().ToDecimal().ToString("C");
        //    }
        //}
    }
        
    /// <summary>
    /// 绑定数据后，分配各列的宽度
    /// </summary>
    private void gridView1_DataSourceChanged(object sender, EventArgs e)
    {
        if (winGridViewPager1.gridView1.Columns.Count > 0 && winGridViewPager1.gridView1.RowCount > 0)
        {
            //统一设置100宽度
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in winGridViewPager1.gridView1.Columns)
            {
                column.Width = 100;
            }

            //可特殊设置特别的宽度
            SetGridColumWidth("TableName", 150);
            SetGridColumWidth("LastUpdateDate", 150);
            SetGridColumWidth("CreationDate", 150);
            SetGridColumWidth("Note", 200);
        }
    }

    private void SetGridColumWidth(string columnName, int width)
    {
        DevExpress.XtraGrid.Columns.GridColumn column = winGridViewPager1.gridView1.Columns.ColumnByFieldName(columnName);
        if (column != null)
        {
            column.Width = width;
        }
    }

    /// <summary>
    /// 编写初始化窗体的实现，可以用于刷新
    /// </summary>
    public override async Task FormOnLoad()
    {   
        await BindData();
    }
        
    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    private void InitDictItem()
    {
        //初始化代码
    }
        
    /// <summary>
    /// 分页控件刷新操作
    /// </summary>
    private async void winGridViewPager1_OnRefresh(object sender, EventArgs e)
    {
        await BindData();
    }
        
    /// <summary>
    /// 分页控件删除操作
    /// </summary>
    private async void winGridViewPager1_OnDeleteSelected(object sender, EventArgs e)
    {
        if ("您确定删除选定的记录么？".ShowYesNoAndUxTips() == DialogResult.No)
        {
            return;
        }

        int[] rowSelected = winGridViewPager1.GridView1.GetSelectedRows();
        foreach (int iRow in rowSelected)
        {
            string id = winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
            await _bll.DeleteAsync(id);
        }
             
        await BindData();
    }
        
    /// <summary>
    /// 分页控件编辑项操作
    /// </summary>
    private async void winGridViewPager1_OnEditSelected(object sender, EventArgs e)
    {
        string id = winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
        List<string> idList = new List<string>();
        for (int i = 0; i < winGridViewPager1.gridView1.RowCount; i++)
        {
            string strTemp = winGridViewPager1.GridView1.GetRowCellDisplayText(i, "ID");
            idList.Add(strTemp);
        }

        if (!string.IsNullOrEmpty(id))
        {
            FrmEditOperationLogSetting dlg = App.GetService<FrmEditOperationLogSetting>();
            dlg.ID = id;
            dlg.IdList = idList;
            dlg.OnDataSaved += dlg_OnDataSaved;
                
            if (DialogResult.OK == dlg.ShowDialog())
            {
                await BindData();
            }
        }
    }        
        
    async void dlg_OnDataSaved(object sender, EventArgs e)
    {
        await BindData();
    }
        
    /// <summary>
    /// 分页控件新增操作
    /// </summary>        
    private void winGridViewPager1_OnAddNew(object sender, EventArgs e)
    {
        btnAddNew_Click(sender, e);
    }
        
    /// <summary>
    /// 分页控件全部导出操作前的操作
    /// </summary> 
    private async void winGridViewPager1_OnStartExport(object sender, EventArgs e)
    {
        Dictionary<string,string> condition = GetConditionSql();
        winGridViewPager1.AllToExport = await _bll.FindAsync(condition);
    }

    /// <summary>
    /// 分页控件翻页的操作
    /// </summary> 
    private async void winGridViewPager1_OnPageChanged(object sender, EventArgs e)
    {
        await BindData();
    }
        
    /// <summary>
    /// 根据查询条件构造查询语句
    /// </summary> 
    private Dictionary<string,string> GetConditionSql()
    {
        //如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
        var condition = new NameValueCollection
        {
            { OperationLogSettingInfo.FieldTableName, txtTableName.Text.Trim() },
            { OperationLogSettingInfo.FieldNote, txtNote.Text.Trim() },
        };

        if (chkForbid.Checked)
        {
            condition.Add(OperationLogSettingInfo.FieldForbid, "1");
        }
        
        return condition.ToDicString();
    }
        
    /// <summary>
    /// 绑定列表数据
    /// </summary>
    private async Task BindData()
    {
        //entity
        winGridViewPager1.DisplayColumns = "TableName,Forbid,InsertLog,DeleteLog,UpdateLog,Note,Creator,CreationDate,Editor,LastUpdateDate";
        winGridViewPager1.ColumnNameAlias = await _bll.GetColumnNameAliasAsync();//字段列显示名称转义

        #region 添加别名解析

        //this.winGridViewPager1.AddColumnAlias("Forbid", "是否禁用");
        //this.winGridViewPager1.AddColumnAlias("TableName", "数据库表");
        //this.winGridViewPager1.AddColumnAlias("InsertLog", "记录插入日志");
        //this.winGridViewPager1.AddColumnAlias("DeleteLog", "记录删除日志");
        //this.winGridViewPager1.AddColumnAlias("UpdateLog", "记录更新日志");
        //this.winGridViewPager1.AddColumnAlias("Note", "备注");
        //this.winGridViewPager1.AddColumnAlias("Creator", "创建人");
        //this.winGridViewPager1.AddColumnAlias("CreationDate", "创建时间");
        //this.winGridViewPager1.AddColumnAlias("Editor", "编辑人");
        //this.winGridViewPager1.AddColumnAlias("LastUpdateDate", "编辑时间");

        #endregion

        Dictionary<string,string> condition = GetConditionSql();
        PageInput pagerInfo = winGridViewPager1.PagerInfo.Adapt<PageInput>();
        PageResult<OperationLogSettingInfo> list = await _bll.GetEntitiesByPageAsync(new PaginatedSearchInfos(condition, pagerInfo));
        winGridViewPager1.InitDataSource(list, "记录操作日志的数据表配置报表");
    }
        
    /// <summary>
    /// 查询数据操作
    /// </summary>
    private async void btnSearch_Click(object sender, EventArgs e)
    {
        await BindData();
    }
        
    /// <summary>
    /// 新增数据操作
    /// </summary>
    private async void btnAddNew_Click(object sender, EventArgs e)
    {
        FrmEditOperationLogSetting dlg = App.GetService<FrmEditOperationLogSetting>();
        dlg.OnDataSaved += dlg_OnDataSaved;
            
        if (DialogResult.OK == dlg.ShowDialog())
        {
            await BindData();
        }
    }
        
    /// <summary>
    /// 提供给控件回车执行查询的操作
    /// </summary>
    private void SearchControl_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnSearch_Click(sender, e);
        }
    }       

    private async void chkForbid_CheckedChanged(object sender, EventArgs e)
    {
        await BindData();
    }
}