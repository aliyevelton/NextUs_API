using System.Net;

namespace Business.Exceptions.JobExceptions;

public sealed class SalaryTypeNotFoundException : Exception, IBaseException
{
    public SalaryTypeNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
