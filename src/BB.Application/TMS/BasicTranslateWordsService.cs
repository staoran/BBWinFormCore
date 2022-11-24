using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Utils;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 公式定义 业务逻辑类
/// </summary>
[ApiDescriptionSettings("线路与报价")]
public class BasicTranslateWordsService : BaseService<BasicTranslateWords>, IDynamicApiController, ITransient
{
    public BasicTranslateWordsService(BaseRepository<BasicTranslateWords> repository, IValidator<BasicTranslateWords> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<BasicTranslateWords> SetDynamicDefaults(BasicTranslateWords entity)
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
            { nameof(BasicTranslateWords.ISID), 3 },
            { nameof(BasicTranslateWords.TranslateTypeDesc), 3 },
            { nameof(BasicTranslateWords.CreationDate), 1 },
            { nameof(BasicTranslateWords.CreatedBy), 1 },
            { nameof(BasicTranslateWords.LastUpdateDate), 1 },
            { nameof(BasicTranslateWords.LastUpdatedBy), 1 },
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
    public override async Task<bool> InsertAsync(BasicTranslateWords obj)
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
    public override async Task<bool> UpdateAsync(BasicTranslateWords obj)
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
        return Cache.Instance.GetOrCreate($"{nameof(BasicTranslateWords)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(BasicTranslateWords.FieldWordsInFront, SqlOperator.Like),
                new(BasicTranslateWords.FieldWordsBehind, SqlOperator.Like),
                new(BasicTranslateWords.FieldTranslateType, SqlOperator.Equal),
                new(BasicTranslateWords.FieldCanSelectYN, SqlOperator.Equal),
                new(BasicTranslateWords.FieldExampleStr, SqlOperator.Like),
                new(BasicTranslateWords.FieldCancelYN, SqlOperator.Equal),
                new(BasicTranslateWords.FieldRemark, SqlOperator.Like),
                new(BasicTranslateWords.FieldCreationDate, SqlOperator.Between),
                new(BasicTranslateWords.FieldCreatedBy, SqlOperator.Equal),
                new(BasicTranslateWords.FieldLastUpdateDate, SqlOperator.Between),
                new(BasicTranslateWords.FieldLastUpdatedBy, SqlOperator.Equal)
            });
    }
}