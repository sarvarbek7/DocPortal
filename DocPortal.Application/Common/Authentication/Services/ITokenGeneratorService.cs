using DocPortal.Domain.Entities;

namespace DocPortal.Application.Common.Authentication.Services;

public interface ITokenGeneratorService
{
  AccessToken GenerateAccessToken(User user);
}
