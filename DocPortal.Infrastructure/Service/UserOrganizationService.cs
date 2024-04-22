using System.Linq.Expressions;

using DocPortal.Application.Services;
using DocPortal.Domain.Entities;
using DocPortal.Domain.Options;
using DocPortal.Infrastructure.Service.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

using ErrorOr;

using FluentValidation;

namespace DocPortal.Infrastructure.Service;

internal class UserOrganizationService(IUserOrganizationRepository repository, IValidator<UserOrganization> validator) :
  CRUDService<UserOrganization, int>(repository, validator), IUserOrganizationService
{
  public async ValueTask<ErrorOr<UserOrganization>> AddEntityAsync(UserOrganization entity,
                                                           bool saveChanges = true,
                                                           CancellationToken cancellationToken = default)
    => await base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<UserOrganization>> ModifyAsync(UserOrganization entity,
                                                  bool saveChanges = true,
                                                  CancellationToken cancellationToken = default)
    => await base.ModifyAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<UserOrganization>> RemoveAsync(UserOrganization entity,
                                                      bool saveChanges = true,
                                                      CancellationToken cancellationToken = default)
    => await base.RemoveAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<UserOrganization>> RemoveByIdAsync(int id,
                                                          bool saveChanges = true,
                                                          CancellationToken cancellationToken = default)
    => await base.RemoveByIdAsync(id, saveChanges, cancellationToken);

  public IEnumerable<UserOrganization> RetrieveAll(PageOptions pageOptions,
                                                    Expression<Func<UserOrganization, bool>>? predicate = null,
                                                    bool asNoTracking = false,
                                                    ICollection<string>? includedNavigationalProperties = null)
    => base.RetrieveAll(pageOptions, predicate, asNoTracking, includedNavigationalProperties);

  public async ValueTask<ErrorOr<UserOrganization?>> RetrieveByIdAsync(int id,
                                                       bool asNoTracking = false,
                                                       CancellationToken cancellationToken = default)
    => await base.RetrieveByIdAsync(id, asNoTracking, cancellationToken);

  public async ValueTask<IEnumerable<UserOrganization>> AddMultipleUserOrganizations(IEnumerable<UserOrganization> userOrganizations,
                                                                                     bool saveChanges = true,
                                                                                     CancellationToken cancellationToken = default)
  {
    return await repository.AddEntitiesRangeAsync(userOrganizations);
  }

}