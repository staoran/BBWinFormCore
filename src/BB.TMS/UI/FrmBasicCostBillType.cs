using System.Data;
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
using System.Windows.Forms;

namespace BB.TMS.UI;

/// <summary>
/// 预付金操作类型
/// </summary>
#if DESIGNER
public partial class FrmBasicCostBillType : BaseViewDesigner
#else
public partial class FrmBasicCostBillType : BaseViewDock<BasicCostBillType, BasicCostBillTypeHttpService, FrmEditBasicCostBillType>
#endif
{
    public FrmBasicCostBillType(BasicCostBillTypeHttpService bll, LazilyResolved<FrmEditBasicCostBillType> baseForm) : base(bll, baseForm)
    {
        moduleName = "预付金操作类型";

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
    private void FrmBasicCostBillType_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(BasicCostBillType.FieldFlagApp, "审核", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "ISID,CostType,CostDesc,Ctrl,Remark,UseType,CreatedBy,CreationDate,FlagApp,AppUser,AppDate,LastUpdatedBy,LastUpdateDate";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldISID, "自增ID");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldCostType, "类型编号");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldCostDesc, "类型名称");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldCtrl, "正负1/-1");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldUseType, "适用范围");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldAppUser, "审批人");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldAppDate, "审批时间");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(BasicCostBillType.FieldLastUpdateDate, "修改时间");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(BasicCostBillType.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(BasicCostBillType.FieldAppUser, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(BasicCostBillType.FieldLastUpdatedBy, GB.AllUserDict);

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
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldISID, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldCostType, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldCostDesc, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldCtrl, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldRemark, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldUseType, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldAppDate, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(BasicCostBillType.FieldLastUpdateDate, 200);
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
            { BasicCostBillType.FieldCostType, txtCostType.Text.Trim() },
            { BasicCostBillType.FieldCostDesc, txtCostDesc.Text.Trim() },
            { BasicCostBillType.FieldUseType, txtUseType.GetComboBoxValue() },
            { BasicCostBillType.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
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
        var info = new BasicCostBillType
        {
            ISID = GetRowData(dr, "自增ID").ObjToInt(),
            CostType = GetRowData(dr, "类型编号"),
            CostDesc = GetRowData(dr, "类型名称"),
            Ctrl = GetRowData(dr, "正负1/-1").ObjToShort(),
            Remark = GetRowData(dr, "备注"),
            UseType = GetRowData(dr, "适用范围"),
            CreatedBy = GetRowData(dr, "创建人"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            FlagApp = GetRowData(dr, "审核") == "是",
            AppUser = GetRowData(dr, "审批人"),
            AppDate = GetRowData(dr, "审批时间").ToDateTime(dtNow),
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
        List<BasicCostBillType> list = await _bll.FindAsync(condition);

        DataTable dtNew = DataTableHelper.CreateTable(
            "自增ID,类型编号,类型名称,正负1/-1,备注,适用范围,创建人,创建时间,审核,审批人,审批时间,修改人,修改时间");
        var j = 1;
        foreach (BasicCostBillType t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["自增ID"] = t.ISID;
            dr["类型编号"] = t.CostType;
            dr["类型名称"] = t.CostDesc;
            dr["正负1/-1"] = t.Ctrl;
            dr["备注"] = t.Remark;
            dr["适用范围"] = t.UseType;
            dr["创建人"] = t.CreatedBy;
            dr["创建时间"] = t.CreationDate;
            dr["审核"] = t.FlagApp ? "是" : "否";
            dr["审批人"] = t.AppUser;
            dr["审批时间"] = t.AppDate;
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
        AdvDlg?.AddColumnListItem(BasicCostBillType.FieldCreatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(BasicCostBillType.FieldAppUser, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(BasicCostBillType.FieldLastUpdatedBy, GB.AllUserDict);

        #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}