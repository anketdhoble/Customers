using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [Column("created_datetime")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [Column("updated_dateTime")]
        public DateTime? UpdatedDateTime { get; set; }
    }
}
