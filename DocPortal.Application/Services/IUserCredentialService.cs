using DocPortal.Application.Services.Bases;
using DocPortal.Domain.Entities;

using ErrorOr;

namespace DocPortal.Application.Services;

public interface IUserCredentialService : ICrudService<UserCredential, int>
{
  ValueTask<ErrorOr<UserCredential>> RetrieveUserCredentialByLoginAsync(string login, CancellationToken cancellationToken = default);

}
