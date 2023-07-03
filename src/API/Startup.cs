using Autofac;
using Bitly.Core.Queries;
using Convey.CQRS.Commands;
using Convey.MessageBrokers;
using System.Diagnostics.CodeAnalysis;
using Tickmill.Common.API;
using Tickmill.Common.EntityFramework;
using Tickmill.Integrations.Bitly.Core;

namespace Bitly.API
{
    [ExcludeFromCodeCoverage]
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(configuration, env)
        {
        }

        protected override void OnContainerConfigured(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(GetShortenUrl).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>));
        }

        protected override void SubscribeMessages(IBusSubscriber busSubscriber)
        {
        }

        protected override void OnServicesConfigured(IServiceCollection services)
        {
            base.OnServicesConfigured(services);
            services.AddSingleton<IDataSeeder, EmptyDataSeeder>();
            services.AddCore();
            ConfigureTestServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime, IDataSeeder dataSeeder)
        {
            ConfigureBase(app, env, applicationLifetime, dataSeeder);
        }

        protected virtual void ConfigureTestServices(IServiceCollection services)
        {
        }
        private class EmptyDataSeeder : IDataSeeder
        {
            public Task SeedAsync() => Task.CompletedTask;
        }
    }
}
