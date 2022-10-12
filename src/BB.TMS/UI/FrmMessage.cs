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
using BB.Tools.Format;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using DevExpress.XtraBars;
using Furion;
using Furion.Logging.Extensions;
using Message = BB.Entity.TMS.Message;

namespace BB.TMS.UI;

/// <summary>
/// 问题件
/// </summary>
public partial class FrmMessage : BaseViewDock<Message, MessageHttpService, FrmEditMessage, Messages, MessagesHttpService>
{
    public FrmMessage(MessageHttpService bll, MessagesHttpService childBll, LazilyResolved<FrmEditMessage> baseForm) : base(bll, childBll, baseForm)
    {
        moduleName = "问题件";

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

        addButton.Visibility = BarItemVisibility.Always;
        editButton.Visibility = BarItemVisibility.Always;
        checkButton.Visibility = BarItemVisibility.Always;
        importButton.Visibility = BarItemVisibility.Always;
        queryButton.Visibility = BarItemVisibility.Always;
        clearButton.Visibility = BarItemVisibility.Always;
        advQueryButton.Visibility = BarItemVisibility.Always;
        exportButton.Visibility = BarItemVisibility.Always;
        hideTreeButton.Visibility = BarItemVisibility.Always;
        
        bar1.AddBarButtonItem("Reply", "问题件回复", "add", btnReply_Click);

        #endregion
    }

