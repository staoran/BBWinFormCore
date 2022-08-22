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
/// 网点资料
/// </summary>
// public partial class FrmEditNode : BaseEditDesigner
public partial class FrmEditNode : BaseEditForm<Node, NodeHttpService,  Nodes, NodesHttpService>
{
    public FrmEditNode(NodeHttpService bll, NodesHttpService childBll, IValidator<Node> validator, IValidator<Nodes> childValidator) : base(bll, childBll, validator, childValidator)
    {
        InitializeComponent();

        Load += FrmEditTest1Car_Load;
    }

    /// <summary>
    /// 窗体第一次显示前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmEditTest1Car_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初始化明细表的GridView数据显示
    /// </summary>
    protected override async Task InitDetailGrid()
    {
        #region 列初始化

        gridView1.Columns.Clear();
        gridView1.CreateColumn("Operate", "操作", 60).CreateButtonEdit(repositoryBtn_ButtonClick);
        gridView1.CreateColumn(Nodes.FieldTranNodeNO, "网点ID", 100).Visible = false;
        gridView1.CreateColumn(Nodes.FieldTranNodeAreaName, "区域名称", 100).CreateTextEdit();
        gridView1.CreateColumn(Nodes.FieldTranNodeAreaDesc, "区域详情", 100).CreateTextEdit();
        gridView1.CreateColumn(Nodes.FieldDeliveryType, "交货方式", 100).CreateComboBoxEdit().BindDictItems("交货方式");
        gridView1.CreateColumn(Nodes.FieldEFence, "电子围栏", 100).CreateTextEdit();
        gridView1.CreateColumn(Nodes.FieldCenterCoordinate, "中心坐标", 100).CreateTextEdit();
        gridView1.CreateColumn(Nodes.FieldConvertVK, "重泡比", 100).CreateSpinEdit();
        gridView1.CreateColumn(Nodes.FieldAreaId, "区", 100).CreateSearchLookUpEdit().BindCityItems();
        gridView1.CreateColumn(Nodes.FieldAddress, "地址", 100).CreateTextEdit();
        gridView1.CreateColumn(Nodes.FieldPerson, "负责人", 100).CreateTextEdit();
        gridView1.CreateColumn(Nodes.FieldPhone, "联系方式", 100).CreateTextEdit();
        gridView1.CreateColumn(Nodes.FieldSignLimitHour, "到件签收时效", 100).CreateSpinEdit();
        gridView1.CreateColumn(Nodes.FieldCancelYN, "作废", 100).CreateToggleSwitch();
        gridView1.CreateColumn(Nodes.FieldRemark, "备注", 100).CreateTextEdit();
        gridView1.CreateColumn(Nodes.FieldCreationDate, "创建时间", 100).Visible = false;
        gridView1.CreateColumn(Nodes.FieldCreatedBy, "创建人", 100).Visible = false;
        gridView1.CreateColumn(Nodes.FieldLastUpdateDate, "更新时间", 100).Visible = false;
        gridView1.CreateColumn(Nodes.FieldLastUpdatedBy, "更新人", 100).Visible = false;

        gridView1.ViewCaption = @"网点资料明细";

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
        txtTranNodeCostNo.BindDictItems(GB.AllOuDict, null, false, false);
        txtTranNodeType.BindDictItems("网点类型", "9", false, false);
        txtParentNo.BindDictItems(GB.AllOuDict, null, false, false);
        txtLockLimit.BindDictItems("是,否", false);
        txtSendSMS.BindDictItems("是,否", false);
        txtISLocked.BindDictItems("是,否", false);
        txtAckRec.BindDictItems("是,否", false);
        txtAreaNo.BindCityItems(true);
        txtCreatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
        txtTranNodeStatus.BindDictItems("网点状态类型", "1", false, false);
        txtPublicYN.BindDictItems("是,否", false);
        txtFlagApp.BindDictItems("已审核,未审核", false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);
        txtCostMasterYN.BindDictItems("是,否", false);
        txtDispatchOnly.BindDictItems("是,否", false);
        txtIsLockLimitKPI.BindDictItems("是,否", false);
        txtFinancialCenter.BindDictItems(GB.AllOuDict, null, false, false);
        txtWhiteList.BindDictItems(GB.AllOuDict);
        txtBlackList.BindDictItems(GB.AllOuDict);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtTranNodeNO.Tag = Node.FieldTranNodeNO;
        txtTranNodeCostNo.Tag = Node.FieldTranNodeCostNo;
        txtTranNodeName.Tag = Node.FieldTranNodeName;
        txtTranNodeType.Tag = Node.FieldTranNodeType;
        txtTranNodeBeginDate.Tag = Node.FieldTranNodeBeginDate;
        txtTranNodeEndDate.Tag = Node.FieldTranNodeEndDate;
        txtParentNo.Tag = Node.FieldParentNo;
        txtTranNodePerson.Tag = Node.FieldTranNodePerson;
        txtTranNodePersonID.Tag = Node.FieldTranNodePersonID;
        txtTranNodeMobile.Tag = Node.FieldTranNodeMobile;
        txtTranNodeAddress.Tag = Node.FieldTranNodeAddress;
        txtLockLimit.Tag = Node.FieldLockLimit;
        txtLockLimitAmt.Tag = Node.FieldLockLimitAmt;
        txtWarningLimitAmt.Tag = Node.FieldWarningLimitAmt;
        txtSendSMS.Tag = Node.FieldSendSMS;
        txtISLocked.Tag = Node.FieldISLocked;
        txtAckRec.Tag = Node.FieldAckRec;
        txtAgencyRecLimitAmt.Tag = Node.FieldAgencyRecLimitAmt;
        // txtAgencyRecLimitAmtBKP.Tag = Node.FieldAgencyRecLimitAmtBKP;
        txtCarriageForwardLimitAmt.Tag = Node.FieldCarriageForwardLimitAmt;
        // txtCarriageForwardLimitAmtBKP.Tag = Node.FieldCarriageForwardLimitAmtBKP;
        txtAreaNo.Tag = Node.FieldAreaNo;
        txtInTime.Tag = Node.FieldInTime;
        txtOutTime.Tag = Node.FieldOutTime;
        txtRemark.Tag = Node.FieldRemark;
        txtCreationDate.Tag = Node.FieldCreationDate;
        txtCreatedBy.Tag = Node.FieldCreatedBy;
        txtLastUpdateDate.Tag = Node.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = Node.FieldLastUpdatedBy;
        txtTranNodeStatus.Tag = Node.FieldTranNodeStatus;
        txtPublicYN.Tag = Node.FieldPublicYN;
        txtFlagApp.Tag = Node.FieldFlagApp;
        txtAppUser.Tag = Node.FieldAppUser;
        txtAppDate.Tag = Node.FieldAppDate;
        txtSignLoopEndTime.Tag = Node.FieldSignLoopEndTime;
        txtSignLimitTime.Tag = Node.FieldSignLimitTime;
        txtSignDays.Tag = Node.FieldSignDays;
        txtAckRecDays.Tag = Node.FieldAckRecDays;
        txtCostMasterYN.Tag = Node.FieldCostMasterYN;
        txtManagementFee.Tag = Node.FieldManagementFee;
        txtUsageFee.Tag = Node.FieldUsageFee;
        txtDeposit.Tag = Node.FieldDeposit;
        txtContractNote.Tag = Node.FieldContractNote;
        txtDispatchOnly.Tag = Node.FieldDispatchOnly;
        txtPickupWeightLimit.Tag = Node.FieldPickupWeightLimit;
        txtPickupVolumeLimit.Tag = Node.FieldPickupVolumeLimit;
        txtTranNodeAxes.Tag = Node.FieldTranNodeAxes;
        txtIsLockLimitKPI.Tag = Node.FieldIsLockLimitKPI;
        txtFinancialCenter.Tag = Node.FieldFinancialCenter;
        txtWhiteList.Tag = Node.FieldWhiteList;
        txtBlackList.Tag = Node.FieldBlackList;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(NodeInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "网点资料";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
