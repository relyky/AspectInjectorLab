using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Net5ConaoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = AppStartup();

            var app = ActivatorUtilities.CreateInstance<App>(host.Services);

            app.Run(args);
        }

        static IHost AppStartup()
        { 
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            IHost host = Host.CreateDefaultBuilder() 
                .ConfigureServices(ConfigureServices)
                .Build();

            /// 註冊 IServiceProvider 備用
            AOP.ServiceActivator.Configure(host.Services);

            return host;
        }

        static void ConfigureServices(HostBuilderContext context, IServiceCollection services) 
        {
            // 在此註冊 services 
            services.AddScoped<Services.RandomService>();
            services.AddScoped<Services.SumOpService>();
        }
    }
}
