using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace PaymentApp.Api.Controllers
{
    public class BaseController: ControllerBase
    {
        protected string? UserLogin => User?.FindFirst(ClaimTypes.Name)?.Value;
        protected string Token => Request.Headers["Authorization"].ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);
    }
}
