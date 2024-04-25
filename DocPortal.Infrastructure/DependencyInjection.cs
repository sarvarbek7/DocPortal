using DocPortal.Application.Common.Authentication.Services;
using DocPortal.Application.Services;
using DocPortal.Application.Services.Processing;
using DocPortal.Infrastructure.Common.Authentication.Services;
using DocPortal.Infrastructure.Services;
using DocPortal.Infrastructure.Services.Processing;
using DocPortal.Infrastructure.Validators;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace DocPortal.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    services.AddValidators();
    services.AddAuthenticationServices();
    services.AddFoundationServices();
    services.AddProcessingServices();

    return services;
  }

  private static void AddAuthenticationServices(this IServiceCollection services)
  {
    services.AddSingleton<IHashingService, HashingService>();
    services.AddSingleton<ITokenGeneratorService, TokenGeneratorService>();
    services.AddScoped<IAuthService, AuthService>();
  }

  private static void AddProcessingServices(this IServiceCollection services)
  {
    services.AddScoped<IStatisticsService, StatisticsService>();
  }

  private static void AddFoundationServices(this IServiceCollection services)
  {
    services.AddScoped<IDocumentService, DocumentService>()
      .AddScoped<IDocumentTypeService, DocumentTypeService>()
      .AddScoped<IOrganizationService, OrganizationService>()
      .AddScoped<IUserService, UserService>()
      .AddScoped<IUserOrganizationService, UserOrganizationService>()
      .AddScoped<IUserCredentialService, UserCredentialService>();
  }

  private static void AddValidators(this IServiceCollection services)
  {
    services.AddValidatorsFromAssemblyContaining<UserValidator>(includeInternalTypes: true);
  }
}
