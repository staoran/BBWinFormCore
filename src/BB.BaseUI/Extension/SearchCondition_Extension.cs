using BB.Tools.Entity;
using BB.Tools.Format;
using DevExpress.XtraEditors;

namespace BB.BaseUI.Extension;

/// <summary>
/// 查询功能扩展
/// </summary>
public static class SearchConditionExtension
{
    #region 查询相关扩展

    /// <summary>
    /// 添加开始日期和结束日期的查询操作
    /// </summary>
    /// <param name="condition">SearchCondition对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="startDate">开始日期</param>
    /// <param name="endDate">结束日期</param>
    /// <returns></returns>
    public static SearchCondition AddDateCondition(this SearchCondition condition, string fieldName, string startDate, string endDate)
    {
        DateTime date;
        if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out date))
        {
            condition.AddCondition(fieldName, date, SqlOperator.MoreThanOrEqual);
        }

        if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out date))
        {
            condition.AddCondition(fieldName, date.AddDays(1), SqlOperator.LessThan);
        }
        return condition;
    }

    /// <summary>
    /// 添加开始日期和结束日期的查询操作
    /// </summary>
    /// <param name="condition">SearchCondition对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="startDate">开始日期</param>
    /// <param name="endDate">结束日期</param>
    /// <returns></returns>
    public static SearchCondition AddDateCondition(this SearchCondition condition, string fieldName, DateTime startDate, DateTime endDate)
    {
        condition.AddCondition(fieldName, startDate, SqlOperator.MoreThanOrEqual);
        condition.AddCondition(fieldName, endDate.AddDays(1), SqlOperator.LessThan);
        return condition;
    }

    /// <summary>
    /// 添加开始日期和结束日期的查询操作
    /// </summary>
    /// <param name="condition">SearchCondition对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="startCtrl">开始日期控件</param>
    /// <param name="endCtrl">结束日期控件</param>
    /// <returns></returns>
    public static SearchCondition AddDateCondition(this SearchCondition condition, string fieldName, DateEdit startCtrl, DateEdit endCtrl)
    {
        if (startCtrl.Text.Length > 0)
        {
            condition.AddCondition(fieldName, Convert.ToDateTime(startCtrl.DateTime.ToShortDateString()), SqlOperator.MoreThanOrEqual);
        }
        if (endCtrl.Text.Length > 0)
        {
            condition.AddCondition(fieldName, Convert.ToDateTime(endCtrl.DateTime.AddDays(1).ToShortDateString()), SqlOperator.LessThan);
        }
        return condition;
    }

    /// <summary>
    /// 添加数值区间的查询操作
    /// </summary>
    /// <param name="condition">SearchCondition对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="startCtrl">开始范围控件</param>
    /// <param name="endCtrl">结束范围控件</param>
    /// <returns></returns>
    public static SearchCondition AddNumericCondition(this SearchCondition condition, string fieldName, SpinEdit startCtrl, SpinEdit endCtrl)
    {
        if (startCtrl.Text.Length > 0)
        {
            condition.AddCondition(fieldName, startCtrl.Value, SqlOperator.MoreThanOrEqual);
        }
        if (endCtrl.Text.Length > 0)
        {
            condition.AddCondition(fieldName, endCtrl.Value, SqlOperator.LessThanOrEqual);
        }
        return condition;
    }

    /// <summary>
    /// 添加数值区间的查询操作
    /// </summary>
    /// <param name="condition">SearchCondition对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="startCtrl">开始范围控件</param>
    /// <param name="endCtrl">结束范围控件</param>
    /// <returns></returns>
    public static SearchCondition AddNumericCondition(this SearchCondition condition, string fieldName, TextEdit startCtrl, TextEdit endCtrl)
    {
        decimal value = 0;
        if (decimal.TryParse(startCtrl.Text.Trim(), out value))
        {
            condition.AddCondition(fieldName, value, SqlOperator.MoreThanOrEqual);
        }
        if (decimal.TryParse(endCtrl.Text.Trim(), out value))
        {
            condition.AddCondition(fieldName, value, SqlOperator.LessThanOrEqual);
        }
        return condition;
    }

    /// <summary>
    /// 添加数值区间的查询操作(转换小时.分钟为分钟的输入)
    /// </summary>
    /// <param name="condition">SearchCondition对象</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="startCtrl"></param>
    /// <param name="endCtrl"></param>
    /// <returns></returns>
    public static SearchCondition AddNumericCondition2(this SearchCondition condition, string fieldName, TextEdit startCtrl, TextEdit endCtrl)
    {
        decimal value = 0;
        int hour = 0;
        int minute = 0;
        decimal hourMinute = 0;
        if (decimal.TryParse(startCtrl.Text.Trim(), out value))
        {
            hour = (int)value;
            hourMinute = hour * 60;
            string[] startValue = startCtrl.Text.Split(new[] { '.' });
            if (int.TryParse(startValue[1].Trim(), out minute))
            {
                hourMinute += minute;
            }
            condition.AddCondition(fieldName, hourMinute, SqlOperator.MoreThanOrEqual);
        }
        if (decimal.TryParse(endCtrl.Text.Trim(), out value))
        {
            hour = (int)value;
            hourMinute = hour * 60;
            string[] endValue = endCtrl.Text.Split(new[] { '.' });
            if (int.TryParse(endValue[1].Trim(), out minute))
            {
                hourMinute += minute;
            }
            condition.AddCondition(fieldName, hourMinute, SqlOperator.LessThanOrEqual);
        }
        return condition;
    }
    #endregion
}