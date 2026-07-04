using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Automation.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Automation.Interfaces.Rest.Transform;

public static class CreateRoutineCommandFromResourceAssembler
{
    public static CreateRoutineCommand ToCommandFromResource(CreateRoutineResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateRoutineResource cannot be null when converting to command.");
        return new CreateRoutineCommand(resource.Name, resource.ScheduledTime, resource.TriggerType);
    }
}
