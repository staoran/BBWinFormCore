using BB.Entity.Base;

namespace BB.Entity.WareHouse;

[Serializable]
public class ReportAnnualCostHeaderInfo : BaseEntity
{    
    #region Field Members

    private int _mId = 0;         
    private int _mReportType = 0; //报表类型：1为全年费用汇总报表          
    private string _mReportTitle = ""; //报表标题          
    private int _mReportYear = 0; //报表年份          
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
    /// 报表类型：1为全年费用汇总报表
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