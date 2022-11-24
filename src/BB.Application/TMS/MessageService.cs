using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Encrypt;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Utils;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 消息记录 业务逻辑类
/// </summary>
[ApiDescriptionSettings("问题异常")]
public class MessageService : BaseMultiService<Message, Messages>, IDynamicApiController, ITransient
{
    public MessageService(BaseRepository<Message> repository, BaseRepository<Messages> childRepository, IValidator<Message> validator, IValidator<Messages> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<Message> SetDynamicDefaults(Message entity)
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
            { nameof(Message.ISID), 3 },
            // 主键不为空则只读
            { nameof(Message.MsgNo), 4 },
            { nameof(Message.RecvMsgNode), 4 },
            { nameof(Message.SendMsgNode), 1 },
            { nameof(Message.DealStatus), 1 },
            { nameof(Message.CreationDate), 1 },
            { nameof(Message.CreatedBy), 1 },
            { nameof(Message.LastUpdateDate), 1 },
            { nameof(Message.LastUpdatedBy), 1 },
            { nameof(Message.FlagApp), 1 },
            { nameof(Message.AppUser), 1 },
            { nameof(Message.AppDate), 1 },
        };
        // 后端权限控制
        // var permitDict = BllFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Test1Car).FullName, LoginUserInfo.ID.ToInt32());
        return await Task.FromResult(permitDict);
    }

    /// <summary>
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.Instance.GetOrCreate($"{nameof(Message)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(Message.FieldMsgNo, SqlOperator.Like),
                new(Message.FieldMsgType, SqlOperator.Equal),
                new(Message.FieldWaybillNo, SqlOperator.Like),
                new(Message.FieldSendMsgNode, SqlOperator.Equal),
                new(Message.FieldSendMsgContent, SqlOperator.Like),
                new(Message.FieldRecvMsgNode, SqlOperator.Equal),
                new(Message.FieldDealStatus, SqlOperator.Equal),
                new(Message.FieldAttaPath, SqlOperator.Like),
                new(Message.FieldCreationDate, SqlOperator.Between),
                new(Message.FieldCreatedBy, SqlOperator.Equal),
                new(Message.FieldLastUpdateDate, SqlOperator.Between),
                new(Message.FieldLastUpdatedBy, SqlOperator.Equal),
                new(Message.FieldFlagApp, SqlOperator.Equal),
                new(Message.FieldAppUser, SqlOperator.Equal),
                new(Message.FieldAppDate, SqlOperator.Between)
            });
    }
}