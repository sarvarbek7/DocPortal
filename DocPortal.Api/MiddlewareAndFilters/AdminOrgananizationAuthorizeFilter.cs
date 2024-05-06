using DocPortal.Domain.Common;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace DocPortal.Api.MiddlewareAndFilters
{
  internal class AdminOrgananizationAuthorizeFilter : IAuthorizationFilter
  {
    public void OnAuthorization(AuthorizationFilterContext context)
    {
      var httpContext = context.HttpContext;

      var assignedOrganizationsClaim =
        httpContext.User.Claims.FirstOrDefault(claim => claim.Type == "assignedOrganizations");

      var list =
        assignedOrganizationsClaim?.Value.Split('_', StringSplitOptions.RemoveEmptyEntries);

      StringValues organizationId = httpContext.Request.Headers["organization-id"];

      if (list is null || httpContext.User.IsInRole(Role.Admin) && string.IsNullOrEmpty(organizationId) &&
        !list.Contains(organizationId[0]))
      {
        context.Result = new UnauthorizedResult();
      }
    }
  }
}
