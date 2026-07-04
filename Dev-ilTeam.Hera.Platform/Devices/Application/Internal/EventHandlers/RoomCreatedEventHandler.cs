using Dev_ilTeam.Hera.Platform.Devices.Domain.Model.Events;
using Dev_ilTeam.Hera.Platform.Shared.Application.Internal.EventHandlers;

namespace Dev_ilTeam.Hera.Platform.Devices.Application.Internal.EventHandlers;

public class RoomCreatedEventHandler : IEventHandler<RoomCreatedEvent>
{
    public Task Handle(RoomCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent);
    }

    private static Task On(RoomCreatedEvent domainEvent)
    {
        Console.WriteLine("Created Room: {0}", domainEvent.Name);
        return Task.CompletedTask;
    }
}
