using AirlineBookingSystem.Flights.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace AirlineBookingSystem.Flights.Infrastructure.Data
{
    public class FlightContext : IFlightContext
    {
        public IMongoCollection<Flight> Flights { get; }

        public FlightContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Flights = database.GetCollection<Flight>(configuration["DatabaseSettings:CollectionName"]);
        }
    }
}
