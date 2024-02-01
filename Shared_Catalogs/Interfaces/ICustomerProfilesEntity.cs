using Shared_Catalogs.Entities.Customers;

namespace Shared_Catalogs.Interfaces
{
    public interface ICustomerProfilesEntity
    {
        CustomersEntity Customer { get; set; }
        Guid CustomerId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string? ProfileImg { get; set; }
    }
}