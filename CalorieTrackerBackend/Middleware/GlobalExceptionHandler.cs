using CalorieTracker.DTO;
using System.Net;

namespace CalorieTracker.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

        }
        catch ( Exception ex)
        {
            _logger.LogError(ex, "An error occured during request");
            var statusCode = ex switch
            {
                BadHttpRequestException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            var errors = new List<string>();
            var types = new List<string>();

            if (_env.IsDevelopment())
            {
                errors.Add(ex.Message);
                types.Add(ex.GetType().Name);
                
                var innerException = ex.InnerException;
                while (innerException is not null)
                {
                    errors.Add(innerException.Message);
                    types.Add(innerException.GetType().Name);
                    innerException = innerException.InnerException;
                }
            }
            else
            {
                // In Production, return a generic safe message so hackers don't see your database schema details
                errors.Add(statusCode == (int)HttpStatusCode.InternalServerError 
                    ? "An unexpected error occurred on the server." 
                    : ex.Message);
                types.Add(ex.GetType().Name);
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var apiResponse = ApiResponse<string>.Failure(errors, types, statusCode);

            await context.Response.WriteAsJsonAsync(apiResponse);

        }
    }
}