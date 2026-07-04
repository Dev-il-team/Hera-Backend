using Dev_ilTeam.Hera.Platform.Shared.Domain.Model.Events;

namespace Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Events;

public class RoomCreatedEvent(string name) : IEvent
{
    public string Name { get; } = name;
}
