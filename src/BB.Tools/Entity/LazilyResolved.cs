using Microsoft.Extensions.DependencyInjection;

namespace BB.Tools.Entity;

/// <summary>
/// 懒加载
/// </summary>
/// <typeparam name="T"></typeparam>
public class LazilyResolved<T> : Lazy<T> where T : notnull
{
    //Lazy<T> 是一个非常好的延迟加载的特性，
    //可以在使用到该变量的时候才真正进行参数实例化和一系列控制反转的操作。
    //因为通常我们某一个Service中可能只有一部分的方法需要用到某个变量，但是这个变量又需要在构造函数中依赖注入
    public LazilyResolved(IServiceProvider serviceProvider) : base(serviceProvider.GetRequiredService<T>)
    {

    }
}