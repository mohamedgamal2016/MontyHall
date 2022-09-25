using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private const int BufferSize = 1024;

        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = await FormatRequestAsync(context.Request);

            _logger.LogInformation(request);

            await _next(context);

            var response = FormatResponse(context.Request, context.Response);

            _logger.LogInformation(response);
        }

        private async Task<string> FormatRequestAsync(HttpRequest request)
        {
            request.EnableBuffering();

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, false, BufferSize, true))
            {
                var bodyAsText = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return $"{request.Method} request made to {request.GetDisplayUrl()} with body {bodyAsText}";
            }
        }

    private string FormatResponse(HttpRequest request, HttpResponse response)
        {
            return $"Returning {response.StatusCode} response for {request.Method} request made to {request.GetDisplayUrl()}";
        }
    }
}
