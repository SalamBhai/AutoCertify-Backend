namespace Domain.DomainException;

public class ArgumentCannotBeNullException : DomainException
{
    public ArgumentCannotBeNullException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}
