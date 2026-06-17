using DevilTeam.Hera.Platform.Iam.Application.Acl;
using DevilTeam.Hera.Platform.Iam.Application.CommandServices;
using DevilTeam.Hera.Platform.Iam.Application.Internal.CommandServices;
using DevilTeam.Hera.Platform.Iam.Application.Internal.OutboundServices;
using DevilTeam.Hera.Platform.Iam.Application.Internal.QueryServices;
using DevilTeam.Hera.Platform.Iam.Application.QueryServices;
using DevilTeam.Hera.Platform.Iam.Domain.Repositories;
using DevilTeam.Hera.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using DevilTeam.Hera.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DevilTeam.Hera.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using DevilTeam.Hera.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using DevilTeam.Hera.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using DevilTeam.Hera.Platform.Iam.Interfaces.Acl;
using DevilTeam.Hera.Platform.Iam.Resources;
using DevilTeam.Hera.Platform.Resources.Errors;
using DevilTeam.Hera.Platform.Resources.Shared;
using DevilTeam.Hera.Platform.Shared.Domain.Repositories;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi;
using ProblemDetailsFactory = DevilTeam.Hera.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Add ProblemDetails services
builder.Services.AddProblemDetails();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configure Database Context and route EF logs through the app logger pipeline.
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

// Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Explicitly register IStringLocalizer for ErrorMessages, CommonMessages and IamMessages
builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services.AddSingleton<IStringLocalizer<CommonMessages>, StringLocalizer<CommonMessages>>();
builder.Services.AddSingleton<IStringLocalizer<IamMessages>, StringLocalizer<IamMessages>>();

// Register the custom ProblemDetailsFactory
builder.Services.AddSingleton<ProblemDetailsFactory>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "DevilTeam.Hera.Platform",
            Version = "v1",
            Description = "HERA Smart Home Platform API",
            TermsOfService = new Uri("https://hera-smarthome.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "Dev-il-team",
                Email = "contact@hera-smarthome.com"
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
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        { [new OpenApiSecuritySchemeReference("bearer", document)] = [] });
    options.EnableAnnotations();
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context Injection Configuration

// TokenSettings Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// Mediator Configuration

// Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    [typeof(Program)]);

var app = builder.Build();

// Apply pending migrations on startup (safe to call even when schema is up to date)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
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

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
