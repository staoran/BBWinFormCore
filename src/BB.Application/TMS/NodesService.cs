using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Cache;
using BB.Tools.Entity;
using BB.Tools.Extension;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 网点区域 业务逻辑类
/// </summary>
[ApiDescriptionSettings("运输基础资料")]
public class NodesService : BaseService<Nodes>, IDynamicApiController, ITransient
{
    public NodesService(BaseRepository<Nodes> repository, IValidator<Nodes> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<Nodes> SetDynamicDefaults(Nodes entity)
    {
        return Task.FromResult(entity);
    }

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    public override async Task<Dictionary<string, int>> GetPermitDictAsync()
    {
        var permitDict = new Dictionary<string, int>
        {
            { nameof(Nodes.ISID), 3 },
            { nameof(Nodes.TranNodeNO), 3 },
            { nameof(Nodes.ProvinceId), 3 },
            { nameof(Nodes.CityId), 3 },
            { nameof(Nodes.TownId), 3 },
            { nameof(Nodes.CreationDate), 3 },
            { nameof(Nodes.CreatedBy), 3 },
            { nameof(Nodes.LastUpdateDate), 3 },
            { nameof(Nodes.LastUpdatedBy), 3 },
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
    public override async Task<bool> InsertAsync(Nodes obj)
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
    public override async Task<bool> UpdateAsync(Nodes obj)
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
        return Cache.Instance.GetOrCreate($"{nameof(Nodes)}ConditionTypes",
            () =>
            {
                return new List<FieldConditionType>
                {
                    new(Nodes.FieldTranNodeAreaName, SqlOperator.Like),
                    new(Nodes.FieldTranNodeAreaDesc, SqlOperator.Like),
                    new(Nodes.FieldDeliveryType, SqlOperator.Equal),
                    new(Nodes.FieldEFence, SqlOperator.Like),
                    new(Nodes.FieldCenterCoordinate, SqlOperator.Like),
                    new(Nodes.FieldConvertVK, SqlOperator.Between),
                    new(Nodes.FieldAreaId, SqlOperator.Equal),
                    new(Nodes.FieldAddress, SqlOperator.Like),
                    new(Nodes.FieldPerson, SqlOperator.Like),
                    new(Nodes.FieldPhone, SqlOperator.Like),
                    new(Nodes.FieldSignLimitHour, SqlOperator.Between),
                    new(Nodes.FieldCancelYN, SqlOperator.Equal),
                    new(Nodes.FieldRemark, SqlOperator.Like)
                };
            });
    }

    /// <summary>
    /// 构造查询条件
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override async Task<List<IConditionalModel>> GetConditionExc(Dictionary<string, string> searchInfos)
    {
        var condition = await base.GetConditionExc(searchInfos);
        bool areaNoLength =
            !searchInfos[Nodes.FieldAreaId].IsNullOrEmpty() && searchInfos[Nodes.FieldAreaId].Length < 6;
        if (areaNoLength)
        {
            condition.ForEach(x =>
            {
                if (x is ConditionalModel { FieldName: Nodes.FieldAreaId } c)
                {
                    c.ConditionalType = ConditionalType.LikeLeft;
                }
            });
        }

        return condition;
    }
}