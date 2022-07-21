using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Tools.Entity;

namespace BB.Core.DbContext;

/// <summary>
/// 分页查询扩展
/// </summary>
[SuppressSniffer]
public static class PagedQueryableExtensions
{
    // <summary>
    // 分页拓展
    // </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entities"></param>
    /// <param name="pageIndex">页码，必须大于0</param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static 
    #nullable disable
    PageResult<TEntity> ToPagedList<TEntity>(this ISugarQueryable<TEntity> entities, int pageIndex = 1, int pageSize = 20) where TEntity : new()
    {
      if (pageIndex <= 0)
        throw new InvalidOperationException("pageIndex 必须是大于 0 的正整数。");
      var total = 0;
      List<TEntity> list = entities.ToPageList(pageIndex, pageSize, ref total);
      var num2 = (int) Math.Ceiling((double) total / pageSize);
      return new PageResult<TEntity>()
      {
        PageNo = pageIndex,
        PageSize = pageSize,
        Rows = list,
        TotalRows = total,
        TotalPage = num2
      };
    }

    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entities"></param>
    /// <param name="pageIndex">页码，必须大于0</param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static async Task<PageResult<TEntity>> ToPagedListAsync<TEntity>(this ISugarQueryable<TEntity> entities,
      int pageIndex = 1, int pageSize = 20) where TEntity : new()
    {
      if (pageIndex <= 0)
        throw new InvalidOperationException("pageIndex 必须是大于 0 的正整数。");
      RefAsync<int> total = 0;
      List<TEntity> listAsync = await entities.ToPageListAsync(pageIndex, pageSize, total);
      var num = (int) Math.Ceiling((double) total / pageSize);
      return new PageResult<TEntity>()
      {
        PageNo = pageIndex,
        PageSize = pageSize,
        Rows = listAsync,
        TotalRows = total,
        TotalPage = num
      };
    }
}