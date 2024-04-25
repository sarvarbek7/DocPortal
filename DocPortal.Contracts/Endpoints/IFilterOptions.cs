using DocPortal.Domain.Common.Entities;

namespace DocPortal.Contracts.Endpoints;

public interface IFilterOptions<TEntity> where TEntity : class, IEntity
{
}
