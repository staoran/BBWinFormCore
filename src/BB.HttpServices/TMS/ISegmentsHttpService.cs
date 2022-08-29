﻿using BB.Entity.TMS;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.TMS;

public interface ISegmentsHttpService : IHttpDispatchProxy, IBaseHttpService<Segments>
{
}