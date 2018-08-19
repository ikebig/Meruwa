using Meruwa.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Meruwa
{
    public static class ServiceCollectionExtenstions
    {
        public static IServiceCollection AddMeruwa(this IServiceCollection services)
        {
            services.AddSingleton<IStreamingProviderManager, StreamingProviderManager>();
            services.AddScoped<IStreamingService, HttpStreamingService>();

            return services;
        }
    }
}
