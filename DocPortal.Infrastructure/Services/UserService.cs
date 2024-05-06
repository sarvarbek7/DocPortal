using System.Linq.Expressions;

using DocPortal.Application.Options;
using DocPortal.Application.Services;
using DocPortal.Domain.Entities;
using DocPortal.Infrastructure.Common.Extensions;
using DocPortal.Infrastructure.Services.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

using ErrorOr;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

using static DocPortal.Application.Errors.ApplicationError;

namespace DocPortal.Infrastructure.Services;

internal class UserService(IUserRepository repository, IValidator<User> validator) :
  CrudService<User, int>(repository, validator), IUserService
{
  public new async ValueTask<ErrorOr<User>> AddEntityAsync(User entity,
                                                       bool saveChanges = true,
                                                       CancellationToken cancellationToken = default)
    => await base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<User>> ModifyAsync(User entity,
                                                  bool saveChanges = true,
                                                  CancellationToken cancellationToken = default)
    => await base.ModifyAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<User>> RemoveAsync(User entity,
                                                      bool saveChanges = true,
                                                      CancellationToken cancellationToken = default)
    => await base.RemoveAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<User>> RemoveByIdAsync(int id,
                                                          bool saveChanges = true,
                                                          CancellationToken cancellationToken = default,
                                                          int? deletedBy = null)
    => await base.RemoveByIdAsync(id, saveChanges, cancellationToken, deletedBy);

  public new IEnumerable<User> RetrieveAll(PageOptions pageOptions,
                                                    Expression<Func<User, bool>>? predicate = null,
                                                    bool asNoTracking = false,
                                                    ICollection<string>? includedNavigationalProperties = null,
                                                    Func<IQueryable<User>, IOrderedQueryable<User>>? orderFunc = null)
    => base.RetrieveAll(pageOptions, predicate, asNoTracking, includedNavigationalProperties);

  public new async ValueTask<ErrorOr<User?>> RetrieveByIdAsync(int id,
                                                       CancellationToken cancellationToken = default)
    => await base.RetrieveByIdAsync(id, cancellationToken);

  public async ValueTask<ErrorOr<User>> RetrieveUserByIdWithDetailsAsync(int id,
                                                                         bool asNoTracking = false,
                                                                         ICollection<string>? includedNavigationalProperties = null,
                                                                         CancellationToken cancellationToken = default)
  {
    try
    {
      if (id == default)
      {
        return Error.Validation(description: "Id is invalid");
      }

      Expression<Func<User, bool>> predicate = (user) => user.Id == id;

      var initialQuery = repository.GetEntities(predicate);

      initialQuery =
        initialQuery.ApplyIncludedNavigations(includedNavigationalProperties);

      var storedUser = await initialQuery.FirstOrDefaultAsync(cancellationToken);

      if (storedUser is null)
      {
        return UserError.NotFound;
      }

      return storedUser;
    }
    catch (Exception ex)
    {
      return Error.Unexpected();
    }
  }
}