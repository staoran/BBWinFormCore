using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

/// <summary>
/// 角色功能关系
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public class RoleFunction
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public RoleFunction()
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
    public virtual string FunctionId { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public const string DBTableName = "T_ACL_Role_Function";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public const string PrimaryKey = FieldFunctionId;

    /// <summary>
    /// 角色ID
    /// </summary>
    [NonSerialized]
    public const string FieldRoleId = "Role_ID";

    /// <summary>
    /// 功能ID
    /// </summary>
    [NonSerialized]
    public const string FieldFunctionId = "Function_ID";

    #endregion
}