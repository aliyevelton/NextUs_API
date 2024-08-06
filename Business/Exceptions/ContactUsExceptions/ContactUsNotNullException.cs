using System.Net;

namespace Business.Exceptions.ContactUsExceptions;

public sealed class ContactUsNotNullException : Exception, IBaseException
{
    public ContactUsNotNullException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}
