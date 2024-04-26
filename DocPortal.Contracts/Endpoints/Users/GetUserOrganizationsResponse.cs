namespace DocPortal.Contracts.Endpoints.Users;

public record GetUserOrganizationsResponse(List<GetUserOrganizationsResponse.Organization> Organizations)
{
  public record Organization(int Id,
                             string Title,
                             string PhysicalIdentity,
                             int? PrimaryOrganizationId,
                             string? PrimaryOrganizationTitle,
                             string? Details,
                             int SubordinatesCount,
                             int DocumentsCount,
                             int DocumentsCountIncludeSubordinates);
};
