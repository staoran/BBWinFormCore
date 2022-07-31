using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

[DataContract]
[Serializable]
[Table(DBTableName)]
public class UserRoleEntity : BaseEntity
{
    [Key]
    [DataMember]
    [Column(FieldRoleId, "角色编号")]
    public int RoleId { get; set; }

    [Key]
    [DataMember]
    [Column(FieldUserId, "用户编号")]
    public int UserId { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_User_Role";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldRoleId;

    /// <summary>
    /// 角色ID
    /// </summary>
    [NonSerialized]
    public const string FieldRoleId = "Role_ID";

    /// <summary>
    /// 功能ID
    /// </summary>
    [NonSerialized]
    public const string FieldUserId = "User_ID";
}