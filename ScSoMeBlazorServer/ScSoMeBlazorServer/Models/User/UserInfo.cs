using System.Text.Json.Serialization;
using ScSoMeBlazorServer.Models.Location;


namespace ScSoMeBlazorServer.Models.User
{

    //Contains all the information a user would have, but with added info
    public class UserInfo : User
    {

        [JsonPropertyName("SocialMedia")]
        public Dictionary<string, string> socialMedia { get; set; }

        [JsonPropertyName("Description")]
        public string description { get; set; }

        [JsonPropertyName("ProfilePicture")]
        public string profilePicture { get; set; }

        [JsonPropertyName("CoverPicture")]
        public string coverPicture { get; set; }

        [JsonPropertyName("Location")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers for your phone number")]
        public Location.Location location { get; set; }

        [JsonPropertyName("PhoneNumber")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers for your phone number")]
        public string phoneNumber { get; set; }

        [JsonPropertyName("Email")]
        public string email { get; set; }

        [JsonPropertyName("Connections")]
        public List<UserInfo> connections { get; set; }

        [JsonPropertyName("ExternalLinks")]
        public Dictionary<string, string> externalLinks { get; set; }


    }
}
