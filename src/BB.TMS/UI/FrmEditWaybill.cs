using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 运单管理
/// </summary>
#if DESIGNER
public partial class FrmEditWaybill : BaseEditDesigner
{
    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
#else
public partial class FrmEditWaybill : BaseEditForm<Waybill, WaybillHttpService, Waybills, WaybillsHttpService>
{
#endif
    public FrmEditWaybill(WaybillHttpService bll, WaybillsHttpService childBll, IValidator<Waybill> validator, IValidator<Waybills> childValidator) : base(bll, childBll, validator, childValidator)
    {
        InitializeComponent();

        Load += FrmEditWaybill_Load;
    }

    /// <summary>
    /// 窗体第一次显示前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmEditWaybill_Load(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初始化明细表的GridView数据显示
    /// </summary>
    protected override async Task InitDetailGrid()
    {
        #region 列初始化

        gridView1.CreateColumn(Waybills.FieldCargoName, "货物名称", 100).CreateTextEdit();
        gridView1.CreateColumn(Waybills.FieldPackageType, "包装类型", 100).CreateComboBoxEdit().BindDictItems("包装方式");
        gridView1.CreateColumn(Waybills.FieldCargoUnit, "货物单位", 100).CreateTextEdit();
        gridView1.CreateColumn(Waybills.FieldQty, "件数", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldWeight, "重量", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldCubage, "体积", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldPrice, "单价", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldPriceType, "报价类型", 100).CreateComboBoxEdit().BindDictItems("计价方式");
        gridView1.CreateColumn(Waybills.FieldAmountInsured, "保价金额", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldCarriageCharge, "运费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldDeliveryCharge, "送货费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldPremiumCharge, "保险费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldUnloadingCharge, "装卸费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldUpstairsCharge, "上楼费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldWaitNoticeCharge, "通知费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldAckRecCharge, "回单费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldInformationCharge, "信息费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldPackageCharge, "包装费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldPickupCharge, "提货费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldTransferCharge, "中转费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldOtherCharge, "其他费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldAgencyReceiveCharge, "代收款", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldAgencyReceiveChargePoundage, "代收款手续费", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldBrokerage, "回扣", 100).CreateSpinEdit();
        gridView1.CreateColumn(Waybills.FieldPremiumRate, "保险费率", 100).CreateSpinEdit();
                        
        gridView1.ViewCaption = @"运单管理明细";

        #endregion

        await base.InitDetailGrid();
    }

    #region GridView事件

    /// <summary>
    /// 初始化新行
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected override void gridView1_InitNewRow(object s, InitNewRowEventArgs e)
    {
        base.gridView1_InitNewRow(s, e);
        // 此处加入新增列的数据初始化
        // gridView1.SetRowCellValue(e.RowHandle, "ISID", Guid.NewGuid().ToString()); //明细表ID
                //gridView1.SetRowCellValue(e.RowHandle, "Apply_ID", tempInfo.Apply_ID);
        //gridView1.SetRowCellValue(e.RowHandle, "OccurTime", DateTime.Now);
    }

    /// <summary>
    /// 行数据校验
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
    {
        base.gridView1_ValidateRow(sender, e);
                                                    }

    /// <summary>
    /// 自定义行绘制指示器
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected override void gridView1_CustomDrawRowIndicator(object s, RowIndicatorCustomDrawEventArgs e)
    {
        base.gridView1_CustomDrawRowIndicator(s, e);
                                    }

    /// <summary>
    /// 定义单元格样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
    {
        base.gridView1_RowCellStyle(sender, e);
                                                                                            }

    /// <summary>
    /// 自定义列的显示文本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
    {
        base.gridView1_CustomColumnDisplayText(sender, e);
                                            }

    /// <summary>
    /// 自定义列按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void repositoryBtn_ButtonClick(object sender, ButtonPressedEventArgs e)
    {
        base.repositoryBtn_ButtonClick(sender, e);
                                    }

    #endregion

