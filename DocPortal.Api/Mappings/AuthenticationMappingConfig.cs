using DocPortal.Application.Common.Authentication;
using DocPortal.Contracts.Endpoints.Auth;

using Mapster;

namespace DocPortal.Api.Mappings;

internal class AuthenticationMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<RegisterRequest, RegisterDetails>()
      .Map(dest => dest.User.Login, src => src.Login)
      .Map(dest => dest.User.Role, src => src.Role);

    config.NewConfig<RegisterDetails, RegisterResponce>()
      .Map(dest => dest.Login, src => src.User.Login)
      .Map(dest => dest.Role, src => src.User.Role);

    config.NewConfig<LoginRequest, LoginDetails>();
    config.NewConfig<AccessToken, LoginResponce>()
      .Map(dest => dest.Login, src => src.User.Login)
      .Map(dest => dest.Role, src => src.User.Role)
      .Map(dest => dest.UserOrganizations, src => src.User.UserOrganizations);
  }
}
