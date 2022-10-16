using System.Windows.Forms;
using System.Collections;
using BB.BaseUI.BaseUI;
using BB.Tools.Entity;
using BB.Tools.Format;
using BB.Entity.Security;
using BB.HttpServices.Core.Function;
using BB.HttpServices.Core.OU;
using BB.HttpServices.Core.Role;
using BB.HttpServices.Core.User;

namespace BB.Security.UI;

public partial class FrmEditTree : BaseForm
{
    public enum DisplayTreeType  { OU, Role, User, Function }
    public DisplayTreeType DisplayType;
    public string RoleId = string.Empty;//在Role中查看其他相关信息的时候
    public string OUId = string.Empty;//在OU中查看用户列表的时候
    public string UserId = string.Empty;//在User的时候查看功能列表

    private List<string> _removeList = new List<string>();
    private List<string> _addList = new List<string>();

    //CListItem Text=Name, Value = PID
    private Dictionary<string, CListItem> _checkedDict = new Dictionary<string, CListItem>();//记录用户原来选择的内容
    private Dictionary<string, CListItem> _treeDict = new Dictionary<string, CListItem>();//记录所有列表的内容
    private readonly OUHttpService _ouBll;
    private readonly RoleHttpService _roleBll;
    private readonly UserHttpService _userBll;
    private readonly FunctionHttpService _functionBll;
    private readonly OURoleHttpService _ouRoleBll;
    private readonly RoleFunctionHttpService _roleFunctionBll;
    private readonly UserRoleHttpService _userRoleBll;
    private readonly OUUserHttpService _ouUserBll;

    public FrmEditTree(OUHttpService ouBll, RoleHttpService roleBll, UserHttpService userBll, FunctionHttpService functionBll,
        OURoleHttpService ouRoleBll, RoleFunctionHttpService roleFunctionBll, UserRoleHttpService userRoleBll,
        OUUserHttpService ouUserBll)
    {
        InitializeComponent();
        _ouBll = ouBll;
        _roleBll = roleBll;
        _userBll = userBll;
        _functionBll = functionBll;
        _ouRoleBll = ouRoleBll;
        _roleFunctionBll = roleFunctionBll;
        _userRoleBll = userRoleBll;
        _ouUserBll = ouUserBll;
    }

    private async void FrmEditTree_Load(object? sender, EventArgs e)
    {
        await RefreshTreeView();
    }

    private async Task RefreshTreeView()
    {
        ArrayList list = new ArrayList();
        ArrayList chechedList = new ArrayList();

        #region 根据不同条件获取不同的列表
        switch (DisplayType)
        {
            case DisplayTreeType.OU:
                #region OU
                list.AddRange(await _ouBll.GetAllAsync());

                if (!string.IsNullOrEmpty(RoleId))
                {
                    chechedList.AddRange(await _ouBll.GetOUsByRoleAsync(RoleId.ToInt32()));
                }

                foreach (OUInfo info in chechedList)
                {
                    if (!_checkedDict.ContainsKey(info.HandNo))
                    {
                        _checkedDict.Add(info.HandNo, new CListItem(info.Name, info.PID));
                    }
                }
                foreach (OUInfo info in list)
                {
                    if (!_treeDict.ContainsKey(info.HandNo))
                    {
                        _treeDict.Add(info.HandNo, new CListItem(info.Name, info.PID));
                    }
                } 
                #endregion
                break;

            case DisplayTreeType.Role:
                #region Role

                list.AddRange(await _roleBll.GetAllAsync());

                if (!string.IsNullOrEmpty(OUId))
                {
                    chechedList.AddRange(await _ouRoleBll.GetRolesByOuAsync(OUId));
                }
                if (chechedList == null)
                {
                    chechedList = new ArrayList();
                }

                foreach (RoleInfo info in chechedList)
                {
                    if (!_checkedDict.ContainsKey(info.ID.ToString()))
                    {
                        _checkedDict.Add(info.ID.ToString(), new CListItem(info.Name, "-1"));
                    }
                }
                foreach (RoleInfo info in list)
                {
                    if (!_treeDict.ContainsKey(info.ID.ToString()))
                    {
                        _treeDict.Add(info.ID.ToString(), new CListItem(info.Name, "-1"));
                    }
                } 
                #endregion
                break;

            case DisplayTreeType.User:
                #region User
                list.AddRange(await _userBll.GetAllAsync());

                if (!string.IsNullOrEmpty(RoleId))
                {
                    chechedList.AddRange(await _userRoleBll.GetUsersByRoleAsync(RoleId.ToInt32()));
                }
                else if (!string.IsNullOrEmpty(OUId))
                {
                    chechedList.AddRange(await _ouUserBll.GetUsersByOuAsync(OUId));
                }

                if (chechedList == null)
                {
                    chechedList = new ArrayList();
                }

                foreach (UserInfo info in chechedList)
                {
                    if (!_checkedDict.ContainsKey(info.ID.ToString()))
                    {
                        string name = $"{info.Name}（{info.FullName}）";
                        _checkedDict.Add(info.ID.ToString(), new CListItem(name, info.PID.ToString()));
                    }
                }
                foreach (UserInfo info in list)
                {
                    if (!_treeDict.ContainsKey(info.ID.ToString()))
                    {
                        string name = $"{info.Name}（{info.FullName}）";
                        _treeDict.Add(info.ID.ToString(), new CListItem(name, info.PID.ToString()));
                    }
                } 
                #endregion
                break;

            case DisplayTreeType.Function:
                #region Function
                list.AddRange(await _functionBll.GetAllAsync());

                if (!string.IsNullOrEmpty(RoleId))
                {
                    chechedList.AddRange(await _functionBll.GetFunctionsByRoleAsync(RoleId.ToInt32()));
                }
                else if (!string.IsNullOrEmpty(UserId))
                {
                    chechedList.AddRange(await _functionBll.GetFunctionsByUserAsync(Convert.ToInt32(UserId), ""));
                }

                if (chechedList == null)
                {
                    chechedList = new ArrayList();
                }

                foreach (FunctionInfo info in chechedList)
                {
                    if (!_checkedDict.ContainsKey(info.ID))
                    {
                        _checkedDict.Add(info.ID, new CListItem(info.Name, info.PID));
                    }
                }
                foreach (FunctionInfo info in list)
                {
                    if (!_treeDict.ContainsKey(info.ID))
                    {
                        _treeDict.Add(info.ID, new CListItem(info.Name, info.PID));
                    }
                } 
                #endregion
                break;
        } 
        #endregion

        treeView1.Nodes.Clear();
        treeView1.BeginUpdate();
        Cursor.Current = Cursors.WaitCursor;

        foreach(string key in _treeDict.Keys)
        {
            if (_treeDict[key].Value != "-1")
            {
                continue;
            }

            TreeNode item = new TreeNode
            {
                Name = key,
                Text = _treeDict[key].Text,
                Tag = _treeDict[key].Text,
                Checked = _checkedDict.ContainsKey(key)
            };
            treeView1.Nodes.Add(item);

            AddChildNode(item);
        }

        Cursor.Current = Cursors.Default;
        treeView1.EndUpdate();
        treeView1.ExpandAll();
    }

