using PaymentApp.Domain.Abstractions.Repositories;
using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Framework;
using PaymentApp.Infrastructure.Drivers.DbContexts;

namespace PaymentApp.Infrastructure.Reposiroties
{
    public class PaymentRepositoryImpl : IPaymentRepository
    {
        private readonly PgDbContext _pgDbContext;
        public PaymentRepositoryImpl(PgDbContext pgDbContext) => _pgDbContext = pgDbContext;

        public async Task<Result> CreatePayment(Payment payment)
        {
            await _pgDbContext.Payments.AddAsync(payment);
            return Result.Ok();
        }
    }
}
