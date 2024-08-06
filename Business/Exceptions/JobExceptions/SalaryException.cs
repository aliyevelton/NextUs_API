using System.Net;

namespace Business.Exceptions.JobExceptions;

public sealed class SalaryException : Exception, IBaseException
{
    public SalaryException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
