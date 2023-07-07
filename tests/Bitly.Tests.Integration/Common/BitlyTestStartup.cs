using Tickmill.Integrations.Bitly.API;
using Tickmill.Integrations.Bitly.Core.Integrations;
using Tickmill.Integrations.Bitly.Tests.Integration.Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
namespace Tickmill.Integrations.Bitly.Tests.Integration.Common
{
    [ExcludeFromCodeCoverage]
    public class BitlyTestStartup : Startup
    {
        public BitlyTestStartup(IConfiguration configuration, IWebHostEnvironment env) : base(configuration, env)
        {
        }

        protected override void ConfigureTestServices(IServiceCollection services)
        {
            services.AddSingleton<IBitlyService, TestBitlyService>();
        }
    }
}
