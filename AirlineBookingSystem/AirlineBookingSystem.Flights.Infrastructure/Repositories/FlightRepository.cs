using AirlineBookingSystem.Flights.Core.Entities;
using AirlineBookingSystem.Flights.Core.Repositories;
using AirlineBookingSystem.Flights.Infrastructure.Data;
using MongoDB.Driver;

namespace AirlineBookingSystem.Flights.Infrastructure.Repositories
{
    public class FlightRepository(IFlightContext context) : IFlightRepository
    {
        public async Task AddFlightAsync(Flight flight)
        {
            await context.Flights.InsertOneAsync(flight);
        }

        public async Task DeleteFlightAsync(Guid id)
        {
            await context.Flights.DeleteOneAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Flight>> GetFlightsAsync()
        {
            return await context.Flights.Find(flights => true).ToListAsync();
        }
    }
}
