using System.Net;

namespace Business.Exceptions.CategoryExceptions;

public sealed class CategoryAlreadyExistException : Exception, IBaseException
{
    public CategoryAlreadyExistException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.Conflict;
    public string ErrorMessage { get; }
}
