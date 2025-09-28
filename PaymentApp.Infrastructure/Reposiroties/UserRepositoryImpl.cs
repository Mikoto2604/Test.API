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
        

        public async Task<Result<User>> GetByIdAsync(int Id)
        {
            var user = await _pgDbContext.User.Where(x=>x.Id == Id).FirstOrDefaultAsync();
            if (user != null)
                return Result<User>.Ok(user);
            return Result<User>.Error(ResultCode.NotFound);
        }

        public async Task<Result> UpdateBalace(int Id, decimal balance)
        {
            var user = await GetByIdAsync(Id);
            if (user.Data != null)
            {
                user.Data.Balance = balance;
                return Result.Ok();
            }
            return Result<User>.Error(ResultCode.NotFound);
        }
    }
}
