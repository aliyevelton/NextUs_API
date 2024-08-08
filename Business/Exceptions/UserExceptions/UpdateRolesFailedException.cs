using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Business.Exceptions.UserExceptions;

public class UpdateRolesFailedException : Exception, IBaseException
{
    public UpdateRolesFailedException(IEnumerable<IdentityError> errors)
    {
        foreach (var error in errors)
        {
            Console.WriteLine(error.Description);
        }
        ErrorMessage = string.Join(" ", errors.Select(e => e.Description));
    }

    public int StatusCode => (int)HttpStatusCode.Unauthorized;

    public string ErrorMessage { get; }
}
