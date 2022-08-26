using System.Xml;
using BB.Tools.File;

namespace BB.Tools.Utils;

/// <summary>
///	常用显示日期时间、农历、生肖的日历类
/// </summary>
public class CCalendar
{
    /// <summary>
    /// 自定义的CCalendarDate.XML的限定名称，命名空间修改，必须也跟着修改，
    /// 右键查看CCalendarData.xml文件的属性可以看到它的命名空间
    /// </summary>
    const string CCalendarDateFile = "BB.Tools.Utils.CCalendarData.xml";

    /// <summary>
    /// 结构。日期对象
    /// </summary>
    struct StructDate
    {
        public int year;
        public int month;
        public int day;
        public bool isLeap;			//是否闰月
        public int yearCyl;			//年干支
        public int monthCyl;		//月干支
        public int dayCyl;			//日干支
    }

    /// <summary>
    /// 结构。完整的日期对象
    /// </summary>
    public struct StructDateFullInfo
    {
        /// <summary>
        /// 公历年
        /// </summary>
        public int Year;
        /// <summary>
        /// 公历月
        /// </summary>
        public int Month;
        /// <summary>
        /// 公历日
        /// </summary>
        public int Day;
        /// <summary>
        /// 是否闰月
        /// </summary>
        public bool IsLeap;			//是否闰月
        /// <summary>
        /// 农历年
        /// </summary>
        public int Cyear;			//农历年
        /// <summary>
        /// 农历年名称
        /// </summary>
        public string Scyear;		//农历年名称
        /// <summary>
        /// 干支
        /// </summary>
        public string CyearCyl;		//干支年
        /// <summary>
        /// 农历月
        /// </summary>
        public int Cmonth;			//农历月
        /// <summary>
        /// 农历月名称
        /// </summary>
        public string Scmonth;		//农历月名称
        /// <summary>
        /// 干支月
        /// </summary>
        public string CmonthCyl;	//干支月
        /// <summary>
        /// 农历日
        /// </summary>
        public int Cday;			//农历日
        /// <summary>
        /// 农历日名称
        /// </summary>
        public string Scday;		//农历日名称
        /// <summary>
        /// 干支日
        /// </summary>
        public string CdayCyl;		//干支日
        /// <summary>
        /// 节气
        /// </summary>
        public string solarterm;	//节气
        /// <summary>
        /// 星期几
        /// </summary>
        public string DayInWeek;	//星期几
        /// <summary>
        /// 节日
        /// </summary>
        public string Feast;		//节日
        /// <summary>
        /// 系统问候语
        /// </summary>
        public string Info;			//系统问候语
        /// <summary>
        /// 主题图片
        /// </summary>
        public string Image;	    //主题图片
        /// <summary>
        /// 完整的日期信息
        /// </summary>
        public string Fullinfo;		//完整的日期信息
        /// <summary>
        /// 有特别的问候语吗
        /// </summary>
        public bool SayHello;		//有特别的问候语吗？
    }

    #region 农历相关数据

