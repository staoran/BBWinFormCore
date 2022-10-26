using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.Security;

/// <summary>
/// 角色菜单关系
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public class RoleMenu : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public RoleMenu()
    {
    }

    #region Property Members

    /// <summary>
    /// 角色ID
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldRoleId)]
    public virtual int RoleId { get; set; }

    /// <summary>
    /// 功能ID
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldFunctionId)]
    public virtual string MenuId { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_Role_Menu";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldFunctionId;

    /// <summary>
    /// 角色ID
    /// </summary>
    [NonSerialized]
    public const string FieldRoleId = "Role_ID";

    /// <summary>
    /// 功能ID
    /// </summary>
    [NonSerialized]
    public const string FieldFunctionId = "Menu_ID";

    #endregion
}