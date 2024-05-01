using System.Linq.Expressions;

using DocPortal.Application.Options;
using DocPortal.Application.Services;
using DocPortal.Domain.Entities;
using DocPortal.Infrastructure.Services.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

using ErrorOr;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

using static DocPortal.Application.Errors.ApplicationError;

namespace DocPortal.Infrastructure.Services;

internal class UserCredentialService(IUserCredentialsRepository repository,
                                     IValidator<UserCredential> validator) :
  CrudService<UserCredential, int>(repository, validator),
  IUserCredentialService
{
  public new async ValueTask<ErrorOr<UserCredential>> AddEntityAsync(UserCredential entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    => await base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<UserCredential>> ModifyAsync(UserCredential entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    => await base.ModifyAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<UserCredential>> RemoveAsync(UserCredential entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    => await base.RemoveAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<UserCredential>> RemoveByIdAsync(int id, bool saveChanges = true, CancellationToken cancellationToken = default)
    => await base.RemoveByIdAsync(id, saveChanges, cancellationToken);

  public new IEnumerable<UserCredential> RetrieveAll(PageOptions pageOptions,
                                                     Expression<Func<UserCredential, bool>>? predicate = null,
                                                     bool asNoTracking = false,
                                                     ICollection<string>? includedNavigationalProperties = null,
                                                     Func<IQueryable<UserCredential>, IOrderedQueryable<UserCredential>>? orderFunc = null)
    => base.RetrieveAll(pageOptions, predicate);

  public new async ValueTask<ErrorOr<UserCredential?>> RetrieveByIdAsync(int id,
                                                                         CancellationToken cancellationToken = default)
    => await base.RetrieveByIdAsync(id, cancellationToken);

  public async ValueTask<ErrorOr<UserCredential>> RetrieveUserCredentialByLoginAsync(string login, CancellationToken cancellationToken = default)
  {
    try
    {
      var foundUserCredential =
        await repository.GetEntities(credential => credential.Login == login).FirstOrDefaultAsync();

      if (foundUserCredential is null)
      {
        return UserError.NotFound;
      }

      return foundUserCredential;
    }
    catch (Exception ex)
    {
      return Error.Unexpected();
    }
  }
}
