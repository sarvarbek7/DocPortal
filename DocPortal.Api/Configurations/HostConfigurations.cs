using System.Reflection;

using DocPortal.Infrastructure;
using DocPortal.Persistance;

namespace DocPortal.Api.Configurations;

internal static partial class HostConfigurations
{
  private static readonly ICollection<Assembly> Assemblies;

  static HostConfigurations()
  {
    Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
    Assemblies.Add(Assembly.GetExecutingAssembly());
  }

  public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
  {
    builder.Services.AddAuthenticationInfrastucture(builder.Configuration);

    builder.Services.AddPersistance(builder.Configuration);

    builder.Services.AddInfrastructure();

    builder.Services.ConfigureRouteOptions();

    builder.Services.AddMappers();

    builder.Services.AddQueryServices();

    return builder;
  }

  public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
  {
    return new(app);
  }
}
