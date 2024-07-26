using System.Net;

namespace Business.Exceptions.ContactUsExceptions;

public sealed class ContactUsNotFoundException : Exception, IBaseException
{
    public ContactUsNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
