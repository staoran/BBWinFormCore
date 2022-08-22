using System.Drawing;
using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Control.Security;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Tools.Format;
using BB.Entity.Security;
using BB.HttpServices.Core.OU;
using BB.HttpServices.Core.Role;
using BB.HttpServices.Core.User;

namespace BB.Security.UI;

public partial class FrmSelectUser : BaseForm
{
    private Dictionary<string, string> _mSelectUserDict = new Dictionary<string,string>();
    private readonly UserHttpService _bll;
    private readonly OUHttpService _oubll;
    private readonly RoleHttpService _roleBLL;
    private readonly UserRoleHttpService _userRoleBll;

    /// <summary>
    /// 选择的角色/人员/部门/业务相关人员的字典数据（实际数据）
    /// </summary>
    public Dictionary<string, string> SelectUserDict
    {
        get => _mSelectUserDict;
        set => _mSelectUserDict = new Dictionary<string, string>(value);
    }

    public FrmSelectUser(UserHttpService bll, OUHttpService ouBll, RoleHttpService roleBll,
        UserRoleHttpService userRoleBll)
    {
        InitializeComponent();

        _bll = bll;
        _oubll = ouBll;
        _roleBLL = roleBll;
        _userRoleBll = userRoleBll;

        winGridView1.ShowCheckBox = true;
        winGridView1.ShowExportButton = false;
        winGridView1.ShowLineNumber = true;
        winGridView1.BestFitColumnWith = false;//是否设置为自动调整宽度，false为不设置
        winGridView1.OnRefresh += winGridView1_OnRefresh;
        winGridView1.gridView1.DataSourceChanged += gridView1_DataSourceChanged;
    }

    /// <summary>
    /// 编写初始化窗体的实现，可以用于刷新
    /// </summary>
    public override async Task FormOnLoad()
    {
        if (!DesignMode)
        {
            await InitDeptTree();
            await InitRoleTree();
        }
    }

