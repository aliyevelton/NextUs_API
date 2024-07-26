using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Business.Exceptions.UserExceptions;

public sealed class UserNotFoundByEmailException : Exception, IBaseException
{
    public UserNotFoundByEmailException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
