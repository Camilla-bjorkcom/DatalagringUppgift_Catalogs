using Shared_Catalogs.Entities.Customers;

namespace Shared_Catalogs.Interfaces
{
    public interface ICustomerPhoneNumbersEntity
    {
        int ContactId { get; set; }
        ContactInformationEntity ContactInformation { get; set; }
        string PhoneNumber { get; set; }
    }
}