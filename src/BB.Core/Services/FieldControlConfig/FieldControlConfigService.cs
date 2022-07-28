using System.Collections.Generic;
using System.Linq;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Tools.Validation;

namespace BB.Core.Services.FieldControlConfig;

public class FieldControlConfigService : BaseService<Entity.Security.FieldControlConfig>, IFieldControlConfigService
{
    public FieldControlConfigService(BaseRepository<Entity.Security.FieldControlConfig> repository) : base(repository)
    {
    }
    
    /// <summary>
    /// 获取数据库的所有表名称
    /// </summary>
    /// <returns></returns>
    public List<string> GetTableNames()
    {
        return Repository.GetTableNames();
    }
                       
    /// <summary>
    /// 获取表的主键
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    public IEnumerable<string> GetTableKeyList(string name)
    {
        return Repository.GetTableKeyList(name);
    }
                       
    /// <summary>
    /// 获取表的自增字段
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    public List<string> GetTableIdentityList(string name)
    {
        return Repository.GetTableIdentityList(name);
    }
                       
    /// <summary>
    /// 获取表的注释
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    public string GetTableComment(string name)
    {
        return Repository.GetTableComment(name);
    }
                       
    /// <summary>
    /// 获取表的控件配置模版
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    public IEnumerable<Entity.Security.FieldControlConfig> GetFieldControlConfigs(string name)
    {
        List<DbColumnInfo> cols = Repository.Db.DbMaintenance.GetColumnInfosByTableName(name);
        List<Entity.Security.FieldControlConfig> list = new();
        bool isSortSet = true;
        cols.ForEach(x =>
        {
            bool isnull = x.IsNullable;
            string des = x.ColumnDescription;
            string n = x.DbColumnName;
            string t = x.DataType;
            int l = x.Length;
            string csharpFieldType = TypeUtil.ChangeToCSharpType(t);
            string csharpFieldFullType = TypeUtil.ChangeToCSharFullType(t);
            string controlType = csharpFieldType switch
            {
                "DateTime" => "DateEdit",
                "decimal" => "SpinEdit",
                "bool" => "ToggleSwitch",
                _ => "TextEdit"
            };
            if (controlType == "TextEdit")
            {
                controlType = n switch
                {
                    "CreatedBy" => "ComboBoxEdit",
                    "AppUser" => "ComboBoxEdit",
                    "LastUpdatedBy" => "ComboBoxEdit",
                    "OrgCode" => "ComboBoxEdit",
                    "TranNode" => "ComboBoxEdit",
                    _ => "TextEdit"
                };

                if (n.Contains("Type"))
                {
                    controlType = "ComboBoxEdit";
                }

                if (l > 100)
                {
                    controlType = "MemoEdit";
                }

                if (n.Contains("AreaNo") || n.Contains("AreaId"))
                {
                    controlType = "SearchLookUpEdit";
                }

                if (n.Contains("ProvinceNo") || n.Contains("ProvinceId") || n.Contains("CityNo") ||
                    n.Contains("CityId"))
                {
                    controlType = "ComboBoxEdit";
                }
            }

            string defaultValue = x.DefaultValue;
            if (defaultValue.IsNullOrEmpty())
            {
                if (controlType == "ToggleSwitch")
                {
                    defaultValue = "false";
                }
                else
                {
                    defaultValue = n switch
                    {
                        "CreatedBy" => "LoginUserInfo.ID.ToString()",
                        "LastUpdatedBy" => "LoginUserInfo.ID.ToString()",
                        "OrgCode" => "LoginUserInfo.CompanyId",
                        "TranNode" => "LoginUserInfo.CompanyId",
                        "CreationDate" => "DateTime.Now",
                        "LastUpdateDate" => "DateTime.Now",
                        "FlagApp" => "false",
                        _ => ""
                    };
                }
            }

            string dataSource = n switch
            {
                "CreatedBy" => "GB.AllUserDict",
                "AppUser" => "GB.AllUserDict",
                "LastUpdatedBy" => "GB.AllUserDict",
                "OrgCode" => "GB.AllOuDict",
                "TranNode" => "GB.AllOuDict",
                "FlagApp" => "\"已审核,未审核\"",
                _ => ""
            };
            bool isCheckNull = n switch
            {
                "CreatedBy" => true,
                "LastUpdatedBy" => true,
                "CreationDate" => true,
                "LastUpdateDate" => true,
                "OrgCode" => true,
                "TranNode" => true,
                _ => !isnull
            };
            bool isReadOnly = n switch
            {
                "CreatedBy" => true,
                "AppUser" => true,
                "LastUpdatedBy" => true,
                "OrgCode" => true,
                "TranNode" => true,
                "FlagApp" => true,
                "CreationDate" => true,
                "LastUpdateDate" => true,
                "AppDate" => true,
                _ => false
            };
            bool isLock = n switch
            {
                "LastUpdateDate" => true,
                _ => false
            };
            string isSort = n switch
            {
                "isid" => "desc",
                "ISID" => "desc",
                "CreationDate" => "desc",
                _ => ""
            };
            bool isVisible = !(n.Contains("ProvinceNo") || n.Contains("ProvinceId") || n.Contains("CityNo") ||
                               n.Contains("CityId"));
            bool isEdit = !(n.Contains("ProvinceNo") || n.Contains("ProvinceId") || n.Contains("CityNo") ||
                            n.Contains("CityId"));
            bool isSearch = !(n.Contains("ProvinceNo") || n.Contains("ProvinceId") || n.Contains("CityNo") ||
                              n.Contains("CityId"));
            bool isAdvSearch = !(n.Contains("ProvinceNo") || n.Contains("ProvinceId") || n.Contains("CityNo") ||
                                 n.Contains("CityId"));
            string tableComment = GetTableComment(x.TableName);
            var info = new Entity.Security.FieldControlConfig()
            {
                TableName = x.TableName,
                TableDes = tableComment.IsNullOrEmpty() ? x.TableName : tableComment,
                DataBaseFieldName = n,
                DataBaseFieldType = t,
                DataBaseFieldLong = l,
                CSharpFieldLong = csharpFieldType == "string" ? l : 0,
                CSharpFieldType = csharpFieldType + (isnull && csharpFieldType != "string" && csharpFieldType != "bool" && n != "CreationDate" && n != "LastUpdateDate" && n != "CreationDate" && n != "LastUpdateDate" ? "?" : ""),
                CSharpFieldFullType = csharpFieldFullType,
                CSharpFieldName = n,
                CSharpFieldDes = des.IsNullOrEmpty() ? n : des,
                ControlName = $"txt{n}",
                DataBaseFieldDes = des,
                ControlLabelName = des.IsNullOrEmpty() ? n : des,
                ControlType = controlType,
                IsNull = isnull,
                IsKey = x.IsPrimarykey,
                IsIdentity = x.IsIdentity,
                IsVisible = isVisible,
                IsEdit = isEdit,
                IsSearch = isSearch,
                IsAdvSearch = isAdvSearch,
                IsCheckLong = csharpFieldType == "string" && l > 0,
                IsCheckNull = isCheckNull,
                IsReadonly = isReadOnly,
                Defaults = defaultValue,
                DataTableName = dataSource,
                Sort = list.Count,
                OrderBy = isSortSet ? isSort : "",
                OptimisticLock = isLock
            };
            isSortSet = isSort.IsNullOrEmpty();

            list.Add(info);
        });
        return list;
    }

    /// <summary>
    /// 获取数据库的全部表名称和注释
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetTableNamesAndComments()
    {
        return Repository.Db.DbMaintenance.GetTableInfoList()
            .OrderBy(x=>x.Name)
            .Select(x => x.Name + "|" + (x.Description.IsNullOrEmpty() ? x.Name : x.Description))
            .ToList();
    }
}