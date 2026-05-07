using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Payments.Application.Commands;
using AirlineBookingSystem.Payments.Core.Entities;
using AirlineBookingSystem.Payments.Core.Repositories;
using Mapster;
using MassTransit;
using MediatR;

namespace AirlineBookingSystem.Payments.Application.Handlers
{
    public class ProcessPaymentHandler(IPublishEndpoint publishEndpoint, IPaymentRepository repository) : IRequestHandler<ProcessPaymentCommand, Guid>
    {
        public async Task<Guid> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = request.Adapt<Payment>();
            payment.Id = Guid.NewGuid();
            payment.PaymentDate = DateTime.UtcNow;

            Console.WriteLine($"Process Payment of Booking #{request.BookingId}. Amount = {request.Amount}");

            await repository.ProcessPaymentAsync(payment);

            // Publish PaymentProcessedEvent
            await publishEndpoint.Publish(payment.Adapt<PaymentProcessedEvent>());

            return payment.Id;
        }
    }
}
