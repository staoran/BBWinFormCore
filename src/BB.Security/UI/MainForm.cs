using System.Windows.Forms;

using DevExpress.XtraBars.Ribbon;
using System.Diagnostics;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Entity.Security;
using Furion;

namespace BB.Security.UI;

public partial class MainForm : RibbonForm
{
    public MainForm()
    {
        InitializeComponent();

        InitSkinGallery();
        InitForms();
    }

    void InitSkinGallery()
    {
        DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbiSkins, true);
        ribbonControl.Toolbar.ItemLinks.Clear();
        ribbonControl.Toolbar.ItemLinks.Add(rgbiSkins);
    }

    private void InitForms()
    {
        #region 初始化系统名称
        try
        {
            string manufacturer = GB.Config.AppConfigGet("Manufacturer");
            string applicationName = "权限管理系统";//config.AppConfigGet("ApplicationName");
            Text = $"{manufacturer}-{applicationName}";

        }
        catch
        { }
        #endregion

        ChildWinManagement.LoadMdiForm(this, typeof(FrmUser));
        ChildWinManagement.LoadMdiForm(this, typeof(FrmOu));
        ChildWinManagement.LoadMdiForm(this, typeof(FrmRole));

        //如果用户是公司管理员，不需要维护系统类型和功能点
        bool isCompanyAdmin = GB.UserInRole(RoleInfo.COMPANY_ADMIN_NAME);
        if (isCompanyAdmin)
        {
            tool_SystemType.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tool_Function.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tool_SysMenu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }
    }

    private void tool_User_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmUser));
    }

    private void tool_OU_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmOu));
    }

    private void tool_Role_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmRole));
    }

    private void tool_SystemType_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        FrmSystemType dlg = App.GetService<FrmSystemType>();
        dlg.ShowDialog();
    }
    private void tool_Function_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmFunction));
    }

    private void tool_SysMenu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmMenu));
    }

    private void tool_LoginLog_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmLoginLog));
    }

    private void tool_Quit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        if (DialogResult.Yes == "您确定退出权限系统么？".ShowYesNoAndUxTips())
        {
            Close();
        }
    }

    private void tool_BlackIP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmBlackIp));
    }

    private void tool_OperationLog_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        ChildWinManagement.LoadMdiForm(this, typeof(FrmOperationLog));
    }

    private void btnFeeBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
    {
        //使用QQ开放平台的发邮件界面
        string mailUrl = string.Format("https://mail.qq.com");
        Process.Start(mailUrl);
    }

}