﻿using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpServices.Base;
using BB.Tools.Entity;
using Furion.RemoteRequest;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.Dict;

public interface IDictTypeHttpService : IHttpDispatchProxy, IBaseHttpService<DictTypeInfo>
{
    /// <summary>
    /// 获取所有字典类型的列表集合(Key为名称，Value为ID值）
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns></returns>
    [Get("allType")]
    Task<RESTfulResult<Dictionary<string, string>>> GetAllTypeAsync(string dictTypeId = "");

    /// <summary>
    /// 判断是否重复，如果重复返回True，否则为False
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="id">编号</param>
    /// <returns></returns>
    [Get("checkDuplicated")]
    Task<RESTfulResult<bool>> CheckDuplicatedAsync(string name, string id);

    /// <summary>
    /// 获取字典类型的树形结构列表
    /// </summary>
    /// <returns></returns>
    [Get("tree")]
    Task<RESTfulResult<List<DictTypeNodeInfo>>> GetTreeAsync();

    /// <summary>
    /// 获取所有字典类型的终结点数据
    /// </summary>
    /// <returns></returns>
    [Get("endPointItems")]
    Task<RESTfulResult<List<CListItem>>> GetEndpointItemsAsync(List<DictTypeNodeInfo> nodeInfos = null, List<CListItem> items = null);

    /// <summary>
    /// 获取字典类型顶级的列表
    /// </summary>
    /// <returns></returns>
    [Get("topItems")]
    Task<RESTfulResult<List<DictTypeInfo>>> GetTopItemsAsync();

    /// <summary>
    /// 获取指定ID下的树形结构列表
    /// </summary>
    /// <param name="mainId">字典类型ID</param>
    /// <returns></returns>
    [Get("treeById")]
    Task<RESTfulResult<List<DictTypeNodeInfo>>> GetTreeByIdAsync([Required] string mainId);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        var builder = new UriBuilder(req.BaseAddress!);
        var path = req.BaseAddress!.AbsolutePath;
        builder.Path = $"{path}dictType/";
        req.BaseAddress = builder.Uri;
    }
}