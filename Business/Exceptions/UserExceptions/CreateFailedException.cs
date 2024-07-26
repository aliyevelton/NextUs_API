using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Business.Exceptions.UserExceptions;

public sealed class CreateFailedException : Exception, IBaseException
{
    public CreateFailedException(IEnumerable<IdentityError> errors)
    {
        foreach (var error in errors)
        {
            if (error.Code == "DuplicateUserName" || error.Code == "DuplicateEmail")
            {
                ErrorMessage = "User already exists with this email";
                return;
            }
        }
        ErrorMessage = string.Join(" ", errors.Select(e => e.Description));
    }

    public int StatusCode => (int)HttpStatusCode.Unauthorized;

    public string ErrorMessage { get; }
}
