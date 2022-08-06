using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BB.Tools.Entity;

public delegate void PageInfoChanged(PagerInfo info);

[Serializable]
[DataContract]
public class PagerInfo
{
    public event PageInfoChanged? OnPageInfoChanged;

    private int _pageNo; //当前页码
    private int _pageSize;//每页显示的记录
    private int _totalRows;//记录总数

    #region 属性变量

    /// <summary>
    /// 获取或设置当前页码
    /// </summary>
    [XmlElement(ElementName = "CurrenetPageIndex")]
    [DataMember]
    public int PageNo
    {
        get => _pageNo;
        set
        {
            _pageNo = value;

            OnPageInfoChanged?.Invoke(this);
        }
    }

    /// <summary>
    /// 每页条数
    /// </summary>
    [XmlElement(ElementName = "PageSize")]
    [DataMember]
    public int PageSize
    {
        get => _pageSize;
        set
        {
            _pageSize = value;
            OnPageInfoChanged?.Invoke(this);
        }
    }

    /// <summary>
    /// 总条数
    /// </summary>
    [XmlElement(ElementName = "RecordCount")]
    [DataMember]
    public int TotalRows
    {
        get => _totalRows;
        set
        {
            _totalRows = value;
            OnPageInfoChanged?.Invoke(this);
        }
    }

    /// <summary>
    /// 总页数
    /// </summary>
    [XmlElement(ElementName = "TotalPage")]
    [DataMember]
    public int TotalPage { get; set; }

    /// <summary>
    /// 是否有上一页
    /// </summary>
    [XmlElement(ElementName = "HasPrevPages")]
    [DataMember]
    public bool HasPrevPages => PageNo > 1;

    /// <summary>
    /// 是否有下一页
    /// </summary>
    [XmlElement(ElementName = "HasNextPages")]
    [DataMember]
    public bool HasNextPages => PageNo < TotalPage;

    #endregion
}