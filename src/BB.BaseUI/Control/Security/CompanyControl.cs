using System.Data;
using BB.BaseUI.Other;
using BB.Entity.Security;
using BB.HttpServices.Core.OU;
using BB.Tools.Format;
using DevExpress.XtraEditors;
using Furion;

namespace BB.BaseUI.Control.Security;

/// <summary>
/// 公司显示控件
/// </summary>
public partial class CompanyControl : XtraUserControl
{
    /// <summary>
    /// 选择的值发生变化的时候
    /// </summary>
    public event EventHandler EditValueChanged;

    public CompanyControl()
    {
        InitializeComponent();

        txtCompany.EditValueChanged += txtCompany_EditValueChanged;
    }

    void txtCompany_EditValueChanged(object? sender, EventArgs e)
    {
        if (EditValueChanged != null)
        {
            EditValueChanged(sender, e);
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public async void Init()
    {
        DataTable dt = DataTableHelper.CreateTable("ImageIndex|int,ID,PID,Name");
        List<OUInfo> list = new();
        var _bll = App.GetService<OUHttpService>();
        if(GB.UserInRole(RoleInfo.SUPER_ADMIN_NAME))
        {
            list = await _bll.GetGroupCompanyAsync();
        }
        else
        {
            OUInfo myCompanyInfo = await _bll.FindByIdAsync(GB.LoginUserInfo.CompanyId);
            if (myCompanyInfo != null)
            {
                list.Add(myCompanyInfo);
            }
        }

        DataRow dr = null;
        foreach (OUInfo info in list)
        {
            dr = dt.NewRow();
            dr["ImageIndex"] = GB.GetImageIndex(info.Category);
            dr["ID"] = info.HandNo;
            dr["PID"] = info.PID;
            dr["Name"] = info.Name;
            dt.Rows.Add(dr);
        }

        ////增加一行空的
        //dr = dt.NewRow();
        //dr["ID"] = "0";
        //dr["PID"] = "-1";
        //dr["Name"] = "无";
        //dt.Rows.InsertAt(dr, 0);

        //设置图形序号
        treeListLookUpEdit1TreeList.SelectImageList = imageList2;
        treeListLookUpEdit1TreeList.StateImageList = imageList2;

        txtCompany.Properties.TreeList.KeyFieldName = "ID";
        txtCompany.Properties.TreeList.ParentFieldName = "PID";
        txtCompany.Properties.DataSource = dt;
        txtCompany.Properties.ValueMember = "ID";
        txtCompany.Properties.DisplayMember = "Name";
    }

    /// <summary>
    /// 公司名称
    /// </summary>
    public override string Text
    {
        get => txtCompany.Text;
        set => txtCompany.Text = value;
    }

    /// <summary>
    /// 公司ID
    /// </summary>
    public string Value
    {
        get
        {
            string result;
            if (txtCompany.EditValue == null || txtCompany.EditValue.ToString() == "0")
            {
                result = "-1";
            }
            else
            {                  
                result = txtCompany.EditValue.ToString();
            }
            return result;
        }
        set => txtCompany.EditValue = value;
    }

    private void CompanyControl_Load(object? sender, EventArgs e)
    {
        if (!DesignMode)
        {
            Init();
        }
    }

    private void btnRefresh_Click(object? sender, EventArgs e)
    {
        if (!DesignMode)
        {
            Init();
        }
    }
}