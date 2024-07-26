using System.Net;

namespace Business.Exceptions.CommonExceptions;

public sealed class FileSizeException : Exception, IBaseException
{
    public FileSizeException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}
