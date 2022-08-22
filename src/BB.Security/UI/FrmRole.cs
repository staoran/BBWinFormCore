using System.Drawing;
using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Tools.Entity;
using BB.Tools.Format;
using BB.Entity.Security;
using BB.HttpServices.Core.Function;
using BB.HttpServices.Core.OU;
using BB.HttpServices.Core.Role;
using BB.HttpServices.Core.RoleData;
using BB.HttpServices.Core.SystemType;
using BB.HttpServices.Core.User;
using Furion;
using Furion.Logging.Extensions;

namespace BB.Security.UI;

/// <summary>
/// 角色管理模块
/// </summary>
public partial class FrmRole : BaseForm
{
    private string _currentId = string.Empty;
    //用来记录用户选择的公司，部门列表
    List<string> _companyDataList = new();
    List<string> _deptDataList = new();
    private readonly RoleHttpService _bll;
    private readonly OUHttpService _ouBll;
    private readonly FunctionHttpService _functionBll;
    private readonly UserHttpService _userBll;
    private readonly SystemTypeHttpService _systemTypeBll;
    private readonly RoleDataHttpService _roleDataBll;
    private readonly UserRoleHttpService _userRoleBll;
    private readonly OURoleHttpService _ouRoleBll;
    private readonly RoleFunctionHttpService _roleFunctionBll;

    public FrmRole(RoleHttpService bll, OUHttpService ouBll, FunctionHttpService functionBll, UserHttpService userBll,
        SystemTypeHttpService systemTypeBll, RoleDataHttpService roleDataBll, UserRoleHttpService userRoleBll,
        OURoleHttpService ouRoleBll, RoleFunctionHttpService roleFunctionBll)
    {
        InitializeComponent();
        _bll = bll;
        _ouBll = ouBll;
        _functionBll = functionBll;
        _userBll = userBll;
        _systemTypeBll = systemTypeBll;
        _roleDataBll = roleDataBll;
        _userRoleBll = userRoleBll;
        _ouRoleBll = ouRoleBll;
        _roleFunctionBll = roleFunctionBll;
    }

    private async void FrmRole_Load(object sender, EventArgs e)
    {
        if (!DesignMode)
        {
            InitDictItem();                
            await InitTreeFunction();
            await RefreshTreeView();
        }
    }

    private void InitDictItem()
    {
        //初始化分类
        string[] enumNames = EnumHelper.GetMemberNames<RoleCategoryEnum>();
        txtCategory.Properties.Items.Clear();
        foreach (string item in enumNames)
        {
            txtCategory.Properties.Items.Add(item);
        }
    }
        
    private async Task RefreshTreeView()
    {
        treeView1.BeginUpdate();
        treeView1.Nodes.Clear();

        List<OUInfo> list = await GB.GetMyTopGroup();
        foreach (OUInfo groupInfo in list)
        {
            if (groupInfo is { Deleted: false })
            {
                TreeNode topnode = AddOuNode(groupInfo);
                await AddRole(groupInfo, topnode);

                if (groupInfo.Category == "集团")
                {
                    List<OUInfo> sublist = await _ouBll.GetAllCompanyAsync(groupInfo.HandNo);
                    foreach (OUInfo info in sublist)
                    {
                        if (!info.Deleted)
                        {
                            TreeNode ouNode = AddOuNode(info, topnode);
                            await AddRole(info, ouNode);
                        }
                    }
                }
                treeView1.Nodes.Add(topnode);
            }
        }
           
        treeView1.ExpandAll();
        treeView1.EndUpdate();
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
        List<RoleInfo> roleList = await _bll.GetRolesByCompanyAsync(ouInfo.HandNo);
        foreach (RoleInfo roleInfo in roleList)
        {
            TreeNode roleNode = new TreeNode
            {
                Text = roleInfo.Name,
                Tag = roleInfo, //角色信息放到Tag里面
                ImageIndex = 3,
                SelectedImageIndex = 3
            };
            if (ouInfo.Deleted)
            {
                roleNode.ForeColor = Color.Red;
                continue;//跳过不显示
            }

            treeNode.Nodes.Add(roleNode);
        }
    }

