using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Filter;
using BB.Core.Services.Base;
using BB.Core.Services.User;
using BB.Entity.Security;
using BB.Tools.Format;

namespace BB.Core.Services.Function;

public class FunctionService : BaseService<FunctionInfo>, IDynamicApiController, ITransient
{
    private readonly UserRoleService _userRoleService;

    public FunctionService(BaseRepository<FunctionInfo> repository, UserRoleService userRoleService) : base(repository)
    {
        _userRoleService = userRoleService;
    }

    /// <summary>
    /// 重写删除操作，把下面的节点提到上一级
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public override async Task<bool> DeleteAsync([ModelBinder(typeof(ObjectModelBinder))][Required]object key)
    {
        if (await Repository.AsQueryable().Where(x => x.PID == key.ObjToStr()).AnyAsync())
        {
            throw Oops.Bah("当前功能存在子级功能，暂无法删除。");
        }

        return await base.DeleteAsync(key);
    }

    /// <summary>
    /// 根据角色ID列表和系统类型ID，获取对应的操作功能列表
    /// </summary>
    /// <param name="roleList">角色ID</param>
    /// <param name="typeId">系统类型ID</param>
    /// <returns></returns>
    public async Task<List<FunctionInfo>> GetFunctionsAsync(IEnumerable<int> roleList, string typeId)
    {
        int[] enumerable = roleList as int[] ?? roleList.ToArray();
        if (!enumerable.Any())
        {
            enumerable = new[] { -1 };
        }
        
        return await Repository.AsQueryable()
            .InnerJoin<RoleFunction>((f, rf) =>
                f.ID == rf.FunctionId && enumerable.Contains(rf.RoleId))
            .WhereIF(!typeId.IsNullOrEmpty(), f => f.SystemTypeId == typeId)
            .ToListAsync();
    }

    /// <summary>
    /// 根据角色ID字符串（逗号分开)和系统类型ID，获取对应的操作功能列表
    /// </summary>
    /// <param name="roleIDs">角色ID</param>
    /// <param name="typeId">系统类型ID</param>
    /// <returns></returns>
    public async Task<List<FunctionNodeInfo>> GetFunctionNodesAsync(string roleIDs, string typeId)
    {
        if (roleIDs == string.Empty)
        {
            roleIDs = "-1";
        }
        List<FunctionNodeInfo> arrReturn = new();
        List<FunctionInfo> dt = await GetFunctionsAsync(roleIDs.Split(',').Select(x=>x.ObjToInt()), typeId);
        
        List<FunctionInfo> children = dt.Where(x => x.PID == "-1").OrderBy(x => x.SortCode).ToList();
        children.ForEach(x =>
        {
            FunctionNodeInfo child = GetNode(x.ID, dt);
            arrReturn.Add(child);
        });

        return arrReturn;
    }

    /// <summary>
    /// 根据角色ID获取对应的操作功能列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<List<FunctionInfo>> GetFunctionsByRoleAsync(int roleId)
    {
        return await Repository.AsQueryable()
            .LeftJoin<RoleFunction>((f, rf) => f.ID == rf.FunctionId && rf.RoleId == roleId)
            .ToListAsync();
    }

    /// <summary>
    /// 根据用户ID，获取对应的功能列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    public async Task<List<FunctionInfo>> GetFunctionsByUserAsync(int userId, string typeId)
    {
        List<RoleInfo> rolesByUser = await _userRoleService.GetRolesByUserAsync(userId);
        List<int> roleIDs = rolesByUser.Select(info => info.ID).ToList();

        List<FunctionInfo> functions = new ();
        if (roleIDs.Any())
        {
            functions = await GetFunctionsAsync(roleIDs, typeId);
        }
        
        return functions;
    }

    /// <summary>
    /// 根据用户ID，获取对应的功能列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    public async Task<List<FunctionNodeInfo>> GetFunctionNodesByUserAsync(int userId, string typeId)
    {
        List<RoleInfo> rolesByUser = await _userRoleService.GetRolesByUserAsync(userId);
        string roleIDs = ",";
        foreach (RoleInfo info in rolesByUser)
        {
            roleIDs = roleIDs + info.ID + ",";
        }
        roleIDs = roleIDs.Trim(',');//移除前后的逗号

        List<FunctionNodeInfo> functions = new List<FunctionNodeInfo>();
        if (!string.IsNullOrEmpty(roleIDs))
        {
            functions = await GetFunctionNodesAsync(roleIDs, typeId);
        }
        return functions;
    }

