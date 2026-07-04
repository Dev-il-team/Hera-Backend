using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.ValueObjects;

namespace Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Commands;

public record CreateRoutineCommand(string Name, string ScheduledTime, ETriggerType TriggerType);
