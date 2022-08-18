using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 消息回复 业务逻辑类
/// </summary>
public class MessagesHttpService : BaseHttpService<Messages>
{
    public MessagesHttpService(IMessagesHttpService httpService) : base(httpService)
    {
    }
}