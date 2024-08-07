using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customers.Persistence.Context
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Making Id as a Primary Key
            modelBuilder.Entity<Customer>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Customer>()
               .Property(x => x.Id)
               .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Customer>()
                .Property(b => b.CreatedDateTime)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
