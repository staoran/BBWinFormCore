using BB.Core.Services.Region;
using BB.Entity.Security;
using BB.Entity.TMS;
using Microsoft.Extensions.DependencyInjection;

namespace BB.Application.TMS.Dtos;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Node, OUInfo>()
            .BeforeMapping((s, d) =>
            {
                using var scope = App.RootServices.CreateScope();
                var regionService = scope.ServiceProvider.GetRequiredService<RegionService>();
                d.Address = regionService.GetAllDistrictAsync().Result
                    .Find(x => x.Id.ToString() == s.AreaNo).FullName + s.TranNodeAddress;
            })
            .Ignore(d => d.Address)
            .Map(d => d.PID, s => s.ParentNo)
            .Map(d => d.HandNo, s => s.TranNodeNO)
            .Map(d => d.SortCode, s => s.TranNodeNO)
            .Map(d => d.Name, s => s.TranNodeName)
            .Map(d => d.OuterPhone, s => s.TranNodeMobile)
            .Map(d => d.Note, s => s.Remark)
            .Map(d => d.Creator, s => s.CreatedBy)
            .Map(d => d.CreatedBy, s => s.CreatedBy)
            .Map(d => d.Editor, s => s.CreatedBy)
            .Map(d => d.Address, s => s.TranNodeAddress)
            .Map(d => d.CompanyId, s => s.CompanyNo)
            .Map(d => d.Enabled, s => s.TranNodeStatus == "1")
            .Map(d => d.Deleted, s => s.TranNodeStatus == "9")
            .Map(d => d.Category, s => s.TranNodeType);
    }
}