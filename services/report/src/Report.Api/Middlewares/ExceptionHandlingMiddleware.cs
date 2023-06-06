using Newtonsoft.Json;
using Report.Domain.Exceptions;
using System.Net;

namespace Report.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env)
        {
            ex = ex.GetBaseException();

            var statusCode = HttpStatusCode.InternalServerError;
            string message = ex.Message;
            string? stackTrace = env.IsProduction() ? null : ex.StackTrace;

            switch (ex)
            {
                case ValidationException _:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                default:
                    break;
            }

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(
                new 
                {
                    Message = message,
                    StackTrace = stackTrace
                }
            ));
        }
    }
}