    private List<string> _addedFunctionList = new();//增加的功能列表
    private List<string> _deletedFunctionList = new();//删除的功能列表
    private void GetFunctionChanges(TreeNode node)
    {
        if (node.Tag != null)
        {
            string id = node.Tag.ToString();
            if (!node.Checked && _dictFunction.ContainsKey(id))
            {
                _deletedFunctionList.Add(id);
            }
            if (node.Checked && !_dictFunction.ContainsKey(id))
            {
                _addedFunctionList.Add(id);
            }
        }

        foreach (TreeNode subNode in node.Nodes)
        {
            GetFunctionChanges(subNode);
        }
    }

    Dictionary<string,string> _dictFunction = new();//最初的用户列表
    private async Task RefreshFunctions(int roleId)
    {
        _dictFunction = new Dictionary<string, string>();

        List<FunctionInfo> list = await _functionBll.GetFunctionsByRoleAsync(roleId);

        //增加一个字典方便快速选择
        foreach (FunctionInfo info in list)
        {
            if (!_dictFunction.ContainsKey(info.ID))
            {
                _dictFunction.Add(info.ID, info.ID);
            }
        }
                
        //如果是公司管理员一级，不能修改自己角色的权限（避免误操作，不再显示）
        bool isSuperAdmin = await _userRoleBll.UserIsSuperAdminAsync(GB.LoginUserInfo.ID);
        TreeNode selectNode = treeView1.SelectedNode;
        if (selectNode is { Text: RoleInfo.COMPANY_ADMIN_NAME } && !isSuperAdmin)
        {
            treeFunction.CheckBoxes = false;
        }
        else
        {
            treeFunction.CheckBoxes = true;
        }

        //判断角色具有哪些功能，更新勾选项
        foreach (TreeNode node in treeFunction.Nodes)
        {
            RefreshFunctionNode(node, _dictFunction);
        }
    }

    /// <summary>
    /// 为了提高速度，第一次需要构建功能树节点
    /// </summary>
    private async Task InitTreeFunction()
    {
        bool isSuperAdmin = await _userRoleBll.UserIsSuperAdminAsync(GB.LoginUserInfo.ID);
        treeFunction.BeginUpdate();
        treeFunction.Nodes.Clear();

        //初始化全部功能树
        List<SystemTypeInfo> typeList = await _systemTypeBll.GetAllAsync();
        foreach (SystemTypeInfo typeInfo in typeList)
        {
            TreeNode parentNode = treeFunction.Nodes.Add(typeInfo.Oid, typeInfo.Name, 0, 0);

            //如果是超级管理员，不根据角色获取，否则根据角色获取对应的分配权限
            //也就是说，公司管理员只能分配自己被授权的功能，而超级管理员不受限制
            List<FunctionNodeInfo> allNode = new();
            if (isSuperAdmin)
            {
                allNode = await _functionBll.GetTreeAsync(typeInfo.Oid);
            }
            else
            {
                allNode = await _functionBll.GetFunctionNodesByUserAsync(GB.LoginUserInfo.ID, typeInfo.Oid);
            }
            AddFunctionNode(parentNode, allNode);
        }
        treeFunction.ExpandAll();
        treeFunction.EndUpdate();
    }

    /// <summary>
    /// 初始化功能树
    /// </summary>
    private void AddFunctionNode(TreeNode node, List<FunctionNodeInfo> list)
    {
        foreach (FunctionNodeInfo info in list)
        {
            TreeNode subNode = new TreeNode(info.Name, 1, 1)
            {
                Tag = info.ID
            };
            node.Nodes.Add(subNode);

            AddFunctionNode(subNode, info.Children);
        }
    }
    /// <summary>
    /// 根据角色更新功能树勾选
    /// </summary>
    private void RefreshFunctionNode(TreeNode node, Dictionary<string, string> dictFunction)
    {
        foreach (TreeNode subNode in node.Nodes)
        {
            if (subNode.Tag != null && dictFunction.ContainsKey(subNode.Tag.ToString()))
            {
                subNode.Checked = true;
            }
            else
            {
                subNode.Checked = false;
            }
            RefreshFunctionNode(subNode, dictFunction);
        }
    }

