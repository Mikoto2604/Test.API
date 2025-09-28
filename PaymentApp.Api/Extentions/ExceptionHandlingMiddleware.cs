using PaymentApp.Domain.Framework;
using PaymentApp.Domain.Helper.Exceptions;
using System.Net;

namespace PaymentApp.Api.Extentions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(ServiceException serviceException)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)serviceException.httpCode;
                await httpContext.Response.WriteAsJsonAsync(new Result
                {
                    Code = serviceException.resultCode,
                    Message = serviceException.Message,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(Result.Error(ResultCode.InternalServerError, ResultCode.InternalServerError.ToString()));
            }
        }
    }
}
