using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BB.Core.Filter;

/// <summary>
/// 全局异常处理类
/// </summary>
public class GlobalExceptionHandler : IGlobalExceptionHandler, ISingleton
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        // 写日志
        context.Exception.ToString().LogError();

        return Task.CompletedTask;
    }
}