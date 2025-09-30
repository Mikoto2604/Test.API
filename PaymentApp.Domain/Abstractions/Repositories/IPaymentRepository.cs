using PaymentApp.Domain.Entities;

namespace PaymentApp.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Репозиторий для работы с платежами
    /// </summary>
    public interface IPaymentRepository
    {
        /// <summary>
        /// Метод для создания платежа
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        Task CreatePaymentAsync(Payment payment);
    }
}
