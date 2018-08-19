using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Meruwa.Http.Middlewares
{
    public class StreamingEndpointMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MeruwaOptions _options;

        public StreamingEndpointMiddleware(RequestDelegate next, MeruwaOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext httpContext, IStreamingService streamingService)
        {
            PathString startPath = $"{_options.Path}/push";
            PathString remainingPath;
            if (httpContext.Request.Path.StartsWithSegments(startPath, out remainingPath))
            {
                CancellationToken cancellationToken = httpContext.RequestAborted;
                var segments = remainingPath.Value.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (segments == null || !segments.Any() || string.IsNullOrWhiteSpace(segments[0]))
                {
                    var innerEx = new Exception($"The expected path must start with '{startPath}/{{channel}}', where {{channel}} represents the streaming channel name");
                    throw new ArgumentNullException("channel", innerEx);
                }

                var channelName = segments[0];
                var streamingRequest = new HttpStreamingRequest
                {
                    Name = channelName,
                    HttpContext = httpContext,
                    CancellationToken = cancellationToken
                };

                await streamingService.Process(streamingRequest);
            }
            else
            {
                await _next.Invoke(httpContext);
            }
        }
    }
}
