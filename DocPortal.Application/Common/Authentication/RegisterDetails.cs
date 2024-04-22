using DocPortal.Domain.Entities;

namespace DocPortal.Application.Common.Authentication;

public record RegisterDetails(User User,
                              string? Password = null,
                              ICollection<UserOrganization>? UserOrganizations = null);
