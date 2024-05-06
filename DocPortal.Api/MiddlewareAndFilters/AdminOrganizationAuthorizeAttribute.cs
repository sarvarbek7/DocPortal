using Microsoft.AspNetCore.Mvc;

namespace DocPortal.Api.MiddlewareAndFilters;

internal class AdminOrganizationAuthorizeAttribute() :
  TypeFilterAttribute(typeof(AdminOrgananizationAuthorizeFilter));
