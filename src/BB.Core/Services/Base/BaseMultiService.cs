using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Filter;
using BB.Entity.Base;
using BB.Tools.Entity;
using BB.Tools.Validation;
using FluentValidation;

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
    private readonly IValidator<T1> _childValidator;

    public BaseMultiService(BaseRepository<T> repository, BaseRepository<T1> childRepository, IValidator<T> validator, IValidator<T1> childValidator) : base(repository, validator)
    {
        _childRepository = childRepository;
        _childValidator = childValidator;
    }

    /// <summary>
    /// 级联插入指定对象到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public override async Task<bool> InsertAsync(T obj)
    {
        await CheckEntityAsync(OperationType.Add, obj);
        if (obj.ChildTableList != null)
        {
            await Parallel.ForEachAsync(obj.ChildTableList, async (x, _) => await CheckEntityAsync(OperationType.Add, x));
        }

        return await Repository.Db.InsertNav(obj)
            .Include(x => x.ChildTableList)
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
        await Parallel.ForEachAsync(list, async (entity, _) => await CheckEntityAsync(OperationType.Add, entity));

        return await Repository.Db.InsertNav(list)
            .Include(x => x.ChildTableList)
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 级联更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> UpdateAsync([Required]T obj)
    {
        await CheckEntityAsync(OperationType.Edit, obj);

        return await Repository.Db.UpdateNav(obj)
            .Include(x => x.ChildTableList)
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
            .Include(x1 => x1.ChildTableList)
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
            .Include(x1 => x1.ChildTableList)
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 验证一个实体或键值对
    /// </summary>
    /// <param name="operationType">操作类型</param>
    /// <param name="obj">实体或键值对</param>
    /// <returns></returns>
    [NonAction]
    public override async Task CheckEntityAsync(OperationType operationType, object obj)
    {
        await base.CheckEntityAsync(operationType, obj);
        if (obj is T { ChildTableList.Count: > 0 } o)
        {
            await Parallel.ForEachAsync(o.ChildTableList,
                async (entity, _) => await _childValidator.ValidateAndThrowAsync(entity, operationType));
        }
    }
}