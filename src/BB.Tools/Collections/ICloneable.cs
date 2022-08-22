namespace BB.Tools.Collections;

/// <summary>
/// 定义一个泛型克隆接口，值类型或类可以实现克隆操作。
/// </summary>
/// <typeparam name="T">值类型或类</typeparam>
public interface ICloneable<T>
{
    /// <summary>
    /// 创建一个当前实例的新对象
    /// </summary>
    T Clone();
}