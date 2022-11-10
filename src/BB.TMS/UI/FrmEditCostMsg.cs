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
/// 费用调整
/// </summary>
public partial class FrmEditCostMsg : BaseEditForm<CostMsg, CostMsgHttpService, CostMsgs, CostMsgsHttpService>
{
    public FrmEditCostMsg(CostMsgHttpService bll, CostMsgsHttpService childBll, IValidator<CostMsg> validator, IValidator<CostMsgs> childValidator) : base(bll, childBll, validator, childValidator)
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

        gridView1.CreateColumn(CostMsgs.FieldCostMsgNo, "费用调整编号", 100).CreateTextEdit();
        gridView1.CreateColumn(CostMsgs.FieldStatusID, "单据状态", 100).CreateComboBoxEdit().BindDictItems("费用调整状态");
        gridView1.CreateColumn(CostMsgs.FieldRecvMsgNode, "回复网点", 100).CreateComboBoxEdit().BindDictItems(GB.AllOuDict);
        gridView1.CreateColumn(CostMsgs.FieldRecvMsgContent, "回复内容", 100).CreateTextEdit();
        gridView1.CreateColumn(CostMsgs.FieldAttaPath, "附件", 100).CreateMemoEdit();
        gridView1.CreateColumn(CostMsgs.FieldCreationDate, "创建时间", 100).CreateDateEdit();
        gridView1.CreateColumn(CostMsgs.FieldCreatedBy, "创建人", 100).CreateComboBoxEdit().BindDictItems(GB.AllUserDict);
        gridView1.CreateColumn(CostMsgs.FieldLastUpdateDate, "修改时间", 100).CreateDateEdit();
        gridView1.CreateColumn(CostMsgs.FieldLastUpdatedBy, "修改人", 100).CreateComboBoxEdit().BindDictItems(GB.AllUserDict);

        gridView1.ViewCaption = @"费用调整明细";

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
        txtSourceType.BindDictItems("费用调整来源", "1", false, false);
        txtSendMsgNode.BindDictItems(GB.AllOuDict, GB.LoginUserInfo.CompanyId, false, false);
        txtRecvMsgType.BindDictItems("费用接受类型", "1", false, false);
        txtRecvMsgAccount.BindDictItems(GB.AllOuDict, null, false, false);
        txtValueType.BindDictItems("费用调整类型", "1", false, false);
        txtStatusID.BindDictItems("费用调整状态", "0", false, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), false, false);
        txtFlagApp.BindDictItems("已审核,未审核", false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);
        txtFinancialCenter.BindDictItems(GB.AllOuDict, null, false, false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override async Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtCostMsgNo.Tag = CostMsg.FieldCostMsgNo;
        txtSourceType.Tag = CostMsg.FieldSourceType;
        txtWaybillNo.Tag = CostMsg.FieldWaybillNo;
        txtSendMsgNode.Tag = CostMsg.FieldSendMsgNode;
        txtSendMsgContent.Tag = CostMsg.FieldSendMsgContent;
        txtAttaPath.Tag = CostMsg.FieldAttaPath;
        txtRecvMsgType.Tag = CostMsg.FieldRecvMsgType;
        txtRecvMsgAccount.Tag = CostMsg.FieldRecvMsgAccount;
        txtValueType.Tag = CostMsg.FieldValueType;
        txtSourceValue.Tag = CostMsg.FieldSourceValue;
        txtActiveValue.Tag = CostMsg.FieldActiveValue;
        txtStatusID.Tag = CostMsg.FieldStatusID;
        txtCreationDate.Tag = CostMsg.FieldCreationDate;
        txtCreatedBy.Tag = CostMsg.FieldCreatedBy;
        txtLastUpdateDate.Tag = CostMsg.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = CostMsg.FieldLastUpdatedBy;
        txtFlagApp.Tag = CostMsg.FieldFlagApp;
        txtAppUser.Tag = CostMsg.FieldAppUser;
        txtAppDate.Tag = CostMsg.FieldAppDate;
        txtFinancialCenter.Tag = CostMsg.FieldFinancialCenter;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(CostMsgInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "费用调整";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
