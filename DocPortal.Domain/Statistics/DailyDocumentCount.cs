﻿namespace DocPortal.Domain.Statistics;

public record DailyDocumentCount(DateOnly Day, int DocumentTypeId, int Count);
