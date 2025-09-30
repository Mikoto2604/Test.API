using Microsoft.AspNetCore.Mvc;
using PaymentApp.Domain.Framework;
using Microsoft.AspNetCore.Authorization;
using PaymentApp.Application.Services.Transaction;

namespace PaymentApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController: BaseController
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService) => _transactionService = transactionService;

        [HttpPost("payment")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Payment()
        {
            if(string.IsNullOrEmpty(UserLogin))
                return Unauthorized();
            return Ok(await _transactionService.PaymentAsync(UserLogin));
        }
    }
}
