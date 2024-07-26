using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Business.Exceptions.AuthExceptions;

public sealed class EmailConfirmationException : Exception, IBaseException
{
    public EmailConfirmationException(IEnumerable<IdentityError> errors)
    {
        ErrorMessage = string.Join(" ", errors.Select(e => e.Description));
    }
    public EmailConfirmationException(string message) : base(message)
    {
        ErrorMessage = message;
    }
    public int StatusCode => (int)HttpStatusCode.BadRequest;

    public string ErrorMessage { get; } 
}
