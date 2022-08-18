using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 网点资料 业务逻辑类
/// </summary>
public class NodeService : BaseHttpService<Node>
{
    public NodeService(INodeHttpService httpService) : base(httpService)
    {
    }
}