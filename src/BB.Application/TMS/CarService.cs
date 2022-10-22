using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Utils;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 车辆档案 业务逻辑类
/// </summary>
[ApiDescriptionSettings("运输基础资料")]
public class CarService : BaseService<Car>, IDynamicApiController, ITransient
{
    public CarService(BaseRepository<Car> repository, IValidator<Car> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Car> NewEntityAsync()
    {
        Car entity = await base.NewEntityAsync();
        entity.TranNode = LoginUserInfo.CompanyId;
        entity.Tonnage = "0";
        entity.Long = 0;
        entity.Width = 0;
        entity.Height = 0;
        entity.Volume = 0;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
        entity.FlagApp = false;
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
            { nameof(Car.TranNode), 1 },
            // 主键不为空则只读
            { nameof(Car.CarNo), 4 },
            { nameof(Car.CreationDate), 1 },
            { nameof(Car.CreatedBy), 1 },
            { nameof(Car.LastUpdateDate), 1 },
            { nameof(Car.LastUpdatedBy), 1 },
            { nameof(Car.FlagApp), 1 },
            { nameof(Car.AppUser), 1 },
            { nameof(Car.AppDate), 1 },
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
    public override async Task<bool> InsertAsync(Car obj)
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
    public override async Task<bool> UpdateAsync(Car obj)
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
        return Cache.Instance.GetOrCreate($"{nameof(Car)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(Car.FieldTranNode, SqlOperator.Equal),
                new(Car.FieldCarNo, SqlOperator.Like),
                new(Car.FieldProperty, SqlOperator.Equal),
                new(Car.FieldModel, SqlOperator.Equal),
                new(Car.FieldCarType, SqlOperator.Equal),
                new(Car.FieldServiceRange, SqlOperator.Equal),
                new(Car.FieldTrustLevel, SqlOperator.Equal),
                new(Car.FieldTargetPlace, SqlOperator.Like),
                new(Car.FieldBuyDate, SqlOperator.Between),
                new(Car.FieldOperationNo, SqlOperator.Like),
                new(Car.FieldEngineNo, SqlOperator.Like),
                new(Car.FieldVIN, SqlOperator.Like),
                new(Car.FieldTonnage, SqlOperator.Like),
                new(Car.FieldLong, SqlOperator.Between),
                new(Car.FieldWidth, SqlOperator.Between),
                new(Car.FieldHeight, SqlOperator.Between),
                new(Car.FieldVolume, SqlOperator.Between),
                new(Car.FieldContact, SqlOperator.Like),
                new(Car.FieldContactTel, SqlOperator.Like),
                new(Car.FieldDriver, SqlOperator.Like),
                new(Car.FieldDriverCert, SqlOperator.Like),
                new(Car.FieldDriverMobile, SqlOperator.Like),
                new(Car.FieldDriverTel, SqlOperator.Like),
                new(Car.FieldDriverHomeAddress, SqlOperator.Like),
                new(Car.FieldRemark, SqlOperator.Like),
                new(Car.FieldCreationDate, SqlOperator.Between),
                new(Car.FieldCreatedBy, SqlOperator.Equal),
                new(Car.FieldLastUpdateDate, SqlOperator.Between),
                new(Car.FieldLastUpdatedBy, SqlOperator.Equal),
                new(Car.FieldFlagApp, SqlOperator.Equal),
                new(Car.FieldAppUser, SqlOperator.Equal),
                new(Car.FieldAppDate, SqlOperator.Between)
            });
    }
}