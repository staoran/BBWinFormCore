using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.UserParameter;

public interface IUserParameterHttpService : IHttpDispatchProxy, IBaseHttpService<UserParameterInfo>
{
    /// <summary>
    /// 保存配置（插入或更新）到数据库
    /// </summary>
    /// <param name="info">信息对象</param>
    /// <returns></returns>
    [Post("saveParamater")]
    Task<RESTfulResultControl<bool>> SaveParamaterAsync([Body]UserParameterInfo info);

    /// <summary>
    /// 根据类名称和用户标识获取参数配置内容
    /// </summary>
    /// <param name="name">类名称</param>
    /// <param name="creator">用户标识</param>
    /// <returns></returns>
    [Get("loadParameter")]
    Task<RESTfulResultControl<string>> LoadParameterAsync([QueryString][Required] string name, [QueryString]string creator = "");

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}userParameter/";
        // req.BaseAddress = builder.Uri;
    }
}