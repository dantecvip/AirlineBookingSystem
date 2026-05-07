using AirlineBookingSystem.Flights.Application.Commands;
using AirlineBookingSystem.Flights.Core.Entities;
using AirlineBookingSystem.Flights.Core.Repositories;
using Mapster;
using MediatR;

namespace AirlineBookingSystem.Flights.Application.Handlers
{
    public class CreateFlightHandler(IFlightRepository repository) : IRequestHandler<CreateFlightCommand, Guid>
    {
        public async Task<Guid> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = request.Adapt<Flight>();
            flight.Id = Guid.NewGuid();

            await repository.AddFlightAsync(flight);
            return flight.Id;
        }
    }
}
