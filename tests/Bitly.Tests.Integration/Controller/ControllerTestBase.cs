using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tickmill.Integrations.Bitly.API;
using Tickmill.Integrations.Bitly.Tests.Integration.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace Tickmill.Integrations.Bitly.Tests.Integration.Controller
{
    [ExcludeFromCodeCoverage]
    [Collection("sequential")]
    public abstract class ControllerTestsBase : IDisposable,
        IClassFixture<CustomWebApplicationFactory<BitlyTestStartup>>
    {
        private readonly WebApplicationFactory<BitlyTestStartup> _factory;
        private readonly HttpClient _client;
        private string _route;

        protected void SetRoute(string route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                _route = string.Empty;
                return;
            }

            if (route.StartsWith("/"))
            {
                route = route.Substring(1, route.Length - 1);
            }

            if (route.EndsWith("/"))
            {
                route = route.Substring(0, route.Length - 1);
            }

            _route = $"{route}/";
        }

        protected static T Map<T>(object data) => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(data));

        protected Task<HttpResponseMessage> GetAsync(string endpoint)
            => _client.GetAsync(GetEndpoint(endpoint));

        protected Task<HttpResponseMessage> PostAsync<T>(string endpoint, T command)
            => _client.PostAsync(GetEndpoint(endpoint), GetPayload(command));

        protected Task<HttpResponseMessage> PutAsync<T>(string endpoint, T command)
            => _client.PutAsync(GetEndpoint(endpoint), GetPayload(command));

        protected Task<HttpResponseMessage> DeleteAsync(string endpoint)
            => _client.DeleteAsync(GetEndpoint(endpoint));

        protected Task<HttpResponseMessage> SendAsync(string method, string endpoint)
            => SendAsync(GetMethod(method), endpoint);

        protected Task<HttpResponseMessage> SendAsync(HttpMethod method, string endpoint)
            => _client.SendAsync(new HttpRequestMessage(method, GetEndpoint(endpoint)));

        private static HttpMethod GetMethod(string method)
            => method.ToUpperInvariant() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "PATCH" => HttpMethod.Patch,
                "DELETE" => HttpMethod.Delete,
                _ => null
            };

        private string GetEndpoint(string endpoint) => $"{_route}{endpoint}";

        private static StringContent GetPayload(object value)
            => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

        protected static async Task<T> ReadAsync<T>(HttpResponseMessage response)
            => JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());

        #region Arrange

        protected ControllerTestsBase(CustomWebApplicationFactory<BitlyTestStartup> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                var projectDir = Directory.GetCurrentDirectory();
                var configPath = Path.Combine(projectDir, "appsettings.json");
                var testConfigPath = Path.Combine(projectDir, "appsettings.tests.json");
                builder.UseSolutionRelativeContentRoot("../", "Tickmill.Integrations.Bitly.sln");
                builder.ConfigureAppConfiguration(conf =>
                {
                    conf.AddJsonFile(configPath);
                    conf.AddJsonFile(testConfigPath);
                });
                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                });
            });
            _client = _factory.CreateClient();
        }

        public virtual void Dispose()
        {
            _client.Dispose();
        }

        #endregion
    }
}

