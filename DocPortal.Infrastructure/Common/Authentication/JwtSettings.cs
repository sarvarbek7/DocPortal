namespace DocPortal.Infrastructure.Common.Authentication;

public class JwtSettings
{
  public const string SectionName = "Jwt";

  public string Issuer { get; set; } = default!;
  public string Audience { get; set; } = default!;
  public string Key { get; set; } = default!;
  public int ExpirationTimeInMinutes { get; set; }
}
