using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DocPortal.Api.Http
{
  internal static class HttpContextService
  {
    public static string? GetUserId(HttpContext context)
    {
      IEnumerable<Claim>? claims = context.User?.Claims;

      if (claims is null)
      {
        return null;
      }

      var userIdClaim =
        claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);

      if (userIdClaim is null)
      {
        return null;
      }

      return userIdClaim.Value;
    }
  }
}