    /// <summary>
    /// 绑定数据后，分配各列的宽度
    /// </summary>
    private void gridView1_DataSourceChanged(object sender, EventArgs e)
    {
        if (winGridView1.gridView1.Columns.Count > 0 && winGridView1.gridView1.RowCount > 0)
        {
            //统一设置100宽度
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in winGridView1.gridView1.Columns)
            {
                column.Width = 100;
            }

            //可特殊设置特别的宽度
            SetGridColumWidth("Gender", 50);
            SetGridColumWidth("Email", 150);
            SetGridColumWidth("Note", 150);
        }

    }

    private void SetGridColumWidth(string columnName, int width)
    {
        DevExpress.XtraGrid.Columns.GridColumn column = winGridView1.gridView1.Columns.ColumnByFieldName(columnName);
        if (column != null)
        {
            column.Width = width;
        }
    }

    async void winGridView1_OnRefresh(object sender, EventArgs e)
    {
        await BindGridData();
    }

    private async Task BindGridData()
    {
        List<UserInfo> list = new();
        if (treeDept.SelectedNode is { Tag: { } })
        {
            var ouId = treeDept.SelectedNode.Tag.ObjToStr();
            list = await _bll.FindByDeptAsync(ouId);
        }
        else if (treeRole.SelectedNode is { Tag: { } })
        {
            int roleId = treeRole.SelectedNode.Tag.ToString().ToInt32();
            list = await _userRoleBll.GetUsersByRoleAsync(roleId);
        }

        //entity
        winGridView1.DisplayColumns = "HandNo,Name,FullName,Title,MobilePhone,OfficePhone,Email,Gender,QQ,Note";
        winGridView1.ColumnNameAlias = await _bll.GetColumnNameAliasAsync();//字段列显示名称转义

        winGridView1.DataSource = list;
    }

    /// <summary>
    /// 刷新选择信息
    /// </summary>
    private void RefreshSelectItems()
    {
        flowLayoutPanel1.Controls.Clear();
        foreach (string key in SelectUserDict.Keys)
        {
            string info = SelectUserDict[key];
            if (!string.IsNullOrEmpty(info))
            {
                UserNameControl control = new UserNameControl();
                control.BindData(key, info);
                control.OnDeleteItem += control_OnDeleteItem;
                flowLayoutPanel1.Controls.Add(control);
            }
        }
        lblItemCount.Text = $"当前选择【{SelectUserDict.Keys.Count}】项目";
    }

    void control_OnDeleteItem(string id)
    {
        if (SelectUserDict.ContainsKey(id))
        {
            SelectUserDict.Remove(id);
            RefreshSelectItems();
        }
    }

    private void FrmSelectFlowUser_Load(object sender, EventArgs e)
    {
        if (!DesignMode)
        {
            RefreshSelectItems();
        }
    }

    private async Task InitDeptTree()
    {
        treeDept.BeginUpdate();
        treeDept.Nodes.Clear();            

        List<OUInfo> list = await GB.GetMyTopGroup();
        foreach (OUInfo groupInfo in list)
        {
            if (groupInfo != null)
            {
                TreeNode topnode = new TreeNode
                {
                    Text = groupInfo.Name,
                    Name = groupInfo.HandNo,
                    Tag = groupInfo.HandNo,
                    ImageIndex = GB.GetImageIndex(groupInfo.Category),
                    SelectedImageIndex = GB.GetImageIndex(groupInfo.Category)
                };

                List<OUNodeInfo> sublist = await _oubll.GetTreeByIdAsync(groupInfo.HandNo);
                AddDept(sublist, topnode);

                treeDept.Nodes.Add(topnode);
            }
        }
        treeDept.ExpandAll();
        treeDept.EndUpdate();
    }

    private void AddDept(List<OUNodeInfo> list, TreeNode treeNode)
    {
        foreach (OUNodeInfo ouInfo in list)
        {
            TreeNode deptNode = new TreeNode
            {
                Text = ouInfo.Name,
                Tag = ouInfo.HandNo,
                ImageIndex = GB.GetImageIndex(ouInfo.Category),
                SelectedImageIndex = GB.GetImageIndex(ouInfo.Category)
            };
            if (ouInfo.Deleted)
            {
                deptNode.ForeColor = Color.Red;
                continue;//跳过不显示
            }
            treeNode.Nodes.Add(deptNode);

            AddDept(ouInfo.Children, deptNode);
        }
    }

    private async Task InitRoleTree()
    {
        treeRole.BeginUpdate();
        treeRole.Nodes.Clear();

        List<OUInfo> list = await GB.GetMyTopGroup();
        foreach (OUInfo groupInfo in list)
        {
            if (groupInfo != null)
            {
                TreeNode topnode = AddOuNode(groupInfo);
                await AddRole(groupInfo, topnode);

                if (groupInfo.Category == "集团")
                {
                    List<OUInfo> sublist = await _oubll.GetAllCompanyAsync(groupInfo.HandNo);
                    foreach (OUInfo info in sublist)
                    {
                        if (!info.Deleted)
                        {
                            TreeNode ouNode = AddOuNode(info, topnode);
                            await AddRole(info, ouNode);
                        }
                    }
                }
                treeRole.Nodes.Add(topnode);
            }
        }

        treeRole.ExpandAll();
        treeRole.EndUpdate();
    }

    private TreeNode AddOuNode(OUInfo ouInfo, TreeNode parentNode = null)
    {
        TreeNode ouNode = new TreeNode
        {
            Text = ouInfo.Name,
            Name = ouInfo.HandNo,
            Tag = ouInfo //机构信息放到Tag里面
        };
        if (ouInfo.Deleted)
        {
            ouNode.ForeColor = Color.Red;
        }
        ouNode.ImageIndex = GB.GetImageIndex(ouInfo.Category);
        ouNode.SelectedImageIndex = GB.GetImageIndex(ouInfo.Category);

        if (parentNode != null)
        {
            parentNode.Nodes.Add(ouNode);
        }

        return ouNode;
    }

    private async Task AddRole(OUInfo ouInfo, TreeNode treeNode)
    {
        List<RoleInfo> roleList = await _roleBLL.GetRolesByCompanyAsync(ouInfo.HandNo);
        foreach (RoleInfo roleInfo in roleList)
        {
            TreeNode roleNode = new TreeNode
            {
                Text = roleInfo.Name,
                Tag = roleInfo.ID,
                ImageIndex = 5,
                SelectedImageIndex = 5
            };
            if (ouInfo.Deleted)
            {
                roleNode.ForeColor = Color.Red;
                continue;//跳过不显示
            }

            treeNode.Nodes.Add(roleNode);
        }
    }

    private void btnClearAll_Click(object sender, EventArgs e)
    {
        SelectUserDict = new Dictionary<string, string>();
        RefreshSelectItems();
        DialogResult = DialogResult.OK;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (SelectUserDict.Count == 0)
        {
            "您还未选择人员".ShowUxTips();
            return;
        }

        DialogResult = DialogResult.OK;
    }

    private async void treeDept_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (!DesignMode)
        {
            treeRole.SelectedNode = null;//检索部门的时候，去除角色的选择
            await BindGridData();
        }
    }

    private async void treeRole_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (!DesignMode)
        {
            treeDept.SelectedNode = null;//检索角色的时候，去掉部门的选择
            await BindGridData();
        }
    }

    private void btnAddUser_Click(object sender, EventArgs e)
    {
        List<int> list = winGridView1.GetCheckedRows();
        foreach(int rowIndex in list)
        {
            string id = winGridView1.GridView1.GetRowCellDisplayText(rowIndex, "ID");
            string name= winGridView1.GridView1.GetRowCellDisplayText(rowIndex, "Name");
            string fullName = winGridView1.GridView1.GetRowCellDisplayText(rowIndex, "FullName");
            string displayname = $"{fullName}（{name}）";

            if (!SelectUserDict.ContainsKey(id))
            {
                SelectUserDict.Add(id, displayname);
            }
        }

        RefreshSelectItems();
    }

}