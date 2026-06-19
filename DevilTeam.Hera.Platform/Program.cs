using DevilTeam.Hera.Platform.DevicesManagement.Application.CommandServices;
using DevilTeam.Hera.Platform.DevicesManagement.Application.Internal.CommandServices;
using DevilTeam.Hera.Platform.DevicesManagement.Application.Internal.QueryServices;
using DevilTeam.Hera.Platform.DevicesManagement.Application.QueryServices;
using DevilTeam.Hera.Platform.DevicesManagement.Domain.Repositories;
using DevilTeam.Hera.Platform.DevicesManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DevilTeam.Hera.Platform.Resources.Errors;
using DevilTeam.Hera.Platform.Resources.Shared;
using DevilTeam.Hera.Platform.Shared.Domain.Repositories;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ProblemDetailsFactory = DevilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

// Routing + Controllers (kebab-case routes, localized data annotations)
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Database (MySQL)
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string 'DefaultConnection' is not set.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services.AddSingleton<IStringLocalizer<CommonMessages>, StringLocalizer<CommonMessages>>();
builder.Services.AddSingleton<ProblemDetailsFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

// Dependency Injection

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Devices Management
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceCommandService, DeviceCommandService>();
builder.Services.AddScoped<IDeviceQueryService, DeviceQueryService>();

var app = builder.Build();

// Apply pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.UseGlobalExceptionHandler();

var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
