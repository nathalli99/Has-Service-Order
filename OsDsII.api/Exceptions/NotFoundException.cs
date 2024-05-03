using System.Net;

namespace OsDsII.api.Exceptions
{
    public sealed class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base
            (
                "HSO-002",
                message,
                HttpStatusCode.NotFound,
                StatusCodes.Status404NotFound,
                null,
                DateTimeOffset.UtcNow,
                null
            )
        { }

        public NotFoundException(string message, string uriPath) : base
    (
        "HSO-002",
        message,
        HttpStatusCode.NotFound,
        StatusCodes.Status404NotFound,
        uriPath,
        DateTimeOffset.UtcNow,
        null
    )
        { }
    }
}
