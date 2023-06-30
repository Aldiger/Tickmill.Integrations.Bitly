using Bitly.Core.Integrations.Dto;
using Bitly.Core.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using Tickmill.Integrations.Bitly.Core.Exceptions;

namespace Bitly.Core.Integrations
{
    public class BitlyService : IBitlyService
    {
        private readonly ILogger<BitlyService> _logger;
        private readonly BitlyOptions _bitlyOptions;
        private readonly HttpClient _httpClient;

        public BitlyService(
            IOptions<BitlyOptions> bitlyOptions,
            ILogger<BitlyService> logger,
            HttpClient httpClient
            )
        {

            _httpClient = httpClient;
            _logger = logger;
            _bitlyOptions = bitlyOptions.Value;

        }

        public async Task<BitlyShortenUrlDto> ShortenUrl(BitlyShortenUrlRequestDto request, CancellationToken token)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"/shorten",request, token);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<BitlyShortenUrlDto>();
                }
                else
                {
                    throw new ServiceApiException(response.ReasonPhrase);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError("Bitly api service error: {exception}", exception);
                throw new ServiceApiException(exception.Message);
            }
        }
    }
}
