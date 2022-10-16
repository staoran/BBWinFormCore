using System.Data;
using BB.Entity.Security;
using BB.HttpServices.Core.Function;
using BB.Tools.Format;
using Furion;

namespace BB.BaseUI.Control.Security;

/// <summary>
/// 功能显示控件
/// </summary>
public partial class FunctionControl : UserControl
{
    /// <summary>
    /// 选择的值发生变化的时候
    /// </summary>
    public event EventHandler EditValueChanged;

    public FunctionControl()
    {
        InitializeComponent();

        txtFunction.EditValueChanged += txtFunction_EditValueChanged;
    }

    void txtFunction_EditValueChanged(object? sender, EventArgs e)
    {
        if (EditValueChanged != null)
        {
            EditValueChanged(sender, e);
        }
    }

    private void FunctionControl_Load(object? sender, EventArgs e)
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
        //InitUpperFunction
        DataTable dt = DataTableHelper.CreateTable("ImageIndex|int,ID,PID,Name,ControlID,SystemType_ID,SortCode");
        List<FunctionInfo> list = await App.GetService<FunctionHttpService>().GetAllAsync();
        DataRow dr = null;
        foreach (FunctionInfo info in list)
        {
            dr = dt.NewRow();
            dr["ImageIndex"] = 0;
            dr["ID"] = info.ID;
            dr["PID"] = info.PID;
            dr["Name"] = info.Name;
            dr["ControlID"] = info.ControlId;
            dr["SystemType_ID"] = info.SystemTypeId;
            dr["SortCode"] = info.SortCode;
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

        txtFunction.Properties.TreeList.KeyFieldName = "ID";
        txtFunction.Properties.TreeList.ParentFieldName = "PID";
        txtFunction.Properties.DataSource = dt;
        txtFunction.Properties.ValueMember = "ID";
        txtFunction.Properties.DisplayMember = "Name";
    }

    /// <summary>
    /// 功能名称
    /// </summary>
    public override string Text
    {
        get => txtFunction.Text;
        set => txtFunction.Text = value;
    }

    /// <summary>
    /// 功能ID
    /// </summary>
    public string Value
    {
        // 值为“0”的时候，是默认的“无”行记录
        get
        {
            string result = "-1";
            if (txtFunction.EditValue == null || txtFunction.EditValue.ToString() == "0")
            {
                result = "-1";
            }
            else
            {
                result = txtFunction.EditValue.ToString();
            }
            return result;
        }
        set
        {
            if (value == "-1")
            {
                txtFunction.EditValue = "0";
            }
            else
            {
                txtFunction.EditValue = value;
            }
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