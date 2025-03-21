namespace khanyisa.api.Middleware;

public interface IErrorHandler
{
    ValueTask<bool> HandleAsync(HttpContext httpContext, Exception exception);
}
