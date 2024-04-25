namespace DocPortal.Domain.Common;

public static class Role
{
  public const string Admin = "admin";
  public const string SuperAdmin = "superadmin";
  public const string User = "user";

  private static string[] availableRoles = [Admin, SuperAdmin, User];

  public static IEnumerable<string> AvailableRoles
  {
    get => availableRoles.Select(role => role.ToLower());
  }
}
