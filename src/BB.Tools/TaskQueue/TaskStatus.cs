namespace BB.Tools.TaskQueue;

/// <summary>
/// 任务的状态。
/// </summary>
public enum TaskStatus
{
	/// <summary>
	/// 未执行。
	/// </summary>
	Unexecuted = 0,

	/// <summary>
	/// 已执行。
	/// </summary>
	Executed = 1,

	/// <summary>
	/// 已取消。
	/// </summary>
	Cancelled = 2,

	/// <summary>
	/// 已执行，但发生错误。
	/// </summary>
	Error
}