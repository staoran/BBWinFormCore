using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 客户收货人 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Customers : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public Customers()
    {
        TranNode = "*当前机构*";
        CreationDate = DateTime.Now;
        CreatedBy = "*当前用户*";
        LastUpdateDate = DateTime.Now;
        LastUpdatedBy = "*当前用户*";
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
    /// 公司编号
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCustomerCode, DisCustomerCode)]
    public string CustomerCode { get; set; }

    /// <summary>
    /// 网点编号
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldTranNode, DisTranNode)]
    public string TranNode { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [DataMember]
    [Column(FieldContactPerson, DisContactPerson)]
    public string ContactPerson { get; set; }

    /// <summary>
    /// 公司名称
    /// </summary>
    [DataMember]
    [Column(FieldNativeName, DisNativeName)]
    public string NativeName { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [DataMember]
    [Column(FieldAddress, DisAddress)]
    public string Address { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldProvinceNo, DisProvinceNo)]
    public string ProvinceNo { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCityNo, DisCityNo)]
    public string CityNo { get; set; }

    /// <summary>
    /// 区
    /// </summary>
    [DataMember]
    [Column(FieldAreaNo, DisAreaNo)]
    public string AreaNo { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    [DataMember]
    [Column(FieldTel, DisTel)]
    public string Tel { get; set; }

    /// <summary>
    /// 手机
    /// </summary>
    [DataMember]
    [Column(FieldMobile, DisMobile)]
    public string Mobile { get; set; }

    /// <summary>
    /// 保险费率
    /// </summary>
    [DataMember]
    [Column(FieldInsuranceRate, DisInsuranceRate)]
    public decimal InsuranceRate { get; set; }

    /// <summary>
    /// 坐标
    /// </summary>
    [DataMember]
    [Column(FieldCoordinate, DisCoordinate)]
    public string Coordinate { get; set; }

    /// <summary>
    /// 默认网点
    /// </summary>
    [DataMember]
    [Column(FieldDefaultToNode, DisDefaultToNode)]
    public string DefaultToNode { get; set; }

    /// <summary>
    /// 默认区域
    /// </summary>
    [DataMember]
    [Column(FieldDefaultToNodes, DisDefaultToNodes)]
    public int? DefaultToNodes { get; set; }

    /// <summary>
    /// 默认区域名称
    /// </summary>
    [DataMember]
    [Column(FieldDefaultToNodesName, DisDefaultToNodesName)]
    public string DefaultToNodesName { get; set; }

    /// <summary>
    /// 货物名称
    /// </summary>
    [DataMember]
    [Column(FieldCargoName, DisCargoName)]
    public string CargoName { get; set; }

    /// <summary>
    /// 包装方式
    /// </summary>
    [DataMember]
    [Column(FieldPackageType, DisPackageType)]
    public string PackageType { get; set; }

    /// <summary>
    /// 货物单位
    /// </summary>
    [DataMember]
    [Column(FieldCargoUnit, DisCargoUnit)]
    public string CargoUnit { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [DataMember]
    [Column(FieldPrice, DisPrice)]
    public decimal Price { get; set; }

    /// <summary>
    /// 计价方式
    /// </summary>
    [DataMember]
    [Column(FieldPriceType, DisPriceType)]
    public string PriceType { get; set; }

    /// <summary>
    /// 创建日期
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
    /// 更新日期
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Column(FieldLastUpdateDate, DisLastUpdateDate)]
    public DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    [DataMember]
    [Column(FieldLastUpdatedBy, DisLastUpdatedBy)]
    public string LastUpdatedBy { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranCustomers";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

    /// <summary>
    /// 外键名
    /// </summary>
    [NonSerialized]
    public new const string ForeignKey = FieldCustomerCode;

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
    /// 公司编号
    /// </summary>
    [NonSerialized]
    public const string FieldCustomerCode = "CustomerCode";

    /// <summary>
    /// 网点编号
    /// </summary>
    [NonSerialized]
    public const string FieldTranNode = "TranNode";

    /// <summary>
    /// 联系人
    /// </summary>
    [NonSerialized]
    public const string FieldContactPerson = "ContactPerson";

    /// <summary>
    /// 公司名称
    /// </summary>
    [NonSerialized]
    public const string FieldNativeName = "NativeName";

    /// <summary>
    /// 地址
    /// </summary>
    [NonSerialized]
    public const string FieldAddress = "Address";

    /// <summary>
    /// 省
    /// </summary>
    [NonSerialized]
    public const string FieldProvinceNo = "ProvinceNo";

    /// <summary>
    /// 市
    /// </summary>
    [NonSerialized]
    public const string FieldCityNo = "CityNo";

    /// <summary>
    /// 区
    /// </summary>
    [NonSerialized]
    public const string FieldAreaNo = "AreaNo";

    /// <summary>
    /// 电话
    /// </summary>
    [NonSerialized]
    public const string FieldTel = "Tel";

    /// <summary>
    /// 手机
    /// </summary>
    [NonSerialized]
    public const string FieldMobile = "Mobile";

    /// <summary>
    /// 保险费率
    /// </summary>
    [NonSerialized]
    public const string FieldInsuranceRate = "InsuranceRate";

    /// <summary>
    /// 坐标
    /// </summary>
    [NonSerialized]
    public const string FieldCoordinate = "Coordinate";

    /// <summary>
    /// 默认网点
    /// </summary>
    [NonSerialized]
    public const string FieldDefaultToNode = "DefaultToNode";

    /// <summary>
    /// 默认区域
    /// </summary>
    [NonSerialized]
    public const string FieldDefaultToNodes = "DefaultToNodes";

    /// <summary>
    /// 默认区域名称
    /// </summary>
    [NonSerialized]
    public const string FieldDefaultToNodesName = "DefaultToNodesName";

    /// <summary>
    /// 货物名称
    /// </summary>
    [NonSerialized]
    public const string FieldCargoName = "CargoName";

    /// <summary>
    /// 包装方式
    /// </summary>
    [NonSerialized]
    public const string FieldPackageType = "PackageType";

    /// <summary>
    /// 货物单位
    /// </summary>
    [NonSerialized]
    public const string FieldCargoUnit = "CargoUnit";

    /// <summary>
    /// 单价
    /// </summary>
    [NonSerialized]
    public const string FieldPrice = "Price";

    /// <summary>
    /// 计价方式
    /// </summary>
    [NonSerialized]
    public const string FieldPriceType = "PriceType";

    /// <summary>
    /// 创建日期
    /// </summary>
    [NonSerialized]
    public const string FieldCreationDate = "CreationDate";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string FieldCreatedBy = "CreatedBy";

    /// <summary>
    /// 更新日期
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
    /// 公司编号
    /// </summary>
    [NonSerialized]
    public const string DisCustomerCode = "公司编号";

    /// <summary>
    /// 网点编号
    /// </summary>
    [NonSerialized]
    public const string DisTranNode = "网点编号";

    /// <summary>
    /// 联系人
    /// </summary>
    [NonSerialized]
    public const string DisContactPerson = "联系人";

    /// <summary>
    /// 公司名称
    /// </summary>
    [NonSerialized]
    public const string DisNativeName = "公司名称";

    /// <summary>
    /// 地址
    /// </summary>
    [NonSerialized]
    public const string DisAddress = "地址";

    /// <summary>
    /// 省
    /// </summary>
    [NonSerialized]
    public const string DisProvinceNo = "省";

    /// <summary>
    /// 市
    /// </summary>
    [NonSerialized]
    public const string DisCityNo = "市";

    /// <summary>
    /// 区
    /// </summary>
    [NonSerialized]
    public const string DisAreaNo = "区";

    /// <summary>
    /// 电话
    /// </summary>
    [NonSerialized]
    public const string DisTel = "电话";

    /// <summary>
    /// 手机
    /// </summary>
    [NonSerialized]
    public const string DisMobile = "手机";

    /// <summary>
    /// 保险费率
    /// </summary>
    [NonSerialized]
    public const string DisInsuranceRate = "保险费率";

    /// <summary>
    /// 坐标
    /// </summary>
    [NonSerialized]
    public const string DisCoordinate = "坐标";

    /// <summary>
    /// 默认网点
    /// </summary>
    [NonSerialized]
    public const string DisDefaultToNode = "默认网点";

    /// <summary>
    /// 默认区域
    /// </summary>
    [NonSerialized]
    public const string DisDefaultToNodes = "默认区域";

    /// <summary>
    /// 默认区域名称
    /// </summary>
    [NonSerialized]
    public const string DisDefaultToNodesName = "默认区域名称";

    /// <summary>
    /// 货物名称
    /// </summary>
    [NonSerialized]
    public const string DisCargoName = "货物名称";

    /// <summary>
    /// 包装方式
    /// </summary>
    [NonSerialized]
    public const string DisPackageType = "包装方式";

    /// <summary>
    /// 货物单位
    /// </summary>
    [NonSerialized]
    public const string DisCargoUnit = "货物单位";

    /// <summary>
    /// 单价
    /// </summary>
    [NonSerialized]
    public const string DisPrice = "单价";

    /// <summary>
    /// 计价方式
    /// </summary>
    [NonSerialized]
    public const string DisPriceType = "计价方式";

    /// <summary>
    /// 创建日期
    /// </summary>
    [NonSerialized]
    public const string DisCreationDate = "创建日期";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string DisCreatedBy = "创建人";

    /// <summary>
    /// 更新日期
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdateDate = "更新日期";

    /// <summary>
    /// 更新人
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdatedBy = "更新人";

    #endregion

    #endregion
}