    //农历月份信息
    int[] _lunarInfo = new[]{0x04bd8,0x04ae0,0x0a570,0x054d5,0x0d260,0x0d950,0x16554,0x056a0,0x09ad0,0x055d2, 
        0x04ae0,0x0a5b6,0x0a4d0,0x0d250,0x1d255,0x0b540,0x0d6a0,0x0ada2,0x095b0,0x14977, 
        0x04970,0x0a4b0,0x0b4b5,0x06a50,0x06d40,0x1ab54,0x02b60,0x09570,0x052f2,0x04970, 
        0x06566,0x0d4a0,0x0ea50,0x06e95,0x05ad0,0x02b60,0x186e3,0x092e0,0x1c8d7,0x0c950, 
        0x0d4a0,0x1d8a6,0x0b550,0x056a0,0x1a5b4,0x025d0,0x092d0,0x0d2b2,0x0a950,0x0b557, 
        0x06ca0,0x0b550,0x15355,0x04da0,0x0a5d0,0x14573,0x052d0,0x0a9a8,0x0e950,0x06aa0, 
        0x0aea6,0x0ab50,0x04b60,0x0aae4,0x0a570,0x05260,0x0f263,0x0d950,0x05b57,0x056a0, 
        0x096d0,0x04dd5,0x04ad0,0x0a4d0,0x0d4d4,0x0d250,0x0d558,0x0b540,0x0b5a0,0x195a6, 
        0x095b0,0x049b0,0x0a974,0x0a4b0,0x0b27a,0x06a50,0x06d40,0x0af46,0x0ab60,0x09570, 
        0x04af5,0x04970,0x064b0,0x074a3,0x0ea50,0x06b58,0x055c0,0x0ab60,0x096d5,0x092e0, 
        0x0c960,0x0d954,0x0d4a0,0x0da50,0x07552,0x056a0,0x0abb7,0x025d0,0x092d0,0x0cab5, 
        0x0a950,0x0b4a0,0x0baa4,0x0ad50,0x055d9,0x04ba0,0x0a5b0,0x15176,0x052b0,0x0a930, 
        0x07954,0x06aa0,0x0ad50,0x05b52,0x04b60,0x0a6e6,0x0a4e0,0x0d260,0x0ea65,0x0d530, 
        0x05aa0,0x076a3,0x096d0,0x04bd7,0x04ad0,0x0a4d0,0x1d0b6,0x0d250,0x0d520,0x0dd45, 
        0x0b5a0,0x056d0,0x055b2,0x049b0,0x0a577,0x0a4b0,0x0aa50,0x1b255,0x06d20,0x0ada0};
    //公历月份
    int[] _solarMonth = new[] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    string[] _cMonthName = new[] { "", "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月" };

