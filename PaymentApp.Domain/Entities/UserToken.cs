namespace PaymentApp.Domain.Entities
{
    /// <summary>
    /// Класс токена пользователей
    /// </summary>
    public class UserToken
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; set; } = null!;

        /// <summary>
        /// Время жизни токена
        /// </summary>
        public DateTime Expiry { get; set; }

        /// <summary>
        /// Статус токена
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Дата отозвания токена
        /// </summary>
        public DateTime RevokedAt { get; set; }

        /// <summary>
        /// Id пользователя 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Навигационное поле 
        /// </summary>
        public User User { get; set; } = null!;
    }
}
