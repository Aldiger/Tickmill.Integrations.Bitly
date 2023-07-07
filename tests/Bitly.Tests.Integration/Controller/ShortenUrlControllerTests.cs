using System.Net.Http;
using System.Threading.Tasks;
using Tickmill.Integrations.Bitly.Tests.Integration.Common;
using Shouldly;
using Xunit;
using System.Diagnostics.CodeAnalysis;
using Tickmill.Integrations.Bitly.Core.Queries;
using Tickmill.Integrations.Bitly.Core.Dto;

namespace Tickmill.Integrations.Bitly.Tests.Integration.Controller
{
    [ExcludeFromCodeCoverage]
    public class ShortenUrlControllerTests : ControllerTestsBase
    {

        [Fact]
        public async Task get_shortenurl_should_return_url()
        {
            var request = new GetShortenUrl("https://www.youtube.com/watch?v=Vp1grPbjmac&t=203s");
            var response = await RequestShortenUrlAsync(request);

            response.IsSuccessStatusCode.ShouldBeTrue();
            var dto = await ReadAsync<ShortenUrlDto>(response);
            dto.ShouldNotBeNull();
        }

        #region Arrange

        private Task<HttpResponseMessage> RequestShortenUrlAsync(GetShortenUrl request)
        {
            var queryString = $"generate?{nameof(request.Url)}={request.Url}";
            var result = GetAsync(queryString);
            return result;
        }

        public ShortenUrlControllerTests(CustomWebApplicationFactory<BitlyTestStartup> factory) : base(factory)
        {
            SetRoute("shortenurl");
        }

        #endregion
    }
}

