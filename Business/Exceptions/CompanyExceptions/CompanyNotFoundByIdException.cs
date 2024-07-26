using System.Net;

namespace Business.Exceptions.CompanyExceptions;

public sealed class CompanyNotFoundByIdException : Exception, IBaseException
{
    public CompanyNotFoundByIdException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}