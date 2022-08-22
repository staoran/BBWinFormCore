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
/// 问题件
/// </summary>
public partial class FrmEditMessage : BaseEditForm<Message, MessageHttpService, Messages, MessagesHttpService>
{
    public FrmEditMessage(MessageHttpService bll, MessagesHttpService childBll, IValidator<Message> validator, IValidator<Messages> childValidator) : base(bll, childBll, validator, childValidator)
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
    protected override Task InitDetailGrid()
    {
        #region 列初始化

        gridView1.Columns.Clear();
        gridView1.CreateColumn(Messages.FieldMsgNo, "问题件编号", 100).CreateTextEdit();
        gridView1.CreateColumn(Messages.FieldDealStatus, "处理状态", 100).CreateComboBoxEdit().BindDictItems("问题件处理类型");
        gridView1.CreateColumn(Messages.FieldDealContent, "处理内容", 100).CreateTextEdit();
        gridView1.CreateColumn(Messages.FieldAttaPath, "附件地址", 100).CreateTextEdit();
        gridView1.CreateColumn(Messages.FieldCreationDate, "创建时间", 100).CreateDateEdit();
        gridView1.CreateColumn(Messages.FieldCreatedBy, "创建人", 100).CreateComboBoxEdit().BindDictItems(GB.AllUserDict);
        gridView1.CreateColumn(Messages.FieldLastUpdateDate, "修改时间", 100).CreateDateEdit();
        gridView1.CreateColumn(Messages.FieldLastUpdateBy, "修改人", 100).CreateComboBoxEdit().BindDictItems(GB.AllUserDict);

        gridView1.ViewCaption = @"问题件回复";
        groupControl1.Text = gridView1.ViewCaption;

        #endregion

        // 明细只读
        var permitDict = new Dictionary<string, int>
        {
            { Messages.FieldISID, 3 },
            { Messages.FieldMsgNo, 3 },
            { Messages.FieldDealStatus, 1 },
            { Messages.FieldDealContent, 1 },
            { Messages.FieldAttaPath, 1 },
            { Messages.FieldLastReadTime, 3 },
            { Messages.FieldLaseRealAccount, 3 },
            { Messages.FieldCreationDate, 1 },
            { Messages.FieldCreatedBy, 1 },
            { Messages.FieldLastUpdateDate, 1 },
            { Messages.FieldLastUpdateBy, 1 }
        };
        gridView1.SetColumnsPermit(permitDict);
        return Task.CompletedTask;
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
        txtMsgType.BindDictItems("问题件类型", "99", false, false);
        txtSendMsgNode.BindDictItems(GB.AllOuDict, null, false, false);
        txtRecvMsgNode.BindDictItems(GB.AllOuDict, null, false, false);
        txtDealStatus.BindDictItems("问题件处理类型", "0", false, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
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

        txtMsgNo.Tag = Message.FieldMsgNo;
        txtMsgType.Tag = Message.FieldMsgType;
        txtWaybillNo.Tag = Message.FieldWaybillNo;
        txtSendMsgNode.Tag = Message.FieldSendMsgNode;
        txtSendMsgContent.Tag = Message.FieldSendMsgContent;
        txtRecvMsgNode.Tag = Message.FieldRecvMsgNode;
        txtDealStatus.Tag = Message.FieldDealStatus;
        txtAttaPath.Tag = Message.FieldAttaPath;
        txtCreationDate.Tag = Message.FieldCreationDate;
        txtCreatedBy.Tag = Message.FieldCreatedBy;
        txtLastUpdateDate.Tag = Message.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = Message.FieldLastUpdatedBy;
        txtFlagApp.Tag = Message.FieldFlagApp;
        txtAppUser.Tag = Message.FieldAppUser;
        txtAppDate.Tag = Message.FieldAppDate;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(MessageInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "问题件";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
