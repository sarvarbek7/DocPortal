﻿using System.Linq.Expressions;

using DocPortal.Application.Services.Processing;
using DocPortal.Domain.Entities;
using DocPortal.Domain.Statistics;
using DocPortal.Persistance.DataContext;

using Microsoft.EntityFrameworkCore;

namespace DocPortal.Infrastructure.Services.Processing;

internal class StatisticsService(ApplicationDbContext dbContext) : IStatisticsService
{
  public int GetOrganizationsCount(Expression<Func<Organization, bool>>? predicate = null)
  {
    var initialQuery = dbContext.Organizations.AsQueryable();

    if (predicate is not null)
    {
      initialQuery = initialQuery.Where(predicate);
    }

    return initialQuery.Count();
  }

  public int GetUsersCount(Expression<Func<User, bool>>? predicate)
  {
    var initialQuery = dbContext.Users.AsQueryable();

    if (predicate is not null)
    {
      initialQuery = initialQuery.Where(predicate);
    }

    dbContext.Organizations.Any(user => user.Id == 1);

    return initialQuery.Count();
  }

  public int GetDocumentsCount(Expression<Func<Document, bool>>? predicate)
  {
    var initialQuery = dbContext.Documents.AsQueryable();

    if (predicate is not null)
    {
      initialQuery = initialQuery.Where(predicate);
    }

    return initialQuery.Count();
  }

  public int GetDocumentTypesCount() => dbContext.DocumentTypes.Count();

  public List<DocumentCountByOrganization> GetDocumentsCountGroupByOrganization(Expression<Func<Document, bool>>? predicate)
  {
    var initialQuery = dbContext.Documents.AsQueryable();

    if (predicate is not null)
    {
      initialQuery = initialQuery.Where(predicate);
    }

    var docCountsByGroup = initialQuery.GroupBy(
      keySelector: document => document.OrganizationId,
      resultSelector: (key, documents) =>
        new DocumentCountByOrganization(key, documents.Count())).ToList();

    return docCountsByGroup;
  }

  public List<int> GetSubordinates(int id)
  {
    return dbContext.Database
      .SqlQuery<int>($"select * from get_subordinate_ids({id})")
      .AsEnumerable()
      .Where(subId => subId != id).ToList();
  }

  public IEnumerable<DailyDocumentCount> GetDailyDocumentCount(int year)
  {
    if (year == default)
    {
      year = DateTime.Now.Year;
    }

    var firstDayOfYear = new DateOnly(year, 1, 1);
    var lastDayOfYear = new DateOnly(year, 12, 31);

    return dbContext.Documents.Where(doc =>
    doc.RegisteredDate >= firstDayOfYear && doc.RegisteredDate <= lastDayOfYear)
      .GroupBy(doc => new { doc.RegisteredDate, doc.DocumentTypeId }, (key, docs) =>
      new DailyDocumentCount(key.RegisteredDate, key.DocumentTypeId, docs.Count())).AsEnumerable();
  }

  public List<MonthlyDocumentCount> GetMonthlyDocumentCount(int year)
  {
    if (year == default)
    {
      year = DateTime.Now.Year;
    }

    var dailyDocumentCount = GetDailyDocumentCount(year);

    return dailyDocumentCount.GroupBy(dailyDocCount =>
      new { dailyDocCount.Day.Month, dailyDocCount.DocumentTypeId }, (key, docCounts) =>
        new MonthlyDocumentCount(key.Month, key.DocumentTypeId, docCounts.Sum(doc => doc.Count))).ToList();
  }

  public List<DocumentCountByOrgAndDoctype> GetDocumentCountByOrgAndDoctype(Expression<Func<Document, bool>>? predicate)
  {
    var initialQuery = dbContext.Documents.AsQueryable();

    if (predicate is not null)
    {
      initialQuery = initialQuery.Where(predicate);
    }

    var documentCountByOrgAndDocType = initialQuery.GroupBy(
      keySelector: document => new { document.OrganizationId, document.DocumentTypeId },
      resultSelector: (key, documents) =>
        new DocumentCountByOrgAndDoctype(key.OrganizationId, key.DocumentTypeId, documents.Count())).ToList();

    return documentCountByOrgAndDocType;
  }

  public int GetDownloadsCount()
  {
    return dbContext.Documents.Sum(doc => doc.DownloadCount);
  }
}
