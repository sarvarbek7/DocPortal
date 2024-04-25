using DocPortal.Domain.Entities;

namespace DocPortal.Application.Common.Authentication;

public record RegisterDetails(User User,
                              string Login,
                              string Password,
                              ICollection<UserOrganization>? UserOrganizations = null);
