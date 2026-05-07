using AirlineBookingSystem.Payments.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBookingSystem.Payments.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentCommand command)
        {
            var id = mediator.Send(command);
            return CreatedAtAction(nameof(ProcessPayment), new { id }, command);
        }

        [HttpPost("refun/{id}")]
        public async Task<IActionResult> RefundPayment(Guid id)
        {
            await mediator.Send(new RefundPaymentCommand(id));
            return NoContent();
        }
    }
}