    private void AddChildNode(TreeNode fnode)
    {
        foreach (string key in _treeDict.Keys)
        {
            if (_treeDict[key].Value != fnode.Name)
            {
                continue;
            }
            TreeNode item = new TreeNode
            {
                Name = key,
                Text = _treeDict[key].Text,
                Tag = _treeDict[key].Text,
                Checked = _checkedDict.ContainsKey(key)
            };
            fnode.Nodes.Add(item);

            AddChildNode(item);
        }
    }

    private void GetChanges(TreeNode node)
    {
        string id = node.Name;
        if (!node.Checked && _checkedDict.ContainsKey(id))
        {
            _removeList.Add(id);
        }
        if (node.Checked && !_checkedDict.ContainsKey(id))
        {
            _addList.Add(id);
        }

        foreach (TreeNode subNode in node.Nodes)
        {
            GetChanges(subNode);
        }
    }

    private async void btnOK_Click(object? sender, EventArgs e)
    {
        foreach (TreeNode node in treeView1.Nodes)
        {
            GetChanges(node);
        }

        #region 根据不同条件获取不同的列表
        switch (DisplayType)
        {
            case DisplayTreeType.OU:
                foreach (string id in _removeList)
                {
                    if (!string.IsNullOrEmpty(RoleId))
                    {
                        await _ouRoleBll.RemoveOuAsync(id, RoleId.ToInt32());
                    }
                }
                foreach (string id in _addList)
                {
                    if (!string.IsNullOrEmpty(RoleId))
                    {
                        await _ouRoleBll.AddOUAsync(id, RoleId.ToInt32());
                    }
                }
                break;

            case DisplayTreeType.Role:
                break;

            case DisplayTreeType.User:
                foreach (string id in _removeList)
                {
                    if (!string.IsNullOrEmpty(RoleId))
                    {
                        await _userRoleBll.RemoveUserAsync(id.ToInt32(), RoleId.ToInt32());
                    }
                    else if (!string.IsNullOrEmpty(OUId))
                    {
                        await _ouUserBll.RemoveUserAsync(id.ToInt32(), OUId);
                    }
                }         
                foreach (string id in _addList)
                {
                    if (!string.IsNullOrEmpty(RoleId))
                    {
                        await _userRoleBll.AddUserAsync(id.ToInt32(), RoleId.ToInt32());
                    }
                    else if (!string.IsNullOrEmpty(OUId))
                    {
                        await _ouBll.AddUserAsync(id.ToInt32(), OUId);
                    }
                }
                break;

            case DisplayTreeType.Function:
                foreach (string id in _removeList)
                {
                    if (!string.IsNullOrEmpty(RoleId))
                    {
                        await _roleFunctionBll.RemoveFunctionAsync(id, RoleId.ToInt32());
                    }
                }
                foreach (string id in _addList)
                {
                    if (!string.IsNullOrEmpty(RoleId))
                    {
                        await _roleFunctionBll.AddFunctionAsync(id, RoleId.ToInt32());
                    }
                }
                break;
        }
        #endregion
    }

    private void chkAll_CheckedChanged(object? sender, EventArgs e)
    {
        foreach (TreeNode node in treeView1.Nodes)
        {
            CheckSelect(node, chkAll.Checked);
        }
    }

    private void CheckSelect(TreeNode node, bool selectAll)
    {
        node.Checked = selectAll;
        foreach (TreeNode subNode in node.Nodes)
        {
            CheckSelect(subNode, selectAll);
        }
    }
}