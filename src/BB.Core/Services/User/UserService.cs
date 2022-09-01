using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Filter;
using BB.Core.Services.Base;
using BB.Entity.Security;
using BB.Tools.Encrypt;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using BB.Tools.Utils;
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
    /// 构造一个简单用户信息类集合
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    [NonAction]
    private async Task<List<SimpleUserInfo>> FillSimpleUsersAsync(string sql)
    {
        return await Repository.Db.SqlQueryable<UserInfo>(sql).Select<SimpleUserInfo>().ToListAsync();
    }

    /// <summary>
    /// 根据查询条件获取简单用户对象列表
    /// </summary>
    /// <param name="condition">查询条件</param>
    /// <returns></returns>
    [NonAction]
    public async Task<List<SimpleUserInfo>> FindSimpleUsersAsync(string condition)
    {
        //串连条件语句为一个完整的Sql语句
        string sql = $"Select {SimpleUserColumnString} From {Repository.TableName} Where Deleted = 0 ";
        if (!string.IsNullOrEmpty(condition))
        {
            sql += $" AND {condition} ";
        }
        string orderBy = Repository.SortField.IsNullOrEmpty() ? string.Empty : $"{Repository.SortField} {(Repository.IsDescending ? "desc" : "asc")}";
        if (!string.IsNullOrEmpty(condition))
        {
            sql += $" Order by {orderBy}";
        }

        return await FillSimpleUsersAsync(sql);
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
        string condition = $" ID In ({userIds})";
        return await FindSimpleUsersAsync(condition);
    }
               
    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> FindByDeptAsync(string ouId)
    {
        string condition = $"Dept_ID='{ouId}' ";
        return await FindAsync(condition);
    }

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> FindByCompanyAsync(string companyId)
    {
        string condition = $"Company_ID='{companyId}' ";
        return await FindAsync(condition);
    }

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> FindSimpleUsersByCompanyAsync(string companyId)
    {
        string condition = $"Company_ID='{companyId}' ";
        return await FindSimpleUsersAsync(condition);
    }

    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> FindSimpleUsersByDeptAsync(string ouId)
    {
        string condition = $"Dept_ID='{ouId}' ";
        return await FindSimpleUsersAsync(condition);
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
            string condition = $"Name ='{userName}' ";
            info = await FindSingleAsync(condition);
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
        string result = "";
        if (!string.IsNullOrWhiteSpace(userId))
        {
            string sql = $"Select FullName FROM T_ACL_User WHERE  OpenID ='{userId}'";
            result = await SqlValueListAsync(sql);
        }
        return result;
    }

    /// <summary>
    /// 根据用户ID获取用户全名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<string> GetFullNameByIdAsync(int userId)
    {
        string sql = $"Select FullName from {Repository.TableName} Where ID={userId}";
        return await SqlValueListAsync(sql);
    }

    /// <summary>
    /// 根据用户登录名称，获取用户全名
    /// </summary>
    /// <param name="userName">用户登录名称</param>
    /// <returns></returns>
    public async Task<string> GetFullNameByNameAsync(string userName)
    {
        string sql = $"Select FullName from {Repository.TableName} Where Name='{userName}' ";
        return await SqlValueListAsync(sql);
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
        return await Repository.UseTransactionAsync(async () =>
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
    /// <returns></returns>
    public async Task<byte[]> GetPersonImageBytesAsync(UserImageType imagetype, int userId)
    {
        string fieldName = GetFieldNameByImageType(imagetype);

        string sql = $"Select {fieldName} from {Repository.TableName} where Id = {userId} ";
        return await Repository.Db.Ado.SqlQuerySingleAsync<byte[]>(sql);
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

        string sql = $"update {Repository.TableName} set {fieldName}=@image where Id = {userId} ";
        return await Repository.Db.Ado.ExecuteCommandAsync(sql, new SugarParameter("@image", imageBytes)) > 0;
    }

    /// <summary>
    /// 设置删除标志
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="deleted">是否删除</param>
    /// <returns></returns>
    public async Task<bool> SetDeletedFlagAsync(object id, bool deleted = true)
    {
        int intDeleted = deleted ? 1 : 0;
        string sql = $"Update {Repository.TableName} Set Deleted={intDeleted} Where ID = {id} ";
        return await Repository.SqlExecuteAsync(sql) > 0;
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
        string condition = $"OpenId='{openid}' AND ID <> {id} ";
        bool isDuplicatedErr = await IsExistRecordAsync(condition);
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
            string condition = $"OpenId='{openid}' ";
            result = await FindSingleAsync(condition);
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
            string condition = $"UnionId='{unionId}' ";
            result = await FindSingleAsync(condition);
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
            string condition = $"CorpUserId='{openid}' ";
            result = await FindSingleAsync(condition);
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
            string condition = $"OpenId='{openid}' ";
            result = await IsExistRecordAsync(condition);
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
                new(UserInfo.FieldHandNo, SqlOperator.Like),
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