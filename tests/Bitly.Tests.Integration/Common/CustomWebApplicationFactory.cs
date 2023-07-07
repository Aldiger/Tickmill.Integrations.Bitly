using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Tickmill.Integrations.Bitly.Tests.Integration.Common
{
    [ExcludeFromCodeCoverage]
    public class CustomWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
    {
        protected override IHostBuilder CreateHostBuilder()
             => Host.CreateDefaultBuilder(null)
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .UseEnvironment("tests")
                    .ConfigureWebHostDefaults(builder => builder.UseStartup<T>());
    }
}
