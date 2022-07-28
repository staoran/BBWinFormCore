using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.Services.Base;
using BB.Entity.Base;
using BB.Entity.Security;
using BB.Tools.Entity;

namespace BB.Core.Services.User;

public interface IUserService : IBaseService<UserInfo>
{
    /// <summary>
    /// 批量设置过期
    /// </summary>
    /// <param name="idList">ID集合</param>
    /// <param name="expired">是否过期</param>
    /// <returns></returns>
    Task<bool> BatchExpireAsync(List<int> idList, bool expired);

    /// <summary>
    /// 设置用户的过期与否
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="expired">是否禁用，true为禁用，否则为启用</param>
    Task SetExpireAsync(int userId, bool expired);

    /// <summary>
    /// 取消用户的过期设置，变为正常状态
    /// </summary>
    /// <param name="userId">用户ID</param>
    Task CancelExpireAsync(int userId);

    /// <summary>
    /// 根据查询条件获取简单用户对象列表
    /// </summary>
    /// <param name="condition">查询条件</param>
    /// <returns></returns>
    Task<List<SimpleUserInfo>> FindSimpleUsersAsync(string condition);

    /// <summary>
    /// 获取所有用户的基本信息
    /// </summary>
    /// <returns></returns>
    Task<List<SimpleUserInfo>> GetSimpleUsersAsync();

    /// <summary>
    /// 获取指定ID字符串的用户基本信息
    /// </summary>
    /// <param name="userIds">ID字符串,逗号分开</param>
    /// <returns></returns>
    Task<List<SimpleUserInfo>> GetSimpleUsersAsync(string userIds);

    /// <summary>
    /// 通过用户机构ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="ouId">用户机构ID方式</param>
    /// <returns></returns>
    Task<List<SimpleUserInfo>> GetSimpleUsersByOuAsync(string ouId);

    /// <summary>
    /// 通过用户角色ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="roleId">用户角色ID</param>
    /// <returns></returns>
    Task<List<SimpleUserInfo>> GetSimpleUsersByRoleAsync(int roleId);

    /// <summary>
    /// 通过机构ID获取对应的用户列表
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <returns></returns>
    Task<List<UserInfo>> GetUsersByOuAsync(string ouId);

    /// <summary>
    /// 通过角色ID获取对应的用户列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    Task<List<UserInfo>> GetUsersByRoleAsync(int roleId);

    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    Task<List<UserInfo>> FindByDeptAsync(string ouId);

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    Task<List<UserInfo>> FindByCompanyAsync(string companyId);

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    Task<List<SimpleUserInfo>> FindSimpleUsersByCompanyAsync(string companyId);

    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    Task<List<SimpleUserInfo>> FindSimpleUsersByDeptAsync(string ouId);

    /// <summary>
    /// 通过用户登录名称获取对应的用户信息
    /// </summary>
    /// <param name="userName">用户登录名称</param>
    /// <returns></returns>
    Task<UserInfo> GetUserByNameAsync(string userName);

    /// <summary>
    /// 根据用户ID获取用户登录名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<string> GetNameByIdAsync(int userId);

    /// <summary>
    /// 根据用户ID获取用户登录名称
    /// </summary>
    /// <param name="userID">用户ID</param>
    /// <returns></returns>
    Task<string> GetFullNameByOpenIdAsync(string userID);

    /// <summary>
    /// 根据用户ID获取用户全名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<string> GetFullNameByIdAsync(int userId);

    /// <summary>
    /// 根据用户登录名称，获取用户全名
    /// </summary>
    /// <param name="userName">用户登录名称</param>
    /// <returns></returns>
    Task<string> GetFullNameByNameAsync(string userName);

    /// <summary>
    /// 获取用户在指定系统类型下的功能集合
    /// </summary>
    /// <param name="typeId"></param>
    /// <returns></returns>
    Task<List<FunctionInfo>> GetUserFunctionsAsync(string typeId);

    /// <summary>
    /// 修改用户密码
    /// </summary>
    /// <param name="userName">修改用户名</param>
    /// <param name="userPassword">用户密码（未加密）</param>
    /// <returns></returns>
    Task<bool> ModifyPasswordAsync(string userName, string userPassword);

