namespace BB.Security.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.tool_User = new DevExpress.XtraBars.BarButtonItem();
            this.tool_OU = new DevExpress.XtraBars.BarButtonItem();
            this.tool_Role = new DevExpress.XtraBars.BarButtonItem();
            this.tool_Function = new DevExpress.XtraBars.BarButtonItem();
            this.tool_Quit = new DevExpress.XtraBars.BarButtonItem();
            this.rgbiSkins = new DevExpress.XtraBars.RibbonGalleryBarItem();
            this.tool_LoginLog = new DevExpress.XtraBars.BarButtonItem();
            this.tool_SysMenu = new DevExpress.XtraBars.BarButtonItem();
            this.tool_SystemType = new DevExpress.XtraBars.BarButtonItem();
            this.tool_BlackIP = new DevExpress.XtraBars.BarButtonItem();
            this.tool_OperationLog = new DevExpress.XtraBars.BarButtonItem();
            this.btnFeeBack = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ApplicationButtonText = null;
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Images = this.imageCollection1;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.tool_User,
            this.tool_OU,
            this.tool_Role,
            this.tool_Function,
            this.tool_Quit,
            this.rgbiSkins,
            this.tool_LoginLog,
            this.tool_SysMenu,
            this.tool_SystemType,
            this.tool_BlackIP,
            this.tool_OperationLog,
            this.btnFeeBack});
            this.ribbonControl.LargeImages = this.imageCollection1;
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 14;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.PageHeaderItemLinks.Add(this.btnFeeBack);
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl.ShowToolbarCustomizeItem = false;
            this.ribbonControl.Size = new System.Drawing.Size(1008, 148);
            this.ribbonControl.Toolbar.ShowCustomizeItem = false;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "resource1.ico");
            this.imageCollection1.Images.SetKeyName(1, "User.ICO");
            this.imageCollection1.Images.SetKeyName(2, "Security.ico");
            this.imageCollection1.Images.SetKeyName(3, "key.ico");
            this.imageCollection1.Images.SetKeyName(4, "Quit.ico");
            this.imageCollection1.Images.SetKeyName(5, "order.ico");
            this.imageCollection1.Images.SetKeyName(6, "menu.ico");
            this.imageCollection1.Images.SetKeyName(7, "0036.ICO");
            this.imageCollection1.Images.SetKeyName(8, "user_32.png");
            this.imageCollection1.Images.SetKeyName(9, "warn.ico");
            // 
            // tool_User
            // 
            this.tool_User.Caption = "用户管理";
            this.tool_User.Id = 1;
            this.tool_User.LargeImageIndex = 0;
            this.tool_User.Name = "tool_User";
            this.tool_User.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tool_User_ItemClick);
            // 
            // tool_OU
            // 
            this.tool_OU.Caption = "组织机构管理";
            this.tool_OU.Id = 3;
            this.tool_OU.LargeImageIndex = 1;
            this.tool_OU.Name = "tool_OU";
            this.tool_OU.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tool_OU_ItemClick);
            // 
            // tool_Role
            // 
            this.tool_Role.Caption = "角色管理";
            this.tool_Role.Id = 4;
            this.tool_Role.LargeImageIndex = 2;
            this.tool_Role.Name = "tool_Role";
            this.tool_Role.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tool_Role_ItemClick);
            // 
            // tool_Function
            // 
            this.tool_Function.Caption = "功能管理";
            this.tool_Function.Id = 5;
            this.tool_Function.LargeImageIndex = 3;
            this.tool_Function.Name = "tool_Function";
            this.tool_Function.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tool_Function_ItemClick);
            // 
            // tool_Quit
            // 
            this.tool_Quit.Caption = "退出系统";
            this.tool_Quit.Id = 6;
            this.tool_Quit.LargeImageIndex = 4;
            this.tool_Quit.Name = "tool_Quit";
            this.tool_Quit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tool_Quit_ItemClick);
            // 
            // rgbiSkins
            // 
            this.rgbiSkins.Caption = "ribbonGalleryBarItem1";
            this.rgbiSkins.Id = 7;
            this.rgbiSkins.Name = "rgbiSkins";
            // 
            // tool_LoginLog
            // 
            this.tool_LoginLog.Caption = "用户登陆日志";
            this.tool_LoginLog.Id = 8;
            this.tool_LoginLog.ImageIndex = 5;
            this.tool_LoginLog.LargeImageIndex = 5;
            this.tool_LoginLog.Name = "tool_LoginLog";
            this.tool_LoginLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tool_LoginLog_ItemClick);
            // 
            // tool_SysMenu
            // 
            this.tool_SysMenu.Caption = "菜单管理";
            this.tool_SysMenu.Id = 9;
            this.tool_SysMenu.ImageIndex = 6;
            this.tool_SysMenu.LargeImageIndex = 6;
            this.tool_SysMenu.Name = "tool_SysMenu";
            this.tool_SysMenu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tool_SysMenu_ItemClick);
            // 
            // tool_SystemType
            // 
            this.tool_SystemType.Caption = "系统类型定义";
            this.tool_SystemType.Id = 10;
            this.tool_SystemType.ImageIndex = 7;
            this.tool_SystemType.LargeImageIndex = 7;
            this.tool_SystemType.Name = "tool_SystemType";
            this.tool_SystemType.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tool_SystemType_ItemClick);
            // 
            // tool_BlackIP
            // 
            this.tool_BlackIP.Caption = "系统黑白名单";
            this.tool_BlackIP.Id = 11;
            this.tool_BlackIP.ImageIndex = 8;
            this.tool_BlackIP.LargeImageIndex = 8;
            this.tool_BlackIP.Name = "tool_BlackIP";
            this.tool_BlackIP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tool_BlackIP_ItemClick);
            // 
            // tool_OperationLog
            // 
            this.tool_OperationLog.Caption = "用户操作日志";
            this.tool_OperationLog.Id = 12;
            this.tool_OperationLog.ImageIndex = 9;
            this.tool_OperationLog.LargeImageIndex = 9;
            this.tool_OperationLog.Name = "tool_OperationLog";
            this.tool_OperationLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.tool_OperationLog_ItemClick);
            // 
            // btnFeeBack
            // 
            this.btnFeeBack.Caption = "问题反馈";
            this.btnFeeBack.Description = "问题反馈";
            this.btnFeeBack.Glyph = ((System.Drawing.Image)(resources.GetObject("btnFeeBack.Glyph")));
            this.btnFeeBack.Hint = "问题反馈";
            this.btnFeeBack.Id = 13;
            this.btnFeeBack.Name = "btnFeeBack";
            this.btnFeeBack.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFeeBack_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "权限管理系统";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.tool_User);
            this.ribbonPageGroup1.ItemLinks.Add(this.tool_OU);
            this.ribbonPageGroup1.ItemLinks.Add(this.tool_Role);
            this.ribbonPageGroup1.ItemLinks.Add(this.tool_SystemType);
            this.ribbonPageGroup1.ItemLinks.Add(this.tool_Function);
            this.ribbonPageGroup1.ItemLinks.Add(this.tool_SysMenu);
            this.ribbonPageGroup1.ItemLinks.Add(this.tool_BlackIP, true);
            this.ribbonPageGroup1.ItemLinks.Add(this.tool_LoginLog);
            this.ribbonPageGroup1.ItemLinks.Add(this.tool_OperationLog);
            this.ribbonPageGroup1.ItemLinks.Add(this.tool_Quit);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "权限管理";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.rgbiSkins);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "界面皮肤";
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InActiveTabPageHeader;
            this.xtraTabbedMdiManager1.MdiParent = this;
            this.xtraTabbedMdiManager1.PageImagePosition = DevExpress.XtraTab.TabPageImagePosition.Far;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2010 Blue";
            // 
            // MainForm
            // 
            this.AllowMdiBar = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 695);
            this.Controls.Add(this.ribbonControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Ribbon = this.ribbonControl;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "步兵软件科技发展有限公司---权限管理系统     ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem tool_User;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem tool_OU;
        private DevExpress.XtraBars.BarButtonItem tool_Role;
        private DevExpress.XtraBars.BarButtonItem tool_Function;
        private DevExpress.XtraBars.BarButtonItem tool_Quit;
        private DevExpress.XtraBars.RibbonGalleryBarItem rgbiSkins;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.BarButtonItem tool_LoginLog;
        private DevExpress.XtraBars.BarButtonItem tool_SysMenu;
        private DevExpress.XtraBars.BarButtonItem tool_SystemType;
        private DevExpress.XtraBars.BarButtonItem tool_BlackIP;
        private DevExpress.XtraBars.BarButtonItem tool_OperationLog;
        private DevExpress.XtraBars.BarButtonItem btnFeeBack;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}