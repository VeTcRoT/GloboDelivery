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

            var problemDetails = _problemDetailsFactory
                .CreateProblemDetails(context, StatusCodes.Status500InternalServerError, title: "An error occurred", detail: exception.Message);

            switch (exception)
            {
                case BadRequestException badRequestException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case NotFoundException notFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Resource not found";
                    break;
                case ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    problemDetails.Title = "Validation error";
                    problemDetails.Extensions["validationErrors"] = validationException.ValidationErrors;
                    break;
            }

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
