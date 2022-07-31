using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Core.Services.User;
using BB.Entity.Security;
using BB.Tools.Format;

namespace BB.Core.Services.OU;

public class OUService : BaseService<OUInfo>, IDynamicApiController, ITransient
{
    private readonly UserRoleService _userRoleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public OUService(BaseRepository<OUInfo> repository, UserRoleService userRoleService) :
        base(repository)
    {
        _userRoleService = userRoleService;
    }

    /// <summary>
    /// 删除机构
    /// </summary>
    /// <param name="key">机构ID</param>
    /// <returns></returns>
    public override async Task<bool> DeleteAsync(object key)
    {
        if (await Repository.AsQueryable().Where(x => x.PID == key.ObjToStr()).AnyAsync())
        {
            throw Oops.Bah("当前机构存在下属机构，暂无法删除。");
        }

        return await base.DeleteAsync(key);
    }

    /// <summary>
    /// 重载只是显示未被删除的记录
    /// </summary>
    /// <returns></returns>
    [NonAction]
    public async Task<List<OUInfo>> GetAllAsync()
    {
        return await FindAsync(" Deleted = 0");
    }

    /// <summary>
    /// 获取顶部的集团信息
    /// </summary>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetTopGroupAsync()
    {
        return await FindAsync("PID='-1' ");
    }
               
    /// <summary>
    /// 根据当前用户身份，获取对应的顶级机构管理节点。
    /// 如果是超级管理员，返回集团节点；如果是公司管理员，返回其公司节点
    /// </summary>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetMyTopGroupAsync()
    {
        List<OUInfo> list = new();

        if (await _userRoleService.UserInRoleAsync(LoginUserInfo.ID, RoleInfo.SUPER_ADMIN_ID))
        {
            //超级管理员取集团节点
            list.AddRange(await GetTopGroupAsync());
        }
        else
        {
            OUInfo groupInfo = await FindByIdAsync(LoginUserInfo.CompanyId);//公司管理员取公司节点
            list.Add(groupInfo);
        }
        return list;
    }
        
    /// <summary>
    /// 获取部门分类为公司的列表【Category='公司'】
    /// </summary>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetAllCompanyAsync(string groupId)
    {
        string condition = $"Category='公司' AND PID='{groupId}' ";
        return await FindAsync(condition);
    }

    /// <summary>
    /// 获取集团和公司的列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetGroupCompanyAsync()
    {
        string condition = string.Format("Category='公司' or Category='集团' ");
        return await FindAsync(condition);
    }

    /// <summary>
    /// 获取集团和公司的树形结构列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<OUNodeInfo>> GetGroupCompanyTreeAsync()
    {
        List<OUNodeInfo> list = new();

        //顶级OU的PID=-1 为集团OU节点
        List<OUInfo> ouList = await GetTopGroupAsync();
        foreach (OUInfo groupOu in ouList)
        {
            if (groupOu != null)
            {
                var groupNodeInfo = new OUNodeInfo(groupOu);

                List<OUInfo> companyList = await GetAllCompanyAsync(groupOu.HandNo);
                foreach (OUInfo info in companyList)
                {
                    groupNodeInfo.Children.Add(new OUNodeInfo(info));
                }
                list.Add(groupNodeInfo);
            }
        }
        return list;
    }
        
    /// <summary>
    /// 为机构制定新的人员列表
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <param name="newUserList">人员列表</param>
    /// <returns></returns>
    public async Task<bool> EditOuUsersAsync(string ouId, List<int> newUserList)
    {
        return await Repository.UseTransactionAsync(async () =>
        {
            if (await Repository.Db.Deleteable<OUUserEntity>().Where(x=>x.OUId == ouId).ExecuteCommandHasChangeAsync())
            {
                throw Oops.Bah("历史关联数据删除失败");
            }

            List<OUUserEntity> list = newUserList.Select(x => new OUUserEntity() { OUId = ouId, UserId = x }).ToList();
            if (list.Count != await Repository.Db.Insertable(list).ExecuteCommandAsync())
            {
                throw Oops.Bah("写入人员列表失败");
            }
        }, e =>
        {
            e.Message.LogError();
            throw e;
        });
    }

    /// <summary>
    /// 为机构添加相关用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="ouId">机构ID</param>
    public async Task AddUserAsync(int userId, string ouId)
    {
        await Repository.Db.Insertable(new OUUserEntity() { OUId = ouId, UserId = userId }).ExecuteCommandAsync();
    }

    /// <summary>
    /// 根据角色ID获取对应的机构列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetOUsByRoleAsync(int roleId)
    {
        return await Repository.Db.Queryable<OUInfo, OURoleEntity>((x, r) => x.HandNo == r.OUId)
            .Where((x, r) => r.RoleId == roleId)
            .ToListAsync();
    }

