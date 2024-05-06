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
        claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

      if (userIdClaim is null)
      {
        return null;
      }

      return userIdClaim.Value;
    }
  }
}
