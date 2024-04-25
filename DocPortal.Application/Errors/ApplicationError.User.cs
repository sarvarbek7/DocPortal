using ErrorOr;

namespace DocPortal.Application.Errors
{
  public static partial class ApplicationError
  {
    public static class UserError
    {
      public static Error NotFound =>
        Error.NotFound(code: "User.NotFound",
                       description: "User can not found");

      public static Error AlreadyExistsUserWithLogin =>
        Error.Conflict(code: "User.AlreadyExists",
                       description: "User is already exists with this login");

      public static Error AlreadyAssignedToOrganizations =>
        Error.Validation("User.AlreadyAssigned", description: "The user already assigned these organizations");

      public static Error NotAssigned =>
        Error.Validation("User.NotAssigned", description: "User not assigned this organization");
    }
  }
}
