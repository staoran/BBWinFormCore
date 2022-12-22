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
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 运单线路表
/// </summary>
#if DESIGNER
public partial class FrmWaybillSegment : BaseViewDesigner
#else
public partial class FrmWaybillSegment : BaseViewDock<WaybillSegment, WaybillSegmentHttpService, FrmEditWaybillSegment>
#endif
{
    public FrmWaybillSegment(WaybillSegmentHttpService bll, LazilyResolved<FrmEditWaybillSegment> baseForm) : base(bll, baseForm)
    {
        moduleName = "运单线路表";

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


        #endregion
    }

    /// <summary>
    /// 窗体首次打开后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmWaybillSegment_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(WaybillSegment.FieldSegmentType, "线路选择类型");
            x.AddNode(WaybillSegment.FieldSegmentBeginYN, "起始登记", GB.YesOrNo);
            x.AddNode(WaybillSegment.FieldSegmentEndYN, "到达登记", GB.YesOrNo);
            x.AddNode(WaybillSegment.FieldStatusId, "运单线路状态");
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtSegmentType.BindDictItems("线路选择类型", null, true, false);
        txtSegmentBeginNode.BindDictItems(GB.AllOuDict, null, true, false);
        txtSegmentBeginYN.BindDictItems(GB.YesOrNo, null, true, false);
        txtSegmentEndYN.BindDictItems(GB.YesOrNo, null, true, false);
        txtStatusId.BindDictItems("运单线路状态", null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "ISID,WaybillNo,SegmentNo,CarmarkNo,SegmentType,SegmentName,SegmentBeginNode,SegmentEndNode,ExpectedTime,ExpectedHour,ExpectedDistance,ExpectedOilWear,ExpectedCharge,ExpectedPontage,SegmentBeginYN,SegmentBeginUser,SegmentBeginDate,SegmentBeginRemark,SegmentEndYN,SegmentEndUser,SegmentEndDate,SegmentEndRemark,StatusId,Remark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldISID, "自增ID");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldWaybillNo, "运单编号");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentNo, "线路编号");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldCarmarkNo, "车标号");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentType, "线路类型");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentName, "线路名称");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentBeginNode, "线路起点");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentEndNode, "线路终点");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldExpectedTime, "预估发车时间");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldExpectedHour, "预估时间");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldExpectedDistance, "预估距离");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldExpectedOilWear, "预估油耗");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldExpectedCharge, "预估成本");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldExpectedPontage, "预估路桥费");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentBeginYN, "起始登记");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentBeginUser, "起始登记人");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentBeginDate, "起始登记时间");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentBeginRemark, "起始登记备注");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentEndYN, "到达登记");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentEndUser, "到达登记人");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentEndDate, "到达登记时间");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldSegmentEndRemark, "到达登记备注");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldStatusId, "状态");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(WaybillSegment.FieldLastUpdatedBy, "修改人");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(WaybillSegment.FieldSegmentType, "线路选择类型");
        winGridViewPager1.SetColumnDataSource(WaybillSegment.FieldSegmentBeginNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(WaybillSegment.FieldSegmentEndNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(WaybillSegment.FieldSegmentBeginUser, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(WaybillSegment.FieldSegmentEndUser, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(WaybillSegment.FieldStatusId, "运单线路状态");
        winGridViewPager1.SetColumnDataSource(WaybillSegment.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(WaybillSegment.FieldLastUpdatedBy, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行


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
            //     gridView.SetGridColumWidth(WaybillSegment.FieldISID, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldWaybillNo, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentNo, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldCarmarkNo, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentType, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentName, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentBeginNode, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentEndNode, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldExpectedTime, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldExpectedHour, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldExpectedDistance, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldExpectedOilWear, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldExpectedCharge, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldExpectedPontage, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentBeginYN, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentBeginUser, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentBeginDate, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentBeginRemark, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentEndYN, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentEndUser, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentEndDate, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldSegmentEndRemark, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldStatusId, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldRemark, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(WaybillSegment.FieldLastUpdatedBy, 200);
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
        return (_treeCondition ?? _advanceCondition ?? new NameValueCollection
        {
            { WaybillSegment.FieldWaybillNo, txtWaybillNo.Text.Trim() },
            { WaybillSegment.FieldSegmentNo, txtSegmentNo.Text.Trim() },
            { WaybillSegment.FieldCarmarkNo, txtCarmarkNo.Text.Trim() },
            { WaybillSegment.FieldSegmentType, txtSegmentType.GetComboBoxValue() },
            { WaybillSegment.FieldSegmentName, txtSegmentName.Text.Trim() },
            { WaybillSegment.FieldSegmentBeginNode, txtSegmentBeginNode.GetComboBoxValue() },
            { WaybillSegment.FieldSegmentBeginYN, txtSegmentBeginYN.GetComboBoxValue() },
            { WaybillSegment.FieldSegmentEndYN, txtSegmentEndYN.GetComboBoxValue() },
            { WaybillSegment.FieldStatusId, txtStatusId.GetComboBoxValue() },
            { WaybillSegment.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { WaybillSegment.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
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
        var info = new WaybillSegment
        {
            ISID = GetRowData(dr, "自增ID").ObjToInt(),
            WaybillNo = GetRowData(dr, "运单编号"),
            SegmentNo = GetRowData(dr, "线路编号"),
            CarmarkNo = GetRowData(dr, "车标号"),
            SegmentType = GetRowData(dr, "线路类型"),
            SegmentName = GetRowData(dr, "线路名称"),
            SegmentBeginNode = GetRowData(dr, "线路起点"),
            SegmentEndNode = GetRowData(dr, "线路终点"),
            ExpectedTime = GetRowData(dr, "预估发车时间").ToDateTime(dtNow),
            ExpectedHour = GetRowData(dr, "预估时间").ObjToInt(),
            ExpectedDistance = GetRowData(dr, "预估距离").ToDecimal(),
            ExpectedOilWear = GetRowData(dr, "预估油耗").ToDecimal(),
            ExpectedCharge = GetRowData(dr, "预估成本").ToDecimal(),
            ExpectedPontage = GetRowData(dr, "预估路桥费").ToDecimal(),
            SegmentBeginYN = GetRowData(dr, "起始登记") == "是",
            SegmentBeginUser = GetRowData(dr, "起始登记人"),
            SegmentBeginDate = GetRowData(dr, "起始登记时间").ToDateTime(dtNow),
            SegmentBeginRemark = GetRowData(dr, "起始登记备注"),
            SegmentEndYN = GetRowData(dr, "到达登记") == "是",
            SegmentEndUser = GetRowData(dr, "到达登记人"),
            SegmentEndDate = GetRowData(dr, "到达登记时间").ToDateTime(dtNow),
            SegmentEndRemark = GetRowData(dr, "到达登记备注"),
            StatusId = GetRowData(dr, "状态"),
            Remark = GetRowData(dr, "备注"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
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
        List<WaybillSegment> list = await _bll.FindAsync(condition);

        DataTable dtNew = DataTableHelper.CreateTable(
            "自增ID,运单编号,线路编号,车标号,线路类型,线路名称,线路起点,线路终点,预估发车时间,预估时间,预估距离,预估油耗,预估成本,预估路桥费,起始登记,起始登记人,起始登记时间,起始登记备注,到达登记,到达登记人,到达登记时间,到达登记备注,状态,备注,创建时间,创建人,修改时间,修改人");
        var j = 1;
        foreach (WaybillSegment t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["自增ID"] = t.ISID;
            dr["运单编号"] = t.WaybillNo;
            dr["线路编号"] = t.SegmentNo;
            dr["车标号"] = t.CarmarkNo;
            dr["线路类型"] = t.SegmentType;
            dr["线路名称"] = t.SegmentName;
            dr["线路起点"] = t.SegmentBeginNode;
            dr["线路终点"] = t.SegmentEndNode;
            dr["预估发车时间"] = t.ExpectedTime;
            dr["预估时间"] = t.ExpectedHour;
            dr["预估距离"] = t.ExpectedDistance;
            dr["预估油耗"] = t.ExpectedOilWear;
            dr["预估成本"] = t.ExpectedCharge;
            dr["预估路桥费"] = t.ExpectedPontage;
            dr["起始登记"] = t.SegmentBeginYN ? "是" : "否";
            dr["起始登记人"] = t.SegmentBeginUser;
            dr["起始登记时间"] = t.SegmentBeginDate;
            dr["起始登记备注"] = t.SegmentBeginRemark;
            dr["到达登记"] = t.SegmentEndYN ? "是" : "否";
            dr["到达登记人"] = t.SegmentEndUser;
            dr["到达登记时间"] = t.SegmentEndDate;
            dr["到达登记备注"] = t.SegmentEndRemark;
            dr["状态"] = t.StatusId;
            dr["备注"] = t.Remark;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["修改时间"] = t.LastUpdateDate;
            dr["修改人"] = t.LastUpdatedBy;
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
            ex.ToString().LogError();;
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
        AdvDlg?.AddColumnListItem(WaybillSegment.FieldSegmentType, "线路选择类型");
        AdvDlg?.AddColumnListItem(WaybillSegment.FieldSegmentBeginNode, GB.AllOuDict);
        AdvDlg?.AddColumnListItem(WaybillSegment.FieldSegmentEndNode, GB.AllOuDict);
        AdvDlg?.AddColumnListItem(WaybillSegment.FieldSegmentBeginUser, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(WaybillSegment.FieldSegmentEndUser, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(WaybillSegment.FieldStatusId, "运单线路状态");
        AdvDlg?.AddColumnListItem(WaybillSegment.FieldCreatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(WaybillSegment.FieldLastUpdatedBy, GB.AllUserDict);

        #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}