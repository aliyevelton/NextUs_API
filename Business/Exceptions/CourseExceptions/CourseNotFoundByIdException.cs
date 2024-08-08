using System.Net;

namespace Business.Exceptions.CourseExceptions;

public sealed class CourseNotFoundByIdException : Exception, IBaseException
{
    public CourseNotFoundByIdException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
