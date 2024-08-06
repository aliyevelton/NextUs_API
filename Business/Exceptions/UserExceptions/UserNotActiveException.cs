using System.Net;

namespace Business.Exceptions.UserExceptions;

public sealed class UserNotActiveException : Exception, IBaseException
{
    public UserNotActiveException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.Unauthorized;

    public string ErrorMessage { get; }
}
