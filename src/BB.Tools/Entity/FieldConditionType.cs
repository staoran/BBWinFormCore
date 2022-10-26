namespace BB.Tools.Entity;

/// <summary>
/// 字段查询条件类型
/// </summary>
public class FieldConditionType
{
    /// <summary>
    /// 字段查询条件类型
    /// </summary>
    /// <param name="fieldName">数据库字段名称</param>
    /// <param name="sqlOperator">查询类型</param>
    /// <param name="queryRequired">是否必填</param>
    /// <param name="enabledConditions">是否生效的逻辑</param>
    public FieldConditionType(string fieldName, SqlOperator sqlOperator, bool queryRequired = false, Func<Dictionary<string, string>, Task<bool>>? enabledConditions = null)
    {
        FieldName = fieldName;
        SqlOperator = sqlOperator;
        QueryRequired = queryRequired;
        EnabledConditions = enabledConditions;
    }

    /// <summary>
    /// 字段名称
    /// </summary>
    public string FieldName { get; set; }

    /// <summary>
    /// 查询类型
    /// </summary>
    public SqlOperator SqlOperator { get; set; }

    /// <summary>
    /// 是否必填
    /// </summary>
    public bool QueryRequired { get; set; }

    /// <summary>
    /// 是否生效的委托
    /// </summary>
    public Func<Dictionary<string, string>, Task<bool>>? EnabledConditions { get; set; }
}