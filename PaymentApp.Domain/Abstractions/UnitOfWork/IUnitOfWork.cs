using PaymentApp.Domain.Abstractions.Repositories;

namespace PaymentApp.Domain.Abstractions.UnitOfWork
{
    /// <summary>
    /// Интерфейс для работы с транзакциями. 
    /// </summary>
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository UserRepository { get; }
        IPaymentRepository PaymentRepository { get; }

        IUserTokenRepository UserTokenRepository { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
