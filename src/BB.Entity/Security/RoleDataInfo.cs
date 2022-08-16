using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.Security;

/// <summary>
/// 角色的数据权限
/// </summary>
[DataContract]
[Table(DBTableName)]
public class RoleDataInfo : BaseEntity
{    
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    [Table(DBTableName)]
    public RoleDataInfo()
    {
        ID = System.Guid.NewGuid().ToString();
        RoleId = 0;

    }

    #region Property Members

    /// <summary>
    /// ID
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldID)]
    public virtual string ID { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    [DataMember]
    [Column(FieldRoleId)]
    public virtual int RoleId { get; set; }

    /// <summary>
    /// 所属机构
    /// </summary>
    [DataMember]
    [Column(FieldBelongCompanys)]
    public virtual string BelongCompanys { get; set; }

    /// <summary>
    /// 所属部门
    /// </summary>
    [DataMember]
    [Column(FieldBelongDepts)]
    public virtual string BelongDepts { get; set; }

    /// <summary>
    /// 排除部门
    /// </summary>
    [DataMember]
    [Column(FieldExcludeDepts)]
    public virtual string ExcludeDepts { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldNote)]
    public virtual string Note { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_RoleData";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldID;

    [NonSerialized]
    public const string FieldID = "ID";

    /// <summary>
    /// 角色ID
    /// </summary>
    [NonSerialized]
    public const string FieldRoleId = "Role_ID";

    /// <summary>
    /// 所属公司
    /// </summary>
    [NonSerialized]
    public const string FieldBelongCompanys = "BelongCompanys";

    /// <summary>
    /// 所属部门
    /// </summary>
    [NonSerialized]
    public const string FieldBelongDepts = "BelongDepts";

    /// <summary>
    /// 排除部门
    /// </summary>
    [NonSerialized]
    public const string FieldExcludeDepts = "ExcludeDepts";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldNote = "Note";

    #endregion

}