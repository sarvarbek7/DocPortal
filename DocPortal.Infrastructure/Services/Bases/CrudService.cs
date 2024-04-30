using System.Linq.Expressions;

using DocPortal.Application.Options;
using DocPortal.Application.Services.Bases;
using DocPortal.Domain.Common.Entities;
using DocPortal.Infrastructure.Common.Extensions;
using DocPortal.Persistance.Repositories.Interfaces.Bases;

using ErrorOr;

using FluentValidation;

namespace DocPortal.Infrastructure.Services.Bases;

internal abstract class CrudService<TEntity, TId>(
  IBasicCrudRepository<TEntity, TId> repository,
  IValidator<TEntity> validator) : ICrudService<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : struct
{

  public async ValueTask<ErrorOr<TEntity>> AddEntityAsync(TEntity entity,
                                                           bool saveChanges = true,
                                                           CancellationToken cancellationToken = default)
  {
    try
    {
      if (await repository.EntityExistsAsync((ent => ent.Id.Equals(entity.Id))))
      {
        return Error.Conflict(code: $"{typeof(TEntity).Name}.Conflict",
          description: $"{typeof(TEntity).Name} with id {entity.Id} is already exists.");
      }

      var validationResult =
        await validator.ValidateAsync(entity, cancellationToken);

      if (!validationResult.IsValid)
      {
        var errors =
          validationResult.Errors.ConvertAll(error => Error.Validation(
            code: error.PropertyName, description: error.ErrorMessage));

        return errors;
      }

      return await repository.AddEntityAsync(entity, saveChanges, cancellationToken);
    }
    catch (Npgsql.PostgresException ex)
      when (ex.SqlState == "23503")
    {
      return Error.NotFound();
    }
    catch
    {
      return Error.Unexpected(code: $"{typeof(TEntity).Name}.Unexpected", description: "Can not process request.");
    }
  }

  public async ValueTask<ErrorOr<TEntity>> ModifyAsync(TEntity entity,
                                                  bool saveChanges = true,
                                                  CancellationToken cancellationToken = default)
  {
    try
    {
      if (!await repository.EntityExistsAsync((ent) => ent.Id.Equals(entity.Id)))
      {
        return Error.NotFound(code: $"{typeof(TEntity).Name}.NotFound",
          description: $"{typeof(TEntity).Name} with id {entity.Id} is not exists.");
      }

      var validationResult =
        await validator.ValidateAsync(entity, cancellationToken);

      if (!validationResult.IsValid)
      {
        var errors = validationResult.Errors.ConvertAll(error => Error.Validation(
          code: error.PropertyName,
          description: error.ErrorMessage));

        return errors;
      }

      return await repository.UpdateAsync(entity, saveChanges, cancellationToken);
    }
    catch (Exception ex)
    {
      return Error.Unexpected(code: $"{typeof(TEntity).Name}.Unexpected", description: "Can not process request.");
    }
  }

  public async ValueTask<ErrorOr<TEntity>> RemoveAsync(TEntity entity,
                                                        bool saveChanges = true,
                                                        CancellationToken cancellationToken = default)
  {
    try
    {
      if (!await repository.EntityExistsAsync((ent) => ent.Id.Equals(entity.Id)))
      {
        return Error.NotFound(code: $"{typeof(TEntity).Name}.NotFound",
          description: $"{typeof(TEntity).Name} with id {entity.Id} is not exists.");
      }

      var validationResult =
        await validator.ValidateAsync(entity, cancellationToken);

      if (!validationResult.IsValid)
      {
        var errors = validationResult.Errors.ConvertAll(error => Error.Validation(
          code: error.PropertyName,
          description: error.ErrorMessage));

        return errors;
      }

      return await repository.DeleteEntityAsync(entity, saveChanges, cancellationToken);
    }
    catch (Exception ex)
    {
      return Error.Unexpected(code: $"{typeof(TEntity).Name}.Unexpected", description: "Can not process request.");
    }
  }

  public async ValueTask<ErrorOr<TEntity>> RemoveByIdAsync(TId id,
                                                            bool saveChanges = true,
                                                            CancellationToken cancellationToken = default)
  {
    try
    {
      if (!await repository.EntityExistsAsync((ent) => ent.Id.Equals(id)))
      {
        return Error.NotFound(code: $"{typeof(TEntity).Name}.NotFound",
          description: $"Modified {typeof(TEntity).Name} with id {id} is not exists.");
      }

      var entity = await repository.DeleteEntityByIdAsync(id, saveChanges, cancellationToken);

      return entity;
    }
    catch (Exception ex)
    {
      return Error.Unexpected(code: $"{typeof(TEntity).Name}.Unexpected", description: "Can not process request.");
    }
  }

  public IEnumerable<TEntity> RetrieveAll(PageOptions pageOptions,
                                                    Expression<Func<TEntity, bool>>? predicate = null,
                                                    bool asNoTracking = false,
                                                    ICollection<string>? includedNavigationalProperties = null,
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFunc = null)
  {
    try
    {
      var initialQuery = repository.GetEntities(predicate, asNoTracking);

      if (orderFunc is null)
      {
        initialQuery = initialQuery
        .OrderBy(entity => entity.Id);
      }
      else
      {
        initialQuery = orderFunc(initialQuery);
      }

      initialQuery = initialQuery
        .Skip((pageOptions.PageToken - 1) * pageOptions.PageSize)
        .Take(pageOptions.PageSize);

      initialQuery =
        initialQuery.ApplyIncludedNavigations(includedNavigationalProperties);

      return initialQuery.AsEnumerable();
    }
    catch (Exception ex)
    {
      Console.Write(ex);
      return null;
    }
  }

  public async ValueTask<ErrorOr<TEntity?>> RetrieveByIdAsync(TId id,
                                                         bool asNoTracking = false,
                                                         CancellationToken cancellationToken = default)
  {
    try
    {
      var entity =
        await repository.GetEntityByIdAsync(id, asNoTracking, cancellationToken);

      return entity is null
        ? Error.NotFound(code: $"{typeof(TEntity).Name}.NotFound",
          description: $"Modified {typeof(TEntity).Name} with id {id} is not exists.")
        : entity;
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      return Error.Unexpected(code: $"{typeof(TEntity).Name}.Unexpected", description: "Can not process request.");
    }
  }

  public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return await repository.SaveChangesAsync(cancellationToken);
  }
}
