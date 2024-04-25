namespace DocPortal.Contracts.Dtos;

public record UserOrganizationDto(int Id, int UserId, int OrganizationId, string? OrganizationTitle);
