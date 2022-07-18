using BB.Entity.Base;

namespace BB.Entity.WareHouse;

[Serializable]
public class ReportAnnualCostDetailInfo : BaseEntity
{    
    #region Field Members

    private int _mId = 0;         
    private int _mHeaderId = 0; //报表头ID          
    private int _mReportYear = 0; //报表年份          
    private string _mItemType = ""; //备件类别          
    private string _mCostCenterOrDept = ""; //成本中心或部门          
    private decimal _mOne = 0; //总金额          
    private decimal _mTwo = 0;         
    private decimal _mThree = 0;         
    private decimal _mFour = 0;         
    private decimal _mFive = 0;         
    private decimal _mSix = 0;         
    private decimal _mSeven = 0;         
    private decimal _mEight = 0;         
    private decimal _mNine = 0;         
    private decimal _mTen = 0;
    private decimal _mEleven = 0;
    private decimal _mTwelve = 0;
    private decimal _mTotal = 0;     
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
    /// 备件类别
    /// </summary>
    public virtual string ItemType
    {
        get
        {
            return _mItemType;
        }
        set
        {
            _mItemType = value;
        }
    }

    /// <summary>
    /// 成本中心或部门
    /// </summary>
    public virtual string CostCenterOrDept
    {
        get
        {
            return _mCostCenterOrDept;
        }
        set
        {
            _mCostCenterOrDept = value;
        }
    }

    /// <summary>
    /// 总金额
    /// </summary>
    public virtual decimal One
    {
        get
        {
            return _mOne;
        }
        set
        {
            _mOne = value;
        }
    }

    public virtual decimal Two
    {
        get
        {
            return _mTwo;
        }
        set
        {
            _mTwo = value;
        }
    }

    public virtual decimal Three
    {
        get
        {
            return _mThree;
        }
        set
        {
            _mThree = value;
        }
    }

    public virtual decimal Four
    {
        get
        {
            return _mFour;
        }
        set
        {
            _mFour = value;
        }
    }

    public virtual decimal Five
    {
        get
        {
            return _mFive;
        }
        set
        {
            _mFive = value;
        }
    }

    public virtual decimal Six
    {
        get
        {
            return _mSix;
        }
        set
        {
            _mSix = value;
        }
    }

    public virtual decimal Seven
    {
        get
        {
            return _mSeven;
        }
        set
        {
            _mSeven = value;
        }
    }

    public virtual decimal Eight
    {
        get
        {
            return _mEight;
        }
        set
        {
            _mEight = value;
        }
    }

    public virtual decimal Nine
    {
        get
        {
            return _mNine;
        }
        set
        {
            _mNine = value;
        }
    }

    public virtual decimal Ten
    {
        get
        {
            return _mTen;
        }
        set
        {
            _mTen = value;
        }
    }

    public virtual decimal Eleven
    {
        get
        {
            return _mEleven;
        }
        set
        {
            _mEleven = value;
        }
    }

    public virtual decimal Twelve
    {
        get
        {
            return _mTwelve;
        }
        set
        {
            _mTwelve = value;
        }
    }

    public virtual decimal Total
    {
        get
        {
            return _mTotal;
        }
        set
        {
            _mTotal = value;
        }
    }
    /// <summary>
    /// 额外的数据分类码
    /// </summary>
    public virtual string ReportCode
    {
        get
        {
            return _mReportCode;
        }
        set
        {
            _mReportCode = value;
        }
    }


    #endregion

}