    /// <summary>
    /// 获取指定用户的机构列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetOUsByUserAsync(int userId)
    {
        string sql = "SELECT * FROM T_ACL_OU INNER JOIN T_ACL_OU_User On T_ACL_OU.HandNo=T_ACL_OU_User.OU_ID WHERE User_ID = " + userId;
        return await Repository.GetListAsync(sql);
    }

    /// <summary>
    /// 获取指定公司下的所有部门
    /// </summary>
    /// <param name="companyId">用户所在公司</param>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetOusByCompanyAsync(string companyId)
    {
        string condition = $"Company_ID='{companyId}' AND Deleted=0";
        var list = await FindAsync(condition);
        return list;
    }
                        
    /// <summary>
    /// 根据指定机构节点ID，获取其下面所有机构列表
    /// </summary>
    /// <param name="parentId">指定机构节点ID</param>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetAllOUsByParentAsync(string parentId)
    {
        List<OUInfo> list = new();
        List<OUInfo> dt = await Repository.GetListAsync(x => !x.Deleted);
        List<OUInfo> childList = dt.Where(x => x.PID == parentId).OrderBy(x => x.SortCode).ToList();
        childList.ForEach(x => list.AddRange(GetOu(x.HandNo, dt)));
        return list;
    }

    private List<OUInfo> GetOu(string id, List<OUInfo> dt)
    {
        List<OUInfo> list = new();
        OUInfo ouInfo = dt.Find(x => x.HandNo == id);
        list.Add(ouInfo);

        List<OUInfo> children = dt.Where(x => x.PID == id).OrderBy(x => x.SortCode).ToList();
        children.ForEach(x => { list.AddRange(GetOu(x.HandNo, dt)); });

        return list;
    }

    /// <summary>
    /// 获取树形结构的机构列表
    /// </summary>
    public async Task<List<OUNodeInfo>> GetTreeAsync()
    {
        List<OUNodeInfo> arrReturn = new();
        List<OUInfo> dt = await GetAllAsync(OUInfo.FieldPID);
        List<OUInfo> childList = dt.Where(x => x.PID == "-1").OrderBy(x => x.SortCode).ToList();
        childList.ForEach(x => arrReturn.Add(GetNode(x.HandNo, dt)));
        return arrReturn;
    }

    /// <summary>
    /// 获取指定机构下面的树形列表
    /// </summary>
    /// <param name="mainOuid">指定机构ID</param>
    public async Task<List<OUNodeInfo>> GetTreeByIdAsync(string mainOuid)
    {
        List<OUNodeInfo> arrReturn = new();
        List<OUInfo> dt = await GetAllAsync(OUInfo.FieldPID);
        List<OUInfo> childList = dt.Where(x => x.PID == mainOuid).OrderBy(x => x.SortCode).ToList();
        childList.ForEach(x => arrReturn.Add(GetNode(x.HandNo, dt)));
        return arrReturn;
    }

    private OUNodeInfo GetNode(string id, List<OUInfo> dt)
    {
        OUInfo ouInfo = dt.Find(x => x.HandNo == id);
        OUNodeInfo ouNodeInfo = new(ouInfo);

        List<OUInfo> children = dt.Where(x => x.PID == id).OrderBy(x => x.SortCode).ToList();
        children.ForEach(x =>
        {
            OUNodeInfo child = GetNode(x.HandNo, dt);
            ouNodeInfo.Children.Add(child);
        });

        return ouNodeInfo;
    }

    /// <summary>
    /// 获取机构的名称
    /// </summary>
    /// <param name="id">机构ID</param>
    /// <returns></returns>
    public async Task<string> GetNameAsync(string id)
    {
        return await GetFieldValueAsync(id, OUInfo.FieldName);
    }

    /// <summary>
    /// 根据机构名称获取对应的对象
    /// </summary>
    /// <param name="name">机构名称</param>
    /// <returns></returns>
    public async Task<OUInfo> FindByNameAsync(string name)
    {
        string condition = $"Name ='{name}' ";
        return await FindSingleAsync(condition);
    }
                        
    /// <summary>
    /// 设置删除标志
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="deleted">是否删除</param>
    /// <returns></returns>
    public async Task<bool> SetDeletedFlagAsync(object id, bool deleted = true)
    {
        return await UpdateFieldsAsync(new Hashtable() { { OUInfo.FieldDeleted, deleted ? 1 : 0 }, { OUInfo.PrimaryKey, id } });
    }

    /// <summary>
    /// 根据机构的ID递归获取其公司的ID
    /// </summary>
    /// <param name="ouId"></param>
    /// <returns></returns>
    private async Task<OUInfo> GetCompanyInfoAsycn(string ouId)
    {
        OUInfo info = await FindByIdAsync(ouId);
        if (info.Category == "公司")
        {
            return info;
        }
        else
        {
            return await GetCompanyInfoAsycn(info.PID);
        }
    }
}