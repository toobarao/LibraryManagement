using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.ErrorHandling
{
    internal class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger)
        {
            _logger = _logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            // 2. Map exception types to specific HTTP Status Codes
            var statusCode = exception switch
            {
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError // Fallback for unexpected bugs
            };

            httpContext.Response.StatusCode = statusCode;

            // 3. Create a standardized Problem Details response
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = "An error occurred while processing your request.",
                Detail = exception.Message, // Note: In production, consider hiding raw message details
                Instance = httpContext.Request.Path
            };

            // 4. Write back to the response stream
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            // Return true to signal that this exception has been successfully handled 
            return true;
        }
    }
}
