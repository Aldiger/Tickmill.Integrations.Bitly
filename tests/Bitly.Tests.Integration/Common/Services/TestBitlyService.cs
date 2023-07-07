using Tickmill.Integrations.Bitly.Core.Integrations;
using Tickmill.Integrations.Bitly.Core.Integrations.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Tickmill.Integrations.Bitly.Tests.Integration.Common.Services
{
    public class TestBitlyService : IBitlyService
    {
        public Task<BitlyShortenUrlDto> ShortenUrl(BitlyShortenUrlRequestDto request, CancellationToken token)
        {
            return Task.FromResult(new BitlyShortenUrlDto
            {
                LongUrl = "bit.ly/abc"
            });
        }
    }
}
