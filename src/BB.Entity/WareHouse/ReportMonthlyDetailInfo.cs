using BB.Entity.Base;

namespace BB.Entity.WareHouse;

[Serializable]
public class ReportMonthlyDetailInfo : BaseEntity
{    
    #region Field Members

    private int _mId = 0;         
    private int _mHeaderId = 0; //报表头ID          
    private int _mReportYear = 0; //报表年份          
    private int _mReportMonth = 0; //报表月份          
    private string _mYearMonth = ""; //报表年月          
    private string _mItemName = ""; //项目名称          
    private int _mLastCount = 0; //上月结存数量          
    private decimal _mLastMoney = 0; //上月结存金额          
    private int _mCurrentInCount = 0; //本月入库数量          
    private decimal _mCurrentInMoney = 0; //本月入库金额          
    private int _mCurrentOutCount = 0; //本月出库数量          
    private decimal _mCurrentOutMoney = 0; //本月出库金额          
    private int _mCurrentCount = 0; //本月结存数量          
    private decimal _mCurrentMoney = 0; //本月结存金额          
    private string _mReportCode = ""; //额外的数据分类码          

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
    /// 报表头ID
    /// </summary>
    public virtual int HeaderId
    {
        get
        {
            return _mHeaderId;
        }
        set
        {
            _mHeaderId = value;
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
    /// 项目名称
    /// </summary>
    public virtual string ItemName
    {
        get
        {
            return _mItemName;
        }
        set
        {
            _mItemName = value;
        }
    }

    /// <summary>
    /// 上月结存数量
    /// </summary>
    public virtual int LastCount
    {
        get
        {
            return _mLastCount;
        }
        set
        {
            _mLastCount = value;
        }
    }

    /// <summary>
    /// 上月结存金额
    /// </summary>
    public virtual decimal LastMoney
    {
        get
        {
            return _mLastMoney;
        }
        set
        {
            _mLastMoney = value;
        }
    }

    /// <summary>
    /// 本月入库数量
    /// </summary>
    public virtual int CurrentInCount
    {
        get
        {
            return _mCurrentInCount;
        }
        set
        {
            _mCurrentInCount = value;
        }
    }

    /// <summary>
    /// 本月入库金额
    /// </summary>
    public virtual decimal CurrentInMoney
    {
        get
        {
            return _mCurrentInMoney;
        }
        set
        {
            _mCurrentInMoney = value;
        }
    }

    /// <summary>
    /// 本月出库数量
    /// </summary>
    public virtual int CurrentOutCount
    {
        get
        {
            return _mCurrentOutCount;
        }
        set
        {
            _mCurrentOutCount = value;
        }
    }

    /// <summary>
    /// 本月出库金额
    /// </summary>
    public virtual decimal CurrentOutMoney
    {
        get
        {
            return _mCurrentOutMoney;
        }
        set
        {
            _mCurrentOutMoney = value;
        }
    }

    /// <summary>
    /// 本月结存数量
    /// </summary>
    public virtual int CurrentCount
    {
        get
        {
            return _mCurrentCount;
        }
        set
        {
            _mCurrentCount = value;
        }
    }

    /// <summary>
    /// 本月结存金额
    /// </summary>
    public virtual decimal CurrentMoney
    {
        get
        {
            return _mCurrentMoney;
        }
        set
        {
            _mCurrentMoney = value;
        }
    }

    /// <summary>
    /// 额外的数据分类码
    /// </summary>
    public string ReportCode
    {
        get { return _mReportCode; }
        set { _mReportCode = value; }
    }

    #endregion

}