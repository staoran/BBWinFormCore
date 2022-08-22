using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.Tools.Entity;
using BB.Entity.Security;
using BB.HttpServices.Core.Function;
using BB.HttpServices.Core.Role;
using BB.HttpServices.Core.SystemType;
using BB.Tools.Extension;
using Furion.Logging.Extensions;

namespace BB.Security.UI;

public partial class FrmFunction : BaseForm
{
    private string _currentId = string.Empty;
    private readonly SystemTypeHttpService _systemTypeBLL;
    private readonly FunctionHttpService _bll;
    private readonly RoleHttpService _roleBLL;
    private readonly RoleFunctionHttpService _roleFunctionBll;

    public FrmFunction(FunctionHttpService bll, SystemTypeHttpService systemTypeBll, RoleHttpService roleBll,
        RoleFunctionHttpService roleFunctionBll)
    {
        InitializeComponent();
        _systemTypeBLL = systemTypeBll;
        _bll = bll;
        _roleBLL = roleBll;
        _roleFunctionBll = roleFunctionBll;
        functionControl1.EditValueChanged += functionControl1_EditValueChanged;
    }

    void functionControl1_EditValueChanged(object sender, EventArgs e)
    {
        string item = functionControl1.Value;
        if (!string.IsNullOrEmpty(item) && item == "-1")
        {
            SetSystemTypeVisible(true);
        }
        else
        {
            SetSystemTypeVisible(false);
        }
    }

    private async void FrmFunction_Load(object sender, EventArgs e)
    {
        await InitDictItem();
        await RefreshTreeView();
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    private async Task InitDictItem()
    {
        //绑定系统类型
        List<SystemTypeInfo> systemList = await _systemTypeBLL.GetAllAsync();
        foreach (SystemTypeInfo info in systemList)
        {
            txtSystemType.Properties.Items.Add(new CListItem(info.Name, info.Oid));
        }
        if (txtSystemType.Properties.Items.Count == 1)
        {
            txtSystemType.SelectedIndex = 0;
        }
    }

    private async Task RefreshTreeView()
    {
        treeView1.Nodes.Clear();
        treeView1.BeginUpdate();
        Cursor.Current = Cursors.WaitCursor;                       

        List<SystemTypeInfo> typeList = await _systemTypeBLL.GetAllAsync();
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
            List<FunctionNodeInfo> funList = await _bll.GetTreeAsync(systemType);
            foreach (FunctionNodeInfo info in funList)
            {
                TreeNode item = new TreeNode
                {
                    Name = info.ID,
                    Text = info.Name, //一级菜单节点
                    Tag = info.ID, //info;//记录其info到Tag中，作为判断依据
                    ImageIndex = 1,
                    SelectedImageIndex = 1
                };
                pNode.Nodes.Add(item);

                AddChildNode(info.Children, item);
            }
            pNode.Expand();
        }

        Cursor.Current = Cursors.Default;
        treeView1.EndUpdate();
        //this.treeView1.ExpandAll();
    }

    private void AddChildNode(List<FunctionNodeInfo> list, TreeNode fnode)
    {
        foreach (FunctionNodeInfo info in list)
        {
            TreeNode item = new TreeNode
            {
                Name = info.ID,
                Text = info.Name, //二、三级菜单节点
                Tag = info.ID, //info;//记录其FunctionNodeInfo到Tag中，作为判断依据
                ImageIndex = 1,
                SelectedImageIndex = 1
            };
            fnode.Nodes.Add(item);
            fnode.Collapse();

            AddChildNode(info.Children, item);
        }
    }

    private void ClearInput()
    {
        txtFunctionID.Text = "";
        txtName.Text = "";
        functionControl1.Value = "-1";
        lvwRole.Items.Clear();
    }

