using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Utils;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 线路报价明细 业务逻辑类
/// </summary>
[ApiDescriptionSettings("线路与报价")]
public class SegmentsService : BaseService<Segments>, IDynamicApiController, ITransient
{
    public SegmentsService(BaseRepository<Segments> repository, IValidator<Segments> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Segments> NewEntityAsync()
    {
        Segments entity = await base.NewEntityAsync();
        entity.PayNodeType = "1";
        entity.RecvNodeType = "4";
        entity.OpenTime = DateTime.Now;
        entity.Closetime = DateTime.Now;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
        entity.FinancialCenterType = "4";
        return entity;
    }

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    public override async Task<Dictionary<string, int>> GetPermitDictAsync()
    {
        var permitDict = new Dictionary<string, int>
        {
            { Segments.FieldISID, 3 },
            { Segments.FieldSegmentNo, 3 },
            { Segments.FieldCreationDate, 3 },
            { Segments.FieldCreatedBy, 3 },
            { Segments.FieldLastUpdateDate, 3 },
            { Segments.FieldLastUpdatedBy, 3 },
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
    public override async Task<bool> InsertAsync(Segments obj)
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
    public override async Task<bool> UpdateAsync(Segments obj)
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
        return Cache.Instance.GetOrCreate($"{nameof(Segments)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(Segments.FieldCostType, SqlOperator.Equal),
                new(Segments.FieldQuotationNo, SqlOperator.Equal),
                new(Segments.FieldPayNodeType, SqlOperator.Equal),
                new(Segments.FieldPayNodeNo, SqlOperator.Like),
                new(Segments.FieldRecvNodeType, SqlOperator.Equal),
                new(Segments.FieldRecvNodeNo, SqlOperator.Like),
                new(Segments.FieldOpenTime, SqlOperator.Between),
                new(Segments.FieldClosetime, SqlOperator.Between),
                new(Segments.FieldRemark, SqlOperator.Like),
                new(Segments.FieldFinancialCenterType, SqlOperator.Equal),
                new(Segments.FieldFinancialCenter, SqlOperator.Like)
            });
    }
}