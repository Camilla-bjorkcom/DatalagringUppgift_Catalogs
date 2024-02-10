using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Shared_Catalogs.Entities.Customers;

public class CustomerPhoneNumbersEntity
{
    [Key]
    public string PhoneNumber { get; set; } = null!;

    [Key]
    [ForeignKey (nameof(ContactInformation))]
    public int ContactInformationId { get; set; }

    //Hämtar en kontaktinformation
    public virtual ContactInformationEntity ContactInformation { get; set; } = null!;
}
