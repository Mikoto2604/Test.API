using Microsoft.EntityFrameworkCore.Storage;
using PaymentApp.Domain.Abstractions.Repositories;
using PaymentApp.Domain.Abstractions.UnitOfWork;
using PaymentApp.Infrastructure.Drivers.DbContexts;

namespace PaymentApp.Infrastructure.UnitOfWork
{
    public class UnitOfWorkImpl : IUnitOfWork
    {
        private readonly PgDbContext _pgDbContext;
        private IDbContextTransaction? _transaction;
        public IUserRepository UserRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IUserTokenRepository UserTokenRepository { get; }

        public UnitOfWorkImpl(PgDbContext pgDbContext,
                      IUserRepository userRepository,
                      IPaymentRepository paymentRepository,
                      IUserTokenRepository userTokenRepository)
        {
            _pgDbContext = pgDbContext;
            UserRepository = userRepository;
            PaymentRepository = paymentRepository;
            UserTokenRepository = userTokenRepository;
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
                _transaction = await _pgDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _pgDbContext.SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _pgDbContext.Dispose();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
