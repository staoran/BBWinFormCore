using BB.Entity.Base;

namespace BB.Entity.WareHouse;

[Serializable]
public class WareHouseInfo : BaseEntity
{    
    #region Field Members

    private int _mId = 0;         
    private string _mName = ""; //仓库名称          
    private string _mManager = ""; //仓库负责人          
    private string _mPhone = ""; //联系电话          
    private string _mAddress = ""; //仓库地址          
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
    /// 仓库名称
    /// </summary>
    public virtual string Name
    {
        get
        {
            return _mName;
        }
        set
        {
            _mName = value;
        }
    }

    /// <summary>
    /// 仓库负责人
    /// </summary>
    public virtual string Manager
    {
        get
        {
            return _mManager;
        }
        set
        {
            _mManager = value;
        }
    }

    /// <summary>
    /// 联系电话
    /// </summary>
    public virtual string Phone
    {
        get
        {
            return _mPhone;
        }
        set
        {
            _mPhone = value;
        }
    }

    /// <summary>
    /// 仓库地址
    /// </summary>
    public virtual string Address
    {
        get
        {
            return _mAddress;
        }
        set
        {
            _mAddress = value;
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