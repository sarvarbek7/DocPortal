namespace DocPortal.Domain.Statistics;
public record DocumentCountByOrgAndDoctype(int OrganizationId, int DocumentTypeId, int Count);
