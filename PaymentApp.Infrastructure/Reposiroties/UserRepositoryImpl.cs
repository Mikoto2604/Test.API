using Microsoft.EntityFrameworkCore;
using PaymentApp.Domain.Abstractions.Repositories;
using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Framework;
using PaymentApp.Infrastructure.Drivers.DbContexts;


namespace PaymentApp.Infrastructure.Reposiroties
{
    public class UserRepositoryImpl : IUserRepository
    {
        private readonly PgDbContext _pgDbContext;
        public UserRepositoryImpl(PgDbContext pgDbContext)=> _pgDbContext = pgDbContext;

        public async Task<Result<User>> GetByLoginAsync(string login)
        {
            var user = await _pgDbContext.Users.Where(x => x.Login == login).FirstOrDefaultAsync();
            if (user != null)
                return Result<User>.Ok(user);
            return Result<User>.Error(ResultCode.NotFound);
        }

        public async Task<Result<User>> GetByIdAsync(int Id)
        {
            var user = await _pgDbContext.Users.Where(x=>x.Id == Id).FirstOrDefaultAsync();
            if (user != null)
                return Result<User>.Ok(user);
            return Result<User>.Error(ResultCode.NotFound);
        }
    }
}
