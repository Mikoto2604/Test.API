using PaymentApp.Application.Dto;
using PaymentApp.Domain.Abstractions.UnitOfWork;
using PaymentApp.Domain.Framework;

namespace PaymentApp.Application.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> PayAsync(PaymentDto paymentDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                return Result.Ok();
            }
            catch (Exception ex) 
            {

            }
            return Result.Ok();
        }
    }
}
