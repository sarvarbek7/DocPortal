using ErrorOr;

namespace DocPortal.Application.Errors;

public static partial class ApplicationError
{
  public static class AuthenticationError
  {
    public static Error InvalidCredentials =>
      Error.Unauthorized(code: "Authentication.InvalidCredentials",
                         description: "InvalidCredentials");
  }
}
