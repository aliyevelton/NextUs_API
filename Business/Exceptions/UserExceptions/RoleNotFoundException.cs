using System.Net;

namespace Business.Exceptions.UserExceptions;

public sealed class RoleNotFoundException : Exception, IBaseException
{
    public RoleNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
