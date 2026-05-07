using AirlineBookingSystem.Bookings.Application.Commands;
using AirlineBookingSystem.Bookings.Core.Entities;
using AirlineBookingSystem.Bookings.Core.Repositories;
using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using Mapster;
using MassTransit;
using MediatR;

namespace AirlineBookingSystem.Bookings.Application.Handlers
{
    public class CreateBookingHandler(IBookingRepository repository, IPublishEndpoint publishEndpoint) : IRequestHandler<CreateBookingCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = request.Adapt<Booking>();
            booking.Id = Guid.NewGuid();
            booking.BookingDate = DateTime.UtcNow;

            await repository.AddBookingAsync(booking);

            // Publish FlighBookedEvent
            await publishEndpoint.Publish(booking.Adapt<FlightBookedEvent>(), cancellationToken);

            return booking.Id;
        }
    }
}
