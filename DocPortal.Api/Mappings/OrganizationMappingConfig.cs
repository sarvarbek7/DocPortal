using DocPortal.Contracts.Dtos;
using DocPortal.Domain.Entities;

using Mapster;

namespace DocPortal.Api.Mappings
{
  internal class OrganizationMappingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<Organization, OrganizationDto>();

      config.NewConfig<OrganizationDto, Organization>()
        .Ignore(org => org.PrimaryOrganization);
    }
  }
}
