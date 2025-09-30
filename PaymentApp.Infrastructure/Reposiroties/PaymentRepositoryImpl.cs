using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Abstractions.Repositories;
using PaymentApp.Infrastructure.Drivers.DbContexts;

namespace PaymentApp.Infrastructure.Reposiroties
{
    public class PaymentRepositoryImpl : IPaymentRepository
    {
        private readonly PgDbContext _pgDbContext;
        public PaymentRepositoryImpl(PgDbContext pgDbContext) => _pgDbContext = pgDbContext;

        public async Task CreatePaymentAsync(Payment payment) =>  await _pgDbContext.Payments.AddAsync(payment);
    }
}
