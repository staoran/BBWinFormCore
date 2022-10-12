using System.Security.Claims;
using BB.Entity.Base;

namespace BB.Core.Services.Base;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<ClaimsPrincipal, LoginUserInfo>()
            .Map(dest => dest.ID, src => src.FindFirstValue("ID"))
            .Map(dest => dest.DeptId, src => src.FindFirstValue("DeptId"))
            .Map(dest => dest.DeptName, src => src.FindFirstValue("DeptName"))
            .Map(dest => dest.CompanyId, src => src.FindFirstValue("CompanyId"))
            .Map(dest => dest.CompanyName, src => src.FindFirstValue("CompanyName"))
            // .Map(dest => dest.RoleNameList, src => src.FindFirstValue("RoleNameList"))
            // .Map(dest => dest.RoleIdList, src => src.FindFirstValue("RoleIdList"))
            .Map(dest => dest.Name, src => src.FindFirstValue("Name"))
            .Map(dest => dest.FullName, src => src.FindFirstValue("FullName"))
            .Map(dest => dest.Gender, src => src.FindFirstValue("Gender"))
            .Map(dest => dest.Email, src => src.FindFirstValue("Email"))
            .Map(dest => dest.IsAdmin, src => src.FindFirstValue("IsAdmin"))
            .Map(dest => dest.Data1, src => src.FindFirstValue("Data1"))
            .Map(dest => dest.Data2, src => src.FindFirstValue("Data2"))
            .Map(dest => dest.Data3, src => src.FindFirstValue("Data3"));
    }
}