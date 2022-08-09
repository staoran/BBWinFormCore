using System.Data;
using BB.BaseUI.Other;
using BB.Entity.Security;
using BB.HttpService.OU;
using BB.Tools.Format;
using DevExpress.XtraEditors;
using Furion;

namespace BB.BaseUI.Control.Security;

/// <summary>
/// 部门显示控件
/// </summary>
public partial class DeptControl : XtraUserControl
{
    public string ParentOUId = "-1";

    /// <summary>
    /// 选择的值发生变化的时候
    /// </summary>
    public event EventHandler EditValueChanged;

    public DeptControl()
    {
        InitializeComponent();

        txtDept.EditValueChanged += cmbUpperOU_EditValueChanged;
    }

    void cmbUpperOU_EditValueChanged(object sender, EventArgs e)
    {
        if (EditValueChanged != null)
        {
            EditValueChanged(sender, e);
        }
    }

    private async void DeptControl_Load(object sender, EventArgs e)
    {
        if (!DesignMode)
        {
            //限定用户的选择级别
            List<OUInfo> list = await GB.GetMyTopGroup();
            foreach (OUInfo ouInfo in list)
            {
                if (ouInfo != null)
                {
                    ParentOUId = ouInfo.HandNo;
                }
            }

            Init();
        }
    }

    /// <summary>
    /// 初始化部门信息
    /// </summary>
    public async void Init()
    { 
        //InitUpperOU
        DataTable dt = DataTableHelper.CreateTable("ImageIndex|int,ID,PID,Name,HandNo,Category,Address,Note");
        DataRow dr = null;

        if (!string.IsNullOrEmpty(ParentOUId))
        {
            var _bll = App.GetService<OUHttpService>();
            List<OUInfo> list = await _bll.GetAllOUsByParentAsync(ParentOUId);
            OUInfo parentInfo = await _bll.FindByIdAsync(ParentOUId);
            if (parentInfo != null)
            {
                list.Insert(0, parentInfo);
            }
                
            foreach (OUInfo info in list)
            {
                dr = dt.NewRow();
                dr["ImageIndex"] = GB.GetImageIndex(info.Category);
                dr["ID"] = info.HandNo;
                dr["PID"] = info.PID;
                dr["Name"] = info.Name;
                dr["HandNo"] = info.HandNo;
                dr["Category"] = info.Category;
                dr["Address"] = info.Address;
                dr["Note"] = info.Note;

                dt.Rows.Add(dr);
            }
        }
        //增加一行空的
        dr = dt.NewRow();
        dr["ID"] = "0"; //使用0代替-1，避免出现节点的嵌套显示，因为-1已经作为了一般节点的顶级标识
        dr["PID"] = "-1";
        dr["Name"] = "无";
        dt.Rows.InsertAt(dr, 0);

        //设置图形序号
        treeListLookUpEdit1TreeList.SelectImageList = imageList2;
        treeListLookUpEdit1TreeList.StateImageList = imageList2;

        txtDept.Properties.TreeList.KeyFieldName = "ID";
        txtDept.Properties.TreeList.ParentFieldName = "PID";
        txtDept.Properties.DataSource = dt;
        txtDept.Properties.ValueMember = "ID";
        txtDept.Properties.DisplayMember = "Name";
    }

    /// <summary>
    /// 部门名称
    /// </summary>
    public override string Text
    {
        get => txtDept.Text;
        set => txtDept.Text = value;
    }

    /// <summary>
    /// 部门ID
    /// </summary>
    public string Value
    {
        // 值为“0”的时候，是默认的“无”行记录
        get
        {
            string result = "-1";
            if (txtDept.EditValue == null || txtDept.EditValue.ToString() == "0")
            {
                result = "-1";
            }
            else
            {
                result = txtDept.EditValue.ToString();
            }
            return result;
        }
        set
        {
            if (value == "-1" )
            {
                txtDept.EditValue = "0";
            }
            else
            {
                txtDept.EditValue = value;
            }
        }
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        if (!DesignMode)
        {
            Init();
        }
    }
}