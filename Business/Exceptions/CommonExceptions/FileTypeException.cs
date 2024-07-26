using System.Net;

namespace Business.Exceptions.CommonExceptions;

public sealed class FileTypeException : Exception, IBaseException
{
    public FileTypeException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.BadRequest;

    public string ErrorMessage { get; }
}
