using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScSoMeBlazorServer.Models.UserData
{
    public class UserInfo : User
    {
        public UserInfo(SocialMedia socialMedia, string description, string profilePicture, string coverPicture, Location location, string phonenumber, string email, ExternalLink externalLinks)
        {
            SocialMedia = socialMedia;
            Description = description;
            ProfilePicture = profilePicture;
            CoverPicture = coverPicture;
            Location = location;
            Phonenumber = phonenumber;
            Email = email;
            ExternalLinks = externalLinks;
        }

        public UserInfo()
        {
        }

        [JsonPropertyName("socialMedia")]
        public SocialMedia SocialMedia { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("profilePicture")]
        public string ProfilePicture { get; set; }

        [JsonPropertyName("coverPicture")]
        public string CoverPicture { get; set; }

        [JsonPropertyName("location")]
        //[Required(ErrorMessage = "Location is required")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers for your phone number")]
        public Location Location { get; set; }

        [JsonPropertyName("phonenumber")]
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers for your phone number")]
        public string Phonenumber { get; set; }

        [JsonPropertyName("email")]
        [Required]
        public string Email { get; set; }

        //[JsonPropertyName("Connections")]
        //public List<UserInfo> Connections { get; set; }

        [JsonPropertyName("link")]
        public ExternalLink ExternalLinks { get; set; }

        [JsonIgnore]
        public bool isEditing { get; set; }
    }
}
