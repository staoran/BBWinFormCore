using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Encrypt;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 费用调整 业务逻辑类
/// </summary>
public class CostMsgService : BaseMultiService<CostMsg, CostMsgs>, IDynamicApiController, ITransient
{
    public CostMsgService(BaseRepository<CostMsg> repository, BaseRepository<CostMsgs> childRepository, IValidator<CostMsg> validator, IValidator<CostMsgs> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<CostMsg> NewEntityAsync()
    {
        CostMsg entity = await base.NewEntityAsync();
        entity.CostMsgNo = Snowflake.Instance().GetId().ToString();
        entity.SourceType = "1";
        entity.SendMsgNode = LoginUserInfo.CompanyId;
        entity.RecvMsgType = "1";
        entity.ValueType = "1";
        entity.SourceValue = 0;
        entity.ActiveValue = 0;
        entity.StatusID = "0";
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
            { CostMsg.FieldISID, 3 },
            { CostMsg.FieldCostMsgNo, 1 },
            { CostMsg.FieldSourceISID, 3 },
            { CostMsg.FieldWaybillNo, 4 },
            { CostMsg.FieldSendMsgNode, 1 },
            { CostMsg.FieldRecvMsgType, 1 },
            { CostMsg.FieldSourceValue, 1 },
            { CostMsg.FieldStatusID, 1 },
            { CostMsg.FieldCreationDate, 1 },
            { CostMsg.FieldCreatedBy, 1 },
            { CostMsg.FieldLastUpdateDate, 1 },
            { CostMsg.FieldLastUpdatedBy, 1 },
            { CostMsg.FieldFlagApp, 1 },
            { CostMsg.FieldAppUser, 1 },
            { CostMsg.FieldAppDate, 1 },
            { CostMsg.FieldFinancialCenter, 1 },
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
    public override async Task<bool> InsertAsync(CostMsg obj)
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
    public override async Task<bool> UpdateAsync(CostMsg obj)
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

        #region 删除后

        if (!succeed) return false;

        //可添加其他关联操作

        return true;

        #endregion
    }

    /// <summary>
    /// 构造查询语句
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override string GetConditionSql(NameValueCollection searchInfos)
    {
        var condition = new SearchCondition();
        condition.AddCondition(CostMsg.FieldCostMsgNo, searchInfos[CostMsg.FieldCostMsgNo], SqlOperator.Like);
        condition.AddCondition(CostMsg.FieldSourceType, searchInfos[CostMsg.FieldSourceType], SqlOperator.Equal);
        condition.AddCondition(CostMsg.FieldWaybillNo, searchInfos[CostMsg.FieldWaybillNo], SqlOperator.Like);
        condition.AddCondition(CostMsg.FieldSendMsgNode, searchInfos[CostMsg.FieldSendMsgNode], SqlOperator.Equal);
        condition.AddCondition(CostMsg.FieldSendMsgContent, searchInfos[CostMsg.FieldSendMsgContent], SqlOperator.Like);
        condition.AddCondition(CostMsg.FieldAttaPath, searchInfos[CostMsg.FieldAttaPath], SqlOperator.Like);
        condition.AddCondition(CostMsg.FieldRecvMsgType, searchInfos[CostMsg.FieldRecvMsgType], SqlOperator.Equal);
        condition.AddCondition(CostMsg.FieldRecvMsgAccount, searchInfos[CostMsg.FieldRecvMsgAccount], SqlOperator.Equal);
        condition.AddCondition(CostMsg.FieldValueType, searchInfos[CostMsg.FieldValueType], SqlOperator.Equal);
        condition.AddCondition(CostMsg.FieldSourceValue, searchInfos[CostMsg.FieldSourceValue], SqlOperator.Between);
        condition.AddCondition(CostMsg.FieldActiveValue, searchInfos[CostMsg.FieldActiveValue], SqlOperator.Between);
        condition.AddCondition(CostMsg.FieldStatusID, searchInfos[CostMsg.FieldStatusID], SqlOperator.Equal);
        condition.AddCondition(CostMsg.FieldCreationDate, searchInfos[CostMsg.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(CostMsg.FieldCreatedBy, searchInfos[CostMsg.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(CostMsg.FieldLastUpdateDate, searchInfos[CostMsg.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(CostMsg.FieldLastUpdatedBy, searchInfos[CostMsg.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(CostMsg.FieldFlagApp, searchInfos[CostMsg.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(CostMsg.FieldAppUser, searchInfos[CostMsg.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(CostMsg.FieldAppDate, searchInfos[CostMsg.FieldAppDate], SqlOperator.Between);
        condition.AddCondition(CostMsg.FieldFinancialCenter, searchInfos[CostMsg.FieldFinancialCenter], SqlOperator.Equal);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}