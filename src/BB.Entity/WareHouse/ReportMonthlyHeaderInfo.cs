using BB.Entity.Base;

namespace BB.Entity.WareHouse;

[Serializable]
public class ReportMonthlyHeaderInfo : BaseEntity
{    
    #region Field Members

    private int _mId = 0;
    private int _mReportType = 0; //报表类型：报表类型：1为库房部门结存，2库房结存，3为所有库房结存报表（包括备件属类，备件类型），4为车间成本月报表
    private string _mReportTitle = ""; //报表标题          
    private int _mReportYear = 0; //报表年份          
    private int _mReportMonth = 0; //报表月份          
    private string _mYearMonth = ""; //报表年月          
    private DateTime _mCreateDate = DateTime.Now; //报表创建日期          
    private string _mCreator = ""; //报表创建人员          
    private string _mNote = ""; //备注          

    #endregion

    #region Property Members
        
    public virtual int Id
    {
        get
        {
            return _mId;
        }
        set
        {
            _mId = value;
        }
    }

    /// <summary>
    /// 报表类型：1为库房部门结存，2库房结存，3为所有库房结存报表（包括备件属类，备件类型），4为车间成本月报表
    /// </summary>
    public virtual int ReportType
    {
        get
        {
            return _mReportType;
        }
        set
        {
            _mReportType = value;
        }
    }

    /// <summary>
    /// 报表标题
    /// </summary>
    public virtual string ReportTitle
    {
        get
        {
            return _mReportTitle;
        }
        set
        {
            _mReportTitle = value;
        }
    }

    /// <summary>
    /// 报表年份
    /// </summary>
    public virtual int ReportYear
    {
        get
        {
            return _mReportYear;
        }
        set
        {
            _mReportYear = value;
        }
    }

    /// <summary>
    /// 报表月份
    /// </summary>
    public virtual int ReportMonth
    {
        get
        {
            return _mReportMonth;
        }
        set
        {
            _mReportMonth = value;
        }
    }

    /// <summary>
    /// 报表年月
    /// </summary>
    public virtual string YearMonth
    {
        get
        {
            return _mYearMonth;
        }
        set
        {
            _mYearMonth = value;
        }
    }

    /// <summary>
    /// 报表创建日期
    /// </summary>
    public virtual DateTime CreateDate
    {
        get
        {
            return _mCreateDate;
        }
        set
        {
            _mCreateDate = value;
        }
    }

    /// <summary>
    /// 报表创建人员
    /// </summary>
    public virtual string Creator
    {
        get
        {
            return _mCreator;
        }
        set
        {
            _mCreator = value;
        }
    }

    /// <summary>
    /// 备注
    /// </summary>
    public virtual string Note
    {
        get
        {
            return _mNote;
        }
        set
        {
            _mNote = value;
        }
    }


    #endregion

}