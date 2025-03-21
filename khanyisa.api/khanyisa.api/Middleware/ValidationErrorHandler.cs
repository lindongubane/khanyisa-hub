using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace khanyisa.api.Middleware;

public class ValidationErrorHandler : IErrorHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public ValidationErrorHandler(IProblemDetailsService problemDetailsService) => _problemDetailsService = problemDetailsService;

    public async ValueTask<bool> HandleAsync(HttpContext httpContext, Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            var validationFailureResponse = new ValidationFailureResponse
            {
                Errors = validationException.Errors.Select(x => new ValidationResponse
                {
                    PropertyName = x.PropertyName,
                    Message = x.ErrorMessage
                })
            };
            
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = new ProblemDetails
                {
                    Title = "Validation Error",
                    Status = StatusCodes.Status400BadRequest,
                    Type = "https://httpstatuses.com/400",
                    Extensions = { ["errors"] = validationFailureResponse.Errors }
                }
            });
        }

        return false;
    }
}
