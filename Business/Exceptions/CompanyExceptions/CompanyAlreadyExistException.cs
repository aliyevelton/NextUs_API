using System.Net;

namespace Business.Exceptions.CompanyExceptions;

public sealed class CompanyAlreadyExistException : Exception, IBaseException
{
    public CompanyAlreadyExistException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}
