using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Commands;
using Dev_ilTeam.Hera.Platform.Automation.Domain.Model.ValueObjects;

namespace Dev_ilTeam.Hera.Platform.Automation.Domain.Model.Aggregates;

public partial class Routine
{
    public Routine()
    {
        Name = string.Empty;
        ScheduledTime = string.Empty;
        TriggerType = ETriggerType.Time;
        Status = ERoutineStatus.Active;
    }

    public Routine(string name, string scheduledTime, ETriggerType triggerType) : this()
    {
        Name = name;
        ScheduledTime = scheduledTime;
        TriggerType = triggerType;
    }

    public Routine(CreateRoutineCommand command) : this(command.Name, command.ScheduledTime, command.TriggerType)
    {
    }

    public int Id { get; }
    public string Name { get; private set; }
    public string ScheduledTime { get; private set; }
    public ETriggerType TriggerType { get; private set; }
    public ERoutineStatus Status { get; private set; }

    public Routine Reschedule(string scheduledTime)
    {
        ScheduledTime = scheduledTime;
        return this;
    }

    public void Pause()
    {
        Status = ERoutineStatus.Paused;
    }

    public void Resume()
    {
        Status = ERoutineStatus.Active;
    }
}
