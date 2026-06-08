namespace DevilTeam.Hera.Platform.Automation.Infrastructure.Persistence.EFC.Configuration;

using Domain.Model.Aggregates;
using Domain.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// EF Core configuration for the AutomationRule aggregate.
/// </summary>
public class AutomationRuleConfiguration : IEntityTypeConfiguration<AutomationRule>
{
    public void Configure(EntityTypeBuilder<AutomationRule> builder)
    {
        // Table name
        builder.ToTable("automation_rules");

        // Primary key
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        // Basic fields
        builder.Property(r => r.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(r => r.Description)
            .HasColumnName("description")
            .HasMaxLength(255);

        builder.Property(r => r.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(r => r.HomeId)
            .HasColumnName("home_id")
            .IsRequired();

        builder.Property(r => r.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        // Trigger - owned entity (columnas dentro de la misma tabla)
        builder.OwnsOne(r => r.Trigger, trigger =>
        {
            trigger.Property(t => t.TriggerType)
                .HasColumnName("trigger_type")
                .HasConversion(
                    v => v.ToString(),
                    v => (TriggerType)Enum.Parse(typeof(TriggerType), v))
                .IsRequired();

            trigger.Property(t => t.ScheduledTime)
                .HasColumnName("trigger_scheduled_time")
                .HasMaxLength(5); // HH:mm

            trigger.Property(t => t.DeviceId)
                .HasColumnName("trigger_device_id");

            trigger.Property(t => t.ConditionValue)
                .HasColumnName("trigger_condition_value")
                .HasMaxLength(100);
        });

        // AutomationAction - owned entity (columnas dentro de la misma tabla)
        builder.OwnsOne(r => r.Action, action =>
        {
            action.Property(a => a.ActionType)
                .HasColumnName("action_type")
                .HasConversion(
                    v => v.ToString(),
                    v => (ActionType)Enum.Parse(typeof(ActionType), v))
                .IsRequired();

            action.Property(a => a.TargetDeviceId)
                .HasColumnName("action_target_device_id");

            action.Property(a => a.ActionValue)
                .HasColumnName("action_value")
                .HasMaxLength(255);
        });

        // Audit timestamps
        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(r => r.UpdatedAt)
            .HasColumnName("updated_at");
    }
}