    /// <summary>
    /// 获取当前用户在指定系统类型下的功能集合
    /// </summary>
    /// <param name="typeId"></param>
    /// <returns></returns>
    public async Task<List<FunctionInfo>> GetUserFunctionsAsync(string typeId)
    {
        List<FunctionInfo> functionsByUser = null;
        if (LoginUserInfo != null)
        {
            functionsByUser = await GetFunctionsByUserAsync(LoginUserInfo.ID, typeId);
        }
        return functionsByUser;
    }

    /// <summary>
    /// 获取树形结构的功能列表
    /// </summary>
    /// <param name="systemType">系统类型的OID</param>
    public async Task<List<FunctionNodeInfo>> GetTreeAsync(string systemType)
    {
        List<FunctionNodeInfo> arrReturn = new();
        List<FunctionInfo> menuInfos = await Repository.AsQueryable()
            .WhereIF(!systemType.IsNullOrEmpty(), x => x.SystemTypeId == systemType)
            .OrderBy(x => x.PID)
            .OrderBy(x => x.Name)
            .ToListAsync();

        List<FunctionInfo> roots = menuInfos.Where(x => x.PID == "-1").OrderBy(x => x.SortCode).ToList();
        
        roots.ForEach(x =>
        {
            FunctionNodeInfo menuNodeInfo = GetNode(x.ID, menuInfos);
            arrReturn.Add(menuNodeInfo);
        });

        return arrReturn;
    }

    /// <summary>
    /// 获取指定功能下面的树形列表
    /// </summary>
    /// <param name="mainId">指定功能ID</param>
    public async Task<List<FunctionNodeInfo>> GetTreeByIdAsync(string mainId)
    {
        List<FunctionNodeInfo> arrReturn = new ();
        List<FunctionInfo> dt = (await Repository.GetListAsync()).OrderBy(x => x.PID).ThenBy(x => x.Name).ToList();
        List<FunctionInfo> children = dt.Where(x => x.PID == mainId).OrderBy(x => x.SortCode).ToList();
        children.ForEach(x =>
        {
            FunctionNodeInfo child = GetNode(x.ID, dt);
            arrReturn.Add(child);
        });

        return arrReturn;
    }
                       
    /// <summary>
    /// 根据角色获取树形结构的功能列表
    /// </summary>
    public async Task<List<FunctionNodeInfo>> GetTreeWithRoleAsync(string systemType, List<int> roleList)
    {
        List<FunctionNodeInfo> list = new();
        if (roleList.Count > 0)
        {
            List<FunctionInfo> dt = (await GetFunctionsAsync(roleList, systemType))
                .OrderBy(f => f.PID)
                .ThenBy(f => f.Name)
                .ToList();
        
            List<FunctionInfo> children = dt.Where(x => x.PID == "-1").OrderBy(x => x.SortCode).ToList();
            children.ForEach(x =>
            {
                FunctionNodeInfo child = GetNode(x.ID, dt);
                list.Add(child);
            });
        }
        return list;
    }

    private FunctionNodeInfo GetNode(string id, List<FunctionInfo> dt)
    {
        FunctionInfo menuInfo = dt.Find(x => x.ID == id);
        FunctionNodeInfo menuNodeInfo = new (menuInfo);
        
        List<FunctionInfo> children = dt.Where(x => x.PID == id).OrderBy(x => x.SortCode).ToList();
        children.ForEach(x =>
        {
            FunctionNodeInfo child = GetNode(x.ID, dt);
            menuNodeInfo.Children.Add(child);
        });

        return menuNodeInfo;
    }

    /// <summary>
    /// 根据角色获取树形结构的功能列表
    /// </summary>
    public async Task<List<FunctionNodeInfo>> GetTreeWithUserAsync(string systemType, int userId)
    {
        List<RoleInfo> rolesByUser = await _userRoleService.GetRolesByUserAsync(userId);
        List<int> roleList = new List<int>();
        foreach (RoleInfo info in rolesByUser)
        {
            roleList.Add(info.ID);
        }

        return await GetTreeWithRoleAsync(systemType, roleList);
    }
    
