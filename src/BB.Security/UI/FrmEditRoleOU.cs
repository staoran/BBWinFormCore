using System.Drawing;
using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Other;
using BB.Entity.Security;
using BB.HttpServices.Core.OU;

namespace BB.Security.UI;

/// <summary>
/// 角色包含机构
/// </summary>
public partial class FrmEditRoleOU : BaseForm
{
    private Dictionary<string, string> _mSelectOUDict = new();
    private readonly OUHttpService _oubll;

    /// <summary>
    /// 选择的部门字典数据（实际数据）
    /// </summary>
    public Dictionary<string, string> SelectOUDict
    {
        get => _mSelectOUDict;
        set => _mSelectOUDict = new Dictionary<string, string>(value);
    }

    public FrmEditRoleOU(OUHttpService bll)
    {
        InitializeComponent();
        _oubll = bll;
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
            deptNode.Checked = SelectOUDict.ContainsKey(ouInfo.HandNo);//选中的
            treeNode.Nodes.Add(deptNode);

            AddDept(ouInfo.Children, deptNode);
        }
    }

    private async Task RefreshTreeView()
    {
        treeView1.BeginUpdate();
        Cursor.Current = Cursors.WaitCursor;
        treeView1.Nodes.Clear();

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
                    Checked = SelectOUDict.ContainsKey(groupInfo.HandNo) //选中的
                };

                List<OUNodeInfo> sublist = await _oubll.GetTreeByIdAsync(groupInfo.HandNo);
                AddDept(sublist, topnode);

                treeView1.Nodes.Add(topnode);
            }
        }

        treeView1.ExpandAll();
        treeView1.EndUpdate();
        Cursor.Current = Cursors.Default;
    }

    private async void FrmEditRoleOU_Load(object? sender, EventArgs e)
    {
        if (!DesignMode)
        {
            await RefreshTreeView();
        }
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

    private void btnOK_Click(object? sender, EventArgs e)
    {
        List<string> list = new();
        foreach (TreeNode node in treeView1.Nodes)
        {
            list.AddRange(GetSelected(node));
        }

        Dictionary<string, string> dict = new();
        foreach (string id in list)
        {
            if (!dict.ContainsKey(id))
            {
                dict.Add(id, id);
            }
        }

        SelectOUDict = dict;
        DialogResult = DialogResult.OK;
    }

    private List<string> GetSelected(TreeNode node)
    {
        List<string> list = new();
        foreach (TreeNode subNode in node.Nodes)
        {
            if (subNode.Checked && subNode.Tag != null)
            {
                list.Add(subNode.Tag.ToString());
            }
            list.AddRange(GetSelected(subNode));
        }
        return list;
    }
}