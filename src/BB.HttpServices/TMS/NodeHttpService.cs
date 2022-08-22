using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 网点资料 业务逻辑类
/// </summary>
public class NodeHttpService : BaseHttpService<Node>
{
    public NodeHttpService(INodeHttpService httpService) : base(httpService)
    {
    }
}