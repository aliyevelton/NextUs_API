using System.Net;

namespace Business.Exceptions.CourseExceptions;

public sealed class CourseCategoryNotFoundByIdException : Exception, IBaseException
{
    public CourseCategoryNotFoundByIdException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public int StatusCode => (int)HttpStatusCode.NotFound;

    public string ErrorMessage { get; }
}
