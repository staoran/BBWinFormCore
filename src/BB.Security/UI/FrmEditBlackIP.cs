using System.Windows.Forms;
using System.Net;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Tools.Entity;
using BB.Tools.Format;
using BB.Entity.Security;
using BB.HttpServices.Core.BlackIP;
using Furion;
using Furion.Logging.Extensions;

namespace BB.Security.UI;

public partial class FrmEditBlackIp : BaseEditForm
{
    /// <summary>
    /// 创建一个临时对象，方便在附件管理中获取存在的GUID
    /// </summary>
    private BlackIpInfo _tempInfo = new BlackIpInfo();

    private readonly BlackIPHttpService _bll;

    public FrmEditBlackIp(BlackIPHttpService bll)
    {
        InitializeComponent();
        _bll = bll;
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
            "请输入显示名称".ShowUxTips();
            txtName.Focus();
            result = false;
        }
        else if (txtAuthorizeType.Text.Length == 0)
        {
            "请选择授权类型".ShowUxTips();
            txtAuthorizeType.Focus();
            result = false;
        }
        else if (txtIPStart.Text.Length == 0)
        {
            "请输入IP起始地址".ShowUxTips();
            txtIPStart.Focus();
            result = false;
        }
        else if (txtIPEnd.Text.Length == 0)
        {
            "请输入IP结束地址".ShowUxTips();
            txtIPEnd.Focus();
            result = false;
        }

        IPAddress ip1 = IPAddress.Parse(txtIPStart.Text);
        IPAddress ip2 = IPAddress.Parse(txtIPEnd.Text);

        if (ip1.Compare(ip2) == 1)
        {
            "请IP开始地址不能大于结束地址, 请修改".ShowUxTips();
            txtIPEnd.Focus();
            result = false;
        }

        #endregion

        return Task.FromResult(result);
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    private void InitDictItem()
    {
        //初始化分类
        Dictionary<string, object> dictEnum = EnumHelper.GetMemberKeyValue<AuthrizeType>();
        txtAuthorizeType.Properties.Items.Clear();
        foreach (string item in dictEnum.Keys)
        {
            txtAuthorizeType.Properties.Items.Add(new CListItem(dictEnum[item].ToString(), item));
        }
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
            BlackIpInfo info = await _bll.FindByIdAsync(ID);
            if (info != null)
            {
                _tempInfo = info;//重新给临时对象赋值，使之指向存在的记录对象

                txtName.Text = info.Name;
                txtAuthorizeType.SetComboBoxItem(info.AuthorizeType.ToString());
                txtForbid.Checked = info.Forbid;
                txtIPStart.Text = info.IPStart;
                txtIPEnd.Text = info.IPEnd;
                txtNote.Text = info.Note;
                txtCreator.Text = info.Creator;
                txtCreationDate.SetDateTime(info.CreationDate);
            }
            #endregion
            //this.btnOK.Enabled = GB.HasFunction("BlackIP/Edit");             
        }
        else
        {
            txtCreator.Text = GB.LoginUserInfo.FullName;//默认为当前登录用户
            txtCreationDate.DateTime = DateTime.Now; //默认当前时间
            //this.btnOK.Enabled = GB.HasFunction("BlackIP/Add");  
        }

        await RefreshUsers();
    }

    public override async Task ClearScreen()
    {
        _tempInfo = new BlackIpInfo();
        await base.ClearScreen();
    }

    /// <summary>
    /// 编辑或者保存状态下取值函数
    /// </summary>
    /// <param name="info"></param>
    private void SetInfo(BlackIpInfo info)
    {
        info.Name = txtName.Text;
        info.AuthorizeType = txtAuthorizeType.GetComboBoxValue().ToInt32();
        info.Forbid = txtForbid.Checked;
        info.IPStart = txtIPStart.Text;
        info.IPEnd = txtIPEnd.Text;
        info.Note = txtNote.Text;
        info.Editor = GB.LoginUserInfo.FullName;
        info.LastUpdatedBy = GB.LoginUserInfo.ID.ToString();
        info.LastUpdateDate = DateTime.Now;

        info.CurrentLoginUserId = GB.LoginUserInfo.ID.ToString(); //记录当前登录的用户信息，供操作日志记录使用
    }

    /// <summary>
    /// 新增状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveAddNew()
    {
        BlackIpInfo info = _tempInfo;//必须使用存在的局部变量，因为部分信息可能被附件使用
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

        BlackIpInfo info = await _bll.FindByIdAsync(ID);
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

    private void FrmEditBlackIP_Load(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 记录用户的选择情况
    /// </summary>
    Dictionary<string, string> _selectUserDict = new Dictionary<string, string>();
    private async Task RefreshUsers()
    {
        _selectUserDict = new Dictionary<string, string>();

        lvwUser.BeginUpdate();
        lvwUser.Items.Clear();
        List<SimpleUserInfo> list = await _bll.GetSimpleUserByBlackIpAsync(_tempInfo.ID);
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

    private List<int> _addedUserList = new List<int>();
    private List<int> _deletedUserList = new List<int>();

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
        FrmSelectUser dlg = App.GetService<FrmSelectUser>();
        dlg.SelectUserDict = _selectUserDict;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
            GetUserDictChangs(_selectUserDict, dlg.SelectUserDict);

            foreach (int id in _deletedUserList)
            {
                await _bll.RemoveUserAsync(id, _tempInfo.ID);
            }
            foreach (int id in _addedUserList)
            {
                await _bll.AddUserAsync(id, _tempInfo.ID);
            }

            await RefreshUsers();
        }
        else
        {
            "请选择具体的机构".ShowUxTips();
        }
    }

    private async void btnRemoveUser_Click(object? sender, EventArgs e)
    {
        if (lvwUser.SelectedItem != null)
        {
            CListItem userItem = lvwUser.SelectedItem as CListItem;
            if (userItem != null)
            {
                int userId = Convert.ToInt32(userItem.Value);
                await _bll.RemoveUserAsync(userId, _tempInfo.ID);
                await RefreshUsers();
            }
        }
    }
}