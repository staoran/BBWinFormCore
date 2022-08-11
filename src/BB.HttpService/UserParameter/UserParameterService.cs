using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpService.Base;
using Microsoft.AspNetCore.Mvc;

namespace BB.HttpService.UserParameter;

/// <summary>
/// 用户参数配置
/// </summary>
public class UserParameterHttpService : BaseHttpService<UserParameterInfo>
{
    private readonly IUserParameterHttpService _httpService;

    public UserParameterHttpService(IUserParameterHttpService httpService) : base(httpService)
    {
        _httpService = httpService;
    }

    /// <summary>
    /// 保存配置（插入或更新）到数据库
    /// </summary>
    /// <param name="info">信息对象</param>
    /// <returns></returns>
    public async Task<bool> SaveParamaterAsync(UserParameterInfo info)
    {
        return (await _httpService.SaveParamaterAsync(info)).Data;
    }

    /// <summary>
    /// 根据类名称和用户标识获取参数配置内容
    /// </summary>
    /// <param name="name">类名称</param>
    /// <param name="creator">用户标识</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<string> LoadParameterAsync([Required] string name, string creator = null)
    {
        return (await _httpService.LoadParameterAsync(name, creator)).Data;
    }
}