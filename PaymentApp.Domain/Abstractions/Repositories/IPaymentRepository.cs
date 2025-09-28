using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Framework;

namespace PaymentApp.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Репозиторий для работы с платежями
    /// </summary>
    public interface IPaymentRepository
    {
        /// <summary>
        /// Метод для создания платежа
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        Task<Result> CreatePayment(Payment payment);
    }
}
