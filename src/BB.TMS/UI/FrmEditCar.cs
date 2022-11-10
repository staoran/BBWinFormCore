using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.BaseUI.Other;
using BB.Entity.TMS;
using BB.HttpServices.TMS;
using FluentValidation;

namespace BB.TMS.UI;

/// <summary>
/// 车辆档案
/// </summary>
public partial class FrmEditCar : BaseEditForm<Car, CarHttpService>
{
    public FrmEditCar(CarHttpService bll, IValidator<Car> validator) : base(bll, validator)
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
    /// 初始化数据字典
    /// </summary>
    protected override Task InitDictItem()
    {
        //初始化代码
        txtTranNode.BindDictItems(GB.AllOuDict, GB.LoginUserInfo.CompanyId, false, false);
        txtProperty.BindDictItems("车辆性质", null, false, false);
        txtModel.BindDictItems("车型", null, false, false);
        txtCarType.BindDictItems("车体状况", null, false, false);
        txtServiceRange.BindDictItems("服务范围", null, false, false);
        txtTrustLevel.BindDictItems("信誉类型", null, false, false);
        txtCreatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), true, false);
        txtLastUpdatedBy.BindDictItems(GB.AllUserDict, GB.LoginUserInfo.ID.ToString(), true, false);
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

        txtTranNode.Tag = Car.FieldTranNode;
        txtCarNo.Tag = Car.FieldCarNo;
        txtProperty.Tag = Car.FieldProperty;
        txtModel.Tag = Car.FieldModel;
        txtCarType.Tag = Car.FieldCarType;
        txtServiceRange.Tag = Car.FieldServiceRange;
        txtTrustLevel.Tag = Car.FieldTrustLevel;
        txtTargetPlace.Tag = Car.FieldTargetPlace;
        txtBuyDate.Tag = Car.FieldBuyDate;
        txtOperationNo.Tag = Car.FieldOperationNo;
        txtEngineNo.Tag = Car.FieldEngineNo;
        txtVIN.Tag = Car.FieldVIN;
        txtTonnage.Tag = Car.FieldTonnage;
        txtLong.Tag = Car.FieldLong;
        txtWidth.Tag = Car.FieldWidth;
        txtHeight.Tag = Car.FieldHeight;
        txtVolume.Tag = Car.FieldVolume;
        txtContact.Tag = Car.FieldContact;
        txtContactTel.Tag = Car.FieldContactTel;
        txtDriver.Tag = Car.FieldDriver;
        txtDriverCert.Tag = Car.FieldDriverCert;
        txtDriverMobile.Tag = Car.FieldDriverMobile;
        txtDriverTel.Tag = Car.FieldDriverTel;
        txtDriverHomeAddress.Tag = Car.FieldDriverHomeAddress;
        txtRemark.Tag = Car.FieldRemark;
        txtCreationDate.Tag = Car.FieldCreationDate;
        txtCreatedBy.Tag = Car.FieldCreatedBy;
        txtLastUpdateDate.Tag = Car.FieldLastUpdateDate;
        txtLastUpdatedBy.Tag = Car.FieldLastUpdatedBy;
        txtFlagApp.Tag = Car.FieldFlagApp;
        txtAppUser.Tag = Car.FieldAppUser;
        txtAppDate.Tag = Car.FieldAppDate;

        #endregion

         await base.SetPermit();
    }

    // /// <summary>
    // /// 查看编辑附件信息
    // /// </summary>
    //private void SetAttachInfo(CarInfo info)
    //{
    //    this.attachmentGUID.AttachmentGUID = info.AttachGUID;
    //    this.attachmentGUID.userId = LoginUserInfo.Name;

    //    string name = "车辆档案";
    //    if (!string.IsNullOrEmpty(name))
    //    {
    //        string dir = string.Format("{0}", name);
    //        this.attachmentGUID.Init(dir, info.OrgCode, LoginUserInfo.Name);
    //    }
    //}
}
