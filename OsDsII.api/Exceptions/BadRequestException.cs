using System.Net;

namespace OsDsII.api.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) :
        base
            (
                "HSO-004", // código identificador de erros
                message,
                HttpStatusCode.BadRequest,
                StatusCodes.Status400BadRequest,
                null,
                DateTimeOffset.UtcNow,
                null
            )
        { }

        public BadRequestException(string message, string uriPath) : 
        base
            (
                "HSO-004",
                message,
                HttpStatusCode.BadRequest,
                StatusCodes.Status400BadRequest,
                uriPath,
                DateTimeOffset.UtcNow,
                null
            )
        { }
    }
}
