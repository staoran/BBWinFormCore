using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 费用类型 业务逻辑类
/// </summary>
public class BasicCostTypeService : BaseService<BasicCostType>, IDynamicApiController, ITransient
{
    public BasicCostTypeService(BaseRepository<BasicCostType> repository, IValidator<BasicCostType> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<BasicCostType> NewEntityAsync()
    {
        BasicCostType entity = await base.NewEntityAsync();
        entity.UseYN = false;
        entity.CostYN = false;
        entity.FlagApp = false;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.CreationDate = DateTime.Now;
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
            // 主键不为空则只读
            { BasicCostType.FieldCostType, 4 },
            { BasicCostType.FieldCostTypeDesc, 4 },
            { BasicCostType.FieldFlagApp, 1 },
            { BasicCostType.FieldAppUser, 1 },
            { BasicCostType.FieldAppDate, 1 },
            { BasicCostType.FieldCreatedBy, 1 },
            { BasicCostType.FieldCreationDate, 1 },
            { BasicCostType.FieldLastUpdatedBy, 1 },
            { BasicCostType.FieldLastUpdateDate, 1 }
        };
        // 主键不为空则只读
        permitDict.TryAdd(BasicCostType.PrimaryKey, 4);
        // 后端权限控制
        // var permitDict = BllFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Test1Car).FullName, LoginUserInfo.ID.ToInt32());
        return await Task.FromResult(permitDict);
    }

    /// <summary>
    /// 新增一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public override async Task<bool> InsertAsync(BasicCostType obj)
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
    public override async Task<bool> UpdateAsync(BasicCostType obj)
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
        condition.AddCondition(BasicCostType.FieldCostType, searchInfos[BasicCostType.FieldCostType], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldCostTypeDesc, searchInfos[BasicCostType.FieldCostTypeDesc], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldCtrl, searchInfos[BasicCostType.FieldCtrl], SqlOperator.Between);
        condition.AddCondition(BasicCostType.FieldUseYN, searchInfos[BasicCostType.FieldUseYN], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldUseType, searchInfos[BasicCostType.FieldUseType], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldPayNodeType, searchInfos[BasicCostType.FieldPayNodeType], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldRecvNodeType, searchInfos[BasicCostType.FieldRecvNodeType], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldPayPostType, searchInfos[BasicCostType.FieldPayPostType], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldRecvPostType, searchInfos[BasicCostType.FieldRecvPostType], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldCostYN, searchInfos[BasicCostType.FieldCostYN], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldRemark, searchInfos[BasicCostType.FieldRemark], SqlOperator.Like);
        condition.AddCondition(BasicCostType.FieldFlagApp, searchInfos[BasicCostType.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldAppUser, searchInfos[BasicCostType.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldAppDate, searchInfos[BasicCostType.FieldAppDate], SqlOperator.Between);
        condition.AddCondition(BasicCostType.FieldCreatedBy, searchInfos[BasicCostType.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldCreationDate, searchInfos[BasicCostType.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(BasicCostType.FieldLastUpdatedBy, searchInfos[BasicCostType.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(BasicCostType.FieldLastUpdateDate, searchInfos[BasicCostType.FieldLastUpdateDate], SqlOperator.Between);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}