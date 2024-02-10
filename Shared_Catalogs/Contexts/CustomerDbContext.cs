using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Shared_Catalogs.Entities.Customers;

namespace Shared_Catalogs.Contexts;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
    }

    public virtual DbSet<AddressesEntity> Addresses { get; set; }
    public virtual DbSet<ContactInformationEntity> ContactInformation { get; set; }
    public virtual DbSet<CustomerPhoneNumbersEntity> CustomerPhoneNumbers { get; set; }
    public virtual DbSet<CustomerProfilesEntity> CustomerProfiles { get; set; }

    public virtual DbSet<CustomersEntity> Customers { get; set; }
    public virtual DbSet<CustomerTypeEntity> CustomerType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerTypeEntity>()
            .HasIndex(x => x.CustomerType)
            .IsUnique();

        modelBuilder.Entity<ContactInformationEntity>()
           .HasIndex(x => x.Email)
           .IsUnique();

        modelBuilder.Entity<CustomerPhoneNumbersEntity>()
           .HasKey(x => new { x.ContactInformationId, x.PhoneNumber });

    }
}
