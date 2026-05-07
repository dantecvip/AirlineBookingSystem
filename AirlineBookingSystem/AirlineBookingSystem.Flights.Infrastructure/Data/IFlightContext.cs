using AirlineBookingSystem.Flights.Core.Entities;
using MongoDB.Driver;

namespace AirlineBookingSystem.Flights.Infrastructure.Data
{
    public interface IFlightContext
    {
        IMongoCollection<Flight> Flights { get; }
    }
}
