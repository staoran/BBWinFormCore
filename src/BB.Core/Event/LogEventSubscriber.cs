using System;
using System.Threading.Tasks;
using BB.Core.Services.LoginLog;
using Furion.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace BB.Core.Event;

/// <summary>
/// 日志事件订阅
/// </summary>
public class LogEventSubscriber : IEventSubscriber
{
    private readonly IServiceProvider _serviceProvider;

    public LogEventSubscriber(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 新增登陆日志
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    [EventSubscribe("Add:LoginLog")]
    public async Task AddLoginLog(EventHandlerExecutingContext context)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        var rep = scope.ServiceProvider.GetRequiredService<LoginLogService>();
        dynamic payload = context.Source.Payload;
        await rep.AddLoginLogAsync(payload.Info, payload.SystemType, payload.IP, "", payload.Note);
    }
}