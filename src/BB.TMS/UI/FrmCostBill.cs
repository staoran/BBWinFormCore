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
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 预付金管理
/// </summary>
#if DESIGNER
public partial class FrmCostBill : BaseViewDesigner
#else
public partial class FrmCostBill : BaseViewDock<CostBill, CostBillHttpService, FrmEditCostBill>
#endif
{
    public FrmCostBill(CostBillHttpService bll, LazilyResolved<FrmEditCostBill> baseForm) : base(bll, baseForm)
    {
        moduleName = "预付金管理";

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
    private void FrmCostBill_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(CostBill.FieldCostBillType, "费用单据类型");
            x.AddNode(CostBill.FieldCostType, "预付金操作类型", GB.AllCostBillType);
            x.AddNode(CostBill.FieldPostYN, "入账", GB.YesOrNo);
            x.AddNode(CostBill.FieldStatusID, "账单状态");
            x.AddNode(CostBill.FieldFlagApp, "已审核", "未审核");
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtCostBillType.BindDictItems("费用单据类型", "1", true, false);
        txtTranNodeNO.BindDictItems(GB.AllOuDict, null, true, false);
        txtTranNodeNOPay.BindDictItems(GB.AllOuDict, null, true, false);
        txtCostType.BindDictItems(GB.AllCostBillType, null, true, false);
        txtPostYN.BindDictItems(GB.YesOrNo, null, true, false);
        txtStatusID.BindDictItems("账单状态", "0", true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "CostBillType,CostBillNo,WaybillNo,TranNodeNO,TranNodeNOPay,SourceNo,CostType,Ctrl,Cost,PostYN,PostDate,PostBy,StatusID,Remark,CreationDate,CreatedBy,CreatedByNode,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate,FinancialCenter";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(CostBill.FieldCostBillType, "预付单类型");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldCostBillNo, "预付单编号");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldWaybillNo, "运单编号");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldTranNodeNO, "收款网点");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldTranNodeNOPay, "付款网点");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldSourceNo, "来源单号");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldCostType, "操作类型");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldCtrl, "正负");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldCost, "金额");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldPostYN, "入账");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldPostDate, "入账时间");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldPostBy, "入账人");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldStatusID, "单据状态");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldCreatedByNode, "创建网点");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldFlagApp, "审批");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldAppUser, "审批人");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldAppDate, "审批时间");
        // winGridViewPager1.AddColumnAlias(CostBill.FieldFinancialCenter, "财务中心");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(CostBill.FieldCostBillType, "费用单据类型");
        winGridViewPager1.SetColumnDataSource(CostBill.FieldTranNodeNO, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(CostBill.FieldTranNodeNOPay, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(CostBill.FieldCostType, GB.AllCostBillType);
        winGridViewPager1.SetColumnDataSource(CostBill.FieldPostBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(CostBill.FieldStatusID, "账单状态");
        winGridViewPager1.SetColumnDataSource(CostBill.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(CostBill.FieldCreatedByNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(CostBill.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(CostBill.FieldAppUser, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(CostBill.FieldFinancialCenter, GB.AllOuDict);

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
            //     gridView.SetGridColumWidth(CostBill.FieldCostBillType, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldCostBillNo, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldWaybillNo, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldTranNodeNO, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldTranNodeNOPay, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldSourceNo, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldCostType, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldCtrl, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldCost, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldPostYN, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldPostDate, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldPostBy, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldStatusID, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldRemark, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldCreatedByNode, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldAppDate, 200);
            //     gridView.SetGridColumWidth(CostBill.FieldFinancialCenter, 200);
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
            { CostBill.FieldCostBillType, txtCostBillType.GetComboBoxValue() },
            { CostBill.FieldCostBillNo, txtCostBillNo.Text.Trim() },
            { CostBill.FieldWaybillNo, txtWaybillNo.Text.Trim() },
            { CostBill.FieldTranNodeNO, txtTranNodeNO.GetComboBoxValue() },
            { CostBill.FieldTranNodeNOPay, txtTranNodeNOPay.GetComboBoxValue() },
            { CostBill.FieldSourceNo, txtSourceNo.Text.Trim() },
            { CostBill.FieldCostType, txtCostType.GetComboBoxValue() },
            { CostBill.FieldPostYN, txtPostYN.GetComboBoxValue() },
            { CostBill.FieldPostDate, txtPostDate1.EditValue.ObjToStr() },
            { CostBill.FieldPostDate, txtPostDate2.EditValue.ObjToStr() },
            { CostBill.FieldStatusID, txtStatusID.GetComboBoxValue() },
            { CostBill.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { CostBill.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
            { CostBill.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
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
        var info = new CostBill
        {
            CostBillType = GetRowData(dr, "预付单类型"),
            CostBillNo = GetRowData(dr, "预付单编号"),
            WaybillNo = GetRowData(dr, "运单编号"),
            TranNodeNO = GetRowData(dr, "收款网点"),
            TranNodeNOPay = GetRowData(dr, "付款网点"),
            SourceNo = GetRowData(dr, "来源单号"),
            CostType = GetRowData(dr, "操作类型"),
            Ctrl = GetRowData(dr, "正负").ObjToShort(),
            Cost = GetRowData(dr, "金额").ToDecimal(),
            PostYN = GetRowData(dr, "入账") == "是",
            PostDate = GetRowData(dr, "入账时间").ToDateTime(dtNow),
            PostBy = GetRowData(dr, "入账人"),
            StatusID = GetRowData(dr, "单据状态"),
            Remark = GetRowData(dr, "备注"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            CreatedByNode = GetRowData(dr, "创建网点"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
            FlagApp = GetRowData(dr, "审批").ObjToBool(),
            AppUser = GetRowData(dr, "审批人"),
            AppDate = GetRowData(dr, "审批时间").ToDateTime(dtNow),
            FinancialCenter = GetRowData(dr, "财务中心"),
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
        List<CostBill> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "预付单类型,预付单编号,运单编号,收款网点,付款网点,来源单号,操作类型,正负,金额,入账,入账时间,入账人,单据状态,备注,创建时间,创建人,创建网点,修改时间,修改人,审批,审批人,审批时间,财务中心");
        var j = 1;
        foreach (CostBill t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["预付单类型"] = t.CostBillType;
            dr["预付单编号"] = t.CostBillNo;
            dr["运单编号"] = t.WaybillNo;
            dr["收款网点"] = t.TranNodeNO;
            dr["付款网点"] = t.TranNodeNOPay;
            dr["来源单号"] = t.SourceNo;
            dr["操作类型"] = t.CostType;
            dr["正负"] = t.Ctrl;
            dr["金额"] = t.Cost;
            dr["入账"] = t.PostYN ? "是" : "否";
            dr["入账时间"] = t.PostDate;
            dr["入账人"] = t.PostBy;
            dr["单据状态"] = t.StatusID;
            dr["备注"] = t.Remark;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["创建网点"] = t.CreatedByNode;
            dr["修改时间"] = t.LastUpdateDate;
            dr["修改人"] = t.LastUpdatedBy;
            dr["审批"] = t.FlagApp;
            dr["审批人"] = t.AppUser;
            dr["审批时间"] = t.AppDate;
            dr["财务中心"] = t.FinancialCenter;
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
            AdvDlg?.AddColumnListItem(CostBill.FieldCostBillType, "费用单据类型");
            AdvDlg?.AddColumnListItem(CostBill.FieldTranNodeNO, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(CostBill.FieldTranNodeNOPay, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(CostBill.FieldCostType, GB.AllCostBillType);
            AdvDlg?.AddColumnListItem(CostBill.FieldPostBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(CostBill.FieldStatusID, "账单状态");
            AdvDlg?.AddColumnListItem(CostBill.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(CostBill.FieldCreatedByNode, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(CostBill.FieldLastUpdatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(CostBill.FieldAppUser, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(CostBill.FieldFinancialCenter, GB.AllOuDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}