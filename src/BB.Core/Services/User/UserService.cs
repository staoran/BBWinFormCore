using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Core.Services.BlackIP;
using BB.Core.Services.Function;
using BB.Core.Services.LoginLog;
using BB.Core.Services.OU;
using BB.Core.Services.Role;
using BB.Entity.Base;
using BB.Entity.Security;
using BB.Tools.Encrypt;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using Furion.ClayObject.Extensions;
using Furion.EventBus;
using Furion.UnifyResult;

namespace BB.Core.Services.User;

public class UserService : BaseService<UserInfo>, IDynamicApiController, ITransient
{
    private const string SimpleUserColumnString = "ID,Name,Password,FullName,HandNo,MobilePhone,Email,Dept_ID,Company_ID";
    private readonly IRoleService _roleService;
    private readonly IFunctionService _functionService;
    private readonly IBlackIPService _blackIPService;
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    /// 当前请求上下文
    /// </summary>
    private readonly HttpContext _httpContext;

    public UserService(BaseRepository<UserInfo> repository, IRoleService roleService, IFunctionService functionService,
        IBlackIPService blackIPService, IEventPublisher eventPublisher, IHttpContextAccessor httpContextAccessor) : base(repository)
    {
        _roleService = roleService;
        _functionService = functionService;
        _blackIPService = blackIPService;
        _eventPublisher = eventPublisher;
        _httpContext = httpContextAccessor.HttpContext;
    }

