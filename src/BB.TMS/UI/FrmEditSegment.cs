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
/// 线路管理
/// </summary>
#if DESIGNER
public partial class FrmEditSegment : BaseEditDesigner
{
    protected DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    protected DevExpress.XtraLayout.LayoutControl layoutControl1;
#else
public partial class FrmEditSegment : BaseEditForm<Segment, SegmentHttpService,  Segments, SegmentsHttpService>
{
#endif
    public FrmEditSegment(SegmentHttpService bll, SegmentsHttpService childBll, IValidator<Segment> validator, IValidator<Segments> childValidator) : base(bll, childBll, validator, childValidator)
    {
        InitializeComponent();

        Load += FrmEditTest1Car_Load;
    }

    /// <summary>
    /// 窗体第一次显示前
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmEditTest1Car_Load(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初始化明细表的GridView数据显示
    /// </summary>
    protected override async Task InitDetailGrid()
    {
        #region 列初始化

        gridView1.CreateColumn(Segments.FieldSegmentNo, "线路编号", 100).Visible = false;
        gridView1.CreateColumn(Segments.FieldCostType, "费用类型", 100).CreateComboBoxEdit().BindDictItems(GB.AllCostType);
        gridView1.CreateColumn(Segments.FieldQuotationNo, "报价编号", 100).CreateSearchLookUpEdit();
        gridView1.CreateColumn(Segments.FieldPayNodeType, "付款网点类型", 100).CreateComboBoxEdit().BindDictItems("收付部门类型");
        gridView1.CreateColumn(Segments.FieldPayNodeNo, "付款网点", 100).CreateTextEdit();
        gridView1.CreateColumn(Segments.FieldRecvNodeType, "收款网点类型", 100).CreateComboBoxEdit().BindDictItems("收付部门类型");
        gridView1.CreateColumn(Segments.FieldRecvNodeNo, "收款网点", 100).CreateTextEdit();
        gridView1.CreateColumn(Segments.FieldOpenTime, "启用时间", 100).CreateDateEdit();
        gridView1.CreateColumn(Segments.FieldClosetime, "终止时间", 100).CreateDateEdit();
        gridView1.CreateColumn(Segments.FieldRemark, "备注", 100).CreateMemoEdit();
        gridView1.CreateColumn(Segments.FieldCreationDate, "创建时间", 100).Visible = false;
        gridView1.CreateColumn(Segments.FieldCreatedBy, "创建人", 100).Visible = false;
        gridView1.CreateColumn(Segments.FieldLastUpdateDate, "修改时间", 100).Visible = false;
        gridView1.CreateColumn(Segments.FieldLastUpdatedBy, "修改人", 100).Visible = false;
        gridView1.CreateColumn(Segments.FieldFinancialCenterType, "财务中心类型", 100).CreateComboBoxEdit().BindDictItems("收付财务中心类型");
        gridView1.CreateColumn(Segments.FieldFinancialCenter, "财务中心", 100).CreateTextEdit();

        gridView1.ViewCaption = @"线路管理明细";

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
        txtSegmentType.BindDictItems("线路类型", "1", false, false);
        txtSegmentBeginNode.BindDictItems(GB.AllOuDict, null, false, false);
        txtSegmentEndNode.BindDictItems(GB.AllOuDict, null, false, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
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

        txtSegmentNo.Tag = Segment.FieldSegmentNo;
        txtSegmentType.Tag = Segment.FieldSegmentType;
        txtSegmentName.Tag = Segment.FieldSegmentName;
        txtSegmentBeginNode.Tag = Segment.FieldSegmentBeginNode;
        txtSegmentEndNode.Tag = Segment.FieldSegmentEndNode;
        txtPlanBeginTime.Tag = Segment.FieldPlanBeginTime;
        txtExpectedHour.Tag = Segment.FieldExpectedHour;
        txtExpectedDistance.Tag = Segment.FieldExpectedDistance;
        txtExpectedOilWear.Tag = Segment.FieldExpectedOilWear;
        txtExpectedCharge.Tag = Segment.FieldExpectedCharge;
        txtExpectedPontage.Tag = Segment.FieldExpectedPontage;
        txtRemark.Tag = Segment.FieldRemark;
        txtCreationDate.Tag = Segment.FieldCreationDate;
        txtCreatedBy.Tag = Segment.FieldCreatedBy;
        txtLastUpdateDate.Tag = Segment.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = Segment.FieldLastUpdatedBy;
        txtFlagApp.Tag = Segment.FieldFlagApp;
        txtAppUser.Tag = Segment.FieldAppUser;
        txtAppDate.Tag = Segment.FieldAppDate;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(SegmentInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "线路管理";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
