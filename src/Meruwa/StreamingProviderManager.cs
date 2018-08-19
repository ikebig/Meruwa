using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Meruwa
{    
    public class StreamingProviderManager : IStreamingProviderManager
    {
        private readonly ConcurrentDictionary<string, IStreamingProvider> _providers = new ConcurrentDictionary<string, IStreamingProvider>();

        public void Add(string channel, IStreamingProvider provider)
        {
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            _providers.AddOrUpdate(channel, provider, (n, p) => provider);
        }

        public void Add(string channel, Func<IStreamingProvider> providerFactory)
        {
            var provider = providerFactory.Invoke();
            Add(channel, provider);
        }

        public void Remove(string channel)
        {
            if (string.IsNullOrWhiteSpace(channel))
                throw new ArgumentNullException(nameof(channel));

            IStreamingProvider provider;
            _providers.TryRemove(channel, out provider);
            provider = null;//TODO: implement IDisposable on IStreamingProviders
        }

        public IStreamingProvider Get(string channel)
        {
            if (!_providers.ContainsKey(channel))
                throw new MeruwaException($"A channel with name '{channel}' does not exist.");

            return _providers[channel];
        }
    }
}
