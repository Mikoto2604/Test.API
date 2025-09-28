namespace PaymentApp.Domain.Entities
{
    /// <summary>
    /// Класс пользователей
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; } = null!;

        /// <summary>
        /// Захешированный пароль пользователя
        /// </summary>
        public string PasswordHash { get; set; } = null!;

        /// <summary>
        /// Максимальное количество попыток аутентификации
        /// </summary>
        public short MaxAuthAttempts { get; set; }

        /// <summary>
        /// Количество текущих неудачных попыток
        /// </summary>
        public short FailedLoginAttempts { get; set; }

        /// <summary>
        /// Статус блокировки пользователя
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Баланс пользователя
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Валюта баланса пользователя
        /// </summary>
        public string BalanceCcy { get; set; } = null!;

        /// <summary>
        /// Навигационное поле по коллекции токенов
        /// </summary>
        public ICollection<UserToken> Tokens { get; set; } = new List<UserToken>();

        /// <summary>
        /// Навигационное поле по коллекции платежей
        /// </summary>
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
