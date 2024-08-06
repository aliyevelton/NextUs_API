using System.Net;

namespace Business.Exceptions.UserExceptions;

public sealed class UserAlreadyExistsException : Exception, IBaseException
{
    public UserAlreadyExistsException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
