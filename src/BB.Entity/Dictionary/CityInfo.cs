using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.Dictionary;

/// <summary>
/// 城市
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class CityInfo : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public CityInfo()
    {
        ID = 0;
        ProvinceId = 0;
    }

    #region Property Members

    /// <summary>
    /// 城市ID
    /// </summary>
    [DataMember]
    [Key]
    [Identity]
    [Sort(IsDesc)]
    [Column(FieldID)]
    public virtual long ID { get; set; }

    /// <summary>
    /// 城市名称
    /// </summary>
    [DataMember]
    [Column(FieldCityName)]
    public virtual string CityName { get; set; }

    /// <summary>
    /// 邮政编码
    /// </summary>
    [DataMember]
    [Column(FieldZipCode)]
    public virtual string ZipCode { get; set; }

    /// <summary>
    /// 所属省份ID
    /// </summary>
    [DataMember]
    [Column(FieldProvinceId)]
    public virtual long ProvinceId { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "TB_City";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldID;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldID;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public new const bool IsDesc = false;

    [NonSerialized]
    public const string FieldID = "ID";

    [NonSerialized]
    public const string FieldCityName = "CityName";

    [NonSerialized]
    public const string FieldZipCode = "ZipCode";

    [NonSerialized]
    public const string FieldProvinceId = "ProvinceID";

    #endregion
}