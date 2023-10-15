using GloboDelivery.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace GloboDelivery.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ExceptionHandlerMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
        {
            _next = next;
            _problemDetailsFactory = problemDetailsFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/problem+json";

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetails = _problemDetailsFactory
                .CreateProblemDetails(context, StatusCodes.Status500InternalServerError, title: "An error occurred", detail: exception.Message);

            switch (exception)
            {
                case BadRequestException badRequestException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case NotFoundException notFoundException:
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Resource not found";
                    break;
                case ValidationException validationException:
                    problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    problemDetails.Title = "Validation error";
                    problemDetails.Detail = "One or more validation errors occurred.";
                    problemDetails.Extensions["errors"] = validationException.ValidationErrors;
                    break;
            }

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
