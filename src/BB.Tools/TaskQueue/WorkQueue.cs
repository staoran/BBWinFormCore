using System.Collections;

namespace BB.Tools.TaskQueue;

public delegate void TaskExecutedHandler(Guid taskId);

public delegate void TaskExecuteErrorHandler(Guid taskId, object data, Exception exception);

/// <summary>
/// 任务的工作队列，负责任务的执行。
/// </summary>
internal class WorkQueue : IDisposable
{
	private readonly Guid _workId = Guid.NewGuid();
	private string _workName = "";

	private readonly Queue _queue = Queue.Synchronized(new Queue()); //线程安全的任务队列。
	private Action<object> _command; //任务的执行委托。
	private Thread _workThread = null; //任务执行线程。

	private bool _exit = false; //退出线程的标记。

	private AutoResetEvent _workEvent = new AutoResetEvent(false);

	/// <summary>
	/// 任务的工作队列。
	/// </summary>
	/// <param name="command">执行任务的委托。</param>
	/// <param name="ignoreExceptionList">忽略执行任务时发生的指定异常。</param>
	public WorkQueue(Action<object> command, string workName)
	{
		_workName = workName;
		_command = command ?? throw new ArgumentNullException("command");

		//构造任务的工作线程。
		_workThread = new Thread(delegate()
		{
			_exit = false;
			Work();
		});
		_workThread.IsBackground = true;
		_workThread.Start();
		//Console.WriteLine("Create Thead：{0}，{1}", _WorkName, _WorkId);
	}

	internal event TaskExecutedHandler TaskExecuted; //任务已执行事件。
	private void RaiseTaskExecuted(Task task)
	{
		if (task.Status == TaskStatus.Executed && TaskExecuted != null)
		{
			TaskExecuted(task.Id);
		}
	}

	internal event TaskExecuteErrorHandler TaskExecuteError; //任务执行时发生错误的事件。
	private void RaiseTaskExecuteError(Task task, Exception ex)
	{
		if (TaskExecuteError == null)
		{
			return;
		}

		try
		{
			TaskExecuteError(task.Id, task.Data, ex);
		}
		catch (Exception ex1) //如果外部处理异常时发生了错误，那么忽略，这么做了为了保证任务队列不被外部的错误打断。
		{
			//throw;
			Console.WriteLine(ex1);
		}
	}

	private void Work()
	{
		while (!_exit)
		{
			Task task = Dequeue();
			if (task == null) //如果没有任务，那么工作队列暂停，一直等到有任务进来才启动。
			{
				_workEvent.WaitOne();
				continue;
			}

			ExecuteTask(task);
		}
	}

	private Task Dequeue()
	{
		Task task = null;
		if (_queue.Count > 0)
		{
			task = (Task)_queue.Dequeue();
		}
		return task;
	}

	private bool ExecuteTask(Task task)
	{
		if (task.Status == TaskStatus.Cancelled) //如果任务已经被取消，那么不执行此任务。
		{
			return true;
		}

		bool success = false;

		while (!success)
		{
			try
			{
				_command(task.Data);
				success = true;
			}
			catch (Exception ex)
			{
				RaiseTaskExecuteError(task, ex);
				success = false;
				break; //如果发生了不可预料的异常，那么当前任务中止执行。
			}
		}

		task.Status = success ? TaskStatus.Executed : TaskStatus.Error;
		RaiseTaskExecuted(task);
		return success;
	}

	public void Enqueue(Task task)
	{
		_workEvent.Set();
		_queue.Enqueue(task);
	}

	public void Dispose()
	{
		_exit = true;
		_workEvent.Set();
		if (_workThread != null)
		{
			_workThread.Abort();
			_workThread = null;
			//Console.WriteLine("Dispose Thead：{0}，{1}", _WorkName, _WorkId);
		}
	}

}