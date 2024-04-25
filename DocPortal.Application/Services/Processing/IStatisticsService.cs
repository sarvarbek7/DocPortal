using System.Linq.Expressions;

using DocPortal.Domain.Entities;

namespace DocPortal.Application.Services.Processing;

public interface IStatisticsService
{
  int GetOrganizationsCount(Expression<Func<Organization, bool>>? predicate = null);
  int GetUsersCount(Expression<Func<User, bool>>? predicate);
  int GetDocumentsCount(Expression<Func<Document, bool>>? predicate);
  int GetDocumentTypesCount();
}
