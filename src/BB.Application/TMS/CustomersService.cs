using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 客户收货人 业务逻辑类
/// </summary>
public class CustomersService : BaseService<Customers>, IDynamicApiController, ITransient
{
    public CustomersService(BaseRepository<Customers> repository, IValidator<Customers> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Customers> NewEntityAsync()
    {
        Customers entity = await base.NewEntityAsync();
        entity.TranNode = LoginUserInfo.CompanyId;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
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
            { Customers.FieldISID, 3 },
            { Customers.FieldCustomerCode, 3 },
            { Customers.FieldTranNode, 3 },
            { Customers.FieldProvinceNo, 3 },
            { Customers.FieldCityNo, 3 },
            { Customers.FieldCreationDate, 3 },
            { Customers.FieldCreatedBy, 3 },
            { Customers.FieldLastUpdateDate, 3 },
            { Customers.FieldLastUpdatedBy, 3 }
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
    /// 构造查询语句
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override string GetConditionSql(NameValueCollection searchInfos)
    {
        var condition = new SearchCondition();
        condition.AddCondition(Customers.FieldISID, searchInfos[Customers.FieldISID], SqlOperator.Like);
        condition.AddCondition(Customers.FieldCustomerCode, searchInfos[Customers.FieldCustomerCode], SqlOperator.Like);
        condition.AddCondition(Customers.FieldTranNode, searchInfos[Customers.FieldTranNode], SqlOperator.Equal);
        condition.AddCondition(Customers.FieldContactPerson, searchInfos[Customers.FieldContactPerson], SqlOperator.Like);
        condition.AddCondition(Customers.FieldNativeName, searchInfos[Customers.FieldNativeName], SqlOperator.Like);
        condition.AddCondition(Customers.FieldAddress, searchInfos[Customers.FieldAddress], SqlOperator.Like);
        bool areaNoLength = !searchInfos[Customers.FieldAreaNo].IsNullOrEmpty() && searchInfos[Customers.FieldAreaNo].Length < 6;
        condition.AddCondition(Customers.FieldAreaNo, searchInfos[Customers.FieldAreaNo],
            areaNoLength ? SqlOperator.LikeStartAt : SqlOperator.Equal);
        condition.AddCondition(Customers.FieldTel, searchInfos[Customers.FieldTel], SqlOperator.Like);
        condition.AddCondition(Customers.FieldMobile, searchInfos[Customers.FieldMobile], SqlOperator.Like);
        condition.AddCondition(Customers.FieldInsuranceRate, searchInfos[Customers.FieldInsuranceRate], SqlOperator.Between);
        condition.AddCondition(Customers.FieldCoordinate, searchInfos[Customers.FieldCoordinate], SqlOperator.Like);
        condition.AddCondition(Customers.FieldDefaultToNode, searchInfos[Customers.FieldDefaultToNode], SqlOperator.Equal);
        condition.AddCondition(Customers.FieldDefaultToNodes, searchInfos[Customers.FieldDefaultToNodes], SqlOperator.Equal);
        condition.AddCondition(Customers.FieldDefaultToNodesName, searchInfos[Customers.FieldDefaultToNodesName], SqlOperator.Like);
        condition.AddCondition(Customers.FieldCargoName, searchInfos[Customers.FieldCargoName], SqlOperator.Like);
        condition.AddCondition(Customers.FieldPackageType, searchInfos[Customers.FieldPackageType], SqlOperator.Equal);
        condition.AddCondition(Customers.FieldCargoUnit, searchInfos[Customers.FieldCargoUnit], SqlOperator.Like);
        condition.AddCondition(Customers.FieldPrice, searchInfos[Customers.FieldPrice], SqlOperator.Between);
        condition.AddCondition(Customers.FieldPriceType, searchInfos[Customers.FieldPriceType], SqlOperator.Equal);
        condition.AddCondition(Customers.FieldCreationDate, searchInfos[Customers.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(Customers.FieldCreatedBy, searchInfos[Customers.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(Customers.FieldLastUpdateDate, searchInfos[Customers.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(Customers.FieldLastUpdatedBy, searchInfos[Customers.FieldLastUpdatedBy], SqlOperator.Equal);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}