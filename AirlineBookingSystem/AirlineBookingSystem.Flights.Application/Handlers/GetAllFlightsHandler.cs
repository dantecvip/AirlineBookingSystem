using AirlineBookingSystem.Flights.Application.Queries;
using AirlineBookingSystem.Flights.Core.Entities;
using AirlineBookingSystem.Flights.Core.Repositories;
using MediatR;

namespace AirlineBookingSystem.Flights.Application.Handlers
{
    public class GetAllFlightsHandler(IFlightRepository repository) : IRequestHandler<GetAllFlightsQuery, IEnumerable<Flight>>
    {
        public async Task<IEnumerable<Flight>> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetFlightsAsync();
        }
    }
}
