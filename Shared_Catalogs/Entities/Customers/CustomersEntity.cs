using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared_Catalogs.Interfaces;

namespace Shared_Catalogs.Entities.Customers;

public class CustomersEntity 
{
    [Key]
    public int Id { get; set; }
    public int AddressesId { get; set; }
    public AddressesEntity Addresses { get; set; } = null!;
    public int CustomerTypeId { get; set; }
    public CustomerTypeEntity CustomerType { get; set; } = null!;

    public CustomerProfilesEntity CustomerProfiles { get; set; } = null!;
    public ContactInformationEntity ContactInformation { get; set; } = null!;

}
