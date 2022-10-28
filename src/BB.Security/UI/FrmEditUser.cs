using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Tools.Entity;
using BB.Tools.Format;
using BB.Entity.Security;
using BB.HttpServices.Core.Menu;
using BB.HttpServices.Core.OU;
using BB.HttpServices.Core.SystemType;
using BB.HttpServices.Core.User;
using Furion.Logging.Extensions;

namespace BB.Security.UI;

public partial class FrmEditUser : BaseEditForm
{
    /// <summary>
    /// 创建一个临时对象，方便在附件管理中获取存在的GUID
    /// </summary>
    private UserInfo _tempInfo = new UserInfo();

    private readonly UserHttpService _bll;
    private readonly OUHttpService _ouBll;
    private readonly SystemTypeHttpService _systemTypeBll;
    private readonly MenuHttpService _menuBll;
    private readonly UserRoleHttpService _userRoleBll;

    public FrmEditUser(UserHttpService bll, OUHttpService ouBll, SystemTypeHttpService systemTypeBll,
        MenuHttpService menuBll, UserRoleHttpService userRoleBll)
    {
        InitializeComponent();
        _bll = bll;
        _ouBll = ouBll;
        _systemTypeBll = systemTypeBll;
        _menuBll = menuBll;
        _userRoleBll = userRoleBll;

        txtCompany.EditValueChanged += txtCompany_EditValueChanged;
        txtDept.EditValueChanged += txtDept_EditValueChanged;
    }

