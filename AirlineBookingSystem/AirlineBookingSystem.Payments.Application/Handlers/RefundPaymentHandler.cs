using AirlineBookingSystem.Payments.Application.Commands;
using AirlineBookingSystem.Payments.Core.Repositories;
using MediatR;

namespace AirlineBookingSystem.Payments.Application.Handlers
{
    public class RefundPaymentHandler(IPaymentRepository repository) : IRequestHandler<RefundPaymentCommand>
    {
        public async Task Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
        {
            await repository.RefundPaymentAsync(request.PaymentId);
        }
    }
}
