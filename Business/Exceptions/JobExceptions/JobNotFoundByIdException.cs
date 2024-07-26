using System.Net;

namespace Business.Exceptions.JobExceptions;

public sealed class JobNotFoundByIdException : Exception, IBaseException
{
    public JobNotFoundByIdException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
