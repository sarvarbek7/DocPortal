namespace DocPortal.Application.Common.Authentication.Services;

public interface IHashingService
{
  public string HashText(string text);

  public bool ValidateHash(string text, string hash);
}
