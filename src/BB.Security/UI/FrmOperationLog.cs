using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.DocViewer;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Entity.Security;
using BB.HttpServices.Core.OperationLog;
using BB.HttpServices.Core.OU;
using BB.Tools.Collections;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using Furion.Logging.Extensions;
using Mapster;

namespace BB.Security.UI;

/// <summary>
/// 用户关键操作记录
/// </summary>	
public partial class FrmOperationLog : BaseDock
{
    NameValueCollection? _treeCondition = null;
    private readonly OperationLogHttpService _bll;
    private readonly OUHttpService _oubll;

    public FrmOperationLog(OperationLogHttpService bll, OUHttpService ouBll)
    {
        InitializeComponent();

        _bll = bll;
        _oubll = ouBll;

        InitDictItem();

        winGridViewPager1.OnPageChanged += winGridViewPager1_OnPageChanged;
        winGridViewPager1.OnStartExport += winGridViewPager1_OnStartExport;
        winGridViewPager1.OnEditSelected += winGridViewPager1_OnEditSelected;
        //this.winGridViewPager1.OnAddNew += new EventHandler(winGridViewPager1_OnAddNew);
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
        //if (e.Column.FieldName == "OrderStatus")
        //{
        //    string status = this.winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "OrderStatus").ToString();
        //    Color color = Color.White;
        //    if (status == "已审核")
        //    {
        //        e.Appearance.BackColor = Color.Red;
        //        e.Appearance.BackColor2 = Color.LightCyan;
        //    }
        //}
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
            SetGridColumWidth("MacAddress", 150);
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
        InitTree();
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
            FrmEditOperationLog dlg = new FrmEditOperationLog(_bll);
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
        CListItem[] condition = GetConditionSql();
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
    private CListItem[] GetConditionSql()
    {
        //如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
        var condition = new NameValueCollection
        {
            { OperationLogInfo.FieldLoginName, txtLoginName.Text.Trim() },
            { OperationLogInfo.FieldTableName, txtTableName.Text.Trim() },
            { OperationLogInfo.FieldOperationType, txtOperationType.Text.Trim() },
            { OperationLogInfo.FieldCreationDate, txtCreationDate1.Text.Trim() },
            { OperationLogInfo.FieldCreationDate, txtCreationDate2.Text.Trim() }
        };

        return condition.ToCListItems();
    }
        
    /// <summary>
    /// 绑定列表数据
    /// </summary>
    private async Task BindData()
    {
        //entity
        winGridViewPager1.DisplayColumns = "LoginName,FullName,CompanyName,TableName,OperationType,IPAddress,MacAddress,CreationDate";
        winGridViewPager1.ColumnNameAlias = await _bll.GetColumnNameAliasAsync();//字段列显示名称转义

        #region 添加别名解析

        //this.winGridViewPager1.AddColumnAlias("LoginName", "登录名");
        //this.winGridViewPager1.AddColumnAlias("FullName", "真实名称");
        //this.winGridViewPager1.AddColumnAlias("CompanyName", "所属公司名称");
        //this.winGridViewPager1.AddColumnAlias("TableName", "操作表名称");
        //this.winGridViewPager1.AddColumnAlias("OperationType", "操作类型");
        //this.winGridViewPager1.AddColumnAlias("CreationDate", "创建时间");

        #endregion

        CListItem[] condition = GetConditionSql();
        PageInput pagerInfo = winGridViewPager1.PagerInfo.Adapt<PageInput>();
        PageResult<OperationLogInfo> list = await _bll.GetEntitiesByPageAsync(new PaginatedSearchInfos(condition, pagerInfo));
        winGridViewPager1.InitDataSource(list, "用户关键操作记录报表");
    }
        
    /// <summary>
    /// 查询数据操作
    /// </summary>
    private async void btnSearch_Click(object sender, EventArgs e)
    {
        _treeCondition = null;
        await BindData();
    }
        
    /// <summary>
    /// 新增数据操作
    /// </summary>
    private async void btnAddNew_Click(object sender, EventArgs e)
    {
        FrmEditOperationLog dlg = new FrmEditOperationLog(_bll);
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

	 						 						 						 						 						 						 						 						 						 
    private string _moduleName = "用户关键操作记录";

    /// <summary>
    /// 导出Excel的操作
    /// </summary>
    private async void btnExport_Click(object sender, EventArgs e)
    {
        string file = FileDialogHelper.SaveExcel($"{_moduleName}.xls");
        if (!string.IsNullOrEmpty(file))
        {
            CListItem[] condition = GetConditionSql();
            List<OperationLogInfo> list = await _bll.FindAsync(condition);
            DataTable dtNew = DataTableHelper.CreateTable("序号|int,登录用户ID,登录名,真实名称,所属公司ID,所属公司名称,操作表名称,操作类型,日志描述,IP地址,Mac地址,创建时间");
            DataRow dr;
            int j = 1;
            for (int i = 0; i < list.Count; i++)
            {
                dr = dtNew.NewRow();
                dr["序号"] = j++;
                dr["登录用户ID"] = list[i].UserId;
                dr["登录名"] = list[i].LoginName;
                dr["真实名称"] = list[i].FullName;
                dr["所属公司ID"] = list[i].CompanyId;
                dr["所属公司名称"] = list[i].CompanyName;
                dr["操作表名称"] = list[i].TableName;
                dr["操作类型"] = list[i].OperationType;
                dr["日志描述"] = list[i].Note;
                dr["IP地址"] = list[i].IPAddress;
                dr["Mac地址"] = list[i].MacAddress;
                dr["创建时间"] = list[i].CreationDate;
                dtNew.Rows.Add(dr);
            }

            try
            {
                string error = "";
                AsposeExcelTools.DataTableToExcel2(dtNew, file, out error);
                if (!string.IsNullOrEmpty(error))
                {
                    $"导出Excel出现错误：{error}".ShowUxError();
                }
                else
                {
                    if ("导出成功，是否打开文件？".ShowYesNoAndUxTips() == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(file);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString().LogError();
                ex.Message.ShowUxError();
            }
        }
    }

    private void btnSetTableLog_Click(object sender, EventArgs e)
    {
        //FrmOperationLogSetting dlg = new FrmOperationLogSetting();
        //dlg.ShowDialog();
        ChildWinManagement.LoadMdiForm(GB.MainDialog, typeof(FrmOperationLogSetting));
    }

    private async Task InitTree()
    {
        treeView1.BeginUpdate();
        treeView1.Nodes.Clear();
        //添加一个未分类和全部客户的组别
        TreeNode topNode = new TreeNode("所有记录", 0, 0);
        treeView1.Nodes.Add(topNode);

        TreeNode companyNode = new TreeNode("所属公司", 1, 1);
        treeView1.Nodes.Add(companyNode);

        List<OUInfo> companyList = new List<OUInfo>();
        if (GB.UserInRole(RoleInfo.SUPER_ADMIN_NAME))
        {
            List<OUInfo> list = await GB.GetMyTopGroup();
            foreach (OUInfo groupInfo in list)
            {
                companyList.AddRange(await _oubll.GetAllCompanyAsync(groupInfo.HandNo));
            }
        }
        else
        {
            OUInfo myCompanyInfo = await _oubll.FindByIdAsync(GB.LoginUserInfo.CompanyId);
            if (myCompanyInfo != null)
            {
                companyList.Add(myCompanyInfo);
            }
        }

        foreach (OUInfo info in companyList)
        {
            TreeNode subNode = new TreeNode(info.Name, 1, 1)
            {
                Tag = $"Company_ID='{info.HandNo}' "
            };
            companyNode.Nodes.Add(subNode);
        }

        TreeNode tableNode = new TreeNode("数据库表", 2, 2);
        treeView1.Nodes.Add(tableNode);
        List<string> tableList = await _bll.GetFieldListAsync("TableName");

        bool isCompanyAdmin = GB.UserInRole(RoleInfo.COMPANY_ADMIN_NAME);
        foreach (string tablename in tableList)
        {
            TreeNode subNode = new TreeNode(tablename, 3, 3);                
            //如果是公司管理员，增加公司标识
            if (isCompanyAdmin)
            {
                subNode.Tag = $"TableName='{tablename}' AND Company_ID='{GB.LoginUserInfo.CompanyId}' ";
            }
            else
            {
                subNode.Tag = $"TableName='{tablename}' ";
            }
            tableNode.Nodes.Add(subNode);

            List<string> operationList = new List<string>() { "增加", "修改", "删除"};
            foreach (string operationType in operationList)
            {
                TreeNode operationNode = new TreeNode(operationType, 4, 4);                    
                //如果是公司管理员，增加公司标识
                if (isCompanyAdmin)
                {
                    operationNode.Tag =
                        $"TableName='{tablename}'  AND OperationType='{operationType}' AND Company_ID='{GB.LoginUserInfo.CompanyId}' ";
                }
                else
                {
                    operationNode.Tag = $"TableName='{tablename}' AND OperationType='{operationType}' ";
                }
                subNode.Nodes.Add(operationNode);
            }
        }

        treeView1.ExpandAll();
        treeView1.EndUpdate();
    }

    private async void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node is { Tag: { } })
        {
            _treeCondition = (NameValueCollection?)e.Node.Tag;
            await BindData();
        }
        else
        {
            _treeCondition = null;
            await BindData();
        }
    }

    private void menuTree_ExpandAll_Click(object sender, EventArgs e)
    {
        treeView1.ExpandAll();
    }

    private void menuTree_Clapase_Click(object sender, EventArgs e)
    {
        treeView1.CollapseAll();
    }

    private void menuTree_Refresh_Click(object sender, EventArgs e)
    {
        InitTree();
    }

}