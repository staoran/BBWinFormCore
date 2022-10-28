using System.Drawing;
using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Tools.Entity;
using BB.Tools.Format;
using BB.Entity.Security;
using BB.HttpServices.Core.OU;
using BB.HttpServices.Core.Role;
using BB.HttpServices.Core.User;
using Furion;
using Furion.Logging.Extensions;

namespace BB.Security.UI;

public partial class FrmOu : BaseForm
{
    private string _currentId = string.Empty;

    private List<int> _addedUserList = new();
    private List<int> _deletedUserList = new();
    private readonly OUHttpService _bll;
    private readonly OURoleHttpService _ouRoleBll;
    private readonly OUUserHttpService _ouUserBll;

    public FrmOu(OUHttpService bll, OURoleHttpService ouRoleBll, OUUserHttpService ouUserBll)
    {
        InitializeComponent();
        _bll = bll;
        _ouRoleBll = ouRoleBll;
        _ouUserBll = ouUserBll;
    }

    private async void FrmOU_Load(object? sender, EventArgs e)
    {
        if (!DesignMode)
        {
            await RefreshTreeView();
            InitDictItem();
        }
    }

    private void InitDictItem()
    {
        //初始化分类
        string[] enumNames = EnumHelper.GetMemberNames<OuCategoryEnum>();
        txtCategory.Properties.Items.Clear();
        foreach (string item in enumNames)
        {
            txtCategory.Properties.Items.Add(item);
        }
    }

    private async Task RefreshTreeView()
    {
        treeView1.Nodes.Clear();
        treeView1.BeginUpdate();
        Cursor.Current = Cursors.WaitCursor;

        List<OUInfo> list = await GB.GetMyTopGroup();
        foreach (OUInfo groupInfo in list)
        {
            //不显示删除的机构
            if (groupInfo is { Deleted: false })
            {
                TreeNode topnode = new TreeNode
                {
                    Text = groupInfo.Name,
                    Name = groupInfo.HandNo,
                    Tag = groupInfo.HandNo,
                    ImageIndex = GB.GetImageIndex(groupInfo.Category),
                    SelectedImageIndex = GB.GetImageIndex(groupInfo.Category)
                };
                treeView1.Nodes.Add(topnode);

                List<OUNodeInfo> sublist = await _bll.GetTreeByIdAsync(groupInfo.HandNo);
                AddOuNode(sublist, topnode);
            }
        }

        Cursor.Current = Cursors.Default;
        treeView1.EndUpdate();
        treeView1.ExpandAll();
    }

    private void AddOuNode(List<OUNodeInfo> list, TreeNode parentNode)
    {
        foreach (OUNodeInfo ouInfo in list)
        {
            TreeNode ouNode = new TreeNode
            {
                Text = ouInfo.Name,
                Name = ouInfo.HandNo,
                Tag = ouInfo.HandNo
            };
            if (ouInfo.Deleted)
            {
                ouNode.ForeColor = Color.Red;
                continue;//跳过不显示
            }
            ouNode.ImageIndex = GB.GetImageIndex(ouInfo.Category);
            ouNode.SelectedImageIndex = GB.GetImageIndex(ouInfo.Category);
            parentNode.Nodes.Add(ouNode);

            AddOuNode(ouInfo.Children, ouNode);
        }
    }

    private async Task RefreshRoles(string id)
    {
        lvwRole.BeginUpdate();
        lvwRole.Items.Clear();
        List<RoleInfo> list = await _ouRoleBll.GetRolesByOuAsync(id);
        foreach (RoleInfo info in list)
        {
            CListItem item = new CListItem(info.Name, info.ID.ToString());
            lvwRole.Items.Add(item);
        }
        if (lvwRole.Items.Count > 0)
        {
            lvwRole.SelectedIndex = 0;
        }
        lvwRole.EndUpdate();
    }

