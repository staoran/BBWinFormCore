using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Filter;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Core.Services.Base;

/// <summary>
/// 主从服务抽象扩展基类
/// </summary>
/// <typeparam name="T">主表实体</typeparam>
/// <typeparam name="T1">子表实体</typeparam>
public class BaseMultiService<T, T1> : BaseService<T>
    where T : BaseEntity<T1>, new()
    where T1 : BaseEntity, new()
{
    private readonly BaseRepository<T1> _childRepository;

    public BaseMultiService(BaseRepository<T> repository, BaseRepository<T1> childRepository) : base(repository)
    {
        _childRepository = childRepository;
    }

    /// <summary>
    /// 级联插入指定对象到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public override async Task<bool> InsertAsync(T obj)
    {
        CheckEntity(OperationType.Add, obj);
        obj.ChildTableList?.ForEach(x => CheckEntity(OperationType.Add, x));

        return await Repository.Db.InsertNav(obj)
            .Include(x=>x.ChildTableList)
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 级联插入指定对象集合到数据库中
    /// </summary>
    /// <param name="list">指定的对象集合</param>
    /// <returns>执行操作是否成功。</returns>
    [ApiDescriptionSettings(KeepVerb = true)]
    public override async Task<bool> InsertRangeAsync([Required]List<T> list)
    {
        list.ForEach(x =>
        {
            CheckEntity(OperationType.Add, x);
            x.ChildTableList?.ForEach(x1 => CheckEntity(OperationType.Add, x1));
        });

        return await Repository.Db.InsertNav(list)
            .Include(x=>x.ChildTableList)
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 级联更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> UpdateAsync([Required]T obj)
    {
        CheckEntity(OperationType.Edit, obj);
        obj.ChildTableList?.ForEach(x => CheckEntity(OperationType.Edit, x));

        return await Repository.Db.UpdateNav(obj)
            .Include(x=>x.ChildTableList)
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 根据指定对象的ID,从数据库中删除指定对象和子对象
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [QueryParameters]
    public override async Task<bool> DeleteAsync([ModelBinder(typeof(ObjectModelBinder))][Required]object key)
    {
        var data = await FindByIdAsync(key);
        return await Repository.Db.DeleteNav(data)
            .Include(x1=>x1.ChildTableList)
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 根据指定对象的ID,从数据库中删除指定对象和子对象
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [ApiDescriptionSettings(KeepVerb = true)]
    public override async Task<bool> DeleteByIdsAsync([ModelBinder(typeof(ObjectModelBinder))][Required]object[] key)
    {
        var data = await FindByIDsAsync(key);
        return await Repository.Db.DeleteNav(data)
            .Include(x1=>x1.ChildTableList)
            .ExecuteCommandAsync();
    }
}