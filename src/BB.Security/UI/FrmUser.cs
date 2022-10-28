using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.DocViewer;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Tools.Format;
using BB.Entity.Security;
using BB.HttpServices.Core.OU;
using BB.HttpServices.Core.Role;
using BB.HttpServices.Core.User;
using BB.Tools.Entity;
using BB.Tools.Extension;
using DevExpress.XtraGrid.Views.Base;
using Furion;
using Furion.Logging.Extensions;
using Mapster;

namespace BB.Security.UI;

/// <summary>
/// 系统用户信息
/// </summary>	
public partial class FrmUser : BaseDock
{
    string _selectedDeptId = "";
    private readonly string _moduleName = "系统用户信息";
    private readonly UserHttpService _bll;
    private readonly OUHttpService _ouBll;
    private readonly RoleHttpService _roleBll;
    private readonly UserRoleHttpService _userRoleBll;

    public FrmUser(UserHttpService bll, OUHttpService ouBll, RoleHttpService roleBll, UserRoleHttpService userRoleBll)
    {
        InitializeComponent();

        _bll = bll;
        _ouBll = ouBll;
        _roleBll = roleBll;
        _userRoleBll = userRoleBll;

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
        winGridViewPager1.gridView1.DataSourceChanged += gridView1_DataSourceChanged;
        winGridViewPager1.gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
        winGridViewPager1.gridView1.RowCellStyle += gridView1_RowCellStyle;
        winGridViewPager1.PagerInfo.PageSize = 20;//指定20个一页

        //关联回车键进行查询
        foreach (Control control in layoutControl1.Controls)
        {
            control.KeyUp += SearchControl_KeyUp;
        }
    }

