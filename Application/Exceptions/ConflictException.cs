using System.Net;

namespace Application.Exceptions;

public class ConflictException : Exception
{
    public List<string>? ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }

    public ConflictException(string message, List<string>? errors = default, HttpStatusCode statusCode = HttpStatusCode.Conflict)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }
}