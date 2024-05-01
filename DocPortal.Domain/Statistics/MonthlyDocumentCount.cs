namespace DocPortal.Domain.Statistics;

public record MonthlyDocumentCount(int MonthOrder, int DocumentTypeId, int Count);
