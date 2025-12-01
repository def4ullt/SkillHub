using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ServiceDefaults.Middlewares
{
    public class CorrelationIdDelegatingHandler : DelegatingHandler
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";
        private IHttpContextAccessor httpContextAccessor;

        public CorrelationIdDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string? correlationId = httpContextAccessor.HttpContext?.Items["CorrelationId"] as string;

            if (!string.IsNullOrWhiteSpace(correlationId))
            {
                if (!request.Headers.Contains(CorrelationIdHeaderName))
                {
                    request.Headers.Add(CorrelationIdHeaderName, correlationId);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
