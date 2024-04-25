using DocPortal.Domain.Common.Entities;

namespace DocPortal.Contracts.Endpoints;

public interface IIncludeQueryOptions<TEntity> where TEntity : class, IEntity
{
}
