using ErrorOr;

namespace DocPortal.Application.Common.Authentication.Services;

public interface IAuthService
{
  ValueTask<ErrorOr<RegisterDetails>> RegisterAsync(RegisterDetails details);

  ValueTask<ErrorOr<AccessToken>> LoginAsync(LoginDetails details);
}
