using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 消息回复 业务逻辑类
/// </summary>
public class MessagesService : BaseService<Messages>, IDynamicApiController, ITransient
{
    public MessagesService(BaseRepository<Messages> repository, IValidator<Messages> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Messages> NewEntityAsync()
    {
        Messages entity = await base.NewEntityAsync();
        entity.DealStatus = "8";
        entity.LastReadTime = DateTime.Now;
        entity.LaseRealAccount = LoginUserInfo.CompanyId;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdateBy = LoginUserInfo.ID.ToString();
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
            { Messages.FieldISID, 3 },
            { Messages.FieldMsgNo, 1 },
            { Messages.FieldLastReadTime, 3 },
            { Messages.FieldLaseRealAccount, 3 },
            { Messages.FieldCreationDate, 1 },
            { Messages.FieldCreatedBy, 1 },
            { Messages.FieldLastUpdateDate, 1 },
            { Messages.FieldLastUpdateBy, 1 }
        };
        // 后端权限控制
        // var permitDict = BllFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Test1Car).FullName, LoginUserInfo.ID.ToInt32());
        return await Task.FromResult(permitDict);
    }

    /// <summary>
    /// 更新一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> UpdateAsync(Messages obj)
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
        condition.AddCondition(Messages.FieldMsgNo, searchInfos[Messages.FieldMsgNo], SqlOperator.Like);
        condition.AddCondition(Messages.FieldDealStatus, searchInfos[Messages.FieldDealStatus], SqlOperator.Like);
        condition.AddCondition(Messages.FieldDealContent, searchInfos[Messages.FieldDealContent], SqlOperator.Like);
        condition.AddCondition(Messages.FieldAttaPath, searchInfos[Messages.FieldAttaPath], SqlOperator.Like);
        condition.AddCondition(Messages.FieldCreationDate, searchInfos[Messages.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(Messages.FieldCreatedBy, searchInfos[Messages.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(Messages.FieldLastUpdateDate, searchInfos[Messages.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(Messages.FieldLastUpdateBy, searchInfos[Messages.FieldLastUpdateBy], SqlOperator.Like);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}