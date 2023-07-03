using Bitly.Core.Dto;
using Convey.CQRS.Queries;

namespace Bitly.Core.Queries
{
    public record GetShortenUrl(string Url, string Domain = "bit.ly") : IQuery<ShortenUrlDto>
    {
    }
}
