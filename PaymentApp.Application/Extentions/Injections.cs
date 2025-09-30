using PaymentApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PaymentApp.Application.Services.Auth;
using Microsoft.Extensions.DependencyInjection;
using PaymentApp.Application.Services.Transaction;

namespace PaymentApp.Application.Extentions
{
    public static class Injections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            return services;
        }
    }
}
