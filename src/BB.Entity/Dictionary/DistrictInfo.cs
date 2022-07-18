using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Dictionary;

/// <summary>
/// 行政区
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class DistrictInfo : BaseEntity
{    
    /// <summary>
    /// 构造函数
    /// </summary>
    public DistrictInfo()
    {

    }

    #region Property Members

    /// <summary>
    /// 行政区ID
    /// </summary>
    [DataMember]
    [Key]
    [Identity]
    [Sort(IsDesc)]
    [Column(FieldID)]
    public virtual int ID { get; set; }

    /// <summary>
    /// 行政区名称
    /// </summary>
    [DataMember]
    [Column(FieldDistrictName)]
    public virtual string DistrictName { get; set; }

    /// <summary>
    /// 所属城市ID
    /// </summary>
    [DataMember]
    [Column(FieldCityId)]
    public virtual int CityId { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "TB_District";

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
    public const string FieldDistrictName = "DistrictName";

    [NonSerialized]
    public const string FieldCityId = "CityID";

    #endregion
}