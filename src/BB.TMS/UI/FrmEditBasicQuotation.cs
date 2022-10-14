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
/// 公式报价
/// </summary>
public partial class FrmEditBasicQuotation : BaseEditForm<BasicQuotation, BasicQuotationHttpService, BasicQuotations, BasicQuotationsHttpService>
{
    public FrmEditBasicQuotation(BasicQuotationHttpService bll, BasicQuotationsHttpService childBll, IValidator<BasicQuotation> validator, IValidator<BasicQuotations> childValidator) : base(bll, childBll, validator, childValidator)
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

        gridView1.CreateColumn(BasicQuotations.FieldQuotationNo, "报价编号", 100).Visible = false;
        gridView1.CreateColumn(BasicQuotations.FieldMathConditional, "条件范围", 100).CreateTextEdit();
        gridView1.CreateColumn(BasicQuotations.FieldMathContent, "条件公式", 100).CreateTextEdit();
        gridView1.CreateColumn(BasicQuotations.FieldRemark, "备注", 100).CreateTextEdit();
        gridView1.CreateColumn(BasicQuotations.FieldCreationDate, "创建时间", 100).Visible = false;
        gridView1.CreateColumn(BasicQuotations.FieldCreatedBy, "创建人", 100).Visible = false;
        gridView1.CreateColumn(BasicQuotations.FieldLastUpdateDate, "修改时间", 100).Visible = false;
        gridView1.CreateColumn(BasicQuotations.FieldLastUpdatedBy, "修改人", 100).Visible = false;

        gridView1.ViewCaption = @"公式报价明细";

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
        txtTranNodeNO.BindDictItems(GB.AllOuDict, LoginUserInfo.CompanyId, false, false);
        txtCostType.BindDictItems(GB.AllCostType, null, false, false);
        txtCargoType.BindDictItems("货物名称", null, true, false);
        txtPickUpType.BindDictItems("收货类型", null, true, false);
        txtDeliveryType.BindDictItems("交货方式", null, true, false);
        txtTransportType.BindDictItems("运输类型", null, true, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
        txtFlagApp.BindDictItems("已审核,未审核", false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);
        txtRakeMarkYN.BindDictItems("是,否", false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtQuotationNo.Tag = BasicQuotation.FieldQuotationNo;
        txtQuotationDesc.Tag = BasicQuotation.FieldQuotationDesc;
        txtTranNodeNO.Tag = BasicQuotation.FieldTranNodeNO;
        txtCostType.Tag = BasicQuotation.FieldCostType;
        txtCargoType.Tag = BasicQuotation.FieldCargoType;
        txtPickUpType.Tag = BasicQuotation.FieldPickUpType;
        txtDeliveryType.Tag = BasicQuotation.FieldDeliveryType;
        txtTransportType.Tag = BasicQuotation.FieldTransportType;
        txtFroms.Tag = BasicQuotation.FieldFroms;
        txtTos.Tag = BasicQuotation.FieldTos;
        txtBeginTime.Tag = BasicQuotation.FieldBeginTime;
        txtEndTime.Tag = BasicQuotation.FieldEndTime;
        txtRemark.Tag = BasicQuotation.FieldRemark;
        txtCreationDate.Tag = BasicQuotation.FieldCreationDate;
        txtCreatedBy.Tag = BasicQuotation.FieldCreatedBy;
        txtLastUpdateDate.Tag = BasicQuotation.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = BasicQuotation.FieldLastUpdatedBy;
        txtFlagApp.Tag = BasicQuotation.FieldFlagApp;
        txtAppUser.Tag = BasicQuotation.FieldAppUser;
        txtAppDate.Tag = BasicQuotation.FieldAppDate;
        txtRakeMarkYN.Tag = BasicQuotation.FieldRakeMarkYN;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(BasicQuotationInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "公式报价";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
