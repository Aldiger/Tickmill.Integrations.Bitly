using Tickmill.Integrations.Bitly.Core.Integrations.Dto;

namespace Tickmill.Integrations.Bitly.Core.Integrations
{
    public interface IBitlyService
    {
        Task<BitlyShortenUrlDto> ShortenUrl(BitlyShortenUrlRequestDto request, CancellationToken token = default);
    }
}
