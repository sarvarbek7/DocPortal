using DocPortal.Application.Services.Bases;
using DocPortal.Domain.Entities;

using ErrorOr;

namespace DocPortal.Application.Services;

public interface IUserService : ICRUDService<User, int>
{
  ValueTask<ErrorOr<User>> RetrieveUserByLoginAsync(string login);
}
