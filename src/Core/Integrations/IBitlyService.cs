using Bitly.Core.Integrations.Dto;

namespace Bitly.Core.Integrations
{
    public interface IBitlyService
    {
        Task<BitlyShortenUrlDto> ShortenUrl(BitlyShortenUrlRequestDto request, CancellationToken token);
    }
}