    void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
    {
        if (e.Column.FieldName == "AuditStatus")
        {
            string status = winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "AuditStatus").ObjToStr();
            Color color = Color.White;
            if (status == "未审核")
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
        }
        else if (e.Column.FieldName == "IsExpire")
        {
            string status = winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "IsExpire").ObjToStr();
            Color color = Color.White;
            if (status.ObjToBool())
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
        }
        else if (e.Column.FieldName == "Deleted")
        {
            string status = winGridViewPager1.gridView1.GetRowCellValue(e.RowHandle, "Deleted").ObjToStr();
            Color color = Color.White;
            if (status.ObjToBool())
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
        }
    }
    void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
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
    }

    /// <summary>
    /// 绑定数据后，分配各列的宽度
    /// </summary>
    private void gridView1_DataSourceChanged(object? sender, EventArgs e)
    {
        if (winGridViewPager1.gridView1.Columns.Count > 0 && winGridViewPager1.gridView1.RowCount > 0)
        {
            //统一设置100宽度
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in winGridViewPager1.gridView1.Columns)
            {
                column.Width = 100;
            }

            //可特殊设置特别的宽度
            SetGridColumWidth("Gender", 50);
            SetGridColumWidth("Email", 150);
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
        await InitDeptTreeView();
        await InitRoleTree();
        await BindData();
    }

    #region 初始化组织结构树方法

    /// <summary>
    /// 初始化组织机构列表
    /// </summary>
    private async Task InitDeptTreeView()
    {
        treeDept.BeginUpdate();
        treeDept.Nodes.Clear();

        List<OUInfo>  list = await GB.GetMyTopGroup();
        foreach (OUInfo groupInfo in list)
        {
            if (groupInfo is { Deleted: false })
            {
                TreeNode topnode = new TreeNode
                {
                    Text = groupInfo.Name,
                    Name = groupInfo.HandNo,
                    ImageIndex = GB.GetImageIndex(groupInfo.Category),
                    SelectedImageIndex = GB.GetImageIndex(groupInfo.Category),
                    Tag = new NameValueCollection() { { UserInfo.FieldCompanyId, groupInfo.HandNo } }
                };
                treeDept.Nodes.Add(topnode);

                List<OUNodeInfo> sublist = await _ouBll.GetTreeByIdAsync(groupInfo.HandNo);
                AddOuNode(sublist, topnode);
            }
        }

        treeDept.ExpandAll();
        treeDept.EndUpdate();
    }

    private void AddOuNode(List<OUNodeInfo> list, TreeNode parentNode)
    {
        foreach (OUNodeInfo ouInfo in list)
        {
            TreeNode ouNode = new TreeNode
            {
                Text = ouInfo.Name,
                Name = ouInfo.HandNo
            };
            if (ouInfo.Deleted)
            {
                ouNode.ForeColor = Color.Red;
                continue;//跳过不显示
            }
            ouNode.ImageIndex = GB.GetImageIndex(ouInfo.Category);
            ouNode.SelectedImageIndex = GB.GetImageIndex(ouInfo.Category);
            if (ouNode.ImageIndex <= 1)//0,1为集团、公司
            {
                ouNode.Tag = new NameValueCollection() { { UserInfo.FieldCompanyId, ouInfo.HandNo } };
            }
            else
            {
                ouNode.Tag = new NameValueCollection() { { UserInfo.FieldDeptId, ouInfo.HandNo } };
            }
            parentNode.Nodes.Add(ouNode);

            AddOuNode(ouInfo.Children, ouNode);
        }            
    }

    #endregion

    #region 初始化角色树方法

    /// <summary>
    /// 初始化角色列表
    /// </summary>
    private async Task InitRoleTree()
    {
        treeRole.BeginUpdate();
        treeRole.Nodes.Clear();

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
                        TreeNode ouNode = AddOuNode(info, topnode);
                        await AddRole(info, ouNode);
                    }
                }
                treeRole.Nodes.Add(topnode);
            }
        }

        treeRole.ExpandAll();
        treeRole.EndUpdate();
    }

    private TreeNode AddOuNode(OUInfo ouInfo, TreeNode? parentNode = null)
    {
        TreeNode ouNode = new TreeNode
        {
            Text = ouInfo.Name,
            Tag = new NameValueCollection() { { UserInfo.FieldCompanyId, ouInfo.HandNo } },
            ImageIndex = GB.GetImageIndex(ouInfo.Category),
            SelectedImageIndex = GB.GetImageIndex(ouInfo.Category)
        };

        parentNode?.Nodes.Add(ouNode);

        return ouNode;
    }

    private async Task AddRole(OUInfo ouInfo, TreeNode treeNode)
    {
        List<RoleInfo> roleList = await _roleBll.GetRolesByCompanyAsync(ouInfo.HandNo);
        foreach (RoleInfo roleInfo in roleList)
        {
            TreeNode roleNode = new TreeNode
            {
                Text = roleInfo.Name,
                Tag = roleInfo.ID,
                ImageIndex = 5,
                SelectedImageIndex = 5
            };

            treeNode.Nodes.Add(roleNode);
        }
    }

    #endregion

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
    private async void winGridViewPager1_OnRefresh(object? sender, EventArgs e)
    {
        await BindData();
    }

    /// <summary>
    /// 分页控件删除操作
    /// </summary>
    private async void winGridViewPager1_OnDeleteSelected(object? sender, EventArgs e)
    {
        if ("您确定删除选定的记录么？".ShowYesNoAndUxTips() == DialogResult.No)
        {
            return;
        }

        int[] rowSelected = winGridViewPager1.GridView1.GetSelectedRows();
        foreach (int iRow in rowSelected)
        {
            string id = winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");
            await _bll.SetDeletedFlagAsync(id);
        }

        await BindData();
    }

    /// <summary>
    /// 分页控件编辑项操作
    /// </summary>
    private async void winGridViewPager1_OnEditSelected(object? sender, EventArgs e)
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
            FrmEditUser dlg = App.GetService<FrmEditUser>();
            dlg.ID = id;
            dlg.IdList = idList;
            dlg.OnDataSaved += dlg_OnDataSaved;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                await BindData();
            }
        }
    }

    async void dlg_OnDataSaved(object? sender, EventArgs e)
    {
        await BindData();
    }

    /// <summary>
    /// 分页控件新增操作
    /// </summary>        
    private void winGridViewPager1_OnAddNew(object? sender, EventArgs e)
    {
        btnAddNew_Click(sender, e);
    }

    /// <summary>
    /// 分页控件全部导出操作前的操作
    /// </summary> 
    private async void winGridViewPager1_OnStartExport(object? sender, EventArgs e)
    {
        Dictionary<string,string> condition = GetConditionSql();
        winGridViewPager1.AllToExport = await _bll.FindAsync(condition);
    }

    /// <summary>
    /// 分页控件翻页的操作
    /// </summary> 
    private async void winGridViewPager1_OnPageChanged(object? sender, EventArgs e)
    {
        await BindData();
    }

    /// <summary>
    /// 高级查询条件语句对象
    /// </summary>
    private NameValueCollection? _advanceCondition;
    private NameValueCollection? _treeCondition;
    bool _isUseRoleSearch = false;//是否使用角色查询

    /// <summary>
    /// 根据查询条件构造查询语句
    /// </summary> 
    private Dictionary<string,string> GetConditionSql()
    {
        //如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
        var condition = _treeCondition ?? _advanceCondition ?? new NameValueCollection
        {
            { UserInfo.FieldHandNo, txtHandNo.Text.Trim() },
            { UserInfo.FieldName, txtName.Text.Trim() },
            { UserInfo.FieldFullName, txtFullName.Text.Trim() },
            { UserInfo.FieldNickname, txtNickname.Text.Trim() },
            { UserInfo.FieldMobilePhone, txtMobilePhone.Text.Trim() },
            { UserInfo.FieldEmail, txtEmail.Text.Trim() },
            { UserInfo.FieldGender, txtGender.Text.Trim() },
            { UserInfo.FieldQQ, txtQq.Text.Trim() },
        };

        //如非选定，只显示正常用户
        if (!chkIncludeDelete.Checked)
        {
            condition.AddOrSet(UserInfo.FieldDeleted, "0");
        }

        return condition.ToDicString();
    }

    /// <summary>
    /// 绑定列表数据
    /// </summary>
    private async Task BindData()
    {
        try
        {
            ShowWaitForm();
            WaitForm.SetWaitFormDescription("数据加载中...");
            //entity
            winGridViewPager1.DisplayColumns = "HandNo,Name,FullName,Title,MobilePhone,OfficePhone,Email,Gender,QQ,AuditStatus,IsExpire,Deleted,Note";
            winGridViewPager1.ColumnNameAlias = await _bll.GetColumnNameAliasAsync();//字段列显示名称转义

            #region 添加别名解析

            //this.winGridViewPager1.AddColumnAlias("HandNo", "用户编码");
            //this.winGridViewPager1.AddColumnAlias("Name", "用户名/登录名");
            //this.winGridViewPager1.AddColumnAlias("FullName", "用户全名");
            //this.winGridViewPager1.AddColumnAlias("IsExpire", "是否过期");
            //this.winGridViewPager1.AddColumnAlias("Title", "职务头衔");
            //this.winGridViewPager1.AddColumnAlias("MobilePhone", "移动电话");
            //this.winGridViewPager1.AddColumnAlias("OfficePhone", "办公电话");
            //this.winGridViewPager1.AddColumnAlias("Email", "邮件地址");
            //this.winGridViewPager1.AddColumnAlias("Gender", "性别");
            //this.winGridViewPager1.AddColumnAlias("QQ", "QQ号码");
            //this.winGridViewPager1.AddColumnAlias("AuditStatus", "审核状态");

            #endregion

            Dictionary<string,string> condition = GetConditionSql();
            PageInput pagerInfo = winGridViewPager1.PagerInfo.Adapt<PageInput>();
            PageResult<UserInfo> list = await _bll.GetEntitiesByPageAsync(new PaginatedSearchInfos(condition, pagerInfo));
            winGridViewPager1.InitDataSource(list, "系统用户信息报表");
        }
        finally
        {
            HideWaitForm();
        }
    }

    /// <summary>
    /// 绑定列表数据（根据角色查询）
    /// </summary>
    private async Task BindDataUseRole(int roleId)
    {
        //entity
        winGridViewPager1.DisplayColumns = "HandNo,Name,FullName,Title,MobilePhone,OfficePhone,Email,Gender,QQ,AuditStatus,IsExpire,Deleted,Note";
        winGridViewPager1.ColumnNameAlias = await _bll.GetColumnNameAliasAsync();//字段列显示名称转义

        List<UserInfo> list = await _userRoleBll.GetUsersByRoleAsync(roleId);
        winGridViewPager1.InitDataSource(new PageResult<UserInfo>(list), "系统用户信息报表");
    }

    /// <summary>
    /// 查询数据操作
    /// </summary>
    private async void btnSearch_Click(object? sender, EventArgs e)
    {
        _treeCondition = null;
        _advanceCondition = null;//必须重置查询条件，否则可能会使用高级查询条件了
        _isUseRoleSearch = false;

        await BindData();
    }

    /// <summary>
    /// 新增数据操作
    /// </summary>
    private async void btnAddNew_Click(object? sender, EventArgs e)
    {
        //默认部门
        string deptId = "";
        TreeNode node = treeDept.SelectedNode;
        if (node != null)
        {
            deptId = node.Name;
        }

        FrmEditUser dlg = App.GetService<FrmEditUser>();
        dlg.OnDataSaved += dlg_OnDataSaved;

        if (DialogResult.OK == dlg.ShowDialog())
        {
            await BindData();
        }
    }

    /// <summary>
    /// 提供给控件回车执行查询的操作
    /// </summary>
    private void SearchControl_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnSearch_Click(sender, e);
        }
    }

    /// <summary>
    /// 导入Excel的操作
    /// </summary>          
    private void btnImport_Click(object? sender, EventArgs e)
    {
        //如果导入的Excel不指定部门，则默认使用选定的部门作为记录的部门
        TreeNode deptNode = treeDept.SelectedNode;
        if (deptNode != null)
        {
            _selectedDeptId = deptNode.Name;
        }
        else
        {
            "请选择组织机构节点，然后在进行导入，默认导入用户属于该部门节点".ShowUxTips();
            return;
        }

        string templateFile = $"{_moduleName}-模板.xls";
        FrmImportExcelData dlg = new FrmImportExcelData();
        dlg.SetTemplate(templateFile, System.IO.Path.Combine(Application.StartupPath, templateFile));
        dlg.OnDataSave += ExcelData_OnDataSave;
        dlg.OnRefreshData += ExcelData_OnRefreshData;
        dlg.ShowDialog();
    }

    async void ExcelData_OnRefreshData(object? sender, EventArgs e)
    {
        await BindData();
    }

    async Task<bool> ExcelData_OnDataSave(DataRow dr)
    {
        string companyName = dr["所属公司名称"].ToString();
        OUInfo companyInfo = await _ouBll.FindByNameAsync(companyName);
        if (companyInfo == null)
        {
            //公司名称不存在，提示错误并记录日志
            throw new ArgumentException($"公司名称【{companyName}】不存在，记录已跳过");
        }

        string name = dr["用户名/登录名"].ToString();
        if (string.IsNullOrEmpty(name))
        {
            return false;//用户名为空，则跳过
        }
        else
        {
            //判断是否登录名重复，如果重复，则提示错误，并记录了日志
            bool isExist = await _bll.IsExistKeyAsync("Name", name);
            if (isExist)
            {
                throw new ArgumentException($"用户名/登录名【{name}】已存在，记录已跳过");
            }
        }

        string deptName = dr["默认部门名称"].ToString();
        OUInfo deptInfo = null;
        //if (!string.IsNullOrEmpty(deptName))
        //{
        //    deptInfo = Rpc.Create<IOUBLL>().FindByName(deptName);
        //}

        //默认使用选定的部门作为记录的部门                
        if (!string.IsNullOrEmpty(_selectedDeptId))
        {
            deptInfo = await _ouBll.FindByIdAsync(_selectedDeptId);
        }

        bool success = false;
        bool converted = false;
        DateTime dtDefault = Convert.ToDateTime("1900-01-01");
        DateTime dt;
        UserInfo info = new UserInfo
        {
            HandNo = dr["用户编码"].ToString(),
            Name = name,
            FullName = dr["用户全名"].ToString(),
            Nickname = dr["用户呢称"].ToString(),
            Gender = dr["性别"].ToString(),
            MobilePhone = dr["移动电话"].ToString(),
            Email = dr["邮件地址"].ToString()
        };

        #region 可选字段

        if (dr.Table.Columns.Contains("是否过期"))
        {
            info.IsExpire = dr["是否过期"].ToString().ToInt32() > 0;
        }
        if (dr.Table.Columns.Contains("职务头衔"))
        {
            info.Title = dr["职务头衔"].ToString();
        }
        if (dr.Table.Columns.Contains("身份证号码"))
        {
            info.IdentityCard = dr["身份证号码"].ToString();
        }
        if (dr.Table.Columns.Contains("办公电话"))
        {
            info.OfficePhone = dr["办公电话"].ToString();
        }
        if (dr.Table.Columns.Contains("家庭电话"))
        {
            info.HomePhone = dr["家庭电话"].ToString();
        }
        if (dr.Table.Columns.Contains("住址"))
        {
            info.Address = dr["住址"].ToString();
        }
        if (dr.Table.Columns.Contains("办公地址"))
        {
            info.WorkAddr = dr["办公地址"].ToString();
        }
        if (dr.Table.Columns.Contains("出生日期"))
        {
            converted = DateTime.TryParse(dr["出生日期"].ToString(), out dt);
            if (converted && dt > dtDefault)
            {
                info.Birthday = dt;
            }
        }
        if (dr.Table.Columns.Contains("QQ号码"))
        {
            info.QQ = dr["QQ号码"].ToString();
        }
        if (dr.Table.Columns.Contains("个性签名"))
        {
            info.Signature = dr["个性签名"].ToString();
        }
        if (dr.Table.Columns.Contains("审核状态"))
        {
            info.AuditStatus = dr["审核状态"].ToString();
        }
        if (dr.Table.Columns.Contains("备注"))
        {
            info.Note = dr["备注"].ToString();
        }
        if (dr.Table.Columns.Contains("自定义字段"))
        {
            info.CustomField = dr["自定义字段"].ToString();
        }
        if (dr.Table.Columns.Contains("排序码"))
        {
            info.SortCode = dr["排序码"].ToString();
        } 
        #endregion

        #region 自动字段

        //默认部门，可以为空
        info.DeptName = deptName;
        if (deptInfo != null)
        {
            info.DeptId = deptInfo.HandNo;
        }

        //公司名称，不能为空
        info.CompanyName = companyName;
        if (companyInfo != null)
        {
            info.CompanyId = companyInfo.HandNo;
        }

        info.Creator = GB.LoginUserInfo.FullName;
        info.CreatedBy = GB.LoginUserInfo.ID.ToString();
        info.CreationDate = DateTime.Now;
        info.Editor = GB.LoginUserInfo.FullName;
        info.LastUpdatedBy = GB.LoginUserInfo.ID.ToString(); 
        #endregion

        success = await _bll.InsertAsync(info);
        return success;
    }

    /// <summary>
    /// 导出Excel的操作
    /// </summary>
    private async void btnExport_Click(object? sender, EventArgs e)
    {
        string file = FileDialogHelper.SaveExcel($"{_moduleName}.xls");
        if (!string.IsNullOrEmpty(file))
        {
            List<UserInfo> list = new List<UserInfo>();

            TreeNode selectedNode = treeRole.SelectedNode;
            if (_isUseRoleSearch && selectedNode is { Tag: { } })
            {
                string roleId = selectedNode.Tag.ObjToStr();
                if (!string.IsNullOrEmpty(roleId))
                {
                    list = await _userRoleBll.GetUsersByRoleAsync(roleId.ToInt32());
                }
            }
            else
            {
                Dictionary<string,string> condition = GetConditionSql();
                list = await _bll.FindAsync(condition);
            }

            DataTable dtNew = DataTableHelper.CreateTable("序号|int,用户编码,用户名/登录名,用户全名,用户呢称,是否过期,职务头衔,身份证号码,移动电话,办公电话,家庭电话,邮件地址,住址,办公地址,性别,出生日期,QQ号码,个性签名,审核状态,备注,自定义字段,默认部门名称,所属公司名称,排序码");
            DataRow dr;
            int j = 1;
            DateTime dtDefault = Convert.ToDateTime("1900-01-01");
            for (int i = 0; i < list.Count; i++)
            {
                dr = dtNew.NewRow();
                dr["序号"] = j++;
                dr["用户编码"] = list[i].HandNo;
                dr["用户名/登录名"] = list[i].Name;
                dr["用户全名"] = list[i].FullName;
                dr["用户呢称"] = list[i].Nickname;
                dr["是否过期"] = list[i].IsExpire ? "1" : "0";
                dr["职务头衔"] = list[i].Title;
                dr["身份证号码"] = list[i].IdentityCard;
                dr["移动电话"] = list[i].MobilePhone;
                dr["办公电话"] = list[i].OfficePhone;
                dr["家庭电话"] = list[i].HomePhone;
                dr["邮件地址"] = list[i].Email;
                dr["住址"] = list[i].Address;
                dr["办公地址"] = list[i].WorkAddr;
                dr["性别"] = list[i].Gender;
                if (list[i].Birthday > dtDefault)
                {
                    dr["出生日期"] = list[i].Birthday;
                }
                dr["QQ号码"] = list[i].QQ;
                dr["个性签名"] = list[i].Signature;
                dr["审核状态"] = list[i].AuditStatus;
                dr["备注"] = list[i].Note;
                dr["自定义字段"] = list[i].CustomField;
                dr["默认部门名称"] = list[i].DeptName;
                dr["所属公司名称"] = list[i].CompanyName;
                dr["排序码"] = list[i].SortCode;
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

    private async void treeDept_AfterSelect(object sender, TreeViewEventArgs e)
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

    private async void treeRole_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (e.Node is { Tag: { } })
        {
            if (e.Node.Tag is NameValueCollection tag)
            {
                _treeCondition = tag;
                await BindData();
            }
            else
            {
                _isUseRoleSearch = true;
                await BindDataUseRole(e.Node.Tag.ToString().ToInt32());
            }
        }
        else
        {
            _treeCondition = null;
            await BindData();
        }
    }

    private void menuDept_AddNew_Click(object? sender, EventArgs e)
    {
        btnAddNew_Click(sender, e);
    }

    private void menuDept_ExpandAll_Click(object? sender, EventArgs e)
    {            
        treeDept.ExpandAll();
    }

    private void menuDept_Collapse_Click(object? sender, EventArgs e)
    {
        treeDept.CollapseAll();
    }

    private async void menuDept_Refresh_Click(object? sender, EventArgs e)
    {
        await InitDeptTreeView();
    }

    private void menuRole_ExpandAll_Click(object? sender, EventArgs e)
    {            
        treeRole.ExpandAll();
    }

    private void menuRole_Collapse_Click(object? sender, EventArgs e)
    {
        treeRole.CollapseAll();
    }

    private async void menuRole_Refresh_Click(object? sender, EventArgs e)
    {
        await InitRoleTree();
    }

    private async void menu_InitPassword_Click(object? sender, EventArgs e)
    {
        if ("您确定重置选定记录的用户密码么？ \r\n重置后密码将设置为【12345678】".ShowYesNoAndUxTips() == DialogResult.No)
        {
            return;
        }

        int[] rowSelected = winGridViewPager1.GridView1.GetSelectedRows();
        foreach (int iRow in rowSelected)
        {
            string changeUserId = winGridViewPager1.GridView1.GetRowCellDisplayText(iRow, "ID");

            bool success = await _bll.ResetPasswordAsync(changeUserId.ToInt32());
            (success ? "重置密码操作成功" : "操作失败").ShowUxTips();
        }
    }

    private async void chkIncludeDelete_CheckedChanged(object? sender, EventArgs e)
    {
        await BindData();
    }

}