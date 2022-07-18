using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

/// <summary>
/// 功能菜单
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class MenuInfo : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public MenuInfo()
    {
        ID = Guid.NewGuid().ToString();
        PID = "-1";
        Visible = true;
        Expand = false;
        CreateTime = DateTime.Now;
        EditTime = DateTime.Now;
        Deleted = false;
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
    /// 显示名称
    /// </summary>
    [DataMember]
    [Column(FieldName)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 图标（遗留不用）
    /// </summary>
    [DataMember]
    [Column(FieldIcon)]
    public virtual string Icon { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [DataMember]
    [Sort(IsDesc)]
    [Column(FieldSeq)]
    public virtual string Seq { get; set; }

    /// <summary>
    /// 功能ID
    /// </summary>
    [DataMember]
    [Column(FieldFunctionId)]
    public virtual string FunctionId { get; set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    [DataMember]
    [Column(FieldVisible)]
    public virtual bool Visible { get; set; }

    /// <summary>
    /// Winform窗体类型
    /// </summary>
    [DataMember]
    [Column(FieldWinformType)]
    public virtual string WinformType { get; set; }

    /// <summary>
    /// Web界面Url地址
    /// </summary>
    [DataMember]
    [Column(FieldUrl)]
    public virtual string Url { get; set; }

    /// <summary>
    /// Web界面的菜单图标
    /// </summary>
    [DataMember]
    [Column(FieldWebIcon)]
    public virtual string WebIcon { get; set; }

    /// <summary>
    /// 系统编号
    /// </summary>
    [DataMember]
    [Column(FieldSystemTypeId)]
    public virtual string SystemTypeId { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [DataMember]
    [Column(FieldCreator)]
    public virtual string Creator { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [DataMember]
    [Column(FieldCreatorId)]
    public virtual string CreatorId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Column(FieldCreateTime)]
    public virtual DateTime CreateTime { get; set; }

    /// <summary>
    /// 编辑人
    /// </summary>
    [DataMember]
    [Column(FieldEditor)]
    public virtual string Editor { get; set; }

    /// <summary>
    /// 编辑人ID
    /// </summary>
    [DataMember]
    [Column(FieldEditorId)]
    public virtual string EditorId { get; set; }

    /// <summary>
    /// 编辑时间
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Column(FieldEditTime)]
    public virtual DateTime EditTime { get; set; }

    /// <summary>
    /// 是否已删除
    /// </summary>
    [DataMember]
    [Column(FieldDeleted)]
    public virtual bool Deleted { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    [DataMember]
    [Column(FieldEmbedIcon)]
    public virtual byte[] EmbedIcon { get; set; }

    /// <summary>
    /// 是否展开
    /// </summary>
    [DataMember]
    [Column(FieldExpand)]
    public virtual bool Expand { get; set; }

    /// <summary>
    /// 特殊标签
    /// </summary>
    [DataMember]
    [Column(FieldTag)]
    public virtual string Tag { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_Menu";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldID;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldSeq;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public new const bool IsDesc = true;

    /// <summary>
    /// 乐观锁字段
    /// </summary>
    [NonSerialized]
    public new const string OptimisticLockKey = FieldEditTime;

    [NonSerialized]
    public const string FieldID = "ID";

    /// <summary>
    /// 父ID
    /// </summary>
    [NonSerialized]
    public const string FieldPID = "PID";

    /// <summary>
    /// 显示名称
    /// </summary>
    [NonSerialized]
    public const string FieldName = "Name";

    /// <summary>
    /// 图标
    /// </summary>
    [NonSerialized]
    public const string FieldIcon = "Icon";

    /// <summary>
    /// 排序
    /// </summary>
    [NonSerialized]
    public const string FieldSeq = "Seq";

    /// <summary>
    /// 功能ID
    /// </summary>
    [NonSerialized]
    public const string FieldFunctionId = "FunctionId";

    /// <summary>
    /// 是否可见
    /// </summary>
    [NonSerialized]
    public const string FieldVisible = "Visible";

    /// <summary>
    /// Winform窗体类型
    /// </summary>
    [NonSerialized]
    public const string FieldWinformType = "WinformType";

    /// <summary>
    /// Web界面Url地址
    /// </summary>
    [NonSerialized]
    public const string FieldUrl = "Url";

    /// <summary>
    /// Web界面的菜单图标
    /// </summary>
    [NonSerialized]
    public const string FieldWebIcon = "WebIcon";

    /// <summary>
    /// 系统编号
    /// </summary>
    [NonSerialized]
    public const string FieldSystemTypeId = "SystemType_ID";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string FieldCreator = "Creator";

    /// <summary>
    /// 创建人ID
    /// </summary>
    [NonSerialized]
    public const string FieldCreatorId = "Creator_ID";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string FieldCreateTime = "CreateTime";

    /// <summary>
    /// 编辑人
    /// </summary>
    [NonSerialized]
    public const string FieldEditor = "Editor";

    /// <summary>
    /// 编辑人ID
    /// </summary>
    [NonSerialized]
    public const string FieldEditorId = "Editor_ID";

    /// <summary>
    /// 编辑时间
    /// </summary>
    [NonSerialized]
    public const string FieldEditTime = "EditTime";

    /// <summary>
    /// 是否已删除
    /// </summary>
    [NonSerialized]
    public const string FieldDeleted = "Deleted";

    /// <summary>
    /// 图标
    /// </summary>
    [NonSerialized]
    public const string FieldEmbedIcon = "EmbedIcon";

    /// <summary>
    /// 是否展开
    /// </summary>
    [NonSerialized]
    public const string FieldExpand = "Expand";

    /// <summary>
    /// 特殊标签
    /// </summary>
    [NonSerialized]
    public const string FieldTag = "Tag";

    #endregion
}

/// <summary>
/// 功能菜单节点对象
/// </summary>
[Serializable]
[DataContract]
public class MenuNodeInfo : MenuInfo
{
    private List<MenuNodeInfo> _mChildren = new List<MenuNodeInfo>();

    /// <summary>
    /// 子菜单实体类对象集合
    /// </summary>
    [DataMember]
    public List<MenuNodeInfo> Children
    {
        get => _mChildren;
        set => _mChildren = value;
    }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public MenuNodeInfo()
    {
        _mChildren = new List<MenuNodeInfo>();
    }

    /// <summary>
    /// 参数构造函数
    /// </summary>
    /// <param name="menuInfo">MenuInfo对象</param>
    public MenuNodeInfo(MenuInfo menuInfo)
    {
        base.ID = menuInfo.ID;
        base.Name = menuInfo.Name;
        base.PID = menuInfo.PID;
        base.Seq = menuInfo.Seq;
        base.Visible = menuInfo.Visible;
        base.Expand = menuInfo.Expand;
        base.FunctionId = menuInfo.FunctionId;
        base.Icon = menuInfo.Icon;
        base.EmbedIcon = menuInfo.EmbedIcon;
        base.WebIcon = menuInfo.WebIcon;
        base.WinformType = menuInfo.WinformType;
        base.Url = menuInfo.Url;
        base.SystemTypeId = menuInfo.SystemTypeId;
        base.Creator = menuInfo.Creator;
        base.CreatorId = menuInfo.CreatorId;
        base.CreateTime = menuInfo.CreateTime;
        base.Editor = menuInfo.Editor;
        base.EditorId = menuInfo.EditorId;
        base.EditTime = menuInfo.EditTime;
        base.Deleted = menuInfo.Deleted;
        base.Tag = menuInfo.Tag;
    }
}