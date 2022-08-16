using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.Security;

[DataContract]
[Serializable]
[Table(DBTableName)]
public class OURoleEntity : BaseEntity
{
    [Key]
    [DataMember]
    [Column(FieldRoleId, "角色编号")]
    public int RoleId { get; set; }
    [Key]
    [DataMember]
    [Column(FieldOUId, "机构编号")]
    public string OUId { get; set; }
    
    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_OU_Role";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldOUId;

    /// <summary>
    /// 角色ID
    /// </summary>
    [NonSerialized]
    public const string FieldRoleId = "Role_ID";

    /// <summary>
    /// 功能ID
    /// </summary>
    [NonSerialized]
    public const string FieldOUId = "OU_ID";
}