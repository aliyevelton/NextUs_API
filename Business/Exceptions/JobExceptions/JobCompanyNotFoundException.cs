using System.Net;

namespace Business.Exceptions.JobExceptions;

public sealed class JobCompanyNotFoundException : Exception, IBaseException
{
    public JobCompanyNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
