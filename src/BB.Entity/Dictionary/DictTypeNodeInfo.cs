using System.Runtime.Serialization;

namespace BB.Entity.Dictionary;

/// <summary>
/// 字典类型节点
/// </summary>
[Serializable]
[DataContract]
public class DictTypeNodeInfo : DictTypeInfo
{
	/// <summary>
	/// 子菜单实体类对象集合
	/// </summary>
	[DataMember]
	public List<DictTypeNodeInfo> Children { get; set; }

	/// <summary>
	/// 构造函数
	/// </summary>
	public DictTypeNodeInfo()
	{
		Children = new List<DictTypeNodeInfo>();
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="typeInfo">字典类型对象</param>
	public DictTypeNodeInfo(DictTypeInfo typeInfo) : this()
	{
		base.ID = typeInfo.ID;
		base.Name = typeInfo.Name;
		base.Remark = typeInfo.Remark;
		base.SEQ = typeInfo.SEQ;
		base.PID = typeInfo.PID;
		base.Editor = typeInfo.Editor;
		base.LastUpdated = typeInfo.LastUpdated;
		base.DbSQL = typeInfo.DbSQL;
	}
}