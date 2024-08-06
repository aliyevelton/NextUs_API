using System.Net;

namespace Business.Exceptions.JobExceptions;

public sealed class JobTypeNotFoundException : Exception, IBaseException
{
    public JobTypeNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
