using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Framework;

namespace PaymentApp.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователя
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Метод для получения пользователя по Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Result<User>> GetByIdAsync(int Id);

        /// <summary>
        /// Метод для обновления баланса
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="balance"></param>
        /// <returns></returns>
        Task<Result> UpdateBalace(int Id, decimal balance);
    }
}
