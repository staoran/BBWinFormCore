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
using DevExpress.Data;
using DevExpress.XtraBars;
using Furion.Logging.Extensions;

namespace BB.TMS.UI;

/// <summary>
/// 客户资料
/// </summary>
public partial class FrmCustomer : BaseViewDock<Customer, CustomerHttpService, FrmEditCustomer, Customers, CustomersHttpService>
{
    public FrmCustomer(CustomerHttpService bll, CustomersHttpService childBll, LazilyResolved<FrmEditCustomer> baseForm) : base(bll, childBll, baseForm)
    {
        moduleName = "客户资料";

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
    private void FrmCustomer_Shown(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 快查树初始化
    /// </summary>
    protected override void InitTree()
    {
        treeView1.InitTree(x =>
        {
            x.AddNode(Customer.FieldFlagApp, "审核", GB.YesOrNo);
        });
    }

    /// <summary>
    /// 初始化字典列表内容
    /// </summary>
    protected sealed override async Task InitDictItem()
    {
        #region 查询表单初始化

        txtAreaNo.BindCityItems(false);
        txtFlagApp.BindDictItems(GB.YesOrNo, null, true, false);

        #endregion

        #region Grid初始化

        await base.InitDictItem();

        #region 主表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridViewPager1.DisplayColumns = "CustomerCode,MnemonicCode,TranNode,ContactPerson,NativeName,Address,AreaNo,Tel,Mobile,Bank,BankAccount,Remark,InUse,PaymentType,InvoiceFax,CommissionType,CommissionRate,SalesDeputy,ProjectManager,FlagInvoice,SalesPerson,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy,FlagApp,AppUser,AppDate";

        #region 添加别名解析，两种方式均可

        // winGridViewPager1.AddColumnAlias(Customer.FieldCustomerCode, "公司编号");
        // winGridViewPager1.AddColumnAlias(Customer.FieldMnemonicCode, "助记码");
        // winGridViewPager1.AddColumnAlias(Customer.FieldTranNode, "网点编号");
        // winGridViewPager1.AddColumnAlias(Customer.FieldContactPerson, "联系人");
        // winGridViewPager1.AddColumnAlias(Customer.FieldNativeName, "公司名称");
        // winGridViewPager1.AddColumnAlias(Customer.FieldAddress, "地址");
        // winGridViewPager1.AddColumnAlias(Customer.FieldAreaNo, "区");
        // winGridViewPager1.AddColumnAlias(Customer.FieldTel, "电话号码");
        // winGridViewPager1.AddColumnAlias(Customer.FieldMobile, "手机号");
        // winGridViewPager1.AddColumnAlias(Customer.FieldBank, "开户行");
        // winGridViewPager1.AddColumnAlias(Customer.FieldBankAccount, "银行账号");
        // winGridViewPager1.AddColumnAlias(Customer.FieldRemark, "备注");
        // winGridViewPager1.AddColumnAlias(Customer.FieldInUse, "是否在用(Y/N）");
        // winGridViewPager1.AddColumnAlias(Customer.FieldPaymentType, "付款方式");
        // winGridViewPager1.AddColumnAlias(Customer.FieldInvoiceFax, "税率");
        // winGridViewPager1.AddColumnAlias(Customer.FieldCommissionType, "业务员提成方式");
        // winGridViewPager1.AddColumnAlias(Customer.FieldCommissionRate, "提成比例");
        // winGridViewPager1.AddColumnAlias(Customer.FieldSalesDeputy, "客服");
        // winGridViewPager1.AddColumnAlias(Customer.FieldProjectManager, "项目主管");
        // winGridViewPager1.AddColumnAlias(Customer.FieldFlagInvoice, "开票");
        // winGridViewPager1.AddColumnAlias(Customer.FieldSalesPerson, "业务员");
        // winGridViewPager1.AddColumnAlias(Customer.FieldCreationDate, "创建日期");
        // winGridViewPager1.AddColumnAlias(Customer.FieldCreatedBy, "创建人");
        // winGridViewPager1.AddColumnAlias(Customer.FieldLastUpdateDate, "更新日期");
        // winGridViewPager1.AddColumnAlias(Customer.FieldLastUpdatedBy, "更新人");
        // winGridViewPager1.AddColumnAlias(Customer.FieldFlagApp, "审核");
        // winGridViewPager1.AddColumnAlias(Customer.FieldAppUser, "审核人");
        // winGridViewPager1.AddColumnAlias(Customer.FieldAppDate, "审核日期");

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridViewPager1.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridViewPager1.SetColumnDataSource(Customer.FieldTranNode, GB.AllOuDict);
        winGridViewPager1.SetColumnDataSource(Customer.FieldAreaNo, GB.AllRegions);
        winGridViewPager1.SetColumnDataSource(Customer.FieldPaymentType, "付款方式");
        winGridViewPager1.SetColumnDataSource(Customer.FieldCommissionType, "计价方式");
        winGridViewPager1.SetColumnDataSource(Customer.FieldSalesDeputy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Customer.FieldProjectManager, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Customer.FieldSalesPerson, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Customer.FieldCreatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Customer.FieldLastUpdatedBy, GB.AllUserDict);
        winGridViewPager1.SetColumnDataSource(Customer.FieldAppUser, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行

        winGridViewPager1.AddColumnSummaryItem(Customer.FieldCustomerCode, SummaryItemType.Count, "{0}");
        winGridViewPager1.ShowFooter = true;

        #endregion

        #endregion

        #region 明细表列

        // 显示列的内容和排序，如不设置则默认按照别名解析排列
        // winGridView2.DisplayColumns = "ISID,ContactPerson,NativeName,Address,AreaNo,Tel,Mobile,InsuranceRate,Coordinate,DefaultToNode,DefaultToNodes,DefaultToNodesName,CargoName,PackageType,CargoUnit,Price,PriceType,CreationDate,CreatedBy,LastUpdateDate,LastUpdatedBy";

        #region 添加别名解析，两种方式均可，尽量从后台取

        #region 前端直接定义

        // winGridView2.AddColumnAlias(Customers.FieldISID, "自增ID");
        // winGridView2.AddColumnAlias(Customers.FieldContactPerson, "联系人");
        // winGridView2.AddColumnAlias(Customers.FieldNativeName, "公司名称");
        // winGridView2.AddColumnAlias(Customers.FieldAddress, "地址");
        // winGridView2.AddColumnAlias(Customers.FieldAreaNo, "区");
        // winGridView2.AddColumnAlias(Customers.FieldTel, "电话");
        // winGridView2.AddColumnAlias(Customers.FieldMobile, "手机");
        // winGridView2.AddColumnAlias(Customers.FieldInsuranceRate, "保险费率");
        // winGridView2.AddColumnAlias(Customers.FieldCoordinate, "坐标");
        // winGridView2.AddColumnAlias(Customers.FieldDefaultToNode, "默认网点");
        // winGridView2.AddColumnAlias(Customers.FieldDefaultToNodes, "默认区域");
        // winGridView2.AddColumnAlias(Customers.FieldDefaultToNodesName, "默认区域名称");
        // winGridView2.AddColumnAlias(Customers.FieldCargoName, "货物名称");
        // winGridView2.AddColumnAlias(Customers.FieldPackageType, "包装方式");
        // winGridView2.AddColumnAlias(Customers.FieldCargoUnit, "货物单位");
        // winGridView2.AddColumnAlias(Customers.FieldPrice, "单价");
        // winGridView2.AddColumnAlias(Customers.FieldPriceType, "计价方式");
        // winGridView2.AddColumnAlias(Customers.FieldCreationDate, "创建日期");
        // winGridView2.AddColumnAlias(Customers.FieldCreatedBy, "创建人");
        // winGridView2.AddColumnAlias(Customers.FieldLastUpdateDate, "更新日期");
        // winGridView2.AddColumnAlias(Customers.FieldLastUpdatedBy, "更新人");

        #endregion

        // 获取字段显示权限，并设置(默认不使用字段权限)
        // winGridView2.gridView1.SetColumnsPermit(permitDict);

        #endregion

        #region 添加列字段数据源

        winGridView2.SetColumnDataSource(Customers.FieldAreaNo, GB.AllRegions);
        winGridView2.SetColumnDataSource(Customers.FieldDefaultToNode, GB.AllOuDict);
        winGridView2.SetColumnDataSource(Customers.FieldPackageType, "包装方式");
        winGridView2.SetColumnDataSource(Customers.FieldPriceType, "计价方式");
        winGridView2.SetColumnDataSource(Customers.FieldCreatedBy, GB.AllUserDict);
        winGridView2.SetColumnDataSource(Customers.FieldLastUpdatedBy, GB.AllUserDict);

        #endregion

        #region 添加列字段汇总行

        winGridView2.AddColumnSummaryItem(Customers.FieldISID, SummaryItemType.Count, "{0}");
        winGridView2.ShowFooter = true;

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
            //     gridView.SetGridColumWidth(Customer.FieldCustomerCode, 200);
            //     gridView.SetGridColumWidth(Customer.FieldMnemonicCode, 200);
            //     gridView.SetGridColumWidth(Customer.FieldTranNode, 200);
            //     gridView.SetGridColumWidth(Customer.FieldContactPerson, 200);
            //     gridView.SetGridColumWidth(Customer.FieldNativeName, 200);
            //     gridView.SetGridColumWidth(Customer.FieldAddress, 200);
            //     gridView.SetGridColumWidth(Customer.FieldAreaNo, 200);
            //     gridView.SetGridColumWidth(Customer.FieldTel, 200);
            //     gridView.SetGridColumWidth(Customer.FieldMobile, 200);
            //     gridView.SetGridColumWidth(Customer.FieldBank, 200);
            //     gridView.SetGridColumWidth(Customer.FieldBankAccount, 200);
            //     gridView.SetGridColumWidth(Customer.FieldRemark, 200);
            //     gridView.SetGridColumWidth(Customer.FieldInUse, 200);
            //     gridView.SetGridColumWidth(Customer.FieldPaymentType, 200);
            //     gridView.SetGridColumWidth(Customer.FieldInvoiceFax, 200);
            //     gridView.SetGridColumWidth(Customer.FieldCommissionType, 200);
            //     gridView.SetGridColumWidth(Customer.FieldCommissionRate, 200);
            //     gridView.SetGridColumWidth(Customer.FieldSalesDeputy, 200);
            //     gridView.SetGridColumWidth(Customer.FieldProjectManager, 200);
            //     gridView.SetGridColumWidth(Customer.FieldFlagInvoice, 200);
            //     gridView.SetGridColumWidth(Customer.FieldSalesPerson, 200);
            //     gridView.SetGridColumWidth(Customer.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(Customer.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(Customer.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(Customer.FieldLastUpdatedBy, 200);
            //     gridView.SetGridColumWidth(Customer.FieldFlagApp, 200);
            //     gridView.SetGridColumWidth(Customer.FieldAppUser, 200);
            //     gridView.SetGridColumWidth(Customer.FieldAppDate, 200);
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
            //     gridView.SetGridColumWidth(Customers.FieldISID, 200);
            //     gridView.SetGridColumWidth(Customers.FieldContactPerson, 200);
            //     gridView.SetGridColumWidth(Customers.FieldNativeName, 200);
            //     gridView.SetGridColumWidth(Customers.FieldAddress, 200);
            //     gridView.SetGridColumWidth(Customers.FieldAreaNo, 200);
            //     gridView.SetGridColumWidth(Customers.FieldTel, 200);
            //     gridView.SetGridColumWidth(Customers.FieldMobile, 200);
            //     gridView.SetGridColumWidth(Customers.FieldInsuranceRate, 200);
            //     gridView.SetGridColumWidth(Customers.FieldCoordinate, 200);
            //     gridView.SetGridColumWidth(Customers.FieldDefaultToNode, 200);
            //     gridView.SetGridColumWidth(Customers.FieldDefaultToNodes, 200);
            //     gridView.SetGridColumWidth(Customers.FieldDefaultToNodesName, 200);
            //     gridView.SetGridColumWidth(Customers.FieldCargoName, 200);
            //     gridView.SetGridColumWidth(Customers.FieldPackageType, 200);
            //     gridView.SetGridColumWidth(Customers.FieldCargoUnit, 200);
            //     gridView.SetGridColumWidth(Customers.FieldPrice, 200);
            //     gridView.SetGridColumWidth(Customers.FieldPriceType, 200);
            //     gridView.SetGridColumWidth(Customers.FieldCreationDate, 200);
            //     gridView.SetGridColumWidth(Customers.FieldCreatedBy, 200);
            //     gridView.SetGridColumWidth(Customers.FieldLastUpdateDate, 200);
            //     gridView.SetGridColumWidth(Customers.FieldLastUpdatedBy, 200);
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
            { Customer.FieldCustomerCode, txtCustomerCode.Text.Trim() },
            { Customer.FieldMnemonicCode, txtMnemonicCode.Text.Trim() },
            { Customer.FieldContactPerson, txtContactPerson.Text.Trim() },
            { Customer.FieldNativeName, txtNativeName.Text.Trim() },
            { Customer.FieldAddress, txtAddress.Text.Trim() },
            { Customer.FieldAreaNo, txtAreaNo.EditValue.ObjToStr() },
            { Customer.FieldTel, txtTel.Text.Trim() },
            { Customer.FieldMobile, txtMobile.Text.Trim() },
            { Customer.FieldCreationDate, txtCreationDate1.EditValue.ObjToStr() },
            { Customer.FieldCreationDate, txtCreationDate2.EditValue.ObjToStr() },
            { Customer.FieldFlagApp, txtFlagApp.GetComboBoxValue() },
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
        var info = new Customer
        {
            CustomerCode = GetRowData(dr, "公司编号"),
            MnemonicCode = GetRowData(dr, "助记码"),
            TranNode = GetRowData(dr, "网点编号"),
            ContactPerson = GetRowData(dr, "联系人"),
            NativeName = GetRowData(dr, "公司名称"),
            Address = GetRowData(dr, "地址"),
            AreaNo = GetRowData(dr, "区"),
            Tel = GetRowData(dr, "电话号码"),
            Mobile = GetRowData(dr, "手机号"),
            Bank = GetRowData(dr, "开户行"),
            BankAccount = GetRowData(dr, "银行账号"),
            Remark = GetRowData(dr, "备注"),
            InUse = GetRowData(dr, "是否在用(Y/N）") == "是",
            PaymentType = GetRowData(dr, "付款方式"),
            InvoiceFax = GetRowData(dr, "税率").ToDecimal(),
            CommissionType = GetRowData(dr, "业务员提成方式"),
            CommissionRate = GetRowData(dr, "提成比例").ToDecimal(),
            SalesDeputy = GetRowData(dr, "客服"),
            ProjectManager = GetRowData(dr, "项目主管"),
            FlagInvoice = GetRowData(dr, "开票") == "是",
            SalesPerson = GetRowData(dr, "业务员"),
            CreationDate = GetRowData(dr, "创建日期").ToDateTime(dtNow),
            CreatedBy = GetRowData(dr, "创建人"),
            LastUpdateDate = GetRowData(dr, "更新日期").ToDateTime(dtNow),
            LastUpdatedBy = GetRowData(dr, "更新人"),
            FlagApp = GetRowData(dr, "审核") == "是",
            AppUser = GetRowData(dr, "审核人"),
            AppDate = GetRowData(dr, "审核日期").ToDateTime(dtNow),
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
        List<Customer> list = await _bll.FindAsync(condition);
        DataTable dtNew = DataTableHelper.CreateTable(
            "公司编号,助记码,网点编号,联系人,公司名称,地址,区,电话号码,手机号,开户行,银行账号,备注,是否在用(Y/N）,付款方式,税率,业务员提成方式,提成比例,客服,项目主管,开票,业务员,创建日期,创建人,更新日期,更新人,审核,审核人,审核日期");
        var j = 1;
        foreach (Customer t in list)
        {
            DataRow dr = dtNew.NewRow();
            dr["序号"] = j++;
            dr["公司编号"] = t.CustomerCode;
            dr["助记码"] = t.MnemonicCode;
            dr["网点编号"] = t.TranNode;
            dr["联系人"] = t.ContactPerson;
            dr["公司名称"] = t.NativeName;
            dr["地址"] = t.Address;
            dr["区"] = t.AreaNo;
            dr["电话号码"] = t.Tel;
            dr["手机号"] = t.Mobile;
            dr["开户行"] = t.Bank;
            dr["银行账号"] = t.BankAccount;
            dr["备注"] = t.Remark;
            dr["是否在用(Y/N）"] = t.InUse ? "是" : "否";
            dr["付款方式"] = t.PaymentType;
            dr["税率"] = t.InvoiceFax;
            dr["业务员提成方式"] = t.CommissionType;
            dr["提成比例"] = t.CommissionRate;
            dr["客服"] = t.SalesDeputy;
            dr["项目主管"] = t.ProjectManager;
            dr["开票"] = t.FlagInvoice ? "是" : "否";
            dr["业务员"] = t.SalesPerson;
            dr["创建日期"] = t.CreationDate;
            dr["创建人"] = t.CreatedBy;
            dr["更新日期"] = t.LastUpdateDate;
            dr["更新人"] = t.LastUpdatedBy;
            dr["审核"] = t.FlagApp ? "是" : "否";
            dr["审核人"] = t.AppUser;
            dr["审核日期"] = t.AppDate;
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
            AdvDlg?.AddColumnListItem(Customer.FieldTranNode, GB.AllOuDict);
            AdvDlg?.AddColumnListItem(Customer.FieldPaymentType, "付款方式");
            AdvDlg?.AddColumnListItem(Customer.FieldCommissionType, "计价方式");
            AdvDlg?.AddColumnListItem(Customer.FieldSalesDeputy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Customer.FieldProjectManager, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Customer.FieldSalesPerson, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Customer.FieldCreatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Customer.FieldLastUpdatedBy, GB.AllUserDict);
            AdvDlg?.AddColumnListItem(Customer.FieldAppUser, GB.AllUserDict);

            #endregion

        AdvDlg?.ShowDialog();
    }

    #endregion
}