    private async Task RefreshRoles(string functionId)
    {
        lvwRole.Items.Clear();
        List<RoleInfo> list = await _roleFunctionBll.GetRolesByFunctionAsync(functionId);
        foreach (RoleInfo info in list)
        {
            string displayName = info.Name;
            if (!string.IsNullOrEmpty(info.CompanyName))
            {
                displayName = $"{info.Name}({info.CompanyName})";
            }
            CListItem item = new CListItem(displayName, info.ID.ToString());
            lvwRole.Items.Add(item);
        }
        if (lvwRole.Items.Count > 0)
        {
            lvwRole.SelectedIndex = 0;
        }
    }

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
        if (node != null && !string.IsNullOrEmpty(node.Name))
        {
            if ("您确认删除指定节点吗？\r\n如果该节点含有子节点，子节点不会被删除，且它们会被提升一级".ShowYesNoAndUxTips() == DialogResult.Yes)
            {
                //int id = Convert.ToInt32(node.Name);
                try
                {
                    await _bll.DeleteAsync(node.Name);
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

    private async void menu_DeletAll_Click(object sender, EventArgs e)
    {
        TreeNode node = treeView1.SelectedNode;
        if (node != null && !string.IsNullOrEmpty(node.Name))
        {
            if ("您确认删除指定节点及其子节点吗？\r\n如果该节点含有子节点，子节点也会一并删除！".ShowYesNoAndUxTips() == DialogResult.Yes)
            {
                //int id = Convert.ToInt32(node.Name);
                try
                {
                    await _bll.DeleteWithSubNodeAsync(node.Name);
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

    private void menu_Add_Click(object sender, EventArgs e)
    {
        ClearInput();
        _currentId = "";
        TreeNode node = treeView1.SelectedNode;
        if (node is { Tag: { } })
        {
            functionControl1.Value = node.Name;
        }
        txtName.Focus();
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
        if (e.Node is { Tag: { } })
        {
            _currentId = e.Node.Tag.ToString();
            FunctionInfo info = await _bll.FindByIdAsync(_currentId);

            if (info != null)
            {
                txtName.Text = info.Name;
                txtFunctionID.Text = info.ControlId;
                txtSortCode.Text = info.SortCode;

                FunctionInfo info2 = await _bll.FindByIdAsync(info.PID);
                if (info2 != null)
                {
                    functionControl1.Value = info.PID;
                }
                else
                {
                    functionControl1.Value = "-1";                        
                    txtSystemType.SetComboBoxItem(info.SystemTypeId);//设置系统类型
                }

                await RefreshRoles(_currentId);
            }
        }
    }

    private FunctionInfo SetFunction(FunctionInfo info)
    {
        info.Name = txtName.Text;
        info.PID = functionControl1.Value;
        info.ControlId = txtFunctionID.Text;
        info.SortCode = txtSortCode.Text;
        return info;
    }

    private void SetSystemTypeVisible(bool visible)
    {
        txtSystemType.Visible = visible;
        lblSystemType.Visible = visible;
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {            
        if (functionControl1.Text.Length == 0)
            return;

        #region 验证用户输入
        if (txtName.Text == "")
        {
            "功能名称不能为空".ShowUxTips();
            txtName.Focus();
            return;
        }
        else if (txtFunctionID.Text == "")
        {
            "功能ID不能为空".ShowUxTips();
            txtFunctionID.Focus();
            return;
        }
        else if (txtSystemType.Visible && txtSystemType.Text.Length == 0)
        {
            "系统类型编号不能为空".ShowUxTips();
            txtSystemType.Focus();
            return;
        }

        #endregion

        if (!string.IsNullOrEmpty(_currentId))
        {
            try
            {
                FunctionInfo info = await _bll.FindByIdAsync(_currentId);
                if (info != null)
                {
                    info = SetFunction(info);
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
            string pid = functionControl1.Value;
            FunctionInfo functionInfo = await _bll.FindByIdAsync(pid);

            if (functionInfo.SystemTypeId.IsNullOrEmpty())
            {
                functionInfo.SystemTypeId = txtSystemType.GetComboBoxValue();
            }

            FunctionInfo info = new FunctionInfo();
            info = SetFunction(info);
            info.SystemTypeId = functionInfo.SystemTypeId;//和父节点的SystemType_ID一样。

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

    private void btnAdd_Click(object sender, EventArgs e)
    {
        menu_Add_Click(sender, e);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        menu_Delete_Click(sender, e);
    }

    private void btnBatchAdd_Click(object sender, EventArgs e)
    {
        TreeNode node = treeView1.SelectedNode;
        if (node is { Tag: { } })
        {
            FrmAddMoreFunction dlg = new FrmAddMoreFunction(_bll);
            dlg.OnDataSaved += dlg_OnDataSaved;
            dlg.SetUpperFunction(node.Name);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //提高速度，避免重复更新
                //await RefreshTreeView();
            }
        }
        else
        {
            "请选择功能节点再执行操作".ShowUxTips();
        }
    }

    async void dlg_OnDataSaved(object sender, EventArgs e)
    {
        //提高速度，避免重复更新
        //await RefreshTreeView();
    }

    private void menu_Collapse_Click(object sender, EventArgs e)
    {
        treeView1.CollapseAll();
    }

}