    /// <summary>
    /// 记录用户的选择情况
    /// </summary>
    Dictionary<string, string> _selectUserDict = new();
    private async Task RefreshUsers(int roleId)
    {
        lvwUser.BeginUpdate();
        lvwUser.Items.Clear();//清空列表

        _selectUserDict = new Dictionary<string, string>();
        List<UserInfo> list = await _userRoleBll.GetUsersByRoleAsync(roleId);
        foreach (UserInfo info in list)
        {
            string name = $"{info.FullName}（{info.Name}）";
            CListItem item = new CListItem(name, info.ID.ToString());
            lvwUser.Items.Add(item);

            if (!_selectUserDict.ContainsKey(info.ID.ToString()))
            {
                _selectUserDict.Add(info.ID.ToString(), name);
            }
        }
        if (lvwUser.Items.Count > 0)
        {
            lvwUser.SelectedIndex = 0;
        }
        lvwUser.EndUpdate();
    }

    private async Task RefreshOUs(int roleId)
    {
        lvwOU.BeginUpdate();
        lvwOU.Items.Clear();

        List<OUInfo> list = await _ouBll.GetOUsByRoleAsync(roleId);
        foreach (OUInfo info in list)
        {
            CListItem item = new CListItem(info.Name, info.HandNo);
            lvwOU.Items.Add(item);
        }
        if (lvwOU.Items.Count > 0)
        {
            lvwOU.SelectedIndex = 0;
        }
        lvwOU.EndUpdate();
    }

