using System.Text.Json.Serialization;

namespace Tickmill.Integrations.Bitly.Core.Integrations.Dto
{
    public class BitlyShortenUrlRequestDto
    {
        [JsonPropertyName("long_url")]
        public string LongUrl { get; set; }
        [JsonPropertyName("domain")]
        public string Domain { get; set; }
        [JsonPropertyName("group_guid")]
        public string GroupGuid { get; set; }
    }
}
