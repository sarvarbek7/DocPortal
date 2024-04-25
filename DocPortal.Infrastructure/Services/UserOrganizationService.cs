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

internal class UserOrganizationService(IUserOrganizationRepository repository, IValidator<UserOrganization> validator) :
  CrudService<UserOrganization, int>(repository, validator), IUserOrganizationService
{
  public new async ValueTask<ErrorOr<UserOrganization>> AddEntityAsync(UserOrganization entity,
                                                           bool saveChanges = true,
                                                           CancellationToken cancellationToken = default)
    => await base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<UserOrganization>> ModifyAsync(UserOrganization entity,
                                                  bool saveChanges = true,
                                                  CancellationToken cancellationToken = default)
    => await base.ModifyAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<UserOrganization>> RemoveAsync(UserOrganization entity,
                                                      bool saveChanges = true,
                                                      CancellationToken cancellationToken = default)
    => await base.RemoveAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<UserOrganization>> RemoveByIdAsync(int id,
                                                          bool saveChanges = true,
                                                          CancellationToken cancellationToken = default)
    => await base.RemoveByIdAsync(id, saveChanges, cancellationToken);

  public new IEnumerable<UserOrganization> RetrieveAll(PageOptions pageOptions,
                                                    Expression<Func<UserOrganization, bool>>? predicate = null,
                                                    bool asNoTracking = false,
                                                    ICollection<string>? includedNavigationalProperties = null)
    => base.RetrieveAll(pageOptions, predicate, asNoTracking, includedNavigationalProperties);

  public new async ValueTask<ErrorOr<UserOrganization?>> RetrieveByIdAsync(int id,
                                                       bool asNoTracking = false,
                                                       CancellationToken cancellationToken = default)
    => await base.RetrieveByIdAsync(id, asNoTracking, cancellationToken);

  public async ValueTask<IEnumerable<UserOrganization>> AddMultipleUserOrganizationsAsync(IEnumerable<UserOrganization> userOrganizations,
                                                                                     bool saveChanges = true,
                                                                                     CancellationToken cancellationToken = default)
  {
    return await repository.AddEntitiesRangeAsync(userOrganizations);
  }

  public async ValueTask<ErrorOr<UserOrganization>> RetrieveByUserIdAndOrganizationIdAsync(int userId, int organizationId, CancellationToken cancellationToken = default)
  {
    try
    {
      var userOrganization = await repository.GetEntities(
        uO => uO.UserId == userId && uO.OrganizationId == organizationId).FirstOrDefaultAsync(cancellationToken);

      if (userOrganization is null)
      {
        return UserError.NotAssigned;
      }

      return userOrganization;
    }
    catch
    {
      return Error.Unexpected();
    }
  }
}