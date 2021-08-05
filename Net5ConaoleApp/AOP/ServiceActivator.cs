using Microsoft.Extensions.DependencyInjection;
using System;

namespace Net5ConaoleApp.AOP
{
    /// <summary>
    /// Add static service resolver to use when dependencies injection is not available
    /// ref → [Resolving instances with ASP.NET Core DI in static classes](https://www.davidezoccarato.cloud/resolving-instances-with-asp-net-core-di-in-static-classes/)
    /// </summary>
    public class ServiceActivator
    {
        internal static IServiceProvider _serviceProvider = null;

        /// <summary>
        /// Configure ServiceActivator with full serviceProvider
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Create a scope where use this ServiceActivator
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IServiceScope GetScope(IServiceProvider serviceProvider = null)
        {
            var provider = serviceProvider ?? _serviceProvider;
            return provider?
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
        }
    }
}
