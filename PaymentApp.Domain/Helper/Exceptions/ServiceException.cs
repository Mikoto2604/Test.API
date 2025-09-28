using PaymentApp.Domain.Framework;
using System.Net;

namespace PaymentApp.Domain.Helper.Exceptions
{
    public class ServiceException: Exception
    {
        public ResultCode resultCode { get; set; } = ResultCode.InternalServerError;

        public HttpStatusCode httpCode { get; set; } = HttpStatusCode.InternalServerError;

        public ServiceException(ResultCode resultCode, HttpStatusCode httpCode, string message): base(message) 
        {
            this.resultCode = resultCode;
            this.httpCode = httpCode;
        }
    }
}
