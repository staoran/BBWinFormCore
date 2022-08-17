using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 车辆档案 业务逻辑类
/// </summary>
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
            { Car.FieldTranNode, 1 },
            // 主键不为空则只读
            { Car.FieldCarNo, 4 },
            { Car.FieldCreationDate, 1 },
            { Car.FieldCreatedBy, 1 },
            { Car.FieldLastUpdateDate, 1 },
            { Car.FieldLastUpdatedBy, 1 },
            { Car.FieldFlagApp, 1 },
            { Car.FieldAppUser, 1 },
            { Car.FieldAppDate, 1 },
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
    /// 构造查询语句
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override string GetConditionSql(NameValueCollection searchInfos)
    {
        var condition = new SearchCondition();
        condition.AddCondition(Car.FieldTranNode, searchInfos[Car.FieldTranNode], SqlOperator.Equal);
        condition.AddCondition(Car.FieldCarNo, searchInfos[Car.FieldCarNo], SqlOperator.Like);
        condition.AddCondition(Car.FieldProperty, searchInfos[Car.FieldProperty], SqlOperator.Equal);
        condition.AddCondition(Car.FieldModel, searchInfos[Car.FieldModel], SqlOperator.Equal);
        condition.AddCondition(Car.FieldCarType, searchInfos[Car.FieldCarType], SqlOperator.Equal);
        condition.AddCondition(Car.FieldServiceRange, searchInfos[Car.FieldServiceRange], SqlOperator.Equal);
        condition.AddCondition(Car.FieldTrustLevel, searchInfos[Car.FieldTrustLevel], SqlOperator.Equal);
        condition.AddCondition(Car.FieldTargetPlace, searchInfos[Car.FieldTargetPlace], SqlOperator.Like);
        condition.AddCondition(Car.FieldBuyDate, searchInfos[Car.FieldBuyDate], SqlOperator.Between);
        condition.AddCondition(Car.FieldOperationNo, searchInfos[Car.FieldOperationNo], SqlOperator.Like);
        condition.AddCondition(Car.FieldEngineNo, searchInfos[Car.FieldEngineNo], SqlOperator.Like);
        condition.AddCondition(Car.FieldVIN, searchInfos[Car.FieldVIN], SqlOperator.Like);
        condition.AddCondition(Car.FieldTonnage, searchInfos[Car.FieldTonnage], SqlOperator.Like);
        condition.AddCondition(Car.FieldLong, searchInfos[Car.FieldLong], SqlOperator.Between);
        condition.AddCondition(Car.FieldWidth, searchInfos[Car.FieldWidth], SqlOperator.Between);
        condition.AddCondition(Car.FieldHeight, searchInfos[Car.FieldHeight], SqlOperator.Between);
        condition.AddCondition(Car.FieldVolume, searchInfos[Car.FieldVolume], SqlOperator.Between);
        condition.AddCondition(Car.FieldContact, searchInfos[Car.FieldContact], SqlOperator.Like);
        condition.AddCondition(Car.FieldContactTel, searchInfos[Car.FieldContactTel], SqlOperator.Like);
        condition.AddCondition(Car.FieldDriver, searchInfos[Car.FieldDriver], SqlOperator.Like);
        condition.AddCondition(Car.FieldDriverCert, searchInfos[Car.FieldDriverCert], SqlOperator.Like);
        condition.AddCondition(Car.FieldDriverMobile, searchInfos[Car.FieldDriverMobile], SqlOperator.Like);
        condition.AddCondition(Car.FieldDriverTel, searchInfos[Car.FieldDriverTel], SqlOperator.Like);
        condition.AddCondition(Car.FieldDriverHomeAddress, searchInfos[Car.FieldDriverHomeAddress], SqlOperator.Like);
        condition.AddCondition(Car.FieldRemark, searchInfos[Car.FieldRemark], SqlOperator.Like);
        condition.AddCondition(Car.FieldCreationDate, searchInfos[Car.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(Car.FieldCreatedBy, searchInfos[Car.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(Car.FieldLastUpdateDate, searchInfos[Car.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(Car.FieldLastUpdatedBy, searchInfos[Car.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(Car.FieldFlagApp, searchInfos[Car.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(Car.FieldAppUser, searchInfos[Car.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(Car.FieldAppDate, searchInfos[Car.FieldAppDate], SqlOperator.Between);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}