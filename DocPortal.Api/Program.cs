using System.Text.Json.Serialization;

using DocPortal.Api.Configurations;

internal class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.ConfigureHttpJsonOptions(options =>
    {
      options.SerializerOptions.AllowTrailingCommas = true;
      options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      options.SerializerOptions.PropertyNameCaseInsensitive = true;
      options.SerializerOptions.WriteIndented = true;
    });

    const string _myCustomCorsPolicy = "FrontEndRequests";
    // Add services to the container.
    builder.Services.AddCors(options =>
    {
      options.AddPolicy(name: _myCustomCorsPolicy,
        builder =>
        {
          builder.WithOrigins("http://localhost:5173")
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

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
  }
}