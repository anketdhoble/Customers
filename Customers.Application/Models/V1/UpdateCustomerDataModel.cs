namespace Customers.Application.Models.V1
{
    public class UpdateCustomerDataModel
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public DateTime UpdatedDateTime { get; set; } = DateTime.Now;
    }
}
