using System.Data;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Diagnostics;
using BB.BaseUI.BaseUI;
using BB.BaseUI.DocViewer;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.BaseUI.WinForm;
using BB.Tools.Format;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using DevExpress.XtraBars;
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 承运商资料
/// </summary>
public partial class FrmLogisticCompany : BaseViewDock<LogisticCompany, LogisticCompanyHttpService, FrmEditLogisticCompany>
{
    public FrmLogisticCompany(LogisticCompanyHttpService bll, LazilyResolved<FrmEditLogisticCompany> baseForm) : base(bll, baseForm)
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

        #endregion
    }

    /// <summary>
    /// 窗体首次打开后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FrmLogisticCompany_Shown(object? sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(LogisticCompany.FieldTrustLevel, "信誉类型");
            x.AddNode(LogisticCompany.FieldInUse, "是否启用", GB.YesOrNo);
            x.AddNode(LogisticCompany.FieldFlagInvoice, "是否开票", GB.YesOrNo);
            x.AddNode(LogisticCompany.FieldFlagApp, "审核", GB.YesOrNo);
            x.AddNode(LogisticCompany.FieldCancelYN, "是否作废", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtTrustLevel.BindDictItems("信誉类型", "", true, false);
        txtInUse.BindDictItems(GB.YesOrNo, null, true, false);
        txtFlagInvoice.BindDictItems(GB.YesOrNo, null, true, false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);
        txtCancelYN.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "OrgCode,LogisticCode,LogisticName,ZJM,Contacts,Tel,Address,MainLine,TrustLevel,Legal,Tax,Bank,BankAccount,PaymentTerm,ContractDate1,ContractDate2,InUse,FlagInvoice,Remark,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate,CancelYN,CancelDate,CancelUser";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldOrgCode, "网点名称");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldLogisticCode, "承运商编号");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldLogisticName, "承运商名称");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldZJM, "助记码");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldContacts, "联系人");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldTel, "电话");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldAddress, "地址");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldMainLine, "主营线路");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldTrustLevel, "信誉");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldLegal, "法人");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldTax, "税号");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldBank, "开户行");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldBankAccount, "银行账号");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldPaymentTerm, "账期说明");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldContractDate1, "合同起始日期");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldContractDate2, "合同结束日期");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldInUse, "是否启用");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldFlagInvoice, "是否开票");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldCreationDate, "创建时间");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldLastUpdateDate, "更新时间");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldLastUpdatedBy, "更新人");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldAppUser, "审批人");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldAppDate, "审批时间");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldCancelYN, "是否作废");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldCancelDate, "作废时间");
        // winGridViewPager1.AddColumnAlias(LogisticCompany.FieldCancelUser, "作废人");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(LogisticCompany.FieldOrgCode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(LogisticCompany.FieldTrustLevel, "信誉类型");
        winGridViewPager1.SetColumnDataSource(LogisticCompany.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(LogisticCompany.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(LogisticCompany.FieldAppUser, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行


        #endregion

        #endregion

        #endregion
    }

    #region 网格列表信息

    /// <summary>
    /// 数据源变更时，分配各列的宽度
    /// </summary>
    protected override void gridView1_DataSourceChanged(object? sender, EventArgs e)
    {
        base.gridView1_DataSourceChanged(sender, e);
        if (winGridViewPager1.gridView1.Columns.Count > 0 && winGridViewPager1.gridView1.RowCount > 0)
        {
            #region 单独设置列宽

            // 可特殊设置特别的宽度
            // GridView gridView = winGridViewPager1.gridView1;
            // if (gridView != null)
            // {
            //     gridView.SetGridColumWidth(LogisticCompany.FieldOrgCode, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldLogisticCode, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldLogisticName, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldZJM, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldContacts, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldTel, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldAddress, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldMainLine, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldTrustLevel, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldLegal, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldTax, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldBank, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldBankAccount, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldPaymentTerm, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldContractDate1, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldContractDate2, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldInUse, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldFlagInvoice, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldRemark, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldAppDate, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldCancelYN, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldCancelDate, 200);
            //     gridView.SetGridColumWidth(LogisticCompany.FieldCancelUser, 200);
            // }

            #endregion
        }
    }

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
            { LogisticCompany.FieldLogisticName, txtLogisticName.Text.Trim() },
            { LogisticCompany.FieldZJM, txtZJM.Text.Trim() },
            { LogisticCompany.FieldContacts, txtContacts.Text.Trim() },
            { LogisticCompany.FieldTel, txtTel.Text.Trim() },
            { LogisticCompany.FieldMainLine, txtMainLine.Text.Trim() },
            { LogisticCompany.FieldTrustLevel, txtTrustLevel.GetComboBoxValue() },
            { LogisticCompany.FieldContractDate1, txtContractDate11.EditValue.ObjToStr() },
            { LogisticCompany.FieldContractDate1, txtContractDate12.EditValue.ObjToStr() },
            { LogisticCompany.FieldContractDate2, txtContractDate21.EditValue.ObjToStr() },
            { LogisticCompany.FieldContractDate2, txtContractDate22.EditValue.ObjToStr() },
            { LogisticCompany.FieldInUse, txtInUse.GetComboBoxValue() },
            { LogisticCompany.FieldFlagInvoice, txtFlagInvoice.GetComboBoxValue() },
            { LogisticCompany.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
            { LogisticCompany.FieldCancelYN, txtCancelYN.GetComboBoxValue() },
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
        var info = new LogisticCompany
        {
            OrgCode = GetRowData(dr, "网点名称"),
            LogisticCode = GetRowData(dr, "承运商编号"),
            LogisticName = GetRowData(dr, "承运商名称"),
            ZJM = GetRowData(dr, "助记码"),
            Contacts = GetRowData(dr, "联系人"),
            Tel = GetRowData(dr, "电话"),
            Address = GetRowData(dr, "地址"),
            MainLine = GetRowData(dr, "主营线路"),
            TrustLevel = GetRowData(dr, "信誉"),
            Legal = GetRowData(dr, "法人"),
            Tax = GetRowData(dr, "税号"),
            Bank = GetRowData(dr, "开户行"),
            BankAccount = GetRowData(dr, "银行账号"),
            PaymentTerm = GetRowData(dr, "账期说明"),
            ContractDate1 = GetRowData(dr, "合同起始日期").ToDateTime(dtNow),
            ContractDate2 = GetRowData(dr, "合同结束日期").ToDateTime(dtNow),
            InUse = GetRowData(dr, "是否启用") == "是",
            FlagInvoice = GetRowData(dr, "是否开票") == "是",
            Remark = GetRowData(dr, "备注"),
            CreationDate = GetRowData(dr, "创建时间").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "更新时间").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "更新人"),
            FlagApp = GetRowData(dr, "审核") == "是",
            AppUser = GetRowData(dr, "审批人"),
            AppDate = GetRowData(dr, "审批时间").ToDateTime(dtNow),
            CancelYN = GetRowData(dr, "是否作废") == "是",
            CancelDate = GetRowData(dr, "作废时间").ToDateTime(dtNow),
            CancelUser = GetRowData(dr, "作废人"),
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
        List<LogisticCompany> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "网点名称,承运商编号,承运商名称,助记码,联系人,电话,地址,主营线路,信誉,法人,税号,开户行,银行账号,账期说明,合同起始日期,合同结束日期,是否启用,是否开票,备注,创建时间,创建人,更新时间,更新人,审核,审批人,审批时间,是否作废,作废时间,作废人");
        var j = 1;
        foreach (LogisticCompany t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["网点名称"] = t.OrgCode;
            dr["承运商编号"] = t.LogisticCode;
            dr["承运商名称"] = t.LogisticName;
            dr["助记码"] = t.ZJM;
            dr["联系人"] = t.Contacts;
            dr["电话"] = t.Tel;
            dr["地址"] = t.Address;
            dr["主营线路"] = t.MainLine;
            dr["信誉"] = t.TrustLevel;
            dr["法人"] = t.Legal;
            dr["税号"] = t.Tax;
            dr["开户行"] = t.Bank;
            dr["银行账号"] = t.BankAccount;
            dr["账期说明"] = t.PaymentTerm;
            dr["合同起始日期"] = t.ContractDate1;
            dr["合同结束日期"] = t.ContractDate2;
            dr["是否启用"] = t.InUse ? "是" : "否";
            dr["是否开票"] = t.FlagInvoice ? "是" : "否";
            dr["备注"] = t.Remark;
            dr["创建时间"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["更新时间"] = t.LastUpdateDate;
            dr["更新人"] = t.LastUpdatedBy;
            dr["审核"] = t.FlagApp ? "是" : "否";
            dr["审批人"] = t.AppUser;
            dr["审批时间"] = t.AppDate;
            dr["是否作废"] = t.CancelYN ? "是" : "否";
            dr["作废时间"] = t.CancelDate;
            dr["作废人"] = t.CancelUser;
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
            AdvDlg?.AddColumnListItem(LogisticCompany.FieldOrgCode, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(LogisticCompany.FieldTrustLevel, "信誉类型");
            AdvDlg?.AddColumnListItem(LogisticCompany.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(LogisticCompany.FieldLastUpdatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(LogisticCompany.FieldAppUser, GB.AllUserDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}