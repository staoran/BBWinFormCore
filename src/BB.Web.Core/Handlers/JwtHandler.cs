using System.Threading.Tasks;
using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BB.Web.Core.Handlers;

public class JwtHandler : AppAuthorizeHandler
{
    public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 这里写您的授权判断逻辑，授权通过返回 true，否则返回 false

        return Task.FromResult(true);
    }
}