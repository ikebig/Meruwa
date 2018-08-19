using System;

namespace Meruwa
{
    /// <summary>
    /// Manages all <see cref="IStreamingProvider"/>. This should be injected as a singleton.
    /// </summary>
    public interface IStreamingProviderManager
    {
        void Add(string name, IStreamingProvider provider);
        void Add(string name, Func<IStreamingProvider> providerFactory);
        void Remove(string name);
        IStreamingProvider Get(string name);
    }
}
