using AirlineBookingSystem.Flights.Application.Commands;
using AirlineBookingSystem.Flights.Core.Repositories;
using MediatR;

namespace AirlineBookingSystem.Flights.Application.Handlers
{
    public class DeleteFlightHandler(IFlightRepository repository) : IRequestHandler<DeleteFlightCommand>
    {
        public async Task Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteFlightAsync(request.Id);
        }
    }
}
