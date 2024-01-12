namespace luizalabs.UserService.API.Middleware;

using Domain.Core.Exceptions;
using System.Net;
using System.Text.Json;

public class ErrorHandlerMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;
    
    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            _logger.LogCritical($"{error.GetType().Name} -> {error.Message}");

            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                AppException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new { code = response.StatusCode, message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}