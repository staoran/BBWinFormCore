using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.DocViewer;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Tools.Entity;
using BB.Entity.Security;
using BB.HttpServices.Core.LoginLog;
using BB.HttpServices.Core.OU;
using BB.HttpServices.Core.SystemType;
using BB.Tools.Extension;
using BB.Tools.Format;
using Furion.Logging.Extensions;
using Mapster;

namespace BB.Security.UI;

/// <summary>
/// 用户登录日志信息
/// </summary>	
public partial class FrmLoginLog : BaseForm
{
    NameValueCollection? _treeCondition = null;
    private readonly SystemTypeHttpService _systemTypeBLL;
    private readonly LoginLogHttpService _bll;
    private readonly OUHttpService _oubll;

    public FrmLoginLog(LoginLogHttpService bll, SystemTypeHttpService systemTypeBll, OUHttpService ouBll)
    {
        InitializeComponent();

        _systemTypeBLL = systemTypeBll;
        _bll = bll;
        _oubll = ouBll;

        winGridViewPager1.OnPageChanged += winGridViewPager1_OnPageChanged;
        winGridViewPager1.OnStartExport += winGridViewPager1_OnStartExport;
        winGridViewPager1.OnDeleteSelected += winGridViewPager1_OnDeleteSelected;
        winGridViewPager1.OnRefresh += winGridViewPager1_OnRefresh;
        winGridViewPager1.ShowLineNumber = true;
        winGridViewPager1.AppendedMenu = contextMenuStrip1;
        winGridViewPager1.BestFitColumnWith = false;
        winGridViewPager1.gridView1.DataSourceChanged += gridView1_DataSourceChanged;
        winGridViewPager1.gridView1.RowCellStyle += gridView1_RowCellStyle;
        winGridViewPager1.gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;

        //关联回车键进行查询
        foreach (Control control in layoutControl1.Controls)
        {
            control.KeyUp += SearchControl_KeyUp;
        }
    }

    private async Task InitDictItem()
    {
        List<SystemTypeInfo> systemList = await _systemTypeBLL.GetAllAsync();
        foreach (SystemTypeInfo info in systemList)
        {
            txtSystemType.Properties.Items.Add(new CListItem(info.Name, info.Oid));
        }
        txtSystemType.Properties.Items.Add(new CListItem("所有", ""));
    }

    void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
    {
        if (e.Column.FieldName == "LastUpdated")
        {
            e.Column.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
        }
    }

    /// <summary>
    /// 对显示的字段内容进行转义
    /// </summary>
    void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
    {
        if (e.Column.ColumnType == typeof(DateTime))
        {
            string columnName = e.Column.FieldName;                
            if (e.Value != null && Convert.ToDateTime(e.Value) <= Convert.ToDateTime("1900-1-1"))
            {
                e.DisplayText = "";
            }
        }
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
            SetGridColumWidth("ID", 60);
            SetGridColumWidth("User_ID", 80);
            SetGridColumWidth("LoginName", 60);
            SetGridColumWidth("Company_ID", 80);
            SetGridColumWidth("Note", 200);
            SetGridColumWidth("LastUpdated", 150);
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
        await InitDictItem();
        await InitTree();
        await BindData();
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
    /// 分页控件全部导出操作前的操作
    /// </summary> 
    private async void winGridViewPager1_OnStartExport(object sender, EventArgs e)
    {
        winGridViewPager1.AllToExport = await _bll.GetAllAsync();
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
        return new NameValueCollection
        {
            { LoginLogInfo.FieldLoginName, txtLoginName.Text },
            { LoginLogInfo.FieldNote, txtNote.Text.Trim() },
            { LoginLogInfo.FieldFullName, txtRealName.Text.Trim() },
            { LoginLogInfo.FieldLastUpdated, dateTimePicker1.DateTime.ToString("yyyy-MM-dd") },
            { LoginLogInfo.FieldLastUpdated, dateTimePicker2.DateTime.AddDays(1).ToString("yyyy-MM-dd") },
            { LoginLogInfo.FieldSystemTypeId, txtSystemType.GetComboBoxValue() },
            { LoginLogInfo.FieldIPAddress, txtIPAddress.Text.Trim() },
            { LoginLogInfo.FieldMacAddress, txtMacAddress.Text.Trim() }
        }.ToCListItems();
    }
        
    /// <summary>
    /// 绑定列表数据
    /// </summary>
    private async Task BindData()
    {
        #region 添加别名解析
        winGridViewPager1.DisplayColumns = "ID,User_ID,LoginName,FullName,Company_ID,CompanyName,Note,IPAddress,MacAddress,SystemType_ID,LastUpdated";
        winGridViewPager1.ColumnNameAlias = await _bll.GetColumnNameAliasAsync();//字段列显示名称转义
        //this.winGridViewPager1.AddColumnAlias("ID", "编号");
        //this.winGridViewPager1.AddColumnAlias("User_ID", "登录用户ID");
        //this.winGridViewPager1.AddColumnAlias("LoginName", "登录名");
        //this.winGridViewPager1.AddColumnAlias("FullName", "真实名称");
        //this.winGridViewPager1.AddColumnAlias("Note", "日志描述");
        //this.winGridViewPager1.AddColumnAlias("IPAddress", "IP地址");
        //this.winGridViewPager1.AddColumnAlias("MacAddress", "Mac地址");
        //this.winGridViewPager1.AddColumnAlias("LastUpdated", "记录日期");
        //this.winGridViewPager1.AddColumnAlias("SystemType_ID", "系统类型");

        #endregion

        CListItem[] condition = GetConditionSql();
        PageInput pagerInfo = winGridViewPager1.PagerInfo.Adapt<PageInput>();
        PageResult<LoginLogInfo> list = await _bll.GetEntitiesByPageAsync(new PaginatedSearchInfos(condition, pagerInfo));
        winGridViewPager1.InitDataSource(list, "用户登录日志信息报表");
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
    /// 提供给控件回车执行查询的操作
    /// </summary>
    private void SearchControl_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnSearch_Click(sender, e);
        }
    }

