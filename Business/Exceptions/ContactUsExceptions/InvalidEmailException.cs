using System.Net;

namespace Business.Exceptions.ContactUsExceptions;

public sealed class InvalidEmailException : Exception, IBaseException
{
    public InvalidEmailException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}
