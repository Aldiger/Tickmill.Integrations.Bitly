using System.Text.Json.Serialization;

namespace Tickmill.Integrations.Bitly.Core.Integrations.Dto
{
    public class BitlyDeeplinkDto
    {
        [JsonPropertyName("guid")]
        public string Guid { get; set; }
        [JsonPropertyName("bitlink")]
        public string Bitlink { get; set; }
        [JsonPropertyName("app_uri_path")]
        public string AppUriPath { get; set; }
        [JsonPropertyName("install_url")]
        public string InstallUrl { get; set; }
        [JsonPropertyName("app_guid")]
        public string AppGuid { get; set; }
        [JsonPropertyName("os")]
        public string Os { get; set; }
        [JsonPropertyName("install_type")]
        public string InstallType { get; set; }
        [JsonPropertyName("created")]
        public string Created { get; set; }
        [JsonPropertyName("modified")]
        public string Modified { get; set; }
        [JsonPropertyName("brand_guid")]
        public string BrandGuid { get; set; }
    }


}
