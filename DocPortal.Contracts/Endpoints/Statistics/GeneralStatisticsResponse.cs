namespace DocPortal.Contracts.Endpoints.Statistics;

public record GeneralStatisticsResponse(
  int DocumentsCount,
  int OrganizationsCount,
  int DownloadsCount
  );

