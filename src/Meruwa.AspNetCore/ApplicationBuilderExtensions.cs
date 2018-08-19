using Meruwa.Http.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Meruwa
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMeruwa(this IApplicationBuilder app, IDictionary<string,IStreamingProvider> channels, MeruwaOptions options)
        {
            var streamingProviderManager = app.ApplicationServices.GetService<IStreamingProviderManager>();

            if (channels != null && channels.Any())
            {
                foreach (var channel in channels)
                {
                    streamingProviderManager.Add(channel.Key, channel.Value);
                }
            }

            app.UseMiddleware<StreamingEndpointMiddleware>(options);            

            return app;
        }

        public static IApplicationBuilder UseMeruwa(this IApplicationBuilder app, IDictionary<string, IStreamingProvider> channels)
        {
            return app.UseMeruwa(channels, MeruwaOptions.Default);
        }

        public static IApplicationBuilder UseMeruwa(this IApplicationBuilder app, Func<IDictionary<string, IStreamingProvider>> channelFactory, MeruwaOptions options)
        {
            var channels = channelFactory.Invoke();
            return app.UseMeruwa(channels, options);
        }

        public static IApplicationBuilder UseMeruwa(this IApplicationBuilder app, Func<IDictionary<string, IStreamingProvider>> channelFactory)
        {
            return app.UseMeruwa(channelFactory, MeruwaOptions.Default);
        }
    }
}
