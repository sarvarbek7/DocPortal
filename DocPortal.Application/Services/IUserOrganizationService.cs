using DocPortal.Application.Services.Bases;
using DocPortal.Domain.Entities;

namespace DocPortal.Application.Services;

public interface IUserOrganizationService : ICRUDService<UserOrganization, int>
{
  ValueTask<IEnumerable<UserOrganization>> AddMultipleUserOrganizations(IEnumerable<UserOrganization> userOrganizations, bool saveChanges = true, CancellationToken cancellationToken = default);
}
