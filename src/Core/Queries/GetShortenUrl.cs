using Tickmill.Integrations.Bitly.Core.Dto;
using Convey.CQRS.Queries;

namespace Tickmill.Integrations.Bitly.Core.Queries
{
    public record GetShortenUrl(string Url, string Domain = "bit.ly") : IQuery<ShortenUrlDto>
    {
    }
}
