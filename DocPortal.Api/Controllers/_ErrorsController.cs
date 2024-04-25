using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DocPortal.Api.Controllers;

[Route("/error")]
internal class _ErrorsController : ControllerBase
{
  [HttpGet]
  public IActionResult Error()
  {
    Exception? exception =
        HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

    return Problem(
        statusCode: StatusCodes.Status400BadRequest,
        detail: exception?.Message);
  }
}
