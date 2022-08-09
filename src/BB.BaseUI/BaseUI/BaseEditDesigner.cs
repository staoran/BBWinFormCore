using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace BB.BaseUI.BaseUI;

public partial class BaseEditDesigner : BaseEditForm
{
    public BaseEditDesigner()
    {
        InitializeComponent();
    }
    
    public BaseEditDesigner(bool isDesignMode) : this()
    {
    }
    
    

    /// <summary>
    /// 初始化控件的显示信息
    /// </summary>
    /// <param name="info"></param>
    protected virtual void DisplayInfo(object info)
    {
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected virtual void SetPermit()
    {
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    protected virtual void InitDictItem()
    {
    }

    protected virtual void SetInfo(object tempInfo)
    {
    }

    /// <summary>
    /// 初始化明细表的GridView数据显示
    /// </summary>
    protected virtual void InitDetailGrid()
    {
    }

    /// <summary>
    /// 初始化新行
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected virtual void gridView1_InitNewRow(object s, InitNewRowEventArgs e)
    {
    }

    /// <summary>
    /// 行数据校验
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
    {
    }

    /// <summary>
    /// 自定义行绘制指示器
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected virtual void gridView1_CustomDrawRowIndicator(object s, RowIndicatorCustomDrawEventArgs e)
    {
        if (e.Info.IsRowIndicator && e.RowHandle >= 0)
        {
            e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }

    /// <summary>
    /// 定义单元格样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        // GridView gridView = gridView1;
        // if (e.Column.FieldName is "OrderStatus" or "OrderStatus")
        // {
        //     string status = gridView.GetRowCellValue(e.RowHandle, e.Column.FieldName).ObjToStr();
        //     Color color = Color.White;
        //     if (status is "已审核" or "Y" || status.ToLower() == "true")
        //     {
        //         e.Appearance.BackColor = Color.Red;
        //         e.Appearance.BackColor2 = Color.LightCyan;
        //     }
        // }
    }

    /// <summary>
    /// 自定义列的显示文本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
    {
        // string columnName = e.Column.FieldName;
        // else if (columnName == "DictType_ID")
        // {
        //     e.DisplayText = BLLFactory<DictData>.Instance.GetFieldValue(string.Concat(e.Value), "Name");
        // }
    }

    /// <summary>
    /// 自定义列按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void repositoryBtn_ButtonClick(object sender, ButtonPressedEventArgs e)
    {
    }
}