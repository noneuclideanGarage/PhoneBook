using Serilog;

namespace PhoneBook.WebApi.Helpers.Middleware;

public class TaskCancelledExceptionCatchMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception _) when (_ is OperationCanceledException or TaskCanceledException)
        {
            Log.Information("Task cancelled.");
        }
    }
}