using System.Data;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Diagnostics;
using BB.BaseUI.BaseUI;
using BB.BaseUI.DocViewer;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.HttpServices.TMS;
using BB.Tools.Format;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using DevExpress.XtraBars;
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 公式报价
/// </summary>
// public partial class FrmBasicQuotation : BaseViewDesigner
public partial class FrmBasicQuotation : BaseViewDock<BasicQuotation, BasicQuotationHttpService, FrmEditBasicQuotation, BasicQuotations, BasicQuotationsHttpService>
{
    public FrmBasicQuotation(BasicQuotationHttpService bll, BasicQuotationsHttpService childBll, LazilyResolved<FrmEditBasicQuotation> baseForm) : base(bll, childBll, baseForm)
    {
        moduleName = "公式报价";

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
    private void FrmBasicQuotation_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(BasicQuotation.FieldCostType, "费用类型", GB.AllCostType);
            x.AddNode(BasicQuotation.FieldCargoType, "货物名称");
            x.AddNode(BasicQuotation.FieldPickUpType, "收货类型");
            x.AddNode(BasicQuotation.FieldDeliveryType, "交货方式");
            x.AddNode(BasicQuotation.FieldTransportType, "运输类型");
            x.AddNode(BasicQuotation.FieldFlagApp, "审批", GB.YesOrNo);
            x.AddNode(BasicQuotation.FieldRakeMarkYN, "仅用于抽成", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtCostType.BindDictItems(GB.AllCostType, null, true, false);
        txtCargoType.BindDictItems("货物名称", "", true, false);
        txtPickUpType.BindDictItems("收货类型", "", true, false);
        txtDeliveryType.BindDictItems("交货方式", "", true, false);
        txtTransportType.BindDictItems("运输类型", "", true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);
        txtRakeMarkYN.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "QuotationNo,QuotationDesc,TranNodeNO,CostType,CargoType,PickUpType,DeliveryType,TransportType,Froms,Tos,BeginTime,EndTime,Remark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate,RakeMarkYN";

        #region 添加别名解析，两种方式均可

        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldQuotationNo, "报价编号");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldQuotationDesc, "报价名称");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldTranNodeNO, "所属网点");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldCostType, "费用类型");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldCargoType, "货物类型");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldPickUpType, "收货方式");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldDeliveryType, "交货方式");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldTransportType, "运输方式");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldFroms, "起始区域");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldTos, "目的区域");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldBeginTime, "生效时间");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldEndTime, "过期时间");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldFlagApp, "审批");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldAppUser, "审批人");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldAppDate, "审批时间");
        // winGridViewPager1.AddColumnAlias(BasicQuotation.FieldRakeMarkYN, "仅用于抽成");

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(BasicQuotation.FieldTranNodeNO, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(BasicQuotation.FieldCostType, GB.AllCostType);
        winGridViewPager1.SetColumnDataSource(BasicQuotation.FieldCargoType, "货物名称");
        winGridViewPager1.SetColumnDataSource(BasicQuotation.FieldPickUpType, "收货类型");
        winGridViewPager1.SetColumnDataSource(BasicQuotation.FieldDeliveryType, "交货方式");
        winGridViewPager1.SetColumnDataSource(BasicQuotation.FieldTransportType, "运输类型");
        winGridViewPager1.SetColumnDataSource(BasicQuotation.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(BasicQuotation.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(BasicQuotation.FieldAppUser, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行


        #endregion

        #endregion

        #region 明细表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridView2.DisplayColumns = "ISID,MathConditional,MathContent,Remark";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridView2.AddColumnAlias(BasicQuotations.FieldISID, "自增ID");
        // winGridView2.AddColumnAlias(BasicQuotations.FieldMathConditional, "条件范围");
        // winGridView2.AddColumnAlias(BasicQuotations.FieldMathContent, "条件公式");
        // winGridView2.AddColumnAlias(BasicQuotations.FieldRemark, "备注");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridView2.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源


        #endregion

        #region 添加列字段汇总行


        #endregion

        #endregion

        #endregion
    }

    #region 网格列表信息

    #region 主表列表信息

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
            //     gridView.SetGridColumWidth(BasicQuotation.FieldQuotationNo, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldQuotationDesc, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldTranNodeNO, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldCostType, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldCargoType, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldPickUpType, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldDeliveryType, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldTransportType, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldFroms, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldTos, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldBeginTime, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldEndTime, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldRemark, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldAppDate, 200);
            //     gridView.SetGridColumWidth(BasicQuotation.FieldRakeMarkYN, 200);
            // }

            #endregion
        }
    }

    #endregion

    #region 从表明细列表

    /// <summary>
    /// 明细表数据源变更时
    /// </summary>
    protected override void gridView2_DataSourceChanged(object? sender, EventArgs e)
    {
        base.gridView2_DataSourceChanged(sender, e);
        // 绑定数据后，分配各列的宽度
        if (winGridView2.gridView1.Columns.Count > 0 && winGridView2.gridView1.RowCount > 0)
        {
            // 可特殊设置特别的宽度
            // GridView gridView = winGridView2.gridView1;
            // if (gridView != null)
            // {
            //     gridView.SetGridColumWidth(BasicQuotations.FieldISID, 200);
            //     gridView.SetGridColumWidth(BasicQuotations.FieldMathConditional, 200);
            //     gridView.SetGridColumWidth(BasicQuotations.FieldMathContent, 200);
            //     gridView.SetGridColumWidth(BasicQuotations.FieldRemark, 200);
            // }
        }
    }

    #endregion

    #endregion

    #region 快捷查询条件

    /// <summary>
    /// 根据查询条件构造查询条件对象
    /// </summary>
    protected override Dictionary<string,string> GetQueryCondition()
    {
        // 如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
        return (_treeCondition ?? _advanceCondition ?? new NameValueCollection
        {
            { BasicQuotation.FieldQuotationNo, txtQuotationNo.Text.Trim() },
            { BasicQuotation.FieldQuotationDesc, txtQuotationDesc.Text.Trim() },
            { BasicQuotation.FieldCostType, txtCostType.GetComboBoxValue() },
            { BasicQuotation.FieldCargoType, txtCargoType.GetComboBoxValue() },
            { BasicQuotation.FieldPickUpType, txtPickUpType.GetComboBoxValue() },
            { BasicQuotation.FieldDeliveryType, txtDeliveryType.GetComboBoxValue() },
            { BasicQuotation.FieldTransportType, txtTransportType.GetComboBoxValue() },
            { BasicQuotation.FieldFroms, txtFroms.Text.Trim() },
            { BasicQuotation.FieldTos, txtTos.Text.Trim() },
            { BasicQuotation.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
            { BasicQuotation.FieldRakeMarkYN, txtRakeMarkYN.GetComboBoxValue() },
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
        var info = new BasicQuotation
        {
            QuotationNo = GetRowData(dr, "报价编号"),
            QuotationDesc = GetRowData(dr, "报价名称"),
            TranNodeNO = GetRowData(dr, "所属网点"),
            CostType = GetRowData(dr, "费用类型"),
            CargoType = GetRowData(dr, "货物类型"),
            PickUpType = GetRowData(dr, "收货方式"),
            DeliveryType = GetRowData(dr, "交货方式"),
            TransportType = GetRowData(dr, "运输方式"),
            Froms = GetRowData(dr, "起始区域"),
            Tos = GetRowData(dr, "目的区域"),
            BeginTime = GetRowData(dr, "生效时间").ToDateTime(dtNow),
            EndTime = GetRowData(dr, "过期时间").ToDateTime(dtNow),
            Remark = GetRowData(dr, "备注"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
            FlagApp = GetRowData(dr, "审批") == "是",
            AppUser = GetRowData(dr, "审批人"),
            AppDate = GetRowData(dr, "审批时间").ToDateTime(dtNow),
            RakeMarkYN = GetRowData(dr, "仅用于抽成") == "是",
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
        List<BasicQuotation> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "报价编号,报价名称,所属网点,费用类型,货物类型,收货方式,交货方式,运输方式,起始区域,目的区域,生效时间,过期时间,备注,创建时间,创建人,修改时间,修改人,审批,审批人,审批时间,仅用于抽成");
        var j = 1;
        foreach (BasicQuotation t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["报价编号"] = t.QuotationNo;
            dr["报价名称"] = t.QuotationDesc;
            dr["所属网点"] = t.TranNodeNO;
            dr["费用类型"] = t.CostType;
            dr["货物类型"] = t.CargoType;
            dr["收货方式"] = t.PickUpType;
            dr["交货方式"] = t.DeliveryType;
            dr["运输方式"] = t.TransportType;
            dr["起始区域"] = t.Froms;
            dr["目的区域"] = t.Tos;
            dr["生效时间"] = t.BeginTime;
            dr["过期时间"] = t.EndTime;
            dr["备注"] = t.Remark;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["修改时间"] = t.LastUpdateDate;
            dr["修改人"] = t.LastUpdatedBy;
            dr["审批"] = t.FlagApp ? "是" : "否";
            dr["审批人"] = t.AppUser;
            dr["审批时间"] = t.AppDate;
            dr["仅用于抽成"] = t.RakeMarkYN ? "是" : "否";
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
            AdvDlg?.AddColumnListItem(BasicQuotation.FieldTranNodeNO, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(BasicQuotation.FieldCostType, GB.AllCostType);
            AdvDlg?.AddColumnListItem(BasicQuotation.FieldCargoType, "货物名称");
            AdvDlg?.AddColumnListItem(BasicQuotation.FieldPickUpType, "收货类型");
            AdvDlg?.AddColumnListItem(BasicQuotation.FieldDeliveryType, "交货方式");
            AdvDlg?.AddColumnListItem(BasicQuotation.FieldTransportType, "运输类型");
            AdvDlg?.AddColumnListItem(BasicQuotation.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(BasicQuotation.FieldLastUpdatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(BasicQuotation.FieldAppUser, GB.AllUserDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}