using Shared_Catalogs.Entities.Customers;

namespace Shared_Catalogs.Interfaces
{
    public interface ICustomersEntity
    {
        AddressesEntity Addresses { get; set; }
        int AddressId { get; set; }
        ContactInformationEntity ContactInformation { get; set; }
        CustomerProfilesEntity CustomerProfiles { get; set; }
        CustomerTypeEntity CustomerType { get; set; }
        int CustomerTypeId { get; set; }
        Guid Id { get; set; }
    }
}