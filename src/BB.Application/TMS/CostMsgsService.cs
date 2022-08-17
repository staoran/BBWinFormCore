using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 费用调整确认 业务逻辑类
/// </summary>
public class CostMsgsService : BaseService<CostMsgs>, IDynamicApiController, ITransient
{
    public CostMsgsService(BaseRepository<CostMsgs> repository, IValidator<CostMsgs> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<CostMsgs> NewEntityAsync()
    {
        CostMsgs entity = await base.NewEntityAsync();
        entity.StatusID = "1";
        entity.RecvMsgNode = LoginUserInfo.CompanyId;
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
            { CostMsgs.FieldISID, 3 },
            { CostMsgs.FieldCostMsgNo, 1 },
            { CostMsgs.FieldStatusID, 4 },
            { CostMsgs.FieldRecvMsgNode, 1 },
            { CostMsgs.FieldCreationDate, 1 },
            { CostMsgs.FieldCreatedBy, 1 },
            { CostMsgs.FieldLastUpdateDate, 1 },
            { CostMsgs.FieldLastUpdatedBy, 1 },
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
    public override async Task<bool> InsertAsync(CostMsgs obj)
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
    public override async Task<bool> UpdateAsync(CostMsgs obj)
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
        condition.AddCondition(CostMsgs.FieldCostMsgNo, searchInfos[CostMsgs.FieldCostMsgNo], SqlOperator.Like);
        condition.AddCondition(CostMsgs.FieldStatusID, searchInfos[CostMsgs.FieldStatusID], SqlOperator.Equal);
        condition.AddCondition(CostMsgs.FieldRecvMsgNode, searchInfos[CostMsgs.FieldRecvMsgNode], SqlOperator.Equal);
        condition.AddCondition(CostMsgs.FieldRecvMsgContent, searchInfos[CostMsgs.FieldRecvMsgContent], SqlOperator.Like);
        condition.AddCondition(CostMsgs.FieldAttaPath, searchInfos[CostMsgs.FieldAttaPath], SqlOperator.Like);
        condition.AddCondition(CostMsgs.FieldCreationDate, searchInfos[CostMsgs.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(CostMsgs.FieldCreatedBy, searchInfos[CostMsgs.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(CostMsgs.FieldLastUpdateDate, searchInfos[CostMsgs.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(CostMsgs.FieldLastUpdatedBy, searchInfos[CostMsgs.FieldLastUpdatedBy], SqlOperator.Equal);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}