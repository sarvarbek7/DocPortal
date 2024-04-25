using DocPortal.Contracts.Dtos;

namespace DocPortal.Contracts.Endpoints.Organizations;

public record GetAllOrganizationsResponse(IEnumerable<OrganizationDto> Organizations,
                                          int Total,
                                          int PageSize);

