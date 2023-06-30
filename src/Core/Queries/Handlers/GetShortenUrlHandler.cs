using Bitly.Core.Dto;
using Bitly.Core.Integrations;
using Bitly.Core.Integrations.Dto;
using Bitly.Core.Options;
using Convey.CQRS.Queries;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bitly.Core.Queries.Handlers
{
    public class GetShortenUrlHandler : IQueryHandler<GetShortenUrl, ShortenUrlDto>
    {
        private readonly IBitlyService _bitlyService;
        private readonly BitlyOptions _bitlyOptions;
        private ILogger<GetShortenUrlHandler> _logger;

        public GetShortenUrlHandler(
            IBitlyService bitlyService,
            IOptions<BitlyOptions> options,
            ILogger<GetShortenUrlHandler> logger
            ) { 
            _bitlyService = bitlyService;
            _bitlyOptions = options.Value;
            _logger = logger;
        }

        public async Task<ShortenUrlDto> HandleAsync(GetShortenUrl query, CancellationToken token)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (string.IsNullOrWhiteSpace(query.Url))
                throw new ArgumentNullException("Url cannot be empty.");

            var request = new BitlyShortenUrlRequestDto
            {
                Domain = query.Domain,
                LongUrl = query.Url,
                GroupGuid = _bitlyOptions.GroupId
            };

            try
            {
                var result = await _bitlyService.ShortenUrl(request, token);

                return result.ToDto();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return default;
            }
        }
    }
}
