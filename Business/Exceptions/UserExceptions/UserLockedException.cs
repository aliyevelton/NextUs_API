using System.Net;

namespace Business.Exceptions.UserExceptions;

public sealed class UserLockedException : Exception, IBaseException
{
    public UserLockedException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.Unauthorized;

    public string ErrorMessage { get; }
}
