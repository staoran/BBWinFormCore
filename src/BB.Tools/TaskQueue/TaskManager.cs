using System.Collections;

namespace BB.Tools.TaskQueue;

/// <summary>
/// 任务的管理器，负责任务的创建、释放、持久化。
/// </summary>
public class TaskManager : IDisposable
{
	private Hashtable _taskHashtable = Hashtable.Synchronized(new Hashtable()); //线程安全的任务表。

	private WorkQueue _workQueue; //任务的工作队列。

	public TaskManager(Action<object> command, string name)
	{
		_workQueue = new WorkQueue(command, name);
		_workQueue.TaskExecuted += _WorkQueue_TaskExecuted;
		_workQueue.TaskExecuteError += _WorkQueue_TaskExecuteError;
	}

	/// <summary>
	/// 任务已执行事件
	/// </summary>
	public event TaskExecutedHandler TaskExecuted; //任务已执行事件。

	/// <summary>
	/// 任务执行时发生错误的事件
	/// </summary>
	public event TaskExecuteErrorHandler TaskExecuteError; //任务执行时发生错误的事件。

	#region - _WorkQueue 事件 -

	void _WorkQueue_TaskExecuted(Guid taskId)
	{
		Remove(taskId); //任务执行完毕（成功）后，从工作队列里移除。

		if (TaskExecuted != null)
		{
			TaskExecuted(taskId);
		}
	}

	void _WorkQueue_TaskExecuteError(Guid taskId, object data, Exception exception)
	{
		Remove(taskId); //任务执行失败后，从工作队列里移除。

		if (TaskExecuteError != null)
		{
			TaskExecuteError(taskId, data, exception);
		}
	}

	#endregion

	#region - Append Task -

	public Guid Append(object data, TaskPriority priority = TaskPriority.General)
	{
		Task task = CreateTask(data, priority);

		//任务表和队列表同时增加任务。
		_taskHashtable.Add(task.Id, task);
		_workQueue.Enqueue(task);

		return task.Id;
	}

	//考虑到仅仅用CreatedOn是无法区别两个Task的先后顺序，因此加了序号。
	private int _taskNumber = 0;

	private Task CreateTask(object data, TaskPriority priority)
	{
		Task task = new Task()
		{
			Id = Guid.NewGuid(),
			Data = data,
			Priority = priority,
			Status = TaskStatus.Unexecuted,
			CreatedOn = DateTime.Now,
			Number = Interlocked.Increment(ref _taskNumber)
		};

		return task;
	}

	#endregion

	#region - Remove Task -

	public void Remove(Guid taskId)
	{
		if (_taskHashtable.ContainsKey(taskId))
		{
			//因为Queue只能顺序读取，无法轮询查找，因此只能把任务状态标记为取消状态，当队列读取到取消状态的任务时，自动丢弃。
			Task task = (Task)_taskHashtable[taskId];
			task.Status = TaskStatus.Cancelled;

			_taskHashtable.Remove(task); //Cacel的任务可以直接从任务表中移除。
		}
	}

	#endregion

	#region - Dispose -

	private bool _disposed = false;

	/// <summary>        
	/// 实现IDisposable中的Dispose方法
	/// </summary>        
	public void Dispose()
	{
		//必须为true            
		Dispose(true);

		//通知垃圾回收机制不再调用终结器（析构器）
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// 必须，以备程序员忘记了显式调用Dispose方法
	/// </summary>        
	~TaskManager()
	{
		//必须为false            
		Dispose(false);
	}

	/// <summary>        
	/// 非密封类修饰用protected virtual
	/// 密封类修饰用private
	/// </summary>        
	/// <param name="disposing"></param>        
	protected virtual void Dispose(bool disposing)
	{
		if (_disposed)
		{
			return;
		}

		if (disposing)
		{
			// 清理托管资源
			_workQueue.Dispose();
			_workQueue = null;
		}

		// 清理非托管资源

		//让类型知道自己已经被释放
		_disposed = true;
	}

	#endregion
}