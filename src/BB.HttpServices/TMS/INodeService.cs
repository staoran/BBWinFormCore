﻿using BB.Entity.TMS;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.TMS;

public interface INodeHttpService : IHttpDispatchProxy, IBaseHttpService<Node>
{
}