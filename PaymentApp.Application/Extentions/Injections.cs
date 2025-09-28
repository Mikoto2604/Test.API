using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentApp.Application.Services.Auth;
using PaymentApp.Domain.Abstractions.Repositories;
using PaymentApp.Domain.Abstractions.UnitOfWork;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Extentions
{
    public static class Injections
    {
        public static IServiceCollection AddAppication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            return services;
        }
    }
}
