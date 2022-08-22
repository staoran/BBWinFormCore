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
using BB.Tools.Extension;
using BB.Tools.Format;
using BB.Entity.TMS;
using BB.Tools.Entity;
using DevExpress.XtraBars;
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 线路管理
/// </summary>
public partial class FrmSegment : BaseViewDock<Segment, SegmentHttpService, FrmEditSegment, Segments, SegmentsHttpService>
{
    public FrmSegment(SegmentHttpService bll, SegmentsHttpService childBll, FrmEditSegment baseForm) : base(bll, childBll, baseForm)
    {
        moduleName = "线路管理";
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
    private void FrmSegment_Shown(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(Segment.FieldSegmentType, "线路类型");
            x.AddNode(Segment.FieldFlagApp, "审核", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtSegmentType.BindDictItems("线路类型", "1", true, false);
        txtSegmentBeginNode.BindDictItems(GB.AllOuDict, null, true, false);
        txtSegmentEndNode.BindDictItems(GB.AllOuDict, null, true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "SegmentNo,SegmentType,SegmentName,SegmentBeginNode,SegmentEndNode,PlanBeginTime,ExpectedHour,ExpectedDistance,ExpectedOilWear,ExpectedCharge,ExpectedPontage,Remark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate";

        #region 添加别名解析，两种方式均可

        // winGridViewPager1.AddColumnAlias(Segment.FieldSegmentNo, "线路编号");
        // winGridViewPager1.AddColumnAlias(Segment.FieldSegmentType, "线路类型");
        // winGridViewPager1.AddColumnAlias(Segment.FieldSegmentName, "线路名称");
        // winGridViewPager1.AddColumnAlias(Segment.FieldSegmentBeginNode, "起始网点");
        // winGridViewPager1.AddColumnAlias(Segment.FieldSegmentEndNode, "结束网点");
        // winGridViewPager1.AddColumnAlias(Segment.FieldPlanBeginTime, "起始时间");
        // winGridViewPager1.AddColumnAlias(Segment.FieldExpectedHour, "预估时间（小时）");
        // winGridViewPager1.AddColumnAlias(Segment.FieldExpectedDistance, "预估距离");
        // winGridViewPager1.AddColumnAlias(Segment.FieldExpectedOilWear, "预估油耗");
        // winGridViewPager1.AddColumnAlias(Segment.FieldExpectedCharge, "预估开支");
        // winGridViewPager1.AddColumnAlias(Segment.FieldExpectedPontage, "预估路桥费");
        // winGridViewPager1.AddColumnAlias(Segment.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(Segment.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(Segment.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(Segment.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(Segment.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(Segment.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(Segment.FieldAppUser, "审核人");
        // winGridViewPager1.AddColumnAlias(Segment.FieldAppDate, "审核时间");

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(Segment.FieldSegmentType, "线路类型");
        winGridViewPager1.SetColumnDataSource(Segment.FieldSegmentBeginNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Segment.FieldSegmentEndNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Segment.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Segment.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Segment.FieldAppUser, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行


        #endregion

        #endregion

        #region 明细表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridView2.DisplayColumns = "ISID,CostType,QuotationNo,PayNodeType,PayNodeNo,RecvNodeType,RecvNodeNo,OpenTime,Closetime,Remark,FinancialCenterType,FinancialCenter";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridView2.AddColumnAlias(Segments.FieldISID, "自增ID");
        // winGridView2.AddColumnAlias(Segments.FieldCostType, "费用类型");
        // winGridView2.AddColumnAlias(Segments.FieldQuotationNo, "报价编号");
        // winGridView2.AddColumnAlias(Segments.FieldPayNodeType, "付款网点类型");
        // winGridView2.AddColumnAlias(Segments.FieldPayNodeNo, "付款网点");
        // winGridView2.AddColumnAlias(Segments.FieldRecvNodeType, "收款网点类型");
        // winGridView2.AddColumnAlias(Segments.FieldRecvNodeNo, "收款网点");
        // winGridView2.AddColumnAlias(Segments.FieldOpenTime, "启用时间");
        // winGridView2.AddColumnAlias(Segments.FieldClosetime, "终止时间");
        // winGridView2.AddColumnAlias(Segments.FieldRemark, "备注");
        // winGridView2.AddColumnAlias(Segments.FieldFinancialCenterType, "财务中心类型");
        // winGridView2.AddColumnAlias(Segments.FieldFinancialCenter, "财务中心");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridView2.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridView2.SetColumnDataSource(Segments.FieldCostType, GB.AllCostType);
        winGridView2.SetColumnDataSource(Segments.FieldPayNodeType, "收付部门类型");
        winGridView2.SetColumnDataSource(Segments.FieldRecvNodeType, "收付部门类型");
        winGridView2.SetColumnDataSource(Segments.FieldFinancialCenterType, "收付财务中心类型");

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
            //     gridView.SetGridColumWidth(Segment.FieldSegmentNo, 200);
            //     gridView.SetGridColumWidth(Segment.FieldSegmentType, 200);
            //     gridView.SetGridColumWidth(Segment.FieldSegmentName, 200);
            //     gridView.SetGridColumWidth(Segment.FieldSegmentBeginNode, 200);
            //     gridView.SetGridColumWidth(Segment.FieldSegmentEndNode, 200);
            //     gridView.SetGridColumWidth(Segment.FieldPlanBeginTime, 200);
            //     gridView.SetGridColumWidth(Segment.FieldExpectedHour, 200);
            //     gridView.SetGridColumWidth(Segment.FieldExpectedDistance, 200);
            //     gridView.SetGridColumWidth(Segment.FieldExpectedOilWear, 200);
            //     gridView.SetGridColumWidth(Segment.FieldExpectedCharge, 200);
            //     gridView.SetGridColumWidth(Segment.FieldExpectedPontage, 200);
            //     gridView.SetGridColumWidth(Segment.FieldRemark, 200);
            //     gridView.SetGridColumWidth(Segment.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(Segment.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(Segment.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(Segment.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(Segment.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(Segment.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(Segment.FieldAppDate, 200);
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
            //     gridView.SetGridColumWidth(Segments.FieldISID, 200);
            //     gridView.SetGridColumWidth(Segments.FieldCostType, 200);
            //     gridView.SetGridColumWidth(Segments.FieldQuotationNo, 200);
            //     gridView.SetGridColumWidth(Segments.FieldPayNodeType, 200);
            //     gridView.SetGridColumWidth(Segments.FieldPayNodeNo, 200);
            //     gridView.SetGridColumWidth(Segments.FieldRecvNodeType, 200);
            //     gridView.SetGridColumWidth(Segments.FieldRecvNodeNo, 200);
            //     gridView.SetGridColumWidth(Segments.FieldOpenTime, 200);
            //     gridView.SetGridColumWidth(Segments.FieldClosetime, 200);
            //     gridView.SetGridColumWidth(Segments.FieldRemark, 200);
            //     gridView.SetGridColumWidth(Segments.FieldFinancialCenterType, 200);
            //     gridView.SetGridColumWidth(Segments.FieldFinancialCenter, 200);
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
            { Segment.FieldSegmentNo, txtSegmentNo.Text.Trim() },
            { Segment.FieldSegmentType, txtSegmentType.GetComboBoxValue() },
            { Segment.FieldSegmentName, txtSegmentName.Text.Trim() },
            { Segment.FieldSegmentBeginNode, txtSegmentBeginNode.GetComboBoxValue() },
            { Segment.FieldSegmentEndNode, txtSegmentEndNode.GetComboBoxValue() },
            { Segment.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { Segment.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
            { Segment.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
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
        var info = new Segment
        {
            SegmentNo = GetRowData(dr, "线路编号"),
            SegmentType = GetRowData(dr, "线路类型"),
            SegmentName = GetRowData(dr, "线路名称"),
            SegmentBeginNode = GetRowData(dr, "起始网点"),
            SegmentEndNode = GetRowData(dr, "结束网点"),
            PlanBeginTime = GetRowData(dr, "起始时间").ToDateTime(dtNow),
            ExpectedHour = GetRowData(dr, "预估时间（小时）").ToDecimal(),
            ExpectedDistance = GetRowData(dr, "预估距离").ToDecimal(),
            ExpectedOilWear = GetRowData(dr, "预估油耗").ToDecimal(),
            ExpectedCharge = GetRowData(dr, "预估开支").ToDecimal(),
            ExpectedPontage = GetRowData(dr, "预估路桥费").ToDecimal(),
            Remark = GetRowData(dr, "备注"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
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
        CListItem[] condition = GetQueryCondition();
        List<Segment> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "线路编号,线路类型,线路名称,起始网点,结束网点,起始时间,预估时间（小时）,预估距离,预估油耗,预估开支,预估路桥费,备注,创建时间,创建人,修改时间,修改人,审核,审核人,审核时间");
        var j = 1;
        foreach (Segment t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["线路编号"] = t.SegmentNo;
            dr["线路类型"] = t.SegmentType;
            dr["线路名称"] = t.SegmentName;
            dr["起始网点"] = t.SegmentBeginNode;
            dr["结束网点"] = t.SegmentEndNode;
            dr["起始时间"] = t.PlanBeginTime;
            dr["预估时间（小时）"] = t.ExpectedHour;
            dr["预估距离"] = t.ExpectedDistance;
            dr["预估油耗"] = t.ExpectedOilWear;
            dr["预估开支"] = t.ExpectedCharge;
            dr["预估路桥费"] = t.ExpectedPontage;
            dr["备注"] = t.Remark;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["修改时间"] = t.LastUpdateDate;
            dr["修改人"] = t.LastUpdatedBy;
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
            AdvDlg?.AddColumnListItem(Segment.FieldSegmentType, "线路类型");
            AdvDlg?.AddColumnListItem(Segment.FieldSegmentBeginNode, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(Segment.FieldSegmentEndNode, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(Segment.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Segment.FieldLastUpdatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Segment.FieldAppUser, GB.AllUserDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}