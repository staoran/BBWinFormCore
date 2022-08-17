using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 区域分组 业务逻辑类
/// </summary>
public class BasicGroupListService : BaseService<BasicGroupList>, IDynamicApiController, ITransient
{
    public BasicGroupListService(BaseRepository<BasicGroupList> repository, IValidator<BasicGroupList> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<BasicGroupList> NewEntityAsync()
    {
        BasicGroupList entity = await base.NewEntityAsync();
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
    /// <param name="isNew"></param>
    /// <returns></returns>
    public override async Task<Dictionary<string, int>> GetPermitDictAsync()
    {
        var permitDict = new Dictionary<string, int>
        {
            // 主键不为空则只读
            { BasicGroupList.FieldISID, 4 },
            { BasicGroupList.FieldGroupName, 4 },
            { BasicGroupList.FieldCreationDate, 1 },
            { BasicGroupList.FieldCreatedBy, 1 },
            { BasicGroupList.FieldLastUpdateDate, 1 },
            { BasicGroupList.FieldLastUpdatedBy, 1 },
            { BasicGroupList.FieldFlagApp, 1 },
            { BasicGroupList.FieldAppUser, 1 },
            { BasicGroupList.FieldAppDate, 1 }
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
    /// 构造查询语句
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override string GetConditionSql(NameValueCollection searchInfos)
    {
        var condition = new SearchCondition();
        condition.AddCondition(BasicGroupList.FieldGroupName, searchInfos[BasicGroupList.FieldGroupName], SqlOperator.Like);
        condition.AddCondition(BasicGroupList.FieldGroupType, searchInfos[BasicGroupList.FieldGroupType], SqlOperator.Equal);
        condition.AddCondition(BasicGroupList.FieldCostType, searchInfos[BasicGroupList.FieldCostType], SqlOperator.Equal);
        condition.AddCondition(BasicGroupList.FieldGroupContent, searchInfos[BasicGroupList.FieldGroupContent], SqlOperator.Like);
        condition.AddCondition(BasicGroupList.FieldGroupExceptContent, searchInfos[BasicGroupList.FieldGroupExceptContent], SqlOperator.Like);
        condition.AddCondition(BasicGroupList.FieldRemark, searchInfos[BasicGroupList.FieldRemark], SqlOperator.Like);
        condition.AddCondition(BasicGroupList.FieldCreationDate, searchInfos[BasicGroupList.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(BasicGroupList.FieldCreatedBy, searchInfos[BasicGroupList.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(BasicGroupList.FieldLastUpdateDate, searchInfos[BasicGroupList.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(BasicGroupList.FieldLastUpdatedBy, searchInfos[BasicGroupList.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(BasicGroupList.FieldFlagApp, searchInfos[BasicGroupList.FieldFlagApp], SqlOperator.Like);
        condition.AddCondition(BasicGroupList.FieldAppUser, searchInfos[BasicGroupList.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(BasicGroupList.FieldAppDate, searchInfos[BasicGroupList.FieldAppDate], SqlOperator.Between);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}