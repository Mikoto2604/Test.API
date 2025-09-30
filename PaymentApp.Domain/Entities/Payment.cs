namespace PaymentApp.Domain.Entities
{
    /// <summary>
    /// Класс платежей пользователя
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Уникальный идентификатор платежа
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Id пользователя 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Навигационное поле 
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Дата создания платежа
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
