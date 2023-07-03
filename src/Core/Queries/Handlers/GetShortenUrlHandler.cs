using Bitly.Core.Dto;
using Bitly.Core.Integrations;
using Bitly.Core.Integrations.Dto;
using Bitly.Core.Options;
using Convey.CQRS.Queries;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tickmill.Integrations.Bitly.Core.Exceptions;

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

            if (!IsValidUrl(query.Url))
                throw new UrlCannotBeInvalidException();

            var request = new BitlyShortenUrlRequestDto
            {
                Domain = query.Domain,
                LongUrl = query.Url,
                GroupGuid = _bitlyOptions.GroupId
            };

            var result = await _bitlyService.ShortenUrl(request, token);

            return result.ToDto();
        }

        private bool IsValidUrl(string url)
        {
            Uri? uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
