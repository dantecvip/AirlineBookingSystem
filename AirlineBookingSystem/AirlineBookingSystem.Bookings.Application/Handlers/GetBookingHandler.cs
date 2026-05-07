using AirlineBookingSystem.Bookings.Application.Queries;
using AirlineBookingSystem.Bookings.Core.Entities;
using AirlineBookingSystem.Bookings.Core.Repositories;
using MediatR;

namespace AirlineBookingSystem.Bookings.Application.Handlers
{
    public class GetBookingHandler(IBookingRepository bookingRepository) : IRequestHandler<GetBookingQuery, Booking>
    {
        public Task<Booking> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            return bookingRepository.GetBookingByIdAsync(request.Id)!;
        }
    }
}
