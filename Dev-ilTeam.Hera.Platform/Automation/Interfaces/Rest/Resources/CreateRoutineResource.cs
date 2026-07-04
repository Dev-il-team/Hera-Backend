using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.ValueObjects;

namespace Dev_ilTeam.Hera.Platform.Automation.Interfaces.Rest.Resources;

public record CreateRoutineResource(string Name, string ScheduledTime, ETriggerType TriggerType);
