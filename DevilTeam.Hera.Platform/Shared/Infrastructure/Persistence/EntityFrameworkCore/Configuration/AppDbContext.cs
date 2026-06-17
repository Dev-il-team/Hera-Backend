// NOTE: The Automation bounded context is not implemented yet (its types do not exist
// in the solution). The references below are commented out so the solution compiles.
// Re-enable them once the Automation bounded context is added.
// using DevilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;
// using DevilTeam.Hera.Platform.Automation.Infrastructure.Persistence.EFC.Configuration;
using DevilTeam.Hera.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using DevilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace DevilTeam.Hera.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context for the HERA Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // Automation (pending implementation)
    // public DbSet<AutomationRule> AutomationRules { get; set; }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Apply audit timestamp interceptor for all IAuditableEntity implementations
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <summary>
    ///     On creating the database model
    /// </summary>
    /// <remarks>
    ///     This method is used to create the database model for the application.
    /// </remarks>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // IAM Context
        builder.ApplyIamConfiguration();

        // Automation Context (pending implementation)
        // builder.ApplyConfiguration(new AutomationRuleConfiguration());

        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}