    private async void btnDeleteMonthLog_Click(object sender, EventArgs e)
    {
        if ("您确定删除一个月前的日志记录么？".ShowYesNoAndUxTips() == DialogResult.No)
        {
            return;
        }

        try
        {
            await _bll.DeleteMonthLogAsync();
            await BindData();
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();
            ex.Message.ShowUxError();
        }
    }

    private string _moduleName = "用户登录日志信息";
    /// <summary>
    /// 导出Excel的操作
    /// </summary>
    private async void btnExport_Click(object sender, EventArgs e)
    {
        string file = FileDialogHelper.SaveExcel($"{_moduleName}.xls");
        if (!string.IsNullOrEmpty(file))
        {
            CListItem[] condition = GetConditionSql();
            List<LoginLogInfo> list = await _bll.FindAsync(condition);
            DataTable dtNew = DataTableHelper.CreateTable("序号|int,登录用户ID,登录名,真实名称,所属公司ID,所属公司名称,日志描述,IP地址,Mac地址,更新时间,系统编号");
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
                dr["日志描述"] = list[i].Note;
                dr["IP地址"] = list[i].IPAddress;
                dr["Mac地址"] = list[i].MacAddress;
                dr["更新时间"] = list[i].LastUpdated;
                dr["系统编号"] = list[i].SystemTypeId;
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

    private async Task InitTree()
    {
        treeView1.BeginUpdate();
        treeView1.Nodes.Clear();
        //添加一个未分类和全部客户的组别
        TreeNode topNode = new TreeNode("所有记录", 0, 0);
        treeView1.Nodes.Add(topNode);

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

        TreeNode companyNode = new TreeNode("所属公司", 1, 1);
        treeView1.Nodes.Add(companyNode);
        foreach (OUInfo info in companyList)
        {
            //添加公司节点
            TreeNode subNode = new TreeNode(info.Name, 1, 1)
            {
                Tag = new NameValueCollection() { { LoginLogInfo.FieldCompanyId, info.HandNo } }
            };
            companyNode.Nodes.Add(subNode);

            //下面在添加系统类型节点
            List<SystemTypeInfo> typeList = await _systemTypeBLL.GetAllAsync();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                TreeNode typeNode = new TreeNode(typeInfo.Name, 2, 2)
                {
                    Tag = new NameValueCollection()
                    {
                        { LoginLogInfo.FieldCompanyId, info.HandNo },
                        { LoginLogInfo.FieldSystemTypeId, typeInfo.Oid }
                    }
                };
                subNode.Nodes.Add(typeNode);
            }

            TreeNode securityNode = new TreeNode("权限管理系统", 2, 2)
            {
                Tag = new NameValueCollection()
                {
                    { LoginLogInfo.FieldCompanyId, info.HandNo },
                    { LoginLogInfo.FieldSystemTypeId, "Security" }
                }
            };
            subNode.Nodes.Add(securityNode);
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

    private async void menuTree_Refresh_Click(object sender, EventArgs e)
    {
        await InitTree();
    }
}