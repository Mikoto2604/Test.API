using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PaymentApp.Application.Dto.Auth;
using PaymentApp.Domain.Abstractions.Repositories;
using PaymentApp.Domain.Abstractions.UnitOfWork;
using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Framework;
using PaymentApp.Domain.Helper.Exceptions;
using System.Net;
using System.Security.Claims;

namespace PaymentApp.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        ILogger<AuthService> _logger;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserTokenRepository _userTokenRepository;

        public AuthService(IUserTokenRepository userTokenRepository, IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher, 
                           IJwtService jwtService, ILogger<AuthService> logger)
        {
            _logger = logger;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _userTokenRepository = userTokenRepository;
        }
        public async Task<Result<TokenDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var user = await _unitOfWork.UserRepository.GetByLoginAsync(loginRequestDto.Login);
                if (user.Code == ResultCode.Ok && user.Data != null)
                {
                    if (!user.Data.IsBlocked)
                    {
                        var verificationResult = _passwordHasher.VerifyHashedPassword(user.Data, user.Data.PasswordHash, loginRequestDto.Password);
                        if (verificationResult == PasswordVerificationResult.Success)
                        {
                            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, loginRequestDto.Login),
                            new Claim(ClaimTypes.Name, loginRequestDto.Login)
                        };
                            var resultToken = _jwtService.GenerateToken(claims);
                            var userToken = new UserToken
                            {
                                Token = resultToken.Data.Token,
                                Expiry = resultToken.Data.Expiry,
                                IsActive = resultToken.Data.IsActive,
                                UserId = user.Data.Id,
                                User = user.Data
                            };

                            await _userTokenRepository.AddAsync(userToken);
                            await _unitOfWork.CommitAsync();
                            return Result<TokenDto>.Ok(resultToken.Data);
                        }
                        else
                        {
                            await RegisterFailedLoginAsync(user.Data.Id);
                            await _unitOfWork.CommitAsync();
                            throw new ServiceException(ResultCode.BadRequest, HttpStatusCode.BadRequest, "Invalid password!");
                        }
                    }
                    throw new ServiceException(ResultCode.BadRequest, HttpStatusCode.BadRequest, "Account is locked!");
                }
                else
                {
                    throw new ServiceException(ResultCode.BadRequest, HttpStatusCode.BadRequest, "Invalid credentials");
                }
            }
            catch(ServiceException serviceException)
            {
                _logger.LogError($"{serviceException.Message}");
                await _unitOfWork.RollbackAsync();
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                await _unitOfWork.RollbackAsync();
                throw new ServiceException(ResultCode.InternalServerError, HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString());
            }
        }

        public async Task RegisterFailedLoginAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user.Code == ResultCode.Ok && user.Data != null)
            {
                user.Data.FailedLoginAttempts += 1;
                if (user.Data.MaxLoginAttempts == user.Data.FailedLoginAttempts)
                    user.Data.IsBlocked = true;
            }
        }

        public async Task<Result> LogoutAsync(string token) =>  await _jwtService.DeactivateToken(token); 
    }
}
