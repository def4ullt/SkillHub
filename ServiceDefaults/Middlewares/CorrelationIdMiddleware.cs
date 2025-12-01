using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace ServiceDefaults.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";
        private RequestDelegate next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string correlationId;

            if (context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var headerValue) && !string.IsNullOrWhiteSpace(headerValue))
            {
                correlationId = headerValue.ToString();
            }
            else
            {
                correlationId = Guid.NewGuid().ToString();
            }

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.TryAdd(CorrelationIdHeaderName, correlationId);
                    return Task.CompletedTask;
                });

                context.Items["CorrelationId"] = correlationId;

                var logger = context.RequestServices.GetService(typeof(ILogger<CorrelationIdMiddleware>)) as ILogger<CorrelationIdMiddleware>;

                logger?.LogInformation("Request started - CorrelationId: {CorrelationId}, Path: {Path}, Method: {Method}", correlationId, context.Request.Path, context.Request.Method);

                try
                {
                    await next(context);
                }
                finally
                {
                    logger?.LogInformation("Request completed - CorrelationId: {CorrelationId}, StatusCode: {StatusCode}", correlationId, context.Response.StatusCode);
                }
            }
        }
    }
}
