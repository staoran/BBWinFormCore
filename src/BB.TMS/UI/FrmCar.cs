using System.Data;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Diagnostics;
using BB.BaseUI.BaseUI;
using BB.BaseUI.DocViewer;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Tools.Format;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using DevExpress.Data;
using DevExpress.XtraBars;
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 车辆档案
/// </summary>
public partial class FrmCar : BaseViewDock<Car, CarHttpService, FrmEditCar>
{
    public FrmCar(CarHttpService bll, LazilyResolved<FrmEditCar> baseForm) : base(bll, baseForm)
    {
        moduleName = "车辆档案";
        InitializeComponent();
    }

    /// <summary>
    /// 编写初始化窗体的实现
    /// </summary>
    public override async Task FormOnLoad()
    {
        await base.FormOnLoad();

        await InitDictItem();

        #region 查询表单

        txtLong1.KeyDown += SpinEdit_KeyDown;
        txtLong2.KeyDown += SpinEdit_KeyDown;

        txtWidth1.KeyDown += SpinEdit_KeyDown;
        txtWidth2.KeyDown += SpinEdit_KeyDown;

        txtHeight1.KeyDown += SpinEdit_KeyDown;
        txtHeight2.KeyDown += SpinEdit_KeyDown;

        txtVolume1.KeyDown += SpinEdit_KeyDown;
        txtVolume2.KeyDown += SpinEdit_KeyDown;


        #endregion

        #region 按钮和按钮权限

        addButton.Visibility = BarItemVisibility.Always;
        editButton.Visibility = BarItemVisibility.Always;
        checkButton.Visibility = BarItemVisibility.Always;
        importButton.Visibility = BarItemVisibility.Always;
        queryButton.Visibility = BarItemVisibility.Always;
        clearButton.Visibility = BarItemVisibility.Always;
        advQueryButton.Visibility = BarItemVisibility.Always;
        exportButton.Visibility = BarItemVisibility.Always;
        hideTreeButton.Visibility = BarItemVisibility.Always;

        #endregion
    }

