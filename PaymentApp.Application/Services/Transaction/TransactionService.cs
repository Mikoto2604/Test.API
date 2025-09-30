using System.Net;
using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Framework;
using Microsoft.Extensions.Logging;
using PaymentApp.Domain.Helper.Exceptions;
using PaymentApp.Domain.Abstractions.UnitOfWork;

namespace PaymentApp.Application.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        ILogger<TransactionService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public TransactionService(IUnitOfWork unitOfWork, ILogger<TransactionService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> PaymentAsync(string userLogin)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var user = await _unitOfWork.UserRepository.GetByLoginForUpdateAsync(userLogin);
                if(user.Code == ResultCode.Ok)
                {
                    if(user.Data.Balance < 1.1m)
                        throw new ServiceException(ResultCode.BadRequest, HttpStatusCode.BadRequest, "Insufficient funds!");
                    user.Data.Balance -= 1.1m;
                    var payment = new Payment
                    {
                        Amount = 1.1m,
                        UserId = user.Data.Id,
                        User = user.Data,
                        CreatedAt = DateTime.Now,
                    };
                   await _unitOfWork.PaymentRepository.CreatePaymentAsync(payment);
                   await _unitOfWork.CommitAsync();
                   return Result.Ok();
                }
                throw new ServiceException(ResultCode.BadRequest, HttpStatusCode.BadRequest, "Invalid credentials");
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
    }
}
