using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.Region;

public interface IProvinceHttpService : IHttpDispatchProxy, IBaseHttpService<ProvinceInfo>
{
    /// <summary>
    /// 根据省份ID获取名称
    /// </summary>
    /// <param name="id">省份ID</param>
    /// <returns></returns>
    [Get("nameById")]
    Task<RESTfulResultControl<string>> GetNameByIdAsync([QueryString][Required] int id);

    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">省份名称</param>
    /// <returns></returns>
    [Get("idByName")]
    Task<RESTfulResultControl<string>> GetIdByNameAsync([QueryString][Required] string name);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}province/";
        // req.BaseAddress = builder.Uri;
    }
}