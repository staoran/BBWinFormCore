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
/// 普通报价
/// </summary>
public partial class FrmQuotation : BaseViewDock<Quotation, QuotationHttpService, FrmEditQuotation, Quotations, QuotationsHttpService>
{
    public FrmQuotation(QuotationHttpService bll, QuotationsHttpService childBll, FrmEditQuotation baseForm) : base(bll, childBll, baseForm)
    {
        moduleName = "普通报价";
        
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
    private void FrmQuotation_Shown(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(Quotation.FieldCostType, "费用类型", GB.AllCostType);
            x.AddNode(Quotation.FieldCargoTypePerYN, "支持货物类型加成", GB.YesOrNo);
            x.AddNode(Quotation.FieldFlagApp, "审批", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtCostType.BindDictItems(GB.AllCostType, null, true, false);
        txtCargoTypePerYN.BindDictItems(GB.YesOrNo, null, true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "QuotationNo,QuotationDesc,CostType,CargoTypePerYN,Remark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate";

        #region 添加别名解析，两种方式均可

        // winGridViewPager1.AddColumnAlias(Quotation.FieldQuotationNo, "报价编号");
        // winGridViewPager1.AddColumnAlias(Quotation.FieldQuotationDesc, "报价名称");
        // winGridViewPager1.AddColumnAlias(Quotation.FieldCostType, "费用类型");
        // winGridViewPager1.AddColumnAlias(Quotation.FieldCargoTypePerYN, "支持加成");
        // winGridViewPager1.AddColumnAlias(Quotation.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(Quotation.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(Quotation.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(Quotation.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(Quotation.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(Quotation.FieldFlagApp, "审批");
        // winGridViewPager1.AddColumnAlias(Quotation.FieldAppUser, "审批人");
        // winGridViewPager1.AddColumnAlias(Quotation.FieldAppDate, "审批时间");

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(Quotation.FieldCostType, GB.AllCostType);
        winGridViewPager1.SetColumnDataSource(Quotation.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Quotation.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Quotation.FieldAppUser, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行


        #endregion

        #endregion

        #region 明细表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridView2.DisplayColumns = "ISID,QuotationType,FromGroups,ToGroups,MinCost,MaxCost,FirstCost,FirstValue,MinValue,MaxValue,UnitPrice,UnitPricePer,Remark";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridView2.AddColumnAlias(Quotations.FieldISID, "自增ID");
        // winGridView2.AddColumnAlias(Quotations.FieldQuotationType, "报价类型");
        // winGridView2.AddColumnAlias(Quotations.FieldFromGroups, "起始组");
        // winGridView2.AddColumnAlias(Quotations.FieldToGroups, "到达组");
        // winGridView2.AddColumnAlias(Quotations.FieldMinCost, "最小金额");
        // winGridView2.AddColumnAlias(Quotations.FieldMaxCost, "最大金额");
        // winGridView2.AddColumnAlias(Quotations.FieldFirstCost, "首值金额");
        // winGridView2.AddColumnAlias(Quotations.FieldFirstValue, "首值");
        // winGridView2.AddColumnAlias(Quotations.FieldMinValue, "最小值");
        // winGridView2.AddColumnAlias(Quotations.FieldMaxValue, "最大值");
        // winGridView2.AddColumnAlias(Quotations.FieldUnitPrice, "单价");
        // winGridView2.AddColumnAlias(Quotations.FieldUnitPricePer, "单价加成");
        // winGridView2.AddColumnAlias(Quotations.FieldRemark, "备注");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridView2.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridView2.SetColumnDataSource(Quotations.FieldQuotationType, "计价方式");

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
    protected override void gridView1_DataSourceChanged(object sender, EventArgs e)
    {
        base.gridView1_DataSourceChanged(sender, e);
        if (winGridViewPager1.gridView1.Columns.Count > 0 && winGridViewPager1.gridView1.RowCount > 0)
        {
            #region 单独设置列宽

            // 可特殊设置特别的宽度
            // GridView gridView = winGridViewPager1.gridView1;
            // if (gridView != null)
            // {
            //     gridView.SetGridColumWidth(Quotation.FieldQuotationNo, 200);
            //     gridView.SetGridColumWidth(Quotation.FieldQuotationDesc, 200);
            //     gridView.SetGridColumWidth(Quotation.FieldCostType, 200);
            //     gridView.SetGridColumWidth(Quotation.FieldCargoTypePerYN, 200);
            //     gridView.SetGridColumWidth(Quotation.FieldRemark, 200);
            //     gridView.SetGridColumWidth(Quotation.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(Quotation.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(Quotation.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(Quotation.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(Quotation.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(Quotation.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(Quotation.FieldAppDate, 200);
            // }

            #endregion
        }
    }

    #endregion

    #region 从表明细列表

    /// <summary>
    /// 明细表数据源变更时
    /// </summary>
    protected override void gridView2_DataSourceChanged(object sender, EventArgs e)
    {
        base.gridView2_DataSourceChanged(sender, e);
        // 绑定数据后，分配各列的宽度
        if (winGridView2.gridView1.Columns.Count > 0 && winGridView2.gridView1.RowCount > 0)
        {
            // 可特殊设置特别的宽度
            // GridView gridView = winGridView2.gridView1;
            // if (gridView != null)
            // {
            //     gridView.SetGridColumWidth(Quotations.FieldISID, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldQuotationType, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldFromGroups, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldToGroups, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldMinCost, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldMaxCost, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldFirstCost, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldFirstValue, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldMinValue, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldMaxValue, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldUnitPrice, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldUnitPricePer, 200);
            //     gridView.SetGridColumWidth(Quotations.FieldRemark, 200);
            // }
        }
    }

    #endregion

    #endregion

    #region 快捷查询条件

    /// <summary>
    /// 根据查询条件构造查询条件对象
    /// </summary>
    protected override CListItem[] GetQueryCondition()
    {
        // 如果存在高级查询对象信息，则使用高级查询条件，否则使用主表条件查询
        return (_treeCondition ?? _advanceCondition ?? new NameValueCollection
        {
            { Quotation.FieldQuotationNo, txtQuotationNo.Text.Trim() },
            { Quotation.FieldQuotationDesc, txtQuotationDesc.Text.Trim() },
            { Quotation.FieldCostType, txtCostType.GetComboBoxValue() },
            { Quotation.FieldCargoTypePerYN, txtCargoTypePerYN.GetComboBoxValue() },
            { Quotation.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
        }).ToCListItems();
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
        var info = new Quotation
        {
            QuotationNo = GetRowData(dr, "报价编号"),
            QuotationDesc = GetRowData(dr, "报价名称"),
            CostType = GetRowData(dr, "费用类型"),
            CargoTypePerYN = GetRowData(dr, "支持加成") == "是",
            Remark = GetRowData(dr, "备注"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
            FlagApp = GetRowData(dr, "审批") == "是",
            AppUser = GetRowData(dr, "审批人"),
            AppDate = GetRowData(dr, "审批时间").ToDateTime(dtNow),
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
        CListItem[] condition = GetQueryCondition();
        List<Quotation> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "报价编号,报价名称,费用类型,支持加成,备注,创建时间,创建人,修改时间,修改人,审批,审批人,审批时间");
        var j = 1;
        foreach (Quotation t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["报价编号"] = t.QuotationNo;
            dr["报价名称"] = t.QuotationDesc;
            dr["费用类型"] = t.CostType;
            dr["支持加成"] = t.CargoTypePerYN ? "是" : "否";
            dr["备注"] = t.Remark;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["修改时间"] = t.LastUpdateDate;
            dr["修改人"] = t.LastUpdatedBy;
            dr["审批"] = t.FlagApp ? "是" : "否";
            dr["审批人"] = t.AppUser;
            dr["审批时间"] = t.AppDate;
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
        AdvDlg?.AddColumnListItem(Quotation.FieldCostType, GB.AllCostType);
        AdvDlg?.AddColumnListItem(Quotation.FieldCreatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(Quotation.FieldLastUpdatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(Quotation.FieldAppUser, GB.AllUserDict);

        #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}