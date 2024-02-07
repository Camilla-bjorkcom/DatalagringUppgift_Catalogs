using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Shared_Catalogs.Entities.Customers;

public class CustomerPhoneNumbersEntity
{
    [Key]
    public int Id { get; set; }

    [Key]
    public string PhoneNumber { get; set; } = null!;

    public int ContactId { get; set; }

    //Hämtar en kontaktinformation
    public virtual ContactInformationEntity ContactInformation { get; set; } = null!;
}