    //天干
    string[] _gan = new[] { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
    //地支
    string[] _zhi = new[] { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
    //生肖
    string[] _animals = new[] { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };
    //节气
    string[] _solarTerm = new[] {"小寒","大寒","立春","雨水","惊蛰","春分","清明","谷雨","立夏","小满"
        ,"芒种","夏至","小暑","大暑","立秋","处暑","白露","秋分","寒露","霜降"
        ,"立冬","小雪","大雪","冬至"};
    //节气对应数值？
    int[] _solarTermInfo = new[] {0,21208,42467,63836,85337,107014,128867,150921,173149,195551,218072
        ,240693,263343,285989,308563,331033,353350,375494,397447,419210,440795
        ,462224,483532,504758};
    //农历日子
    string[] _nStr1 = new[] { "日", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
    string[] _nStr2 = new[] { "初", "十", "廿", "卅", "　" };
    //公历月份名称
    string[] _monthName = new[] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

    #endregion

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public CCalendar() { }

    //传回农历 y 年的总天数
    private int GetLYearDays(int y)
    {
        int sum = 348;

        for (int i = 0x8000; i > 0x8; i >>= 1)
        {
            sum += ((_lunarInfo[y - 1900] & i) > 0) ? 1 : 0;
        }

        return sum + GetLeapDays(y);
    }

    //传回农历 y 年 m 月的总天数 
    private int GetLMonthDays(int y, int m)
    {
        return (((_lunarInfo[y - 1900] & (0x10000 >> m)) > 0) ? 30 : 29);
    }

    //传回农历 y 年闰哪个月 1-12 , 没闰传回 0
    private int GetLeapMonth(int y)
    {
        return (_lunarInfo[y - 1900] & 0xf);
    }

    //传回农历 y 年闰月的天数
    private int GetLeapDays(int y)
    {
        if (GetLeapMonth(y) > 0)
        {
            return (((_lunarInfo[y - 1900] & 0x10000) > 0) ? 30 : 29);
        }
        else
        {
            return 0;
        }
    }

    //得到农历日期
    private StructDate GetLunar(DateTime date)
    {
        StructDate sd;
        int i = 0;
        int leap = 0, temp = 0;
        DateTime baseDate = new DateTime(1900, 1, 31);	//基准时间
        int offset = (date - baseDate).Days;			//与基准时间相隔天数

        sd.dayCyl = offset + 40;
        sd.monthCyl = 14;

        for (i = 1900; i < 2050 && offset > 0; i++)
        {
            temp = GetLYearDays(i);
            offset -= temp;
            sd.monthCyl += 12;
        }
        if (offset < 0)
        {
            offset += temp;
            i--;
            sd.monthCyl -= 12;
        }

        sd.year = i;
        sd.yearCyl = i - 1864;

        //闰哪个月
        leap = GetLeapMonth(i);
        sd.isLeap = false;
        for (i = 1; i < 13 && offset > 0; i++)
        {
            //闰月 
            if (leap > 0 && i == (leap + 1) && sd.isLeap == false)
            {
                --i;
                sd.isLeap = true;
                temp = GetLeapDays(sd.year);
            }
            else
            {
                temp = GetLMonthDays(sd.year, i);
            }
            //解除闰月 
            if (sd.isLeap == true && i == (leap + 1))
            {
                sd.isLeap = false;
            }
            offset -= temp;
            if (sd.isLeap == false)
            {
                sd.monthCyl++;
            }
        }
        if (offset == 0 && leap > 0 && i == leap + 1)
        {
            if (sd.isLeap)
            {
                sd.isLeap = false;
            }
            else
            {
                sd.isLeap = true;
                --i;
                --sd.monthCyl;
            }
        }
        if (offset < 0)
        {
            offset += temp;
            --i;
            --sd.monthCyl;
        }

        sd.month = i;
        sd.day = offset + 1;

        return sd;
    }

    //传回公历 y 年 m 月的天数 
    private int SolarDays(int y, int m)
    {
        if (m == 2)
        {
            return (((y % 4 == 0) && (y % 100 != 0) || (y % 400 == 0)) ? 29 : 28);
        }
        else
        {
            return (_solarMonth[m]);
        }
    }

    //传入 offset 传回天干地支, 0=甲子 
    private string Cyclical(int num)
    {
        return (_gan[num % 10] + _zhi[num % 12]);
    }

    //某年的第n个节气为几日(从0,即小寒起算)
    //	n:节气下标
    private int GetSolarTermDay(int y, int n)
    {
        double minutes = 0;
        //1900年1月6日：小寒
        DateTime baseDate = new DateTime(1900, 1, 6, 2, 5, 0);
        minutes = 525948.766245 * (y - 1900) + _solarTermInfo[n - 1];

        DateTime veryDate = baseDate.AddMinutes(minutes);

        return veryDate.Day;
    }

    //农历日子 
    private string GetCDay(int d)
    {
        string s = "";

        switch (d)
        {
            case 10:
                s = "初十";
                break;
            case 20:
                s = "二十";
                break;
            case 30:
                s = "三十";
                break;
            default:
                s = _nStr2[(int)Math.Floor((double)d / 10)];
                s += _nStr1[d % 10];
                break;
        }
        return (s);
    }

    /// <summary>
    /// 得到日期信息
    /// </summary>
    /// <param name="d">待检查的日子</param>
    /// <returns>日期信息</returns>
    public StructDateFullInfo GetDateInfo(DateTime d)
    {            
        string calendarXmlData = FileUtil.ReadFileFromEmbedded(CCalendarDateFile);

        StructDateFullInfo dayinfo;
        StructDate day = GetLunar(d);

        dayinfo.IsLeap = day.isLeap;

        dayinfo.Year = d.Year;
        dayinfo.Cyear = day.year;
        dayinfo.Scyear = _animals[(day.year - 4) % 12];
        dayinfo.CyearCyl = Cyclical(day.yearCyl);//干支年

        dayinfo.Month = d.Month;
        dayinfo.Cmonth = day.month;
        dayinfo.Scmonth = _cMonthName[day.month];
        dayinfo.CmonthCyl = Cyclical(day.monthCyl);//干支月

        dayinfo.Day = d.Day;
        dayinfo.Cday = day.day;
        dayinfo.Scday = GetCDay(day.day);//日子
        dayinfo.CdayCyl = Cyclical(day.dayCyl);//干支日

        switch (d.DayOfWeek)
        {
            case DayOfWeek.Sunday:
                dayinfo.DayInWeek = "星期日";
                break;
            case DayOfWeek.Monday:
                dayinfo.DayInWeek = "星期一";
                break;
            case DayOfWeek.Tuesday:
                dayinfo.DayInWeek = "星期二";
                break;
            case DayOfWeek.Wednesday:
                dayinfo.DayInWeek = "星期三";
                break;
            case DayOfWeek.Thursday:
                dayinfo.DayInWeek = "星期四";
                break;
            case DayOfWeek.Friday:
                dayinfo.DayInWeek = "星期五";
                break;
            case DayOfWeek.Saturday:
                dayinfo.DayInWeek = "星期六";
                break;
            default:
                dayinfo.DayInWeek = "星期？";
                break;
        }

        //节气
        //每个月有两个节气
        int d1 = GetSolarTermDay(d.Year, d.Month * 2 - 1);
        int d2 = GetSolarTermDay(d.Year, d.Month * 2);
        if (dayinfo.Day == d1)
        {
            dayinfo.solarterm = _solarTerm[d.Month * 2 - 2];
        }
        else if (dayinfo.Day == d2)
        {
            dayinfo.solarterm = _solarTerm[d.Month * 2 - 1];
        }
        else
        {
            dayinfo.solarterm = "";
        }

        //节日及问候语
        dayinfo.Info = "";
        dayinfo.Feast = "";
        dayinfo.Image = "";
        dayinfo.SayHello = false;
        XmlDocument feastdoc = new XmlDocument();
        feastdoc.LoadXml(calendarXmlData);

        //公历
        XmlNodeList nodeList = feastdoc.SelectNodes("descendant::AD/feast[@day='" + d.ToString("MMdd") + "']");
        foreach (XmlNode root in nodeList)
        {
            dayinfo.Feast += root.Attributes["name"].InnerText + " ";
            if (root.Attributes["sayhello"].InnerText == "yes")
            {//需要显示节日问候语
                dayinfo.Info = root["hello"].InnerText;
                //看看是否需要计算周年
                if (root["startyear"] != null)
                {
                    int startyear = Convert.ToInt32(root["startyear"].InnerText);
                    dayinfo.Info = dayinfo.Info.Replace("_YEARS_", (d.Year - startyear).ToString());
                }
                dayinfo.Image = root["img"].InnerText;
                dayinfo.SayHello = true;
            }
        }

        //农历
        string smmdd = "";
        smmdd = (dayinfo.Cmonth.ToString().Length == 2) ? dayinfo.Cmonth.ToString() : ("0" + dayinfo.Cmonth);
        smmdd += (dayinfo.Cday.ToString().Length == 2) ? dayinfo.Cday.ToString() : ("0" + dayinfo.Cday);
        XmlNode feast = feastdoc.SelectSingleNode("descendant::LUNAR/feast[@day='" + smmdd + "']");
        if (feast != null)
        {
            dayinfo.Feast += feast.Attributes["name"].InnerText;

            if (feast.Attributes["sayhello"].InnerText == "yes")
            {//需要显示节日问候语
                dayinfo.Info += feast["hello"].InnerText;
                dayinfo.Image = feast["img"].InnerText;
                dayinfo.SayHello = true;
            }
        }
        //普通日子或没有庆贺语
        if (dayinfo.Info == "")
        {
            feast = feastdoc.SelectSingleNode("descendant::NORMAL/day[@time1<'" + d.ToString("HHmm") + "']");
            if (feast != null)
            {
                dayinfo.Info = feast["hello"].InnerText;
                dayinfo.Image = feast["img"].InnerText;
            }
        }

        dayinfo.Fullinfo = dayinfo.Year + "年" + dayinfo.Month + "月" + dayinfo.Day + "日";
        dayinfo.Fullinfo += dayinfo.DayInWeek;
        dayinfo.Fullinfo += " 农历" + dayinfo.CyearCyl + "[" + dayinfo.Scyear + "]年";
        if (dayinfo.IsLeap)
        {
            dayinfo.Fullinfo += "闰";
        }
        dayinfo.Fullinfo += dayinfo.Scmonth + dayinfo.Scday;
        if (dayinfo.solarterm != "")
        {
            dayinfo.Fullinfo += "  " + dayinfo.solarterm;
        }

        return dayinfo;
    }

    /// <summary>
    /// 得到精简日期信息（不含节日）
    /// </summary>
    /// <param name="d">待检查的日子</param>
    /// <returns>日期信息</returns>
    public StructDateFullInfo GetDateTidyInfo(DateTime d)
    {
        StructDateFullInfo dayinfo;
        StructDate day = GetLunar(d);

        dayinfo.IsLeap = day.isLeap;

        dayinfo.Year = d.Year;
        dayinfo.Cyear = day.year;
        dayinfo.Scyear = _animals[(day.year - 4) % 12];
        dayinfo.CyearCyl = Cyclical(day.yearCyl);//干支年

        dayinfo.Month = d.Month;
        dayinfo.Cmonth = day.month;
        dayinfo.Scmonth = _cMonthName[day.month];
        dayinfo.CmonthCyl = Cyclical(day.monthCyl);//干支月

        dayinfo.Day = d.Day;
        dayinfo.Cday = day.day;
        dayinfo.Scday = GetCDay(day.day);//日子
        dayinfo.CdayCyl = Cyclical(day.dayCyl);//干支日

        switch (d.DayOfWeek)
        {
            case DayOfWeek.Sunday:
                dayinfo.DayInWeek = "星期日";
                break;
            case DayOfWeek.Monday:
                dayinfo.DayInWeek = "星期一";
                break;
            case DayOfWeek.Tuesday:
                dayinfo.DayInWeek = "星期二";
                break;
            case DayOfWeek.Wednesday:
                dayinfo.DayInWeek = "星期三";
                break;
            case DayOfWeek.Thursday:
                dayinfo.DayInWeek = "星期四";
                break;
            case DayOfWeek.Friday:
                dayinfo.DayInWeek = "星期五";
                break;
            case DayOfWeek.Saturday:
                dayinfo.DayInWeek = "星期六";
                break;
            default:
                dayinfo.DayInWeek = "星期？";
                break;
        }

        dayinfo.Info = "";
        dayinfo.Feast = "";
        dayinfo.Image = "";
        dayinfo.SayHello = false;

        //节气
        //每个月有两个节气
        int d1 = GetSolarTermDay(d.Year, d.Month * 2 - 1);
        int d2 = GetSolarTermDay(d.Year, d.Month * 2);
        if (dayinfo.Day == d1)
        {
            dayinfo.solarterm = _solarTerm[d.Month * 2 - 2];
        }
        else if (dayinfo.Day == d2)
        {
            dayinfo.solarterm = _solarTerm[d.Month * 2 - 1];
        }
        else
        {
            dayinfo.solarterm = "";
        }

        dayinfo.Fullinfo = dayinfo.Year + "年" + dayinfo.Month + "月" + dayinfo.Day + "日";
        dayinfo.Fullinfo += " " + dayinfo.DayInWeek;
        dayinfo.Fullinfo += " 农历" + dayinfo.CyearCyl + "（" + dayinfo.Scyear + "）年";
        if (dayinfo.IsLeap)
        {
            dayinfo.Fullinfo += "闰";
        }
        dayinfo.Fullinfo += dayinfo.Scmonth + dayinfo.Scday;
        if (dayinfo.solarterm != "")
        {
            dayinfo.Fullinfo += " " + dayinfo.solarterm;
        }

        return dayinfo;
    }

}