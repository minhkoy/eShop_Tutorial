using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler 
        (ILogger<CustomExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(
                "Error message: {exceptionMessage}, Time of occurence: {time}",
                exception.Message, DateTime.UtcNow);
            (string Detail, string Title, int StatusCode) = exception switch
            {
                InternalServerException => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError),
                NotFoundException => (exception.Message, exception.GetType().Name, StatusCodes.Status404NotFound),
                BadRequestException => (exception.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),
                ValidationException => (exception.Message, exception.GetType().Name, StatusCodes.Status422UnprocessableEntity),
                _ => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError)
            };

            var problemDetails = new ProblemDetails
            {
                Title = Title,
                Status = StatusCode,
                Detail = Detail,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }
    }
}
