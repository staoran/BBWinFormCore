using System.Runtime.Serialization;

namespace BB.Tools.Entity;

/// <summary>
/// 框架用来记录字典键值的类，用于Comobox等控件对象的值传递
/// </summary>
[Serializable]
[DataContract]
public class CListItem
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public CListItem() { }

    /// <summary>
    /// 参数化构造CListItem对象
    /// </summary>
    /// <param name="text">显示的内容</param>
    /// <param name="value">实际的值内容</param>
    public CListItem(string text, string value)
    {
        Text = text;
        Value = value;
    }

    /// <summary>
    /// 参数化构造CListItem对象
    /// </summary>
    /// <param name="text">显示的内容</param>
    public CListItem(string text)
    {
        Text = text;
        Value = text;
    }

    /// <summary>
    /// 显示内容
    /// </summary>
    [DataMember]
    public string Text { get; set; }

    /// <summary>
    /// 实际值内容
    /// </summary>
    [DataMember]
    public string Value { get; set; }

    /// <summary>
    /// 返回显示的内容
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Text;
    }

}