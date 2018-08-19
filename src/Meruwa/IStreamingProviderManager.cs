using System;

namespace Meruwa
{
    /// <summary>
    /// Manages all <see cref="IStreamingProvider"/>. This should be injected as a singleton.
    /// </summary>
    public interface IStreamingProviderManager
    {
        void Add(string channel, IStreamingProvider provider);
        void Add(string channel, Func<IStreamingProvider> providerFactory);
        void Remove(string channel);
        IStreamingProvider Get(string channel);
    }
}
