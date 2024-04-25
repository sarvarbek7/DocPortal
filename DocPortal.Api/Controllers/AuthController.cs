
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
  [Authorize(Roles = Role.SuperAdmin)]
  [Route("api/[controller]")]
  public class AuthController(IAuthService authService, IMapper mapper) : _ApiController
  {
    [HttpPost("register")]
    public async ValueTask<IActionResult> Register(RegisterRequest request)
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

        var registerResponce = mapper.Map<RegisterResponse>(result.Value);

        return Ok(registerResponce);
      }
      catch (Exception ex)
      {
        return Problem([Error.Unexpected(description: ex.Message)]);
      }
    }

    [HttpPut("credentials")]
    public async ValueTask<IActionResult> UpdateUserCredential(UpdateUserCredentialRequest request)
    {
      try
      {
        var details = mapper.Map<UpdateCredentialDetails>(request);

        var errorOrUserCred =
          await authService.UpdateUserCredentialAsync(details);

        return errorOrUserCred.Match(
          value => Ok(mapper.Map<UpdateUserCredentialResponce>(value)),
          Problem);
      }
      catch (Exception ex)
      {
        return Problem([Error.Unexpected()]);
      }
    }

    [HttpDelete("credentials/{id:int:required}")]
    public async ValueTask<IActionResult> DeleteUserCredential(int id)
    {
      try
      {
        var errorOrValue =
          await authService.DeleterUserCredentialAsync(id);

        return errorOrValue.Match(
          value => NoContent(),
          Problem);
      }
      catch
      {
        return Problem([Error.Unexpected()]);
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

        var response = mapper.Map<LoginResponse>(accessToken);

        return Ok(response);
      }
      catch (Exception ex)
      {
        return Problem([Error.Unexpected(description: ex.Message)]);
      }
    }

    [AllowAnonymous]
    [HttpGet("logout")]
    public IActionResult Logout()
    {
      return Ok();
    }
  }
}
