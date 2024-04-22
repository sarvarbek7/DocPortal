
using DocPortal.Application.Common.Authentication;
using DocPortal.Application.Common.Authentication.Services;
using DocPortal.Contracts.Endpoints.Auth;
using DocPortal.Domain.Common;

using ErrorOr;

using MapsterMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocPortal.Api.Controllers
{
  [Route("api/[controller]")]
  public class AuthController(IAuthService authService, IMapper mapper) : ApiController
  {
    [Authorize(Roles = Role.SuperAdmin)]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
      try
      {
        var registerDetails = mapper.Map<RegisterDetails>(request);

        var result =
          await authService.RegisterAsync(registerDetails);

        if (result.IsError)
        {
          return Problem(result.Errors);
        }

        var registerResponce = mapper.Map<RegisterResponce>(result.Value);

        return Ok(registerResponce);
      }
      catch (Exception ex)
      {
        return Problem([Error.Unexpected(description: ex.Message)]);
      }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async ValueTask<IActionResult> Login(LoginRequest request)
    {
      try
      {
        var loginDetails = mapper.Map<LoginDetails>(request);

        ErrorOr<AccessToken> accessTokenOrError =
          await authService.LoginAsync(loginDetails);

        if (accessTokenOrError.IsError)
        {
          return Problem(accessTokenOrError.Errors);
        }

        AccessToken accessToken = accessTokenOrError.Value;

        var response = mapper.Map<LoginResponce>(accessToken);

        return Ok(response);
      }
      catch (Exception ex)
      {
        return Problem([Error.Unexpected(description: ex.Message)]);
      }
    }
  }
}
