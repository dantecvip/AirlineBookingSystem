using AirlineBookingSystem.Bookings.Core.Entities;
using AirlineBookingSystem.Bookings.Core.Repositories;
using Dapper;
using System.Data;

namespace AirlineBookingSystem.Bookings.Infrastructure.Repositories
{
    public class BookingRepository(IDbConnection dbConnection) : IBookingRepository
    {
        public async Task AddBookingAsync(Booking booking)
        {
            const string sql = @"
                INSERT INTO Bookings (Id, FlightId, PassengerName, SeatNumber, BookingDate)
                VALUES (@Id, @FlightId, @PassengerName, @SeatNumber, @BookingDate)";

            await dbConnection.ExecuteAsync(sql, booking);
        }

        public Task<Booking?> GetBookingByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM Bookings WHERE Id = @Id";

            return dbConnection.QuerySingleOrDefaultAsync<Booking>(sql, new { Id = id });
        }
    }
}
