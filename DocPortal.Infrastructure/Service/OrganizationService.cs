using System.Linq.Expressions;

using DocPortal.Application.Services;
using DocPortal.Domain.Entities;
using DocPortal.Domain.Options;
using DocPortal.Infrastructure.Service.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

using ErrorOr;

using FluentValidation;

namespace DocPortal.Infrastructure.Service;

internal class OrganizationService(IOrganizationRepository repository, IValidator<Organization> validator) :
  CRUDService<Organization, int>(repository, validator), IOrganizationService
{
  public async ValueTask<ErrorOr<Organization>> AddEntityAsync(Organization entity,
                                                           bool saveChanges = true,
                                                           CancellationToken cancellationToken = default)
    => await base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<Organization>> ModifyAsync(Organization entity,
                                                  bool saveChanges = true,
                                                  CancellationToken cancellationToken = default)
    => await base.ModifyAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<Organization>> RemoveAsync(Organization entity,
                                                      bool saveChanges = true,
                                                      CancellationToken cancellationToken = default)
    => await base.RemoveAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<Organization>> RemoveByIdAsync(int id,
                                                          bool saveChanges = true,
                                                          CancellationToken cancellationToken = default)
    => await base.RemoveByIdAsync(id, saveChanges, cancellationToken);

  public IEnumerable<Organization> RetrieveAll(PageOptions pageOptions,
                                                    Expression<Func<Organization, bool>>? predicate = null,
                                                    bool asNoTracking = false,
                                                    ICollection<string>? includedNavigationalProperties = null)
    => base.RetrieveAll(pageOptions, predicate, asNoTracking, includedNavigationalProperties);

  public async ValueTask<ErrorOr<Organization?>> RetrieveByIdAsync(int id,
                                                       bool asNoTracking = false,
                                                       CancellationToken cancellationToken = default)
    => await base.RetrieveByIdAsync(id, asNoTracking, cancellationToken);

}