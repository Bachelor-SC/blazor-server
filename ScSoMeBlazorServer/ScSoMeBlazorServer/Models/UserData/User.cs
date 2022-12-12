using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScSoMeBlazorServer.Models.UserData
{
    public class User 
    {
        public User(string username, string hashedPassword, int? subscriptionLevel)
        {
            Username = username;
            HashedPassword = hashedPassword;
            SubscriptionLevel = subscriptionLevel;
        }

        public User()
        {
        }

        [JsonPropertyName("username")]
        [Required]
        public string Username { get; set; }

        [JsonPropertyName("hashedPassword")]
        [Required]
        [MaxLength(256, ErrorMessage = "Password is too long, please enter a shorter one")]
        public string HashedPassword { get; set; }

        [JsonPropertyName("subscriptionLevel")]
        public int? SubscriptionLevel { get; set; }
    }
}
