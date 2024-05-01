using DocPortal.Domain.Common.Entities;

namespace DocPortal.Domain.Entities;

public class UserCredential : IEntity<int>
{
  public int Id { get; set; }
  public string Login { get; set; }
  public string Password { get; set; }

  public void UpdateEntityState(IEntity basedOnThisEntity)
  {
    if (basedOnThisEntity is not UserCredential)
    {
      throw new InvalidOperationException();
    }
    else
    {
      UserCredential credential = (basedOnThisEntity as UserCredential)!;

      this.Login = credential.Login;
      this.Password = credential.Password;
    }
  }
}
