using System.Linq.Expressions;

using DocPortal.Domain.Entities;
using DocPortal.Domain.Statistics;

namespace DocPortal.Application.Services.Processing;

public interface IStatisticsService
{
  int GetOrganizationsCount(Expression<Func<Organization, bool>>? predicate = null);
  int GetUsersCount(Expression<Func<User, bool>>? predicate);
  int GetDocumentsCount(Expression<Func<Document, bool>>? predicate);
  List<DocumentCountByOrganization> GetDocumentsCountGroupByOrganization(Expression<Func<Document, bool>>? predicate);
  List<DocumentCountByOrgAndDoctype> GetDocumentCountByOrgAndDoctype(Expression<Func<Document, bool>>? predicate);
  int GetDocumentTypesCount();
  List<int> GetSubordinates(int id);
  IEnumerable<DailyDocumentCount> GetDailyDocumentCount(int year);
  List<MonthlyDocumentCount> GetMonthlyDocumentCount(int year);
}
