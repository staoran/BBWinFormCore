using System.Security;
using System.Security.Permissions;

namespace BB.BaseUI.LocalReport;

/// <summary>
/// 处理进程
/// </summary>
internal sealed class ProcessingThread
{
    private Thread _mBackgroundThread;
    private object _mOperation;
    private Action _beginAsyncExecutionDelegate;
    private Action<Exception> _endEndAsyncExecutionDelegate;

    public void BeginBackgroundOperation(object operation)
    {
        if (_mBackgroundThread != null)
        {
            _mBackgroundThread.Join();
        }
        _mOperation = operation;
        _mBackgroundThread = new Thread(ProcessThreadMain);

        Type t = operation.GetType();
        _beginAsyncExecutionDelegate = (Action)Delegate.CreateDelegate(typeof(Action), operation, "BeginAsyncExecution");
        _endEndAsyncExecutionDelegate = (Action<Exception>)Delegate.CreateDelegate(typeof(Action<Exception>), operation, "EndAsyncExecution");

        try
        {
            PropagateThreadCulture();
        }
        catch (SecurityException)
        {
        }
        _mBackgroundThread.Name = "Rendering";
        _mBackgroundThread.IsBackground = true;
        _mBackgroundThread.Start(operation);
    }

    public bool Cancel(int millisecondsTimeout)
    {
        if (!IsRendering)
        {
            return true;
        }
        try
        {
            object operation = _mOperation;
            if (operation != null)
            {
                _mBackgroundThread.Abort();
            }
        }
        catch (ThreadStateException)
        {
            if (IsRendering)
            {
                throw;
            }
        }
        return ((millisecondsTimeout != 0) && _mBackgroundThread.Join(millisecondsTimeout));
    }

    private void ProcessThreadMain(object arg)
    {
        Exception e = null;
        try
        {
            _beginAsyncExecutionDelegate();
        }
        catch (Exception exception2)
        {
            e = exception2;
            for (Exception exception3 = exception2; exception3 != null; exception3 = exception3.InnerException)
            {
                if (exception3 is ThreadAbortException)
                {
                    e = new OperationCanceledException();
                    return;
                }
            }
        }
        finally
        {
            _endEndAsyncExecutionDelegate(e);
            _mOperation = null;
        }
    }

    [SecurityCritical, SecurityTreatAsSafe, SecurityPermission(SecurityAction.Assert, ControlThread = true)]
    private void PropagateThreadCulture()
    {
        _mBackgroundThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
        _mBackgroundThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
    }

    // Properties
    private bool IsRendering => ((_mBackgroundThread != null) && _mBackgroundThread.IsAlive);
}