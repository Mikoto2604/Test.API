using PaymentApp.Application.Dto;
using PaymentApp.Domain.Framework;

namespace PaymentApp.Application.Services.Payment
{
    public interface IPaymentService
    {
        Task<Result> PayAsync(PaymentDto paymentDto);
    }
}
