using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Tickmill.Integrations.Bitly.Tests.Integration.Common;
using Shouldly;
using Xunit;

namespace Tickmill.Integrations.Bitly.Tests.Integration.Controller
{
    [ExcludeFromCodeCoverage]
    public class HomeControllerTests : ControllerTestsBase
    {
        [Fact]
        public async Task get_empty_endpoint_should_return_bitly_integration_name()
        {
            var response = await GetAsync("");
            response.IsSuccessStatusCode.ShouldBeTrue();
            var content = await response.Content.ReadAsStringAsync();
            content.ShouldBe("bitly-integration");
        }

        #region Arrange

        public HomeControllerTests(CustomWebApplicationFactory<BitlyTestStartup> factory) : base(factory)
        {
        }

        #endregion
    }
}

