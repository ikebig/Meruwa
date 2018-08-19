using System.Threading.Tasks;

namespace Meruwa
{
    public interface IStreamingService
    {
        Task Process(StreamingRequest streamingContentRequest);
    }
}
