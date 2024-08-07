using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customers.Domain.Entities
{
    [Table("customer")]
    public partial class Customer : BaseEntity
    {
        [Required]
        [Column("first_name")]
        public required string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        public required string LastName { get; set; }
        [Required]
        [Column("city")]
        public required string City { get; set; }
        [Required]
        [Column("email_id")]
        public required string EmailId { get; set; }
    }
}
