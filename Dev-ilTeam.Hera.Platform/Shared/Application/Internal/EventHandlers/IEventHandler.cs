using Dev_ilTeam.Hera.Platform.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace Dev_ilTeam.Hera.Platform.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}
