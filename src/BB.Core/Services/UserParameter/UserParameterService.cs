using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Dictionary;

namespace BB.Core.Services.UserParameter;

/// <summary>
/// 用户参数配置
/// </summary>
public class UserParameterService : BaseService<UserParameterInfo>, IDynamicApiController, ITransient
{
    public UserParameterService(BaseRepository<UserParameterInfo> repository) : base(repository)
    {
    }

    /// <summary>
    /// 保存配置（插入或更新）到数据库
    /// </summary>
    /// <param name="info">信息对象</param>
    /// <returns></returns>
    public async Task<bool> SaveParamaterAsync(UserParameterInfo info)
    {
        if (string.IsNullOrEmpty(info.Name)) throw Oops.Bah("类型名称不能为空");

        //类名称和用户组合一个唯一名称 
        string settingsFilename = info.Name;
        if (!string.IsNullOrEmpty(info.Creator))
        {
            settingsFilename = Path.Combine(info.Creator, info.Name);
        }

        if (await Repository.IsAnyAsync(x => x.Name == settingsFilename))
        {
            return await Repository.UpdateAsync(x => new UserParameterInfo { Content = info.Content },
                x => x.Name == settingsFilename);
        }

        info.Name = settingsFilename; //修改保存的名称
        return await Repository.InsertAsync(info);
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
        string result = null;
        if (!string.IsNullOrEmpty(name))
        {
            //类名称和用户组合一个唯一名称 
            string settingsFilename = name;
            if (!string.IsNullOrEmpty(creator))
            {
                settingsFilename = Path.Combine(creator, name);
            }

            result = await Repository.AsQueryable()
                .Where(x => x.Name == settingsFilename)
                .Select(x => x.Content)
                .FirstAsync();
        }
        return result;
    }
}