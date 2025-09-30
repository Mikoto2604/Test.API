using PaymentApp.Domain.Framework;

namespace PaymentApp.Application.Services.Transaction
{
    public interface ITransactionService
    {
        Task<Result> PaymentAsync(string userLogin);
    }
}
