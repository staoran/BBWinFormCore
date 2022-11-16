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
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 费用调整
/// </summary>
public partial class FrmCostMsg : BaseViewDock<CostMsg, CostMsgHttpService, FrmEditCostMsg, CostMsgs, CostMsgsHttpService>
{
    public FrmCostMsg(CostMsgHttpService bll, CostMsgsHttpService childBll, LazilyResolved<FrmEditCostMsg> baseForm) : base(bll, childBll, baseForm)
    {
        moduleName = "费用调整";

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
    private void FrmCostMsg_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(CostMsg.FieldSourceType, "费用调整来源");
            x.AddNode(CostMsg.FieldValueType, "费用调整类型");
            x.AddNode(CostMsg.FieldStatusID, "费用调整状态");
            x.AddNode(CostMsg.FieldFlagApp, "审核", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtSourceType.BindDictItems("费用调整来源", "1", true, false);
        txtRecvMsgAccount.BindDictItems(GB.AllOuDict, null, true, false);
        txtValueType.BindDictItems("费用调整类型", "1", true, false);
        txtStatusID.BindDictItems("费用调整状态", "0", true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "CostMsgNo,SourceType,WaybillNo,SendMsgNode,SendMsgContent,AttaPath,RecvMsgType,RecvMsgAccount,ValueType,SourceValue,ActiveValue,StatusID,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate,FinancialCenter";

        #region 添加别名解析，两种方式均可

        // winGridViewPager1.AddColumnAlias(CostMsg.FieldCostMsgNo, "费用调整编号");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldSourceType, "来源类型");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldWaybillNo, "运单编号");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldSendMsgNode, "申请网点");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldSendMsgContent, "内容描述");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldAttaPath, "附件");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldRecvMsgType, "接收类型");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldRecvMsgAccount, "接收网点");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldValueType, "费用类型");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldSourceValue, "原始值");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldActiveValue, "修改值");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldStatusID, "单据状态");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldAppUser, "审批人");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldAppDate, "审批时间");
        // winGridViewPager1.AddColumnAlias(CostMsg.FieldFinancialCenter, "财务中心");

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(CostMsg.FieldSourceType, "费用调整来源");
        winGridViewPager1.SetColumnDataSource(CostMsg.FieldSendMsgNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(CostMsg.FieldRecvMsgType, "费用接受类型");
        winGridViewPager1.SetColumnDataSource(CostMsg.FieldRecvMsgAccount, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(CostMsg.FieldValueType, "费用调整类型");
        winGridViewPager1.SetColumnDataSource(CostMsg.FieldStatusID, "费用调整状态");
        winGridViewPager1.SetColumnDataSource(CostMsg.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(CostMsg.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(CostMsg.FieldAppUser, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(CostMsg.FieldFinancialCenter, GB.AllOuDict);

        #endregion

        #region 添加列字段汇总行


        #endregion

        #endregion

        #region 明细表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridView2.DisplayColumns = "ISID,CostMsgNo,StatusID,RecvMsgNode,RecvMsgContent,AttaPath,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridView2.AddColumnAlias(CostMsgs.FieldISID, "自增ID");
        // winGridView2.AddColumnAlias(CostMsgs.FieldCostMsgNo, "费用调整编号");
        // winGridView2.AddColumnAlias(CostMsgs.FieldStatusID, "单据状态");
        // winGridView2.AddColumnAlias(CostMsgs.FieldRecvMsgNode, "回复网点");
        // winGridView2.AddColumnAlias(CostMsgs.FieldRecvMsgContent, "回复内容");
        // winGridView2.AddColumnAlias(CostMsgs.FieldAttaPath, "附件");
        // winGridView2.AddColumnAlias(CostMsgs.FieldCreationDate, "创建时间");
        // winGridView2.AddColumnAlias(CostMsgs.FieldCreatedBy, "创建人");
        // winGridView2.AddColumnAlias(CostMsgs.FieldLastUpdateDate, "修改时间");
        // winGridView2.AddColumnAlias(CostMsgs.FieldLastUpdatedBy, "修改人");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridView2.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridView2.SetColumnDataSource(CostMsgs.FieldStatusID, "费用调整状态");
        winGridView2.SetColumnDataSource(CostMsgs.FieldRecvMsgNode, GB.AllOuDict);
        winGridView2.SetColumnDataSource(CostMsgs.FieldCreatedBy, GB.AllUserDict);
        winGridView2.SetColumnDataSource(CostMsgs.FieldLastUpdatedBy, GB.AllUserDict);

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
            //     gridView.SetGridColumWidth(CostMsg.FieldCostMsgNo, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldSourceType, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldWaybillNo, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldSendMsgNode, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldSendMsgContent, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldAttaPath, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldRecvMsgType, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldRecvMsgAccount, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldValueType, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldSourceValue, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldActiveValue, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldStatusID, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldAppDate, 200);
            //     gridView.SetGridColumWidth(CostMsg.FieldFinancialCenter, 200);
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
            //     gridView.SetGridColumWidth(CostMsgs.FieldISID, 200);
            //     gridView.SetGridColumWidth(CostMsgs.FieldCostMsgNo, 200);
            //     gridView.SetGridColumWidth(CostMsgs.FieldStatusID, 200);
            //     gridView.SetGridColumWidth(CostMsgs.FieldRecvMsgNode, 200);
            //     gridView.SetGridColumWidth(CostMsgs.FieldRecvMsgContent, 200);
            //     gridView.SetGridColumWidth(CostMsgs.FieldAttaPath, 200);
            //     gridView.SetGridColumWidth(CostMsgs.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(CostMsgs.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(CostMsgs.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(CostMsgs.FieldLastUpdatedBy, 200);
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
            { CostMsg.FieldCostMsgNo, txtCostMsgNo.Text.Trim() },
            { CostMsg.FieldSourceType, txtSourceType.GetComboBoxValue() },
            { CostMsg.FieldWaybillNo, txtWaybillNo.Text.Trim() },
            { CostMsg.FieldSendMsgContent, txtSendMsgContent.Text.Trim() },
            { CostMsg.FieldRecvMsgAccount, txtRecvMsgAccount.GetComboBoxValue() },
            { CostMsg.FieldValueType, txtValueType.GetComboBoxValue() },
            { CostMsg.FieldStatusID, txtStatusID.GetComboBoxValue() },
            { CostMsg.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { CostMsg.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
            { CostMsg.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
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
        var info = new CostMsg
        {
            CostMsgNo = GetRowData(dr, "费用调整编号"),
            SourceType = GetRowData(dr, "来源类型"),
            WaybillNo = GetRowData(dr, "运单编号"),
            SendMsgNode = GetRowData(dr, "申请网点"),
            SendMsgContent = GetRowData(dr, "内容描述"),
            AttaPath = GetRowData(dr, "附件"),
            RecvMsgType = GetRowData(dr, "接收类型"),
            RecvMsgAccount = GetRowData(dr, "接收网点"),
            ValueType = GetRowData(dr, "费用类型"),
            SourceValue = GetRowData(dr, "原始值").ToDecimal(),
            ActiveValue = GetRowData(dr, "修改值").ToDecimal(),
            StatusID = GetRowData(dr, "单据状态"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
            FlagApp = GetRowData(dr, "审核") == "是",
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
        List<CostMsg> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "费用调整编号,来源类型,运单编号,申请网点,内容描述,附件,接收类型,接收网点,费用类型,原始值,修改值,单据状态,创建时间,创建人,修改时间,修改人,审核,审批人,审批时间,财务中心");
        var j = 1;
        foreach (CostMsg t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["费用调整编号"] = t.CostMsgNo;
            dr["来源类型"] = t.SourceType;
            dr["运单编号"] = t.WaybillNo;
            dr["申请网点"] = t.SendMsgNode;
            dr["内容描述"] = t.SendMsgContent;
            dr["附件"] = t.AttaPath;
            dr["接收类型"] = t.RecvMsgType;
            dr["接收网点"] = t.RecvMsgAccount;
            dr["费用类型"] = t.ValueType;
            dr["原始值"] = t.SourceValue;
            dr["修改值"] = t.ActiveValue;
            dr["单据状态"] = t.StatusID;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["修改时间"] = t.LastUpdateDate;
            dr["修改人"] = t.LastUpdatedBy;
            dr["审核"] = t.FlagApp ? "是" : "否";
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
            AdvDlg?.AddColumnListItem(CostMsg.FieldSourceType, "费用调整来源");
            AdvDlg?.AddColumnListItem(CostMsg.FieldSendMsgNode, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(CostMsg.FieldRecvMsgType, "费用接受类型");
            AdvDlg?.AddColumnListItem(CostMsg.FieldRecvMsgAccount, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(CostMsg.FieldValueType, "费用调整类型");
            AdvDlg?.AddColumnListItem(CostMsg.FieldStatusID, "费用调整状态");
            AdvDlg?.AddColumnListItem(CostMsg.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(CostMsg.FieldLastUpdatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(CostMsg.FieldAppUser, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(CostMsg.FieldFinancialCenter, GB.AllOuDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}