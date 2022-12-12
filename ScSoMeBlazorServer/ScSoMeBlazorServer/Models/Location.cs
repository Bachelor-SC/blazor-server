using ScSoMeBlazorServer.Models.UserData;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScSoMeBlazorServer.Models
{


    public class Location
    {
        public Location(string username, string address, int postalCode, string city)
        {
            Username = username;
            Address = address;
            PostalCode = postalCode;
            City = city;
        }

        public Location()
        {
        }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("address")]
        [Required]
        public string Address { get; set; }

        [JsonPropertyName("postalCode")]
        [Required]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers allowed")]
        public int PostalCode { get; set; }

        [JsonPropertyName("city")]
        [Required]
        public string City { get; set; }
    }
}
