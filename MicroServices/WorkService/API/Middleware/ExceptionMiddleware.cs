using BLL.Helpers;
using Npgsql;
using System.Diagnostics;
using System.Net;

namespace API.Middleware
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
            var sw = Stopwatch.StartNew();

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Error: {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                await HandleExceptionAsync(context, ex);
            }
            finally
            {
                sw.Stop();
                logger.LogInformation(
                    "Request {Method} {Path} finished in {Elapsed}ms with status {Status}",
                    context.Request.Method,
                    context.Request.Path,
                    sw.ElapsedMilliseconds,
                    context.Response.StatusCode);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string message = ex.Message;

            switch (ex)
            {
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;

                case DuplicateNameException:
                    code = HttpStatusCode.Conflict;
                    break;

                case NpgsqlException:
                    code = HttpStatusCode.ServiceUnavailable;
                    message = "Database connection failed.";
                    break;

                case TimeoutException:
                    code = HttpStatusCode.RequestTimeout;
                    message = "The operation timed out.";
                    break;

                case InvalidOperationException:
                    code = HttpStatusCode.BadRequest;
                    break;

                case ArgumentException:
                    code = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = (int)code;

            var response = new
            {
                error = message,
                type = ex.GetType().Name,
                status = (int)code
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
