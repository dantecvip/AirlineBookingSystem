using MassTransit;
using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;

namespace AirlineBookingSystem.Bookings.Application.Consumers
{
    public class NotificationEventConsumer : IConsumer<NotificationEvent>
    {
        public async Task Consume(ConsumeContext<NotificationEvent> context)
        {
            var notificationEvent = context.Message;

            Console.WriteLine($"Received Notification Event: Recipient={notificationEvent.Recipient}, " +
                $"Message={notificationEvent.Message}, Type={notificationEvent.Type}");

            await Task.CompletedTask;
        }
    }
}
