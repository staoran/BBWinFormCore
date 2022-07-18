using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Staff;

/// <summary>
/// 学术任职情况
/// </summary>
[DataContract]
public class StaffAcademicInfo : BaseEntity
{ 
	/// <summary>
	/// 默认构造函数（需要初始化属性的在此处理）
	/// </summary>
	public StaffAcademicInfo()
	{
		ID = System.Guid.NewGuid().ToString();
		Seq = 0;
	}

	#region Property Members
        
	[DataMember]
	public virtual string ID { get; set; }

	/// <summary>
	/// 人员ID
	/// </summary>
	[DataMember]
	public virtual string Staff_ID { get; set; }

	/// <summary>
	/// 起始年月
	/// </summary>
	[DataMember]
	public virtual string StartDate { get; set; }

	/// <summary>
	/// 截止年月
	/// </summary>
	[DataMember]
	public virtual string EndDate { get; set; }

	/// <summary>
	/// 任职组织
	/// </summary>
	[DataMember]
	public virtual string ServeOraganize { get; set; }

	/// <summary>
	/// 职务
	/// </summary>
	[DataMember]
	public virtual string OfficeRank { get; set; }

	/// <summary>
	/// 序号
	/// </summary>
	[DataMember]
	public virtual int Seq { get; set; }


	#endregion

}