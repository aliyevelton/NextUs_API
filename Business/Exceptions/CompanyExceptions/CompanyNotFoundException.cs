using System.Net;

namespace Business.Exceptions.CompanyExceptions;

public sealed class CompanyNotFoundException : Exception, IBaseException
{
    public CompanyNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
