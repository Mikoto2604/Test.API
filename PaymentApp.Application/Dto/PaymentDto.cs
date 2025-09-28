using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Dto
{
    public class PaymentDto
    {
        /// <summary>
        /// Уникальный идентификатор платежа
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id пользователя 
        /// </summary>
        public int UserId { get; set; }

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
