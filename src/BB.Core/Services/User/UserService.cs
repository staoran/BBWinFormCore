using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Filter;
using BB.Core.Services.Base;
using BB.Entity.Security;
using BB.Tools.Cache;
using BB.Tools.Encrypt;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using FluentValidation;
using Furion.EventBus;

namespace BB.Core.Services.User;

[ApiDescriptionSettings("用户与机构")]
public class UserService : BaseService<UserInfo>, IDynamicApiController, ITransient
{
    private const string SimpleUserColumnString = "ID,Name,Password,FullName,HandNo,MobilePhone,Email,Dept_ID,Company_ID";
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    /// 当前请求上下文
    /// </summary>
    private readonly HttpContext _httpContext;

    public UserService(BaseRepository<UserInfo> repository, IValidator<UserInfo> validator, IEventPublisher eventPublisher, 
        IHttpContextAccessor httpContextAccessor) : base(repository, validator)
    {
        _eventPublisher = eventPublisher;
        _httpContext = httpContextAccessor.HttpContext;
    }

    /// <summary>
    /// 重写删除操作，检查保留管理员用户
    /// </summary>
    /// <param name="key">主键的值</param>
    /// <returns></returns>
    public override async Task<bool> DeleteAsync([ModelBinder(typeof(ObjectModelBinder))][Required]object key)
    {
        if (await Repository.AsQueryable().Where(x => x.PID == key.ObjToInt()).AnyAsync())
        {
            throw Oops.Bah("当前用户存在下属用户，暂无法删除。");
        }

        return await UseTransactionAsync(async () =>
        {
            // 删除角色
            
            await base.DeleteAsync(key);
        });
    }

    /// <summary>
    /// 批量设置过期
    /// </summary>
    /// <param name="idList">ID集合</param>
    /// <param name="expired">是否过期</param>
    /// <returns></returns>
    public async Task<bool> BatchExpireAsync(List<int> idList, bool expired)
    {
        bool result = await Repository.AsUpdateable()
            .SetColumns(u => u.IsExpire == expired)
            .Where(x => idList.Contains(x.ID))
            .ExecuteCommandHasChangeAsync();

        if (result)
        {
            //记录用户修改密码日志
            string message = string.Format("{0}{2}了用户【{1}】的账号", LoginUserInfo.FullName, idList.Splice(),
                expired ? "禁用" : "启用");

            await _eventPublisher.PublishAsync("Add:LoginLog", new 
            {
                Info = LoginUserInfo,
                SystemType = "Security",
                IP = _httpContext.GetRemoteIpAddressToIPv4(),
                Note = message
            });
        }

        return result;
    }

    /// <summary>
    /// 设置用户的过期与否
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="expired">是否禁用，true为禁用，否则为启用</param>
    public async Task SetExpireAsync(int userId, bool expired)
    {
        await BatchExpireAsync(new List<int>(userId), expired);
    }

    /// <summary>
    /// 取消用户的过期设置，变为正常状态
    /// </summary>
    /// <param name="userId">用户ID</param>
    public async Task CancelExpireAsync(int userId)
    {
        UserInfo info = await FindByIdAsync(userId);
        if (info.IsExpire)
        {
            info.IsExpire = false;
            await UpdateAsync(info);
        }
    }

    /// <summary>
    /// 根据查询条件获取简单用户对象列表
    /// </summary>
    /// <param name="expression">查询条件</param>
    /// <returns></returns>
    [NonAction]
    public async Task<List<SimpleUserInfo>> FindSimpleUsersAsync(Expression<Func<UserInfo,bool>> expression)
    {
        return await Repository.AsQueryable()
            .WhereIF(expression != null, expression)
            .Where(x => !x.Deleted)
            .OrderByIF(Repository.SortField.IsNullOrEmpty(), $"{Repository.SortField} {(Repository.IsDescending ? "desc" : "asc")}")
            .Select<SimpleUserInfo>(SimpleUserColumnString)
            .ToListAsync();
    }

    /// <summary>
    /// 获取所有用户的基本信息
    /// </summary>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "allSimpleUsers")]
    public async Task<List<SimpleUserInfo>> GetSimpleUsersAsync()
    {
        return await FindSimpleUsersAsync(null);
    }

