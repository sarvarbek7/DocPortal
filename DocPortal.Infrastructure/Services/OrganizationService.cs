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

internal class OrganizationService(IOrganizationRepository repository, IValidator<Organization> validator) :
  CrudService<Organization, int>(repository, validator), IOrganizationService
{
  public new async ValueTask<ErrorOr<Organization>> AddEntityAsync(Organization entity,
                                                           bool saveChanges = true,
                                                           CancellationToken cancellationToken = default)
  {
    bool withPhysicalIdentityExists =
      await repository.EntityExistsAsync(org => org.PhysicalIdentity == entity.PhysicalIdentity, cancellationToken);

    return withPhysicalIdentityExists
      ? Error.Conflict("Organization.Conflict", $"Organization with physical identity {entity.PhysicalIdentity} is already registered.")
      : await base.AddEntityAsync(entity, saveChanges, cancellationToken);
  }

  public new async ValueTask<ErrorOr<Organization>> ModifyAsync(Organization entity,
                                                  bool saveChanges = true,
                                                  CancellationToken cancellationToken = default)
    => await base.ModifyAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<Organization>> RemoveAsync(Organization entity,
                                                      bool saveChanges = true,
                                                      CancellationToken cancellationToken = default)
    => await base.RemoveAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<Organization>> RemoveByIdAsync(int id,
                                                          bool saveChanges = true,
                                                          CancellationToken cancellationToken = default)
    => await base.RemoveByIdAsync(id, saveChanges, cancellationToken);

  public new IEnumerable<Organization> RetrieveAll(PageOptions pageOptions,
                                               Expression<Func<Organization, bool>>? predicate = null,
                                               bool asNoTracking = false,
                                               ICollection<string>? includedNavigationalProperties = null,
                                               Func<IQueryable<Organization>, IOrderedQueryable<Organization>>? orderFunc = null)
    => base.RetrieveAll(pageOptions, predicate, asNoTracking, includedNavigationalProperties);

  public new async ValueTask<ErrorOr<Organization?>> RetrieveByIdAsync(int id,
                                                       bool asNoTracking = false,
                                                       CancellationToken cancellationToken = default)
    => await base.RetrieveByIdAsync(id, asNoTracking, cancellationToken);

  public async ValueTask<ErrorOr<Organization>> RetrieveOrganizationByIdWithDetails(int id,
                                                         bool asNoTracking = false,
                                                         ICollection<string>? includedNavigationalProperties = null)
  {
    Expression<Func<Organization, bool>>? predicate = null;

    if (id == default)
    {
      return Error.Validation("Id is invalid.");
    }
    predicate = (Organization org) => org.Id == id;

    var initialQuery = repository.GetEntities(predicate);

    initialQuery = initialQuery.ApplyIncludedNavigations(includedNavigationalProperties);

    var storedOrganization =
      await initialQuery.FirstOrDefaultAsync();

    if (storedOrganization is null)
    {
      return OrganizationError.NotFound;
    }

    return storedOrganization;
  }
}