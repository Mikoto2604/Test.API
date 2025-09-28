using Payment.Domain.Entities;
using Payment.Domain.Framework;

namespace Payment.Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<Result> Add(User user);
    }
}
