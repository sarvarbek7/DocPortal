using DocPortal.Contracts.Dtos;

namespace DocPortal.Contracts.Endpoints.Organizations;

public record GetOrganizationByIdResponse(OrganizationDto Organization,
                                         OrganizationDto? Parent,
                                         IEnumerable<UserOrganizationDto>? Admins,
                                         IEnumerable<OrganizationDto>? Subordinates,
                                         IEnumerable<DocumentDto>? Documents);
