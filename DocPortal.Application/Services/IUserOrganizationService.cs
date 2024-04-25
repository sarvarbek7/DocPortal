using DocPortal.Application.Services.Bases;
using DocPortal.Domain.Entities;

using ErrorOr;

namespace DocPortal.Application.Services;

public interface IUserOrganizationService : ICrudService<UserOrganization, int>
{
  ValueTask<IEnumerable<UserOrganization>> AddMultipleUserOrganizationsAsync(IEnumerable<UserOrganization> userOrganizations, bool saveChanges = true, CancellationToken cancellationToken = default);

  ValueTask<ErrorOr<UserOrganization>> RetrieveByUserIdAndOrganizationIdAsync(int userId, int organizationId, CancellationToken cancellationToken = default);
}
