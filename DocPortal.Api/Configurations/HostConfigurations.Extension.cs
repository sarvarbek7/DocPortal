using System.Reflection;
using System.Text;

using DocPortal.Api.QueryServices;
using DocPortal.Domain.Entities;
using DocPortal.Infrastructure.Common.Authentication;

using Mapster;

using MapsterMapper;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DocPortal.Api.Configurations;
internal static partial class HostConfigurations
{
  private static void ConfigureRouteOptions(this IServiceCollection services)
  {
    services.Configure<RouteOptions>(config =>
    {
      config.AppendTrailingSlash = true;
      config.LowercaseUrls = true;
      config.LowercaseQueryStrings = true;
    });
  }

  private static void AddMappers(this IServiceCollection services)
  {
    TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
    config.Scan(Assembly.GetExecutingAssembly());

    services.AddSingleton(config);

    services.AddScoped<IMapper, ServiceMapper>();
  }

  public static void AddAuthenticationInfrastucture(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<JwtSettings>(
    config: configuration.GetSection(JwtSettings.SectionName));

    JwtSettings jwtSettings =
      configuration.GetSection(JwtSettings.SectionName)
      .Get<JwtSettings>()!;

    services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidIssuer = jwtSettings.Issuer,
          ValidAudience = jwtSettings.Audience,
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Key)),
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ClockSkew = TimeSpan.FromSeconds(5)
        };
      });

    services.AddAuthorization();
  }

  private static void AddQueryServices(this IServiceCollection services)
  {
    services.AddSingleton<IQueryService<Organization>, OrganizationQueryService>();
    services.AddSingleton<IQueryService<User>, UserQueryService>();
    services.AddSingleton<IQueryService<Document>, DocumentQueryService>();
  }
}
