using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.Security;

/// <summary>
/// 系统功能定义
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class FunctionInfo : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public FunctionInfo()
    {
        ID = Guid.NewGuid().ToString();
        PID = "-1";

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
    /// 父ID
    /// </summary>
    [DataMember]
    [Column(FieldPID)]
    public virtual string PID { get; set; }

    /// <summary>
    /// 功能名称
    /// </summary>
    [DataMember]
    [Column(FieldName)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 控制标识
    /// </summary>
    [DataMember]
    [Column(FieldControlId)]
    public virtual string ControlId { get; set; }

    /// <summary>
    /// 系统编号
    /// </summary>
    [DataMember]
    [Column(FieldSystemTypeId)]
    public virtual string SystemTypeId { get; set; }

    /// <summary>
    /// 排序码
    /// </summary>
    [DataMember]
    [Column(FieldSortCode)]
    public virtual string SortCode { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_Function";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldID;

    [NonSerialized]
    public const string FieldID = "ID";

    /// <summary>
    /// 父ID
    /// </summary>
    [NonSerialized]
    public const string FieldPID = "PID";

    /// <summary>
    /// 功能名称
    /// </summary>
    [NonSerialized]
    public const string FieldName = "Name";

    /// <summary>
    /// 控制标识
    /// </summary>
    [NonSerialized]
    public const string FieldControlId = "ControlID";

    /// <summary>
    /// 系统编号
    /// </summary>
    [NonSerialized]
    public const string FieldSystemTypeId = "SystemType_ID";

    /// <summary>
    /// 排序码
    /// </summary>
    [NonSerialized]
    public const string FieldSortCode = "SortCode";

    #endregion
}

/// <summary>
/// 系统功能节点对象
/// </summary>
[Serializable]
[DataContract]
public class FunctionNodeInfo : FunctionInfo
{
    private List<FunctionNodeInfo> _mChildren = new List<FunctionNodeInfo>();

    /// <summary>
    /// 子菜单实体类对象集合
    /// </summary>
    [DataMember]
    public List<FunctionNodeInfo> Children
    {
        get => _mChildren;
        set => _mChildren = value;
    }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public FunctionNodeInfo()
    {
        _mChildren = new List<FunctionNodeInfo>();
    }

    /// <summary>
    /// 参数构造函数
    /// </summary>
    /// <param name="functionInfo">FunctionInfo对象</param>
    public FunctionNodeInfo(FunctionInfo functionInfo)
    {
        base.ControlId = functionInfo.ControlId;
        base.ID = functionInfo.ID;
        base.Name = functionInfo.Name;
        base.PID = functionInfo.PID;
        base.SystemTypeId = functionInfo.SystemTypeId;
        base.SortCode = functionInfo.SortCode;
    }
}