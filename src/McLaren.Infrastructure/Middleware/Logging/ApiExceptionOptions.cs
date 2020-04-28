using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace McLaren.Infrastructure.Middleware.Logging
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }  
    }
}