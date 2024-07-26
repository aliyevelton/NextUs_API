using System.Net;

namespace Business.Exceptions.AuthExceptions;

public sealed class LoginFailedException : Exception, IBaseException
{
    public LoginFailedException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.Unauthorized;

    public string ErrorMessage { get; }
}
