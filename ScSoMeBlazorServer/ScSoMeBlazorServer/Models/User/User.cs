using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScSoMeBlazorServer.Models.User
{

    public class User
    {
        [JsonPropertyName("Username")]
        [Required]
        public string username { get; set; }

        [JsonPropertyName("Password")]
        [Required]
        [MaxLength(256, ErrorMessage = "Password is too long, please enter a shorter one")]
        public string password { get; set; }


        [JsonPropertyName("SubscriptionLevel")]
        public int subscriptionLevel { get; set; }

        //As example
        //1 = Standard user
        //2 = Paid User

    }
}