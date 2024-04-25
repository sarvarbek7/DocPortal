using DocPortal.Domain.Common.Entities;

namespace DocPortal.Domain.Entities;

public class UserCredential : IEntity<int>
{
  public int Id { get; set; }
  public string Login { get; set; }
  public string Password { get; set; }
}
