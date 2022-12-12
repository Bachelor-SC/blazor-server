using System.Text.Json.Serialization;

namespace ScSoMeBlazorServer.Models
{
    public class SocialMedia
    {
        public SocialMedia(string username, string facebook, string linkedIn, string instagram)
        {
            Username = username;
            Facebook = facebook;
            LinkedIn = linkedIn;
            Instagram = instagram;
        }

        public SocialMedia()
        {
        }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("facebook")]
        public string Facebook { get; set; }

        [JsonPropertyName("linkedIn")]
        public string LinkedIn { get; set; }
        
        [JsonPropertyName("instagram")]
        public string Instagram { get; set; }
    }
}
