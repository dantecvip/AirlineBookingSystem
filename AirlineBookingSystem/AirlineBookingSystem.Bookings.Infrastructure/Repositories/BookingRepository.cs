using AirlineBookingSystem.Bookings.Core.Entities;
using AirlineBookingSystem.Bookings.Core.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace AirlineBookingSystem.Bookings.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDatabase _redisDatabase;
        private const string RedisKeyPrefix = "booking_";

        public BookingRepository(IConnectionMultiplexer redisConnection)
        {
            _redisDatabase = redisConnection.GetDatabase();
        }

        public async Task AddBookingAsync(Booking booking)
        {
            var data = JsonSerializer.Serialize(booking);
            await _redisDatabase.StringSetAsync($"{RedisKeyPrefix}{booking.Id}", data);
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid id)
        {
            var data = await _redisDatabase.StringGetAsync($"{RedisKeyPrefix}{id}");

            return data.HasValue ? JsonSerializer.Deserialize<Booking>((byte[])data!) : null;
        }
    }
}
