using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.Dictionary;

/// <summary>
/// 全国省份
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class ProvinceInfo : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public ProvinceInfo()
    {
        ID = 0;

    }

    #region Property Members

    /// <summary>
    /// 省份ID
    /// </summary>
    [DataMember]
    [Key]
    [Identity]
    [Sort(IsDesc)]
    [Column(FieldID)]
    public virtual long ID { get; set; }

    /// <summary>
    /// 省份名称
    /// </summary>
    [DataMember]
    [Column(FieldProvinceName)]
    public virtual string ProvinceName { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "TB_Province";

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
    public const string FieldProvinceName = "ProvinceName";

    #endregion
}