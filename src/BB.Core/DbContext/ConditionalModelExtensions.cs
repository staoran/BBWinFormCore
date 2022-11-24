using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Tools.Entity;
using BB.Tools.Format;

namespace BB.Core.DbContext;

/// <summary>
/// 查询数据构造扩展
/// </summary>
[SuppressSniffer]
public static class ConditionalModelExtensions
{
    /// <summary>
    /// 根据查询数据构造查询模型集合
    /// </summary>
    /// <returns></returns> 
    public static async Task<List<IConditionalModel>> BuildConditionExc(this Dictionary<string, string> conditionData, List<FieldConditionType> dic)
    {
        if (conditionData == null)
        {
            throw Oops.Bah("没有初始化查询数据");
        }

        if (dic.Count == 0)
        {
            throw Oops.Bah("未配置有效的查询条件");
        }

        List<IConditionalModel> conditionalModels = new();
        // 循环预设的查询字段，避免错误的非法的查询参数名
        foreach (FieldConditionType fieldConditionType in dic)
        {
            if (!conditionData.TryGetValue(fieldConditionType.FieldName, out string value) && fieldConditionType.QueryRequired)
            {
                throw Oops.Bah($"缺少必填的查询参数：{fieldConditionType.FieldName}");
            }

            if (string.IsNullOrEmpty(value)) continue;

            var ifTrue = true;
            if (fieldConditionType.EnabledConditions != null)
            {
                ifTrue = await fieldConditionType.EnabledConditions(conditionData);
            }

            if (ifTrue)
            {
                if (fieldConditionType.SqlOperator == SqlOperator.Between)
                {
                    if (value.Contains(','))
                    {
                        // 如果有逗号分隔，转数组后，取有效值做相应运算
                        var values = value.Split(",");
                        if (!values[0].IsNullOrEmpty() && values[0].IsDateTime())
                        {
                            conditionalModels.Add(new ConditionalModel()
                            {
                                FieldName = fieldConditionType.FieldName,
                                ConditionalType = ConditionalType.GreaterThanOrEqual,
                                FieldValue = values[0]
                            });
                        }
                        if (!values[1].IsNullOrEmpty() && values[1].IsDateTime())
                        {
                            conditionalModels.Add(new ConditionalModel()
                            {
                                FieldName = fieldConditionType.FieldName,
                                ConditionalType = ConditionalType.LessThanOrEqual,
                                FieldValue = values[1]
                            });
                        }
                    }
                    else
                    {
                        if ( value.IsDateTime())
                        {
                            // 如果没有逗号分隔，默认按最小值处理，做大于等于运算
                            conditionalModels.Add(new ConditionalModel()
                            {
                                FieldName = fieldConditionType.FieldName,
                                ConditionalType = ConditionalType.GreaterThanOrEqual,
                                FieldValue = value
                            });
                        }
                    }
                }
                else
                {
                    conditionalModels.Add(new ConditionalModel()
                    {
                        FieldName = fieldConditionType.FieldName,
                        ConditionalType = fieldConditionType.SqlOperator.ToConditionalType(),
                        FieldValue = value
                    });
                }
            }
        }

        return conditionalModels;
    }

    /// <summary>
    /// 是时间日期，并且合法(大于1900年)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static bool IsDateTime(this string str)
    {
        var date = str.ObjToDateNull();
        return date == null || date > new DateTime(1900, 1, 1);
    }

    /// <summary>
    /// 根据传入对象的值类型获取其对应的DbType类型
    /// </summary>
    /// <param name="sqlOperator">对象的值</param>
    /// <returns>DbType类型</returns>
    public static ConditionalType ToConditionalType(this SqlOperator sqlOperator)
    {
        switch (sqlOperator)
        {
            case SqlOperator.Like:
                return ConditionalType.Like;
            case SqlOperator.NotLike:
                return ConditionalType.NoLike;
            case SqlOperator.LikeStartAt:
                return ConditionalType.LikeLeft;
            case SqlOperator.Equal:
                return ConditionalType.Equal;
            case SqlOperator.NotEqual:
                return ConditionalType.NoEqual;
            case SqlOperator.MoreThan:
                return ConditionalType.GreaterThan;
            case SqlOperator.LessThan:
                return ConditionalType.LessThan;
            case SqlOperator.MoreThanOrEqual:
                return ConditionalType.GreaterThanOrEqual;
            case SqlOperator.LessThanOrEqual:
                return ConditionalType.LessThanOrEqual;
            case SqlOperator.In:
                return ConditionalType.In;
            case SqlOperator.NotIn:
                return ConditionalType.NotIn;
            case SqlOperator.LikeEndAt:
                return ConditionalType.NotIn;
            case SqlOperator.IsNullOrEmpty:
                return ConditionalType.IsNullOrEmpty;
            case SqlOperator.IsNot:
                return ConditionalType.IsNot;
            case SqlOperator.EqualNull:
                return ConditionalType.EqualNull;
            case SqlOperator.InLike:
                return ConditionalType.InLike;
            case SqlOperator.Between:
            case SqlOperator.NotBetween:
            case SqlOperator.Empty:
            default:
                throw new ArgumentOutOfRangeException(nameof(sqlOperator), sqlOperator, "不受支持的连接类型");
        }
    }
}