    /// <summary>
    /// 管理员重置密码
    /// </summary>
    /// <param name="changeUserId">修改账号ID</param>
    /// <returns></returns>
    Task<bool> ResetPasswordAsync(int changeUserId);

    /// <summary>
    /// 判断用户是否在指定的角色名称中
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <param name="roleName">角色名称,多个角色用逗号分开</param>
    /// <returns></returns>
    Task<bool> UserInRoleAsync(string userName, string roleName);

    /// <summary>
    /// 判断用户是否在指定的角色名称中
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleName">角色名称</param>
    /// <returns></returns>
    Task<bool> UserInRoleByIdAsync(int userId, string roleName);

    /// <summary>
    /// 判断用户是否为公司管理员
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <returns></returns>
    Task<bool> UserIsCompanyAdminAsync(string userName);

    /// <summary>
    /// 判断用户是否为超级管理员
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <returns></returns>
    Task<bool> UserIsSuperAdminAsync(string userName);

    /// <summary>
    /// 判断用户是否为管理员，超级管理员、公司级别的系统管理员均通过。
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <returns></returns>
    Task<bool> UserIsAdminAsync(string userName);

    /// <summary>
    /// 根据用户名、密码验证用户身份有效性
    /// </summary>
    /// <param name="userName">用户名称</param>
    /// <param name="userPassword">用户密码</param>
    /// <param name="systemType">系统类型ID</param>
    /// <returns></returns>
    Task<LoginUserInfo> VerifyUserAsync(string userName, string userPassword, string systemType);

    /// <summary>
    /// 更新用户登录的时间和IP地址
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="ip">IP地址</param>
    /// <param name="macAddr">MAC地址</param>
    /// <returns></returns>
    Task<bool> UpdateUserLoginDataAsync(int id, string ip, string macAddr);

    /// <summary>
    /// 根据个人图片枚举类型获取图片数据
    /// </summary>
    /// <param name="imagetype">图片枚举类型</param>
    /// <returns></returns>
    Task<byte[]> GetPersonImageBytesAsync(UserImageType imagetype, int userId);

    /// <summary>
    /// 更新个人相关图片数据
    /// </summary>
    /// <param name="imagetype">图片类型</param>
    /// <param name="userId">用户ID</param>
    /// <param name="imageBytes">图片字节数组</param>
    /// <returns></returns>
    Task<bool> UpdatePersonImageBytesAsync(UserImageType imagetype, int userId, byte[] imageBytes);

    /// <summary>
    /// 设置删除标志
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="deleted">是否删除</param>
    /// <returns></returns>
    Task<bool> SetDeletedFlagAsync(object id, bool deleted = true);

    /// <summary>
    /// 绑定用户，第一次或重复绑定同一个，提示成功，否则提示失败
    /// </summary>
    /// <param name="openid">用户的OpenID</param>
    /// <param name="unionId"></param>
    /// <param name="id">用户ID</param>
    /// <returns></returns>
    Task BindUserAsync(string openid, string unionId, int id);

    /// <summary>
    /// 根据OpenID获取对应的用户信息
    /// </summary>
    /// <param name="openid">微信OpenID</param>
    /// <returns></returns>
    Task<UserInfo> FindByOpenIdAsync(string openid);

    /// <summary>
    /// 使用唯一的UnionID来获取用户
    /// </summary>
    /// <param name="unionId">开放平台下唯一的UnionID</param>
    /// <returns></returns>
    Task<UserInfo> FindByUnionIdAsync(string unionId);

    /// <summary>
    /// 根据微信企业微信的UserID获取对应的用户信息
    /// </summary>
    /// <param name="openid">微信企业微信的UserID</param>
    /// <returns></returns>
    Task<UserInfo> FindByCorpUserIdAsync(string openid);

    /// <summary>
    /// 更新用户的角色列表
    /// </summary>
    /// <param name="userid">用户ID</param>
    /// <param name="roleList">角色列表</param>
    Task UpdateRolesAsync(int userid, List<int> roleList);

    /// <summary>
    /// 判断用户是否绑定了OpenID
    /// </summary>
    /// <param name="openid">微信OpenID</param>
    /// <returns></returns>
    Task<bool> IsExistOpenIdAsync(string openid);

    /// <summary>
    /// 清空绑定的用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns></returns>
    Task<bool> CancelBindWechatAsync(int id);
}