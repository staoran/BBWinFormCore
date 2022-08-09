namespace BB.Tools.TaskQueue;

[Serializable]
internal class Task
{
	public Guid Id
	{
		get;
		set;
	}

	public object Data
	{
		get;
		set;
	}

	public TaskStatus Status
	{
		get;
		set;
	}

	public TaskPriority Priority
	{
		get;
		set;
	}

	//创建的时间。
	public DateTime CreatedOn
	{
		get;
		set;
	}

	//创建的序号。注意，序号不是全局唯一的，只是用于当两个Task的创建时间一样时能够排序。
	public int Number
	{
		get;
		set;
	}

}