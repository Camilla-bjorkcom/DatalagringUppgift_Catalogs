using Shared_Catalogs.Entities.Customers;

namespace Shared_Catalogs.Interfaces
{
    public interface IAddressesEntity
    {
        string City { get; set; }
        ICollection<CustomersEntity> Customers { get; set; }
        int Id { get; set; }
        string PostalCode { get; set; }
        string StreetName { get; set; }
    }
}