using System.Reflection;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;

namespace BB.BaseUI.BaseUI;

partial class AboutBox : DevExpress.XtraEditors.XtraForm
{
    public AboutBox()
    {
        InitializeComponent();

        //this.Text = String.Format("关于 {0} ", AssemblyTitle);
        //this.labelProductName.Text = AssemblyProduct;
        //this.labelVersion.Text = String.Format("版本 {0} ", AssemblyVersion);
        //this.labelCopyright.Text = AssemblyCopyright;
        //this.labelCompanyName.Text = AssemblyCompany;
        //this.textBoxDescription.Text = AssemblyDescription;

        #region 初始化系统名称
        try
        {
            string manufacturer = GB.Config.AppConfigGet("Manufacturer");
            string certificatedCompany = GB.Config.AppConfigGet("CertificatedCompany");
            string applicationName = GB.Config.AppConfigGet("ApplicationName");

            Text = $"{certificatedCompany}-{applicationName}";
            lblProductName.Text = applicationName;
            lblVersion.Text = $"版本 {AssemblyVersion}";
            lblCopyright.Text = AssemblyCopyright;
            lblCertificated.Text = $"授权【{certificatedCompany}】使用";
            string description = GB.Config.AppConfigGet("Description");//软件介绍
            txtDescription.Text = description;
            lblContact.Text = AssemblyDescription;//联系方式
        }
        catch
        {
            // ignored
        }

        #endregion 
    }

    #region 程序集属性访问器

    public string AssemblyTitle
    {
        get
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (titleAttribute.Title != "")
                {
                    return titleAttribute.Title;
                }
            }
            return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
        }
    }

    public string AssemblyVersion
    {
        get
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }

    public string AssemblyDescription
    {
        get
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }
    }

    public string AssemblyProduct
    {
        get
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyProductAttribute)attributes[0]).Product;
        }
    }

    public string AssemblyCopyright
    {
        get
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }
    }

    public string AssemblyCompany
    {
        get
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyCompanyAttribute)attributes[0]).Company;
        }
    }
    #endregion

    private void lblContact_Click(object? sender, EventArgs e)
    {            
        if (lblContact.Text.Trim().Length > 0)
        {
            Clipboard.SetText(lblContact.Text);
            "感谢您的支持！".ShowUxTips();
        }
    }

    private void AboutBox_KeyUp(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            Close();
        }
    }

}