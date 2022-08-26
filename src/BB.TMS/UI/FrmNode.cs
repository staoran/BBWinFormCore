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
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 网点资料
/// </summary>
public partial class FrmNode : BaseViewDock<Node, NodeHttpService, FrmEditNode, Nodes, NodesHttpService>
{
    public FrmNode(NodeHttpService bll, NodesHttpService childBll, FrmEditNode baseForm) : base(bll, childBll, baseForm)
    {
        moduleName = "网点资料";

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

        #endregion
    }

    /// <summary>
    /// 窗体首次打开后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmNode_Shown(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(Node.FieldTranNodeType, "网点类型");
            x.AddNode(Node.FieldLockLimit, "锁机限制", GB.YesOrNo);
            x.AddNode(Node.FieldSendSMS, "开通短信", GB.YesOrNo);
            x.AddNode(Node.FieldISLocked, "封锁", GB.YesOrNo);
            x.AddNode(Node.FieldAckRec, "开通回单", GB.YesOrNo);
            x.AddNode(Node.FieldTranNodeStatus, "网点状态类型");
            x.AddNode(Node.FieldPublicYN, "是否开放", GB.YesOrNo);
            x.AddNode(Node.FieldFlagApp, "审核", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtTranNodeType.BindDictItems("网点类型", "9", true, false);
        txtParentNo.BindDictItems(GB.AllOuDict, null, true, false);
        txtLockLimit.BindDictItems(GB.YesOrNo, null, true, false);
        txtSendSMS.BindDictItems(GB.YesOrNo, null, true, false);
        txtISLocked.BindDictItems(GB.YesOrNo, null, true, false);
        txtAckRec.BindDictItems(GB.YesOrNo, null, true, false);
        txtAreaNo.BindCityItems(false);
        txtTranNodeStatus.BindDictItems("网点状态类型", "1", true, false);
        txtPublicYN.BindDictItems(GB.YesOrNo, null, true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "TranNodeNO,TranNodeCostNo,TranNodeName,TranNodeType,TranNodeBeginDate,TranNodeEndDate,ParentNo,TranNodePerson,TranNodePersonID,TranNodeMobile,TranNodeAddress,LockLimit,LockLimitAmt,WarningLimitAmt,SendSMS,ISLocked,AckRec,AgencyRecLimitAmt,AgencyRecLimitAmtBKP,CarriageForwardLimitAmt,CarriageForwardLimitAmtBKP,AreaNo,InTime,OutTime,Remark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,TranNodeStatus,PublicYN,FlagApp,AppUser,AppDate,SignLoopEndTime,SignLimitTime,SignDays,AckRecDays,CostMasterYN,ManagementFee,UsageFee,Deposit,ContractNote,DispatchOnly,PickupWeightLimit,PickupVolumeLimit,TranNodeAxes,IsLockLimitKPI,FinancialCenter,WhiteList,BlackList";

        #region 添加别名解析，两种方式均可

        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodeNO, "网点ID");
        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodeCostNo, "结算网点编号");
        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodeName, "网点名称");
        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodeType, "网点类型");
        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodeBeginDate, "合同开始时间");
        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodeEndDate, "合同终止时间");
        // winGridViewPager1.AddColumnAlias(Node.FieldParentNo, "上级网点ID");
        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodePerson, "网点负责人");
        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodePersonID, "负责人证件号码");
        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodeMobile, "网点联系方式");
        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodeAddress, "网点地址");
        // winGridViewPager1.AddColumnAlias(Node.FieldLockLimit, "锁机限制");
        // winGridViewPager1.AddColumnAlias(Node.FieldLockLimitAmt, "锁机金额");
        // winGridViewPager1.AddColumnAlias(Node.FieldWarningLimitAmt, "警戒金额");
        // winGridViewPager1.AddColumnAlias(Node.FieldSendSMS, "开通短信");
        // winGridViewPager1.AddColumnAlias(Node.FieldISLocked, "封锁");
        // winGridViewPager1.AddColumnAlias(Node.FieldAckRec, "开通回单");
        // winGridViewPager1.AddColumnAlias(Node.FieldAgencyRecLimitAmt, "代收款限额");
        // winGridViewPager1.AddColumnAlias(Node.FieldAgencyRecLimitAmtBKP, "代收款限额BKP");
        // winGridViewPager1.AddColumnAlias(Node.FieldCarriageForwardLimitAmt, "到付款限额");
        // winGridViewPager1.AddColumnAlias(Node.FieldCarriageForwardLimitAmtBKP, "到付款限额BKP");
        // winGridViewPager1.AddColumnAlias(Node.FieldAreaNo, "区");
        // winGridViewPager1.AddColumnAlias(Node.FieldInTime, "进港时间");
        // winGridViewPager1.AddColumnAlias(Node.FieldOutTime, "出港时间");
        // winGridViewPager1.AddColumnAlias(Node.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(Node.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(Node.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(Node.FieldLastUpdateDate, "更新时间");
        // winGridViewPager1.AddColumnAlias(Node.FieldLastUpdatedBy, "更新人");
        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodeStatus, "网点状态");
        // winGridViewPager1.AddColumnAlias(Node.FieldPublicYN, "是否开放");
        // winGridViewPager1.AddColumnAlias(Node.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(Node.FieldAppUser, "审核人");
        // winGridViewPager1.AddColumnAlias(Node.FieldAppDate, "审核时间");
        // winGridViewPager1.AddColumnAlias(Node.FieldSignLoopEndTime, "签收周期起算时间");
        // winGridViewPager1.AddColumnAlias(Node.FieldSignLimitTime, "签收最晚时间");
        // winGridViewPager1.AddColumnAlias(Node.FieldSignDays, "签收天数");
        // winGridViewPager1.AddColumnAlias(Node.FieldAckRecDays, "回单返回天数");
        // winGridViewPager1.AddColumnAlias(Node.FieldCostMasterYN, "跨平台结算主体");
        // winGridViewPager1.AddColumnAlias(Node.FieldManagementFee, "管理费");
        // winGridViewPager1.AddColumnAlias(Node.FieldUsageFee, "系统使用费");
        // winGridViewPager1.AddColumnAlias(Node.FieldDeposit, "押金");
        // winGridViewPager1.AddColumnAlias(Node.FieldContractNote, "合同备注");
        // winGridViewPager1.AddColumnAlias(Node.FieldDispatchOnly, "仅送货");
        // winGridViewPager1.AddColumnAlias(Node.FieldPickupWeightLimit, "自提限重");
        // winGridViewPager1.AddColumnAlias(Node.FieldPickupVolumeLimit, "自提限方");
        // winGridViewPager1.AddColumnAlias(Node.FieldTranNodeAxes, "网点坐标");
        // winGridViewPager1.AddColumnAlias(Node.FieldIsLockLimitKPI, "网点锁机kpi是否执行");
        // winGridViewPager1.AddColumnAlias(Node.FieldFinancialCenter, "所属财务中心");
        // winGridViewPager1.AddColumnAlias(Node.FieldWhiteList, "白名单");
        // winGridViewPager1.AddColumnAlias(Node.FieldBlackList, "黑名单");

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(Node.FieldTranNodeCostNo, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Node.FieldTranNodeType, "网点类型");
        winGridViewPager1.SetColumnDataSource(Node.FieldParentNo, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Node.FieldAreaNo, GB.AllRegions);
        winGridViewPager1.SetColumnDataSource(Node.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Node.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Node.FieldTranNodeStatus, "网点状态类型");
        winGridViewPager1.SetColumnDataSource(Node.FieldAppUser, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Node.FieldFinancialCenter, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Node.FieldWhiteList, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Node.FieldBlackList, GB.AllOuDict);

        #endregion

        #region 添加列字段汇总行


        #endregion

        #endregion

        #region 明细表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridView2.DisplayColumns = "ISID,TranNodeAreaName,TranNodeAreaDesc,DeliveryType,EFence,CenterCoordinate,ConvertVK,AreaId,Address,Person,Phone,SignLimitHour,CancelYN,Remark";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridView2.AddColumnAlias(Nodes.FieldISID, "自增ID");
        // winGridView2.AddColumnAlias(Nodes.FieldTranNodeAreaName, "区域名称");
        // winGridView2.AddColumnAlias(Nodes.FieldTranNodeAreaDesc, "区域详情");
        // winGridView2.AddColumnAlias(Nodes.FieldDeliveryType, "交货方式");
        // winGridView2.AddColumnAlias(Nodes.FieldEFence, "电子围栏");
        // winGridView2.AddColumnAlias(Nodes.FieldCenterCoordinate, "中心坐标");
        // winGridView2.AddColumnAlias(Nodes.FieldConvertVK, "重泡比");
        // winGridView2.AddColumnAlias(Nodes.FieldAreaId, "区");
        // winGridView2.AddColumnAlias(Nodes.FieldAddress, "地址");
        // winGridView2.AddColumnAlias(Nodes.FieldPerson, "负责人");
        // winGridView2.AddColumnAlias(Nodes.FieldPhone, "联系方式");
        // winGridView2.AddColumnAlias(Nodes.FieldSignLimitHour, "到件签收时效");
        // winGridView2.AddColumnAlias(Nodes.FieldCancelYN, "作废");
        // winGridView2.AddColumnAlias(Nodes.FieldRemark, "备注");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridView2.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridView2.SetColumnDataSource(Nodes.FieldDeliveryType, "交货方式");
        winGridView2.SetColumnDataSource(Nodes.FieldAreaId, GB.AllRegions);

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
            //     gridView.SetGridColumWidth(Node.FieldTranNodeNO, 200);
            //     gridView.SetGridColumWidth(Node.FieldTranNodeCostNo, 200);
            //     gridView.SetGridColumWidth(Node.FieldTranNodeName, 200);
            //     gridView.SetGridColumWidth(Node.FieldTranNodeType, 200);
            //     gridView.SetGridColumWidth(Node.FieldTranNodeBeginDate, 200);
            //     gridView.SetGridColumWidth(Node.FieldTranNodeEndDate, 200);
            //     gridView.SetGridColumWidth(Node.FieldParentNo, 200);
            //     gridView.SetGridColumWidth(Node.FieldTranNodePerson, 200);
            //     gridView.SetGridColumWidth(Node.FieldTranNodePersonID, 200);
            //     gridView.SetGridColumWidth(Node.FieldTranNodeMobile, 200);
            //     gridView.SetGridColumWidth(Node.FieldTranNodeAddress, 200);
            //     gridView.SetGridColumWidth(Node.FieldLockLimit, 200);
            //     gridView.SetGridColumWidth(Node.FieldLockLimitAmt, 200);
            //     gridView.SetGridColumWidth(Node.FieldWarningLimitAmt, 200);
            //     gridView.SetGridColumWidth(Node.FieldSendSMS, 200);
            //     gridView.SetGridColumWidth(Node.FieldISLocked, 200);
            //     gridView.SetGridColumWidth(Node.FieldAckRec, 200);
            //     gridView.SetGridColumWidth(Node.FieldAgencyRecLimitAmt, 200);
            //     gridView.SetGridColumWidth(Node.FieldAgencyRecLimitAmtBKP, 200);
            //     gridView.SetGridColumWidth(Node.FieldCarriageForwardLimitAmt, 200);
            //     gridView.SetGridColumWidth(Node.FieldCarriageForwardLimitAmtBKP, 200);
            //     gridView.SetGridColumWidth(Node.FieldAreaNo, 200);
            //     gridView.SetGridColumWidth(Node.FieldInTime, 200);
            //     gridView.SetGridColumWidth(Node.FieldOutTime, 200);
            //     gridView.SetGridColumWidth(Node.FieldRemark, 200);
            //     gridView.SetGridColumWidth(Node.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(Node.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(Node.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(Node.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(Node.FieldTranNodeStatus, 200);
            //     gridView.SetGridColumWidth(Node.FieldPublicYN, 200);
            //     gridView.SetGridColumWidth(Node.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(Node.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(Node.FieldAppDate, 200);
            //     gridView.SetGridColumWidth(Node.FieldSignLoopEndTime, 200);
            //     gridView.SetGridColumWidth(Node.FieldSignLimitTime, 200);
            //     gridView.SetGridColumWidth(Node.FieldSignDays, 200);
            //     gridView.SetGridColumWidth(Node.FieldAckRecDays, 200);
            //     gridView.SetGridColumWidth(Node.FieldCostMasterYN, 200);
            //     gridView.SetGridColumWidth(Node.FieldManagementFee, 200);
            //     gridView.SetGridColumWidth(Node.FieldUsageFee, 200);
            //     gridView.SetGridColumWidth(Node.FieldDeposit, 200);
            //     gridView.SetGridColumWidth(Node.FieldContractNote, 200);
            //     gridView.SetGridColumWidth(Node.FieldDispatchOnly, 200);
            //     gridView.SetGridColumWidth(Node.FieldPickupWeightLimit, 200);
            //     gridView.SetGridColumWidth(Node.FieldPickupVolumeLimit, 200);
            //     gridView.SetGridColumWidth(Node.FieldTranNodeAxes, 200);
            //     gridView.SetGridColumWidth(Node.FieldIsLockLimitKPI, 200);
            //     gridView.SetGridColumWidth(Node.FieldFinancialCenter, 200);
            //     gridView.SetGridColumWidth(Node.FieldWhiteList, 200);
            //     gridView.SetGridColumWidth(Node.FieldBlackList, 200);
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
            //     gridView.SetGridColumWidth(Nodes.FieldISID, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldTranNodeAreaName, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldTranNodeAreaDesc, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldDeliveryType, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldEFence, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldCenterCoordinate, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldConvertVK, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldAreaId, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldAddress, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldPerson, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldPhone, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldSignLimitHour, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldCancelYN, 200);
            //     gridView.SetGridColumWidth(Nodes.FieldRemark, 200);
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
            { Node.FieldTranNodeNO, txtTranNodeNO.Text.Trim() },
            { Node.FieldTranNodeName, txtTranNodeName.Text.Trim() },
            { Node.FieldTranNodeType, txtTranNodeType.GetComboBoxValue() },
            { Node.FieldParentNo, txtParentNo.GetComboBoxValue() },
            { Node.FieldTranNodePerson, txtTranNodePerson.Text.Trim() },
            { Node.FieldTranNodeMobile, txtTranNodeMobile.Text.Trim() },
            { Node.FieldTranNodeAddress, txtTranNodeAddress.Text.Trim() },
            { Node.FieldLockLimit, txtLockLimit.GetComboBoxValue() },
            { Node.FieldSendSMS, txtSendSMS.GetComboBoxValue() },
            { Node.FieldISLocked, txtISLocked.GetComboBoxValue() },
            { Node.FieldAckRec, txtAckRec.GetComboBoxValue() },
            { Node.FieldAreaNo, txtAreaNo.EditValue.ObjToStr() },
            { Node.FieldTranNodeStatus, txtTranNodeStatus.GetComboBoxValue() },
            { Node.FieldPublicYN, txtPublicYN.GetComboBoxValue() },
            { Node.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
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
        var info = new Node
        {
            TranNodeNO = GetRowData(dr, "网点ID"),
            TranNodeCostNo = GetRowData(dr, "结算网点编号"),
            TranNodeName = GetRowData(dr, "网点名称"),
            TranNodeType = GetRowData(dr, "网点类型"),
            TranNodeBeginDate = GetRowData(dr, "合同开始时间").ToDateTime(dtNow),
            TranNodeEndDate = GetRowData(dr, "合同终止时间").ToDateTime(dtNow),
            ParentNo = GetRowData(dr, "上级网点ID"),
            TranNodePerson = GetRowData(dr, "网点负责人"),
            TranNodePersonID = GetRowData(dr, "负责人证件号码"),
            TranNodeMobile = GetRowData(dr, "网点联系方式"),
            TranNodeAddress = GetRowData(dr, "网点地址"),
            LockLimit = GetRowData(dr, "锁机限制") == "是",
            LockLimitAmt = GetRowData(dr, "锁机金额").ToDecimal(),
            WarningLimitAmt = GetRowData(dr, "警戒金额").ToDecimal(),
            SendSMS = GetRowData(dr, "开通短信") == "是",
            ISLocked = GetRowData(dr, "封锁") == "是",
            AckRec = GetRowData(dr, "开通回单") == "是",
            AgencyRecLimitAmt = GetRowData(dr, "代收款限额").ToDecimal(),
            // AgencyRecLimitAmtBKP = GetRowData(dr, "代收款限额BKP").ToDecimal(),
            CarriageForwardLimitAmt = GetRowData(dr, "到付款限额").ToDecimal(),
            // CarriageForwardLimitAmtBKP = GetRowData(dr, "到付款限额BKP").ToDecimal(),
            AreaNo = GetRowData(dr, "区"),
            InTime = GetRowData(dr, "进港时间").ToDateTime(dtNow),
            OutTime = GetRowData(dr, "出港时间").ToDateTime(dtNow),
            Remark = GetRowData(dr, "备注"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "更新时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "更新人"),
            TranNodeStatus = GetRowData(dr, "网点状态"),
            PublicYN = GetRowData(dr, "是否开放") == "是",
            FlagApp = GetRowData(dr, "审核") == "是",
            AppUser = GetRowData(dr, "审核人"),
            AppDate = GetRowData(dr, "审核时间").ToDateTime(dtNow),
            SignLoopEndTime = GetRowData(dr, "签收周期起算时间").ToDateTime(dtNow),
            SignLimitTime = GetRowData(dr, "签收最晚时间").ToDateTime(dtNow),
            SignDays = GetRowData(dr, "签收天数").ObjToInt(),
            AckRecDays = GetRowData(dr, "回单返回天数").ObjToInt(),
            CostMasterYN = GetRowData(dr, "跨平台结算主体") == "是",
            ManagementFee = GetRowData(dr, "管理费").ToDecimal(),
            UsageFee = GetRowData(dr, "系统使用费").ToDecimal(),
            Deposit = GetRowData(dr, "押金").ToDecimal(),
            ContractNote = GetRowData(dr, "合同备注"),
            DispatchOnly = GetRowData(dr, "仅送货") == "是",
            PickupWeightLimit = GetRowData(dr, "自提限重").ToDecimal(),
            PickupVolumeLimit = GetRowData(dr, "自提限方").ToDecimal(),
            TranNodeAxes = GetRowData(dr, "网点坐标"),
            IsLockLimitKPI = GetRowData(dr, "网点锁机kpi是否执行") == "是",
            FinancialCenter = GetRowData(dr, "所属财务中心"),
            WhiteList = GetRowData(dr, "白名单"),
            BlackList = GetRowData(dr, "黑名单"),
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
        List<Node> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "网点ID,结算网点编号,网点名称,网点类型,合同开始时间,合同终止时间,上级网点ID,网点负责人,负责人证件号码,网点联系方式,网点地址,锁机限制,锁机金额,警戒金额,开通短信,封锁,开通回单,代收款限额,代收款限额BKP,到付款限额,到付款限额BKP,区,进港时间,出港时间,备注,创建时间,创建人,更新时间,更新人,网点状态,是否开放,审核,审核人,审核时间,签收周期起算时间,签收最晚时间,签收天数,回单返回天数,跨平台结算主体,管理费,系统使用费,押金,合同备注,仅送货,自提限重,自提限方,网点坐标,网点锁机kpi是否执行,所属财务中心,白名单,黑名单");
        var j = 1;
        foreach (Node t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["网点ID"] = t.TranNodeNO;
            dr["结算网点编号"] = t.TranNodeCostNo;
            dr["网点名称"] = t.TranNodeName;
            dr["网点类型"] = t.TranNodeType;
            dr["合同开始时间"] = t.TranNodeBeginDate;
            dr["合同终止时间"] = t.TranNodeEndDate;
            dr["上级网点ID"] = t.ParentNo;
            dr["网点负责人"] = t.TranNodePerson;
            dr["负责人证件号码"] = t.TranNodePersonID;
            dr["网点联系方式"] = t.TranNodeMobile;
            dr["网点地址"] = t.TranNodeAddress;
            dr["锁机限制"] = t.LockLimit ? "是" : "否";
            dr["锁机金额"] = t.LockLimitAmt;
            dr["警戒金额"] = t.WarningLimitAmt;
            dr["开通短信"] = t.SendSMS ? "是" : "否";
            dr["封锁"] = t.ISLocked ? "是" : "否";
            dr["开通回单"] = t.AckRec ? "是" : "否";
            dr["代收款限额"] = t.AgencyRecLimitAmt;
            // dr["代收款限额BKP"] = t.AgencyRecLimitAmtBKP;
            dr["到付款限额"] = t.CarriageForwardLimitAmt;
            // dr["到付款限额BKP"] = t.CarriageForwardLimitAmtBKP;
            dr["区"] = t.AreaNo;
            dr["进港时间"] = t.InTime;
            dr["出港时间"] = t.OutTime;
            dr["备注"] = t.Remark;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["更新时间"] = t.LastUpdateDate;
            dr["更新人"] = t.LastUpdatedBy;
            dr["网点状态"] = t.TranNodeStatus;
            dr["是否开放"] = t.PublicYN ? "是" : "否";
            dr["审核"] = t.FlagApp ? "是" : "否";
            dr["审核人"] = t.AppUser;
            dr["审核时间"] = t.AppDate;
            dr["签收周期起算时间"] = t.SignLoopEndTime;
            dr["签收最晚时间"] = t.SignLimitTime;
            dr["签收天数"] = t.SignDays;
            dr["回单返回天数"] = t.AckRecDays;
            dr["跨平台结算主体"] = t.CostMasterYN ? "是" : "否";
            dr["管理费"] = t.ManagementFee;
            dr["系统使用费"] = t.UsageFee;
            dr["押金"] = t.Deposit;
            dr["合同备注"] = t.ContractNote;
            dr["仅送货"] = t.DispatchOnly ? "是" : "否";
            dr["自提限重"] = t.PickupWeightLimit;
            dr["自提限方"] = t.PickupVolumeLimit;
            dr["网点坐标"] = t.TranNodeAxes;
            dr["网点锁机kpi是否执行"] = t.IsLockLimitKPI ? "是" : "否";
            dr["所属财务中心"] = t.FinancialCenter;
            dr["白名单"] = t.WhiteList;
            dr["黑名单"] = t.BlackList;
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
            AdvDlg?.AddColumnListItem(Node.FieldTranNodeCostNo, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(Node.FieldTranNodeType, "网点类型");
            AdvDlg?.AddColumnListItem(Node.FieldParentNo, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(Node.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Node.FieldLastUpdatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Node.FieldTranNodeStatus, "网点状态类型");
            AdvDlg?.AddColumnListItem(Node.FieldAppUser, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Node.FieldFinancialCenter, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(Node.FieldWhiteList, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(Node.FieldBlackList, GB.AllOuDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}