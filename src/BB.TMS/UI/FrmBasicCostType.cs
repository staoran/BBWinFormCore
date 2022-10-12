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
using DevExpress.XtraBars;
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 费用类型
/// </summary>
public partial class FrmBasicCostType : BaseViewDock<BasicCostType, BasicCostTypeHttpService, FrmEditBasicCostType>
{
    public FrmBasicCostType(BasicCostTypeHttpService bll, LazilyResolved<FrmEditBasicCostType> baseForm) : base(bll, baseForm)
    {
        moduleName = "费用类型";

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
    private void FrmBasicCostType_Shown(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(BasicCostType.FieldUseYN, "启用", GB.YesOrNo);
            x.AddNode(BasicCostType.FieldPayNodeType, "收付部门类型");
            x.AddNode(BasicCostType.FieldRecvNodeType, "收付部门类型");
            x.AddNode(BasicCostType.FieldPayPostType, "入账时机");
            x.AddNode(BasicCostType.FieldRecvPostType, "入账时机");
            x.AddNode(BasicCostType.FieldFlagApp, "审核", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtUseYN.BindDictItems(GB.YesOrNo, null, true, false);
        txtPayNodeType.BindDictItems("收付部门类型", null, true, false);
        txtRecvNodeType.BindDictItems("收付部门类型", null, true, false);
        txtPayPostType.BindDictItems("入账时机", null, true, false);
        txtRecvPostType.BindDictItems("入账时机", null, true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "CostType,CostTypeDesc,Ctrl,UseYN,UseType,PayNodeType,RecvNodeType,PayPostType,RecvPostType,CostYN,Remark,FlagApp,AppUser,AppDate,CreatedBy,CreationDate,LastUpdatedBy,LastUpdateDate";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldCostType, "费用类型编号");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldCostTypeDesc, "费用类型");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldCtrl, "正负");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldUseYN, "启用");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldUseType, "适用范围");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldPayNodeType, "付款网点类型");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldRecvNodeType, "收款网点类型");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldPayPostType, "付款入账类型");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldRecvPostType, "收款入账类型");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldCostYN, "收入费用");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldAppUser, "审核人");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldAppDate, "审核时间");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(BasicCostType.FieldLastUpdateDate, "修改时间");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(BasicCostType.FieldPayNodeType, "收付部门类型");
        winGridViewPager1.SetColumnDataSource(BasicCostType.FieldRecvNodeType, "收付部门类型");
        winGridViewPager1.SetColumnDataSource(BasicCostType.FieldPayPostType, "入账时机");
        winGridViewPager1.SetColumnDataSource(BasicCostType.FieldRecvPostType, "入账时机");
        winGridViewPager1.SetColumnDataSource(BasicCostType.FieldAppUser, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(BasicCostType.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(BasicCostType.FieldLastUpdatedBy, GB.AllUserDict);

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
            //     gridView.SetGridColumWidth(BasicCostType.FieldCostType, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldCostTypeDesc, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldCtrl, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldUseYN, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldUseType, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldPayNodeType, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldRecvNodeType, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldPayPostType, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldRecvPostType, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldCostYN, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldRemark, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldAppDate, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(BasicCostType.FieldLastUpdateDate, 200);
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
            { BasicCostType.FieldCostType, txtCostType.Text.Trim() },
            { BasicCostType.FieldCostTypeDesc, txtCostTypeDesc.Text.Trim() },
            { BasicCostType.FieldUseYN, txtUseYN.GetComboBoxValue() },
            { BasicCostType.FieldPayNodeType, txtPayNodeType.GetComboBoxValue() },
            { BasicCostType.FieldRecvNodeType, txtRecvNodeType.GetComboBoxValue() },
            { BasicCostType.FieldPayPostType, txtPayPostType.GetComboBoxValue() },
            { BasicCostType.FieldRecvPostType, txtRecvPostType.GetComboBoxValue() },
            { BasicCostType.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
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
        var info = new BasicCostType
        {
            CostType = GetRowData(dr, "费用类型编号"),
            CostTypeDesc = GetRowData(dr, "费用类型"),
            Ctrl = GetRowData(dr, "正负").ObjToShort(),
            UseYN = GetRowData(dr, "启用") == "是",
            UseType = GetRowData(dr, "适用范围"),
            PayNodeType = GetRowData(dr, "付款网点类型"),
            RecvNodeType = GetRowData(dr, "收款网点类型"),
            PayPostType = GetRowData(dr, "付款入账类型"),
            RecvPostType = GetRowData(dr, "收款入账类型"),
            CostYN = GetRowData(dr, "收入费用") == "是",
            Remark = GetRowData(dr, "备注"),
            FlagApp = GetRowData(dr, "审核") == "是",
            AppUser = GetRowData(dr, "审核人"),
            AppDate = GetRowData(dr, "审核时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
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
        List<BasicCostType> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "费用类型编号,费用类型,正负,启用,适用范围,付款网点类型,收款网点类型,付款入账类型,收款入账类型,收入费用,备注,审核,审核人,审核时间,创建人,创建时间,修改人,修改时间");
        var j = 1;
        foreach (BasicCostType t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["费用类型编号"] = t.CostType;
            dr["费用类型"] = t.CostTypeDesc;
            dr["正负"] = t.Ctrl;
            dr["启用"] = t.UseYN ? "是" : "否";
            dr["适用范围"] = t.UseType;
            dr["付款网点类型"] = t.PayNodeType;
            dr["收款网点类型"] = t.RecvNodeType;
            dr["付款入账类型"] = t.PayPostType;
            dr["收款入账类型"] = t.RecvPostType;
            dr["收入费用"] = t.CostYN ? "是" : "否";
            dr["备注"] = t.Remark;
            dr["审核"] = t.FlagApp ? "是" : "否";
            dr["审核人"] = t.AppUser;
            dr["审核时间"] = t.AppDate;
            dr["创建人"] = t.CreatedBy;
            dr["创建时间"] = t.CreationDate;
            dr["修改人"] = t.LastUpdatedBy;
            dr["修改时间"] = t.LastUpdateDate;
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
            AdvDlg?.AddColumnListItem(BasicCostType.FieldPayNodeType, "收付部门类型");
            AdvDlg?.AddColumnListItem(BasicCostType.FieldRecvNodeType, "收付部门类型");
            AdvDlg?.AddColumnListItem(BasicCostType.FieldPayPostType, "入账时机");
            AdvDlg?.AddColumnListItem(BasicCostType.FieldRecvPostType, "入账时机");
            AdvDlg?.AddColumnListItem(BasicCostType.FieldAppUser, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(BasicCostType.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(BasicCostType.FieldLastUpdatedBy, GB.AllUserDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}