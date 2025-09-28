using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentApp.Domain.Abstractions.Repositories;
using PaymentApp.Domain.Abstractions.UnitOfWork;
using PaymentApp.Infrastructure.Drivers.DbContexts;
using PaymentApp.Infrastructure.Reposiroties;
using PaymentApp.Infrastructure.UnitOfWork;

namespace PaymentApp.Infrastructure.Extentions
{
    public static class Injections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWorkImpl>();
            services.AddScoped<IUserRepository, UserRepositoryImpl>();
            services.AddScoped<IPaymentRepository, PaymentRepositoryImpl>();
            services.AddScoped<IUserTokenRepository, UserTokenRepositoryImpl>();
            services.AddDbContext<PgDbContext>(options =>options.UseNpgsql(),ServiceLifetime.Scoped);
            return services;
        }
    }
}
