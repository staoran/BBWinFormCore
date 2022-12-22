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
/// 运单操作记录
/// </summary>
#if DESIGNER
public partial class FrmWaybillRecords : BaseViewDesigner
#else
public partial class FrmWaybillRecords : BaseViewDock<WaybillRecords, WaybillRecordsHttpService, FrmEditWaybillRecords>
#endif
{
    public FrmWaybillRecords(WaybillRecordsHttpService bll, LazilyResolved<FrmEditWaybillRecords> baseForm) : base(bll, baseForm)
    {
        moduleName = "运单操作记录";

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
    private void FrmWaybillRecords_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(WaybillRecords.FieldStatusID, "运单状态");
            x.AddNode(WaybillRecords.FieldCancelYN, "作废", GB.YesOrNo);
            x.AddNode(WaybillRecords.FieldNotPublic, "对外公开", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtTranNode.BindDictItems(GB.AllOuDict, "*当前机构*", true, false);
        txtTranNodePN.BindDictItems(GB.AllOuDict, null, true, false);
        txtStatusID.BindDictItems("运单状态", null, true, false);
        txtCancelYN.BindDictItems(GB.YesOrNo, null, true, false);
        txtNotPublic.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "ISID,WaybillNo,TranNode,TranNodePN,StatusID,RelatedUser,CarMarkNo,SegmentNo,Remark,CancelYN,CancelDate,CancelBy,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,NotPublic";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldISID, "自增ID");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldWaybillNo, "运单编号");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldTranNode, "当前网点");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldTranNodePN, "上一/下一网点");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldStatusID, "当前状态");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldRelatedUser, "相关人员");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldCarMarkNo, "车标号");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldSegmentNo, "配载编号");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldCancelYN, "作废");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldCancelDate, "作废时间");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldCancelBy, "作废人");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(WaybillRecords.FieldNotPublic, "对外公开");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(WaybillRecords.FieldTranNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(WaybillRecords.FieldTranNodePN, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(WaybillRecords.FieldStatusID, "运单状态");
        winGridViewPager1.SetColumnDataSource(WaybillRecords.FieldRelatedUser, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(WaybillRecords.FieldCancelBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(WaybillRecords.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(WaybillRecords.FieldLastUpdatedBy, GB.AllUserDict);

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
            //     gridView.SetGridColumWidth(WaybillRecords.FieldISID, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldWaybillNo, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldTranNode, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldTranNodePN, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldStatusID, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldRelatedUser, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldCarMarkNo, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldSegmentNo, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldRemark, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldCancelYN, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldCancelDate, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldCancelBy, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(WaybillRecords.FieldNotPublic, 200);
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
            { WaybillRecords.FieldWaybillNo, txtWaybillNo.Text.Trim() },
            { WaybillRecords.FieldTranNode, txtTranNode.GetComboBoxValue() },
            { WaybillRecords.FieldTranNodePN, txtTranNodePN.GetComboBoxValue() },
            { WaybillRecords.FieldStatusID, txtStatusID.GetComboBoxValue() },
            { WaybillRecords.FieldCarMarkNo, txtCarMarkNo.Text.Trim() },
            { WaybillRecords.FieldSegmentNo, txtSegmentNo.Text.Trim() },
            { WaybillRecords.FieldCancelYN, txtCancelYN.GetComboBoxValue() },
            { WaybillRecords.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { WaybillRecords.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
            { WaybillRecords.FieldNotPublic, txtNotPublic.GetComboBoxValue() },
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
        var info = new WaybillRecords
        {
            ISID = GetRowData(dr, "自增ID").ObjToInt(),
            WaybillNo = GetRowData(dr, "运单编号"),
            TranNode = GetRowData(dr, "当前网点"),
            TranNodePN = GetRowData(dr, "上一/下一网点"),
            StatusID = GetRowData(dr, "当前状态"),
            RelatedUser = GetRowData(dr, "相关人员"),
            CarMarkNo = GetRowData(dr, "车标号"),
            SegmentNo = GetRowData(dr, "配载编号"),
            Remark = GetRowData(dr, "备注"),
            CancelYN = GetRowData(dr, "作废") == "是",
            CancelDate = GetRowData(dr, "作废时间").ToDateTime(dtNow),
            CancelBy = GetRowData(dr, "作废人"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
            NotPublic = GetRowData(dr, "对外公开") == "是",
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
        List<WaybillRecords> list = await _bll.FindAsync(condition);

        DataTable dtNew = DataTableHelper.CreateTable(
            "自增ID,运单编号,当前网点,上一/下一网点,当前状态,相关人员,车标号,配载编号,备注,作废,作废时间,作废人,创建时间,创建人,修改时间,修改人,对外公开");
        var j = 1;
        foreach (WaybillRecords t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["自增ID"] = t.ISID;
            dr["运单编号"] = t.WaybillNo;
            dr["当前网点"] = t.TranNode;
            dr["上一/下一网点"] = t.TranNodePN;
            dr["当前状态"] = t.StatusID;
            dr["相关人员"] = t.RelatedUser;
            dr["车标号"] = t.CarMarkNo;
            dr["配载编号"] = t.SegmentNo;
            dr["备注"] = t.Remark;
            dr["作废"] = t.CancelYN ? "是" : "否";
            dr["作废时间"] = t.CancelDate;
            dr["作废人"] = t.CancelBy;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["修改时间"] = t.LastUpdateDate;
            dr["修改人"] = t.LastUpdatedBy;
            dr["对外公开"] = t.NotPublic ? "是" : "否";
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
        AdvDlg?.AddColumnListItem(WaybillRecords.FieldTranNode, GB.AllOuDict);
        AdvDlg?.AddColumnListItem(WaybillRecords.FieldTranNodePN, GB.AllOuDict);
        AdvDlg?.AddColumnListItem(WaybillRecords.FieldStatusID, "运单状态");
        AdvDlg?.AddColumnListItem(WaybillRecords.FieldRelatedUser, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(WaybillRecords.FieldCancelBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(WaybillRecords.FieldCreatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(WaybillRecords.FieldLastUpdatedBy, GB.AllUserDict);

        #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}