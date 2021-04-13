using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nasa.Business.Exceptions;

namespace Nasa.WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<CustomExceptionMiddleware>();
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = $"{exception.Message}{(exception.InnerException != null ? $" - {exception.InnerException.Message}" : string.Empty)}"; // "Unexpected error";
            var errorMessage = $"{context.Request.HttpContext.Connection.RemoteIpAddress}:{exception.Message}";
            _logger.LogError(errorMessage, exception);
            if (exception is CustomExceptionBase customException)
            {
                message = customException.Message;
                statusCode = customException.Code;
            }

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonSerializer.Serialize(new ExceptionResponse
            {
                Message = message,
                StatusCode = statusCode
            }));
        }
    }
}
