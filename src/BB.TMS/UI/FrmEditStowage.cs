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
/// 送货配载
/// </summary>
#if DESIGNER
public partial class FrmEditStowage : BaseEditDesigner
{
    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
#else
public partial class FrmEditStowage : BaseEditForm<Stowage, StowageHttpService, Stowages, StowagesHttpService>
{
#endif
    public FrmEditStowage(StowageHttpService bll, StowagesHttpService childBll, IValidator<Stowage> validator, IValidator<Stowages> childValidator) : base(bll, childBll, validator, childValidator)
    {
        InitializeComponent();

        Load += FrmEditStowage_Load;
    }

    /// <summary>
    /// 窗体第一次显示前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmEditStowage_Load(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初始化明细表的GridView数据显示
    /// </summary>
    protected override async Task InitDetailGrid()
    {
        #region 列初始化

        gridView1.CreateColumn(Stowages.FieldStowageNo, "配载编号", 100).CreateTextEdit();
        gridView1.CreateColumn(Stowages.FieldInputType, "导入类型", 100).CreateComboBoxEdit().BindDictItems("配载导入类型");
        gridView1.CreateColumn(Stowages.FieldWaybillNo, "运单编号", 100).CreateTextEdit();
        gridView1.CreateColumn(Stowages.FieldFromNode, "发货网点", 100).CreateTextEdit();
        gridView1.CreateColumn(Stowages.FieldFromNodes, "发货区域", 100).CreateTextEdit();
        gridView1.CreateColumn(Stowages.FieldToNode, "目的网点", 100).CreateTextEdit();
        gridView1.CreateColumn(Stowages.FieldToNodes, "目的区域", 100).CreateTextEdit();
        gridView1.CreateColumn(Stowages.FieldConsigneeCompanyName, "收货公司", 100).CreateTextEdit();
        gridView1.CreateColumn(Stowages.FieldConsigneeAddress, "收货地址", 100).CreateMemoEdit();
        gridView1.CreateColumn(Stowages.FieldConsigneeTel, "收货电话", 100).CreateTextEdit();
        gridView1.CreateColumn(Stowages.FieldConsignee, "收货人", 100).CreateTextEdit();
        gridView1.CreateColumn(Stowages.FieldDeliveryType, "交货方式", 100).CreateComboBoxEdit().BindDictItems("交货方式");
        gridView1.CreateColumn(Stowages.FieldPaymentType, "付款方式", 100).CreateComboBoxEdit().BindDictItems("付款方式");
        gridView1.CreateColumn(Stowages.FieldAckRecQty, "回单数量", 100).CreateSpinEdit();
        gridView1.CreateColumn(Stowages.FieldAckRecType, "回单类型", 100).CreateTextEdit();
        gridView1.CreateColumn(Stowages.FieldAckRecNo, "回单号", 100).CreateTextEdit();
        gridView1.CreateColumn(Stowages.FieldQty, "数量", 100).CreateSpinEdit();
        gridView1.CreateColumn(Stowages.FieldWeight, "重量", 100).CreateSpinEdit();
        gridView1.CreateColumn(Stowages.FieldCubage, "体积", 100).CreateSpinEdit();
        gridView1.CreateColumn(Stowages.FieldUnloadYN, "是否卸货", 100).CreateToggleSwitch();
        gridView1.CreateColumn(Stowages.FieldUpstairYN, "是否上楼", 100).CreateToggleSwitch();
        gridView1.CreateColumn(Stowages.FieldUpstairNum, "楼层", 100).CreateSpinEdit();
        gridView1.CreateColumn(Stowages.FieldSmsYN, "是否短信", 100).CreateToggleSwitch();
        gridView1.CreateColumn(Stowages.FieldStowageCarriage, "分摊费用", 100).CreateSpinEdit();
        gridView1.CreateColumn(Stowages.FieldStatusID, "状态", 100).CreateComboBoxEdit();
        gridView1.CreateColumn(Stowages.FieldRemark, "备注", 100).CreateTextEdit();
                        
        gridView1.ViewCaption = @"送货配载明细";

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
        txtTranNodeNO.BindDictItems(GB.AllOuDict, GB.LoginUserInfo.CompanyId, false, false);
        txtStowageType.BindDictItems("配载类型", "01", false, false);
        txtTransType.BindDictItems("配载运输类型", "01", false, false);
        txtCheckInAccount.BindDictItems(GB.AllUserDict, null, true, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        txtCheckInYN.BindDictItems("已核销,未核销", false);
        txtFlagApp.BindDictItems("已审核,未审核", false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtStowageNo.Tag = Stowage.FieldStowageNo;
        txtTranNodeNO.Tag = Stowage.FieldTranNodeNO;
        txtStowageType.Tag = Stowage.FieldStowageType;
        txtTransType.Tag = Stowage.FieldTransType;
        txtTransMarkNo.Tag = Stowage.FieldTransMarkNo;
        txtTransNo.Tag = Stowage.FieldTransNo;
        txtTransDriver.Tag = Stowage.FieldTransDriver;
        txtTransDriverPhone.Tag = Stowage.FieldTransDriverPhone;
        txtTransDate.Tag = Stowage.FieldTransDate;
        txtTotalQty.Tag = Stowage.FieldTotalQty;
        txtTotalWeight.Tag = Stowage.FieldTotalWeight;
        txtTotalCubage.Tag = Stowage.FieldTotalCubage;
        txtTotalCarriage.Tag = Stowage.FieldTotalCarriage;
        txtTransCarriage.Tag = Stowage.FieldTransCarriage;
        txtCheckInYN.Tag = Stowage.FieldCheckInYN;
        txtCheckInAccount.Tag = Stowage.FieldCheckInAccount;
        txtCheckInDate.Tag = Stowage.FieldCheckInDate;
        txtRemark.Tag = Stowage.FieldRemark;
        txtCreationDate.Tag = Stowage.FieldCreationDate;
        txtCreatedBy.Tag = Stowage.FieldCreatedBy;
        txtLastUpdateDate.Tag = Stowage.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = Stowage.FieldLastUpdatedBy;
        txtFlagApp.Tag = Stowage.FieldFlagApp;
        txtAppUser.Tag = Stowage.FieldAppUser;
        txtAppDate.Tag = Stowage.FieldAppDate;
        txtIncome.Tag = Stowage.FieldIncome;
        txtSharType.Tag = Stowage.FieldSharType;

        #endregion

        await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(StowageInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "送货配载";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
