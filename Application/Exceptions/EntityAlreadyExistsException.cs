using System.Net;

namespace Application.Exceptions;

public class EntityAlreadyExistsException : Exception
{
    public List<string>? ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }

    public EntityAlreadyExistsException(string message, List<string>? errors = default, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }
}
