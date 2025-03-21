using Microsoft.AspNetCore.Mvc;

namespace khanyisa.api.Middleware;

public class ProblemExceptionHandler : IErrorHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public ProblemExceptionHandler(IProblemDetailsService problemDetailsService) => _problemDetailsService = problemDetailsService;

    public async ValueTask<bool> HandleAsync(HttpContext httpContext, Exception exception)
    {
        if (exception is ProblemException problemException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = problemException.Error,
                    Detail = problemException.Message,
                    Type = "https://httpstatuses.com/400"
                }
            });
        }

        return false;
    }
}