    /// <summary>
    /// 获取指定ID字符串的用户基本信息
    /// </summary>
    /// <param name="userIds">ID字符串,逗号分开</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetSimpleUsersAsync(string userIds)
    {
        var ids = userIds.Split(",");
        return await FindSimpleUsersAsync(x => ids.Contains(x.ID.ToString()));
    }
               
    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> FindByDeptAsync(string ouId)
    {
        return await FindAsync(x => x.DeptId == ouId);
    }

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> FindByCompanyAsync(string companyId)
    {
        return await FindAsync(x => x.CompanyId == companyId);
    }

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> FindSimpleUsersByCompanyAsync(string companyId)
    {
        return await FindSimpleUsersAsync(x => x.CompanyId == companyId);
    }

    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> FindSimpleUsersByDeptAsync(string ouId)
    {
        return await FindSimpleUsersAsync(x => x.DeptId == ouId);
    }

    /// <summary>
    /// 通过用户登录名称获取对应的用户信息
    /// </summary>
    /// <param name="userName">用户登录名称</param>
    /// <returns></returns>
    public async Task<UserInfo> GetUserByNameAsync(string userName)
    {
        UserInfo info = null;
        if (!string.IsNullOrEmpty(userName))
        {
            info = await FindSingleAsync(x => x.Name == userName);
        }
        return info;
    }

    /// <summary>
    /// 根据用户ID获取用户登录名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<string> GetNameByIdAsync(int userId)
    {
        return await GetFieldValueAsync(userId, "Name");
    }


    /// <summary>
    /// 根据用户ID获取用户登录名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<string> GetFullNameByOpenIdAsync(string userId)
    {
        if (!string.IsNullOrWhiteSpace(userId))
        {
            return await Repository.AsQueryable()
                .Where(x => x.OpenId == userId)
                .Select(x => x.FullName)
                .FirstAsync();
        }
        return string.Empty;
    }

    /// <summary>
    /// 根据用户ID获取用户全名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<string> GetFullNameByIdAsync(int userId)
    {
        return await Repository.AsQueryable()
            .Where(x => x.ID == userId)
            .Select(x => x.FullName)
            .FirstAsync();
    }

