using AirlineBookingSystem.Flights.Application.Commands;
using AirlineBookingSystem.Flights.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.Flights.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetFlights()
        {
            var flights = await mediator.Send(new GetAllFlightsQuery());
            return Ok(flights);
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight([FromBody] CreateFlightCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetFlights), new { id }, command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(Guid id)
        {
            await mediator.Send(new DeleteFlightCommand(id));
            return NoContent();
        }
    }
}
