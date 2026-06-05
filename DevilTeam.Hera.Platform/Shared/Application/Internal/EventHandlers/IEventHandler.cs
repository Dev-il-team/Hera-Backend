using DevilTeam.Hera.Platform.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace DevilTeam.Hera.Platform.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}