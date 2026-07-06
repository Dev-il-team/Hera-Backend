using Dev_ilTeam.Hera.Platform.Automation.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Automation.Application.Internal.CommandServices;
using Dev_ilTeam.Hera.Platform.Automation.Application.Internal.QueryServices;
using Dev_ilTeam.Hera.Platform.Automation.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Automation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Dev_ilTeam.Hera.Platform.Automation.Resources;
using Dev_ilTeam.Hera.Platform.Devices.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Devices.Application.Internal.CommandServices;
using Dev_ilTeam.Hera.Platform.Devices.Application.Internal.QueryServices;
using Dev_ilTeam.Hera.Platform.Devices.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Devices.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Devices.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Dev_ilTeam.Hera.Platform.Devices.Resources;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.Internal.CommandServices;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.Internal.QueryServices;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Dev_ilTeam.Hera.Platform.EnergyAnalytics.Resources;
using Dev_ilTeam.Hera.Platform.Iam.Application.Acl;
using Dev_ilTeam.Hera.Platform.Iam.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Iam.Application.Internal.CommandServices;
using Dev_ilTeam.Hera.Platform.Iam.Application.Internal.OutboundServices;
using Dev_ilTeam.Hera.Platform.Iam.Application.Internal.QueryServices;
using Dev_ilTeam.Hera.Platform.Iam.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Iam.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using Dev_ilTeam.Hera.Platform.Iam.Interfaces.Acl;
using Dev_ilTeam.Hera.Platform.Iam.Resources;
using Dev_ilTeam.Hera.Platform.Monitoring.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Monitoring.Application.Internal.CommandServices;
using Dev_ilTeam.Hera.Platform.Monitoring.Application.Internal.QueryServices;
using Dev_ilTeam.Hera.Platform.Monitoring.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Monitoring.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Monitoring.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Dev_ilTeam.Hera.Platform.Monitoring.Resources;
using Dev_ilTeam.Hera.Platform.Profiles.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Profiles.Application.Internal.CommandServices;
using Dev_ilTeam.Hera.Platform.Profiles.Application.Internal.QueryServices;
using Dev_ilTeam.Hera.Platform.Profiles.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Profiles.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Dev_ilTeam.Hera.Platform.Profiles.Resources;
using Dev_ilTeam.Hera.Platform.Subscriptions.Application.CommandServices;
using Dev_ilTeam.Hera.Platform.Subscriptions.Application.Internal.CommandServices;
using Dev_ilTeam.Hera.Platform.Subscriptions.Application.Internal.QueryServices;
using Dev_ilTeam.Hera.Platform.Subscriptions.Application.QueryServices;
using Dev_ilTeam.Hera.Platform.Subscriptions.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Dev_ilTeam.Hera.Platform.Subscriptions.Resources;
using Dev_ilTeam.Hera.Platform.Resources.Errors;
using Dev_ilTeam.Hera.Platform.Resources.Shared;
using Dev_ilTeam.Hera.Platform.Shared.Domain.Repositories;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi;
using ProblemDetailsFactory = Dev_ilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseNpgsql(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services.AddSingleton<IStringLocalizer<CommonMessages>, StringLocalizer<CommonMessages>>();
builder.Services.AddSingleton<IStringLocalizer<IamMessages>, StringLocalizer<IamMessages>>();
builder.Services.AddSingleton<IStringLocalizer<ProfilesMessages>, StringLocalizer<ProfilesMessages>>();
builder.Services.AddSingleton<IStringLocalizer<DevicesMessages>, StringLocalizer<DevicesMessages>>();
builder.Services.AddSingleton<IStringLocalizer<AutomationMessages>, StringLocalizer<AutomationMessages>>();
builder.Services.AddSingleton<IStringLocalizer<MonitoringMessages>, StringLocalizer<MonitoringMessages>>();
builder.Services.AddSingleton<IStringLocalizer<EnergyAnalyticsMessages>, StringLocalizer<EnergyAnalyticsMessages>>();
builder.Services.AddSingleton<IStringLocalizer<SubscriptionsMessages>, StringLocalizer<SubscriptionsMessages>>();

builder.Services.AddSingleton<ProblemDetailsFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Dev-ilTeam.Hera.Platform",
            Version = "v1",
            Description = "Smart Home HERA Platform API",
            TermsOfService = new Uri("https://smart-home-hera.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "Dev-il-team",
                Email = "contact@smart-home-hera.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        { [new OpenApiSecuritySchemeReference("Bearer", document)] = [] });
    options.EnableAnnotations();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IRoomCommandService, RoomCommandService>();
builder.Services.AddScoped<IRoomQueryService, RoomQueryService>();
builder.Services.AddScoped<IDeviceCommandService, DeviceCommandService>();
builder.Services.AddScoped<IDeviceQueryService, DeviceQueryService>();

builder.Services.AddScoped<IRoutineRepository, RoutineRepository>();
builder.Services.AddScoped<IRoutineCommandService, RoutineCommandService>();
builder.Services.AddScoped<IRoutineQueryService, RoutineQueryService>();

builder.Services.AddScoped<ICameraRepository, CameraRepository>();
builder.Services.AddScoped<ICameraCommandService, CameraCommandService>();
builder.Services.AddScoped<ICameraQueryService, CameraQueryService>();

builder.Services.AddScoped<IConsumptionReportRepository, ConsumptionReportRepository>();
builder.Services.AddScoped<IConsumptionReportCommandService, ConsumptionReportCommandService>();
builder.Services.AddScoped<IConsumptionReportQueryService, ConsumptionReportQueryService>();

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();

builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddCortexMediator(
    [typeof(Program)]);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var context = services.GetRequiredService<AppDbContext>();

    const int maxRetries = 10;
    var delay = TimeSpan.FromSeconds(5);
    for (var attempt = 1; attempt <= maxRetries; attempt++)
    {
        try
        {
            context.Database.EnsureCreated();
            logger.LogInformation("Database ready (attempt {Attempt}/{MaxRetries}).", attempt, maxRetries);
            break;
        }
        catch (Exception ex) when (attempt < maxRetries)
        {
            logger.LogWarning(ex,
                "Database not reachable (attempt {Attempt}/{MaxRetries}). Retrying in {Delay}s...",
                attempt, maxRetries, delay.TotalSeconds);
            Thread.Sleep(delay);
        }
    }
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
app.UseRequestAuthorization();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();