using System.Net;

namespace Business.Exceptions.JobApplicationExceptions;

public sealed class JobApplicationNotFoundException : Exception, IBaseException
{
    public JobApplicationNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
