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
/// 签收表
/// </summary>
#if DESIGNER
public partial class FrmWaybillSign : BaseViewDesigner
#else
public partial class FrmWaybillSign : BaseViewDock<WaybillSign, WaybillSignHttpService, FrmEditWaybillSign>
#endif
{
    public FrmWaybillSign(WaybillSignHttpService bll, LazilyResolved<FrmEditWaybillSign> baseForm) : base(bll, baseForm)
    {
        moduleName = "签收表";

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
    private void FrmWaybillSign_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(WaybillSign.FieldDeliveryType, "交货方式");
            x.AddNode(WaybillSign.FieldAckRecYN, "回单签收", GB.YesOrNo);
            x.AddNode(WaybillSign.FieldFlagApp, "审核", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtTranNode.BindDictItems(GB.AllOuDict, "*当前机构*", true, false);
        txtDeliveryType.BindDictItems("交货方式", null, true, false);
        txtAckRecYN.BindDictItems(GB.YesOrNo, null, true, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", true, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "ISID,WaybillNo,TranNode,TranNodes,DeliveryType,Consignee,Consigneeid,ConsigneeidPicAdds,ConsigneeRemark,Qty,SignQty,AckRecYN,AckRecNo,AckRecQty,AckRecRemark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldISID, "自增ID");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldWaybillNo, "运单编号");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldTranNode, "目的网点编号");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldTranNodes, "目的区域编号");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldDeliveryType, "交货类型");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldConsignee, "签收人");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldConsigneeid, "签收人证件编号");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldConsigneeidPicAdds, "签收图片地址");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldConsigneeRemark, "签收备注");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldQty, "数量");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldSignQty, "签收数量");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldAckRecYN, "回单签收");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldAckRecNo, "回单编号");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldAckRecQty, "回单数量");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldAckRecRemark, "回单备注");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldAppUser, "审核人");
        // winGridViewPager1.AddColumnAlias(WaybillSign.FieldAppDate, "审核时间");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(WaybillSign.FieldTranNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(WaybillSign.FieldDeliveryType, "交货方式");
        winGridViewPager1.SetColumnDataSource(WaybillSign.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(WaybillSign.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(WaybillSign.FieldAppUser, GB.AllUserDict);

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
            //     gridView.SetGridColumWidth(WaybillSign.FieldISID, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldWaybillNo, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldTranNode, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldTranNodes, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldDeliveryType, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldConsignee, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldConsigneeid, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldConsigneeidPicAdds, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldConsigneeRemark, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldQty, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldSignQty, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldAckRecYN, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldAckRecNo, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldAckRecQty, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldAckRecRemark, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(WaybillSign.FieldAppDate, 200);
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
            { WaybillSign.FieldWaybillNo, txtWaybillNo.Text.Trim() },
            { WaybillSign.FieldTranNode, txtTranNode.GetComboBoxValue() },
            { WaybillSign.FieldTranNodes, txtTranNodes.Text.Trim() },
            { WaybillSign.FieldDeliveryType, txtDeliveryType.GetComboBoxValue() },
            { WaybillSign.FieldConsignee, txtConsignee.Text.Trim() },
            { WaybillSign.FieldConsigneeid, txtConsigneeid.Text.Trim() },
            { WaybillSign.FieldConsigneeidPicAdds, txtConsigneeidPicAdds.Text.Trim() },
            { WaybillSign.FieldConsigneeRemark, txtConsigneeRemark.Text.Trim() },
            { WaybillSign.FieldQty, txtQty.Text.Trim() },
            { WaybillSign.FieldSignQty, txtSignQty.Text.Trim() },
            { WaybillSign.FieldAckRecYN, txtAckRecYN.GetComboBoxValue() },
            { WaybillSign.FieldAckRecNo, txtAckRecNo.Text.Trim() },
            { WaybillSign.FieldAckRecQty, txtAckRecQty.Text.Trim() },
            { WaybillSign.FieldAckRecRemark, txtAckRecRemark.Text.Trim() },
            { WaybillSign.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { WaybillSign.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
            { WaybillSign.FieldCreatedBy, txtCreatedBy.GetComboBoxValue() },
            { WaybillSign.FieldLastUpdateDate, txtLastUpdateDate1.EditValue.ObjToStr() },
            { WaybillSign.FieldLastUpdateDate, txtLastUpdateDate2.EditValue.ObjToStr() },
            { WaybillSign.FieldLastUpdatedBy, txtLastUpdatedBy.GetComboBoxValue() },
            { WaybillSign.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
            { WaybillSign.FieldAppUser, txtAppUser.GetComboBoxValue() },
            { WaybillSign.FieldAppDate, txtAppDate1.EditValue.ObjToStr() },
            { WaybillSign.FieldAppDate, txtAppDate2.EditValue.ObjToStr() },
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
        var info = new WaybillSign
        {
            ISID = GetRowData(dr, "自增ID").ObjToInt(),
            WaybillNo = GetRowData(dr, "运单编号"),
            TranNode = GetRowData(dr, "目的网点编号"),
            TranNodes = GetRowData(dr, "目的区域编号").ObjToInt(),
            DeliveryType = GetRowData(dr, "交货类型"),
            Consignee = GetRowData(dr, "签收人"),
            Consigneeid = GetRowData(dr, "签收人证件编号"),
            ConsigneeidPicAdds = GetRowData(dr, "签收图片地址"),
            ConsigneeRemark = GetRowData(dr, "签收备注"),
            Qty = GetRowData(dr, "数量").ObjToInt(),
            SignQty = GetRowData(dr, "签收数量").ObjToInt(),
            AckRecYN = GetRowData(dr, "回单签收") == "是",
            AckRecNo = GetRowData(dr, "回单编号"),
            AckRecQty = GetRowData(dr, "回单数量").ObjToInt(),
            AckRecRemark = GetRowData(dr, "回单备注"),
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

        Dictionary<string,string> condition = GetQueryCondition();
        List<WaybillSign> list = await _bll.FindAsync(condition);

        DataTable dtNew = DataTableHelper.CreateTable(
            "自增ID,运单编号,目的网点编号,目的区域编号,交货类型,签收人,签收人证件编号,签收图片地址,签收备注,数量,签收数量,回单签收,回单编号,回单数量,回单备注,创建时间,创建人,修改时间,修改人,审核,审核人,审核时间");
        var j = 1;
        foreach (WaybillSign t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["自增ID"] = t.ISID;
            dr["运单编号"] = t.WaybillNo;
            dr["目的网点编号"] = t.TranNode;
            dr["目的区域编号"] = t.TranNodes;
            dr["交货类型"] = t.DeliveryType;
            dr["签收人"] = t.Consignee;
            dr["签收人证件编号"] = t.Consigneeid;
            dr["签收图片地址"] = t.ConsigneeidPicAdds;
            dr["签收备注"] = t.ConsigneeRemark;
            dr["数量"] = t.Qty;
            dr["签收数量"] = t.SignQty;
            dr["回单签收"] = t.AckRecYN ? "是" : "否";
            dr["回单编号"] = t.AckRecNo;
            dr["回单数量"] = t.AckRecQty;
            dr["回单备注"] = t.AckRecRemark;
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
        AdvDlg?.AddColumnListItem(WaybillSign.FieldTranNode, GB.AllOuDict);
        AdvDlg?.AddColumnListItem(WaybillSign.FieldDeliveryType, "交货方式");
        AdvDlg?.AddColumnListItem(WaybillSign.FieldCreatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(WaybillSign.FieldLastUpdatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(WaybillSign.FieldAppUser, GB.AllUserDict);

        #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}