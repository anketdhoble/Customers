﻿using System.Text.Json.Serialization;

namespace Customers.Web.Models
{
    public class CreateCustomer
    {
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
