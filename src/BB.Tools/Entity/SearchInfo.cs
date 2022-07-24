namespace BB.Tools.Entity;

/// <summary>
/// 查询信息实体类
/// </summary>
[Serializable]
public class SearchInfo
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public SearchInfo() {}

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fieldName">字段名称</param>
    /// <param name="fieldValue">字段的值</param>
    /// <param name="sqlOperator">字段的Sql操作符号</param>
    /// <param name="excludeIfEmpty">如果字段为空或者Null则不作为查询条件</param>
    /// <param name="groupName">分组的名称，如需构造一个括号内的条件 ( Test = "AA1" OR Test = "AA2"), 定义一个组名集中条件</param>
    public SearchInfo(string fieldName, object fieldValue, SqlOperator sqlOperator = SqlOperator.Empty, bool excludeIfEmpty = true, string groupName = null) 
    {
        _fieldName = fieldName;
        _fieldValue = fieldValue;
        _sqlOperator = sqlOperator; 
        _excludeIfEmpty = excludeIfEmpty;
        _groupName = groupName;
    }

    #region 字段属性
    private string _fieldName;
    private object _fieldValue;
    private SqlOperator _sqlOperator;
    private bool _excludeIfEmpty = true;
    private string _groupName;

    /// <summary>
    /// 分组的名称，如需构造一个括号内的条件 ( Test = "AA1" OR Test = "AA2"), 定义一个组名集中条件
    /// </summary>
    public string GroupName
    {
        get => _groupName;
        set => _groupName = value;
    }


    /// <summary>
    /// 字段名称
    /// </summary>
    public string FieldName
    {
        get => _fieldName;
        set => _fieldName = value;
    }

    /// <summary>
    /// 字段的值
    /// </summary>
    public object FieldValue
    {
        get => _fieldValue;
        set => _fieldValue = value;
    }

    /// <summary>
    /// 字段的Sql操作符号
    /// </summary>
    public SqlOperator SqlOperator
    {
        get => _sqlOperator;
        set => _sqlOperator = value;
    }

    /// <summary>
    /// 如果字段为空或者Null则不作为查询条件
    /// </summary>
    public bool ExcludeIfEmpty
    {
        get => _excludeIfEmpty;
        set => _excludeIfEmpty = value;
    } 

    #endregion
}