    /// <summary>
    /// 窗体首次打开后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmMessage_Shown(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(Message.FieldMsgType, "问题件类型");
            x.AddNode(Message.FieldDealStatus, "问题件处理类型");
            x.AddNode(Message.FieldFlagApp, "审核", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtMsgType.BindDictItems("问题件类型", "99", true, false);
        txtSendMsgNode.BindDictItems(GB.AllOuDict, null, true, false);
        txtRecvMsgNode.BindDictItems(GB.AllOuDict, null, true, false);
        txtDealStatus.BindDictItems("问题件处理类型", "0", true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "MsgNo,MsgType,WaybillNo,SendMsgNode,SendMsgContent,RecvMsgNode,DealStatus,AttaPath,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate";

        #region 添加别名解析，两种方式均可

        // winGridViewPager1.AddColumnAlias(Message.FieldMsgNo, "问题件编号");
        // winGridViewPager1.AddColumnAlias(Message.FieldMsgType, "问题件类型");
        // winGridViewPager1.AddColumnAlias(Message.FieldWaybillNo, "运单号");
        // winGridViewPager1.AddColumnAlias(Message.FieldSendMsgNode, "发送方网点");
        // winGridViewPager1.AddColumnAlias(Message.FieldSendMsgContent, "问题件内容");
        // winGridViewPager1.AddColumnAlias(Message.FieldRecvMsgNode, "接收方网点");
        // winGridViewPager1.AddColumnAlias(Message.FieldDealStatus, "处理状态");
        // winGridViewPager1.AddColumnAlias(Message.FieldAttaPath, "附件地址");
        // winGridViewPager1.AddColumnAlias(Message.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(Message.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(Message.FieldLastUpdateDate, "修改时间");
        // winGridViewPager1.AddColumnAlias(Message.FieldLastUpdatedBy, "修改人");
        // winGridViewPager1.AddColumnAlias(Message.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(Message.FieldAppUser, "审核人");
        // winGridViewPager1.AddColumnAlias(Message.FieldAppDate, "审核时间");

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(Message.FieldMsgType, "问题件类型");
        winGridViewPager1.SetColumnDataSource(Message.FieldSendMsgNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Message.FieldRecvMsgNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Message.FieldDealStatus, "问题件处理类型");
        winGridViewPager1.SetColumnDataSource(Message.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Message.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Message.FieldAppUser, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行


        #endregion

        #endregion

        #region 明细表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridView2.DisplayColumns = "ISID,MsgNo,DealStatus,DealContent,AttaPath,CreationDate,CreatedBy,LastUpdateDate,LastUpdateBy";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridView2.AddColumnAlias(Messages.FieldISID, "自增ID");
        // winGridView2.AddColumnAlias(Messages.FieldMsgNo, "问题件编号");
        // winGridView2.AddColumnAlias(Messages.FieldDealStatus, "处理状态");
        // winGridView2.AddColumnAlias(Messages.FieldDealContent, "处理内容");
        // winGridView2.AddColumnAlias(Messages.FieldAttaPath, "附件地址");
        // winGridView2.AddColumnAlias(Messages.FieldCreationDate, "创建时间");
        // winGridView2.AddColumnAlias(Messages.FieldCreatedBy, "创建人");
        // winGridView2.AddColumnAlias(Messages.FieldLastUpdateDate, "修改时间");
        // winGridView2.AddColumnAlias(Messages.FieldLastUpdateBy, "修改人");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridView2.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridView2.SetColumnDataSource(Messages.FieldDealStatus, "问题件处理类型");
        winGridView2.SetColumnDataSource(Messages.FieldCreatedBy, GB.AllUserDict);
        winGridView2.SetColumnDataSource(Messages.FieldLastUpdateBy, GB.AllUserDict);

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
    protected override void gridView1_DataSourceChanged(object sender, EventArgs e)
    {
        base.gridView1_DataSourceChanged(sender, e);
        if (winGridViewPager1.gridView1.Columns.Count > 0 && winGridViewPager1.gridView1.RowCount > 0)
        {
            #region 单独设置列宽

            // 可特殊设置特别的宽度
            // GridView gridView = winGridViewPager1.gridView1;
            // if (gridView != null)
            // {
            //     gridView.SetGridColumWidth(Message.FieldMsgNo, 200);
            //     gridView.SetGridColumWidth(Message.FieldMsgType, 200);
            //     gridView.SetGridColumWidth(Message.FieldWaybillNo, 200);
            //     gridView.SetGridColumWidth(Message.FieldSendMsgNode, 200);
            //     gridView.SetGridColumWidth(Message.FieldSendMsgContent, 200);
            //     gridView.SetGridColumWidth(Message.FieldRecvMsgNode, 200);
            //     gridView.SetGridColumWidth(Message.FieldDealStatus, 200);
            //     gridView.SetGridColumWidth(Message.FieldAttaPath, 200);
            //     gridView.SetGridColumWidth(Message.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(Message.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(Message.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(Message.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(Message.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(Message.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(Message.FieldAppDate, 200);
            // }

            #endregion
        }
    }

    #endregion

    #region 从表明细列表

    /// <summary>
    /// 明细表数据源变更时
    /// </summary>
    protected override void gridView2_DataSourceChanged(object sender, EventArgs e)
    {
        base.gridView2_DataSourceChanged(sender, e);
        // 绑定数据后，分配各列的宽度
        if (winGridView2.gridView1.Columns.Count > 0 && winGridView2.gridView1.RowCount > 0)
        {
            // 可特殊设置特别的宽度
            // GridView gridView = winGridView2.gridView1;
            // if (gridView != null)
            // {
            //     gridView.SetGridColumWidth(Messages.FieldISID, 200);
            //     gridView.SetGridColumWidth(Messages.FieldMsgNo, 200);
            //     gridView.SetGridColumWidth(Messages.FieldDealStatus, 200);
            //     gridView.SetGridColumWidth(Messages.FieldDealContent, 200);
            //     gridView.SetGridColumWidth(Messages.FieldAttaPath, 200);
            //     gridView.SetGridColumWidth(Messages.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(Messages.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(Messages.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(Messages.FieldLastUpdateBy, 200);
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
            { Message.FieldMsgNo, txtMsgNo.Text.Trim() },
            { Message.FieldMsgType, txtMsgType.GetComboBoxValue() },
            { Message.FieldWaybillNo, txtWaybillNo.Text.Trim() },
            { Message.FieldSendMsgNode, txtSendMsgNode.GetComboBoxValue() },
            { Message.FieldSendMsgContent, txtSendMsgContent.Text.Trim() },
            { Message.FieldRecvMsgNode, txtRecvMsgNode.GetComboBoxValue() },
            { Message.FieldDealStatus, txtDealStatus.GetComboBoxValue() },
            { Message.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { Message.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
            { Message.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
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
        var info = new Message
        {
            MsgNo = GetRowData(dr, "问题件编号"),
            MsgType = GetRowData(dr, "问题件类型"),
            WaybillNo = GetRowData(dr, "运单号"),
            SendMsgNode = GetRowData(dr, "发送方网点"),
            SendMsgContent = GetRowData(dr, "问题件内容"),
            RecvMsgNode = GetRowData(dr, "接收方网点"),
            DealStatus = GetRowData(dr, "处理状态"),
            AttaPath = GetRowData(dr, "附件地址"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "修改时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "修改人"),
            FlagApp = GetRowData(dr, "审核") == "是",
            AppUser = GetRowData(dr, "审核人"),
            AppDate = GetRowData(dr, "审核时间").ToDateTime(dtNow),
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
        List<Message> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "问题件编号,问题件类型,运单号,发送方网点,问题件内容,接收方网点,处理状态,附件地址,创建时间,创建人,修改时间,修改人,审核,审核人,审核时间");
        var j = 1;
        foreach (Message t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["问题件编号"] = t.MsgNo;
            dr["问题件类型"] = t.MsgType;
            dr["运单号"] = t.WaybillNo;
            dr["发送方网点"] = t.SendMsgNode;
            dr["问题件内容"] = t.SendMsgContent;
            dr["接收方网点"] = t.RecvMsgNode;
            dr["处理状态"] = t.DealStatus;
            dr["附件地址"] = t.AttaPath;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["修改时间"] = t.LastUpdateDate;
            dr["修改人"] = t.LastUpdatedBy;
            dr["审核"] = t.FlagApp ? "是" : "否";
            dr["审核人"] = t.AppUser;
            dr["审核时间"] = t.AppDate;
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
            ex.ToString().LogError();
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
            AdvDlg?.AddColumnListItem(Message.FieldMsgType, "问题件类型");
            AdvDlg?.AddColumnListItem(Message.FieldSendMsgNode, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(Message.FieldRecvMsgNode, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(Message.FieldDealStatus, "问题件处理类型");
            AdvDlg?.AddColumnListItem(Message.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Message.FieldLastUpdatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Message.FieldAppUser, GB.AllUserDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion

    #region 自定义按钮事件

    /// <summary>
    /// 问题件回复
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private async void btnReply_Click(object sender, ItemClickEventArgs e)
    {
        string no = GetFocusedRowCellPrimaryValue();
        var reply = App.GetService<FrmEditMessages>();
        reply.SetMsgNo(no);
        // 保存成功后事件
        // reply.OnDataSaved += edit_OnDataSaved;
        if (DialogResult.OK == reply.ShowDialog())
        {
            await BindData();
        }
    }

    #endregion
}