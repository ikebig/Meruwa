using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Meruwa
{
    
    public class StreamingProviderManager : IStreamingProviderManager
    {
        private readonly ConcurrentDictionary<string, IStreamingProvider> _providers = new ConcurrentDictionary<string, IStreamingProvider>();

        public void Add(string name, IStreamingProvider provider)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            _providers.AddOrUpdate(name, provider, (n, p) => provider);
        }

        public void Add(string name, Func<IStreamingProvider> providerFactory)
        {
            var provider = providerFactory.Invoke();
            Add(name, provider);
        }

        public void Remove(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            IStreamingProvider provider;
            _providers.TryRemove(name, out provider);
            provider = null;//TODO: implement IDisposable on IStreamingProviders
        }

        public IStreamingProvider Get(string name)
        {
            if (!_providers.ContainsKey(name))
                throw new MeruwaException($"A channel with name '{name}' does not exist.");

            return _providers[name];
        }
    }
}
