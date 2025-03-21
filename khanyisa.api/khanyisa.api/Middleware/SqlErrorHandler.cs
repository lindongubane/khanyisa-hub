using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace khanyisa.api.Middleware;

public class SqlErrorHandler : IErrorHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<SqlErrorHandler> _logger;

    public SqlErrorHandler(IProblemDetailsService problemDetailsService, ILogger<SqlErrorHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> HandleAsync(HttpContext httpContext, Exception exception)
    {
        if (exception is not SqlException sqlException)
        {
            return false;
        }

        _logger.LogError(exception, "SQL Error occurred: {Message}", exception.Message);

        (int statusCode, string title, string detail) = sqlException.Number switch
        {
            2627 or 2601 => (StatusCodes.Status409Conflict, "Database Error", "Duplicate entry. This record already exists."),
            547 => (StatusCodes.Status400BadRequest, "Database Error", "Cannot complete action due to a related record constraint."),
            4060 => (StatusCodes.Status500InternalServerError, "Database Error", "Invalid database. Please check the configuration."),
            18456 => (StatusCodes.Status401Unauthorized, "Database Error", "Database login failed. Check credentials."),
            1205 => (StatusCodes.Status503ServiceUnavailable, "Database Error", "Database deadlock occurred. Please retry the request."),
            53 => (StatusCodes.Status503ServiceUnavailable, "Database Error", "Database connection error. Ensure the server is reachable."),
            _ => (StatusCodes.Status500InternalServerError, "Database Error", "A database error occurred. Please try again later.")
        };

        httpContext.Response.StatusCode = statusCode;
        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Type = $"https://httpstatuses.com/{statusCode}"
            }
        });
    }
}
