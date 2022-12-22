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
/// 回单操作记录
/// </summary>
#if DESIGNER
public partial class FrmWaybillAckRecs : BaseViewDesigner
#else
public partial class FrmWaybillAckRecs : BaseViewDock<WaybillAckRecs, WaybillAckRecsHttpService, FrmEditWaybillAckRecs>
#endif
{
    public FrmWaybillAckRecs(WaybillAckRecsHttpService bll, LazilyResolved<FrmEditWaybillAckRecs> baseForm) : base(bll, baseForm)
    {
        moduleName = "回单操作记录";

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
    private void FrmWaybillAckRecs_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(WaybillAckRecs.FieldStatusID, "运单状态");
            x.AddNode(WaybillAckRecs.FieldCancelYN, "作废", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtTranNode.BindDictItems(GB.AllOuDict, "*当前用户*", true, false);
        txtStatusID.BindDictItems("运单状态", null, true, false);
        txtCancelYN.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "ISID,AckRecNo,TranNode,TranNodePN,StatusID,RelatedUser,CarMarkNo,Remark,CancelYN,CancelDate,CancelBy,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldISID, "自增ID");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldAckRecNo, "回单编号");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldTranNode, "当前网点");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldTranNodePN, "上一/下一网点");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldStatusID, "当前状态");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldRelatedUser, "相关人员");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldCarMarkNo, "车标号");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldCancelYN, "作废");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldCancelDate, "作废时间");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldCancelBy, "作废人");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(WaybillAckRecs.FieldLastUpdatedBy, "修改人");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(WaybillAckRecs.FieldTranNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(WaybillAckRecs.FieldStatusID, "运单状态");
        winGridViewPager1.SetColumnDataSource(WaybillAckRecs.FieldRelatedUser, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(WaybillAckRecs.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(WaybillAckRecs.FieldLastUpdatedBy, GB.AllUserDict);

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
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldISID, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldAckRecNo, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldTranNode, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldTranNodePN, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldStatusID, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldRelatedUser, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldCarMarkNo, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldRemark, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldCancelYN, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldCancelDate, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldCancelBy, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(WaybillAckRecs.FieldLastUpdatedBy, 200);
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
            { WaybillAckRecs.FieldAckRecNo, txtAckRecNo.Text.Trim() },
            { WaybillAckRecs.FieldTranNode, txtTranNode.GetComboBoxValue() },
            { WaybillAckRecs.FieldTranNodePN, txtTranNodePN.Text.Trim() },
            { WaybillAckRecs.FieldStatusID, txtStatusID.GetComboBoxValue() },
            { WaybillAckRecs.FieldCarMarkNo, txtCarMarkNo.Text.Trim() },
            { WaybillAckRecs.FieldCancelYN, txtCancelYN.GetComboBoxValue() },
            { WaybillAckRecs.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { WaybillAckRecs.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
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
        var info = new WaybillAckRecs
        {
            ISID = GetRowData(dr, "自增ID").ObjToInt(),
            AckRecNo = GetRowData(dr, "回单编号"),
            TranNode = GetRowData(dr, "当前网点"),
            TranNodePN = GetRowData(dr, "上一/下一网点"),
            StatusID = GetRowData(dr, "当前状态"),
            RelatedUser = GetRowData(dr, "相关人员"),
            CarMarkNo = GetRowData(dr, "车标号"),
            Remark = GetRowData(dr, "备注"),
            CancelYN = GetRowData(dr, "作废") == "是",
            CancelDate = GetRowData(dr, "作废时间").ToDateTime(dtNow),
            CancelBy = GetRowData(dr, "作废人"),
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
        List<WaybillAckRecs> list = await _bll.FindAsync(condition);

        DataTable dtNew = DataTableHelper.CreateTable(
            "自增ID,回单编号,当前网点,上一/下一网点,当前状态,相关人员,车标号,备注,作废,作废时间,作废人,创建时间,创建人,修改时间,修改人");
        var j = 1;
        foreach (WaybillAckRecs t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["自增ID"] = t.ISID;
            dr["回单编号"] = t.AckRecNo;
            dr["当前网点"] = t.TranNode;
            dr["上一/下一网点"] = t.TranNodePN;
            dr["当前状态"] = t.StatusID;
            dr["相关人员"] = t.RelatedUser;
            dr["车标号"] = t.CarMarkNo;
            dr["备注"] = t.Remark;
            dr["作废"] = t.CancelYN ? "是" : "否";
            dr["作废时间"] = t.CancelDate;
            dr["作废人"] = t.CancelBy;
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
        AdvDlg?.AddColumnListItem(WaybillAckRecs.FieldTranNode, GB.AllOuDict);
        AdvDlg?.AddColumnListItem(WaybillAckRecs.FieldStatusID, "运单状态");
        AdvDlg?.AddColumnListItem(WaybillAckRecs.FieldRelatedUser, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(WaybillAckRecs.FieldCreatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(WaybillAckRecs.FieldLastUpdatedBy, GB.AllUserDict);

        #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}