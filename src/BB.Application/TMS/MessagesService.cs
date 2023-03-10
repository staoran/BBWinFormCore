using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 消息回复 业务逻辑类
/// </summary>
[ApiDescriptionSettings("问题异常")]
public class MessagesService : BaseService<Messages>, IDynamicApiController, ITransient
{
    public MessagesService(BaseRepository<Messages> repository, IValidator<Messages> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<Messages> SetDynamicDefaults(Messages entity)
    {
        return Task.FromResult(entity);
    }

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    public override async Task<Dictionary<string, int>> GetPermitDictAsync()
    {
        var permitDict = new Dictionary<string, int>
        {
            { nameof(Messages.ISID), 3 },
            { nameof(Messages.MsgNo), 1 },
            { nameof(Messages.LastReadTime), 3 },
            { nameof(Messages.LaseRealAccount), 3 },
            { nameof(Messages.CreationDate), 1 },
            { nameof(Messages.CreatedBy), 1 },
            { nameof(Messages.LastUpdateDate), 1 },
            { nameof(Messages.LastUpdateBy), 1 }
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
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.GetOrAdd($"{nameof(Messages)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(Messages.FieldMsgNo, SqlOperator.Like),
                new(Messages.FieldDealStatus, SqlOperator.Like),
                new(Messages.FieldDealContent, SqlOperator.Like),
                new(Messages.FieldAttaPath, SqlOperator.Like),
                new(Messages.FieldCreationDate, SqlOperator.Between),
                new(Messages.FieldCreatedBy, SqlOperator.Equal),
                new(Messages.FieldLastUpdateDate, SqlOperator.Between),
                new(Messages.FieldLastUpdateBy, SqlOperator.Like)
            });
    }
}