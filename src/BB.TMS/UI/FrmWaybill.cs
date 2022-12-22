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
/// 运单管理
/// </summary>
#if DESIGNER
public partial class FrmWaybill : BaseViewDesigner
#else
public partial class FrmWaybill : BaseViewDock<Waybill, WaybillHttpService, FrmEditWaybill, Waybills, WaybillsHttpService>
#endif
{
    public FrmWaybill(WaybillHttpService bll, WaybillsHttpService childBll, LazilyResolved<FrmEditWaybill> baseForm) : base(bll, childBll, baseForm)
    {
        moduleName = "运单管理";

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
    private void FrmWaybill_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(Waybill.FieldDeliveryType, "交货方式");
            x.AddNode(Waybill.FieldPaymentType, "付款方式");
            x.AddNode(Waybill.FieldWaitNoticeYN, "等通知放货", GB.YesOrNo);
            x.AddNode(Waybill.FieldStatusId, "运单状态");
            x.AddNode(Waybill.FieldFlagApp, "审核", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtFromNode.BindDictItems(GB.AllOuDict, "*当前机构*", true, false);
        txtFromAreaId.BindCityItems(false);
        txtToAreaId.BindCityItems(false);
        txtDeliveryType.BindDictItems("交货方式", null, true, false);
        txtPaymentType.BindDictItems("付款方式", null, true, false);
        txtWaitNoticeYN.BindDictItems(GB.YesOrNo, null, true, false);
        txtStatusNode.BindDictItems(GB.AllOuDict, null, true, false);
        txtStatusId.BindDictItems("运单状态", null, true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "WaybillNo,WaybillDate,TransferInNo,OrderNo,FromNode,FromNodes,FromProvinceId,FromCityId,FromAreaId,ToNode,ToNodes,ToProvinceId,ToCityId,ToAreaId,ShipperCompanyName,ShipperAddress,ShipperTel,Shipper,ConsigneeCompanyName,ConsigneeAddress,ConsigneeTel,Consignee,TransportType,PickUpType,DeliveryType,PaymentType,WaitNoticeYN,NoticeUser,NoticeDate,NoticeRemark,AckRecNo,AbnormityType,Qty,Weight,Cubage,ChargeableWeight,AgencyReceiveCharge,CarriagePrepaid,CarriageForward,CarriageMonthly,CarriageReceipt,CarriageOwed,CarriageOther,Brokerage,AmountInsured,StatusNode,StatusId,UnloadYN,UpstairYN,UpstairNum,SalesMan,Remark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate,AutoNoYN,AutoReceipt";

        #region 添加别名解析，两种方式均可

        // winGridViewPager1.AddColumnAlias(Waybill.FieldWaybillNo, "运单号");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldWaybillDate, "运单时间");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldTransferInNo, "转入单号");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldOrderNo, "订单号");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldFromNode, "起始网点编号");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldFromNodes, "起始区域编号");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldFromProvinceId, "发货省");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldFromCityId, "发货市");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldFromAreaId, "发货区");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldToNode, "目的网点编号");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldToNodes, "目的区域编号");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldToProvinceId, "目的省");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldToCityId, "目的市");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldToAreaId, "目的区");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldShipperCompanyName, "发货公司");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldShipperAddress, "发货地址");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldShipperTel, "发货电话");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldShipper, "发货人");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldConsigneeCompanyName, "收货公司");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldConsigneeAddress, "收货地址");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldConsigneeTel, "收货电话");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldConsignee, "收货人");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldTransportType, "运输方式");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldPickUpType, "提货方式");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldDeliveryType, "交货方式");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldPaymentType, "付款方式");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldWaitNoticeYN, "等通知放货");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldNoticeUser, "通知人员");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldNoticeDate, "通知日期");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldNoticeRemark, "通知备注");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldAckRecNo, "回单号");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldAbnormityType, "异形件");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldQty, "数量");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldWeight, "重量");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldCubage, "体积");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldChargeableWeight, "计费重量");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldAgencyReceiveCharge, "代收款");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldCarriagePrepaid, "现付金额");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldCarriageForward, "到付金额");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldCarriageMonthly, "月结金额");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldCarriageReceipt, "回付金额");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldCarriageOwed, "欠付金额");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldCarriageOther, "其他金额");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldBrokerage, "回扣金额");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldAmountInsured, "保价金额");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldStatusNode, "状态网点");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldStatusId, "运单状态");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldUnloadYN, "卸货");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldUpstairYN, "上楼");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldUpstairNum, "上楼层数");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldSalesMan, "业务员");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldAppUser, "审核人");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldAppDate, "审核时间");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldAutoNoYN, "自动单号");
        // winGridViewPager1.AddColumnAlias(Waybill.FieldAutoReceipt, "自动回单");

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(Waybill.FieldFromNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Waybill.FieldFromAreaId, GB.AllRegions);
        winGridViewPager1.SetColumnDataSource(Waybill.FieldToAreaId, GB.AllRegions);
        winGridViewPager1.SetColumnDataSource(Waybill.FieldTransportType, "运输类型");
        winGridViewPager1.SetColumnDataSource(Waybill.FieldPickUpType, "收货类型");
        winGridViewPager1.SetColumnDataSource(Waybill.FieldDeliveryType, "交货方式");
        winGridViewPager1.SetColumnDataSource(Waybill.FieldPaymentType, "付款方式");
        winGridViewPager1.SetColumnDataSource(Waybill.FieldNoticeUser, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Waybill.FieldStatusNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Waybill.FieldStatusId, "运单状态");
        winGridViewPager1.SetColumnDataSource(Waybill.FieldSalesMan, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Waybill.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Waybill.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Waybill.FieldAppUser, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行


        #endregion

        #endregion

        #region 明细表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridView2.DisplayColumns = "ISID,CargoName,PackageType,CargoUnit,Qty,Weight,Cubage,Price,PriceType,AmountInsured,CarriageCharge,DeliveryCharge,PremiumCharge,UnloadingCharge,UpstairsCharge,WaitNoticeCharge,AckRecCharge,InformationCharge,PackageCharge,PickupCharge,TransferCharge,OtherCharge,AgencyReceiveCharge,AgencyReceiveChargePoundage,Brokerage,PremiumRate,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,CancelYN";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridView2.AddColumnAlias(Waybills.FieldISID, "自增ID");
        // winGridView2.AddColumnAlias(Waybills.FieldCargoName, "货物名称");
        // winGridView2.AddColumnAlias(Waybills.FieldPackageType, "包装类型");
        // winGridView2.AddColumnAlias(Waybills.FieldCargoUnit, "货物单位");
        // winGridView2.AddColumnAlias(Waybills.FieldQty, "件数");
        // winGridView2.AddColumnAlias(Waybills.FieldWeight, "重量");
        // winGridView2.AddColumnAlias(Waybills.FieldCubage, "体积");
        // winGridView2.AddColumnAlias(Waybills.FieldPrice, "单价");
        // winGridView2.AddColumnAlias(Waybills.FieldPriceType, "报价类型");
        // winGridView2.AddColumnAlias(Waybills.FieldAmountInsured, "保价金额");
        // winGridView2.AddColumnAlias(Waybills.FieldCarriageCharge, "运费");
        // winGridView2.AddColumnAlias(Waybills.FieldDeliveryCharge, "送货费");
        // winGridView2.AddColumnAlias(Waybills.FieldPremiumCharge, "保险费");
        // winGridView2.AddColumnAlias(Waybills.FieldUnloadingCharge, "装卸费");
        // winGridView2.AddColumnAlias(Waybills.FieldUpstairsCharge, "上楼费");
        // winGridView2.AddColumnAlias(Waybills.FieldWaitNoticeCharge, "通知费");
        // winGridView2.AddColumnAlias(Waybills.FieldAckRecCharge, "回单费");
        // winGridView2.AddColumnAlias(Waybills.FieldInformationCharge, "信息费");
        // winGridView2.AddColumnAlias(Waybills.FieldPackageCharge, "包装费");
        // winGridView2.AddColumnAlias(Waybills.FieldPickupCharge, "提货费");
        // winGridView2.AddColumnAlias(Waybills.FieldTransferCharge, "中转费");
        // winGridView2.AddColumnAlias(Waybills.FieldOtherCharge, "其他费");
        // winGridView2.AddColumnAlias(Waybills.FieldAgencyReceiveCharge, "代收款");
        // winGridView2.AddColumnAlias(Waybills.FieldAgencyReceiveChargePoundage, "代收款手续费");
        // winGridView2.AddColumnAlias(Waybills.FieldBrokerage, "回扣");
        // winGridView2.AddColumnAlias(Waybills.FieldPremiumRate, "保险费率");
        // winGridView2.AddColumnAlias(Waybills.FieldCreationDate, "创建时间");
        // winGridView2.AddColumnAlias(Waybills.FieldCreatedBy, "创建人");
        // winGridView2.AddColumnAlias(Waybills.FieldLastUpdateDate, "修改时间");
        // winGridView2.AddColumnAlias(Waybills.FieldLastUpdatedBy, "修改人");
        // winGridView2.AddColumnAlias(Waybills.FieldCancelYN, "作废");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridView2.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridView2.SetColumnDataSource(Waybills.FieldPackageType, "包装方式");
        winGridView2.SetColumnDataSource(Waybills.FieldPriceType, "计价方式");
        winGridView2.SetColumnDataSource(Waybills.FieldCreatedBy, GB.AllUserDict);
        winGridView2.SetColumnDataSource(Waybills.FieldLastUpdatedBy, GB.AllUserDict);

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
            //     gridView.SetGridColumWidth(Waybill.FieldWaybillNo, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldWaybillDate, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldTransferInNo, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldOrderNo, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldFromNode, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldFromNodes, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldFromProvinceId, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldFromCityId, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldFromAreaId, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldToNode, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldToNodes, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldToProvinceId, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldToCityId, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldToAreaId, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldShipperCompanyName, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldShipperAddress, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldShipperTel, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldShipper, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldConsigneeCompanyName, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldConsigneeAddress, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldConsigneeTel, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldConsignee, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldTransportType, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldPickUpType, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldDeliveryType, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldPaymentType, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldWaitNoticeYN, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldNoticeUser, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldNoticeDate, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldNoticeRemark, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldAckRecNo, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldAbnormityType, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldQty, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldWeight, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldCubage, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldChargeableWeight, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldAgencyReceiveCharge, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldCarriagePrepaid, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldCarriageForward, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldCarriageMonthly, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldCarriageReceipt, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldCarriageOwed, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldCarriageOther, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldBrokerage, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldAmountInsured, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldStatusNode, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldStatusId, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldUnloadYN, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldUpstairYN, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldUpstairNum, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldSalesMan, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldRemark, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldAppDate, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldAutoNoYN, 200);
            //     gridView.SetGridColumWidth(Waybill.FieldAutoReceipt, 200);
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
            //     gridView.SetGridColumWidth(Waybills.FieldISID, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldCargoName, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldPackageType, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldCargoUnit, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldQty, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldWeight, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldCubage, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldPrice, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldPriceType, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldAmountInsured, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldCarriageCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldDeliveryCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldPremiumCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldUnloadingCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldUpstairsCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldWaitNoticeCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldAckRecCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldInformationCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldPackageCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldPickupCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldTransferCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldOtherCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldAgencyReceiveCharge, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldAgencyReceiveChargePoundage, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldBrokerage, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldPremiumRate, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(Waybills.FieldCancelYN, 200);
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
            { Waybill.FieldWaybillNo, txtWaybillNo.Text.Trim() },
            { Waybill.FieldWaybillDate, txtWaybillDate1.EditValue.ObjToStr() },
            { Waybill.FieldWaybillDate, txtWaybillDate2.EditValue.ObjToStr() },
            { Waybill.FieldTransferInNo, txtTransferInNo.Text.Trim() },
            { Waybill.FieldOrderNo, txtOrderNo.Text.Trim() },
            { Waybill.FieldFromNode, txtFromNode.GetComboBoxValue() },
            { Waybill.FieldFromAreaId, txtFromAreaId.EditValue.ObjToStr() },
            { Waybill.FieldToNode, txtToNode.Text.Trim() },
            { Waybill.FieldToAreaId, txtToAreaId.EditValue.ObjToStr() },
            { Waybill.FieldShipperTel, txtShipperTel.Text.Trim() },
            { Waybill.FieldShipper, txtShipper.Text.Trim() },
            { Waybill.FieldConsigneeTel, txtConsigneeTel.Text.Trim() },
            { Waybill.FieldConsignee, txtConsignee.Text.Trim() },
            { Waybill.FieldDeliveryType, txtDeliveryType.GetComboBoxValue() },
            { Waybill.FieldPaymentType, txtPaymentType.GetComboBoxValue() },
            { Waybill.FieldWaitNoticeYN, txtWaitNoticeYN.GetComboBoxValue() },
            { Waybill.FieldAckRecNo, txtAckRecNo.Text.Trim() },
            { Waybill.FieldStatusNode, txtStatusNode.GetComboBoxValue() },
            { Waybill.FieldStatusId, txtStatusId.GetComboBoxValue() },
            { Waybill.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { Waybill.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
            { Waybill.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
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
        var info = new Waybill
        {
            WaybillNo = GetRowData(dr, "运单号"),
            WaybillDate = GetRowData(dr, "运单时间").ToDateTime(dtNow),
            TransferInNo = GetRowData(dr, "转入单号"),
            OrderNo = GetRowData(dr, "订单号"),
            FromNode = GetRowData(dr, "起始网点编号"),
            FromNodes = GetRowData(dr, "起始区域编号").ObjToInt(),
            FromProvinceId = GetRowData(dr, "发货省"),
            FromCityId = GetRowData(dr, "发货市"),
            FromAreaId = GetRowData(dr, "发货区"),
            ToNode = GetRowData(dr, "目的网点编号"),
            ToNodes = GetRowData(dr, "目的区域编号").ObjToInt(),
            ToProvinceId = GetRowData(dr, "目的省"),
            ToCityId = GetRowData(dr, "目的市"),
            ToAreaId = GetRowData(dr, "目的区"),
            ShipperCompanyName = GetRowData(dr, "发货公司"),
            ShipperAddress = GetRowData(dr, "发货地址"),
            ShipperTel = GetRowData(dr, "发货电话"),
            Shipper = GetRowData(dr, "发货人"),
            ConsigneeCompanyName = GetRowData(dr, "收货公司"),
            ConsigneeAddress = GetRowData(dr, "收货地址"),
            ConsigneeTel = GetRowData(dr, "收货电话"),
            Consignee = GetRowData(dr, "收货人"),
            TransportType = GetRowData(dr, "运输方式"),
            PickUpType = GetRowData(dr, "提货方式"),
            DeliveryType = GetRowData(dr, "交货方式"),
            PaymentType = GetRowData(dr, "付款方式"),
            WaitNoticeYN = GetRowData(dr, "等通知放货") == "是",
            NoticeUser = GetRowData(dr, "通知人员"),
            NoticeDate = GetRowData(dr, "通知日期").ToDateTime(dtNow),
            NoticeRemark = GetRowData(dr, "通知备注"),
            AckRecNo = GetRowData(dr, "回单号"),
            AbnormityType = GetRowData(dr, "异形件") == "是",
            Qty = GetRowData(dr, "数量").ObjToInt(),
            Weight = GetRowData(dr, "重量").ToDecimal(),
            Cubage = GetRowData(dr, "体积").ToDecimal(),
            ChargeableWeight = GetRowData(dr, "计费重量").ToDecimal(),
            AgencyReceiveCharge = GetRowData(dr, "代收款").ToDecimal(),
            CarriagePrepaid = GetRowData(dr, "现付金额").ToDecimal(),
            CarriageForward = GetRowData(dr, "到付金额").ToDecimal(),
            CarriageMonthly = GetRowData(dr, "月结金额").ToDecimal(),
            CarriageReceipt = GetRowData(dr, "回付金额").ToDecimal(),
            CarriageOwed = GetRowData(dr, "欠付金额").ToDecimal(),
            CarriageOther = GetRowData(dr, "其他金额").ToDecimal(),
            Brokerage = GetRowData(dr, "回扣金额").ToDecimal(),
            AmountInsured = GetRowData(dr, "保价金额").ToDecimal(),
            StatusNode = GetRowData(dr, "状态网点"),
            StatusId = GetRowData(dr, "运单状态"),
            UnloadYN = GetRowData(dr, "卸货") == "是",
            UpstairYN = GetRowData(dr, "上楼") == "是",
            UpstairNum = GetRowData(dr, "上楼层数").ObjToByte(),
            SalesMan = GetRowData(dr, "业务员"),
            Remark = GetRowData(dr, "备注"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
            FlagApp = GetRowData(dr, "审核") == "是",
            AppUser = GetRowData(dr, "审核人"),
            AppDate = GetRowData(dr, "审核时间").ToDateTime(dtNow),
            AutoNoYN = GetRowData(dr, "自动单号") == "是",
            AutoReceipt = GetRowData(dr, "自动回单") == "是",
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
        List<Waybill> list = await _bll.FindAsync(condition);

        DataTable dtNew = DataTableHelper.CreateTable(
            "运单号,运单时间,转入单号,订单号,起始网点编号,起始区域编号,发货省,发货市,发货区,目的网点编号,目的区域编号,目的省,目的市,目的区,发货公司,发货地址,发货电话,发货人,收货公司,收货地址,收货电话,收货人,运输方式,提货方式,交货方式,付款方式,等通知放货,通知人员,通知日期,通知备注,回单号,异形件,数量,重量,体积,计费重量,代收款,现付金额,到付金额,月结金额,回付金额,欠付金额,其他金额,回扣金额,保价金额,状态网点,运单状态,卸货,上楼,上楼层数,业务员,备注,创建时间,创建人,修改时间,修改人,审核,审核人,审核时间,自动单号,自动回单");
        var j = 1;
        foreach (Waybill t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["运单号"] = t.WaybillNo;
            dr["运单时间"] = t.WaybillDate;
            dr["转入单号"] = t.TransferInNo;
            dr["订单号"] = t.OrderNo;
            dr["起始网点编号"] = t.FromNode;
            dr["起始区域编号"] = t.FromNodes;
            dr["发货省"] = t.FromProvinceId;
            dr["发货市"] = t.FromCityId;
            dr["发货区"] = t.FromAreaId;
            dr["目的网点编号"] = t.ToNode;
            dr["目的区域编号"] = t.ToNodes;
            dr["目的省"] = t.ToProvinceId;
            dr["目的市"] = t.ToCityId;
            dr["目的区"] = t.ToAreaId;
            dr["发货公司"] = t.ShipperCompanyName;
            dr["发货地址"] = t.ShipperAddress;
            dr["发货电话"] = t.ShipperTel;
            dr["发货人"] = t.Shipper;
            dr["收货公司"] = t.ConsigneeCompanyName;
            dr["收货地址"] = t.ConsigneeAddress;
            dr["收货电话"] = t.ConsigneeTel;
            dr["收货人"] = t.Consignee;
            dr["运输方式"] = t.TransportType;
            dr["提货方式"] = t.PickUpType;
            dr["交货方式"] = t.DeliveryType;
            dr["付款方式"] = t.PaymentType;
            dr["等通知放货"] = t.WaitNoticeYN ? "是" : "否";
            dr["通知人员"] = t.NoticeUser;
            dr["通知日期"] = t.NoticeDate;
            dr["通知备注"] = t.NoticeRemark;
            dr["回单号"] = t.AckRecNo;
            dr["异形件"] = t.AbnormityType ? "是" : "否";
            dr["数量"] = t.Qty;
            dr["重量"] = t.Weight;
            dr["体积"] = t.Cubage;
            dr["计费重量"] = t.ChargeableWeight;
            dr["代收款"] = t.AgencyReceiveCharge;
            dr["现付金额"] = t.CarriagePrepaid;
            dr["到付金额"] = t.CarriageForward;
            dr["月结金额"] = t.CarriageMonthly;
            dr["回付金额"] = t.CarriageReceipt;
            dr["欠付金额"] = t.CarriageOwed;
            dr["其他金额"] = t.CarriageOther;
            dr["回扣金额"] = t.Brokerage;
            dr["保价金额"] = t.AmountInsured;
            dr["状态网点"] = t.StatusNode;
            dr["运单状态"] = t.StatusId;
            dr["卸货"] = t.UnloadYN ? "是" : "否";
            dr["上楼"] = t.UpstairYN ? "是" : "否";
            dr["上楼层数"] = t.UpstairNum;
            dr["业务员"] = t.SalesMan;
            dr["备注"] = t.Remark;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["修改时间"] = t.LastUpdateDate;
            dr["修改人"] = t.LastUpdatedBy;
            dr["审核"] = t.FlagApp ? "是" : "否";
            dr["审核人"] = t.AppUser;
            dr["审核时间"] = t.AppDate;
            dr["自动单号"] = t.AutoNoYN ? "是" : "否";
            dr["自动回单"] = t.AutoReceipt ? "是" : "否";
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
        AdvDlg?.AddColumnListItem(Waybill.FieldFromNode, GB.AllOuDict);
        AdvDlg?.AddColumnListItem(Waybill.FieldTransportType, "运输类型");
        AdvDlg?.AddColumnListItem(Waybill.FieldPickUpType, "收货类型");
        AdvDlg?.AddColumnListItem(Waybill.FieldDeliveryType, "交货方式");
        AdvDlg?.AddColumnListItem(Waybill.FieldPaymentType, "付款方式");
        AdvDlg?.AddColumnListItem(Waybill.FieldNoticeUser, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(Waybill.FieldStatusNode, GB.AllOuDict);
        AdvDlg?.AddColumnListItem(Waybill.FieldStatusId, "运单状态");
        AdvDlg?.AddColumnListItem(Waybill.FieldSalesMan, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(Waybill.FieldCreatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(Waybill.FieldLastUpdatedBy, GB.AllUserDict);
        AdvDlg?.AddColumnListItem(Waybill.FieldAppUser, GB.AllUserDict);

        #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}