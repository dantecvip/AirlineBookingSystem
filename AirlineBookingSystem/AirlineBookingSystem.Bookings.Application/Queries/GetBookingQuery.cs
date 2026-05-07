using AirlineBookingSystem.Bookings.Core.Entities;
using MediatR;

namespace AirlineBookingSystem.Bookings.Application.Queries
{
    public record GetBookingQuery(Guid Id) : IRequest<Booking>;
}
