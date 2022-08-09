using System.ComponentModel;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace BB.BaseUI.Control;

/// <summary>
/// 自定义LookupEdit控件
/// </summary>
[UserRepositoryItem("RegisterCustomGridLookUpEdit")]
public class RepositoryItemCustomGridLookUpEdit : RepositoryItemGridLookUpEdit
{
    static RepositoryItemCustomGridLookUpEdit() { RegisterCustomGridLookUpEdit(); }

    public RepositoryItemCustomGridLookUpEdit()
    {
        TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        AutoComplete = false;
    }
    [Browsable(false)]
    public override DevExpress.XtraEditors.Controls.TextEditStyles TextEditStyle
    {
        get => base.TextEditStyle;
        set => base.TextEditStyle = value;
    }
    public const string CUSTOM_GRID_LOOK_UP_EDIT_NAME = "CustomGridLookUpEdit";

    public override string EditorTypeName => CUSTOM_GRID_LOOK_UP_EDIT_NAME;

    public static void RegisterCustomGridLookUpEdit()
    {
        EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CUSTOM_GRID_LOOK_UP_EDIT_NAME,
            typeof(CustomGridLookUpEdit), typeof(RepositoryItemCustomGridLookUpEdit),
            typeof(GridLookUpEditBaseViewInfo), new ButtonEditPainter(), true));
    }

    protected override ColumnView CreateViewInstance()
    {
        return new CustomGridView();
    }

    //protected override GridView CreateViewInstance() { return new CustomGridView(); }
    protected override GridControl CreateGrid() { return new CustomGridControl(); }
}
    
/// <summary>
/// 自定义的GridLookUpEdit控件
/// </summary>
public class CustomGridLookUpEdit : GridLookUpEdit
{
    /// <summary>
    /// 是否禁止新增内容
    /// </summary>
    [Browsable(true), Description("是否禁止新增内容")]
    public bool DisableAddNew { get; set; }

    static CustomGridLookUpEdit()
    {
        RepositoryItemCustomGridLookUpEdit.RegisterCustomGridLookUpEdit();
    }

    public CustomGridLookUpEdit() : base() 
    {
        //初始化一些状态
        Properties.PopupFilterMode = PopupFilterMode.Contains;//包含即可
        Properties.ImmediatePopup = true;//是否马上弹出窗体
        Properties.ValidateOnEnterKey = true;//回车确认
        Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;//文本框可输入
        Properties.NullText = "";
        Properties.NullValuePrompt = "";

        ProcessNewValue += CustomGridLookUpEdit_ProcessNewValue;            
    }
        
    /// <summary>
    /// 实现在列表没有记录的时候，可以录入一个不存在的记录，类似ComoboEidt功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void CustomGridLookUpEdit_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
    {
        if (!DisableAddNew && !DesignMode)
        {
            string displayName = Properties.DisplayMember;
            string valueName = Properties.ValueMember;
            string display = e.DisplayValue.ToString();

            DataTable dtTemp = Properties.DataSource as DataTable;
            if (dtTemp != null)
            {
                DataRow[] selectedRows = dtTemp.Select($"{displayName}='{display.Replace("'", "‘")}'");
                if (selectedRows == null || selectedRows.Length == 0)
                {
                    DataRow row = dtTemp.NewRow();
                    row[displayName] = display;
                    row[valueName] = display;
                    dtTemp.Rows.Add(row);
                    dtTemp.AcceptChanges();
                }
            }

            e.Handled = true;
        }
    }

    public override string EditorTypeName => RepositoryItemCustomGridLookUpEdit.CUSTOM_GRID_LOOK_UP_EDIT_NAME;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public new RepositoryItemCustomGridLookUpEdit Properties => base.Properties as RepositoryItemCustomGridLookUpEdit;

    public override string Text
    {
        get => base.Text;
        set
        {
            if (!DisableAddNew && !DesignMode)
            {
                //如果列表里面没有，添加一个，否则无法显示
                string displayName = Properties.DisplayMember;
                string valueName = Properties.ValueMember;
                string display = value;

                DataTable dtTemp = Properties.DataSource as DataTable;
                if (dtTemp != null)
                {
                    DataRow[] selectedRows = dtTemp.Select($"{displayName}='{display.Replace("'", "‘")}'");
                    if (selectedRows == null || selectedRows.Length == 0)
                    {
                        DataRow row = dtTemp.NewRow();
                        row[displayName] = display;
                        row[valueName] = display;
                        dtTemp.Rows.Add(row);
                        dtTemp.AcceptChanges();
                    }
                }
                base.Text = value;
            }
            else
            {
                base.Text = value;
            }
        }
    }

}