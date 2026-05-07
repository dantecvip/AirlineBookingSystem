using AirlineBookingSystem.Bookings.Application.Commands;
using AirlineBookingSystem.Bookings.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.Bookings.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] CreateBookingCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetBookingById), new { id }, command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(Guid id)
        {
            var booking = await mediator.Send(new GetBookingQuery(id));
            return Ok(booking);
        }
    }
}
