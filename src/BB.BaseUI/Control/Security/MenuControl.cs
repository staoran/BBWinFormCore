using System.Data;
using BB.Entity.Security;
using BB.HttpServices.Core.Menu;
using BB.Tools.Format;
using Furion;

namespace BB.BaseUI.Control.Security;

/// <summary>
/// 菜单显示控件
/// </summary>
public partial class MenuControl : UserControl
{
    /// <summary>
    /// 选择的值发生变化的时候
    /// </summary>
    public event EventHandler EditValueChanged;

    public MenuControl()
    {
        InitializeComponent();

        txtMenu.EditValueChanged += txtMenu_EditValueChanged;
    }

    void txtMenu_EditValueChanged(object sender, EventArgs e)
    {
        if (EditValueChanged != null)
        {
            EditValueChanged(sender, e);
        }
    }

    private void FunctionControl_Load(object sender, EventArgs e)
    {
        if (!DesignMode)
        {
            Init();
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public async void Init()
    {
        DataTable dt = DataTableHelper.CreateTable("ImageIndex|int,ID,PID,Name,FunctionId,SystemType_ID,Seq");
        List<MenuInfo> list = await App.GetService<MenuHttpService>().GetAllAsync();
        DataRow dr = null;
        foreach (MenuInfo info in list)
        {
            dr = dt.NewRow();
            dr["ImageIndex"] = 0;
            dr["ID"] = info.ID;
            dr["PID"] = info.PID;
            dr["Name"] = info.Name;
            dr["FunctionId"] = info.FunctionId;
            dr["SystemType_ID"] = info.SystemTypeId;
            dr["Seq"] = info.Seq;
            dt.Rows.Add(dr);
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

        txtMenu.Properties.TreeList.KeyFieldName = "ID";
        txtMenu.Properties.TreeList.ParentFieldName = "PID";
        txtMenu.Properties.DataSource = dt;
        txtMenu.Properties.ValueMember = "ID";
        txtMenu.Properties.DisplayMember = "Name";
    }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public override string Text
    {
        get => txtMenu.Text;
        set => txtMenu.Text = value;
    }

    /// <summary>
    /// 菜单ID
    /// </summary>
    public string Value
    {
        // 值为“0”的时候，是默认的“无”行记录
        get
        {
            string result = "-1";
            if (txtMenu.EditValue == null || txtMenu.EditValue.ToString() == "0")
            {
                result = "-1";
            }
            else
            {
                result = txtMenu.EditValue.ToString();
            }
            return result;
        }
        set
        {
            if (value == "-1")
            {
                txtMenu.EditValue = "0";
            }
            else
            {
                txtMenu.EditValue = value;
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