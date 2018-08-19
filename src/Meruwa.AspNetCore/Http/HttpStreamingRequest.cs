using Microsoft.AspNetCore.Http;

namespace Meruwa.Http
{
    public class HttpStreamingRequest : StreamingRequest
    {
        public HttpContext HttpContext { get; set; }
    }
}
