using System.Data;
using System.Reflection;
using BB.BaseUI.Extension;
using BB.Entity.Base;
using BB.HttpServices.Base;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using FluentValidation;

namespace BB.BaseUI.BaseUI;

/// <summary>
/// 主从编辑界面基类
/// </summary>
public partial class BaseEditForm<T, IT, T1, IT1> : BaseEditForm<T, IT>
    where T : BaseEntity<T1>, new()
    where IT : BaseHttpService<T>
    where T1 : BaseEntity, new()
    where IT1 : BaseHttpService<T1>
{
    /// <summary>
    /// 是否检查子表为空
    /// </summary>
    private readonly bool _isCheckChildNull;

    /// <summary>
    /// 子表业务逻辑
    /// </summary>
    protected readonly IT1 ChildBll;

    private readonly IValidator<T1> _childValidator;

    /// <summary>
    /// 无参构造函数,默认不检查子表是否为空
    /// </summary>
    protected BaseEditForm(IT bll, IT1 childBll, IValidator<T> validator, IValidator<T1> childValidator) : base(bll, validator)
    {
        InitializeComponent();

        _isCheckChildNull = typeof(T).GetCustomAttribute<IsChildListNullAttribute>().IsNotNull();
        ChildBll = childBll;
        _childValidator = childValidator;

        Shown += BaseEditForm_Shown;
    }

    private void BaseEditForm_Shown(object sender, EventArgs e)
    {
        #region 一些其他设置方法的案例，包括单元格中按钮弹出选择框

        /*
        gridview.Columns.ColumnByFieldName("ID").Visible = false;//设置不可见
        gridview.Columns.ColumnByFieldName("Pallet").CreateCheckEdit();//创建复选框控件
        gridview.Columns.ColumnByFieldName("TradeMode").CreateLookUpEdit().BindDictItems("贸易方向");//创建列表并绑定字典
        gridview.Columns.ColumnByFieldName("OrganizationCode").CreateTextEdit();//文本控件
        gridview.CreateColumn("Remark", "备注", 300, true).CreateMemoEdit();//设置备件内容

        //设置按钮可选择机构
        var deptControl = gridview.Columns.ColumnByFieldName("OuName").CreateButtonEdit(ButtonPredefines.Search);
        deptControl.ButtonClick += (object sender, ButtonPressedEventArgs e) =>
        {
            if (gridview.GetFocusedRow() == null)
            {
                gridview.AddNewRow();//一定要增加
            }

            FrmSelectOU dlg = new FrmSelectOU();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gridview.SetFocusedRowCellValue("OuName", dlg.OuName);
                gridview.SetFocusedRowCellValue("OuID", dlg.OuID);
            }
        };

        //设置可编辑
        gridview.OptionsBehavior.ReadOnly = false;
        gridview.OptionsBehavior.Editable = true;
        */

        #endregion

        #region 网格视图初始化和设置

        // 初始化
        gridView1.InitGridView();

        #endregion

        #region 网格相关事件绑定

        // 绑定列显示值事件
        gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;

        // 绑定行样式事件
        gridView1.RowCellStyle += gridView1_RowCellStyle;

        // 绑定自定义行绘制指示器事件
        gridView1.CustomDrawRowIndicator += gridView1_CustomDrawRowIndicator;

        // 绑定行数据校验事件
        gridView1.ValidateRow += gridView1_ValidateRow;

        // 初始化新行
        gridView1.InitNewRow += gridView1_InitNewRow;

        #endregion
    }
    
    /// <summary>
    /// 数据显示的函数
    /// </summary>
    public override async Task DisplayData()
    {
        await base.DisplayData();

        List<T1> list = new();
        if (!string.IsNullOrEmpty(ID))
        {
            #region 编辑时的显示信息

            var key = TempInfo?.GetPrimaryKeyValue();
            if (key != null)
            {
                list = await ChildBll.FindByForeignKeyAsync(key); //根据外键获取明细列表记录
            }

            #endregion
        }
        else
        {
            // 新增时
        }

        // 统一展示明细数据，没有则绑定空数据源
        // gridControl1.DataSource = new BindingList<TranCustomersInfo>(list);
        // 改为绑定DataTable，用来跟踪增删改
        gridControl1.DataSource = list.ToDataTable();

        // 初始化明细表
        await InitDetailGrid();
    }

    /// <summary>
    /// 初始化明细表的GridView数据显示
    /// </summary>
    protected virtual async Task InitDetailGrid()
    {
        // 获取子表字段权限的列表
        Dictionary<string, int> permitDict = await ChildBll.GetPermitDictAsync();

        // 调整可新增不可编辑的字段
        if (permitDict.ContainsValue(4))
        {
            permitDict.ForEach(x=>
            {
                if (x.Value==4)
                {
                    permitDict[x.Key] = ID.IsNullOrEmpty() ? 0 : 3;
                }
            });
        }

        gridView1.SetColumnsPermit(permitDict);
    }

    /// <summary>
    /// 新增状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveAddNew()
    {
        //必须使用存在的局部变量，因为部分信息可能被附件使用
        if (TempInfo == null) return false;
        // SetInfo(_tempInfo);

        #region 新增数据

        var dt = gridControl1.DataSource as DataTable;
        TempInfo.ChildTableList = dt.ToList<T1>();

        bool succeed = await Bll.InsertAsync(TempInfo);
        if (succeed)
        {
            //可添加其他关联操作

            return true;
        }

        #endregion

        return false;
    }

    /// <summary>
    /// 编辑状态下的数据保存
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> SaveUpdated()
    {
        if (TempInfo == null) return false;
        // SetInfo(_tempInfo);

        #region 更新数据

        var dt = gridControl1.DataSource as DataTable;
        TempInfo.ChildTableList = dt.ToList<T1>();

        bool succeed = await Bll.UpdateAsync(TempInfo);
        if (succeed)
        {
            //可添加其他关联操作

            return true;
        }

        #endregion

        return false;
    }

    /// <summary>
    /// 实现控件输入检查的函数
    /// </summary>
    /// <returns></returns>
    public override async Task<bool> CheckInput()
    {
        var allowEmpty = true;
        if (_isCheckChildNull && gridView1.RowCount == 0)
        {
            
            "明细不可为空".ShowErrorTip(this);
            allowEmpty = false;
        }

        return await base.CheckInput() && allowEmpty;
    }
    
        #region GridView事件

    /// <summary>
    /// 初始化新行
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected virtual async void gridView1_InitNewRow(object s, InitNewRowEventArgs e)
    {
        if (s is not ColumnView view) throw new Exception("行数据获取异常");
        
        T1 entity = await ChildBll.NewEntityAsync(); // 数据在此初始化
        
        string foreignKey = entity.GetField("ForeignKey").ObjToStr(); // 外键字段名称
        if (!ID.IsNullOrEmpty())
        {
            entity.SetProperty(foreignKey, ID); //明细表的外键
        }
        // if (!foreignKey.IsNullOrEmpty() && _tempInfo != null)
        // {
        //     object? primaryValue = _tempInfo.GetProperty(foreignKey); // 从主表中拿到对应的值
        //     entity.SetProperty(foreignKey, primaryValue); //明细表的外键
        // }
        view.EntityToRow(e.RowHandle, entity);
        // 此处加入新增列的数据初始化
        // gridView1.SetRowCellValue(e.RowHandle, "ISID", Guid.NewGuid().ToString()); //明细表ID
        //gridView1.SetRowCellValue(e.RowHandle, "Apply_ID", tempInfo.Apply_ID);
        //gridView1.SetRowCellValue(e.RowHandle, "OccurTime", DateTime.Now);
        // 此处加入新增列的数据初始化
    }

    /// <summary>
    /// 行数据校验
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual async void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
    {
        if (sender is not ColumnView view) throw new Exception("行数据获取异常");

        view.ClearColumnErrors();
        var entity = view.RowToModel<T1>(e.RowHandle);
        view.ProcessValidationResults(e, await _childValidator.ValidateAsync(entity));
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
        if (e.Button.Kind == ButtonPredefines.Delete)
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }
    }

    #endregion
}