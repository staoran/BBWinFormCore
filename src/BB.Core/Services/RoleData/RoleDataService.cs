using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Core.Services.User;
using BB.Entity.Security;
using BB.Tools.Extension;

namespace BB.Core.Services.RoleData;

public class RoleDataService : BaseService<RoleDataInfo>, IDynamicApiController, ITransient
{
    private readonly UserRoleService _userRoleService;
    private readonly UserService _userService;

    public RoleDataService(BaseRepository<RoleDataInfo> repository, UserRoleService userRoleService, UserService userService) : base(repository)
    {
        _userRoleService = userRoleService;
        _userService = userService;
    }

    /// <summary>
    /// 获取用户所属角色对应的管理公司列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<List<string>> GetBelongCompanysByUserAsync(int userId)
    {
        List<RoleDataInfo> roleDataList = await FindByUserAsync(userId);
        List<string> companyList = new();

        foreach (RoleDataInfo roleDataInfo in roleDataList)
        {
            if (string.IsNullOrEmpty(roleDataInfo.BelongCompanys)) continue;
            string[] tmpList = roleDataInfo.BelongCompanys.Split(',');
            foreach (string id in tmpList)
            {
                if (!companyList.Contains(id))
                {
                    companyList.Add(id);
                }
            }
        }
        return companyList;
    }

    /// <summary>
    /// 获取用户所属角色对应的管理公司列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<List<string>> GetBelongDeptsByUserAsync(int userId)
    {
        List<RoleDataInfo> roleDataList = await FindByUserAsync(userId);
        List<string> deptList = new();

        foreach (RoleDataInfo roleDataInfo in roleDataList)
        {
            if (string.IsNullOrEmpty(roleDataInfo.BelongDepts)) continue;
            string[] tmpList = roleDataInfo.BelongDepts.Split(',');
            foreach (string id in tmpList)
            {
                if (!deptList.Contains(id))
                {
                    deptList.Add(id);
                }
            }
        }
        return deptList;
    }


    /// <summary>
    /// 获取用户所属角色对应的数据权限集合
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<List<RoleDataInfo>> FindByUserAsync(int userId)
    {
        //获取用户包含的角色
        List<RoleInfo> rolesByUser = await _userRoleService.GetRolesByUserAsync(userId);
        List<int> roleList = new();
        foreach (RoleInfo info in rolesByUser)
        {
            roleList.Add(info.ID);
        }

        //获取用户信息
        UserInfo userInfo = await _userService.FindByIdAsync(userId);

        //根据角色获取对应的数据权限集合
        List<RoleDataInfo> list = new();
        foreach (int roleId in roleList)
        {
            RoleDataInfo info = await FindByRoleIdAsync(roleId);
            if (info == null) continue;

            #region 替换所在部门和所在公司的值
            if (!string.IsNullOrEmpty(info.BelongCompanys))
            {
                //不重复出现的公司列表
                List<string> notDuplicatedCompanyList = new();

                string[] companyList = info.BelongCompanys.Split(',');
                for (var i = 0; i < companyList.Length; i++)
                {
                    if (companyList[i] == "-1") // -1代表用户所在公司
                    {
                        companyList[i] = userInfo.CompanyId;
                    }

                    if (!notDuplicatedCompanyList.Contains(companyList[i]))
                    {
                        notDuplicatedCompanyList.Add(companyList[i]);
                    }
                }
                info.BelongCompanys = string.Join(",", notDuplicatedCompanyList);
            }
            if (!string.IsNullOrEmpty(info.BelongDepts))
            {
                //不重复出现的部门列表
                List<string> notDuplicatedDeptList = new();

                string[] deptList = info.BelongDepts.Split(',');
                for (var i = 0; i < deptList.Length; i++)
                {
                    if (deptList[i] == "-11") // -11代表用户所在部门
                    {
                        deptList[i] = userInfo.DeptId;
                    }

                    if (!notDuplicatedDeptList.Contains(deptList[i]))
                    {
                        notDuplicatedDeptList.Add(deptList[i]);
                    }
                }

                info.BelongDepts = string.Join(",", deptList);
            } 
            #endregion

            list.Add(info);
        }
        return list;
    }

    /// <summary>
    /// 根据角色ID获取对应的记录对象
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<RoleDataInfo> FindByRoleIdAsync(int roleId)
    {
        string condition = $"Role_ID = {roleId}";
        return await FindSingleAsync(condition);
    }

    /// <summary>
    /// 保存角色的数据权限
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="belongCompanys">包含公司</param>
    /// <param name="belongDepts">包含部门</param>
    /// <returns></returns>
    public async Task<bool> UpdateRoleDataAsync(int roleId, string belongCompanys, string belongDepts)
    {
        bool result = false;
        RoleDataInfo info = await FindByRoleIdAsync(roleId);
        if (info != null)
        {
            info.BelongCompanys = belongCompanys;
            info.BelongDepts = belongDepts;

            result = await UpdateAsync(info);
        }
        else
        {
            info = new RoleDataInfo
            {
                RoleId = roleId,
                BelongCompanys = belongCompanys,
                BelongDepts = belongDepts
            };

            result = await InsertAsync(info);
        }
        return result;
    }

    /// <summary>
    /// 获取数据库的配置，角色数据权限(不对所在公司，所在部门转义）
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetRoleDataDictAsync(int roleId)
    {
        Dictionary<string, string> dict = new();
        //获取用户的角色权限
        RoleDataInfo roleDataInfo = await FindByRoleIdAsync(roleId);
        if (roleDataInfo != null)
        {
            //包含公司
            if (!string.IsNullOrEmpty(roleDataInfo.BelongCompanys))
            {
                List<string> companyList = roleDataInfo.BelongCompanys.ToDelimitedList<string>(",");
                foreach (string id in companyList)
                {
                    if (!dict.ContainsKey(id))
                    {
                        dict.Add(id, id);
                    }
                }
            }
            //包含部门
            if (!string.IsNullOrEmpty(roleDataInfo.BelongDepts))
            {
                List<string> deptList = roleDataInfo.BelongDepts.ToDelimitedList<string>(",");
                foreach (string id in deptList)
                {
                    if (!dict.ContainsKey(id))
                    {
                        dict.Add(id, id);
                    }
                }
            }
            //排除部门

        }
        return dict;
    }
}