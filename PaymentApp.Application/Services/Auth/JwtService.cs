using System.Net;
using System.Text;
using System.Security.Claims;
using PaymentApp.Domain.Framework;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PaymentApp.Application.Dto.Auth;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using PaymentApp.Domain.Helper.Exceptions;
using PaymentApp.Domain.Abstractions.UnitOfWork;


namespace PaymentApp.Application.Services.Auth
{
    public class JwtService : IJwtService
    {
        ILogger<JwtService> _logger;
        private readonly string _jwtKey;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration, IUnitOfWork unitOfWork, ILogger<JwtService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _jwtKey = _configuration["JwtSettings:SecretKey"] ?? throw new Exception("JWT SecretKey not configured");
        }

        public async Task<Result> DeactivateToken(string token)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await _unitOfWork.UserTokenRepository.GetByTokenAsync(token);
                if (result.Code == ResultCode.Ok)
                {
                    if (!result.Data.IsActive)
                        throw new ServiceException(ResultCode.Unauthorized, HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString());
                    result.Data.IsActive = false;
                    result.Data.RevokedAt = DateTime.Now;
                    await _unitOfWork.CommitAsync();
                    return Result.Ok();
                }
                throw new ServiceException(ResultCode.NotFound, HttpStatusCode.NotFound, "Token not found!");
            }
            catch (ServiceException serviceException)
            {
                _logger.LogError($"{serviceException.Message}");
                await _unitOfWork.RollbackAsync();
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message}");
                await _unitOfWork.RollbackAsync();
                throw new ServiceException(ResultCode.InternalServerError, HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString());
            }
        }

        public Result<TokenDto> GenerateToken(List<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtKey);
            var lifetimeMinutesStr = _configuration["JwtSettings:LifetimeMinutes"];
            if (!int.TryParse(lifetimeMinutesStr, out var lifetimeMinutes))
                throw new Exception("JWT LifetimeMinutes not configured or invalid");
            var expiry = DateTime.Now.Add(TimeSpan.FromMinutes(lifetimeMinutes));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiry,
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenData = new TokenDto
            {
                Token = tokenHandler.WriteToken(token),
                Expiry = expiry,
                IsActive = true
            };
            return Result<TokenDto>.Ok(tokenData);
        }

        public async Task<Result<bool>> TokenIsActive(string token)
        {
            var result = await _unitOfWork.UserTokenRepository.GetByTokenAsync(token);
            if (result.Code == ResultCode.Ok)
            {
                if (!result.Data.IsActive)
                    return Result<bool>.Ok(false);
                return Result<bool>.Ok(true);
            }
            return Result<bool>.Error(ResultCode.NotFound);
        }
    }
}
