using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 车辆档案 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Car : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public Car()
    {
    }

    #region Property Members

    /// <summary>
    /// 网点编码
    /// </summary>
    [DataMember]
    [Column(FieldTranNode, DisTranNode)]
    public string TranNode { get; set; }

    /// <summary>
    /// 车牌号
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldCarNo, DisCarNo)]
    public string CarNo { get; set; }

    /// <summary>
    /// 车辆性质
    /// </summary>
    [DataMember]
    [Column(FieldProperty, DisProperty)]
    public string Property { get; set; }

    /// <summary>
    /// 车型
    /// </summary>
    [DataMember]
    [Column(FieldModel, DisModel)]
    public string Model { get; set; }

    /// <summary>
    /// 车体状况
    /// </summary>
    [DataMember]
    [Column(FieldCarType, DisCarType)]
    public string CarType { get; set; }

    /// <summary>
    /// 服务范围
    /// </summary>
    [DataMember]
    [Column(FieldServiceRange, DisServiceRange)]
    public string ServiceRange { get; set; }

    /// <summary>
    /// 信誉
    /// </summary>
    [DataMember]
    [Column(FieldTrustLevel, DisTrustLevel)]
    public string TrustLevel { get; set; }

    /// <summary>
    /// 主营路线
    /// </summary>
    [DataMember]
    [Column(FieldTargetPlace, DisTargetPlace)]
    public string TargetPlace { get; set; }

    /// <summary>
    /// 购买日期
    /// </summary>
    [DataMember]
    [Column(FieldBuyDate, DisBuyDate)]
    public DateTime? BuyDate { get; set; }

    /// <summary>
    /// 运营编号
    /// </summary>
    [DataMember]
    [Column(FieldOperationNo, DisOperationNo)]
    public string OperationNo { get; set; }

    /// <summary>
    /// 发动机号
    /// </summary>
    [DataMember]
    [Column(FieldEngineNo, DisEngineNo)]
    public string EngineNo { get; set; }

    /// <summary>
    /// 车架号
    /// </summary>
    [DataMember]
    [Column(FieldVIN, DisVIN)]
    public string VIN { get; set; }

    /// <summary>
    /// 载重
    /// </summary>
    [DataMember]
    [Column(FieldTonnage, DisTonnage)]
    public string Tonnage { get; set; }

    /// <summary>
    /// 长
    /// </summary>
    [DataMember]
    [Column(FieldLong, DisLong)]
    public decimal Long { get; set; }

    /// <summary>
    /// 宽
    /// </summary>
    [DataMember]
    [Column(FieldWidth, DisWidth)]
    public decimal Width { get; set; }

    /// <summary>
    /// 高
    /// </summary>
    [DataMember]
    [Column(FieldHeight, DisHeight)]
    public decimal Height { get; set; }

    /// <summary>
    /// 体积
    /// </summary>
    [DataMember]
    [Column(FieldVolume, DisVolume)]
    public decimal Volume { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [DataMember]
    [Column(FieldContact, DisContact)]
    public string Contact { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [DataMember]
    [Column(FieldContactTel, DisContactTel)]
    public string ContactTel { get; set; }

    /// <summary>
    /// 驾驶员
    /// </summary>
    [DataMember]
    [Column(FieldDriver, DisDriver)]
    public string Driver { get; set; }

    /// <summary>
    /// 驾驶证号
    /// </summary>
    [DataMember]
    [Column(FieldDriverCert, DisDriverCert)]
    public string DriverCert { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [DataMember]
    [Column(FieldDriverMobile, DisDriverMobile)]
    public string DriverMobile { get; set; }

    /// <summary>
    /// 固定电话
    /// </summary>
    [DataMember]
    [Column(FieldDriverTel, DisDriverTel)]
    public string DriverTel { get; set; }

    /// <summary>
    /// 家庭住址
    /// </summary>
    [DataMember]
    [Column(FieldDriverHomeAddress, DisDriverHomeAddress)]
    public string DriverHomeAddress { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldRemark, DisRemark)]
    public string Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Column(FieldCreationDate, DisCreationDate)]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [DataMember]
    [Column(FieldCreatedBy, DisCreatedBy)]
    public string CreatedBy { get; set; }

    /// <summary>
    /// 最后修改时间
    /// </summary>
    [DataMember]
    [Column(FieldLastUpdateDate, DisLastUpdateDate)]
    public DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// 最后修改人
    /// </summary>
    [DataMember]
    [Column(FieldLastUpdatedBy, DisLastUpdatedBy)]
    public string LastUpdatedBy { get; set; }

    /// <summary>
    /// 审核
    /// </summary>
    [DataMember]
    [Column(FieldFlagApp, DisFlagApp)]
    public bool FlagApp { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    [DataMember]
    [Column(FieldAppUser, DisAppUser)]
    public string AppUser { get; set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    [DataMember]
    [Column(FieldAppDate, DisAppDate)]
    public DateTime? AppDate { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "dt_CarInfo";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldCarNo;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldCreationDate;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public new const bool IsDesc = false;

    /// <summary>
    /// 乐观锁字段
    /// </summary>
    [NonSerialized]
    public new const string OptimisticLockKey = FieldLastUpdateDate;

    #region 列名
    /// <summary>
    /// 网点编码
    /// </summary>
    [NonSerialized]
    public const string FieldTranNode = "TranNode";

    /// <summary>
    /// 车牌号
    /// </summary>
    [NonSerialized]
    public const string FieldCarNo = "CarNo";

    /// <summary>
    /// 车辆性质
    /// </summary>
    [NonSerialized]
    public const string FieldProperty = "Property";

    /// <summary>
    /// 车型
    /// </summary>
    [NonSerialized]
    public const string FieldModel = "Model";

    /// <summary>
    /// 车体状况
    /// </summary>
    [NonSerialized]
    public const string FieldCarType = "CarType";

    /// <summary>
    /// 服务范围
    /// </summary>
    [NonSerialized]
    public const string FieldServiceRange = "ServiceRange";

    /// <summary>
    /// 信誉
    /// </summary>
    [NonSerialized]
    public const string FieldTrustLevel = "TrustLevel";

    /// <summary>
    /// 主营路线
    /// </summary>
    [NonSerialized]
    public const string FieldTargetPlace = "TargetPlace";

    /// <summary>
    /// 购买日期
    /// </summary>
    [NonSerialized]
    public const string FieldBuyDate = "BuyDate";

    /// <summary>
    /// 运营编号
    /// </summary>
    [NonSerialized]
    public const string FieldOperationNo = "OperationNo";

    /// <summary>
    /// 发动机号
    /// </summary>
    [NonSerialized]
    public const string FieldEngineNo = "EngineNo";

    /// <summary>
    /// 车架号
    /// </summary>
    [NonSerialized]
    public const string FieldVIN = "VIN";

    /// <summary>
    /// 载重
    /// </summary>
    [NonSerialized]
    public const string FieldTonnage = "Tonnage";

    /// <summary>
    /// 长
    /// </summary>
    [NonSerialized]
    public const string FieldLong = "Long";

    /// <summary>
    /// 宽
    /// </summary>
    [NonSerialized]
    public const string FieldWidth = "Width";

    /// <summary>
    /// 高
    /// </summary>
    [NonSerialized]
    public const string FieldHeight = "Height";

    /// <summary>
    /// 体积
    /// </summary>
    [NonSerialized]
    public const string FieldVolume = "Volume";

    /// <summary>
    /// 联系人
    /// </summary>
    [NonSerialized]
    public const string FieldContact = "Contact";

    /// <summary>
    /// 联系电话
    /// </summary>
    [NonSerialized]
    public const string FieldContactTel = "ContactTel";

    /// <summary>
    /// 驾驶员
    /// </summary>
    [NonSerialized]
    public const string FieldDriver = "Driver";

    /// <summary>
    /// 驾驶证号
    /// </summary>
    [NonSerialized]
    public const string FieldDriverCert = "DriverCert";

    /// <summary>
    /// 手机号码
    /// </summary>
    [NonSerialized]
    public const string FieldDriverMobile = "DriverMobile";

    /// <summary>
    /// 固定电话
    /// </summary>
    [NonSerialized]
    public const string FieldDriverTel = "DriverTel";

    /// <summary>
    /// 家庭住址
    /// </summary>
    [NonSerialized]
    public const string FieldDriverHomeAddress = "DriverHomeAddress";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldRemark = "Remark";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string FieldCreationDate = "CreationDate";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string FieldCreatedBy = "CreatedBy";

    /// <summary>
    /// 最后修改时间
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdateDate = "LastUpdateDate";

    /// <summary>
    /// 最后修改人
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdatedBy = "LastUpdatedBy";

    /// <summary>
    /// 审核
    /// </summary>
    [NonSerialized]
    public const string FieldFlagApp = "FlagApp";

    /// <summary>
    /// 审核人
    /// </summary>
    [NonSerialized]
    public const string FieldAppUser = "AppUser";

    /// <summary>
    /// 审核时间
    /// </summary>
    [NonSerialized]
    public const string FieldAppDate = "AppDate";

    #endregion

    #region 列显示名
    /// <summary>
    /// 网点编码
    /// </summary>
    [NonSerialized]
    public const string DisTranNode = "网点编码";

    /// <summary>
    /// 车牌号
    /// </summary>
    [NonSerialized]
    public const string DisCarNo = "车牌号";

    /// <summary>
    /// 车辆性质
    /// </summary>
    [NonSerialized]
    public const string DisProperty = "车辆性质";

    /// <summary>
    /// 车型
    /// </summary>
    [NonSerialized]
    public const string DisModel = "车型";

    /// <summary>
    /// 车体状况
    /// </summary>
    [NonSerialized]
    public const string DisCarType = "车体状况";

    /// <summary>
    /// 服务范围
    /// </summary>
    [NonSerialized]
    public const string DisServiceRange = "服务范围";

    /// <summary>
    /// 信誉
    /// </summary>
    [NonSerialized]
    public const string DisTrustLevel = "信誉";

    /// <summary>
    /// 主营路线
    /// </summary>
    [NonSerialized]
    public const string DisTargetPlace = "主营路线";

    /// <summary>
    /// 购买日期
    /// </summary>
    [NonSerialized]
    public const string DisBuyDate = "购买日期";

    /// <summary>
    /// 运营编号
    /// </summary>
    [NonSerialized]
    public const string DisOperationNo = "运营编号";

    /// <summary>
    /// 发动机号
    /// </summary>
    [NonSerialized]
    public const string DisEngineNo = "发动机号";

    /// <summary>
    /// 车架号
    /// </summary>
    [NonSerialized]
    public const string DisVIN = "车架号";

    /// <summary>
    /// 载重
    /// </summary>
    [NonSerialized]
    public const string DisTonnage = "载重";

    /// <summary>
    /// 长
    /// </summary>
    [NonSerialized]
    public const string DisLong = "长";

    /// <summary>
    /// 宽
    /// </summary>
    [NonSerialized]
    public const string DisWidth = "宽";

    /// <summary>
    /// 高
    /// </summary>
    [NonSerialized]
    public const string DisHeight = "高";

    /// <summary>
    /// 体积
    /// </summary>
    [NonSerialized]
    public const string DisVolume = "体积";

    /// <summary>
    /// 联系人
    /// </summary>
    [NonSerialized]
    public const string DisContact = "联系人";

    /// <summary>
    /// 联系电话
    /// </summary>
    [NonSerialized]
    public const string DisContactTel = "联系电话";

    /// <summary>
    /// 驾驶员
    /// </summary>
    [NonSerialized]
    public const string DisDriver = "驾驶员";

    /// <summary>
    /// 驾驶证号
    /// </summary>
    [NonSerialized]
    public const string DisDriverCert = "驾驶证号";

    /// <summary>
    /// 手机号码
    /// </summary>
    [NonSerialized]
    public const string DisDriverMobile = "手机号码";

    /// <summary>
    /// 固定电话
    /// </summary>
    [NonSerialized]
    public const string DisDriverTel = "固定电话";

    /// <summary>
    /// 家庭住址
    /// </summary>
    [NonSerialized]
    public const string DisDriverHomeAddress = "家庭住址";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string DisRemark = "备注";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string DisCreationDate = "创建时间";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string DisCreatedBy = "创建人";

    /// <summary>
    /// 最后修改时间
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdateDate = "最后修改时间";

    /// <summary>
    /// 最后修改人
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdatedBy = "最后修改人";

    /// <summary>
    /// 审核
    /// </summary>
    [NonSerialized]
    public const string DisFlagApp = "审核";

    /// <summary>
    /// 审核人
    /// </summary>
    [NonSerialized]
    public const string DisAppUser = "审核人";

    /// <summary>
    /// 审核时间
    /// </summary>
    [NonSerialized]
    public const string DisAppDate = "审核时间";

    #endregion

    #endregion
}