using System.Net;

namespace OsDsII.api.Exceptions
{
    public sealed class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base
            (
                "HSO-002",
                message,
                HttpStatusCode.Conflict,
                StatusCodes.Status409Conflict,
                null,
                DateTimeOffset.UtcNow,
                null
            )
        { }

        public NotFoundException(string message, string uriPath) : base
    (
        "HSO-002",
        message,
        HttpStatusCode.Conflict,
        StatusCodes.Status409Conflict,
        uriPath,
        DateTimeOffset.UtcNow,
        null
    )
        { }
    }
}
