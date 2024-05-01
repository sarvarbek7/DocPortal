using DocPortal.Application.Services.Bases;
using DocPortal.Domain.Entities;

using ErrorOr;

namespace DocPortal.Application.Services;

public interface IUserService : ICrudService<User, int>
{
  ValueTask<ErrorOr<User>> RetrieveUserByIdWithDetailsAsync(int id,
                                                       bool asNoTracking = false,
                                                       ICollection<string>? includedNavigationalProperties = null,
                                                       CancellationToken cancellationToken = default);
}
