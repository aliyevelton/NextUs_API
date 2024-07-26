using Business.Exceptions;
using Business.Exceptions.JobExceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace API.Extensions;

public static class ExceptionHandlerExtension
{
    public static void AddExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerFeature>();

                var statusCode = (int)HttpStatusCode.InternalServerError;
                string message = "Unexpected error occured...";

                if (feature?.Error is IBaseException)
                {
                    IBaseException exception = (IBaseException)feature.Error; 
                    statusCode = exception.StatusCode;
                    message = exception.ErrorMessage;
                }

                var responseBody = new
                {
                    StatusCode = statusCode,
                    Message = message
                };

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(responseBody);
                await context.Response.CompleteAsync();
            });
        });
    }
}
