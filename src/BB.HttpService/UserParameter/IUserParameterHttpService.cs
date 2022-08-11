using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpService.Base;
using Furion.UnifyResult;

namespace BB.HttpService.UserParameter;

public interface IUserParameterHttpService : IBaseHttpService<UserParameterInfo>
{
    /// <summary>
    /// 保存配置（插入或更新）到数据库
    /// </summary>
    /// <param name="info">信息对象</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> SaveParamaterAsync(UserParameterInfo info);

    /// <summary>
    /// 根据类名称和用户标识获取参数配置内容
    /// </summary>
    /// <param name="name">类名称</param>
    /// <param name="creator">用户标识</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> LoadParameterAsync([Required] string name, string creator = null);
}