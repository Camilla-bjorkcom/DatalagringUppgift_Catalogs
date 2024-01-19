using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared_Catalogs.Entities;

public class CustomerPhoneNumbersEntity
{
    [Key]
    [ForeignKey(nameof(ContactInformationEntity))]
    public int ContactId { get; set; }

    [Key]
    public string PhoneNumber { get; set; } = null!;

    //Hämtar en kontaktinformation
    public virtual ContactInformationEntity ContactInformation { get; set; } = null!;
}
