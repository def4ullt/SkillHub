using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ServiceDefaults.Middlewares
{
    public class GatewayLoggingMiddleware
    {
        private RequestDelegate _next;
        private ILogger<GatewayLoggingMiddleware> _logger;

        public GatewayLoggingMiddleware(RequestDelegate next, ILogger<GatewayLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            string correlationId = context.Items["CorrelationId"]?.ToString() ?? "unknown";
            string method = context.Request.Method;
            string path = context.Request.Path;
            string clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            _logger.LogInformation(
                "Gateway incoming request",
                new
                {
                    Method = method,
                    Path = path,
                    CorrelationId = correlationId,
                    ClientIp = clientIp,
                    Timestamp = DateTime.UtcNow
                });

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                sw.Stop();

                _logger.LogError(ex,"Gateway error",
                    new
                    {
                        CorrelationId = correlationId,
                        Method = method,
                        Path = path,
                        DurationMs = sw.ElapsedMilliseconds
                    });

                throw;
            }

            sw.Stop();

            long? responseLength = context.Response.ContentLength;

            _logger.LogInformation("Gateway response completed",
                new
                {
                    CorrelationId = correlationId,
                    StatusCode = context.Response.StatusCode,
                    DurationMs = sw.ElapsedMilliseconds,
                    ResponseSize = responseLength
                });
        }
    }
}
