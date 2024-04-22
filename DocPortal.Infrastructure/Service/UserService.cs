using System.Linq.Expressions;

using DocPortal.Application.Errors;
using DocPortal.Application.Services;
using DocPortal.Domain.Entities;
using DocPortal.Domain.Options;
using DocPortal.Infrastructure.Service.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

using ErrorOr;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

namespace DocPortal.Infrastructure.Service;

internal class UserService(IUserRepository repository, IValidator<User> validator) :
  CRUDService<User, int>(repository, validator), IUserService
{
  public async ValueTask<ErrorOr<User>> AddEntityAsync(User entity,
                                                       bool saveChanges = true,
                                                       CancellationToken cancellationToken = default)
    => await base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<User>> ModifyAsync(User entity,
                                                  bool saveChanges = true,
                                                  CancellationToken cancellationToken = default)
    => await base.ModifyAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<User>> RemoveAsync(User entity,
                                                      bool saveChanges = true,
                                                      CancellationToken cancellationToken = default)
    => await base.RemoveAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<User>> RemoveByIdAsync(int id,
                                                          bool saveChanges = true,
                                                          CancellationToken cancellationToken = default)
    => await base.RemoveByIdAsync(id, saveChanges, cancellationToken);

  public IEnumerable<User> RetrieveAll(PageOptions pageOptions,
                                                    Expression<Func<User, bool>>? predicate = null,
                                                    bool asNoTracking = false,
                                                    ICollection<string>? includedNavigationalProperties = null)
    => base.RetrieveAll(pageOptions, predicate, asNoTracking, includedNavigationalProperties);

  public async ValueTask<ErrorOr<User?>> RetrieveByIdAsync(int id,
                                                       bool asNoTracking = false,
                                                       CancellationToken cancellationToken = default)
    => await base.RetrieveByIdAsync(id, asNoTracking, cancellationToken);

  public async ValueTask<ErrorOr<User>> RetrieveUserByLoginAsync(string login)
  {
    try
    {
      var foundUser =
      await repository.GetEntities(user => user.Login == login).FirstOrDefaultAsync();

      if (foundUser is null)
      {
        return ApplicationError.UserError.NotFound;
      }

      return foundUser;
    }
    catch (Exception ex)
    {
      return Error.Unexpected(description: ex.Message);
    }
  }
}