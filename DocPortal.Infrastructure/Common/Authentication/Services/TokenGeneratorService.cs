using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using DocPortal.Application.Common.Authentication;
using DocPortal.Application.Common.Authentication.Services;
using DocPortal.Domain.Entities;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DocPortal.Infrastructure.Common.Authentication.Services;

internal class TokenGeneratorService(IOptions<JwtSettings> jwtSettingsOptions) : ITokenGeneratorService
{
  private readonly JwtSettings jwtSettings = jwtSettingsOptions.Value;

  public AccessToken GenerateAccessToken(User user)
  {
    var accessTokenId = Guid.NewGuid();

    var accessTokenExpiryAt =
      DateTime.Now.AddMinutes(jwtSettings.ExpirationTimeInMinutes);

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(jwtSettings.Key));

    var assignedOrganizations = user.UserOrganizations?.OrderBy(uO => uO.OrganizationId)
      .Aggregate("", (a, b) => a + $"_{b.OrganizationId}") ?? string.Empty;

    IEnumerable<Claim> claims = [
      new Claim(JwtRegisteredClaimNames.Jti, accessTokenId.ToString()),
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new Claim(ClaimTypes.Role, user.Role),
      new Claim("assignedOrganizations", assignedOrganizations)
      ];

    var credentials =
      new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var jwtToken = new JwtSecurityToken(
      issuer: jwtSettings.Issuer,
      audience: jwtSettings.Audience,
      claims,
      notBefore: DateTime.Now,
      expires: accessTokenExpiryAt,
      credentials
      );

    string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

    return new AccessToken(Id: accessTokenId,
                           User: user,
                           Token: token,
                           ExpireAt: accessTokenExpiryAt);
  }
}
