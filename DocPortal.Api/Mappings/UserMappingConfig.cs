using DocPortal.Contracts.Dtos;
using DocPortal.Domain.Entities;

using Mapster;

namespace DocPortal.Api.Mappings
{
  internal class UserMappingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<User, UserDto>();
      config.NewConfig<UserDto, User>();
    }
  }
}
