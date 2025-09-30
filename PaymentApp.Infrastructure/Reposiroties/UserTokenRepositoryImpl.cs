using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Framework;
using Microsoft.EntityFrameworkCore;
using PaymentApp.Domain.Abstractions.Repositories;
using PaymentApp.Infrastructure.Drivers.DbContexts;

namespace PaymentApp.Infrastructure.Reposiroties
{
    public class UserTokenRepositoryImpl : IUserTokenRepository
    {
        private readonly PgDbContext _pgDbContext;
        public UserTokenRepositoryImpl(PgDbContext pgDbContext) => _pgDbContext = pgDbContext;
        public async Task AddAsync(UserToken userToken) =>  await _pgDbContext.UserTokens.AddAsync(userToken);

        public async Task<Result<UserToken>> GetByTokenAsync(string token)
        {
            var userToken = await _pgDbContext.UserTokens.Where(x => x.Token == token).FirstOrDefaultAsync();
            if (userToken != null)
                return Result<UserToken>.Ok(userToken);
            return Result<UserToken>.Error(ResultCode.NotFound);
        }
    }
}
