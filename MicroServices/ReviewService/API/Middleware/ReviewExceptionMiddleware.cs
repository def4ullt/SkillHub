using Domain.Helpers;
using System.Diagnostics;
using System.Net;

namespace API.Middleware
{
    public class ReviewExceptionMiddleware
    {
        private RequestDelegate next;
        private ILogger<ReviewExceptionMiddleware> logger;

        public ReviewExceptionMiddleware(RequestDelegate next, ILogger<ReviewExceptionMiddleware> logger)
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
                logger.LogError(ex, "Error processing {Method} {Path}", context.Request.Method, context.Request.Path);
                await HandleExceptionAsync(context, ex);
            }
            finally
            {
                sw.Stop();
                logger.LogInformation(
                    "Request {Method} {Path} finished in {Elapsed}ms with status {StatusCode}",
                    context.Request.Method,
                    context.Request.Path,
                    sw.ElapsedMilliseconds,
                    context.Response.StatusCode);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string message = ex.Message;

            switch (ex)
            {
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;

                case ForbiddenException:
                    code = HttpStatusCode.Forbidden;
                    break;

                case AlreadyExistsException:
                    code = HttpStatusCode.Conflict;
                    break;

                case ArgumentException:
                    code = HttpStatusCode.BadRequest;
                    break;

                case InvalidOperationException:
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

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
