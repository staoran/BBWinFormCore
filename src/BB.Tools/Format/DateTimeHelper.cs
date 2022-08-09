using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using BB.Tools.Utils;

namespace BB.Tools.Format;

/// <summary>
/// 时间相关操作辅助类库
/// </summary>
public class DateTimeHelper
{
    private DateTime _dt = DateTime.Now;

    /// <summary>
    /// 获取某一年有多少周
    /// </summary>
    /// <param name="year">年份</param>
    /// <returns>该年周数</returns>
    public static int GetWeekAmount(int year)
    {
        var end = new DateTime(year, 12, 31); //该年最后一天
        var gc = new GregorianCalendar();
        return gc.GetWeekOfYear(end, CalendarWeekRule.FirstDay, DayOfWeek.Monday); //该年星期数
    }

    /// <summary>
    /// 返回年度第几个星期   默认星期日是第一天
    /// </summary>
    /// <param name="date">时间</param>
    /// <returns></returns>
    public static int WeekOfYear(DateTime date)
    {
        GregorianCalendar gc = new GregorianCalendar();
        return gc.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
    }

    /// <summary>
    /// 返回年度第几个星期
    /// </summary>
    /// <param name="date">时间</param>
    /// <param name="week">一周的开始日期</param>
    /// <returns></returns>
    public static int WeekOfYear(DateTime date, DayOfWeek week)
    {
        GregorianCalendar gc = new GregorianCalendar();
        return gc.GetWeekOfYear(date, CalendarWeekRule.FirstDay, week);
    }


