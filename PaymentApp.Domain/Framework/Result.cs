using System.Text.Json.Serialization;

namespace PaymentApp.Domain.Framework
{
    /// <summary>
    /// Класс, для отображения результата
    /// </summary>
    
    public class Result
    {
        /// <summary>
        /// Код 
        /// </summary>
        public ResultCode Code { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; } = null!;

        public static Result Ok(string? message = null) => new()
        {
            Code = ResultCode.Ok,
            Message = message ?? ResultCode.Ok.ToString()
        };

        public static Result Error(ResultCode code, string message) => new()
        {
            Code = code,
            Message = message
        };
    }

    /// <summary>
    /// Обобщенный класс, для возврата результата
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>: Result
    {
        /// <summary>
        /// Отдаваемые данные
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public static Result<T> Ok(T entity) => new()
        {
            Code = ResultCode.Ok,
            Data = entity,
            Message = ResultCode.Ok.ToString()
        };

        public static Result<T> Error(ResultCode code) => new()
        {
            Code = code,
            Message = code.ToString()
        };
    }

    public enum ResultCode: int
    {
        Ok = 0,
        InternalServerError = 1,
        NotFound = 2,
        BadRequest = 3,
        Unauthorized = 4,
    }
}
