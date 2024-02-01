using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared_Catalogs.Interfaces;

namespace Shared_Catalogs.Entities.Customers;

public class CustomerPhoneNumbersEntity : ICustomerPhoneNumbersEntity
{
    [Key]
    [ForeignKey(nameof(ContactInformationEntity))]
    public int ContactId { get; set; }

    [Key]
    public string PhoneNumber { get; set; } = null!;

    //Hämtar en kontaktinformation
    public virtual ContactInformationEntity ContactInformation { get; set; } = null!;
}
