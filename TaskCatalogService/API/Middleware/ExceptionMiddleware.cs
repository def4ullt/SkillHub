using System.Diagnostics;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using BLL.Helpers;

namespace CatalogService.API.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate next;
        private ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing request: {Method} {Path}", context.Request.Method, context.Request.Path);

                context.Response.ContentType = "application/json";

                var statusCode = HttpStatusCode.InternalServerError;
                string message = ex.Message;

                switch (ex)
                {
                    case NotFoundException:
                        statusCode = HttpStatusCode.NotFound;
                        break;
                    case DuplicateNameException:
                        statusCode = HttpStatusCode.Conflict;
                        break;
                    case DbUpdateException dbEx:
                        statusCode = HttpStatusCode.BadRequest;
                        if (dbEx.InnerException is PostgresException pgEx)
                        {
                            message = $"Database error: {pgEx.SqlState} - {pgEx.MessageText}";
                        }
                        break;
                }

                context.Response.StatusCode = (int)statusCode;

                var response = new
                {
                    error = message,
                    type = ex.GetType().Name
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            finally
            {
                stopwatch.Stop();
                logger.LogInformation("Request {Method} {Path} finished in {ElapsedMilliseconds} ms with status {StatusCode}",
                    context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds,
                    context.Response.StatusCode);
            }
        }
    }
}