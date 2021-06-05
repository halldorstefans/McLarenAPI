using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Net;
using System.Threading.Tasks;

namespace McLaren.Infrastructure.Middleware.Logging
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly ApiExceptionOptions _options;

        public ApiExceptionMiddleware(ApiExceptionOptions options, RequestDelegate next, 
            ILogger<ApiExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _options = options;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = new ApiError
            {
                Id = Guid.NewGuid().ToString(),
                Status = (short)HttpStatusCode.InternalServerError,
                Title = "Some kind of error occurred in the API.  Please use the id and contact our " +
                        "support team if the problem persists."
            };
            
            _options.AddResponseDetails?.Invoke(context, exception, error);            

            var innerExMessage = GetInnermostExceptionMessage(exception);

            _logger.LogError(exception, "Something went wrong! " + innerExMessage  + $" -- { error.Id }.");           

            var result = JsonSerializer.Serialize(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }

        private string GetInnermostExceptionMessage(Exception exception)
        {
            if (exception.InnerException != null)
                return GetInnermostExceptionMessage(exception.InnerException);

            return exception.Message;
        }
    }
}