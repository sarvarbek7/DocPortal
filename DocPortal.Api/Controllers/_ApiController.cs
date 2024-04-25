using DocPortal.Api.Http;

using ErrorOr;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DocPortal.Api.Controllers;

[Authorize]
[ApiController]
public class _ApiController : ControllerBase
{
  protected IActionResult Problem(List<Error> errors)
  {
    HttpContext.Items.Add(HttpContextItemKeys.Errors, errors);

    if (errors.TrueForAll(
        error => error.Type is ErrorType.Validation))
    {
      return ValidationProblem(errors);
    }

    Error firstError = errors.FirstOrDefault();

    return Problem(firstError);
  }

  private IActionResult Problem(Error error)
  {
    int statusCode = error.Type switch
    {
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
      ErrorType.Forbidden => StatusCodes.Status403Forbidden,
      _ => StatusCodes.Status500InternalServerError
    };

    return Problem(statusCode: statusCode, title: error.Description);
  }

  private IActionResult ValidationProblem(List<Error> errors)
  {
    ModelStateDictionary modelStateDictionary = new();

    errors.ForEach(error =>
        modelStateDictionary.AddModelError(
            error.Code,
            error.Description));

    return ValidationProblem(modelStateDictionary);
  }
}
