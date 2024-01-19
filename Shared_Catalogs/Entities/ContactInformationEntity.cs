using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared_Catalogs.Entities;

public class ContactInformationEntity
{

    [Key]
    [ForeignKey(nameof(CustomersEntity))]
    public int CustomerId { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Email { get; set; } = null!;

    public string? LinkedIn {  get; set; }

    //Hämtar en kontakt
    public virtual CustomersEntity Customer { get; set; } = null!;

    //En kontaktinformation kan vara kopplad till en eller flera telefonnummer
    public virtual ICollection<CustomerPhoneNumbersEntity> PhoneNumbers { get; set; } = new HashSet<CustomerPhoneNumbersEntity>();
}
