using DocPortal.Persistance.DataContext;
using DocPortal.Persistance.Repositories;
using DocPortal.Persistance.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocPortal.Persistance;

public static class DependencyInjection
{
  public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
  {
    // Register DbContext
    services.AddDbContext<ApplicationDbContext>(options =>
    {
      var connectionString =
        configuration.GetConnectionString("PostgresqlConnection");

      if (connectionString is not null)
      {
        options.UseNpgsql(connectionString, options =>
        {
          options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        });
      }
    });

    services.AddRepositories();

    return services;
  }

  private static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    services.AddScoped<IDocumentRepository, DocumentRepository>()
      .AddScoped<IDocumentTypeRepository, DocumentTypeRepository>()
      .AddScoped<IOrganizationRepository, OrganizationRepository>()
      .AddScoped<IUserRepository, UserRepository>()
      .AddScoped<IUserOrganizationRepository, UserOrganizationRepository>()
      .AddScoped<IUserCredentialsRepository, UserCredentialRepository>();

    return services;
  }
}
