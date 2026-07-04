using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;
using Dev_ilTeam.Hera.Platform.Automation.Interfaces.Rest.Resources;

namespace Dev_ilTeam.Hera.Platform.Automation.Interfaces.Rest.Transform;

public static class RoutineResourceFromEntityAssembler
{
    public static RoutineResource ToResourceFromEntity(Routine entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Routine entity cannot be null when converting to resource.");
        return new RoutineResource(entity.Id, entity.Name, entity.ScheduledTime, entity.TriggerType.ToString(),
            entity.Status.ToString());
    }
}
