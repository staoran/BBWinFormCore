using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 消息记录 业务逻辑类
/// </summary>
public class MessageService : BaseHttpService<Message>
{
    public MessageService(IMessageHttpService httpService) : base(httpService)
    {
    }
}