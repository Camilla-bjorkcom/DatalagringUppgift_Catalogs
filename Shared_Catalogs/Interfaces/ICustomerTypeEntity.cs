using Shared_Catalogs.Entities.Customers;

namespace Shared_Catalogs.Interfaces
{
    public interface ICustomerTypeEntity
    {
        ICollection<CustomersEntity> Customers { get; set; }
        string CustomerType { get; set; }
        int Id { get; set; }
    }
}