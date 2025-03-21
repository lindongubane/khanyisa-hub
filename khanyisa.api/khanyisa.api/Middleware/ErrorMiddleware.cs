using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace khanyisa.api.Middleware;

public class ErrorMiddleware : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly IEnumerable<IErrorHandler> _errorHandlers;

    public ErrorMiddleware(IProblemDetailsService problemDetailsService, IEnumerable<IErrorHandler> errorHandlers)
    {
        _problemDetailsService = problemDetailsService;
        _errorHandlers = errorHandlers;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        foreach (IErrorHandler handler in _errorHandlers)
        {
            if (await handler.HandleAsync(httpContext, exception))
            {
                return true;
            }
        }

        // Fallback to generic error
        return await WriteProblemResponse(httpContext, StatusCodes.Status500InternalServerError, "Unexpected Error", "An unexpected error occurred.");
    }

    private async ValueTask<bool> WriteProblemResponse(HttpContext httpContext, int statusCode, string title, string detail)
    {
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

// Custom problem exception
public class ProblemException : Exception
{
    public string Error { get; }
    public string Message { get; }

    public ProblemException(string error, string message) : base(message)
    {
        Error = error;
        Message = message;
    }
}

// Validation failure response models
public class ValidationFailureResponse
{
    public required IEnumerable<ValidationResponse> Errors { get; init; }
}

public class ValidationResponse
{
    public required string PropertyName { get; init; }
    public required string Message { get; init; }
}








































//public class ErrorMiddleware : IExceptionHandler
//{
//    private readonly IProblemDetailsService _problemDetailsService;

//    public ErrorMiddleware(IProblemDetailsService problemDetailsService) => _problemDetailsService = problemDetailsService;

//    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
//    {
//        if (exception is ValidationException validattionException)
//        {
//            var validationFailureResponse = new ValidationFailureResponse
//            {
//                Errors = validattionException.Errors.Select(x => new ValidationResponse
//                {
//                    PropertyName = x.PropertyName,
//                    Message = x.ErrorMessage
//                })
//            };

//            var problemDetials = new ProblemDetails
//            {
//                Title = validattionException.GetType().Name,
//                Type = "https://httpstatuses.com/400",
//                Status = StatusCodes.Status400BadRequest,
//                Extensions =
//                {
//                    ["error"] = validationFailureResponse.Errors
//                }
//            };

//            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
//            {
//                HttpContext = httpContext,
//                ProblemDetails = problemDetials
//            });
//        }

//        if (exception is SqlException sqlException)
//        {
//            return await GetSqlError(sqlException, httpContext);
//        }


//        if (exception is ProblemException problemException)
//        {
//            var problemDetials = new ProblemDetails
//            {
//                Status = StatusCodes.Status400BadRequest,
//                Title = problemException.Error,
//                Detail = problemException.Message,
//                Type = "https://httpstatuses.com/400",
//            };

//            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
//            {
//                HttpContext = httpContext,
//                ProblemDetails = problemDetials
//            });
//        }

//        return true;
//    }


//    public async ValueTask<bool> GetSqlError(SqlException sqlException, HttpContext httpContext)
//    {
//        switch (sqlException.Number)
//        {
//            // Unique constraint violation
//            case 2627:
//            case 260:
//                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
//                return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
//                {
//                    HttpContext = httpContext,
//                    ProblemDetails = new ProblemDetails
//                    {
//                        Status = StatusCodes.Status409Conflict,
//                        Title = "Database Error",
//                        Detail = "Duplicate entry. This record already exists.",
//                        Type = "https://httpstatuses.com/400",
//                    }
//                });
//            // Foreign key constraint violation
//            case 547:
//                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
//                return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
//                {
//                    HttpContext = httpContext,
//                    ProblemDetails = new ProblemDetails
//                    {
//                        Status = StatusCodes.Status400BadRequest,
//                        Title = "Database Error",
//                        Detail = "Cannot complete action due to a related record constraint.",
//                        Type = "https://httpstatuses.com/400",
//                    }
//                });
//            // Invalid database
//            case 4060:
//                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
//                return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
//                {
//                    HttpContext = httpContext,
//                    ProblemDetails = new ProblemDetails
//                    {
//                        Status = StatusCodes.Status500InternalServerError,
//                        Title = "Database Error",
//                        Detail = "Invalid database. Please check the configuration.",
//                        Type = "https://httpstatuses.com/400",
//                    }
//                });
//            // Login failed
//            case 18456:
//                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
//                return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
//                {
//                    HttpContext = httpContext,
//                    ProblemDetails = new ProblemDetails
//                    {
//                        Status = StatusCodes.Status401Unauthorized,
//                        Title = "Database Error",
//                        Detail = "Database login failed. Check credentials.",
//                        Type = "https://httpstatuses.com/400",
//                    }
//                });
//            // Deadlock victim
//            case 1205:
//                httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
//                return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
//                {
//                    HttpContext = httpContext,
//                    ProblemDetails = new ProblemDetails
//                    {
//                        Status = StatusCodes.Status503ServiceUnavailable,
//                        Title = "Database Error",
//                        Detail = "Database deadlock occurred. Please retry the request.",
//                        Type = "https://httpstatuses.com/400",
//                    }
//                });
//            // Network-related error
//            case 53:
//                httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
//                return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
//                {
//                    HttpContext = httpContext,
//                    ProblemDetails = new ProblemDetails
//                    {
//                        Status = StatusCodes.Status503ServiceUnavailable,
//                        Title = "Database Error",
//                        Detail = "Database connection error. Ensure the server is reachable.",
//                        Type = "https://httpstatuses.com/400",
//                    }
//                });
//            default:
//                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
//                return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
//                {
//                    HttpContext = httpContext,
//                    ProblemDetails = new ProblemDetails
//                    {
//                        Status = StatusCodes.Status500InternalServerError,
//                        Title = "Database Error",
//                        Detail = "A database error occurred. Please try again later.",
//                        Type = "https://httpstatuses.com/400",
//                    }
//                });
//        }

//    }
//}

//public class ProblemException : Exception
//{
//    public string Error { get; }
//    public string Message { get; }

//    public ProblemException(string error, string message) : base(message)
//    {
//        Error = error;
//        Message = message;
//    }
//}

//public class ValidationFailureResponse
//{
//    public required IEnumerable<ValidationResponse> Errors { get; init; }
//}

//public class ValidationResponse
//{
//    public required string PropertyName { get; init; }

//    public required string Message { get; init; }
//}
