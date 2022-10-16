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
/// 区域分组
/// </summary>
public partial class FrmBasicGroupList : BaseViewDock<BasicGroupList, BasicGroupListHttpService, FrmEditBasicGroupList>
{
    public FrmBasicGroupList(BasicGroupListHttpService bll, LazilyResolved<FrmEditBasicGroupList> baseForm) : base(bll, baseForm)
    {
        moduleName = "区域分组";

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
    private void FrmBasicGroupList_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(BasicGroupList.FieldGroupType, "分组类型");
            x.AddNode(BasicGroupList.FieldCostType, "费用类型", GB.AllCostType);
            x.AddNode(BasicGroupList.FieldFlagApp, "审批", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtGroupType.BindDictItems("分组类型", "", true, false);
        txtCostType.BindDictItems(GB.AllCostType, null, true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "ISID,GroupName,GroupType,CostType,GroupContent,GroupExceptContent,Remark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldISID, "自增ID");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldGroupName, "分组名称");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldGroupType, "分组类型");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldCostType, "费用类型");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldGroupContent, "分组区域");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldGroupExceptContent, "排除区域");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldFlagApp, "审批");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldAppUser, "审批人");
        // winGridViewPager1.AddColumnAlias(BasicGroupList.FieldAppDate, "审批时间");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(BasicGroupList.FieldGroupType, "分组类型");
        winGridViewPager1.SetColumnDataSource(BasicGroupList.FieldCostType, GB.AllCostType);
        winGridViewPager1.SetColumnDataSource(BasicGroupList.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(BasicGroupList.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(BasicGroupList.FieldAppUser, GB.AllUserDict);

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
            //     gridView.SetGridColumWidth(BasicGroupList.FieldISID, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldGroupName, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldGroupType, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldCostType, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldGroupContent, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldGroupExceptContent, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldRemark, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(BasicGroupList.FieldAppDate, 200);
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
            { BasicGroupList.FieldGroupName, txtGroupName.Text.Trim() },
            { BasicGroupList.FieldGroupType, txtGroupType.GetComboBoxValue() },
            { BasicGroupList.FieldCostType, txtCostType.GetComboBoxValue() },
            { BasicGroupList.FieldGroupContent, txtGroupContent.Text.Trim() },
            { BasicGroupList.FieldGroupExceptContent, txtGroupExceptContent.Text.Trim() },
            { BasicGroupList.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
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
        var info = new BasicGroupList
        {
            ISID = GetRowData(dr, "自增ID").ObjToInt(),
            GroupName = GetRowData(dr, "分组名称"),
            GroupType = GetRowData(dr, "分组类型"),
            CostType = GetRowData(dr, "费用类型"),
            GroupContent = GetRowData(dr, "分组区域"),
            GroupExceptContent = GetRowData(dr, "排除区域"),
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
        Dictionary<string,string> condition = GetQueryCondition();
        List<BasicGroupList> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "自增ID,分组名称,分组类型,费用类型,分组区域,排除区域,备注,创建时间,创建人,修改时间,修改人,审批,审批人,审批时间");
        var j = 1;
        foreach (BasicGroupList t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["自增ID"] = t.ISID;
            dr["分组名称"] = t.GroupName;
            dr["分组类型"] = t.GroupType;
            dr["费用类型"] = t.CostType;
            dr["分组区域"] = t.GroupContent;
            dr["排除区域"] = t.GroupExceptContent;
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
            AdvDlg?.AddColumnListItem(BasicGroupList.FieldGroupType, "分组类型");
            AdvDlg?.AddColumnListItem(BasicGroupList.FieldCostType, GB.AllCostType);
            AdvDlg?.AddColumnListItem(BasicGroupList.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(BasicGroupList.FieldLastUpdatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(BasicGroupList.FieldAppUser, GB.AllUserDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}