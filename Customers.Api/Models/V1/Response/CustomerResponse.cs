using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Customers.Api.Models.V1.Response
{
    public class CustomerResponse
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }
        [JsonPropertyName("firstName")]
        public required string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public required string LastName { get; set; }
        [JsonPropertyName("city")]
        public required string City { get; set; }
        [JsonPropertyName("emailId")]
        public required string EmailId { get; set; }
    }
}
