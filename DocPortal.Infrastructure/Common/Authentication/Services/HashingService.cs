using DocPortal.Application.Common.Authentication.Services;

namespace DocPortal.Infrastructure.Common.Authentication.Services;

internal class HashingService : IHashingService
{
  public string HashText(string text)
  {
    return BCrypt.Net.BCrypt.HashPassword(text);
  }

  public bool ValidateHash(string text, string hash)
  {
    return BCrypt.Net.BCrypt.Verify(text, hash);
  }
}
