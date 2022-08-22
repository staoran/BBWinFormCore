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
/// 客户资料
/// </summary>
public partial class FrmEditCustomer : BaseEditForm<Customer, CustomerHttpService, Customers, CustomersHttpService>
{
    public FrmEditCustomer(CustomerHttpService bll, CustomersHttpService childBll, IValidator<Customer> validator, IValidator<Customers> childValidator) : base(bll, childBll, validator, childValidator)
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
        gridView1.CreateColumn(Customers.FieldISID, "自增ID", 100).Visible = false;
        gridView1.CreateColumn(Customers.FieldCustomerCode, "公司编号", 100).Visible = false;
        gridView1.CreateColumn(Customers.FieldTranNode, "网点编号", 100).Visible = false;
        gridView1.CreateColumn(Customers.FieldContactPerson, "联系人", 100).CreateTextEdit();
        gridView1.CreateColumn(Customers.FieldNativeName, "公司名称", 100).CreateTextEdit();
        gridView1.CreateColumn(Customers.FieldAddress, "地址", 100).CreateMemoEdit();
        gridView1.CreateColumn(Customers.FieldAreaNo, "区", 100).CreateSearchLookUpEdit().BindCityItems();
        gridView1.CreateColumn(Customers.FieldTel, "电话", 100).CreateTextEdit();
        gridView1.CreateColumn(Customers.FieldMobile, "手机", 100).CreateTextEdit();
        gridView1.CreateColumn(Customers.FieldInsuranceRate, "保险费率", 100).CreateSpinEdit();
        gridView1.CreateColumn(Customers.FieldCoordinate, "坐标", 100).CreateTextEdit();
        gridView1.CreateColumn(Customers.FieldDefaultToNode, "默认网点", 100).CreateComboBoxEdit().BindDictItems(GB.AllOuDict);
        gridView1.CreateColumn(Customers.FieldDefaultToNodes, "默认区域", 100).CreateComboBoxEdit();
        gridView1.CreateColumn(Customers.FieldDefaultToNodesName, "默认区域名称", 100).CreateTextEdit();
        gridView1.CreateColumn(Customers.FieldCargoName, "货物名称", 100).CreateTextEdit();
        gridView1.CreateColumn(Customers.FieldPackageType, "包装方式", 100).CreateComboBoxEdit().BindDictItems("包装方式");
        gridView1.CreateColumn(Customers.FieldCargoUnit, "货物单位", 100).CreateTextEdit();
        gridView1.CreateColumn(Customers.FieldPrice, "单价", 100).CreateSpinEdit();
        gridView1.CreateColumn(Customers.FieldPriceType, "计价方式", 100).CreateComboBoxEdit().BindDictItems("计价方式");
        gridView1.CreateColumn(Customers.FieldCreationDate, "创建日期", 100).Visible = false;
        gridView1.CreateColumn(Customers.FieldCreatedBy, "创建人", 100).Visible = false;
        gridView1.CreateColumn(Customers.FieldLastUpdateDate, "更新日期", 100).Visible = false;
        gridView1.CreateColumn(Customers.FieldLastUpdatedBy, "更新人", 100).Visible = false;

        gridView1.ViewCaption = @"客户资料明细";

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
        txtTranNode.BindDictItems(GB.AllOuDict, LoginUserInfo.CompanyId, false, false);
        txtAreaNo.BindCityItems(true);
        txtInUse.BindDictItems("是,否", true);
        txtPaymentType.BindDictItems("付款方式", null, false, false);
        txtCommissionType.BindDictItems("计价方式", null, true, false);
        txtSalesDeputy.BindDictItems(GB.AllUserDict, null, true, false);
        txtProjectManager.BindDictItems(GB.AllUserDict, null, true, false);
        txtFlagInvoice.BindDictItems("是,否", false);
        txtSalesPerson.BindDictItems(GB.AllUserDict, null, true, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, LoginUserInfo.ID.ToString(), false, false);
        txtFlagApp.BindDictItems("已审核,未审核", false);
        txtAppUser.BindDictItems(GB.AllUserDict, null, true, false);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 设置控件字段的权限显示或者隐藏(默认不使用字段权限)
    /// </summary>
    protected override Task SetPermit()
    {
        #region 设置控件和字段的对应关系，字段权限判断也用到，无权字段赋值：*

        txtCustomerCode.Tag = Customer.FieldCustomerCode;
        txtMnemonicCode.Tag = Customer.FieldMnemonicCode;
        txtTranNode.Tag = Customer.FieldTranNode;
        txtContactPerson.Tag = Customer.FieldContactPerson;
        txtNativeName.Tag = Customer.FieldNativeName;
        txtAddress.Tag = Customer.FieldAddress;
        txtAreaNo.Tag = Customer.FieldAreaNo;
        txtTel.Tag = Customer.FieldTel;
        txtMobile.Tag = Customer.FieldMobile;
        txtBank.Tag = Customer.FieldBank;
        txtBankAccount.Tag = Customer.FieldBankAccount;
        txtRemark.Tag = Customer.FieldRemark;
        txtInUse.Tag = Customer.FieldInUse;
        txtPaymentType.Tag = Customer.FieldPaymentType;
        txtInvoiceFax.Tag = Customer.FieldInvoiceFax;
        txtCommissionType.Tag = Customer.FieldCommissionType;
        txtCommissionRate.Tag = Customer.FieldCommissionRate;
        txtSalesDeputy.Tag = Customer.FieldSalesDeputy;
        txtProjectManager.Tag = Customer.FieldProjectManager;
        txtFlagInvoice.Tag = Customer.FieldFlagInvoice;
        txtSalesPerson.Tag = Customer.FieldSalesPerson;
        txtCreationDate.Tag = Customer.FieldCreationDate;
        txtCreatedBy.Tag = Customer.FieldCreatedBy;
        txtLastUpdateDate.Tag = Customer.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = Customer.FieldLastUpdatedBy;
        txtFlagApp.Tag = Customer.FieldFlagApp;
        txtAppUser.Tag = Customer.FieldAppUser;
        txtAppDate.Tag = Customer.FieldAppDate;

        #endregion

        base.SetPermit();
        return Task.CompletedTask;
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(CustomerInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "客户资料";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
