using ErrorOr;

namespace DocPortal.Application.Common.Authentication.Services;

public interface IAuthService
{
  ValueTask<ErrorOr<RegisterDetails>> RegisterAsync(RegisterDetails details, CancellationToken cancellationToken = default);
  ValueTask<ErrorOr<AccessToken>> LoginAsync(LoginDetails details, CancellationToken cancellationToken = default);
  ValueTask<ErrorOr<UpdateCredentialDetails>> UpdateUserCredentialAsync(UpdateCredentialDetails details, CancellationToken cancellationToken = default);
  ValueTask<ErrorOr<bool>> DeleterUserCredentialAsync(int id, CancellationToken cancellationToken = default);
}
