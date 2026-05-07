using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Notifications.Application.Commands;
using MassTransit;
using MediatR;

namespace AirlineBookingSystem.Notifications.Application.Consumers
{
    public class PaymentProcessedConsumer(IMediator mediator) : IConsumer<PaymentProcessedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
        {
            var paymentProcessedEvent = context.Message;
            var message = $"Payment of ${paymentProcessedEvent.Amount} for Booking ID: {paymentProcessedEvent.BookingId} was processed successfully.";
            var command = new SendNotificationCommand("someone@example.com", message, "Email");
            await mediator.Send(command);
        }
    }
}