    /// <summary>
    /// 初始化数据字典
    /// </summary>
    protected override Task InitDictItem()
    {
        //初始化代码
        txtFromNode.BindDictItems(GB.AllOuDict, "*当前机构*", false, false);
        txtFromAreaId.BindCityItems(true);
        txtToAreaId.BindCityItems(true);
        txtTransportType.BindDictItems("运输类型", null, true, false);
        txtPickUpType.BindDictItems("收货类型", null, true, false);
        txtDeliveryType.BindDictItems("交货方式", null, false, false);
        txtPaymentType.BindDictItems("付款方式", null, false, false);
        txtWaitNoticeYN.BindDictItems("是,否", false);
        txtAbnormityType.BindDictItems("是,否", false);
        txtStatusNode.BindDictItems(GB.AllOuDict, null, false, false);
        txtStatusId.BindDictItems("运单状态", null, false, false);
        txtUnloadYN.BindDictItems("是,否", false);
        txtUpstairYN.BindDictItems("是,否", false);
        txtSalesMan.BindDictItems(GB.AllUserDict, null, true, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, "*当前用户*", false, false);
        txtFlagApp.BindDictItems("已审核,未审核", false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);
        txtAutoNoYN.BindDictItems("是,否", false);
        txtAutoReceipt.BindDictItems("是,否", false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtWaybillNo.Tag = Waybill.FieldWaybillNo;
        txtWaybillDate.Tag = Waybill.FieldWaybillDate;
        txtTransferInNo.Tag = Waybill.FieldTransferInNo;
        txtOrderNo.Tag = Waybill.FieldOrderNo;
        txtFromNode.Tag = Waybill.FieldFromNode;
        txtFromNodes.Tag = Waybill.FieldFromNodes;
        txtFromProvinceId.Tag = Waybill.FieldFromProvinceId;
        txtFromCityId.Tag = Waybill.FieldFromCityId;
        txtFromAreaId.Tag = Waybill.FieldFromAreaId;
        txtToNode.Tag = Waybill.FieldToNode;
        txtToNodes.Tag = Waybill.FieldToNodes;
        txtToProvinceId.Tag = Waybill.FieldToProvinceId;
        txtToCityId.Tag = Waybill.FieldToCityId;
        txtToAreaId.Tag = Waybill.FieldToAreaId;
        txtShipperCompanyName.Tag = Waybill.FieldShipperCompanyName;
        txtShipperAddress.Tag = Waybill.FieldShipperAddress;
        txtShipperTel.Tag = Waybill.FieldShipperTel;
        txtShipper.Tag = Waybill.FieldShipper;
        txtConsigneeCompanyName.Tag = Waybill.FieldConsigneeCompanyName;
        txtConsigneeAddress.Tag = Waybill.FieldConsigneeAddress;
        txtConsigneeTel.Tag = Waybill.FieldConsigneeTel;
        txtConsignee.Tag = Waybill.FieldConsignee;
        txtTransportType.Tag = Waybill.FieldTransportType;
        txtPickUpType.Tag = Waybill.FieldPickUpType;
        txtDeliveryType.Tag = Waybill.FieldDeliveryType;
        txtPaymentType.Tag = Waybill.FieldPaymentType;
        txtWaitNoticeYN.Tag = Waybill.FieldWaitNoticeYN;
        txtAckRecNo.Tag = Waybill.FieldAckRecNo;
        txtAbnormityType.Tag = Waybill.FieldAbnormityType;
        txtQty.Tag = Waybill.FieldQty;
        txtWeight.Tag = Waybill.FieldWeight;
        txtCubage.Tag = Waybill.FieldCubage;
        txtChargeableWeight.Tag = Waybill.FieldChargeableWeight;
        txtAgencyReceiveCharge.Tag = Waybill.FieldAgencyReceiveCharge;
        txtCarriagePrepaid.Tag = Waybill.FieldCarriagePrepaid;
        txtCarriageForward.Tag = Waybill.FieldCarriageForward;
        txtCarriageMonthly.Tag = Waybill.FieldCarriageMonthly;
        txtCarriageReceipt.Tag = Waybill.FieldCarriageReceipt;
        txtCarriageOwed.Tag = Waybill.FieldCarriageOwed;
        txtCarriageOther.Tag = Waybill.FieldCarriageOther;
        txtBrokerage.Tag = Waybill.FieldBrokerage;
        txtAmountInsured.Tag = Waybill.FieldAmountInsured;
        txtStatusNode.Tag = Waybill.FieldStatusNode;
        txtStatusId.Tag = Waybill.FieldStatusId;
        txtUnloadYN.Tag = Waybill.FieldUnloadYN;
        txtUpstairYN.Tag = Waybill.FieldUpstairYN;
        txtUpstairNum.Tag = Waybill.FieldUpstairNum;
        txtSalesMan.Tag = Waybill.FieldSalesMan;
        txtRemark.Tag = Waybill.FieldRemark;
        txtCreationDate.Tag = Waybill.FieldCreationDate;
        txtCreatedBy.Tag = Waybill.FieldCreatedBy;
        txtLastUpdateDate.Tag = Waybill.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = Waybill.FieldLastUpdatedBy;
        txtFlagApp.Tag = Waybill.FieldFlagApp;
        txtAppUser.Tag = Waybill.FieldAppUser;
        txtAppDate.Tag = Waybill.FieldAppDate;
        txtAutoNoYN.Tag = Waybill.FieldAutoNoYN;
        txtAutoReceipt.Tag = Waybill.FieldAutoReceipt;

        #endregion

        await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(WaybillInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "运单管理";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