    /// <summary>
    /// 窗体首次打开后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmCar_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(Car.FieldProperty, "车辆性质");
            x.AddNode(Car.FieldModel, "车型");
            x.AddNode(Car.FieldCarType, "车体状况");
            x.AddNode(Car.FieldServiceRange, "服务范围");
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtProperty.BindDictItems("车辆性质", "", true, false);
        txtModel.BindDictItems("车型", "", true, false);
        txtCarType.BindDictItems("车体状况", "", true, false);
        txtServiceRange.BindDictItems("服务范围", "", true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "TranNode,CarNo,Property,Model,CarType,ServiceRange,TrustLevel,TargetPlace,BuyDate,OperationNo,EngineNo,VIN,Tonnage,Long,Width,Height,Volume,Contact,ContactTel,Driver,DriverCert,DriverMobile,DriverTel,DriverHomeAddress,Remark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(Car.FieldTranNode, "网点编码");
        // winGridViewPager1.AddColumnAlias(Car.FieldCarNo, "车牌号");
        // winGridViewPager1.AddColumnAlias(Car.FieldProperty, "车辆性质");
        // winGridViewPager1.AddColumnAlias(Car.FieldModel, "车型");
        // winGridViewPager1.AddColumnAlias(Car.FieldCarType, "车体状况");
        // winGridViewPager1.AddColumnAlias(Car.FieldServiceRange, "服务范围");
        // winGridViewPager1.AddColumnAlias(Car.FieldTrustLevel, "信誉");
        // winGridViewPager1.AddColumnAlias(Car.FieldTargetPlace, "主营路线");
        // winGridViewPager1.AddColumnAlias(Car.FieldBuyDate, "购买日期");
        // winGridViewPager1.AddColumnAlias(Car.FieldOperationNo, "运营编号");
        // winGridViewPager1.AddColumnAlias(Car.FieldEngineNo, "发动机号");
        // winGridViewPager1.AddColumnAlias(Car.FieldVIN, "车架号");
        // winGridViewPager1.AddColumnAlias(Car.FieldTonnage, "载重");
        // winGridViewPager1.AddColumnAlias(Car.FieldLong, "长");
        // winGridViewPager1.AddColumnAlias(Car.FieldWidth, "宽");
        // winGridViewPager1.AddColumnAlias(Car.FieldHeight, "高");
        // winGridViewPager1.AddColumnAlias(Car.FieldVolume, "体积");
        // winGridViewPager1.AddColumnAlias(Car.FieldContact, "联系人");
        // winGridViewPager1.AddColumnAlias(Car.FieldContactTel, "联系电话");
        // winGridViewPager1.AddColumnAlias(Car.FieldDriver, "驾驶员");
        // winGridViewPager1.AddColumnAlias(Car.FieldDriverCert, "驾驶证号");
        // winGridViewPager1.AddColumnAlias(Car.FieldDriverMobile, "手机号码");
        // winGridViewPager1.AddColumnAlias(Car.FieldDriverTel, "固定电话");
        // winGridViewPager1.AddColumnAlias(Car.FieldDriverHomeAddress, "家庭住址");
        // winGridViewPager1.AddColumnAlias(Car.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(Car.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(Car.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(Car.FieldLastUpdateDate, "最后修改时间");
        // winGridViewPager1.AddColumnAlias(Car.FieldLastUpdatedBy, "最后修改人");
        // winGridViewPager1.AddColumnAlias(Car.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(Car.FieldAppUser, "审核人");
        // winGridViewPager1.AddColumnAlias(Car.FieldAppDate, "审核时间");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(Car.FieldTranNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Car.FieldProperty, "车辆性质");
        winGridViewPager1.SetColumnDataSource(Car.FieldModel, "车型");
        winGridViewPager1.SetColumnDataSource(Car.FieldCarType, "车体状况");
        winGridViewPager1.SetColumnDataSource(Car.FieldServiceRange, "服务范围");
        winGridViewPager1.SetColumnDataSource(Car.FieldTrustLevel, "信誉类型");
        winGridViewPager1.SetColumnDataSource(Car.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Car.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Car.FieldAppUser, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行

        winGridViewPager1.AddColumnSummaryItem(Car.FieldCarNo, SummaryItemType.Count, "{0}");
        winGridViewPager1.ShowFooter = true;

        #endregion

        #endregion

        #endregion
    }

    #region 网格列表信息

    /// <summary>
    /// 数据源变更时，分配各列的宽度
    /// </summary>
    protected override void gridView1_DataSourceChanged(object? sender, EventArgs e)
    {
        base.gridView1_DataSourceChanged(sender, e);
        if (winGridViewPager1.gridView1.Columns.Count > 0 && winGridViewPager1.gridView1.RowCount > 0)
        {
            #region 单独设置列宽

            // 可特殊设置特别的宽度
            // GridView gridView = winGridViewPager1.gridView1;
            // if (gridView != null)
            // {
            //     gridView.SetGridColumWidth(Car.FieldTranNode, 200);
            //     gridView.SetGridColumWidth(Car.FieldCarNo, 200);
            //     gridView.SetGridColumWidth(Car.FieldProperty, 200);
            //     gridView.SetGridColumWidth(Car.FieldModel, 200);
            //     gridView.SetGridColumWidth(Car.FieldCarType, 200);
            //     gridView.SetGridColumWidth(Car.FieldServiceRange, 200);
            //     gridView.SetGridColumWidth(Car.FieldTrustLevel, 200);
            //     gridView.SetGridColumWidth(Car.FieldTargetPlace, 200);
            //     gridView.SetGridColumWidth(Car.FieldBuyDate, 200);
            //     gridView.SetGridColumWidth(Car.FieldOperationNo, 200);
            //     gridView.SetGridColumWidth(Car.FieldEngineNo, 200);
            //     gridView.SetGridColumWidth(Car.FieldVIN, 200);
            //     gridView.SetGridColumWidth(Car.FieldTonnage, 200);
            //     gridView.SetGridColumWidth(Car.FieldLong, 200);
            //     gridView.SetGridColumWidth(Car.FieldWidth, 200);
            //     gridView.SetGridColumWidth(Car.FieldHeight, 200);
            //     gridView.SetGridColumWidth(Car.FieldVolume, 200);
            //     gridView.SetGridColumWidth(Car.FieldContact, 200);
            //     gridView.SetGridColumWidth(Car.FieldContactTel, 200);
            //     gridView.SetGridColumWidth(Car.FieldDriver, 200);
            //     gridView.SetGridColumWidth(Car.FieldDriverCert, 200);
            //     gridView.SetGridColumWidth(Car.FieldDriverMobile, 200);
            //     gridView.SetGridColumWidth(Car.FieldDriverTel, 200);
            //     gridView.SetGridColumWidth(Car.FieldDriverHomeAddress, 200);
            //     gridView.SetGridColumWidth(Car.FieldRemark, 200);
            //     gridView.SetGridColumWidth(Car.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(Car.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(Car.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(Car.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(Car.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(Car.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(Car.FieldAppDate, 200);
            // }

            #endregion
        }
    }

    #endregion

    #region 快捷查询条件

    /// <summary>
    /// 根据查询条件构造查询条件对象
    /// </summary>
    protected override Dictionary<string,string> GetQueryCondition()
    {
        // 如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
        return (_treeCondition ?? _advanceCondition ?? new NameValueCollection()
        {
            { Car.FieldCarNo, txtCarNo.Text.Trim() },
            { Car.FieldProperty, txtProperty.GetComboBoxValue() },
            { Car.FieldModel, txtModel.GetComboBoxValue() },
            { Car.FieldCarType, txtCarType.GetComboBoxValue() },
            { Car.FieldServiceRange, txtServiceRange.GetComboBoxValue() },
            { Car.FieldTargetPlace, txtTargetPlace.Text.Trim() },
            { Car.FieldEngineNo, txtEngineNo.Text.Trim() },
            { Car.FieldVIN, txtVIN.Text.Trim() },
            { Car.FieldTonnage, txtTonnage.Text.Trim() },
            { Car.FieldLong, txtLong1.EditValue.ObjToStr() },
            { Car.FieldLong, txtLong2.EditValue.ObjToStr() },
            { Car.FieldWidth, txtWidth1.EditValue.ObjToStr() },
            { Car.FieldWidth, txtWidth2.EditValue.ObjToStr() },
            { Car.FieldHeight, txtHeight1.EditValue.ObjToStr() },
            { Car.FieldHeight, txtHeight2.EditValue.ObjToStr() },
            { Car.FieldVolume, txtVolume1.EditValue.ObjToStr() },
            { Car.FieldVolume, txtVolume2.EditValue.ObjToStr() },
            { Car.FieldContact, txtContact.Text.Trim() },
            { Car.FieldContactTel, txtContactTel.Text.Trim() },
            { Car.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { Car.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
        }).ToDicString();
    }

    #endregion

    #region 导入导出

    /// <summary>
    /// 导入数据保存事件
    /// </summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    protected override async Task<bool> ExcelData_OnDataSave(DataRow dr)
    {
        DateTime dtNow = DateTime.Now;
        var info = new Car
        {
            TranNode = GetRowData(dr, "网点编码"),
            CarNo = GetRowData(dr, "车牌号"),
            Property = GetRowData(dr, "车辆性质"),
            Model = GetRowData(dr, "车型"),
            CarType = GetRowData(dr, "车体状况"),
            ServiceRange = GetRowData(dr, "服务范围"),
            TrustLevel = GetRowData(dr, "信誉"),
            TargetPlace = GetRowData(dr, "主营路线"),
            BuyDate = GetRowData(dr, "购买日期").ToDateTime(dtNow),
            OperationNo = GetRowData(dr, "运营编号"),
            EngineNo = GetRowData(dr, "发动机号"),
            VIN = GetRowData(dr, "车架号"),
            Tonnage = GetRowData(dr, "载重"),
            Long = GetRowData(dr, "长").ToDecimal(),
            Width = GetRowData(dr, "宽").ToDecimal(),
            Height = GetRowData(dr, "高").ToDecimal(),
            Volume = GetRowData(dr, "体积").ToDecimal(),
            Contact = GetRowData(dr, "联系人"),
            ContactTel = GetRowData(dr, "联系电话"),
            Driver = GetRowData(dr, "驾驶员"),
            DriverCert = GetRowData(dr, "驾驶证号"),
            DriverMobile = GetRowData(dr, "手机号码"),
            DriverTel = GetRowData(dr, "固定电话"),
            DriverHomeAddress = GetRowData(dr, "家庭住址"),
            Remark = GetRowData(dr, "备注"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "最后修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "最后修改人"),
            FlagApp = GetRowData(dr, "审核") == "是",
            AppUser = GetRowData(dr, "审核人"),
            AppDate = GetRowData(dr, "审核时间").ToDateTime(dtNow),
        };

        return await _bll.InsertAsync(info);
    }

    /// <summary>
    /// 导出的操作
    /// </summary>
    protected override async Task ExportData()
    {
        string file = FileDialogHelper.SaveExcel($"{moduleName}.xls");
                if (string.IsNullOrEmpty(file)) return;
        Dictionary<string,string> condition = GetQueryCondition();
        List<Car> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "网点编码,车牌号,车辆性质,车型,车体状况,服务范围,信誉,主营路线,购买日期,运营编号,发动机号,车架号,载重,长,宽,高,体积,联系人,联系电话,驾驶员,驾驶证号,手机号码,固定电话,家庭住址,备注,创建时间,创建人,最后修改时间,最后修改人,审核,审核人,审核时间");
        var j = 1;
        foreach (Car t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["网点编码"] = t.TranNode;
            dr["车牌号"] = t.CarNo;
            dr["车辆性质"] = t.Property;
            dr["车型"] = t.Model;
            dr["车体状况"] = t.CarType;
            dr["服务范围"] = t.ServiceRange;
            dr["信誉"] = t.TrustLevel;
            dr["主营路线"] = t.TargetPlace;
            dr["购买日期"] = t.BuyDate;
            dr["运营编号"] = t.OperationNo;
            dr["发动机号"] = t.EngineNo;
            dr["车架号"] = t.VIN;
            dr["载重"] = t.Tonnage;
            dr["长"] = t.Long;
            dr["宽"] = t.Width;
            dr["高"] = t.Height;
            dr["体积"] = t.Volume;
            dr["联系人"] = t.Contact;
            dr["联系电话"] = t.ContactTel;
            dr["驾驶员"] = t.Driver;
            dr["驾驶证号"] = t.DriverCert;
            dr["手机号码"] = t.DriverMobile;
            dr["固定电话"] = t.DriverTel;
            dr["家庭住址"] = t.DriverHomeAddress;
            dr["备注"] = t.Remark;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["最后修改时间"] = t.LastUpdateDate;
            dr["最后修改人"] = t.LastUpdatedBy;
            dr["审核"] = t.FlagApp ? "是" : "否";
            dr["审核人"] = t.AppUser;
            dr["审核时间"] = t.AppDate;
            dtNew.Rows.Add(dr);
        }

        try
        {
            AsposeExcelTools.DataTableToExcel2(dtNew, file, out string error);
            if (!string.IsNullOrEmpty(error))
            {
                $"导出Excel出现错误：{error}".ShowUxError();
            }
            else
            {
                if ("导出成功，是否打开文件？".ShowYesNoAndUxTips() == DialogResult.Yes)
                {
                    Process.Start(file);
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString().LogError();
            ex.Message.ShowUxError();
        }
    }

    #endregion

    #region 高级查询

    /// <summary>
    /// 高级查询的操作
    /// </summary>
    protected override async Task AdvanceSearch()
    {
            await base.AdvanceSearch();

            #region 下拉列表数据

            // _advDlg.AddColumnListItem("UserType", Portal.gc.GetDictData("人员类型"));// 字典列表
            // _advDlg.AddColumnListItem("Sex", "男,女");// 固定列表
            // _advDlg.AddColumnListItem("Credit", _bll.GetFieldList("Credit"));// 动态列表
            AdvDlg?.AddColumnListItem(Car.FieldTranNode, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(Car.FieldProperty, "车辆性质");
            AdvDlg?.AddColumnListItem(Car.FieldModel, "车型");
            AdvDlg?.AddColumnListItem(Car.FieldCarType, "车体状况");
            AdvDlg?.AddColumnListItem(Car.FieldServiceRange, "服务范围");
            AdvDlg?.AddColumnListItem(Car.FieldTrustLevel, "信誉类型");
            AdvDlg?.AddColumnListItem(Car.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Car.FieldLastUpdatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Car.FieldAppUser, GB.AllUserDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}