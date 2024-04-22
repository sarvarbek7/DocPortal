using System.Linq.Expressions;

using DocPortal.Application.Services.Bases;
using DocPortal.Domain.Common.Entities;
using DocPortal.Domain.Options;
using DocPortal.Persistance.Repositories.Interfaces.Bases;

using ErrorOr;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

namespace DocPortal.Infrastructure.Service.Bases;

internal abstract class CRUDService<TEntity, TId>(
  IBasicCrudRepository<TEntity, TId> repository,
  IValidator<TEntity> validator) : ICRUDService<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : struct
{

  public async ValueTask<ErrorOr<TEntity>> AddEntityAsync(TEntity entity,
                                                           bool saveChanges = true,
                                                           CancellationToken cancellationToken = default)
  {
    try
    {
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
    catch (Exception ex)
    {
      return Error.Unexpected(code: $"{typeof(TEntity).Name}.Unexpected", description: ex.Message);
    }
  }

  public async ValueTask<ErrorOr<TEntity>> ModifyAsync(TEntity entity,
                                                  bool saveChanges = true,
                                                  CancellationToken cancellationToken = default)
  {
    try
    {
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
      return Error.Unexpected(code: $"{typeof(TEntity).Name}.Unexpected", description: ex.Message);
    }
  }

  public async ValueTask<ErrorOr<TEntity>> RemoveAsync(TEntity entity,
                                                        bool saveChanges = true,
                                                        CancellationToken cancellationToken = default)
  {
    try
    {
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
      return Error.Unexpected(code: $"{typeof(TEntity).Name}.Unexpected", description: ex.Message);
    }
  }

  public async ValueTask<ErrorOr<TEntity>> RemoveByIdAsync(TId id,
                                                            bool saveChanges = true,
                                                            CancellationToken cancellationToken = default)
  {
    try
    {
      var entity = await repository.DeleteEntityByIdAsync(id, saveChanges, cancellationToken);

      return entity;
    }
    catch (Exception ex)
    {
      return Error.Unexpected(code: $"{typeof(TEntity).Name}.Unexpected", description: ex.Message);
    }
  }

  public IEnumerable<TEntity> RetrieveAll(PageOptions pageOptions,
                                                    Expression<Func<TEntity, bool>>? predicate = null,
                                                    bool asNoTracking = false,
                                                    ICollection<string>? includedNavigationalProperties = null)
  {
    try
    {
      var initialQuery = repository.GetEntities(predicate, asNoTracking);

      initialQuery = initialQuery
        .Skip((pageOptions.PageToken - 1) * pageOptions.PageSize)
        .Take(pageOptions.PageSize);

      if (includedNavigationalProperties is not null)
      {
        foreach (string navigationalPropery in includedNavigationalProperties)
        {
          initialQuery = initialQuery.Include(navigationalPropery);
        }
      }

      return initialQuery.AsEnumerable();
    }
    catch (Exception ex)
    {
      // TODO logging
      return null;
    }
  }

  public async ValueTask<ErrorOr<TEntity?>> RetrieveByIdAsync(TId id,
                                                         bool asNoTracking = false,
                                                         CancellationToken cancellationToken = default)
  {
    try
    {
      return await repository.GetEntityByIdAsync(id, asNoTracking, cancellationToken);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      return Error.Unexpected(code: $"{typeof(TEntity).Name}.Unexpected", description: ex.Message);
    }
  }

  public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
  {
    return await repository.SaveChanges(cancellationToken);
  }
}
