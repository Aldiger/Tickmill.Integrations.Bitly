using System.Text.Json.Serialization;

namespace Tickmill.Integrations.Bitly.Core.Integrations.Dto
{

    public class BitlyShortenUrlDto
    {
        [JsonPropertyName("link")]
        public string Link { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("long_url")]
        public string LongUrl { get; set; }
        [JsonPropertyName("archived")]
        public bool Archived { get; set; }
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }
        [JsonPropertyName("custom_bitlinks")]
        public List<string> CustomBitlinks { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        [JsonPropertyName("deeplinks")]
        public List<BitlyDeeplinkDto> Deeplinks { get; set; }
    }


}
