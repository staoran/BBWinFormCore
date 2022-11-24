using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Utils;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 客户收货人 业务逻辑类
/// </summary>
[ApiDescriptionSettings("客户基础资料")]
public class CustomersService : BaseService<Customers>, IDynamicApiController, ITransient
{
    public CustomersService(BaseRepository<Customers> repository, IValidator<Customers> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<Customers> SetDynamicDefaults(Customers entity)
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
            { nameof(Customers.ISID), 3 },
            { nameof(Customers.CustomerCode), 3 },
            { nameof(Customers.TranNode), 3 },
            { nameof(Customers.ProvinceNo), 3 },
            { nameof(Customers.CityNo), 3 },
            { nameof(Customers.CreationDate), 3 },
            { nameof(Customers.CreatedBy), 3 },
            { nameof(Customers.LastUpdateDate), 3 },
            { nameof(Customers.LastUpdatedBy), 3 }
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
    public override async Task<bool> InsertAsync(Customers obj)
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
    public override async Task<bool> UpdateAsync(Customers obj)
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
        return Cache.Instance.GetOrCreate($"{nameof(Customers)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(Customers.FieldISID, SqlOperator.Like),
                new(Customers.FieldCustomerCode, SqlOperator.Like),
                new(Customers.FieldTranNode, SqlOperator.Equal),
                new(Customers.FieldContactPerson, SqlOperator.Like),
                new(Customers.FieldNativeName, SqlOperator.Like),
                new(Customers.FieldAddress, SqlOperator.Like),
                new(Customers.FieldAreaNo, SqlOperator.Equal),
                new(Customers.FieldTel, SqlOperator.Like),
                new(Customers.FieldMobile, SqlOperator.Like),
                new(Customers.FieldInsuranceRate, SqlOperator.Between),
                new(Customers.FieldCoordinate, SqlOperator.Like),
                new(Customers.FieldDefaultToNode, SqlOperator.Equal),
                new(Customers.FieldDefaultToNodes, SqlOperator.Equal),
                new(Customers.FieldDefaultToNodesName, SqlOperator.Like),
                new(Customers.FieldCargoName, SqlOperator.Like),
                new(Customers.FieldPackageType, SqlOperator.Equal),
                new(Customers.FieldCargoUnit, SqlOperator.Like),
                new(Customers.FieldPrice, SqlOperator.Between),
                new(Customers.FieldPriceType, SqlOperator.Equal),
                new(Customers.FieldCreationDate, SqlOperator.Between),
                new(Customers.FieldCreatedBy, SqlOperator.Equal),
                new(Customers.FieldLastUpdateDate, SqlOperator.Between),
                new(Customers.FieldLastUpdatedBy, SqlOperator.Equal)
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
            !searchInfos[Customers.FieldAreaNo].IsNullOrEmpty() && searchInfos[Customers.FieldAreaNo].Length < 6;
        if (areaNoLength)
        {
            condition.ForEach(x =>
            {
                if (x is ConditionalModel { FieldName: Customers.FieldAreaNo } c)
                {
                    c.ConditionalType = ConditionalType.LikeLeft;
                }
            });
        }

        return condition;
    }
}