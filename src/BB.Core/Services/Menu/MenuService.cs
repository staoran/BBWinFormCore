using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Core.Services.User;
using BB.Entity.Security;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Utils;
using FluentValidation;

namespace BB.Core.Services.Menu;

[ApiDescriptionSettings("权限")]
public class MenuService : BaseService<MenuInfo>, IDynamicApiController, ITransient
{
    private readonly UserRoleService _userRoleService;

    public MenuService(BaseRepository<MenuInfo> repository, IValidator<MenuInfo> validator, UserRoleService userRoleService) : base(repository, validator)
    {
        _userRoleService = userRoleService;
    }

    /// <summary>
    /// 获取所有的菜单列表
    /// </summary>
    public async Task<List<MenuInfo>> GetAllMenuAsync(string systemType)
    {
        return await Repository.AsQueryable()
            .Where(x => x.Visible)
            .WhereIF(!systemType.IsNullOrEmpty(), x => x.SystemTypeId == systemType)
            .OrderBy(x => x.PID)
            .OrderBy(x => x.Seq)
            .ToListAsync();
    }
                
    /// <summary>
    /// 获取树形结构的菜单列表
    /// </summary>
    public async Task<List<MenuNodeInfo>> GetTreeAsync(string systemType)
    {
        List<MenuNodeInfo> arrReturn = new();
        List<MenuInfo> menuInfos = await GetAllMenuAsync(systemType);

        List<MenuInfo> roots = menuInfos.Where(x => x.PID == "-1").OrderBy(x => x.Seq).ToList();
        
        roots.ForEach(x =>
        {
            MenuNodeInfo menuNodeInfo = GetNode(x.ID, menuInfos);
            arrReturn.Add(menuNodeInfo);
        });

        return arrReturn;
    }

    /// <summary>
    /// 获取所有的菜单列表
    /// </summary>
    public async Task<List<MenuInfo>> GetAllTreeAsync(string systemType)
    {
        return await Repository.AsQueryable()
            .Where(x => x.Visible)
            .WhereIF(!systemType.IsNullOrEmpty(), x => x.SystemTypeId == systemType)
            .OrderBy(x => x.PID)
            .OrderBy(x => x.Seq)
            .ToListAsync();
    }

    /// <summary>
    /// 获取第一级的菜单列表
    /// </summary>
    public async Task<List<MenuInfo>> GetTopMenuAsync(string systemType)
    {
        return await Repository.AsQueryable()
            .Where(x => x.Visible && x.PID == "-1")
            .WhereIF(!systemType.IsNullOrEmpty(), x => x.SystemTypeId == systemType)
            .OrderBy(x => x.Seq)
            .ToListAsync();
    }

    /// <summary>
    /// 获取指定菜单下面的树形列表
    /// </summary>
    /// <param name="mainMenuId">指定菜单ID</param>
    public async Task<List<MenuNodeInfo>> GetTreeByIdAsync(string mainMenuId)
    {
        List<MenuNodeInfo> arrReturn = new();
        List<MenuInfo> dt = await GetAllMenuAsync("");
        
        List<MenuInfo> children = dt.Where(x => x.PID == mainMenuId).OrderBy(x => x.Seq).ToList();
        children.ForEach(x =>
        {
            MenuNodeInfo child = GetNode(x.ID, dt);
            arrReturn.Add(child);
        });

        return arrReturn;
    }

    /// <summary>
    /// 根据指定的父ID获取其下面一级（仅限一级）的菜单列表
    /// </summary>
    /// <param name="pid">菜单父ID</param>
    public async Task<List<MenuInfo>> GetMenuByIdAsync(string pid)
    {
        return await Repository.GetListAsync(x=>x.PID == pid);
    }

    private MenuNodeInfo GetNode(string id, List<MenuInfo> dt)
    {
        MenuInfo menuInfo = dt.Find(x => x.ID == id);
        MenuNodeInfo menuNodeInfo = new(menuInfo);

        List<MenuInfo> children = dt.Where(x => x.PID == id).OrderBy(x => x.Seq).ToList();
        children.ForEach(x =>
        {
            MenuNodeInfo child = GetNode(x.ID, dt);
            menuNodeInfo.Children.Add(child);
        });

        return menuNodeInfo;
    }

