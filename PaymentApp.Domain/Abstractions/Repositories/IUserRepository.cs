using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Framework;

namespace PaymentApp.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователями
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Метод для получения пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<Result<User>> GetByLoginAsync(string login);

        /// <summary>
        /// Метод для получения пользователя с блокировкой доступа
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<Result<User>> GetByLoginForUpdateAsync(string login);

        /// <summary>
        /// Метод для получения пользователя по Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Result<User>> GetByIdAsync(int Id);
    }
}
