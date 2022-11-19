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
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;

using BB.Entity.TMS;

using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 送货配载
/// </summary>
public partial class FrmStowage : BaseViewDock<Stowage, StowageHttpService, FrmEditStowage, Stowages, StowagesHttpService>
{
    public FrmStowage(StowageHttpService bll, StowagesHttpService childBll, LazilyResolved<FrmEditStowage> baseForm) : base(bll, childBll, baseForm)
    {
        moduleName = "送货配载";

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
    private void FrmStowage_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(Stowage.FieldStowageType, "配载类型");
            x.AddNode(Stowage.FieldTransType, "配载运输类型");
            x.AddNode(Stowage.FieldCheckInYN, "核销", GB.YesOrNo);
            x.AddNode(Stowage.FieldFlagApp, "审核", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtStowageType.BindDictItems("配载类型", "01", true, false);
        txtTransType.BindDictItems("配载运输类型", "01", true, false);
        txtCheckInYN.BindDictItems(GB.YesOrNo, null, true, false);
        txtCheckInAccount.BindDictItems(GB.AllUserDict, null, true, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "StowageNo,TranNodeNO,StowageType,TransType,TransMarkNo,TransNo,TransDriver,TransDriverPhone,TransDate,TotalQty,TotalWeight,TotalCubage,TotalCarriage,TransCarriage,CheckInYN,CheckInAccount,CheckInDate,Remark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate,Income,SharType";

        #region 添加别名解析，两种方式均可

        // winGridViewPager1.AddColumnAlias(Stowage.FieldStowageNo, "配载编号");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTranNodeNO, "网点编号");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldStowageType, "配载单类型");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTransType, "配载运输类型");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTransMarkNo, "配载车标号");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTransNo, "车辆编号");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTransDriver, "司机");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTransDriverPhone, "司机电话");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTransDate, "承运时间");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTotalQty, "总件数");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTotalWeight, "总重量");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTotalCubage, "总体积");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTotalCarriage, "总运费");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldTransCarriage, "支付费用");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldCheckInYN, "核销");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldCheckInAccount, "核销人");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldCheckInDate, "核销时间");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldAppUser, "审核人");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldAppDate, "审核时间");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldIncome, "收入");
        // winGridViewPager1.AddColumnAlias(Stowage.FieldSharType, "分摊类型");

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(Stowage.FieldTranNodeNO, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Stowage.FieldStowageType, "配载类型");
        winGridViewPager1.SetColumnDataSource(Stowage.FieldTransType, "配载运输类型");
        winGridViewPager1.SetColumnDataSource(Stowage.FieldCheckInAccount, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Stowage.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Stowage.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Stowage.FieldAppUser, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行


        #endregion

        #endregion

        #region 明细表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridView2.DisplayColumns = "ISID,StowageNo,InputType,WaybillNo,FromNode,FromNodes,ToNode,ToNodes,ConsigneeCompanyName,ConsigneeAddress,ConsigneeTel,Consignee,DeliveryType,PaymentType,AckRecQty,AckRecType,AckRecNo,Qty,Weight,Cubage,UnloadYN,UpstairYN,UpstairNum,SmsYN,StowageCarriage,StatusID,Remark";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridView2.AddColumnAlias(Stowages.FieldISID, "自增ID");
        // winGridView2.AddColumnAlias(Stowages.FieldStowageNo, "配载编号");
        // winGridView2.AddColumnAlias(Stowages.FieldInputType, "导入类型");
        // winGridView2.AddColumnAlias(Stowages.FieldWaybillNo, "运单编号");
        // winGridView2.AddColumnAlias(Stowages.FieldFromNode, "发货网点");
        // winGridView2.AddColumnAlias(Stowages.FieldFromNodes, "发货区域");
        // winGridView2.AddColumnAlias(Stowages.FieldToNode, "目的网点");
        // winGridView2.AddColumnAlias(Stowages.FieldToNodes, "目的区域");
        // winGridView2.AddColumnAlias(Stowages.FieldConsigneeCompanyName, "收货公司");
        // winGridView2.AddColumnAlias(Stowages.FieldConsigneeAddress, "收货地址");
        // winGridView2.AddColumnAlias(Stowages.FieldConsigneeTel, "收货电话");
        // winGridView2.AddColumnAlias(Stowages.FieldConsignee, "收货人");
        // winGridView2.AddColumnAlias(Stowages.FieldDeliveryType, "交货方式");
        // winGridView2.AddColumnAlias(Stowages.FieldPaymentType, "付款方式");
        // winGridView2.AddColumnAlias(Stowages.FieldAckRecQty, "回单数量");
        // winGridView2.AddColumnAlias(Stowages.FieldAckRecType, "回单类型");
        // winGridView2.AddColumnAlias(Stowages.FieldAckRecNo, "回单号");
        // winGridView2.AddColumnAlias(Stowages.FieldQty, "数量");
        // winGridView2.AddColumnAlias(Stowages.FieldWeight, "重量");
        // winGridView2.AddColumnAlias(Stowages.FieldCubage, "体积");
        // winGridView2.AddColumnAlias(Stowages.FieldUnloadYN, "是否卸货");
        // winGridView2.AddColumnAlias(Stowages.FieldUpstairYN, "是否上楼");
        // winGridView2.AddColumnAlias(Stowages.FieldUpstairNum, "楼层");
        // winGridView2.AddColumnAlias(Stowages.FieldSmsYN, "是否短信");
        // winGridView2.AddColumnAlias(Stowages.FieldStowageCarriage, "分摊费用");
        // winGridView2.AddColumnAlias(Stowages.FieldStatusID, "状态");
        // winGridView2.AddColumnAlias(Stowages.FieldRemark, "备注");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridView2.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridView2.SetColumnDataSource(Stowages.FieldInputType, "配载导入类型");
        winGridView2.SetColumnDataSource(Stowages.FieldDeliveryType, "交货方式");
        winGridView2.SetColumnDataSource(Stowages.FieldPaymentType, "付款方式");

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
            //     gridView.SetGridColumWidth(Stowage.FieldStowageNo, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTranNodeNO, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldStowageType, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTransType, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTransMarkNo, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTransNo, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTransDriver, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTransDriverPhone, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTransDate, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTotalQty, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTotalWeight, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTotalCubage, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTotalCarriage, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldTransCarriage, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldCheckInYN, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldCheckInAccount, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldCheckInDate, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldRemark, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldAppDate, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldIncome, 200);
            //     gridView.SetGridColumWidth(Stowage.FieldSharType, 200);
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
            //     gridView.SetGridColumWidth(Stowages.FieldISID, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldStowageNo, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldInputType, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldWaybillNo, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldFromNode, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldFromNodes, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldToNode, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldToNodes, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldConsigneeCompanyName, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldConsigneeAddress, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldConsigneeTel, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldConsignee, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldDeliveryType, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldPaymentType, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldAckRecQty, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldAckRecType, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldAckRecNo, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldQty, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldWeight, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldCubage, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldUnloadYN, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldUpstairYN, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldUpstairNum, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldSmsYN, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldStowageCarriage, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldStatusID, 200);
            //     gridView.SetGridColumWidth(Stowages.FieldRemark, 200);
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
            { Stowage.FieldStowageNo, txtStowageNo.Text.Trim() },
            { Stowage.FieldStowageType, txtStowageType.GetComboBoxValue() },
            { Stowage.FieldTransType, txtTransType.GetComboBoxValue() },
            { Stowage.FieldTransMarkNo, txtTransMarkNo.Text.Trim() },
            { Stowage.FieldTransNo, txtTransNo.Text.Trim() },
            { Stowage.FieldTransDriver, txtTransDriver.Text.Trim() },
            { Stowage.FieldTransDriverPhone, txtTransDriverPhone.Text.Trim() },
            { Stowage.FieldCheckInYN, txtCheckInYN.GetComboBoxValue() },
            { Stowage.FieldCheckInAccount, txtCheckInAccount.GetComboBoxValue() },
            { Stowage.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { Stowage.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
            { Stowage.FieldCreatedBy, txtCreatedBy.GetComboBoxValue() },
            { Stowage.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
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
        var info = new Stowage
        {
            StowageNo = GetRowData(dr, "配载编号"),
            TranNodeNO = GetRowData(dr, "网点编号"),
            StowageType = GetRowData(dr, "配载单类型"),
            TransType = GetRowData(dr, "配载运输类型"),
            TransMarkNo = GetRowData(dr, "配载车标号"),
            TransNo = GetRowData(dr, "车辆编号"),
            TransDriver = GetRowData(dr, "司机"),
            TransDriverPhone = GetRowData(dr, "司机电话"),
            TransDate = GetRowData(dr, "承运时间").ToDateTime(dtNow),
            TotalQty = GetRowData(dr, "总件数").ObjToInt(),
            TotalWeight = GetRowData(dr, "总重量").ToDecimal(),
            TotalCubage = GetRowData(dr, "总体积").ToDecimal(),
            TotalCarriage = GetRowData(dr, "总运费").ToDecimal(),
            TransCarriage = GetRowData(dr, "支付费用").ToDecimal(),
            CheckInYN = GetRowData(dr, "核销") == "是",
            CheckInAccount = GetRowData(dr, "核销人"),
            CheckInDate = GetRowData(dr, "核销时间").ToDateTime(dtNow),
            Remark = GetRowData(dr, "备注"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
            FlagApp = GetRowData(dr, "审核") == "是",
            AppUser = GetRowData(dr, "审核人"),
            AppDate = GetRowData(dr, "审核时间").ToDateTime(dtNow),
            Income = GetRowData(dr, "收入").ToDecimal(),
            SharType = GetRowData(dr, "分摊类型"),
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
        List<Stowage> list = await _bll.FindAsync(condition);

        DataTable dtNew = DataTableHelper.CreateTable(
            "配载编号,网点编号,配载单类型,配载运输类型,配载车标号,车辆编号,司机,司机电话,承运时间,总件数,总重量,总体积,总运费,支付费用,核销,核销人,核销时间,备注,创建时间,创建人,修改时间,修改人,审核,审核人,审核时间,收入,分摊类型");
        var j = 1;
        foreach (Stowage t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["配载编号"] = t.StowageNo;
            dr["网点编号"] = t.TranNodeNO;
            dr["配载单类型"] = t.StowageType;
            dr["配载运输类型"] = t.TransType;
            dr["配载车标号"] = t.TransMarkNo;
            dr["车辆编号"] = t.TransNo;
            dr["司机"] = t.TransDriver;
            dr["司机电话"] = t.TransDriverPhone;
            dr["承运时间"] = t.TransDate;
            dr["总件数"] = t.TotalQty;
            dr["总重量"] = t.TotalWeight;
            dr["总体积"] = t.TotalCubage;
            dr["总运费"] = t.TotalCarriage;
            dr["支付费用"] = t.TransCarriage;
            dr["核销"] = t.CheckInYN ? "是" : "否";
            dr["核销人"] = t.CheckInAccount;
            dr["核销时间"] = t.CheckInDate;
            dr["备注"] = t.Remark;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["修改时间"] = t.LastUpdateDate;
            dr["修改人"] = t.LastUpdatedBy;
            dr["审核"] = t.FlagApp ? "是" : "否";
            dr["审核人"] = t.AppUser;
            dr["审核时间"] = t.AppDate;
            dr["收入"] = t.Income;
            dr["分摊类型"] = t.SharType;
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
        AdvDlg?.AddColumnListItem(Stowage.FieldTranNodeNO, GB.AllOuDict);
        AdvDlg?.AddColumnListItem(Stowage.FieldStowageType, "配载类型");
        AdvDlg?.AddColumnListItem(Stowage.FieldTransType, "配载运输类型");
        AdvDlg?.AddColumnListItem(Stowage.FieldCheckInAccount, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(Stowage.FieldCreatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(Stowage.FieldLastUpdatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(Stowage.FieldAppUser, GB.AllUserDict);

        #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}