    /// <summary>
    /// 记录用户的选择情况
    /// </summary>
    Dictionary<string, string> _selectUserDict = new();
    private async Task RefreshUsers(string id)
    {
        _selectUserDict = new Dictionary<string, string>();

        lvwUser.BeginUpdate();
        lvwUser.Items.Clear();
        List<SimpleUserInfo> list = await _ouUserBll.GetSimpleUsersByOuAsync(id);
        foreach (SimpleUserInfo info in list)
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
        
    private void treeView1_MouseDown(object? sender, MouseEventArgs e)
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

    private async void menu_Delete_Click(object? sender, EventArgs e)
    {
        TreeNode node = treeView1.SelectedNode;
        if (node != null && !string.IsNullOrEmpty(node.Name))
        {
            if ("您确认删除吗?".ShowYesNoAndUxTips() == DialogResult.Yes)
            {
                try
                {
                    await _bll.SetDeletedFlagAsync(node.Name);
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

    private void menu_Add_Click(object? sender, EventArgs e)
    {
        ClearInput();
        _currentId = "";

        TreeNode node = treeView1.SelectedNode;
        if (node is { Tag: { } })
        {
            cmbUpperOU.Value = node.Tag.ToString();
        }

        txtName.Focus();
    }

    private void ClearInput()
    {
        txtName.Text = "";
        txtNote.Text = "";
        txtAddress.Text = "";
        txtHandNo.Text = "";
        //this.txtSortCode.Text = "";
        txtCreator.Text = GB.LoginUserInfo.FullName;
        txtCreationDate.Text = DateTime.Now.ToString();
        txtInnerPhone.Text = "";
        txtOuterPhone.Text = "";

        lvwRole.Items.Clear();
        lvwUser.Items.Clear();

        InitDictItem();
    }

    private async Task<OUInfo> SetOuInfo(OUInfo info)
    {
        info.Name = txtName.Text;
        info.Note = txtNote.Text;
        info.Address = txtAddress.Text;
        info.InnerPhone = txtInnerPhone.Text;
        info.OuterPhone = txtOuterPhone.Text;
        info.HandNo = txtHandNo.Text;
        info.SortCode = txtSortCode.Text;
        info.Category = txtCategory.Text;
        info.Editor = GB.LoginUserInfo.FullName;
        info.LastUpdatedBy = GB.LoginUserInfo.ID.ToString();
        info.LastUpdateDate = DateTime.Now;
        info.PID = cmbUpperOU.Value;

        OUInfo pInfo = await _bll.FindByIdAsync(info.PID);
        if (pInfo != null)
        {   
            //pInfo.Category == "集团" ||
            if ( pInfo.Category == "公司")
            {
                info.CompanyId = pInfo.HandNo;
                info.CompanyName = pInfo.Name;
            }
            else if (pInfo.Category == "部门" || pInfo.Category == "工作组")
            {
                info.CompanyId = pInfo.CompanyId;
                info.CompanyName = pInfo.CompanyName;
            }
        }
        info.CurrentLoginUserId = GB.LoginUserInfo.ID.ToString();
        return info;
    }

    private async void menu_Update_Click(object? sender, EventArgs e)
    {
        await RefreshTreeView();
    }

    private void menu_ExpandAll_Click(object? sender, EventArgs e)
    {
        treeView1.ExpandAll();
    }

    private void menu_Collapse_Click(object? sender, EventArgs e)
    {
        treeView1.CollapseAll();
    }

    public TreeNode GetNodeTopParent(TreeNode n)
    {
        TreeNode returnNode = null;
        if (n.Parent == null)
        {
            returnNode = n;
        }
        else
        {
            returnNode = GetNodeTopParent(n.Parent);
        }
        return returnNode;
    }

    private async void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node != null)
        {
            string id = e.Node.Name;
            OUInfo info = await _bll.FindByIdAsync(id);

            if (info != null)
            {
                _currentId = id;

                txtName.Text = info.Name;
                txtNote.Text = info.Note;
                txtAddress.Text = info.Address;
                txtSortCode.Text = info.SortCode;
                txtHandNo.Text = info.HandNo;
                txtOuterPhone.Text = info.OuterPhone;
                txtInnerPhone.Text = info.InnerPhone;
                txtCategory.Text = info.Category;
                txtCreator.Text = info.Creator;
                txtCreationDate.Text = info.CreationDate.ToString();

                //由于选择的节点不同，因此根据用户选择的最顶级机构ID进行初始化，才能列出指定机构下的层次关系
                TreeNode topTreeNode = GetNodeTopParent(e.Node);
                if(topTreeNode is { Tag: { } })
                {
                    string topOuId = topTreeNode.Tag.ToString();
                    cmbUpperOU.ParentOUId = topOuId;
                    cmbUpperOU.Init();
                }
                OUInfo info2 = await _bll.FindByIdAsync(info.PID);
                if (info2 != null)
                {
                    cmbUpperOU.Value = info.PID.ToString();
                }
                else
                {
                    cmbUpperOU.Value = "-1";
                }

                //如果是公司管理员，不能编辑公司的信息
                if (GB.LoginUserInfo.CompanyId == _currentId &&
                    GB.UserInRole(RoleInfo.COMPANY_ADMIN_NAME))
                {
                    btnSave.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                }

                await RefreshUsers(id);
                await RefreshRoles(id);
            }
        }
    }

    private async void btnSave_Click(object? sender, EventArgs e)
    {
        #region 验证用户输入
        if (txtName.Text == "")
        {
            "机构名称不能为空".ShowUxTips();
            txtName.Focus();
            return;
        }
        else if (cmbUpperOU.Text == "")
        {
            "上级机构不能为空".ShowUxTips();
            cmbUpperOU.Focus();
            return;
        }

        #endregion

        if (!string.IsNullOrEmpty(_currentId))
        {                
            try
            {
                OUInfo info = await _bll.FindByIdAsync(_currentId);
                if (info != null)
                {
                    info = await SetOuInfo(info);
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
            OUInfo info = new OUInfo();
            info = await SetOuInfo(info);
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

    /// <summary>
    /// 获取那些变化了（增加的用户、删除的用户列表）
    /// </summary>
    /// <param name="oldDict">旧的列表</param>
    /// <param name="newDict">新的选择列表</param>
    private void GetUserDictChangs(Dictionary<string, string> oldDict, Dictionary<string, string> newDict)
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

    private async void btnEditUser_Click(object? sender, EventArgs e)
    {
        if (treeView1.SelectedNode != null)
        {
            FrmSelectUser dlg = App.GetService<FrmSelectUser>();
            dlg.SelectUserDict = _selectUserDict;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                GetUserDictChangs(_selectUserDict, dlg.SelectUserDict);

                foreach (int id in _deletedUserList)
                {
                    await _ouUserBll.RemoveUserAsync(id, _currentId);
                }
                foreach (int id in _addedUserList)
                {
                    await _bll.AddUserAsync(id, _currentId);
                }

                await RefreshUsers(_currentId);
            }
        }
        else
        {
            "请选择具体的机构".ShowUxTips();
        }
    }

    private async Task DeleteUser(string ouid, int userId)
    {
        await _ouUserBll.RemoveUserAsync(userId, ouid);
        await RefreshUsers(ouid);
    }

    private async void btnRemoveUser_Click(object? sender, EventArgs e)
    {
        if (lvwUser.SelectedItem != null)
        {
            CListItem userItem = lvwUser.SelectedItem as CListItem;
            if (userItem != null)
            {
                int userId = Convert.ToInt32(userItem.Value);
                if (!string.IsNullOrEmpty(_currentId))
                {
                    await DeleteUser(_currentId, userId);
                }
            }
        }
    }

    private void btnAdd_Click(object? sender, EventArgs e)
    {
        menu_Add_Click(sender, e);
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        menu_Delete_Click(sender, e);
    }

}