using PaymentApp.Domain.Framework;

namespace PaymentApp.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Репозиторий для работы с платежями
    /// </summary>
    public interface IPaymentRepository
    {

        Task<Result> CreatePayment();
    }
}
