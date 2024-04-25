namespace DocPortal.Contracts.Dtos;

public record OrganizationDto(int Id,
                              string Title,
                              string PhysicalIdentity,
                              int? PrimaryOrganizationId = null,
                              string? PrimaryOrganizationTitle = null,
                              string? Details = null);