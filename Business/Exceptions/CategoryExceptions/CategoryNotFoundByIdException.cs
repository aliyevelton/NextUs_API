using System.Net;

namespace Business.Exceptions.CategoryExceptions;

public sealed class CategoryNotFoundByIdException : Exception, IBaseException
{
    public CategoryNotFoundByIdException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
