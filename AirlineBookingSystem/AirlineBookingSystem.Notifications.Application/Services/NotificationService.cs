using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Notifications.Application.Interfaces;
using AirlineBookingSystem.Notifications.Core.Entities;
using Mapster;
using MassTransit;

namespace AirlineBookingSystem.Notifications.Application.Services
{
    public class NotificationService(IPublishEndpoint publishEndpoint) : INotificationService
    {
        public async Task SendNotificationAsync(Notification notification)
        {
            // Simulate sending a notification (via email or sms)
            Console.WriteLine($"Notification sent to {notification.Recipient}: {notification.Message}");

            // Publish the event
            var notificationEvent = notification.Adapt<NotificationEvent>();
            await publishEndpoint.Publish(notificationEvent);
        }
    }
}