    /// <summary>
    /// 重写删除操作，检查保留管理员用户
    /// </summary>
    /// <param name="key">主键的值</param>
    /// <returns></returns>
    public override async Task<bool> DeleteAsync(object key)
    {
        List<SimpleUserInfo> adminSimpleUsers = await _roleService.GetAdminSimpleUsersAsync();
        if (adminSimpleUsers.Count == 1)
        {
            SimpleUserInfo simpleUser = adminSimpleUsers[0];
            if (Convert.ToInt32(key) == simpleUser.ID)
            {
                throw Oops.Bah("管理员角色至少需要包含一个用户！");
            }
        }

        if (await Repository.AsQueryable().Where(x => x.PID == key.ObjToInt()).AnyAsync())
        {
            throw Oops.Bah("当前用户存在下属用户，暂无法删除。");
        }

        return await base.DeleteAsync(key);
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
    private async Task<List<SimpleUserInfo>> FillSimpleUsersAsync(string sql)
    {
        return await Repository.Db.SqlQueryable<UserInfo>(sql).Select<SimpleUserInfo>().ToListAsync();
    }

    /// <summary>
    /// 根据查询条件获取简单用户对象列表
    /// </summary>
    /// <param name="condition">查询条件</param>
    /// <returns></returns>
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
    /// 通过用户机构ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="ouId">用户机构ID方式</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetSimpleUsersByOuAsync(string ouId)
    {
        string sql = $@"Select {SimpleUserColumnString} From {Repository.TableName} 
            Inner Join T_ACL_OU_User ON {Repository.TableName}.ID=User_ID Where Deleted = 0 AND T_ACL_OU_User.OU_ID = '{ouId}'";
        return await FillSimpleUsersAsync(sql);
    }

    /// <summary>
    /// 通过用户角色ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="roleId">用户角色ID</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetSimpleUsersByRoleAsync(int roleId)
    {
        string sql = string.Format(@"Select {2} From {0} 
            INNER JOIN T_ACL_User_Role ON {0}.ID=User_ID Where Deleted = 0 AND T_ACL_User_Role.Role_ID ={1} ", Repository.TableName, roleId, SimpleUserColumnString);

        return await FillSimpleUsersAsync(sql);
    }

    /// <summary>
    /// 通过机构ID获取对应的用户列表
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> GetUsersByOuAsync(string ouId)
    {
        string sql = $@"SELECT * FROM {Repository.TableName} INNER JOIN T_ACL_OU_User On {Repository.TableName}.ID=T_ACL_OU_User.User_ID 
            WHERE Deleted = 0 AND T_ACL_OU_User.OU_ID ='{ouId}' ";
        return await Repository.GetListAsync(sql);
    }

    /// <summary>
    /// 通过角色ID获取对应的用户列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> GetUsersByRoleAsync(int roleId)
    {
        string sql = string.Format(@"SELECT * FROM {0} INNER JOIN T_ACL_User_Role On {0}.ID=T_ACL_User_Role.User_ID 
            WHERE Deleted = 0 AND T_ACL_User_Role.Role_ID = {1}", Repository.TableName, roleId);
        return await Repository.GetListAsync(sql);
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
    /// <param name="userID">用户ID</param>
    /// <returns></returns>
    public async Task<string> GetFullNameByOpenIdAsync(string userID)
    {
        string result = "";
        if (!string.IsNullOrWhiteSpace(userID))
        {
            string sql = $"Select FullName FROM T_ACL_User WHERE  OpenID ='{userID}'";
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
    /// 获取用户在指定系统类型下的功能集合
    /// </summary>
    /// <param name="typeId"></param>
    /// <returns></returns>
    public async Task<List<FunctionInfo>> GetUserFunctionsAsync(string typeId)
    {
        List<FunctionInfo> functionsByUser = null;
        if (LoginUserInfo != null)
        {
            functionsByUser = await _functionService.GetFunctionsByUserAsync(LoginUserInfo.ID, typeId);
        }
        return functionsByUser;
    }

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

    public override async Task<bool> UpdateAsync(UserInfo obj)
    {
        if (obj.Password.Length < 50)
        {
            obj.Password = EncryptHelper.ComputeHash(obj.Password, obj.Name.ToLower());
        }
        return await base.UpdateAsync(obj);
    }

    /// <summary>
    /// 判断用户是否在指定的角色名称中
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <param name="roleName">角色名称,多个角色用逗号分开</param>
    /// <returns></returns>
    public async Task<bool> UserInRoleAsync(string userName, string roleName)
    {
        bool result = false;
        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(roleName))
        {
            var userInfo = await GetUserByNameAsync(userName);
            if (userInfo != null)
            {
                var roleNames = roleName.ToDelimitedList<string>(",");
                var roles = await _roleService.GetRolesByUserAsync(userInfo.ID);
                foreach (RoleInfo roleInfo in roles)
                {
                    if (roleNames.Contains(roleInfo.Name))
                    {
                        result = true;
                    }
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 判断用户是否在指定的角色名称中
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleName">角色名称</param>
    /// <returns></returns>
    public async Task<bool> UserInRoleByIdAsync(int userId, string roleName)
    {
        UserInfo userInfo = await FindByIdAsync(userId);
        foreach (RoleInfo info in await _roleService.GetRolesByUserAsync(userInfo.ID))
        {
            if (info.Name == roleName)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 判断用户是否为公司管理员
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <returns></returns>
    public async Task<bool> UserIsCompanyAdminAsync(string userName)
    {
        return await UserInRoleAsync(userName, RoleInfo.COMPANY_ADMIN_NAME);
    }

    /// <summary>
    /// 判断用户是否为超级管理员
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <returns></returns>
    public async Task<bool> UserIsSuperAdminAsync(string userName)
    {
        return await UserInRoleAsync(userName, RoleInfo.SUPER_ADMIN_NAME);
    }

    /// <summary>
    /// 判断用户是否为管理员，超级管理员、公司级别的系统管理员均通过。
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <returns></returns>
    public async Task<bool> UserIsAdminAsync(string userName)
    {
        bool result = await UserInRoleAsync(userName, RoleInfo.SUPER_ADMIN_NAME);
        if (!result)
        {
            result = await UserInRoleAsync(userName, RoleInfo.COMPANY_ADMIN_NAME);
        }
        return result;
    }

    /// <summary>
    /// 根据用户名、密码验证用户身份有效性
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <param name="userPassword">用户密码</param>
    /// <param name="systemType">系统类型ID</param>
    /// <returns></returns>
    public async Task<LoginUserInfo> VerifyUserAsync(string userName, string userPassword, string systemType)
    {
        if (string.IsNullOrEmpty(systemType))
        {
            return null;
        }
        LoginUserInfo loginUserInfo = null;
        UserInfo userInfo = await GetUserByNameAsync(userName);
        if (userInfo is { IsExpire: false, Deleted: false })
        {
            //还需要判断是否在有效期内
            DateTime? expireDate = userInfo.ExpireDate;
            if (expireDate.HasValue && expireDate.Value < DateTime.Now)
            {
                //处理非管理员外的失效设置
                if (userInfo.Name != "admin")
                {
                    var ht = new Hashtable 
                    {
                        { UserInfo.PrimaryKey, userInfo.ID },
                        { UserInfo.FieldIsExpire, 1 } //设置失效
                    };
                    await UpdateFieldsAsync(ht);//更新过期设置
                    throw Oops.Bah("用户已经过期");
                }
            }
            else
            {
                loginUserInfo = userInfo.Adapt<LoginUserInfo>();
                string ip = _httpContext.GetRemoteIpAddressToIPv4();
                bool ipAccess = await _blackIPService.ValidateIpAccessAsync(ip, userInfo.ID);
                if (ipAccess)
                {
                    //如果为null，那么密码为空字符串
                    userPassword = EncryptHelper.ComputeHash(userPassword ?? "", userName.ToLower());
                    if (userPassword == userInfo.Password)
                    {
                        // identity = EncryptHelper.EncryptStr(userName + Convert.ToString(Convert.ToChar(1)) + userPassword, systemType);
                        
                        IDictionary<string,object> payload = loginUserInfo.ToDictionary();
                        
                        // 生成 Token
                        var token = JWTEncryption.Encrypt(payload);

                        // 设置 Swagger 的 Token
                        _httpContext.SigninToSwagger(token);
        
                        // 获取 refreshToken, 默认过期时间43200（30天）
                        string refreshToken = JWTEncryption.GenerateRefreshToken(token);
        
                        // 设置响应报文头
                        _httpContext.Response.Headers["access-token"] = token;
                        _httpContext.Response.Headers["x-access-token"] = refreshToken;
        
                        // 设置附加信息
                        UnifyContext.Fill("登陆成功");

                        //记录用户登录日志
                        await _eventPublisher.PublishAsync("Add:LoginLog", new 
                        {
                            Info = loginUserInfo,
                            SystemType = "Security",
                            IP = ip,
                            Note = "用户登录"
                        });
                    }
                }
                else
                {
                    await _eventPublisher.PublishAsync("Add:LoginLog", new 
                    {
                        Info = loginUserInfo,
                        SystemType = "Security",
                        IP = ip,
                        Note = "用户登录操作被黑白名单禁止登录！"
                    });
                    throw Oops.Bah("用户登录操作被黑名单禁止登录！");
                }
            }
        }
        return loginUserInfo;
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
        return Repository.Db.Ado.SqlQuerySingle<byte[]>(sql);
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
        return Repository.Db.Ado.ExecuteCommand(sql, new SugarParameter("@image", imageBytes)) > 0;
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
    /// 更新用户的角色列表
    /// </summary>
    /// <param name="userid">用户ID</param>
    /// <param name="roleList">角色列表</param>
    public async Task UpdateRolesAsync(int userid, List<int> roleList)
    {
        //移除这个用户的所有角色
        //然后添加新的角色列表

        string sql = $"Delete From T_ACL_User_Role Where User_ID='{userid}' ";
        await Repository.SqlExecuteAsync(sql);

        foreach(int roleId in roleList)
        {
            await _roleService.AddUserAsync(userid, roleId);
        }
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
}