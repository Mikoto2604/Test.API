using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Framework;

namespace PaymentApp.Domain.Abstractions.Repositories
{
    /// <summary>
    /// Репозиторий для работы с токенами пользователя
    /// </summary>
    public interface IUserTokenRepository
    {
        /// <summary>
        /// Метод добавления нового токена
        /// </summary>
        /// <param name="userToken"></param>
        /// <returns></returns>
        Task AddAsync(UserToken userToken);

        /// <summary>
        /// Метод для получения пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Result<UserToken>> GetByTokenAsync(string token);
    }
}
