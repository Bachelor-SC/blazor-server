using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScSoMeBlazorServer.Models
{
    public class ExternalLink
    {
        public ExternalLink(string username, string link)
        {
            Username = username;
            Link = link;
        }

        public ExternalLink()
        {
        }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [Required]
        [JsonPropertyName("link")]
        public string Link { get; set; }
    }
}
