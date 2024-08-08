using System.Net;

namespace Business.Exceptions.CourseExceptions;

public sealed class CourseTypeNotFoundException : Exception, IBaseException
{
    public CourseTypeNotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
