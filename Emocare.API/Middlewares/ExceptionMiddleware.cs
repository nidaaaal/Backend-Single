using Emocare.Shared.Helpers.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace VagueVault.Backend.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment hostEnvironment, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _environment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = GetStatusCode(exception),
                Message = GetUserFriendlyMessage(exception),
                Details = GetDetailedError(exception),
                CorrelationId = context.TraceIdentifier,
                Timestamp = DateTime.UtcNow
            };

            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }

        private int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                ApiException apiEx => apiEx.StatusCode,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        private string GetUserFriendlyMessage(Exception exception)
        {
            if (!_environment.IsDevelopment())
            {
                return exception switch
                {
                    ApiException apiEx => apiEx.Message,
                    _ => "An unexpected error occurred."
                };
            }

            return exception.Message;
        }

        private object GetDetailedError(Exception exception)
        {
            return new
            {
                exception.Message,
                exception.Source
            };
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
