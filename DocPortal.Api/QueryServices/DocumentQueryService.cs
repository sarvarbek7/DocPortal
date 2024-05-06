using System.Linq.Expressions;

using DocPortal.Contracts.Endpoints;
using DocPortal.Contracts.Endpoints.Documents.Options;
using DocPortal.Domain.Entities;

namespace DocPortal.Api.QueryServices
{
  internal class DocumentQueryService : IQueryService<Document>
  {
    public Expression<Func<Document, bool>>? ApplyFilterOptions(IFilterOptions<Document>? filterOptions)
    {
      var filter = filterOptions as DocumentFilterOptions;

      string? title = filter?.Title?.ToLower();
      string? registerNumber = filter?.RegisterNumber?.ToLower();
      int? organizationId = filter?.OrganizationId;
      int? documentTypeId = filter?.DocumentTypeId;
      DateOnly? startDate = filter?.StartDate;
      DateOnly? endDate = filter?.EndDate;

      Expression<Func<Document, bool>> predicate = null;

      if (filter is null)
      {
        return predicate;
      }

      predicate = (document) =>
        (title == null || document.Title.ToLower().Contains(title)) &&
        (registerNumber == null || document.RegisteredNumber.ToLower().StartsWith(registerNumber)) &&
        (organizationId == null || document.OrganizationId == organizationId) &&
        (documentTypeId == null || document.DocumentTypeId == documentTypeId) &&
        (startDate == null || document.RegisteredDate > startDate) &&
        (endDate == null || document.RegisteredDate < endDate) &&
        (filter.isDeleted == null || document.IsDeleted);

      return predicate;
    }

    public ICollection<string> ApplyIncludeQueries(IIncludeQueryOptions<Document>? includeQueryOptions)
    {
      var includedQuery = includeQueryOptions as DocumentIncludeQueryOptions;

      ICollection<string>? includedNavigationalProperties = [];

      if (includedQuery is not null)
      {
        if (includedQuery.IncludeDocumentType)
        {
          includedNavigationalProperties.Add(nameof(Document.DocumentType));
        }
        if (includedQuery.IncludeOrganization)
        {
          includedNavigationalProperties.Add(nameof(Document.Organization));
        }
      }

      return includedNavigationalProperties;
    }

    public Func<IQueryable<Document>, IOrderedQueryable<Document>>? ApplyOrderbyQuery(string? orderby, bool isDescending = false)
    {
      if (orderby is null)
      {
        return q => q.OrderByDescending(document => document.CreatedAt);
      }

      return (orderby, isDescending) switch
      {
        ("title", false) => q => q.OrderBy(entity => entity.Title),
        ("title", true) => q => q.OrderByDescending(entity => entity.Title),
        ("registeredNumber", true) => q => q.OrderBy(entity => entity.RegisteredNumber),
        ("registeredNumber", false) => q => q.OrderByDescending(entity => entity.RegisteredNumber),
        ("registeredDate", true) => q => q.OrderBy(entity => entity.RegisteredDate),
        ("registeredDate", false) => q => q.OrderByDescending(entity => entity.RegisteredDate),
        _ => null
      };
    }
  }
}
