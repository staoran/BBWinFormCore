using System.ComponentModel;
using System.Runtime.Serialization;

namespace BB.Tools.Entity;

/// <summary>
/// 个人图片分类
/// </summary>
[Serializable]
[DataContract]
public enum UserImageType
{
    [EnumMember]
    个人肖像,

    [EnumMember]
    身份证照片1,

    [EnumMember]
    身份证照片2,

    [EnumMember]
    名片1,

    [EnumMember]
    名片2
}

/// <summary>
/// 操作类型枚举
/// </summary>
public enum OperationType
{
    Add,
    Edit,
    Approve,
    Delete,
    View
}

/// <summary>
/// 通讯录类型
/// </summary>
public enum AddressType { 个人, 公共}

/// <summary>
/// Sql的查询符号
/// </summary>
public enum SqlOperator
{
    /// <summary>
    /// 空的，等待指定符号
    /// </summary>
    [Description("等待指定符号")]
    Empty,

    /// <summary>
    /// Like 模糊查询
    /// </summary>
    [Description("Like 模糊查询")]
    Like,

    /// <summary>
    /// Not LiKE 模糊查询
    /// </summary>
    [Description("Not LiKE 模糊查询")]
    NotLike,

    /// <summary>
    /// Like 开始匹配模糊查询，如Like 'ABC%'
    /// </summary>
    [Description("Like 开始匹配模糊查询，如Like 'ABC%'")]
    LikeStartAt,

    /// <summary>
    /// Like 结尾匹配模糊查询，如Like '%ABC'
    /// </summary>
    [Description("Like 结尾匹配模糊查询，如Like '%ABC'")]
    LikeEndAt,

    /// <summary>
    /// ＝ 等于号 
    /// </summary>
    [Description("＝ 等于号")]
    Equal,

    /// <summary>
    /// ＜＞ (≠) 不等于号
    /// </summary>
    [Description("<> (≠) 不等于号")]
    NotEqual,

    /// <summary>
    /// ＞ 大于号
    /// </summary>
    [Description("＞ 大于号")]
    MoreThan,

    /// <summary>
    /// ＜ 小于号 
    /// </summary>
    [Description("＜小于号")]
    LessThan,

    /// <summary>
    /// ≥大于或等于号 
    /// </summary>
    [Description("≥大于或等于号 ")]
    MoreThanOrEqual,

    /// <summary>
    /// ≤ 小于或等于号
    /// </summary>
    [Description("≤ 小于或等于号")]
    LessThanOrEqual,

    /// <summary>
    /// 在某两个值的中间，between and 等同于 ≥ 和 ≤
    /// </summary>
    [Description("在某两个值的中间")]
    Between,

    /// <summary>
    /// 不在某两个值的中间，not between and 等同于 < 和 >
    /// </summary>
    [Description("不在某两个值的中间")]
    NotBetween,

    /// <summary>
    /// 在数据集合中
    /// </summary>
    [Description("在数据集合中")]
    In,

    /// <summary>
    /// 不在数据集合中
    /// </summary>
    [Description("不在数据集合中")]
    NotIn,

    /// <summary>
    /// value 是 null 或 ""
    /// </summary>
    [Description("value 是 null 或 空")]
    IsNullOrEmpty,

    /// <summary>
    /// field is not null
    /// field 不等于 value
    /// </summary>
    [Description("field is not null | field 不等于 value")]
    IsNot,

    /// <summary>
    /// field is null
    /// field = value
    /// </summary>
    [Description("field is null | field = value")]
    EqualNull,

    /// <summary>
    /// 多个 Like or，value = x,x,x
    /// </summary>
    [Description("多个 Like or")]
    InLike,
}

/// <summary>
/// 数据库类型
/// </summary>
public enum DatabaseType { 
    /// <summary>
    /// SqlServer数据库
    /// </summary>
    SqlServer, 

    /// <summary>
    /// Oracle数据库
    /// </summary>
    Oracle, 

    /// <summary>
    /// Access数据库
    /// </summary>
    Access, 

    /// <summary>
    /// MySql数据库
    /// </summary>
    MySql, 

    /// <summary>
    /// SQLite数据库
    /// </summary>
    SqLite, 

    /// <summary>
    /// 达梦数据库
    /// </summary>
    Dm,

    /// <summary>
    /// PostgreSQL数据库
    /// </summary>
    PostgreSql
}