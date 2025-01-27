﻿using DocPortal.Domain.Common.Entities;

namespace DocPortal.Persistance.Repositories.Interfaces.Bases;

public interface IEntityBaseRepository<TEntity, TId>
  : IBasicCrudRepository<TEntity, TId>, IAdvancedCrudRepository<TEntity, TId>
  where TEntity : class, IEntity<TId>
  where TId : struct
{ }
