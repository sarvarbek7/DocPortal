using DocPortal.Contracts.Dtos;
using DocPortal.Domain.Entities;

using Mapster;

namespace DocPortal.Api.Mappings
{
  internal class UserOrganizationMappingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<UserOrganization, UserOrganizationDto>()
        .Map(dest => dest.OrganizationTitle, src => src.AssignedOrganization.Title);

      config.NewConfig<UserOrganizationDto, UserOrganization>();
    }
  }
}
