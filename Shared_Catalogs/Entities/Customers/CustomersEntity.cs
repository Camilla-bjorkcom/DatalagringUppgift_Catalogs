using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared_Catalogs.Interfaces;

namespace Shared_Catalogs.Entities.Customers;

public class CustomersEntity : ICustomersEntity
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(AddressesEntity))]
    public int AddressId { get; set; }

    [ForeignKey(nameof(CustomerTypeEntity))]
    public int CustomerTypeId { get; set; }

    //Hämtar en Address, en Type, en Profile, en ContactInformation
    public virtual AddressesEntity Addresses { get; set; } = null!;
    public virtual CustomerTypeEntity CustomerType { get; set; } = null!;
    public virtual CustomerProfilesEntity CustomerProfiles { get; set; } = null!;
    public virtual ContactInformationEntity ContactInformation { get; set; } = null!;


}
