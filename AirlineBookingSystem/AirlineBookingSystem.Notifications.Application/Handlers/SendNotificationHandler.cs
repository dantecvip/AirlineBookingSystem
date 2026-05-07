using AirlineBookingSystem.Notifications.Application.Commands;
using AirlineBookingSystem.Notifications.Application.Interfaces;
using AirlineBookingSystem.Notifications.Core.Entities;
using Mapster;
using MediatR;

namespace AirlineBookingSystem.Notifications.Application.Handlers
{
    public class SendNotificationHandler(INotificationService service) : IRequestHandler<SendNotificationCommand>
    {
        public async Task Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = request.Adapt<Notification>();
            notification.Id = Guid.NewGuid();

            await service.SendNotificationAsync(notification);
        }
    }
}
