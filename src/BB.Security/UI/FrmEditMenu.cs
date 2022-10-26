using System.Windows.Forms;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Tools.Entity;
using BB.Entity.Security;
using BB.HttpServices.Core.Menu;
using BB.HttpServices.Core.SystemType;
using Furion.Logging.Extensions;

namespace BB.Security.UI;

// public partial class FrmEditMenu : BaseEditDesigner
public partial class FrmEditMenu : BaseEditForm
{
    /// <summary>
    /// 系统标识ID
    /// </summary>
    public string SystemTypeId = "";

    /// <summary>
    /// 上级菜单ID
    /// </summary>
    public string PID = "";

    private readonly SystemTypeHttpService _systemTypeBll;
    private readonly MenuHttpService _bll;

    public FrmEditMenu(MenuHttpService bll, SystemTypeHttpService systemTypeBll)
    {
        InitializeComponent();
        _systemTypeBll = systemTypeBll;
        _bll = bll;

        menuControl1.EditValueChanged += menuControl1_EditValueChanged;
    }

    void menuControl1_EditValueChanged(object? sender, EventArgs e)
    {
        DisplaySystemType();
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
        #endregion

        return Task.FromResult(result);
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    private async Task InitDictItem()
    {
        //初始化代码

        //绑定系统类型
        List<SystemTypeInfo> systemList = await _systemTypeBll.GetAllAsync();
        foreach (SystemTypeInfo info in systemList)
        {
            txtSystemType.Properties.Items.Add(new CListItem(info.Name, info.Oid));
        }
        if (txtSystemType.Properties.Items.Count == 1)
        {
            txtSystemType.SelectedIndex = 0;
        }
        
        txtMenuType.BindDictItems("菜单类型", "1", false, false);
    }

    /// <summary>
    /// 数据显示的函数
    /// </summary>
    public override async Task DisplayData()
    {
        await InitDictItem();//数据字典加载（公用）

        if (!string.IsNullOrEmpty(ID))
        {
            MenuInfo info = await _bll.FindByIdAsync(ID);
            menuControl1.Value = info.PID;
            txtName.Text = info.Name;
            txtIcon.Text = info.Icon;
            txtSeq.Text = info.Seq;
            txtFunctionId.Text = info.FunctionId;
            txtVisible.Checked = info.Visible;
            txtWinformType.Text = info.WinformType;
            txtUrl.Text = info.Url;
            txtWebIcon.Text = info.WebIcon;
            txtSystemType.SetComboBoxItem(info.SystemTypeId);//设置系统类型
            txtMenuType.SetComboBoxItem(info.MenuType);

            //this.btnOK.Enabled = GB.HasFunction("Menu/Edit");             
        }
        else
        {
            //this.btnOK.Enabled = GB.HasFunction("Menu/Add");  
            if (!string.IsNullOrEmpty(SystemTypeId))
            {
                txtSystemType.SetComboBoxItem(SystemTypeId); 
            }
        }

        DisplaySystemType();
    }

    /// <summary>
    /// 编辑或者保存状态下取值函数
    /// </summary>
    /// <param name="info"></param>
    private void SetInfo(MenuInfo info)
    {
        info.PID = menuControl1.Value;
        info.Name = txtName.Text;
        info.Icon = txtIcon.Text;
        info.Seq = txtSeq.Text;
        info.FunctionId = txtFunctionId.Text;
        info.Visible = txtVisible.Checked;
        info.WinformType = txtWinformType.Text;
        info.Url = txtUrl.Text;
        info.WebIcon = txtWebIcon.Text;
        info.SystemTypeId = txtSystemType.GetComboBoxValue();
        info.MenuType = txtMenuType.GetComboBoxValue();

        info.CurrentLoginUserId = GB.LoginUserInfo.ID.ToString();
    }

    public override async Task ClearScreen()
    {
        int intSeq = 0;
        string seqValue = txtSeq.Text;
        string pid = menuControl1.Value;
        await base.ClearScreen();

        txtVisible.Checked = true;
        txtUrl.Text = "#";
        if (int.TryParse(seqValue, out intSeq))
        {
            txtSeq.Text = (intSeq + 1).ToString().PadLeft(seqValue.Trim().Length, '0');
        }
        menuControl1.Value = pid;
    }

    /// <summary>
    /// 新增状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveAddNew()
    {
        MenuInfo info = new MenuInfo();
        SetInfo(info);

        try
        {
            #region 新增数据

            bool succeed = await _bll.InsertAsync(info);
            if (succeed)
            {
                if (menuControl1.Value == "-1")
                {
                    string pid = info.ID;//先记录原来的ID，作为PID

                    //如果顶级菜单项目添加，同时添加一个二级菜单项目
                    info.PID = pid;
                    info.ID = Guid.NewGuid().ToString();
                    info.Seq = "001";
                    await _bll.InsertAsync(info);
                }                    

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
        MenuInfo info = await _bll.FindByIdAsync(ID);
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

    private void FrmEditMenu_Load(object? sender, EventArgs e)
    {

    }

    private void DisplaySystemType()
    {
        if (menuControl1.Value == "-1")
        {
            layoutSystemType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        else
        {
            layoutSystemType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
    }

    private void btnSelectIcon_Click(object? sender, EventArgs e)
    {
        FrmImageGallery dlg = new FrmImageGallery();
        dlg.OnIconSelected += (image, name) =>
        {
            if (!string.IsNullOrEmpty(name))
            {
                txtIcon.Text = name;
            }
        };
        dlg.ShowDialog();
    }

    private void btnSelectWebIcon_Click(object? sender, EventArgs e)
    {
        FrmImageGallery dlg = new FrmImageGallery();
        dlg.OnIconSelected += (image, name) =>
        {
            if (!string.IsNullOrEmpty(name))
            {
                txtWebIcon.Text = name;
            }
        };
        dlg.ShowDialog();
    }

    private string GetIconPath()
    {
        string iconFile = "Icon File(*.ico)|*.ico|Image Files(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png;*.PNG)|(*.BMP;*.bmp;*.JPG;*.jpg;*.GIF;*.gif;*.png;*.PNG)|All File(*.*)|*.*";
        string file = FileDialogHelper.Open("选择图标文件", iconFile, "", Application.StartupPath);
        string result = "";
        if (!string.IsNullOrEmpty(file))
        {
            result = file.Replace(Application.StartupPath, "").Trim('\\');
        }

        return result;
    }
}