    /// <summary>
    /// 右键的时候，设置当前节点为选中节点
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void treeView1_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            TreeNode node = treeView1.GetNodeAt(e.X, e.Y);
            if (node != null)
            {
                treeView1.SelectedNode = node;
            }
        }

        base.OnMouseDown(e);
    }

    private async void menu_Delete_Click(object sender, EventArgs e)
    {
        TreeNode node = treeView1.SelectedNode;
        if (node is { Tag: { } })
        {
            RoleInfo roleInfo = node.Tag as RoleInfo;
            if (roleInfo != null)
            {
                if (RoleInfo.SUPER_ADMIN_NAME.Equals(node.Text, StringComparison.OrdinalIgnoreCase))
                {
                    "保留角色不能删除".ShowUxWarning();
                    return;
                }

                if ("您确认删除吗?".ShowYesNoAndUxTips() == DialogResult.Yes)
                {
                    try
                    {
                        await _bll.SetDeletedFlagAsync(roleInfo.ID);//假删除
                        await RefreshTreeView();
                    }
                    catch (Exception ex)
                    {
                        ex.ToString().LogError();
                        ex.Message.ShowUxError();
                    }
                }
            }
        }
    }

    private void menu_Add_Click(object sender, EventArgs e)
    {
        //跳转到第一个页面
        xtraTabControl1.SelectedTabPageIndex = 0;

        ClearInput();
        _currentId = "";

        TreeNode node = treeView1.SelectedNode;
        if (node is { Tag: { } })
        {
            OUInfo ouInfo = node.Tag as OUInfo;//转换为机构对象
            if (ouInfo != null)
            {
                txtCompany.Value = ouInfo.HandNo;
            }
        }
        txtName.Focus();
    }

    private void ClearInput()
    {
        txtName.Text = "";
        txtNote.Text = "";
        treeFunction.Nodes.Clear();
        lvwOU.Items.Clear();
        lvwUser.Items.Clear();
        txtCompany.Text = "";
        txtCategory.Text = "";
        txtHandNo.Text = "";
        txtSortCode.Text = "";
    }

    private async void menu_Update_Click(object sender, EventArgs e)
    {
        await RefreshTreeView();
    }

    private void menu_ExpandAll_Click(object sender, EventArgs e)
    {
        treeView1.ExpandAll();
    }

    private async void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node != null)
        {
            if (e.Node.Tag != null)
            {
                RoleInfo info = e.Node.Tag as RoleInfo;
                if (info != null)
                {
                    _currentId = info.ID.ToString();
                    txtName.Text = info.Name;
                    txtNote.Text = info.Note;
                    txtSortCode.Text = info.SortCode;
                    txtHandNo.Text = info.HandNo;
                    txtCategory.Text = info.Category;
                    txtCompany.Value = info.CompanyId;

                    await RefreshUsers(info.ID);
                    await RefreshFunctions(info.ID);
                    await RefreshOUs(info.ID);
                    await RefreshTreeRoleData(info.ID);                        
                }
            }
            else if (e.Node.Text == "全部角色")
            {
            }
        }
    }

    private RoleInfo SetRoleInfo(RoleInfo info)
    {
        info.Name = txtName.Text;
        info.Note = txtNote.Text;
        info.CompanyName = txtCompany.Text;
        info.CompanyId = txtCompany.Value;
        info.HandNo = txtHandNo.Text;
        info.SortCode = txtSortCode.Text;
        info.Category = txtCategory.Text;
        info.Editor = GB.LoginUserInfo.FullName;
        info.LastUpdatedBy = GB.LoginUserInfo.ID.ToString();
        info.LastUpdateDate = DateTime.Now;

        info.CurrentLoginUserId = GB.LoginUserInfo.ID.ToString();
        return info;
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        #region 验证用户输入
        if (txtName.Text == "")
        {
            "角色名称不能为空".ShowUxTips();
            txtName.Focus();
            return;
        }            
        else if (txtCompany.Text == "")
        {
            "所属公司不能为空".ShowUxTips();
            txtCompany.Focus();
            return;
        }

        #endregion

        if (!string.IsNullOrEmpty(_currentId))
        {
            TreeNode node = treeView1.SelectedNode;
            if (node is { Tag: { } })
            {
                RoleInfo roleInfo = node.Tag as RoleInfo;
                if (roleInfo != null && RoleInfo.SUPER_ADMIN_NAME.Equals(roleInfo.Name, StringComparison.OrdinalIgnoreCase))
                {
                    "保留角色不能修改".ShowUxWarning();
                    return;
                }
            }

            try
            {   
                RoleInfo info = await _bll.FindByIdAsync(_currentId);
                if (info != null)
                {
                    info = SetRoleInfo(info);
                    await _bll.UpdateAsync(info);

                    await RefreshTreeView();
                }
            }
            catch (Exception ex)
            {
                ex.ToString().LogError();
                ex.Message.ShowUxError();
            }
        }
        else
        {
            if (txtName.Text.Trim() == RoleInfo.SUPER_ADMIN_NAME)
            {
                "超级管理员为保留名称，不能新增使用".ShowUxTips();
                txtName.Focus();
                return;
            }

            RoleInfo info = new RoleInfo();
            info = SetRoleInfo(info);
            info.Creator = GB.LoginUserInfo.FullName;
            info.CreatedBy = GB.LoginUserInfo.ID.ToString();
            info.CreationDate = DateTime.Now;

            try
            {
                await _bll.InsertAsync(info);
                await RefreshTreeView();
            }
            catch (Exception ex)
            {
                ex.ToString().LogError();
                ex.Message.ShowUxError();
            }
        }
    }

    private async Task DeleteFunction(string functionId, int roleId)
    {
        await _roleFunctionBll.RemoveFunctionAsync(functionId, roleId);
        await RefreshFunctions(roleId);
    }

    private async Task DeleteOu(string ouid, int roleId)
    {
        await _ouRoleBll.RemoveOuAsync(ouid, roleId);
        await RefreshOUs(roleId);
    }

    private async Task DeleteUser(int roleId, int userId)
    {
        await _userRoleBll.RemoveUserAsync(userId, roleId);
        await RefreshUsers(roleId);
    }

    private List<int> _addedUserList = new();
    private List<int> _deletedUserList = new();
    /// <summary>
    /// 获取那些变化了（增加的用户、删除的用户列表）
    /// </summary>
    /// <param name="oldDict">旧的列表</param>
    /// <param name="newDict">新的选择列表</param>
    private void GetUserDictChanges(Dictionary<string, string> oldDict, Dictionary<string, string> newDict)
    {
        _addedUserList = new List<int>();
        _deletedUserList = new List<int>();
        foreach (string key in oldDict.Keys)
        {
            if (!newDict.ContainsKey(key))
            {
                _deletedUserList.Add(key.ToInt32());
            }
        }

        foreach (string key in newDict.Keys)
        {
            if (!oldDict.ContainsKey(key))
            {
                _addedUserList.Add(key.ToInt32());
            }
        }
    }

    private async void btnEditUser_Click(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode != null)
        {
            FrmSelectUser dlg = App.GetService<FrmSelectUser>();
            dlg.SelectUserDict = _selectUserDict;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                GetUserDictChanges(_selectUserDict, dlg.SelectUserDict);

                foreach (int id in _deletedUserList)
                {
                    await _userRoleBll.RemoveUserAsync(id, _currentId.ToInt32());
                }
                foreach (int id in _addedUserList)
                {
                    await _userRoleBll.AddUserAsync(id, _currentId.ToInt32());
                }

                await RefreshUsers(_currentId.ToInt32());
            }
        }
        else
        {
            "请选择具体的角色".ShowUxTips();
        }
    }

    private async void btnRemoveUser_Click(object sender, EventArgs e)
    {
        if (lvwUser.SelectedItem != null)
        {
            CListItem userItem = lvwUser.SelectedItem as CListItem;
            if (userItem != null)
            {
                int userId = Convert.ToInt32(userItem.Value);
                if (!string.IsNullOrEmpty(_currentId))
                {
                    int roleId = Convert.ToInt32(_currentId);
                    await DeleteUser(roleId, userId);
                }
            }
        }
    }


    private List<string> _addedOuList = new();
    private List<string> _deletedOuList = new();
    /// <summary>
    /// 获取那些变化了（增加的机构、删除的机构列表）
    /// </summary>
    /// <param name="oldDict">旧的列表</param>
    /// <param name="newDict">新的选择列表</param>
    private void GetOuDictChanges(Dictionary<string, string> oldDict, Dictionary<string, string> newDict)
    {
        foreach (string key in oldDict.Keys)
        {
            if (!newDict.ContainsKey(key))
            {
                _deletedOuList.Add(key);
            }
        }

        foreach (string key in newDict.Keys)
        {
            if (!oldDict.ContainsKey(key))
            {
                _addedOuList.Add(key);
            }
        }
    }
    private async void btnEditOU_Click(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode != null)
        {
            List<OUInfo> list = await _ouBll.GetOUsByRoleAsync(_currentId.ToInt32());
            Dictionary<string, string> ouDict = new();
            foreach (OUInfo info in list)
            {
                if (!ouDict.ContainsKey(info.HandNo))
                {
                    ouDict.Add(info.HandNo, info.HandNo);
                }
            }

            FrmEditRoleOU dlg = new FrmEditRoleOU(_ouBll);
            dlg.SelectOUDict = ouDict;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                GetOuDictChanges(ouDict, dlg.SelectOUDict);

                foreach (string id in _deletedOuList)
                {
                    await _ouRoleBll.RemoveOuAsync(id, _currentId.ToInt32());
                }
                foreach (string id in _addedOuList)
                {
                    await _ouRoleBll.AddOUAsync(id, _currentId.ToInt32());
                }

                await RefreshOUs(_currentId.ToInt32());
            }
        }
        else
        {
            "请选择具体的角色".ShowUxTips();
        }
    }

    private async void btnRemoveOU_Click(object sender, EventArgs e)
    {
        if (lvwOU.SelectedItem != null)
        {
            CListItem item = lvwOU.SelectedItem as CListItem;
            if (item != null)
            {
                string ouId = item.Value;
                if (!string.IsNullOrEmpty(_currentId))
                {
                    int roleId = Convert.ToInt32(_currentId);
                    await DeleteOu(ouId, roleId);
                }
            }
        }
    }

    private async void btnEditFunction_Click(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode != null)
        {
            FrmEditTree dlg = App.GetService<FrmEditTree>();
            dlg.RoleId = _currentId;
            dlg.DisplayType = FrmEditTree.DisplayTreeType.Function;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                await RefreshFunctions(Convert.ToInt32(_currentId));
            }
        }
        else
        {
            "请选择具体的角色".ShowUxTips();
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        menu_Add_Click(sender, e);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        menu_Delete_Click(sender, e);
    }

    private void menu_Collapse_Click(object sender, EventArgs e)
    {
        treeView1.CollapseAll();
    }

    private async void btnSaveFunction_Click(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode != null)
        {
            _addedFunctionList = new List<string>();
            _deletedFunctionList = new List<string>();

            //获取相关的变化
            foreach (TreeNode node in treeFunction.Nodes)
            {
                GetFunctionChanges(node);
            }

            foreach (string id in _deletedFunctionList)
            {
                await _roleFunctionBll.RemoveFunctionAsync(id, _currentId.ToInt32());
            }
            foreach (string id in _addedFunctionList)
            {
                await _roleFunctionBll.AddFunctionAsync(id, _currentId.ToInt32());
            }

            "保存成功".ShowUxTips();
            await RefreshFunctions(_currentId.ToInt32());
        }
        else
        {
            "请选择具体的角色".ShowUxTips();
        }
    }

    private async void btnRefreshFunction_Click(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode != null)
        {
            await InitTreeFunction();
            await RefreshFunctions(_currentId.ToInt32());
        }
        else
        {
            "请选择具体的角色".ShowUxTips();
        }
    }

    private void treeFunction_AfterCheck(object sender, TreeViewEventArgs e)
    {
        CheckSelect(e.Node, e.Node.Checked);
    }

    private void CheckSelect(TreeNode node, bool selectAll)
    {
        foreach (TreeNode subNode in node.Nodes)
        {
            subNode.Checked = selectAll;

            CheckSelect(subNode, selectAll);
        }
    }


    /// <summary>
    /// 用户角色的公司、部门数据集合
    /// </summary>
    private Dictionary<string, string> _roleDataDict = new();

    private void AddRoleDataDept(List<OUNodeInfo> list, TreeNode treeNode)
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
            deptNode.Checked = _roleDataDict.ContainsKey(ouInfo.HandNo);//选中的
            treeNode.Nodes.Add(deptNode);

            AddRoleDataDept(ouInfo.Children, deptNode);
        }
    }

    /// <summary>
    /// 刷新可访问数据的角色数据权限列表
    /// </summary>
    private async Task RefreshTreeRoleData(int roleId)
    {
        _roleDataDict = await _roleDataBll.GetRoleDataDictAsync(roleId);

        treeRoleData.BeginUpdate();
        Cursor.Current = Cursors.WaitCursor;
        treeRoleData.Nodes.Clear();

        #region 增加一个所在公司、所在部门的节点
        string userCompanyId = "-1";//使用-1替代用户所在公司，获取的时候替换为对应公司ID
        string userDeptId = "-11";//使用-11替代用户所在部门, 获取的时候替换为对应部门ID
        TreeNode companyNode = new TreeNode
        {
            Text = "所在公司",
            Name = userCompanyId,
            Tag = userCompanyId,
            ImageIndex = 1,
            SelectedImageIndex = 1,
            Checked = _roleDataDict.ContainsKey(userCompanyId)
        };

        TreeNode deptNode = new TreeNode
        {
            Text = "所在部门",
            Name = userDeptId,
            Tag = userDeptId,
            ImageIndex = 2,
            SelectedImageIndex = 2,
            Checked = _roleDataDict.ContainsKey(userDeptId)
        };

        companyNode.Nodes.Add(deptNode);
        treeRoleData.Nodes.Add(companyNode); 
        #endregion

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
                    SelectedImageIndex = GB.GetImageIndex(groupInfo.Category),
                    Checked = _roleDataDict.ContainsKey(groupInfo.HandNo) //选中的
                };

                List<OUNodeInfo> sublist = await _ouBll.GetTreeByIdAsync(groupInfo.HandNo);
                AddRoleDataDept(sublist, topnode);

                treeRoleData.Nodes.Add(topnode);
            }
        }

        treeRoleData.ExpandAll();
        treeRoleData.EndUpdate();
        Cursor.Current = Cursors.Default;
    }

    private void GetRoleDataSelected(TreeNode node)
    {
        if (node.Checked && node.Tag != null)
        {
            //group 0, company 1, other dept 2,3...
            if (node.ImageIndex <= 1)
            {
                _companyDataList.Add(node.Tag.ToString());
            }
            else
            {
                _deptDataList.Add(node.Tag.ToString());
            }
        }

        foreach (TreeNode subNode in node.Nodes)
        {        
            //继续递归遍历
            GetRoleDataSelected(subNode);
        }
    }
        
    private async void btnSaveRoleData_Click(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode != null)
        {
            //初始化，并使用递归获取数据的列表
            _companyDataList = new List<string>();
            _deptDataList = new List<string>();
            foreach (TreeNode node in treeRoleData.Nodes)
            {
                GetRoleDataSelected(node);
            }

            string companyString = string.Join(",", _companyDataList.ToArray());
            string deptDataString = string.Join(",", _deptDataList);
            bool result = await _roleDataBll.UpdateRoleDataAsync(_currentId.ToInt32(), companyString, deptDataString);
            if (result)
            {
                "保存成功".ShowUxTips();
                await RefreshTreeRoleData(_currentId.ToInt32());
            }
            else
            {
                "保存失败".ShowUxTips();
            }
        }
        else
        {
            "请选择具体的角色".ShowUxTips();
        }
    }

    private async void btnRefreshRoleData_Click(object sender, EventArgs e)
    {
        if (treeView1.SelectedNode != null)
        {
            await RefreshTreeRoleData(_currentId.ToInt32());
        }
        else
        {
            //MessageDxUtil.ShowUxTips("请选择具体的角色");
        }
    }

    private void chkAllRoleData_CheckedChanged(object sender, EventArgs e)
    {
        foreach (TreeNode node in treeRoleData.Nodes)
        {
            CheckRoleDataSelect(node, chkAllRoleData.Checked);
        }
    }

    private void CheckRoleDataSelect(TreeNode node, bool selectAll)
    {
        node.Checked = selectAll;
        foreach (TreeNode subNode in node.Nodes)
        {
            CheckRoleDataSelect(subNode, selectAll);
        }
    }

    private void chkFunctionSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (TreeNode node in treeFunction.Nodes)
        {
            CheckSelect(node, chkFunctionSelectAll.Checked);
        }
    }

    private async void function_Refresh_Click(object sender, EventArgs e)
    {
        await InitTreeFunction();
    }

    private void function_Collapse_Click(object sender, EventArgs e)
    {
        treeFunction.CollapseAll();
    }

    private void function_ExpandAll_Click(object sender, EventArgs e)
    {
        treeFunction.ExpandAll();
    }
}