using DocPortal.Application.Services.Bases;
using DocPortal.Domain.Entities;

using ErrorOr;

namespace DocPortal.Application.Services;

public interface IOrganizationService : ICrudService<Organization, int>
{
  ValueTask<ErrorOr<Organization>> RetrieveOrganizationByIdWithDetails(int id,
                                                  bool asNoTracking = false,
                                                  ICollection<string>? includedNavigationalProperties = null);
}
