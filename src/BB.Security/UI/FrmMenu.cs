using System.Collections.Specialized;
using System.Data;
using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.DocViewer;
using BB.BaseUI.Extension;
using BB.BaseUI.WinForm;
using BB.Entity.Security;
using BB.HttpServices.Core.Menu;
using BB.HttpServices.Core.SystemType;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using Furion;
using Furion.Logging.Extensions;
using Mapster;

namespace BB.Security.UI;

public partial class FrmMenu : BaseForm
{
    NameValueCollection? _treeCondition = null;
    private readonly MenuHttpService _bll;
    private readonly SystemTypeHttpService _systemTypeBll;

    public FrmMenu(MenuHttpService bll, SystemTypeHttpService systemTypeBll)
    {
        InitializeComponent();

        _bll = bll;
        _systemTypeBll = systemTypeBll;

        InitDictItem();

        winGridViewPager1.OnPageChanged += winGridViewPager1_OnPageChanged;
        winGridViewPager1.OnStartExport += winGridViewPager1_OnStartExport;
        winGridViewPager1.OnEditSelected += winGridViewPager1_OnEditSelected;
        winGridViewPager1.OnAddNew += winGridViewPager1_OnAddNew;
        winGridViewPager1.OnDeleteSelected += winGridViewPager1_OnDeleteSelected;
        winGridViewPager1.OnRefresh += winGridViewPager1_OnRefresh;
        winGridViewPager1.AppendedMenu = contextMenuStrip1;
        winGridViewPager1.ShowLineNumber = true;
        winGridViewPager1.BestFitColumnWith = false;
        winGridViewPager1.gridView1.DataSourceChanged += gridView1_DataSourceChanged;

        //关联回车键进行查询
        foreach (Control control in layoutControl1.Controls)
        {
            control.KeyUp += SearchControl_KeyUp;
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
            SetGridColumWidth("Name", 150);
            SetGridColumWidth("Icon", 200);
            SetGridColumWidth("Seq", 80);
            SetGridColumWidth("Visible", 80);
            SetGridColumWidth("WinformType", 400);
            SetGridColumWidth("WebIcon", 200);
            SetGridColumWidth("Url", 200);
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
        await InitTree();
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
            FrmEditMenu dlg = App.GetService<FrmEditMenu>();
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
        await InitTree();
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
        var condition = _treeCondition ??  new NameValueCollection
        {
            { MenuInfo.FieldName, txtName.Text.Trim() },
            { MenuInfo.FieldFunctionId, txtFunctionId.Text.Trim() },
            { MenuInfo.FieldWinformType, txtWinformType.Text.Trim() },
            { MenuInfo.FieldUrl, txtUrl.Text.Trim() }
        };

        if (txtVisible.Checked)
        {
            condition.Add(MenuInfo.FieldVisible, "1");
        }

        return condition.ToDicString();
    }

    /// <summary>
    /// 绑定列表数据
    /// </summary>
    private async Task BindData()
    {
        //entity
        winGridViewPager1.DisplayColumns = "Name,Icon,Seq,FunctionId,Visible,WinformType,WebIcon,Url";
        #region 添加别名解析

        winGridViewPager1.AddColumnAlias("ID", "");
        winGridViewPager1.AddColumnAlias("Name", "显示名称");
        winGridViewPager1.AddColumnAlias("Icon", "图标");
        winGridViewPager1.AddColumnAlias("Seq", "排序");
        winGridViewPager1.AddColumnAlias("FunctionId", "功能ID");
        winGridViewPager1.AddColumnAlias("Visible", "菜单可见");
        winGridViewPager1.AddColumnAlias("WinformType", "Winform窗体类型");
        winGridViewPager1.AddColumnAlias("WebIcon", "Web界面的菜单图标");
        winGridViewPager1.AddColumnAlias("Url", "Web界面Url地址");

        #endregion

        Dictionary<string,string> condition = GetConditionSql();
        PageInput pagerInfo = winGridViewPager1.PagerInfo.Adapt<PageInput>();
        PageResult<MenuInfo> list = await _bll.GetEntitiesByPageAsync(new PaginatedSearchInfos(condition, pagerInfo));
        winGridViewPager1.InitDataSource(list, "功能菜单信息报表");
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
        string selectId = "";
        string systemType = "";
        TreeNode node = treeView1.SelectedNode;
        if (node != null)
        {
            if (node.Tag != null)
            {
                MenuNodeInfo menuInfo = node.Tag as MenuNodeInfo;
                if (menuInfo != null)
                {
                    selectId = menuInfo.ID;
                    systemType = menuInfo.SystemTypeId;
                }
            }
            else
            {
                systemType = node.Name;
            }
        }

        FrmEditMenu dlg = App.GetService<FrmEditMenu>();
        dlg.PID = selectId;
        dlg.SystemTypeId = systemType;
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

    private string _moduleName = "功能菜单";

    /// <summary>
    /// 导出Excel的操作
    /// </summary>
    private async void btnExport_Click(object sender, EventArgs e)
    {
        string file = FileDialogHelper.SaveExcel($"{_moduleName}.xls");
        if (!string.IsNullOrEmpty(file))
        {
            List<MenuInfo> list = await _bll.GetAllAsync();
            DataTable dtNew = DataTableHelper.CreateTable("序号|int,父ID,显示名称,图标,排序,功能ID,菜单可见,Winform窗体类型,Web界面的菜单图标,Web界面Url地址,系统编号");
            DataRow dr;
            int j = 1;
            for (int i = 0; i < list.Count; i++)
            {
                dr = dtNew.NewRow();
                dr["序号"] = j++;
                dr["父ID"] = list[i].PID;
                dr["显示名称"] = list[i].Name;
                dr["图标"] = list[i].Icon;
                dr["排序"] = list[i].Seq;
                dr["功能ID"] = list[i].FunctionId;
                dr["菜单可见"] = list[i].Visible;
                dr["Winform窗体类型"] = list[i].WinformType;
                dr["Web界面的菜单图标"] = list[i].WebIcon;
                dr["Web界面Url地址"] = list[i].Url;
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

    /// <summary>
    /// 绑定树形数据
    /// </summary>
    private async Task InitTree()
    {
        treeView1.Nodes.Clear();
        treeView1.BeginUpdate();
        Cursor.Current = Cursors.WaitCursor;

        //先获取系统类型，然后对不同的系统类型下的菜单进行绑定显示
        List<SystemTypeInfo> typeList = await _systemTypeBll.GetAllAsync();
        foreach (SystemTypeInfo typeInfo in typeList)
        {
            TreeNode pNode = new TreeNode
            {
                Text = typeInfo.Name, //系统类型节点
                Name = typeInfo.Oid,
                ImageIndex = 0,
                SelectedImageIndex = 0
            };
            treeView1.Nodes.Add(pNode);

            string systemType = typeInfo.Oid;//系统标识ID

            //绑定树控件
            //一般情况下，对Ribbon样式而言，一级菜单表示RibbonPage；二级菜单表示PageGroup;三级菜单才是BarButtonItem最终的菜单项。
            List<MenuNodeInfo> menuList = await _bll.GetTreeAsync(systemType);
            foreach (MenuNodeInfo info in menuList)
            {
                TreeNode item = new TreeNode
                {
                    Name = info.ID,
                    Text = info.Name, //一级菜单节点
                    Tag = info, //对菜单而言，记录其MenuNodeInfo到Tag中，作为判断依据
                    ImageIndex = 1,
                    SelectedImageIndex = 1
                };
                pNode.Nodes.Add(item);

                AddChildNode(info.Children, item);
            }
        }

        Cursor.Current = Cursors.Default;
        treeView1.EndUpdate();
        treeView1.ExpandAll();
    }

    private void AddChildNode(List<MenuNodeInfo> list, TreeNode fnode)
    {
        foreach (MenuNodeInfo info in list)
        {
            TreeNode item = new TreeNode
            {
                Name = info.ID,
                Text = info.Name, //二、三级菜单节点
                Tag = info //对菜单而言，记录其MenuNodeInfo到Tag中，作为判断依据
            };
            int index = (fnode.ImageIndex + 1 > 3) ? 3 : fnode.ImageIndex + 1;
            item.ImageIndex = index;
            item.SelectedImageIndex = index; 
            fnode.Nodes.Add(item);

            AddChildNode(info.Children, item);
        }
    }


    private async void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node != null)
        {
            if (e.Node.Tag != null)
            {
                string menuId = e.Node.Name;
                _treeCondition =  new NameValueCollection
                {
                    { MenuInfo.FieldPID, menuId }
                };
                await BindData();
            }
            else
            {
                string systemTypeId = e.Node.Name;
                _treeCondition = new NameValueCollection
                {
                    { MenuInfo.FieldSystemTypeId, systemTypeId }
                };
                await BindData();
            }
        }
    }

    private async void ctxMenuTree_Refresh_Click(object sender, EventArgs e)
    {
        await InitTree();
    }

    private void SelectTreeItem()
    {
        //当鼠标在指定的菜单项上移动的时候，同时调整树形菜单的位置
        string id = winGridViewPager1.gridView1.GetFocusedRowCellDisplayText("ID");
        if (!string.IsNullOrEmpty(id))
        {
            TreeNode node = FindNode(treeView1.Nodes, id);
            if (node != null)
            {
                treeView1.SelectedNode = node;
            }
        }
    }

    private TreeNode FindNode(TreeNodeCollection nodes, string menuId)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Tag != null)
            {
                MenuNodeInfo info = node.Tag as MenuNodeInfo;
                if (info != null && info.ID == menuId)
                {
                    return node;
                }
            }

            TreeNode candidate = FindNode(node.Nodes, menuId);
            if (candidate != null)
                return candidate;
        }

        return null;
    }

    private void txtVisible_CheckedChanged(object sender, EventArgs e)
    {
        btnSearch_Click(sender, e);
    }
}