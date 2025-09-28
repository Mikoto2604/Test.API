using PaymentApp.Application.Services.Auth;
using PaymentApp.Domain.Framework;

namespace PaymentApp.Api.Extentions
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJwtService jwtService)
        {
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                string? authHeader = context.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();

                    var tokenResult = await jwtService.TokenIsActive(token);
                    if (tokenResult.Code == ResultCode.Ok)
                    {
                        if (!tokenResult.Data)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsJsonAsync(Result.Error(ResultCode.Unauthorized, "Token is inactive!"));
                            return;
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsJsonAsync(Result.Error(ResultCode.Unauthorized, "Token not found!"));
                        return;
                    }
                }
            }
            await _next(context);
        }
    }
}