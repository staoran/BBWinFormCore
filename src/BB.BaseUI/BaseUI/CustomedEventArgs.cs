namespace BB.BaseUI.BaseUI;

/// <summary>
/// 泛型的自定义事件参数
/// </summary>
/// <typeparam name="T">指定的泛型数据类型</typeparam>
public class EventArgs<T> : EventArgs
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="t"></param>
    public EventArgs(T t)
    {
        Data = t;
    }

    /// <summary>
    /// 详细的数据
    /// </summary>
    public T Data { get; set; }
}

// 常规事件定义
// public event Action<TEventArgs<CommonResult>> CommonEvent;

/// <summary>
/// 自定义事件参数
/// </summary>
public class MyEventArgs : EventArgs
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 如果不成功，返回的错误信息
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public MyEventArgs() { }

    /// <summary>
    /// 参数化构造函数
    /// </summary>
    /// <param name="success">是否成功</param>
    /// <param name="error">如果不成功，返回的错误信息</param>
    public MyEventArgs(bool success, string error = null)
    {
        Success = success;
        ErrorMessage = error;
    }
}