    /// <summary>
    /// 根据用户登录名称，获取用户全名
    /// </summary>
    /// <param name="userName">用户登录名称</param>
    /// <returns></returns>
    public async Task<string> GetFullNameByNameAsync(string userName)
    {
        return await Repository.AsQueryable()
            .Where(x => x.Name == userName)
            .Select(x => x.FullName)
            .FirstAsync();
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override async Task<bool> InsertAsync(UserInfo obj)
    {
        obj.Password = EncryptHelper.ComputeHash(UserInfo.DEFAULT_PASSWORD, obj.Name.ToLower());
        return await base.InsertAsync(obj);
    }

    /// <summary>
    /// 修改用户密码
    /// </summary>
    /// <param name="userName">修改用户名</param>
    /// <param name="userPassword">用户密码（未加密）</param>
    /// <returns></returns>
    public async Task<bool> ModifyPasswordAsync(string userName, string userPassword)
    {
        bool result = false;
        UserInfo userByName = await GetUserByNameAsync(userName);
        if (userByName != null)
        {
            userPassword = EncryptHelper.ComputeHash(userPassword, userName.ToLower());
            userByName.Password = userPassword;

            result = await UpdateAsync(userByName);
            if (result)
            {
                //记录用户修改密码日志
                await _eventPublisher.PublishAsync("Add:LoginLog", new 
                {
                    Info = LoginUserInfo,
                    SystemType = "Security",
                    IP = _httpContext.GetRemoteIpAddressToIPv4(),
                    Note = "用户修改密码"
                });
            }
        }
        return result;
    }

    /// <summary>
    /// 管理员重置密码
    /// </summary>
    /// <param name="changeUserId">修改账号ID</param>
    /// <returns></returns>
    public async Task<bool> ResetPasswordAsync(int changeUserId)
    {
        bool result = false;
        UserInfo loginInfo = await FindByIdAsync(LoginUserInfo.ID);
        UserInfo changeInfo = await FindByIdAsync(changeUserId);
        if (loginInfo != null && changeInfo != null)
        {
            string initPassword = EncryptHelper.ComputeHash(UserInfo.DEFAULT_PASSWORD, changeInfo.Name.ToLower());
            changeInfo.Password = initPassword;
            result = await UpdateAsync(changeInfo);

            if (result)
            {
                //记录用户修改密码日志
                string message = $"{loginInfo.FullName}重置了用户【{changeInfo.FullName}】的登录密码";
                await _eventPublisher.PublishAsync("Add:LoginLog", new 
                {
                    Info = LoginUserInfo,
                    SystemType = "Security",
                    IP = _httpContext.GetRemoteIpAddressToIPv4(),
                    Note = message
                });
            }
        }
        return result;
    }

    /// <summary>
    /// 更新用户资料
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override async Task<bool> UpdateAsync(UserInfo obj)
    {
        if (obj.Password.Length < 50)
        {
            obj.Password = EncryptHelper.ComputeHash(obj.Password, obj.Name.ToLower());
        }
        return await base.UpdateAsync(obj);
    }

    /// <summary>
    /// 更新用户登录的时间和IP地址
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="ip">IP地址</param>
    /// <param name="macAddr">MAC地址</param>
    /// <returns></returns>
    public async Task<bool> UpdateUserLoginDataAsync(int id, string ip, string macAddr)
    {
        return await UseTransactionAsync(async () =>
        {
            await Repository.AsUpdateable()
                .SetColumns(x => x.LastLoginIP == x.CurrentLoginIP)
                .SetColumns(x => x.LastLoginTime == x.CurrentLoginTime)
                .SetColumns(x => x.LastMacAddress == x.CurrentMacAddress)
                .Where(x => x.ID == id)
                .ExecuteCommandAsync();

            await Repository.AsUpdateable()
                .SetColumns(x => x.CurrentLoginIP == ip)
                .SetColumns(x => x.CurrentLoginTime == DateTime.Now)
                .SetColumns(x => x.CurrentMacAddress == macAddr)
                .Where(x => x.ID == id)
                .ExecuteCommandAsync();
        });
    }

    /// <summary>
    /// 根据个人图片枚举类型获取图片数据
    /// </summary>
    /// <param name="imagetype">图片枚举类型</param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<byte[]> GetPersonImageBytesAsync(UserImageType imagetype, int userId)
    {
        string fieldName = GetFieldNameByImageType(imagetype);

        return await Repository.AsQueryable()
            .Where(x => x.ID == userId)
            .Select<byte[]>(fieldName)
            .FirstAsync();
    }

    /// <summary>
    /// 根据图片枚举类型获取对应的字段名称
    /// </summary>
    /// <param name="imageType">图片枚举类型</param>
    /// <returns></returns>
    private string GetFieldNameByImageType(UserImageType imageType)
    {
        string fieldName = "Portrait";
        switch (imageType)
        {
            case UserImageType.个人肖像:
                fieldName = "Portrait";
                break;
            case UserImageType.身份证照片1:
                fieldName = "IDPhoto1";
                break;
            case UserImageType.身份证照片2:
                fieldName = "IDPhoto2";
                break;
            case UserImageType.名片1:
                fieldName = "BusinessCard1";
                break;
            case UserImageType.名片2:
                fieldName = "BusinessCard2";
                break;
        }
        return fieldName;
    }

    /// <summary>
    /// 更新个人相关图片数据
    /// </summary>
    /// <param name="imagetype">图片类型</param>
    /// <param name="userId">用户ID</param>
    /// <param name="imageBytes">图片字节数组</param>
    /// <returns></returns>
    public async Task<bool> UpdatePersonImageBytesAsync(UserImageType imagetype, int userId, byte[] imageBytes)
    {
        string fieldName = GetFieldNameByImageType(imagetype);
        return await UpdateFieldsAsync(new Hashtable()
            { { fieldName, imageBytes }, { UserInfo.PrimaryKey, userId } });
    }

    /// <summary>
    /// 设置删除标志
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="deleted">是否删除</param>
    /// <returns></returns>
    [QueryParameters]
    public async Task<bool> SetDeletedFlagAsync(object id, bool deleted = true)
    {
        return await UpdateFieldsAsync(new Hashtable()
            { { UserInfo.FieldDeleted, deleted ? 1 : 0 }, { UserInfo.PrimaryKey, id } });
    }

    /// <summary>
    /// 绑定用户，第一次或重复绑定同一个，提示成功，否则提示失败
    /// </summary>
    /// <param name="openid">用户的OpenID</param>
    /// <param name="unionId"></param>
    /// <param name="id">用户ID</param>
    /// <returns></returns>
    public async Task BindUserAsync(string openid, string unionId, int id)
    {
        bool isDuplicatedErr = await IsExistRecordAsync(x => x.OpenId == openid && x.ID != id);
        if (!isDuplicatedErr)
        {
            UserInfo info = await FindByIdAsync(id);
            if (info != null)
            {
                if (string.IsNullOrEmpty(info.UnionId))
                {
                    try
                    {
                        var ht = new Hashtable
                        {
                            { UserInfo.PrimaryKey, id },
                            { UserInfo.FieldOpenId, openid },
                            { UserInfo.FieldUnionId, unionId }
                        };

                        await UpdateAsync(ht);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.LogError();
                        throw Oops.Bah(ex.Message);
                    }
                }
                else if (info.UnionId == unionId && info.ID == id)
                {
                    //重复同一个绑定
                }
            }
        }
        else
        {
            throw Oops.Bah("已经绑定其他用户，不能重复绑定");
        }
    }

    /// <summary>
    /// 根据OpenID获取对应的用户信息
    /// </summary>
    /// <param name="openid">微信OpenID</param>
    /// <returns></returns>
    public async Task<UserInfo> FindByOpenIdAsync(string openid)
    {
        UserInfo result = null;
        if (!string.IsNullOrEmpty(openid))
        {
            result = await FindSingleAsync(x => x.OpenId == openid);
        }
        return result;
    }

    /// <summary>
    /// 使用唯一的UnionID来获取用户
    /// </summary>
    /// <param name="unionId">开放平台下唯一的UnionID</param>
    /// <returns></returns>
    public async Task<UserInfo> FindByUnionIdAsync(string unionId)
    {
        UserInfo result = null;
        if (!string.IsNullOrEmpty(unionId))
        {
            result = await FindSingleAsync(x => x.UnionId == unionId);
        }
        return result;
    }

    /// <summary>
    /// 根据微信企业微信的UserID获取对应的用户信息
    /// </summary>
    /// <param name="openid">微信企业微信的UserID</param>
    /// <returns></returns>
    public async Task<UserInfo> FindByCorpUserIdAsync(string openid)
    {
        UserInfo result = null;
        if (!string.IsNullOrEmpty(openid))
        {
            result = await FindSingleAsync(x => x.CorpUserId == openid);
        }
        return result;
    }

    /// <summary>
    /// 判断用户是否绑定了OpenID
    /// </summary>
    /// <param name="openid">微信OpenID</param>
    /// <returns></returns>
    public async Task<bool> IsExistOpenIdAsync(string openid)
    {
        bool result = false;
        if (!string.IsNullOrEmpty(openid))
        {
            result = await IsExistRecordAsync(x => x.OpenId == openid);
        }
        return result;
    }

    /// <summary>
    /// 清空绑定的用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns></returns>
    public async Task<bool> CancelBindWechatAsync(int id)
    {
        Hashtable ht = new Hashtable
        {
            { UserInfo.PrimaryKey, id },
            { UserInfo.FieldOpenId, "" },
            { UserInfo.FieldUnionId, "" },
            { UserInfo.FieldStatus, "未关联" }
        };

        return await UpdateAsync(ht);
    }

    /// <summary>
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.Instance.GetOrCreate($"{nameof(UserInfo)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(UserInfo.FieldHandNo, SqlOperator.Equal),
                new(UserInfo.FieldDeptId, SqlOperator.Equal),
                new(UserInfo.FieldCompanyId, SqlOperator.Equal),
                new(UserInfo.FieldDeleted, SqlOperator.Equal),
                new(UserInfo.FieldName, SqlOperator.Like ),
                new(UserInfo.FieldFullName, SqlOperator.Like ),
                new(UserInfo.FieldNickname, SqlOperator.Like ),
                new(UserInfo.FieldMobilePhone, SqlOperator.Like ),
                new(UserInfo.FieldEmail, SqlOperator.Like ),
                new(UserInfo.FieldGender, SqlOperator.Like ),
                new(UserInfo.FieldQQ, SqlOperator.Like )
            });
    }
}