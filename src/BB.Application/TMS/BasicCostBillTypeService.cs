using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 预付金操作类型 业务逻辑类
/// </summary>
public class BasicCostBillTypeService : BaseService<BasicCostBillType>, IDynamicApiController, ITransient
{
    public BasicCostBillTypeService(BaseRepository<BasicCostBillType> repository, IValidator<BasicCostBillType> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<BasicCostBillType> NewEntityAsync()
    {
        BasicCostBillType entity = await base.NewEntityAsync();
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.CreationDate = DateTime.Now;
        entity.FlagApp = false;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
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
            { BasicCostBillType.FieldISID, 3 },
            // 主键不为空则只读
            { BasicCostBillType.FieldCostType, 4 },
            { BasicCostBillType.FieldCreatedBy, 1 },
            { BasicCostBillType.FieldCreationDate, 1 },
            { BasicCostBillType.FieldFlagApp, 1 },
            { BasicCostBillType.FieldAppUser, 1 },
            { BasicCostBillType.FieldAppDate, 1 },
            { BasicCostBillType.FieldLastUpdatedBy, 1 },
            { BasicCostBillType.FieldLastUpdateDate, 1 },
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
    public override async Task<bool> InsertAsync(BasicCostBillType obj)
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
    public override async Task<bool> UpdateAsync(BasicCostBillType obj)
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
        condition.AddCondition(BasicCostBillType.FieldCostType, searchInfos[BasicCostBillType.FieldCostType], SqlOperator.Like);
        condition.AddCondition(BasicCostBillType.FieldCostDesc, searchInfos[BasicCostBillType.FieldCostDesc], SqlOperator.Like);
        condition.AddCondition(BasicCostBillType.FieldCtrl, searchInfos[BasicCostBillType.FieldCtrl], SqlOperator.Like);
        condition.AddCondition(BasicCostBillType.FieldRemark, searchInfos[BasicCostBillType.FieldRemark], SqlOperator.Like);
        condition.AddCondition(BasicCostBillType.FieldUseType, searchInfos[BasicCostBillType.FieldUseType], SqlOperator.Equal);
        condition.AddCondition(BasicCostBillType.FieldCreatedBy, searchInfos[BasicCostBillType.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(BasicCostBillType.FieldCreationDate, searchInfos[BasicCostBillType.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(BasicCostBillType.FieldFlagApp, searchInfos[BasicCostBillType.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(BasicCostBillType.FieldAppUser, searchInfos[BasicCostBillType.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(BasicCostBillType.FieldAppDate, searchInfos[BasicCostBillType.FieldAppDate], SqlOperator.Between);
        condition.AddCondition(BasicCostBillType.FieldLastUpdatedBy, searchInfos[BasicCostBillType.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(BasicCostBillType.FieldLastUpdateDate, searchInfos[BasicCostBillType.FieldLastUpdateDate], SqlOperator.Between);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}