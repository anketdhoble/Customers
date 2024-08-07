using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Customers.Api.Models.V1.Request
{
    public class UpdateCutomerRequest
    {
        [Required, JsonPropertyName("id")]
        public required string Id { get; set; }
        [Required, JsonPropertyName("firstName")]
        public required string FirstName { get; set; }
        [Required, JsonPropertyName("lastName")]
        public required string LastName { get; set; }
        [Required, JsonPropertyName("city")]
        public required string City { get; set; }
        [Required, JsonPropertyName("emailId")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string EmailId { get; set; }
    }
}