    /// <summary>
    /// 得到一年中的某周的起始日和截止日
    /// 年 nYear
    /// 周数 nNumWeek
    /// 周始 out dtWeekStart
    /// 周终 out dtWeekeEnd
    /// </summary>
    /// <param name="nYear">年份</param>
    /// <param name="nNumWeek">第几周</param>
    /// <param name="dtWeekStart">开始日期</param>
    /// <param name="dtWeekeEnd">结束日期</param>
    public static void GetWeekTime(int nYear, int nNumWeek, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
    {
        DateTime dt = new DateTime(nYear, 1, 1);
        dt = dt + new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
        dtWeekStart = dt.AddDays(-(int)dt.DayOfWeek + (int)DayOfWeek.Monday);
        dtWeekeEnd = dt.AddDays((int)DayOfWeek.Saturday - (int)dt.DayOfWeek + 1);
    }

    /// <summary>
    /// 得到一年中的某周的起始日和截止日    周一到周五  工作日
    /// </summary>
    /// <param name="nYear">年份</param>
    /// <param name="nNumWeek">第几周</param>
    /// <param name="dtWeekStart">开始日期</param>
    /// <param name="dtWeekeEnd">结束日期</param>
    public static void GetWeekWorkTime(int nYear, int nNumWeek, out DateTime dtWeekStart, out DateTime dtWeekeEnd)
    {
        DateTime dt = new DateTime(nYear, 1, 1);
        dt = dt + new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
        dtWeekStart = dt.AddDays(-(int)dt.DayOfWeek + (int)DayOfWeek.Monday);
        dtWeekeEnd = dt.AddDays((int)DayOfWeek.Saturday - (int)dt.DayOfWeek + 1).AddDays(-2);
    }

    #region P/Invoke 设置本地时间

    [DllImport("kernel32.dll")]
    private static extern bool SetLocalTime(ref Systemtime time);

    [StructLayout(LayoutKind.Sequential)]
    private struct Systemtime
    {
        public short year;
        public short month;
        public short dayOfWeek;
        public short day;
        public short hour;
        public short minute;
        public short second;
        public short milliseconds;
    }

    /// <summary>
    /// 设置本地计算机时间
    /// </summary>
    /// <param name="dt">DateTime对象</param>
    public static void SetLocalTime(DateTime dt)
    {
        Systemtime st;

        st.year = (short)dt.Year;
        st.month = (short)dt.Month;
        st.dayOfWeek = (short)dt.DayOfWeek;
        st.day = (short)dt.Day;
        st.hour = (short)dt.Hour;
        st.minute = (short)dt.Minute;
        st.second = (short)dt.Second;
        st.milliseconds = (short)dt.Millisecond;

        SetLocalTime(ref st);
    }

    #endregion

    #region 获取网络时间
    /// <summary>
    /// 获取中国国家授时中心网络服务器时间发布的当前时间
    /// </summary>
    /// <returns></returns>
    public static DateTime GetChineseDateTime()
    {
        DateTime res = DateTime.MinValue;
        try
        {
            string url = "http://www.time.ac.cn/stime.asp";
            HttpHelper helper = new HttpHelper
            {
                Encoding = Encoding.Default
            };
            string html = helper.GetHtml(url);
            string patDt = @"\d{4}年\d{1,2}月\d{1,2}日";
            string patHr = @"hrs\s+=\s+\d{1,2}";
            string patMn = @"min\s+=\s+\d{1,2}";
            string patSc = @"sec\s+=\s+\d{1,2}";
            Regex regDt = new Regex(patDt);
            Regex regHr = new Regex(patHr);
            Regex regMn = new Regex(patMn);
            Regex regSc = new Regex(patSc);

            res = DateTime.Parse(regDt.Match(html).Value);
            int hr = GetInt(regHr.Match(html).Value, false);
            int mn = GetInt(regMn.Match(html).Value, false);
            int sc = GetInt(regSc.Match(html).Value, false);
            res = res.AddHours(hr).AddMinutes(mn).AddSeconds(sc);
        }
        catch
        {
            // ignored
        }
        return res;
    }

    /// <summary>
    /// 从指定的字符串中获取整数
    /// </summary>
    /// <param name="origin">原始的字符串</param>
    /// <param name="fullMatch">是否完全匹配，若为false，则返回字符串中的第一个整数数字</param>
    /// <returns>整数数字</returns>
    private static int GetInt(string origin, bool fullMatch)
    {
        if (string.IsNullOrEmpty(origin))
        {
            return 0;
        }
        origin = origin.Trim();
        if (!fullMatch)
        {
            string pat = @"-?\d+";
            Regex reg = new Regex(pat);
            origin = reg.Match(origin.Trim()).Value;
        }
        int res = 0;
        int.TryParse(origin, out res);
        return res;
    }
    #endregion

    #region 类实例方法

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public DateTimeHelper()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dateTime">时间</param>
    public DateTimeHelper(DateTime dateTime)
    {
        _dt = dateTime;
    }

    /// <summary>
    /// 构造函数（字符窜时间）
    /// </summary>
    /// <param name="dateTime">时间</param>
    public DateTimeHelper(string dateTime)
    {
        _dt = DateTime.Parse(dateTime);
    }

    /// <summary>
    /// 基于给定（或当前）日期的偏移日期
    /// </summary>
    /// <param name="days">7天前:-7 7天后:+7</param>
    /// <returns></returns>
    public string GetTheDay(int? days)
    {
        int day = days ?? 0;
        return _dt.AddDays(day).ToShortDateString();
    }

    /// <summary>
    /// 周日
    /// </summary>
    /// <param name="weeks">上周-1 下周+1 本周0</param>
    /// <returns></returns>
    public string GetSunday(int? weeks)
    {
        int week = weeks ?? 0;
        return _dt.AddDays(Convert.ToDouble((0 - Convert.ToInt16(_dt.DayOfWeek))) + 7 * week).ToShortDateString();
    }

    /// <summary>
    /// 周六
    /// </summary>
    /// <param name="weeks">上周-1 下周+1 本周0</param>
    /// <returns></returns>
    public string GetSaturday(int? weeks)
    {
        int week = weeks ?? 0;
        return _dt.AddDays(Convert.ToDouble((6 - Convert.ToInt16(_dt.DayOfWeek))) + 7 * week).ToShortDateString();
    }

    /// <summary>
    /// 月第一天
    /// </summary>
    /// <param name="months">上月-1 本月0 下月1</param>
    /// <returns></returns>
    public string GetFirstDayOfMonth(int? months)
    {
        int month = months ?? 0;
        return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(month).ToShortDateString();
    }

    /// <summary>
    /// 月最后一天
    /// </summary>
    /// <param name="months">上月0 本月1 下月2</param>
    /// <returns></returns>
    public string GetLastDayOfMonth(int? months)
    {
        int month = months ?? 0;
        return DateTime.Parse(_dt.ToString("yyyy-MM-01")).AddMonths(month).AddDays(-1).ToShortDateString();
    }

    /// <summary>
    /// 年度第一天
    /// </summary>
    /// <param name="years">上年度-1 下年度+1</param>
    /// <returns></returns>
    public string GetFirstDayOfYear(int? years)
    {
        int year = years ?? 0;
        return DateTime.Parse(_dt.ToString("yyyy-01-01")).AddYears(year).ToShortDateString();
    }

    /// <summary>
    /// 年度最后一天
    /// </summary>
    /// <param name="years">上年度0 本年度1 下年度2</param>
    /// <returns></returns>
    public string GetLastDayOfYear(int? years)
    {
        int year = years ?? 0;
        return DateTime.Parse(_dt.ToString("yyyy-01-01")).AddYears(year).AddDays(-1).ToShortDateString();
    }

    /// <summary>
    /// 季度第一天
    /// </summary>
    /// <param name="quarters">上季度-1 下季度+1</param>
    /// <returns></returns>
    public string GetFirstDayOfQuarter(int? quarters)
    {
        int quarter = quarters ?? 0;
        return _dt.AddMonths(quarter * 3 - ((_dt.Month - 1) % 3)).ToString("yyyy-MM-01");
    }

    /// <summary>
    /// 季度最后一天
    /// </summary>
    /// <param name="quarters">上季度0 本季度1 下季度2</param>
    /// <returns></returns>
    public string GetLastDayOfQuarter(int? quarters)
    {
        int quarter = quarters ?? 0;
        return DateTime.Parse(_dt.AddMonths(quarter * 3 - ((_dt.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
    }

    /// <summary>
    /// 中文星期
    /// </summary>
    /// <returns></returns>
    public string GetDayOfWeekCn()
    {
        string[] day = new[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
        return day[Convert.ToInt16(_dt.DayOfWeek)];
    }

    /// <summary>
    /// 获取星期数字形式,周一开始
    /// </summary>
    /// <returns></returns>
    public int GetDayOfWeekNum()
    {
        int day = (Convert.ToInt16(_dt.DayOfWeek) == 0) ? 7 : Convert.ToInt16(_dt.DayOfWeek);
        return day;
    } 
        
    /// <summary>
    /// 获取本机日期时间
    /// </summary>
    /// <returns></returns>
    public static string GetServerDateTime()
    {
        return GetServerDateTime2().ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static DateTime GetServerDateTime2()
    {
        return DateTime.Now;
    }

    /// <summary>
    /// 获取本机日期
    /// </summary>
    /// <returns></returns>
    public static string GetServerDate()
    {
        return GetServerDateTime2().ToString("yyyy-MM-dd");
    }

    /// <summary>
    /// 获取本机时间
    /// </summary>
    /// <returns></returns>
    public static string GetServerTime()
    {
        return GetServerDateTime2().ToString("HH:mm:ss.fff");
    }

    #endregion

    #region 其他转换静态方法

    /// <summary>
    /// C#的时间到Javascript的时间的转换
    /// </summary>
    /// <param name="theDate"></param>
    /// <returns></returns>
    public static long ConvertTimeToJs(DateTime theDate)
    {
        //string time = (System.DateTime.Now.Subtract(Convert.ToDateTime("1970-01-01 8:0:0"))).TotalMilliseconds.ToString();
        //long d = MilliTimeStamp(DateTime.Now);

        DateTime d1 = new DateTime(1970, 1, 1);
        DateTime d2 = theDate.ToUniversalTime();
        TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);

        return (long)ts.TotalMilliseconds;
    }

    /// <summary>
    /// PHP的时间转换成C#中的DateTime
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static DateTime ConvertPhpToTime(long time)
    {
        DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
        long t = (time + 8 * 60 * 60) * 10000000 + timeStamp.Ticks;
        DateTime dt = new DateTime(t);
        return dt;
    }

    /// <summary>
    ///  C#中的DateTime转换成PHP的时间
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long ConvertTimeToPhp(DateTime time)
    {
        DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
        long a = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;  //注意这里有时区问题，用now就要减掉8个小时
        return a;
    }

    /// <summary>
    /// 获取两个时间的时间差
    /// </summary>
    /// <param name="beginTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <returns></returns>
    public static string GetDiffTime(DateTime beginTime, DateTime endTime)
    {
        int i = 0;
        return GetDiffTime(beginTime, endTime, ref i);
    }

    /// <summary>
    /// 计算2个时间差
    /// </summary>
    /// <param name="beginTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <param name="mindTime">提醒时间</param>
    /// <returns></returns>
    public static string GetDiffTime(DateTime beginTime, DateTime endTime, ref int mindTime)
    {
        string strResout = string.Empty;
        //获得2时间的时间间隔秒计算
        //TimeSpan span = endTime - beginTime;
        TimeSpan span = endTime.Subtract(beginTime);

        int iTatol = Convert.ToInt32(span.TotalSeconds);
        int iMinutes = 1 * 60;
        int iHours = iMinutes * 60;
        int iDay = iHours * 24;
        int iMonth = iDay * 30;
        int iYear = iMonth * 12;

        //提醒时间,到了返回1,否则返回0
        if (mindTime > iTatol && iTatol > 0)
        {
            mindTime = 1;
        }
        else
        {
            mindTime = 0;
        }

        if (iTatol > iYear)
        {
            strResout += iTatol / iYear + "年";
            iTatol = iTatol % iYear;//剩余
        }
        if (iTatol > iMonth)
        {
            strResout += iTatol / iMonth + "月";
            iTatol = iTatol % iMonth;
        }
        if (iTatol > iDay)
        {
            strResout += iTatol / iDay + "天";
            iTatol = iTatol % iDay;

        }
        if (iTatol > iHours)
        {
            strResout += iTatol / iHours + "小时";
            iTatol = iTatol % iHours;
        }
        if (iTatol > iMinutes)
        {
            strResout += iTatol / iMinutes + "分";
            iTatol = iTatol % iMinutes;
        }
        strResout += iTatol + "秒";

        return strResout;
    }

    /// <summary>
    /// 获得两个日期的间隔
    /// </summary>
    /// <param name="dateTime1">日期一。</param>
    /// <param name="dateTime2">日期二。</param>
    /// <returns>日期间隔TimeSpan。</returns>
    public static TimeSpan GetDiffTime2(DateTime dateTime1, DateTime dateTime2)
    {
        TimeSpan ts1 = new TimeSpan(dateTime1.Ticks);
        TimeSpan ts2 = new TimeSpan(dateTime2.Ticks);
        TimeSpan ts = ts1.Subtract(ts2).Duration();
        return ts;
    }

    /// <summary>
    /// 得到随机日期
    /// </summary>
    /// <param name="time1">起始日期</param>
    /// <param name="time2">结束日期</param>
    /// <returns>间隔日期之间的 随机日期</returns>
    public static DateTime GetRandomTime(DateTime time1, DateTime time2)
    {
        Random random = new Random();
        DateTime minTime = new DateTime();
        DateTime maxTime = new DateTime();

        TimeSpan ts = new TimeSpan(time1.Ticks - time2.Ticks);

        // 获取两个时间相隔的秒数
        double dTotalSecontds = ts.TotalSeconds;
        int iTotalSecontds = 0;

        if (dTotalSecontds > Int32.MaxValue)
        {
            iTotalSecontds = Int32.MaxValue;
        }
        else if (dTotalSecontds < Int32.MinValue)
        {
            iTotalSecontds = Int32.MinValue;
        }
        else
        {
            iTotalSecontds = (int)dTotalSecontds;
        }

        if (iTotalSecontds > 0)
        {
            minTime = time2;
            maxTime = time1;
        }
        else if (iTotalSecontds < 0)
        {
            minTime = time1;
            maxTime = time2;
        }
        else
        {
            return time1;
        }

        int maxValue = iTotalSecontds;

        if (iTotalSecontds <= Int32.MinValue)
            maxValue = Int32.MinValue + 1;

        int i = random.Next(Math.Abs(maxValue));
        return minTime.AddSeconds(i);
    }

    #endregion

}

/// <summary>
/// 日期时间扩展类
/// </summary>
public static class DateTimeExtension
{
    /// <summary>
    /// 一天分钟数
    /// </summary>
    public const int MINUTES_OF_THE_DAY = 1440;

    /// <summary>
    /// 秒
    /// </summary>
    public const int SECOND = 1,
        MINUTE = 60 * SECOND,
        HOUR = 60 * MINUTE,
        DAY = 24 * HOUR,
        MONTH = 30 * DAY;

    #region Methods

    /// <summary>
    /// 一天末尾时间
    /// </summary>
    /// <param name="data">需要操作的时间</param>
    /// <returns>DateTime</returns>
    public static DateTime EndOfDay(this DateTime data)
    {
        return new DateTime(data.Year, data.Month, data.Day).AddDays(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
    }

    /// <summary>
    /// 一个月末尾时间
    /// </summary>
    /// <param name="data">需要操作的时间</param>
    /// <returns>DateTime</returns>
    public static DateTime EndOfMonth(this DateTime data)
    {
        return new DateTime(data.Year, data.Month, 1).AddMonths(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
    }

    /// <summary>
    /// 一周末尾时间
    /// </summary>
    /// <param name="date">需要操作的时间</param>
    /// <returns>DateTime</returns>
    public static DateTime EndOfWeek(this DateTime date)
    {
        return EndOfWeek(date, DayOfWeek.Monday);
    }

    /// <summary>
    /// 一周末尾时间
    /// </summary>
    /// <param name="date">需要操作的时间</param>
    /// <param name="startDayOfWeek">一周起始周期</param>
    /// <returns>DateTime</returns>
    public static DateTime EndOfWeek(this DateTime date, DayOfWeek startDayOfWeek)
    {
        DateTime endDate = date;
        DayOfWeek endDayOfWeek = startDayOfWeek - 1;

        if (endDayOfWeek < 0)
        {
            endDayOfWeek = DayOfWeek.Saturday;
        }

        if (endDate.DayOfWeek != endDayOfWeek)
        {
            if (endDayOfWeek < endDate.DayOfWeek)
            {
                endDate = endDate.AddDays(7 - (endDate.DayOfWeek - endDayOfWeek));
            }
            else
            {
                endDate = endDate.AddDays(endDayOfWeek - endDate.DayOfWeek);
            }
        }

        return new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59, 999);
    }

    /// <summary>
    /// 一年末尾时间
    /// </summary>
    /// <param name="date">时间</param>
    /// <returns>DateTime</returns>
    public static DateTime EndOfYear(this DateTime date)
    {
        return new DateTime(date.Year, 1, 1).AddYears(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
    }

    /// <summary>
    /// 一个星期的第一天
    /// </summary>
    /// <param name="date">时间</param>
    /// <returns>DateTime</returns>
    public static DateTime FirstDayOfWeek(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day).AddDays(-(int)date.DayOfWeek);
    }

    /// <summary>
    /// 根据出生日期获取年龄
    /// </summary>
    /// <param name="birthDate">出生日期</param>
    /// <returns>年龄</returns>
    public static int GetAge(this DateTime birthDate)
    {
        if (DateTime.Today.Month < birthDate.Month || (DateTime.Today.Month == birthDate.Month && DateTime.Today.Day < birthDate.Day))
        {
            return DateTime.Today.Year - birthDate.Year - 1;
        }

        return DateTime.Today.Year - birthDate.Year;
    }

    /// <summary>
    /// 日期差计算
    /// </summary>
    /// <param name="startTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <param name="part">时间差枚举</param>
    /// <returns>时间差</returns>
    public static int GetDateDiff(this DateTime startTime, DateTime endTime, DateTimePart part)
    {
        int resutl = 0;

        switch (part)
        {
            case DateTimePart.Year:
                resutl = endTime.Year - startTime.Year;
                break;

            case DateTimePart.Month:
                resutl = (endTime.Year - startTime.Year) * 12 + (endTime.Month - startTime.Month);
                break;

            case DateTimePart.Day:
                resutl = (int)(endTime - startTime).TotalDays;
                break;

            case DateTimePart.Hour:
                resutl = (int)(endTime - startTime).TotalHours;
                break;

            case DateTimePart.Minute:
                resutl = (int)(endTime - startTime).TotalMinutes;
                break;

            case DateTimePart.Second:
                resutl = (int)(endTime - startTime).TotalSeconds;
                break;
        }

        return resutl;
    }

    /// <summary>
    /// 获取一个月有多少天
    /// </summary>
    /// <param name="date">时间</param>
    /// <returns>一个月有多少天</returns>
    public static int GetDays(this DateTime date)
    {
        return DateTime.DaysInMonth(date.Year, date.Month);
    }

    /// <summary>
    /// 友好时间
    /// </summary>
    /// <param name="datetime">DateTime</param>
    /// <returns>友好时间</returns>
    public static string GetFriendlyString(this DateTime datetime)
    {
        string friendlyString = string.Empty;
        TimeSpan ts = DateTime.Now - datetime;
        double totalSeconds = ts.TotalSeconds;

        if (totalSeconds < 1 * SECOND)
        {
            friendlyString = ts.Seconds == 1 ? "1秒前" : ts.Seconds + "秒前";
        }
        else if (totalSeconds < 2 * MINUTE)
        {
            friendlyString = "1分钟之前";
        }
        else if (totalSeconds < 45 * MINUTE)
        {
            friendlyString = ts.Minutes + "分钟";
        }
        else if (totalSeconds < 90 * MINUTE)
        {
            friendlyString = "1小时前";
        }
        else if (totalSeconds < 24 * HOUR)
        {
            friendlyString = ts.Hours + "小时前";
        }
        else if (totalSeconds < 48 * HOUR)
        {
            friendlyString = "昨天";
        }
        else if (totalSeconds < 30 * DAY)
        {
            friendlyString = ts.Days + " 天之前";
        }
        else if (totalSeconds < 12 * MONTH)
        {
            int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
            friendlyString = months <= 1 ? "一个月之前" : months + "月之前";
        }
        else
        {
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            friendlyString = years <= 1 ? "一年前" : years + "年前";
        }

        return friendlyString;
    }

    /// <summary>
    /// 计算两个时间之间工作天数
    /// </summary>
    /// <param name="startTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <returns>工作天数 </returns>
    public static int GetWeekdays(DateTime startTime, DateTime endTime)
    {
        TimeSpan ts = endTime - startTime;
        int weekCount = 0;

        for (int i = 0; i < ts.Days; i++)
        {
            DateTime time = startTime.AddDays(i);

            if (IsWeekDay(time))
            {
                weekCount++;
            }
        }

        return weekCount;
    }

    /// <summary>
    /// 计算两个时间直接周末天数
    /// </summary>
    /// <param name="startTime">开始天数</param>
    /// <param name="endTime">结束天数</param>
    /// <returns>周末天数</returns>
    public static int GetWeekends(DateTime startTime, DateTime endTime)
    {
        TimeSpan ts = endTime - startTime;
        int weekendCount = 0;

        for (int i = 0; i < ts.Days; i++)
        {
            DateTime dt = startTime.AddDays(i);

            if (IsWeekEnd(dt))
            {
                weekendCount++;
            }
        }

        return weekendCount;
    }

    /// <summary>
    /// 获取日期是一年中第几个星期
    /// </summary>
    /// <param name="date">需要计算的时间</param>
    /// <returns>一年中第几个星期</returns>
    public static int GetWeekNumber(this DateTime date)
    {
        var cultureInfo = CultureInfo.CurrentCulture;
        return cultureInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
    }

    /// <summary>
    /// 是否是下午时间
    /// </summary>
    /// <param name="date">时间</param>
    /// <returns>下午时间</returns>
    public static bool IsAfternoon(this DateTime date)
    {
        return date.TimeOfDay >= new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
    }

    /// <summary>
    /// 日期部分比较
    /// </summary>
    /// <param name="date">时间一</param>
    /// <param name="dateToCompare">时间二</param>
    /// <returns>日期部分是否相等</returns>
    public static bool IsDateEqual(this DateTime date, DateTime dateToCompare)
    {
        return date.Date == dateToCompare.Date;
    }

    /// <summary>
    /// 是否是将来时间
    /// </summary>
    /// <param name="date">时间</param>
    /// <returns> 是否是将来时间</returns>
    public static bool IsFuture(this DateTime date)
    {
        return date > DateTime.Now;
    }

    /// <summary>
    /// 是否是上午时间
    /// </summary>
    /// <param name="date">时间</param>
    /// <returns>是否是上午时间</returns>
    public static bool IsMorning(this DateTime date)
    {
        return date.TimeOfDay < new DateTime(2000, 1, 1, 12, 0, 0).TimeOfDay;
    }

    /// <summary>
    /// 是否是当前时间
    /// </summary>
    /// <param name="date">时间</param>
    /// <returns>是否是当前时间</returns>
    public static bool IsNow(this DateTime date)
    {
        return date == DateTime.Now;
    }

    /// <summary>
    /// 是否是过去时间
    /// </summary>
    /// <param name="date">时间</param>
    /// <returns>是否是过去时间</returns>
    public static bool IsPast(this DateTime date)
    {
        return date < DateTime.Now;
    }

    /// <summary>
    /// 时间部分比较
    /// </summary>
    /// <param name="time">时间一</param>
    /// <param name="timeToCompare">时间二</param>
    /// <returns>时间是否一致</returns>
    public static bool IsTimeEqual(this DateTime time, DateTime timeToCompare)
    {
        return time.TimeOfDay == timeToCompare.TimeOfDay;
    }

    /// <summary>
    /// 日期是否是今天
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>是否是今天</returns>
    public static bool IsToday(this DateTime date)
    {
        return date.Date == DateTime.Today;
    }

    /// <summary>
    /// 是否是工作日
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>是否是工作日</returns>
    public static bool IsWeekDay(this DateTime date)
    {
        return !(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
    }

    /// <summary>
    ///  是否是周末
    /// </summary>
    /// <param name="dt">DateTime</param>
    /// <returns>是否是周末</returns>
    public static bool IsWeekEnd(this DateTime dt)
    {
        return Convert.ToInt16(dt.DayOfWeek) > 5;
    }

    /// <summary>
    /// 是否周末
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>是否周末</returns>
    public static bool IsWeekendDay(this DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }

    /// <summary>
    /// 一周最后一天
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>一周最后一天</returns>
    public static DateTime LastDayOfWeek(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day).AddDays(6 - (int)date.DayOfWeek);
    }

    /// <summary>
    /// 时间字符串转换为时间类型
    /// </summary>
    /// <param name="data">需要转换的时间字符串</param>
    /// <param name="format">格式</param>
    /// <returns>若转换时间失败，则返回默认事件值</returns>
    public static DateTime ParseDateTimeString(this string data, string format)
    {
        return DateTime.ParseExact(data, format, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// 把秒转换成分钟
    /// </summary>
    /// <param name="second">秒</param>
    /// <returns>分钟</returns>
    public static int SecondToMinute(int second)
    {
        decimal minute = (decimal)((decimal)second / (decimal)60);
        return Convert.ToInt32(Math.Ceiling(minute));
    }

    /// <summary>
    /// 设置时间
    /// </summary>
    /// <param name="current">需要设置的时间</param>
    /// <param name="hour">需要设置小时部分</param>
    /// <returns>设置后的时间</returns>
    public static DateTime SetTime(this DateTime current, int hour)
    {
        return SetTime(current, hour, 0, 0, 0);
    }

    /// <summary>
    /// 设置时间
    /// </summary>
    /// <param name="current">需要设置的时间</param>
    /// <param name="hour">需要设置小时部分</param>
    /// <param name="minute">需要设置分钟部分</param>
    /// <returns>设置后的时间</returns>
    public static DateTime SetTime(this DateTime current, int hour, int minute)
    {
        return SetTime(current, hour, minute, 0, 0);
    }

    /// <summary>
    /// 设置时间
    /// </summary>
    /// <param name="current">需要设置的时间</param>
    /// <param name="hour">小时</param>
    /// <param name="minute">分钟</param>
    /// <param name="second">秒</param>
    /// <returns>设置后的时间</returns>
    public static DateTime SetTime(this DateTime current, int hour, int minute, int second)
    {
        return SetTime(current, hour, minute, second, 0);
    }

    /// <summary>
    /// 设置时间
    /// </summary>
    /// <param name="current">需要设置的时间.</param>
    /// <param name="hour">小时</param>
    /// <param name="minute">分钟</param>
    /// <param name="second">秒</param>
    /// <param name="millisecond">毫秒</param>
    /// <returns>设置后的时间</returns>
    public static DateTime SetTime(this DateTime current, int hour, int minute, int second, int millisecond)
    {
        return new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);
    }

    /// <summary>
    ///  一天起始时间
    /// </summary>
    /// <param name="date">需要操作的时间</param>
    /// <returns>DateTime</returns>
    public static DateTime StartOfDay(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day);
    }

    /// <summary>
    /// 一个月起始时间
    /// </summary>
    /// <param name="date">需要操作的时间</param>
    /// <returns>DateTime</returns>
    public static DateTime StartOfMonth(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, 1);
    }

    /// <summary>
    /// 一周起始时间
    /// </summary>
    /// <param name="date">需要操作的时间</param>
    /// <returns>DateTime</returns>
    public static DateTime StartOfWeek(this DateTime date)
    {
        return date.StartOfWeek(DayOfWeek.Monday);
    }

    /// <summary>
    /// 一周起始时间
    /// </summary>
    /// <param name="date">时间</param>
    /// <param name="startDayOfWeek">一周起始周天</param>
    /// <returns>一周起始时间</returns>
    public static DateTime StartOfWeek(this DateTime date, DayOfWeek startDayOfWeek)
    {
        DateTime start = new DateTime(date.Year, date.Month, date.Day);

        if (start.DayOfWeek != startDayOfWeek)
        {
            int d = startDayOfWeek - start.DayOfWeek;

            if (startDayOfWeek <= start.DayOfWeek)
            {
                return start.AddDays(d);
            }

            return start.AddDays(-7 + d);
        }

        return start;
    }

    /// <summary>
    /// 一年起始时间
    /// </summary>
    /// <param name="date">需要操作的时间</param>
    /// <returns>DateTime</returns>
    public static DateTime StartOfYear(this DateTime date)
    {
        return new DateTime(date.Year, 1, 1);
    }

    /// <summary>
    /// 转换成EpochTime
    /// </summary>
    /// <param name="date">需要操作的时间</param>
    /// <returns>DateTime</returns>
    public static TimeSpan ToEpochTimeSpan(this DateTime date)
    {
        return date.Subtract(new DateTime(1970, 1, 1));
    }

    /// <summary>
    /// 明天时间
    /// </summary>
    /// <param name="date">需要操作的时间</param>
    /// <returns>DateTime</returns>
    public static DateTime Tomorrow(this DateTime date)
    {
        return date.AddDays(1);
    }

    /// <summary>
    /// 昨天时间
    /// </summary>
    /// <param name="date">需要操作的时间</param>
    /// <returns>DateTime</returns>
    public static DateTime Yesterday(this DateTime date)
    {
        return date.AddDays(-1);
    }

    /// <summary>
    /// 获取时间戳【毫秒】
    /// </summary>
    /// <returns>时间戳</returns>
    public static long GetTimeStamp()
    {
        return Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds);
    }

    /// <summary>
    /// 转换为农历年
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns>农历年</returns>
    public static string ToChineseDate(this DateTime date)
    {
        var cnDate = new ChineseLunisolarCalendar();
        string[] months = { string.Empty, "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "冬月", "腊月" };
        string[] days = { string.Empty, "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十" };
        string[] years = { string.Empty, "甲子", "乙丑", "丙寅", "丁卯", "戊辰", "己巳", "庚午", "辛未", "壬申", "癸酉", "甲戌", "乙亥", "丙子", "丁丑", "戊寅", "己卯", "庚辰", "辛己", "壬午", "癸未", "甲申", "乙酉", "丙戌", "丁亥", "戊子", "己丑", "庚寅", "辛卯", "壬辰", "癸巳", "甲午", "乙未", "丙申", "丁酉", "戊戌", "己亥", "庚子", "辛丑", "壬寅", "癸丑", "甲辰", "乙巳", "丙午", "丁未", "戊申", "己酉", "庚戌", "辛亥", "壬子", "癸丑", "甲寅", "乙卯", "丙辰", "丁巳", "戊午", "己未", "庚申", "辛酉", "壬戌", "癸亥" };
        int year = cnDate.GetYear(date);
        string yearCn = years[cnDate.GetSexagenaryYear(date)];
        int month = cnDate.GetMonth(date),
            day = cnDate.GetDayOfMonth(date),
            leapMonth = cnDate.GetLeapMonth(year);
        string monthCn = months[month];

        if (leapMonth > 0)
        {
            monthCn = month == leapMonth ? $"闰{months[month - 1]}" : monthCn;
            monthCn = month > leapMonth ? months[month - 1] : monthCn;
        }

        return $"{yearCn}年{monthCn}{days[day]}";
    }

    /// <summary>
    /// 将阿拉伯数字转换中文日期数字
    /// </summary>
    /// <param name="data">日期范围1~31</param>
    /// <returns>中文日期数字</returns>
    public static string ToChineseDay(int data)
    {
        string reulst = string.Empty;

        if (!(data == 0 || data > 32))
        {
            string[] days = { "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "廿十", "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十", "三十一" };
            reulst = days[data];
        }

        return reulst;
    }

    /// <summary>
    /// 将阿拉伯数字转换成中文月份数字
    /// <para>eg:ConvertHelper.ToChineseMonth(1)==> "一"</para>
    /// </summary>
    /// <param name="data">月份范围1~12</param>
    /// <returns>中文月份数字</returns>
    public static string ToChineseMonth(this int data)
    {
        string result = string.Empty;

        if (!(data == 0 || data > 12))
        {
            string[] months = { "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二" };
            result = months[data];
        }

        return result;
    }

    #endregion
}

/// <summary>
/// 时间类型
/// </summary>
public enum DateTimePart
{
    Year, Month, Day, Hour, Minute, Second
}