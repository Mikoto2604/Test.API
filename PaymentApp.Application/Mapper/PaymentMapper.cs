using PaymentApp.Application.Dto;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Mapper
{
    public static class PaymentMapper
    {
        public static PaymentDto ToDto(Payment payment)
        {
            return new PaymentDto
            {
                Id = payment.Id,
                UserId = payment.UserId,
                Amount = payment.Amount,
                CreatedAt = payment.CreatedAt,
            };
        }

        public static Payment ToEntity(PaymentDto paymentDto)
        {
            return new Payment
            {
                Id = paymentDto.Id,
                UserId = paymentDto.UserId,
                Amount = paymentDto.Amount,
                CreatedAt = paymentDto.CreatedAt,
            };
        }

        public static List<PaymentDto> ToDtoList(List<Payment> payments) => payments.Select(ToDto).ToList();

        public static List<Payment> ToEntityList(List<PaymentDto> paymentsDto)=> paymentsDto.Select(ToEntity).ToList();
    }
}