    /// <summary>
    /// 批量新增功能权限
    /// </summary>
    /// <param name="mainInfo">模块权限</param>
    /// <param name="isAdd">新增权限</param>
    /// <param name="isUpdate">修改权限</param>
    /// <param name="isDelete">删除权限</param>
    /// <param name="isExport">导出权限</param>
    /// <param name="isImport">导入权限</param>
    /// <param name="isView">查看权限</param>
    /// <returns></returns>
    /// <exception cref="DataException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<bool> AddMore(FunctionInfo mainInfo, bool isAdd, bool isUpdate, bool isDelete, bool isExport, bool isImport, bool isView)
    {
        return await Repository.UseTransactionAsync(async () =>
        {
            if (await InsertAsync(mainInfo))
            {
                FunctionInfo subInfo;
                int sortCodeIndex = 1;

                #region 子功能操作

                if (isAdd)
                {
                    subInfo = CreateSubFunction(mainInfo);
                    subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                    subInfo.ControlId = $"{mainInfo.ControlId}/Add";
                    subInfo.Name = $"添加{mainInfo.Name}";

                    await InsertAsync(subInfo);
                }

                if (isDelete)
                {
                    subInfo = CreateSubFunction(mainInfo);
                    subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                    subInfo.ControlId = $"{mainInfo.ControlId}/Delete";
                    subInfo.Name = $"删除{mainInfo.Name}";
                    await InsertAsync(subInfo);
                }

                if (isUpdate)
                {
                    subInfo = CreateSubFunction(mainInfo);
                    subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                    subInfo.ControlId = $"{mainInfo.ControlId}/Edit";
                    subInfo.Name = $"修改{mainInfo.Name}";
                    await InsertAsync(subInfo);
                }

                if (isView)
                {
                    subInfo = CreateSubFunction(mainInfo);
                    subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                    subInfo.ControlId = $"{mainInfo.ControlId}/View";
                    subInfo.Name = $"查看{mainInfo.Name}";
                    await InsertAsync(subInfo);
                }

                if (isImport)
                {
                    subInfo = CreateSubFunction(mainInfo);
                    subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                    subInfo.ControlId = $"{mainInfo.ControlId}/Import";
                    subInfo.Name = $"导入{mainInfo.Name}";
                    await InsertAsync(subInfo);
                }

                if (isExport)
                {
                    subInfo = CreateSubFunction(mainInfo);
                    subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                    subInfo.ControlId = $"{mainInfo.ControlId}/Export";
                    subInfo.Name = $"导出{mainInfo.Name}";
                    await InsertAsync(subInfo);
                }

                #endregion
            }
            else
            {
                throw Oops.Oh<DataException>("没有数据被保存或修改!");
            }
        }, e =>
        {
            e.ToString().LogError();
            throw e;
        });
    }

    /// <summary>
    /// 删除指定节点及其子节点。如果该节点含有子节点，子节点也会一并删除
    /// </summary>
    /// <param name="mainId">节点ID</param>
    /// <returns></returns>
    public async Task<bool> DeleteWithSubNodeAsync(string mainId)
    {
        //只获取ID、PID所需字段，提高效率
        DataTable dt = await Repository.AsQueryable().Select(x => new { ID = x.ID, PID = x.PID }).ToDataTableAsync();

        List<string> list = new();
        list.AddRange(GetSubNodeIdList(mainId, dt));
        list.Add(mainId);

        return await Repository.DeleteByIdsAsync(list.ToArray());
    }

    /// <summary>
    /// 递归获取指定PID的子节点的ID集合
    /// </summary>
    /// <param name="pid">PID</param>
    /// <param name="dt">所有集合，包含ID、PID</param>
    /// <returns></returns>
    private List<string> GetSubNodeIdList(string pid, DataTable dt)
    {
        List<string> list = new List<string>();

        DataRow[] dataRows = dt.Select($" PID = '{pid}'");
        for (int i = 0; i < dataRows.Length; i++)
        {
            string id = dataRows[i]["ID"].ToString();
            list.Add(id);

            list.AddRange(GetSubNodeIdList(id, dt));//递归获取
        }
        return list;
    }

    private FunctionInfo CreateSubFunction(FunctionInfo mainInfo)
    {
        var subInfo = new FunctionInfo
        {
            PID = mainInfo.ID,
            SystemTypeId = mainInfo.SystemTypeId
        };
        return subInfo;
    }
}