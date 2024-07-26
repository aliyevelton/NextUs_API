using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Business.Exceptions.UserExceptions;

public sealed class DeleteFailedException : Exception, IBaseException
{
    public DeleteFailedException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
