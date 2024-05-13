using System.Text.Json.Serialization;

using DocPortal.Api.Configurations;

internal class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers().AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.AllowTrailingCommas = true;
      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      options.JsonSerializerOptions.WriteIndented = true;
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.ConfigureHttpJsonOptions(options =>
    {
      options.SerializerOptions.AllowTrailingCommas = true;
      options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      options.SerializerOptions.WriteIndented = true;
    });

    const string _myCustomCorsPolicy = "FrontEndRequests";
    // Add services to the container.
    builder.Services.AddCors(options =>
    {
      options.AddPolicy(name: _myCustomCorsPolicy,
        corsBuilder =>
        {
          IConfigurationSection? corsSection =
            builder.Configuration.GetRequiredSection("Cors");

          string originsAsString =
          corsSection?.GetValue<string>("Origins") ?? string.Empty;

          string[] origins = originsAsString.Split(';', StringSplitOptions.RemoveEmptyEntries);

          corsBuilder.WithOrigins(origins)
          .AllowAnyMethod()
          .AllowAnyHeader();
        });
    });

    builder.Configure();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseCors(_myCustomCorsPolicy);

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
  }
}