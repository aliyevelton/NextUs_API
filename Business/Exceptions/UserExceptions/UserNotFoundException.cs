using Business.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Business.Exceptions.UserExceptions;

public sealed class UserNotFoundException : Exception, IBaseException
{
    public int StatusCode { get; }

    public string ErrorMessage { get; }
    public UserNotFoundException(string message, HttpStatusCode? statusCode = null) : base(message)
    {
        if (statusCode is null)
        {
            StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else
        {
            StatusCode = (int)statusCode;
        }
        ErrorMessage = message;
    }
}