    void txtCompany_EditValueChanged(object? sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtCompany.Value))
        {
            txtDept.ParentOUId = txtCompany.Value;
            txtDept.Init();
        }
    }

    async void txtDept_EditValueChanged(object? sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDept.Value))
        {
            await InitManagers(txtDept.Value);
        }
    }

    private async Task InitManagers(string deptId)
    {
        //初始化代码
        cmbManager.Properties.BeginUpdate();
        cmbManager.Properties.Items.Clear();
        cmbManager.Properties.Items.Add(new CListItem("无", "-1"));
        List<UserInfo> list = await _bll.FindByDeptAsync(deptId);
        foreach (UserInfo info in list)
        {
            cmbManager.Properties.Items.Add(new CListItem(info.FullName, info.ID.ToString()));
        }
        cmbManager.Properties.EndUpdate();
    }

    /// <summary>
    /// 实现控件输入检查的函数
    /// </summary>
    /// <returns></returns>
    public override Task<bool> CheckInput()
    {
        bool result = true;//默认是可以通过

        #region MyRegion
        if (txtName.Text.Trim().Length == 0)
        {
            "请输入用户名/登录名".ShowUxTips();
            txtName.Focus();
            result = false;
        }
        else if (txtFullName.Text.Trim().Length == 0)
        {
            "请输入真实姓名".ShowUxTips();
            txtFullName.Focus();
            result = false;
        }
        else if (txtCompany.Text == "")
        {
            "所属公司不能为空".ShowUxTips();
            txtCompany.Focus();
            return Task.FromResult(false);
        }
        else if (txtDept.Text == "")
        {
            "默认部门机构不能为空".ShowUxTips();
            txtDept.Focus();
            return Task.FromResult(false);
        }
        #endregion

        return Task.FromResult(result);
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    private void InitDictItem()
    {
        treeFunction.Nodes.Clear();//清空设计节点
    }

    /// <summary>
    /// 数据显示的函数
    /// </summary>
    public override async Task DisplayData()
    {
        InitDictItem();//数据字典加载（公用）

        if (!string.IsNullOrEmpty(ID))
        {
            #region 显示信息
            UserInfo info = await _bll.FindByIdAsync(ID);
            if (info != null)
            {
                _tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象

                await RefreshOUs(info.ID);
                await RefreshRoles(info.ID);
                await RefreshFunction(info.ID);

                //如果是管理员，不能设置为过期
                bool isAdmin = await _userRoleBll.UserIsSuperAdminAsync(info.ID);
                txtIsExpire.Enabled = !isAdmin;
                txtDeleted.Enabled = !isAdmin;

                cmbManager.SetComboBoxItem(info.PID.ToString());
                txtCompany.Value = info.CompanyId;
                txtDept.Value = info.DeptId;

                txtHandNo.Text = info.HandNo;
                txtName.Text = info.Name;
                txtFullName.Text = info.FullName;
                txtNickname.Text = info.Nickname;
                txtTitle.Text = info.Title;
                txtIdentityCard.Text = info.IdentityCard;
                txtMobilePhone.Text = info.MobilePhone;
                txtOfficePhone.Text = info.OfficePhone;
                txtHomePhone.Text = info.HomePhone;
                txtEmail.Text = info.Email;
                txtAddress.Text = info.Address;
                txtWorkAddr.Text = info.WorkAddr;
                txtGender.Text = info.Gender;
                txtBirthday.SetDateTime(info.Birthday);
                txtQq.Text = info.QQ;
                txtSignature.Text = info.Signature;
                txtAuditStatus.Text = info.AuditStatus;
                txtNote.Text = info.Note;
                txtCustomField.Text = info.CustomField;                   
                txtSortCode.Text = info.SortCode;
                txtCreator.Text = info.Creator;
                txtCreationDate.SetDateTime(info.CreationDate);
                txtIsExpire.Checked = info.IsExpire;
                txtDeleted.Checked = info.Deleted;
            }
            #endregion
            //this.btnOK.Enabled = GB.HasFunction("User/Edit");             
        }
        else
        {
            txtCreator.Text = GB.LoginUserInfo.FullName;//默认为当前登录用户
            txtCreationDate.DateTime = DateTime.Now; //默认当前时间
            //this.btnOK.Enabled = GB.HasFunction("User/Add");  
        }

        //tempInfo在对象存在则为指定对象，新建则是全新的对象，但有一些初始化的GUID用于附件上传
        //SetAttachInfo(tempInfo);
    }

    //private void SetAttachInfo(UserInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = txtName.Text;
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, tempInfo.ID, LoginUserInfo.Name);
    //    }
    //}

    public override async Task ClearScreen()
    {
        _tempInfo = new UserInfo();
        await base.ClearScreen();
    }

    /// <summary>
    /// 编辑或者保存状态下取值函数
    /// </summary>
    /// <param name="info"></param>
    private void SetInfo(UserInfo info)
    {
        info.PID = cmbManager.GetComboBoxValue().ToInt32();
        info.HandNo = txtHandNo.Text;
        info.Name = txtName.Text;
        info.FullName = txtFullName.Text;
        info.Nickname = txtNickname.Text;
        info.IsExpire = txtIsExpire.Checked;
        info.Title = txtTitle.Text;
        info.IdentityCard = txtIdentityCard.Text;
        info.MobilePhone = txtMobilePhone.Text;
        info.OfficePhone = txtOfficePhone.Text;
        info.HomePhone = txtHomePhone.Text;
        info.Email = txtEmail.Text;
        info.Address = txtAddress.Text;
        info.WorkAddr = txtWorkAddr.Text;
        info.Gender = txtGender.Text;
        info.Birthday = txtBirthday.DateTime;
        info.QQ = txtQq.Text;
        info.Signature = txtSignature.Text;
        info.AuditStatus = txtAuditStatus.Text;
        info.Note = txtNote.Text;
        info.CustomField = txtCustomField.Text;
        info.DeptId = txtDept.Value;
        info.DeptName = txtDept.Text;
        info.CompanyId = txtCompany.Value;
        info.CompanyName = txtCompany.Text;
        info.SortCode = txtSortCode.Text;
        info.Editor = GB.LoginUserInfo.FullName;
        info.LastUpdatedBy = GB.LoginUserInfo.ID.ToString();
        info.LastUpdateDate = DateTime.Now;
        info.Deleted = txtDeleted.Checked;

        info.CurrentLoginUserId = GB.LoginUserInfo.ID.ToString();
    }

    /// <summary>
    /// 新增状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveAddNew()
    {
        UserInfo info = _tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
        SetInfo(info);
        info.Creator = GB.LoginUserInfo.FullName;
        info.CreatedBy = GB.LoginUserInfo.ID.ToString();
        info.CreationDate = DateTime.Now;

        try
        {
            #region 新增数据

            bool succeed = await _bll.InsertAsync(info);
            if (succeed)
            {
                //可添加其他关联操作

                return true;
            }
            #endregion
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();
            ex.Message.ShowUxError();
        }
        return false;
    }

    /// <summary>
    /// 编辑状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveUpdated()
    {
        UserInfo info = await _bll.FindByIdAsync(ID);
        if (info != null)
        {
            SetInfo(info);

            try
            {
                #region 更新数据
                bool succeed = await _bll.UpdateAsync(info);
                if (succeed)
                {
                    //可添加其他关联操作

                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.ToString().LogError();
                ex.Message.ShowUxError();
            }
        }
        return false;
    }

    private void txtIdentityCard_Validated(object? sender, EventArgs e)
    {
        if (txtIdentityCard.Text.Trim().Length > 0)
        {
            GenerateBirthdays();
        }
        else
        {
            //this.txtBirthday.Text = "";
            //this.txtSex.Text = "";
        }
    }

    private void GenerateBirthdays()
    {
        string idCardNo = txtIdentityCard.Text.Trim();
        if (!string.IsNullOrEmpty(idCardNo))
        {
            string result = IdCardHelper.Validate(idCardNo);
            if (!string.IsNullOrEmpty(result))
            {
                result.ShowUxTips();
                txtIdentityCard.Focus();
                return;
            }

            DateTime birthDay = IdCardHelper.GetBirthday(idCardNo);
            int age = DateTime.Now.Year - birthDay.Year;
            string sex = IdCardHelper.GetSexName(idCardNo);

            txtBirthday.DateTime = birthDay;
            //this.txtAge.Value = age;
            txtGender.Text = sex;
            txtMobilePhone.Focus();
        }
    }

    private async Task RefreshOUs(int id)
    {
        lvwOU.BeginUpdate();
        lvwOU.Items.Clear();

        List<OUInfo> list = await _ouBll.GetOUsByUserAsync(id);
        foreach (OUInfo info in list)
        {
            lvwOU.Items.Add(info.Name);
        }
        lvwOU.EndUpdate();
    }

    private async Task RefreshRoles(int id)
    {
        lvwRole.BeginUpdate();
        lvwRole.Items.Clear();

        List<RoleInfo> list = await _userRoleBll.GetRolesByUserAsync(id);
        foreach (RoleInfo info in list)
        {
            lvwRole.Items.Add(info.Name);
        }
        lvwRole.EndUpdate();
    }
        
    public async Task RefreshFunction(int id)
    {
        treeFunction.BeginUpdate();
        treeFunction.Nodes.Clear();

        List<SystemTypeInfo> typeList = await _systemTypeBll.GetAllAsync();
        foreach (SystemTypeInfo typeInfo in typeList)
        {
            TreeNode parentNode = treeFunction.Nodes.Add(typeInfo.Oid, typeInfo.Name, 0, 0);
            List<MenuNodeInfo> list = await _menuBll.GetMenuNodesByUser(id, typeInfo.Oid);
            AddFunctionNode(parentNode, list);                
        }

        treeFunction.ExpandAll();
        treeFunction.EndUpdate();            
    }

    private void AddFunctionNode(TreeNode node, List<MenuNodeInfo> list)
    {
        foreach (MenuNodeInfo info in list)
        {
            TreeNode subNode =  node.Nodes.Add(info.FunctionId, info.Name, 1, 1);

            AddFunctionNode(subNode, info.Children);
        }
    }
}