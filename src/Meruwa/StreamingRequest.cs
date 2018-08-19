using System.Threading;

namespace Meruwa
{
    public class StreamingRequest
    {
        public string Name { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }    
}
