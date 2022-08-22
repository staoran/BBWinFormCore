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
/// 普通报价
/// </summary>
// public partial class FrmEditQuotation : BaseEditDesigner
public partial class FrmEditQuotation : BaseEditForm<Quotation, QuotationHttpService, Quotations, QuotationsHttpService>
{
    public FrmEditQuotation(QuotationHttpService bll, QuotationsHttpService childBll, IValidator<Quotation> validator, IValidator<Quotations> childValidator) : base(bll, childBll, validator, childValidator)
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
        gridView1.CreateColumn(Quotations.FieldQuotationNo, "报价编号", 100).Visible = false;
        gridView1.CreateColumn(Quotations.FieldQuotationType, "报价类型", 100).CreateComboBoxEdit().BindDictItems("计价方式");
        gridView1.CreateColumn(Quotations.FieldFromGroups, "起始组", 100).CreateTextEdit();
        gridView1.CreateColumn(Quotations.FieldToGroups, "到达组", 100).CreateTextEdit();
        gridView1.CreateColumn(Quotations.FieldMinCost, "最小金额", 100).CreateSpinEdit();
        gridView1.CreateColumn(Quotations.FieldMaxCost, "最大金额", 100).CreateSpinEdit();
        gridView1.CreateColumn(Quotations.FieldFirstCost, "首值金额", 100).CreateSpinEdit();
        gridView1.CreateColumn(Quotations.FieldFirstValue, "首值", 100).CreateSpinEdit();
        gridView1.CreateColumn(Quotations.FieldMinValue, "最小值", 100).CreateSpinEdit();
        gridView1.CreateColumn(Quotations.FieldMaxValue, "最大值", 100).CreateSpinEdit();
        gridView1.CreateColumn(Quotations.FieldUnitPrice, "单价", 100).CreateSpinEdit();
        gridView1.CreateColumn(Quotations.FieldUnitPricePer, "单价加成", 100).CreateSpinEdit();
        gridView1.CreateColumn(Quotations.FieldMathConditional, "条件范围", 100).Visible = false;
        gridView1.CreateColumn(Quotations.FieldMathContent, "条件公式", 100).Visible = false;
        gridView1.CreateColumn(Quotations.FieldRemark, "备注", 100).CreateTextEdit();
        gridView1.CreateColumn(Quotations.FieldCreationDate, "创建时间", 100).Visible = false;
        gridView1.CreateColumn(Quotations.FieldCreatedBy, "创建人", 100).Visible = false;
        gridView1.CreateColumn(Quotations.FieldLastUpdateDate, "修改时间", 100).Visible = false;
        gridView1.CreateColumn(Quotations.FieldLastUpdatedBy, "修改人", 100).Visible = false;

        gridView1.ViewCaption = @"普通报价明细";

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
        txtCostType.BindDictItems(GB.AllCostBillType, null, false, false);
        txtCargoTypePerYN.BindDictItems("是,否", false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
        txtFlagApp.BindDictItems("已审核,未审核", false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);
        txtTranNodeNO.BindDictItems(GB.AllOuDict, LoginUserInfo.CompanyId, false, false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtQuotationNo.Tag = Quotation.FieldQuotationNo;
        txtQuotationDesc.Tag = Quotation.FieldQuotationDesc;
        txtCostType.Tag = Quotation.FieldCostType;
        txtCargoTypePerYN.Tag = Quotation.FieldCargoTypePerYN;
        txtRemark.Tag = Quotation.FieldRemark;
        txtCreationDate.Tag = Quotation.FieldCreationDate;
        txtCreatedBy.Tag = Quotation.FieldCreatedBy;
        txtLastUpdateDate.Tag = Quotation.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = Quotation.FieldLastUpdatedBy;
        txtFlagApp.Tag = Quotation.FieldFlagApp;
        txtAppUser.Tag = Quotation.FieldAppUser;
        txtAppDate.Tag = Quotation.FieldAppDate;
        txtTranNodeNO.Tag = Quotation.FieldTranNodeNO;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(QuotationInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "普通报价";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
