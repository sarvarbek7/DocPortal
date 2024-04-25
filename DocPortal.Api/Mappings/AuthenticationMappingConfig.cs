using DocPortal.Application.Common.Authentication;
using DocPortal.Contracts.Endpoints.Auth;

using Mapster;

namespace DocPortal.Api.Mappings;

internal class AuthenticationMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<RegisterRequest, RegisterDetails>()
      //.Map(dest => dest.User.Login, src => src.Login)
      .Map(dest => dest.User.Role, src => (src.Role.ToLower()));

    config.NewConfig<RegisterDetails, RegisterResponse>()
      .Map(dest => dest.Login, src => src.Login)
      .Map(dest => dest.Role, src => src.User.Role.ToLower());

    config.NewConfig<LoginRequest, LoginDetails>();
    config.NewConfig<AccessToken, LoginResponse>()
      //.Map(dest => dest.Login, src => src.User.Login)
      .Map(dest => dest.Role, src => src.User.Role.ToLower());

    config.NewConfig<UpdateUserCredentialRequest, UpdateCredentialDetails>();
    config.NewConfig<UpdateCredentialDetails, UpdateUserCredentialResponce>();
  }
}
