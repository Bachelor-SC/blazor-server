using System.Text.Json.Serialization;

namespace ScSoMeBlazorServer.Models.Location
{
    public class Location
    {
        [JsonPropertyName("Address")]
        public string address { get; set; }

        [JsonPropertyName("PostalCode")]
        public int postalCode { get; set; }

        [JsonPropertyName("City")]
        public string city { get; set; }
    }
}
