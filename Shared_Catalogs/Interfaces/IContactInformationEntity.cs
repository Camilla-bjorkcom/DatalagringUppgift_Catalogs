using Shared_Catalogs.Entities.Customers;

namespace Shared_Catalogs.Interfaces
{
    public interface IContactInformationEntity
    {
        CustomersEntity Customer { get; set; }
        Guid CustomerId { get; set; }
        string Email { get; set; }
        string? LinkedIn { get; set; }
        ICollection<CustomerPhoneNumbersEntity> PhoneNumbers { get; set; }
    }
}