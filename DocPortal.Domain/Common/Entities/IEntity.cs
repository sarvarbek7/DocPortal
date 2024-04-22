namespace DocPortal.Domain.Common.Entities;

public interface IEntity<TId> : IEntity
  where TId : struct
{
  TId Id { get; set; }
}

public interface IEntity
{ }