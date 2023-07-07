using Autofac.Extensions.DependencyInjection;
using Tickmill.Integrations.Bitly.API;
using System.Diagnostics.CodeAnalysis;
using Tickmill.Common.API;
using Tickmill.Common.Authentication.Security;
using Tickmill.Common.Logging;
using Tickmill.Common.Metrics;
using Tickmill.Common.Vault;

namespace Tickmill.Integrations.Bitly.API
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static Task Main(string[] args) => CreateHostBuilder(args).Build().RunAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    ConfigStartup.SetConfig(config, hostingContext);
                })
                .ConfigureWebHostDefaults(builder => builder
                    .UseStartup<Startup>()
                    .UseCertificates()
                    .UseLogging()
                    .UseVault()
                    .UseAppMetrics()
                );
    }
}