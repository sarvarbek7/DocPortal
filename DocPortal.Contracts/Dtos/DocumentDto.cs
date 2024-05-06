namespace DocPortal.Contracts.Dtos;

public record DocumentDto(Guid Id,
                          string Title,
                          string RegisteredNumber,
                          DateOnly RegisteredDate,
                          bool IsPrivate,
                          int OrganizationId,
                          int DocumentTypeId,
                          int DownloadCount,
                          DocumentTypeDto? DocumentType,
                          OrganizationDto? Organization);
