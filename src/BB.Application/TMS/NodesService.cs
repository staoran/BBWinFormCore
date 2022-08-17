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
/// 网点区域 业务逻辑类
/// </summary>
public class NodesService : BaseService<Nodes>, IDynamicApiController, ITransient
{
    public NodesService(BaseRepository<Nodes> repository, IValidator<Nodes> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Nodes> NewEntityAsync()
    {
        Nodes entity = await base.NewEntityAsync();
        entity.DeliveryType = "01";
        entity.ConvertVK = 250;
        entity.CancelYN = false;
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
            { Nodes.FieldISID, 3 },
            { Nodes.FieldTranNodeNO, 3 },
            { Nodes.FieldProvinceId, 3 },
            { Nodes.FieldCityId, 3 },
            { Nodes.FieldTownId, 3 },
            { Nodes.FieldCreationDate, 3 },
            { Nodes.FieldCreatedBy, 3 },
            { Nodes.FieldLastUpdateDate, 3 },
            { Nodes.FieldLastUpdatedBy, 3 },
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
    /// 构造查询语句
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override string GetConditionSql(NameValueCollection searchInfos)
    {
        var condition = new SearchCondition();
        condition.AddCondition(Nodes.FieldTranNodeAreaName, searchInfos[Nodes.FieldTranNodeAreaName], SqlOperator.Like);
        condition.AddCondition(Nodes.FieldTranNodeAreaDesc, searchInfos[Nodes.FieldTranNodeAreaDesc], SqlOperator.Like);
        condition.AddCondition(Nodes.FieldDeliveryType, searchInfos[Nodes.FieldDeliveryType], SqlOperator.Equal);
        condition.AddCondition(Nodes.FieldEFence, searchInfos[Nodes.FieldEFence], SqlOperator.Like);
        condition.AddCondition(Nodes.FieldCenterCoordinate, searchInfos[Nodes.FieldCenterCoordinate], SqlOperator.Like);
        condition.AddCondition(Nodes.FieldConvertVK, searchInfos[Nodes.FieldConvertVK], SqlOperator.Between);
        bool areaNoLength = !searchInfos[Nodes.FieldAreaId].IsNullOrEmpty() && searchInfos[Nodes.FieldAreaId].Length < 6;
        condition.AddCondition(Nodes.FieldAreaId, searchInfos[Nodes.FieldAreaId],
            areaNoLength ? SqlOperator.LikeStartAt : SqlOperator.Equal);
        condition.AddCondition(Nodes.FieldAddress, searchInfos[Nodes.FieldAddress], SqlOperator.Like);
        condition.AddCondition(Nodes.FieldPerson, searchInfos[Nodes.FieldPerson], SqlOperator.Like);
        condition.AddCondition(Nodes.FieldPhone, searchInfos[Nodes.FieldPhone], SqlOperator.Like);
        condition.AddCondition(Nodes.FieldSignLimitHour, searchInfos[Nodes.FieldSignLimitHour], SqlOperator.Between);
        condition.AddCondition(Nodes.FieldCancelYN, searchInfos[Nodes.FieldCancelYN], SqlOperator.Equal);
        condition.AddCondition(Nodes.FieldRemark, searchInfos[Nodes.FieldRemark], SqlOperator.Like);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}