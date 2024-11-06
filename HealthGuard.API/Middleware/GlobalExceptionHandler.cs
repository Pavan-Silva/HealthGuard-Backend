using HealthGuard.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace HealthGuard.API.Middleware
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Exception occurred: {message}", exception.Message);

            int statusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            var result = new
            {
                Title = exception.GetType().Name,
                Status = statusCode,
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
            return true;
        }
    }
}
