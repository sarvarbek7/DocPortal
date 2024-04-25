using ErrorOr;

namespace DocPortal.Application.Errors;

public static partial class ApplicationError
{
  public static class OrganizationError
  {
    public static Error NotFound =>
      Error.NotFound(code: "Organization.NotFound",
                     description: "Organization can not found");

    public static Error AlreadyAssignedToUsers =>
      Error.Validation("Organization.AlreadyAssigned", "The organization already assigned these users");
  }
}
