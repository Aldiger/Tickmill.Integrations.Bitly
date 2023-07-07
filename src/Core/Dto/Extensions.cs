using Tickmill.Integrations.Bitly.Core.Integrations.Dto;

namespace Tickmill.Integrations.Bitly.Core.Dto
{
    internal static class Extensions
    {
        public static ShortenUrlDto ToDto(this BitlyShortenUrlDto bitlyShortenUrlDto)
        {
            if (bitlyShortenUrlDto == null)
                return default;

            return new ShortenUrlDto
            {
                Url = bitlyShortenUrlDto.Link,
            };
        }
    }
}
