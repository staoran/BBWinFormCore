using BB.BaseUI.Print;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace BB.BaseUI.BaseUI;

public partial class BaseEditDesigner : XtraForm
{
    public BaseEditDesigner()
    {
        InitializeComponent();
    }

    public BaseEditDesigner(bool isDesignMode) : this()
    {
    }

    public BaseEditDesigner(object bll, object baseForm) : this()
    {
    }

    /// <summary>
    /// 错误管理器
    /// </summary>
    protected DXErrorProvider ErrorProvider;

    /// <summary>
    /// 记录主键
    /// </summary>
    public string ID { get; set; }

    /// <summary>
    /// 所有待展示的ID列表
    /// </summary>
    public List<string> IdList { get; set; }

    /// <summary>
    /// 子窗体数据保存的触发
    /// </summary>
    public event EventHandler OnDataSaved;

    private void BaseEditDesigner_Load(object sender, EventArgs e)
    {
    }
    private async void btnAdd_Click(object? sender, EventArgs e)
    {
    }

    private async void btnOK_Click(object? sender, EventArgs e)
    {
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void picPrint_Click(object? sender, EventArgs e)
    {
        PrintFormHelper.Print(this);
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
    protected virtual async Task SetPermit()
    {
    }

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    protected virtual Task InitDictItem()
    {
        return Task.CompletedTask;
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

    /// <summary>
    /// 显示数据到控件上
    /// </summary>
    public virtual Task DisplayData()
    {
        throw new NotSupportedException("无法直接调用基类方法，请在派生类中实现该方法");
    }

    /// <summary>
    /// 检查输入的有效性
    /// </summary>
    /// <returns>有效</returns>
    public virtual async Task<bool> CheckInput()
    {
        return await Task.FromResult(true);
    }

    /// <summary>
    /// 清除屏幕
    /// </summary>
    public virtual async Task ClearScreen()
    {
    }

    /// <summary>
    /// 保存数据（新增和编辑的保存）
    /// </summary>
    public virtual async Task<bool> SaveEntity()
    {
        bool result;
        if (!string.IsNullOrEmpty(ID))
        {
            //编辑的保存
            result = await SaveUpdated();
        }
        else
        {
            //新增的保存
            result = await SaveAddNew();
        }

        return result;
    }

    /// <summary>
    /// 更新已有的数据
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> SaveUpdated()
    {
        return await Task.FromResult(true);
    }

    /// <summary>
    /// 保存新增的数据
    /// </summary>
    /// <returns></returns>
    public virtual Task<bool> SaveAddNew()
    {
        return Task.FromResult(true);
    }
}