    /// <summary>
    /// 快速新增自动生成模块的菜单
    /// </summary>
    /// <param name="name">菜单名</param>
    /// <param name="winFormType">模块地址</param>
    public async Task<bool> AddTransferMenuAsync(string name, string winFormType)
    {
        if (await IsExistKeyAsync("WinformType", winFormType)) return true;
        var menu = new MenuInfo()
        {
            PID = "363811f1-a57e-4b9f-bc26-ad39379e1f0d",
            Name = name,
            Icon = "Images\\MenuIcon\\301.ico",
            Visible = true,
            WinformType = winFormType,
            Url = "#",
            SystemTypeId = "WareMis",
            CurrentLoginUserId = LoginUserInfo.ID.ToString()
        };
        return await InsertAsync(menu);
    }


    /*
     * 在引入和角色多对多的关系后，菜单作为角色的资源之一，和功能模块并立。
     * 因此在处理上和Function表的处理类似，作为角色的资源之一。
     */ 
      
    /// <summary>
    /// 根据角色集合和系统标识获取对应的菜单集合
    /// </summary>
    /// <param name="roleIDs">角色ID字符串</param>
    /// <param name="typeId">系统类型</param>
    /// <returns></returns>
    public async Task<List<MenuNodeInfo>> GetMenuNodesAsync(string roleIDs, string typeId)
    {
        if (roleIDs == string.Empty)
        {
            roleIDs = "-1";
        }
        // 目前并没有基于 T_ACL_Role_Menu 的菜单权限管理，菜单权限是基于 功能设置 和 功能ID 实现。
        string sql = string.Format(@"SELECT * FROM {1} Where ID in(
            SELECT distinct ID FROM {1} 
            INNER JOIN T_ACL_Role_Menu On {1}.ID=T_ACL_Role_Menu.Menu_ID WHERE Role_ID IN ({0}) ) AND Visible > 0", roleIDs, Repository.TableName);
        if (!string.IsNullOrWhiteSpace(typeId))
        {
            sql += $" AND SystemType_ID='{typeId}' ";
        }

        List<MenuNodeInfo> arrReturn = new ();
        List<MenuInfo> dt = await Repository.GetListAsync(sql);

        List<MenuInfo> roots = dt.Where(x => x.PID == "-1").OrderBy(x => x.Seq).ToList();
        
        roots.ForEach(x =>
        {
            MenuNodeInfo menuNodeInfo = GetNode(x.ID, dt);
            arrReturn.Add(menuNodeInfo);
        });

        return arrReturn;
    }

    /// <summary>
    /// 根据角色ID获取功能集合
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    public async Task<List<MenuInfo>> GetMenusByRole(int roleId, string typeId)
    {
        // 目前并没有基于 T_ACL_Role_Menu 的菜单权限管理，菜单权限是基于 功能设置 和 功能ID 实现。
        string sql = string.Format(@"SELECT * FROM {0} 
            LEFT JOIN T_ACL_Role_Menu On {0}.ID=T_ACL_Role_Menu.Menu_ID WHERE Role_ID = {1}", Repository.TableName, roleId);
        if (!string.IsNullOrWhiteSpace(typeId))
        {
            sql += $" AND SystemType_ID='{typeId}' ";
        }
        return await Repository.GetListAsync(sql);
    }

    /// <summary>
    /// 根据用户ID，获取对应的菜单列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    public async Task<List<MenuNodeInfo>> GetMenuNodesByUser(int userId, string typeId)
    {
        List<RoleInfo> rolesByUser = await _userRoleService.GetRolesByUserAsync(userId);
        string roleIDs = ",";
        foreach (RoleInfo info in rolesByUser)
        {
            roleIDs = roleIDs + info.ID + ",";
        }
        roleIDs = roleIDs.Trim(',');//移除前后的逗号

        List<MenuNodeInfo> menuList = new List<MenuNodeInfo>();
        if (!string.IsNullOrEmpty(roleIDs))
        {
            menuList = await GetMenuNodesAsync(roleIDs, typeId);
        }
        return menuList;
    }

    /// <summary>
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.Instance.GetOrCreate($"{nameof(MenuInfo)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(MenuInfo.FieldName, SqlOperator.Like ),
                new(MenuInfo.FieldFunctionId, SqlOperator.Like ),
                new(MenuInfo.FieldWinformType, SqlOperator.Like ),
                new(MenuInfo.FieldUrl, SqlOperator.Like ),
                new(MenuInfo.FieldVisible, SqlOperator.Equal )
            });
    }
}