using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 网点区域 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Nodes : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public Nodes()
    {
    }

    #region Property Members

    /// <summary>
    /// 自增ID
    /// </summary>
    [DataMember]
    [Key]
    [Identity]
    [Sort(IsDesc)]
    [Column(FieldISID, DisISID)]
    public int ISID { get; set; }

    /// <summary>
    /// 网点ID
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldTranNodeNO, DisTranNodeNO)]
    public string TranNodeNO { get; set; }

    /// <summary>
    /// 区域名称
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeAreaName, DisTranNodeAreaName)]
    public string TranNodeAreaName { get; set; }

    /// <summary>
    /// 区域详情
    /// </summary>
    [DataMember]
    [Column(FieldTranNodeAreaDesc, DisTranNodeAreaDesc)]
    public string TranNodeAreaDesc { get; set; }

    /// <summary>
    /// 交货方式
    /// </summary>
    [DataMember]
    [Column(FieldDeliveryType, DisDeliveryType)]
    public string DeliveryType { get; set; }

    /// <summary>
    /// 电子围栏
    /// </summary>
    [DataMember]
    [Column(FieldEFence, DisEFence)]
    public string EFence { get; set; }

    /// <summary>
    /// 中心坐标
    /// </summary>
    [DataMember]
    [Column(FieldCenterCoordinate, DisCenterCoordinate)]
    public string CenterCoordinate { get; set; }

    /// <summary>
    /// 重泡比
    /// </summary>
    [DataMember]
    [Column(FieldConvertVK, DisConvertVK)]
    public decimal ConvertVK { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldProvinceId, DisProvinceId)]
    public string ProvinceId { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCityId, DisCityId)]
    public string CityId { get; set; }

    /// <summary>
    /// 区
    /// </summary>
    [DataMember]
    [Column(FieldAreaId, DisAreaId)]
    public string AreaId { get; set; }

    /// <summary>
    /// 镇
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldTownId, DisTownId)]
    public string TownId { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [DataMember]
    [Column(FieldAddress, DisAddress)]
    public string Address { get; set; }

    /// <summary>
    /// 负责人
    /// </summary>
    [DataMember]
    [Column(FieldPerson, DisPerson)]
    public string Person { get; set; }

    /// <summary>
    /// 联系方式
    /// </summary>
    [DataMember]
    [Column(FieldPhone, DisPhone)]
    public string Phone { get; set; }

    /// <summary>
    /// 到件签收时效
    /// </summary>
    [DataMember]
    [Column(FieldSignLimitHour, DisSignLimitHour)]
    public decimal SignLimitHour { get; set; }

    /// <summary>
    /// 作废
    /// </summary>
    [DataMember]
    [Column(FieldCancelYN, DisCancelYN)]
    public bool CancelYN { get; set; }

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
    [Hide]
    [Column(FieldCreationDate, DisCreationDate)]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCreatedBy, DisCreatedBy)]
    public string CreatedBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [DataMember]
    [Hide]
    [OptimisticLock]
    [Column(FieldLastUpdateDate, DisLastUpdateDate)]
    public DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldLastUpdatedBy, DisLastUpdatedBy)]
    public string LastUpdatedBy { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranNodes";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

    /// <summary>
    /// 外键名
    /// </summary>
    [NonSerialized]
    public new const string ForeignKey = FieldTranNodeNO;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldISID;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public new const bool IsDesc = true;

    /// <summary>
    /// 乐观锁字段
    /// </summary>
    [NonSerialized]
    public new const string OptimisticLockKey = FieldLastUpdateDate;

    #region 列名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string FieldISID = "ISID";

    /// <summary>
    /// 网点ID
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeNO = "TranNodeNO";

    /// <summary>
    /// 区域名称
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeAreaName = "TranNodeAreaName";

    /// <summary>
    /// 区域详情
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodeAreaDesc = "TranNodeAreaDesc";

    /// <summary>
    /// 交货方式
    /// </summary>
    [NonSerialized]
    public const string FieldDeliveryType = "DeliveryType";

    /// <summary>
    /// 电子围栏
    /// </summary>
    [NonSerialized]
    public const string FieldEFence = "EFence";

    /// <summary>
    /// 中心坐标
    /// </summary>
    [NonSerialized]
    public const string FieldCenterCoordinate = "CenterCoordinate";

    /// <summary>
    /// 重泡比
    /// </summary>
    [NonSerialized]
    public const string FieldConvertVK = "ConvertVK";

    /// <summary>
    /// 省
    /// </summary>
    [NonSerialized]
    public const string FieldProvinceId = "ProvinceId";

    /// <summary>
    /// 市
    /// </summary>
    [NonSerialized]
    public const string FieldCityId = "CityId";

    /// <summary>
    /// 区
    /// </summary>
    [NonSerialized]
    public const string FieldAreaId = "AreaId";

    /// <summary>
    /// 镇
    /// </summary>
    [NonSerialized]
    public const string FieldTownId = "TownId";

    /// <summary>
    /// 地址
    /// </summary>
    [NonSerialized]
    public const string FieldAddress = "Address";

    /// <summary>
    /// 负责人
    /// </summary>
    [NonSerialized]
    public const string FieldPerson = "Person";

    /// <summary>
    /// 联系方式
    /// </summary>
    [NonSerialized]
    public const string FieldPhone = "Phone";

    /// <summary>
    /// 到件签收时效
    /// </summary>
    [NonSerialized]
    public const string FieldSignLimitHour = "SignLimitHour";

    /// <summary>
    /// 作废
    /// </summary>
    [NonSerialized]
    public const string FieldCancelYN = "CancelYN";

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
    /// 更新时间
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdateDate = "LastUpdateDate";

    /// <summary>
    /// 更新人
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdatedBy = "LastUpdatedBy";

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 网点ID
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeNO = "网点ID";

    /// <summary>
    /// 区域名称
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeAreaName = "区域名称";

    /// <summary>
    /// 区域详情
    /// </summary>
    [NonSerialized]
    public const string DisTranNodeAreaDesc = "区域详情";

    /// <summary>
    /// 交货方式
    /// </summary>
    [NonSerialized]
    public const string DisDeliveryType = "交货方式";

    /// <summary>
    /// 电子围栏
    /// </summary>
    [NonSerialized]
    public const string DisEFence = "电子围栏";

    /// <summary>
    /// 中心坐标
    /// </summary>
    [NonSerialized]
    public const string DisCenterCoordinate = "中心坐标";

    /// <summary>
    /// 重泡比
    /// </summary>
    [NonSerialized]
    public const string DisConvertVK = "重泡比";

    /// <summary>
    /// 省
    /// </summary>
    [NonSerialized]
    public const string DisProvinceId = "省";

    /// <summary>
    /// 市
    /// </summary>
    [NonSerialized]
    public const string DisCityId = "市";

    /// <summary>
    /// 区
    /// </summary>
    [NonSerialized]
    public const string DisAreaId = "区";

    /// <summary>
    /// 镇
    /// </summary>
    [NonSerialized]
    public const string DisTownId = "镇";

    /// <summary>
    /// 地址
    /// </summary>
    [NonSerialized]
    public const string DisAddress = "地址";

    /// <summary>
    /// 负责人
    /// </summary>
    [NonSerialized]
    public const string DisPerson = "负责人";

    /// <summary>
    /// 联系方式
    /// </summary>
    [NonSerialized]
    public const string DisPhone = "联系方式";

    /// <summary>
    /// 到件签收时效
    /// </summary>
    [NonSerialized]
    public const string DisSignLimitHour = "到件签收时效";

    /// <summary>
    /// 作废
    /// </summary>
    [NonSerialized]
    public const string DisCancelYN = "作废";

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
    /// 更新时间
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdateDate = "更新时间";

    /// <summary>
    /// 更新人
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdatedBy = "更新人";

    #endregion

    #endregion
}