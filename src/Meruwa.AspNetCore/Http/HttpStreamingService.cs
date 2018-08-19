using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Formatting;

namespace Meruwa.Http
{
    public class HttpStreamingService : IStreamingService
    {
        #region Fields

        private readonly IStreamingProviderManager _streamingProviderManager;

        #endregion

        #region Ctor

        public HttpStreamingService(IStreamingProviderManager streamingProviderManager)
        {
            _streamingProviderManager = streamingProviderManager;
        }

        #endregion

        #region Method

        public Task Process(StreamingRequest streamingRequest)
        {
            var req = streamingRequest as HttpStreamingRequest;
            var httpContext = req.HttpContext;
            var provider = _streamingProviderManager.Get(req.Name);
            
            HttpResponseMessage res = new HttpResponseMessage();
            res.Content = new PushStreamContent((responseStream, httpContent, context) =>
            {
                provider.RegisterOutput(responseStream, req.CancellationToken);

            }, provider.MediaType);
            
            Timer t = provider.Timer;
            return res.Content.CopyToAsync(httpContext.Response.Body);
        }

        #endregion
    }
}
