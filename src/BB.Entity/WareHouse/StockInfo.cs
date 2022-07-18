using BB.Entity.Base;

namespace BB.Entity.WareHouse;

[Serializable]
public class StockInfo : BaseEntity
{    
    #region Field Members

    private int _mId = 0;         
    private string _mItemNo = "";         
    private string _mItemName = "";         
    private string _mItemBigType = "";         
    private string _mItemType = "";         
    private int _mStockQuantity = 0; //库存量          
    private string _mStockMoney = ""; //库存金额          
    private int _mLowWarning = 0; //低储预警          
    private int _mHighWarning = 0; //超储预警          
    private string _mWareHouse = "";         
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
    /// 备件编号
    /// </summary>
    public virtual string ItemNo
    {
        get
        {
            return _mItemNo;
        }
        set
        {
            _mItemNo = value;
        }
    }

    /// <summary>
    /// 备件名称
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
    /// 备件属类
    /// </summary>
    public virtual string ItemBigType
    {
        get
        {
            return _mItemBigType;
        }
        set
        {
            _mItemBigType = value;
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
    /// 库存量
    /// </summary>
    public virtual int StockQuantity
    {
        get
        {
            return _mStockQuantity;
        }
        set
        {
            _mStockQuantity = value;
        }
    }

    /// <summary>
    /// 库存金额
    /// </summary>
    public virtual string StockMoney
    {
        get
        {
            return _mStockMoney;
        }
        set
        {
            _mStockMoney = value;
        }
    }

    /// <summary>
    /// 低储预警
    /// </summary>
    public virtual int LowWarning
    {
        get
        {
            return _mLowWarning;
        }
        set
        {
            _mLowWarning = value;
        }
    }

    /// <summary>
    /// 超储预警
    /// </summary>
    public virtual int HighWarning
    {
        get
        {
            return _mHighWarning;
        }
        set
        {
            _mHighWarning = value;
        }
    }

    public virtual string WareHouse
    {
        get
        {
            return _mWareHouse;
        }
        set
        {
            _mWareHouse = value;
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