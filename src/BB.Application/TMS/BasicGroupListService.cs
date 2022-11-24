using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Utils;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 区域分组 业务逻辑类
/// </summary>
[ApiDescriptionSettings("线路与报价")]
public class BasicGroupListService : BaseService<BasicGroupList>, IDynamicApiController, ITransient
{
    public BasicGroupListService(BaseRepository<BasicGroupList> repository, IValidator<BasicGroupList> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<BasicGroupList> SetDynamicDefaults(BasicGroupList entity)
    {
        return Task.FromResult(entity);
    }

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <param name="isNew"></param>
    /// <returns></returns>
    public override async Task<Dictionary<string, int>> GetPermitDictAsync()
    {
        var permitDict = new Dictionary<string, int>
        {
            // 主键不为空则只读
            { nameof(BasicGroupList.ISID), 4 },
            { nameof(BasicGroupList.GroupName), 4 },
            { nameof(BasicGroupList.CreationDate), 1 },
            { nameof(BasicGroupList.CreatedBy), 1 },
            { nameof(BasicGroupList.LastUpdateDate), 1 },
            { nameof(BasicGroupList.LastUpdatedBy), 1 },
            { nameof(BasicGroupList.FlagApp), 1 },
            { nameof(BasicGroupList.AppUser), 1 },
            { nameof(BasicGroupList.AppDate), 1 }
        };
        // 后端权限控制
        // var permitDict = BllFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Test1Car).FullName, LoginUserInfo.ID.ToInt32());
        return await Task.FromResult(permitDict);
    }

    /// <summary>
    /// 新增一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public override async Task<bool> InsertAsync(BasicGroupList obj)
    {
        #region 新增前

        #endregion

        bool succeed = await base.InsertAsync(obj);

        #region 新增后

        if (!succeed) return false;

        //可添加其他关联操作

        return true;

        #endregion
    }

    /// <summary>
    /// 更新一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> UpdateAsync(BasicGroupList obj)
    {
        #region 修改前

        #endregion

        bool succeed = await base.UpdateAsync(obj);

        #region 修改后

        if (!succeed) return false;

        //可添加其他关联操作

        return true;

        #endregion
    }

    /// <summary>
    /// 删除一条数据
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> DeleteAsync(object key)
    {
        #region 删除前

        #endregion

        bool succeed = await base.DeleteAsync(key);
        if (!succeed) return false;

        #region 删除后

        //可添加其他关联操作

        #endregion

        return true;
    }

    /// <summary>
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.Instance.GetOrCreate($"{nameof(BasicGroupList)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(BasicGroupList.FieldGroupName, SqlOperator.Like),
                new(BasicGroupList.FieldGroupType, SqlOperator.Equal),
                new(BasicGroupList.FieldCostType, SqlOperator.Equal),
                new(BasicGroupList.FieldGroupContent, SqlOperator.Like),
                new(BasicGroupList.FieldGroupExceptContent, SqlOperator.Like),
                new(BasicGroupList.FieldRemark, SqlOperator.Like),
                new(BasicGroupList.FieldCreationDate, SqlOperator.Between),
                new(BasicGroupList.FieldCreatedBy, SqlOperator.Equal),
                new(BasicGroupList.FieldLastUpdateDate, SqlOperator.Between),
                new(BasicGroupList.FieldLastUpdatedBy, SqlOperator.Equal),
                new(BasicGroupList.FieldFlagApp, SqlOperator.Like),
                new(BasicGroupList.FieldAppUser, SqlOperator.Equal),
                new(BasicGroupList.FieldAppDate, SqlOperator.Between)
            });
    }
}