namespace Dev_ilTeam.Hera.Platform.Automation.Interfaces.Rest.Resources;

public record RoutineResource(int Id, string Name, string ScheduledTime, string TriggerType, string Status);
