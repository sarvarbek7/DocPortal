using Microsoft.AspNetCore.Mvc;

namespace DocPortal.Api.Filters;

internal class AdminOrganizationAuthorizeAttribute() :
  TypeFilterAttribute(typeof(AdminOrgananizationAuthorizeFilter));
