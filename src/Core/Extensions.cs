using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Tickmill.Common;
using Bitly.Core.Options;
using Bitly.Core.Integrations;

[assembly: InternalsVisibleTo("Tickmill.Integrations.Bitly.API")]
[assembly: InternalsVisibleTo("Tickmill.Integrations.Bitly.Tests.Integration")]
[assembly: InternalsVisibleTo("Tickmill.Integrations.Bitly.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Tickmill.Integrations.Bitly.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                services.Configure<BitlyOptions>(configuration.GetSection("bitly"));
            }
            var configOptions = services.GetOptions<BitlyOptions>("bitly");

            services.AddHttpClient<IBitlyService, BitlyService>(client =>
            {
                client.BaseAddress = new Uri(configOptions.BaseApiUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {configOptions.ApiKey}");
            });

            return services;
        }
    }
}